using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using RMS.Core.Controller.Print;
using RMS.Core.Enumerator;
using RMS.Core.Model;
using RMS.Core.Model.InfoContract;
using RMS.Core.Model.InfoCustomer;
using RMS.UI.Control.Customers;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms
{
    public partial class ContractForm : XtraForm
    {
        private Session _session { get; }
        private XPCollection<Contract> _contracts { get; set; }
        private XPCollection<CustomerFilter> XpCollectionCustomerFilter { get; set; }

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewContract);
            BVVGlobal.oFuncXpo.PressEnterGrid<Contract, ContractEdit>(gridViewContract);
        }

        public ContractForm(Session session)
        {
            InitializeComponent();
            FunctionalGridSetup();
            _session = session ?? DatabaseConnection.GetWorkSession();
            RenderingCustomerFilter();
        }

        /// <summary>
        /// Обрисовка пользовательских фильтров на форме.
        /// </summary>
        private void RenderingCustomerFilter()
        {
            XpCollectionCustomerFilter = new XPCollection<CustomerFilter>(_session);
            var sessionUser = _session.GetObjectByKey<User>(DatabaseConnection.User.Oid);
            panelControlFilter.Controls.Clear();

            var sizeH = 0;
            foreach (var customerFilter in XpCollectionCustomerFilter.OrderBy(o => o.Number))
            {
                if (customerFilter.IsVisibleContract)
                {
                    customerFilter.Users.Reload();
                    customerFilter.UserGroups.Reload();

                    if (customerFilter.Users.FirstOrDefault(f => f.User.Oid == sessionUser.Oid) != null ||
                            customerFilter.UserGroups.FirstOrDefault(f => sessionUser.UserGroups.FirstOrDefault(sf => sf.UserGroup.Oid == f.UserGroup.Oid) != null) != null)
                    {
                        var customersFilterControl = new CustomersFilterControl(customerFilter, true);
                        panelControlFilter.Controls.Add(customersFilterControl);
                        customersFilterControl.Dock = DockStyle.Top;
                        customersFilterControl.ButtonPress += CustomersFilterControl_ButtonPress;
                        customersFilterControl.DisplayCheck += CustomersFilterControl_DisplayCheck;
                        sizeH += customersFilterControl.Size.Height;
                    }
                }
            }
            accordionCustomersFilterControl.Height = sizeH;
            accordionCustomersFilterControl.Refresh();

            if (panelControlFilter.Controls.Count > 0)
            {
                accordTape.OptionsMinimizing.State = AccordionControlState.Normal;
            }
            else
            {
                accordTape.OptionsMinimizing.State = AccordionControlState.Minimized;
            }
        }

        private void CustomersFilterControl_DisplayCheck(object sender, bool isEditVisible)
        {
            if (isEditVisible)
            {
                RenderingCustomerFilter();
            }
        }

        private void CustomersFilterControl_ButtonPress(object sender, CustomerFilter customerFilter, bool isUse)
        {
            var customersFilterControl = sender as CustomersFilterControl;
            customersFilterControl?.Refresh();

            if (isUse)
            {
                foreach (var item in panelControlFilter.Controls)
                {
                    if (item is CustomersFilterControl customersFilter)
                    {
                        if (customersFilter.CheckButton.Checked == true)
                        {
                            customersFilter.CheckButton.Checked = false;
                        }
                    }
                }

                var groupOperator = customerFilter.GetGroupOperatorContract();
                _contracts.Filter = groupOperator;
            }
            else
            {
                _contracts.Filter = null;
            }
        }

        private async void TaskForm_Load(object sender, EventArgs e)
        {
            _contracts = new XPCollection<Contract>(_session);
            _contracts.Criteria = await cls_BaseSpr.GetCustomerCriteria(null, nameof(ArchiveFolderChange.Customer));

            gridControlContract.DataSource = _contracts;

            if (gridViewContract.Columns[nameof(Contract.Oid)] != null)
            {
                gridViewContract.Columns[nameof(Contract.Oid)].Visible = false;
                gridViewContract.Columns[nameof(Contract.Oid)].Width = 18;
                gridViewContract.Columns[nameof(Contract.Oid)].OptionsColumn.FixedWidth = true;
            }
        }

        private void barBtnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewContract.IsEmpty)
            {
                return;
            }

            var contract = gridViewContract.GetRow(gridViewContract.FocusedRowHandle) as Contract;
            if (contract != null)
            {
                var contractReport = default(ContractReport);

                if (contract.File is null)
                {
                    contractReport = new ContractReport(contract);
                }
                else
                {
                    var isPDF = false;
                    contractReport = new ContractReport(contract.File.FileByte, isPDF);
                }

                contractReport.SaveQuitEvent += delegate (object sender, string tempFile, byte[] wordDocument, string fileName)
                {
                    ContractReport_SaveQuitEvent(sender, tempFile, wordDocument, fileName, contract);
                };
            }
        }

        private void ContractReport_SaveQuitEvent(object sender, string tempFile, byte[] wordDocument, string fileName, Contract contract)
        {
            if (_session.IsConnected)
            {
                var contractReport = sender as ContractReport;

                var contractNumber = default(string);

                if (int.TryParse(contract.Number, out int result))
                {
                    contractNumber = result.ToString();
                }

                if (contract.File is null)
                {
                    contract.File = new Core.Model.File(_session, tempFile)
                    {
                        FileName = fileName
                    };
                }
                else
                {
                    contract.File.FileByte = wordDocument;
                }

                contract.File.Save();
                contract.Save();

                if (System.IO.File.Exists(tempFile))
                {
                    System.IO.File.Delete(tempFile);
                }

                contractReport.SaveQuitEvent -= delegate (object sender, string tempFile, byte[] wordDocument, string fileName) { };
            }
        }

        private void gridViewContract_MouseDown(object sender, MouseEventArgs e)
        {
            DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            GridView gridview = sender as GridView;
            GridHitInfo gridHitInfo = gridview.CalcHitInfo(dxMouseEventArgs.Location);
            //if ((gridHitInfo.InRow || gridHitInfo.InRowCell) && dxMouseEventArgs.Button == MouseButtons.Right)

            if (gridHitInfo.HitTest != GridHitTest.Footer && gridHitInfo.HitTest != GridHitTest.Column)
            {
                if (!gridViewContract.IsEmpty)
                {
                    _contracts.Reload();
                }

                if (dxMouseEventArgs.Button == MouseButtons.Right)
                {
                    popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void accordTape_ContextButtonClick(object sender, ContextItemClickEventArgs e)
        {
            if (e != null && e.Item != null)
            {
                if (e.Item.Name.Equals("acBtnDirectoryFilterControl"))
                {
                    cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.CustomerFilter, -1);
                    RenderingCustomerFilter();
                }
                else if (e.Item.Name.Equals("acBtnAddCustomersFilterControl"))
                {
                    var form = new CustomersFilterForm(_session, WorkZone.ModuleContract);
                    form.ShowDialog();

                    if (form.CustomerFilter != null && form.CustomerFilter.Oid != -1)
                    {
                        RenderingCustomerFilter();
                    }
                }
            }
        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            var temp = Path.GetTempFileName();
            gridViewContract.ExportToXlsx(temp);
            gridViewContract.ShowRibbonPrintPreview();
        }

        private void gridViewContract_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;

            if (view.Columns[nameof(Contract.Oid)] != null)
            {
                var contractOid = view.GetRowCellValue
                            (e.RowHandle, view.Columns[nameof(Contract.Oid)])?.ToString();

                if (int.TryParse(contractOid, out int oid))
                {
                    var contract = _session.GetObjectByKey<Contract>(oid);

                    if (contract != null)
                    {
                        if (contract.ChronicleContract != null && contract.ChronicleContract.Count > 0)
                        {
                            if (contract.ChronicleContract.LastOrDefault()?.Date.AddDays(14) > DateTime.Now)
                            {
                                e.Appearance.BackColor = Color.Aqua;
                            }
                        }
                    }
                }
            }
        }

        private void barBtnTaskAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            BVVGlobal.oFuncXpo.CreateTask<TaskEdit>(_session);
        }

        private void barBtnControlSystemAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewContract.IsEmpty)
            {
                return;
            }

            var obj = gridViewContract.GetRow(gridViewContract.FocusedRowHandle) as Contract;
            if (obj != null)
            {
                var form = new ControlSystemEdit(obj);
                form.ShowDialog();
            }
        }
    }
}