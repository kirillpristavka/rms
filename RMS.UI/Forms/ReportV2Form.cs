using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using PulsLibrary.Extensions.DevForm;
using RMS.Core.Controllers;
using RMS.Core.Controllers.Reports;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Reports;
using RMS.Core.ObjectDTO.Models;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms
{
    public partial class ReportV2Form : XtraForm
    {
        private Session _session;

        private int? _year
        {
            get
            {
                if (int.TryParse(txtYear.Text, out int result) && result >= 1900 && result <= 2024)
                {
                    return result;
                }

                return default;
            }
        }

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridView);
            //TODO: здесь происходит задержка при закрытии формы. Как исправить?
            //BVVGlobal.oFuncXpo.PressEnterGrid<Deal, DealEdit>(gridView, action: async () => await TreeListNodeUpdate().ConfigureAwait(false));
            BVVGlobal.oFuncXpo.PressEnterGrid<ReportChangeCustomerV2, ReportChangeEditV2>(gridView);
        }

        public ReportV2Form(Session session)
        {
            InitializeComponent();
            FunctionalGridSetup();

            _session = session ?? DatabaseConnection.GetWorkSession();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            txtYear.EditValue = DateTime.Now.Year;

            await SetAccessRights();

            using (var uof = new UnitOfWork())
            {
                var customers = await new XPQuery<Customer>(uof)
                    .Select(s => new CustomerDTOPartial(s.Oid, $"{s.Name} ({s.TaxSystemCustomerString})", s.DefaultName))
                    .ToListAsync();

                gridControlCustomer.DataSource = customers.OrderBy(o => o.DefaultName);
                
                if (gridViewCustomer.Columns[nameof(CustomerDTOPartial.Oid)] is GridColumn columnOid)
                {
                    columnOid.Visible = false;
                    columnOid.Width = 25;
                    columnOid.OptionsColumn.FixedWidth = true;
                }

                if (gridViewCustomer.Columns[nameof(CustomerDTOPartial.DefaultName)] is GridColumn columnDefaultName)
                {
                    columnDefaultName.Visible = false;
                }

                if (gridViewCustomer.Columns[nameof(CustomerDTOPartial.Name)] is GridColumn columnName)
                {
                    columnName.Caption = "Наименование";
                    columnName.Width = 250;
                    columnName.OptionsColumn.FixedWidth = true;
                }

                gridViewCustomer.ClearSelection();
            }

            await LoadSettingsForm();
        }

        private void SetGridControlReportChangeOptions()
        {
            gridView.OptionsView.ColumnAutoWidth = false;
            gridView.OptionsView.ShowFooter = true;

            var repositoryItemMemoEdit = gridControl.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
            repositoryItemMemoEdit.WordWrap = true;
            repositoryItemMemoEdit.AllowHtmlDraw = DefaultBoolean.True;

            if (gridView.Columns[nameof(ReportChangeCustomerV2.Oid)] != null)
            {
                gridView.Columns[nameof(ReportChangeCustomerV2.Oid)].Visible = false;
                gridView.Columns[nameof(ReportChangeCustomerV2.Oid)].Width = 18;
                gridView.Columns[nameof(ReportChangeCustomerV2.Oid)].OptionsColumn.FixedWidth = true;
            }
            if (gridView.Columns[nameof(ReportChangeCustomerV2.DeliveryTime)] is GridColumn columnDeliveryTime)
            {
                columnDeliveryTime.Width = 150;
                columnDeliveryTime.OptionsColumn.FixedWidth = true;
                columnDeliveryTime.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }
            if (gridView.Columns[nameof(ReportChangeCustomerV2.Period)] is GridColumn columnPeriod)
            {
                columnPeriod.Width = 150;
                columnPeriod.OptionsColumn.FixedWidth = true;
                columnPeriod.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }
            if (gridView.Columns[nameof(ReportChangeCustomerV2.StatusString)] is GridColumn columnStatusString)
            {
                columnStatusString.ColumnEdit = repositoryItemMemoEdit;
                columnStatusString.Width = 175;
                columnStatusString.OptionsColumn.FixedWidth = true;
                columnStatusString.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                columnStatusString.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            }
            if (gridView.Columns[nameof(ReportChangeCustomerV2.DateCompletion)] is GridColumn columnDateCompletion)
            {
                columnDateCompletion.Width = 125;
                columnDateCompletion.OptionsColumn.FixedWidth = true;
                columnDateCompletion.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }
            if (gridView.Columns[nameof(ReportChangeCustomerV2.ReportString)] is GridColumn columnReportString)
            {
                columnReportString.Width = 150;
                columnReportString.OptionsColumn.FixedWidth = true;
                columnReportString.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }
            if (gridView.Columns[nameof(ReportChangeCustomerV2.AccountantResponsibleString)] is GridColumn columnAccountantResponsibleString)
            {
                columnAccountantResponsibleString.Width = 200;
                columnAccountantResponsibleString.OptionsColumn.FixedWidth = true;
            }
            if (gridView.Columns[nameof(ReportChangeCustomerV2.CustomerString)] is GridColumn columnCustomerString)
            {
                columnCustomerString.Width = 250;
                columnCustomerString.Summary.Clear();
                columnCustomerString.Summary.Add(SummaryItemType.Count, columnCustomerString.Name, "{0}");
            }
            if (gridView.Columns[nameof(ReportChangeCustomerV2.Comment)] is GridColumn columnComment)
            {
                columnComment.ColumnEdit = repositoryItemMemoEdit;
                columnComment.Width = 300;
            }
        }

        private bool _isLoadGridControl;
        private void gridViewCustomer_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (gridView.GetRow(gridView.FocusedRowHandle) is CustomerDTOPartial customerDTO)
                {
                    gridControl.DataSource = new XPCollection<ReportChangeCustomerV2>(
                        _session,
                        new BinaryOperator($"{nameof(ReportChangeCustomerV2.Customer)}.{nameof(XPObject.Oid)}", 
                            customerDTO.Oid));

                    if (_isLoadGridControl is false)
                    {
                        SetGridControlReportChangeOptions();
                        _isLoadGridControl = true;
                    }
                }
            }
        }

        private async System.Threading.Tasks.Task SetAccessRights()
        {
            try
            {
                using (var uof = new UnitOfWork())
                {
                    var user = await uof.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid);
                    if (user != null)
                    {
                        var accessRights = user.AccessRights;
                        if (accessRights != null)
                        {
                            //isEditDealForm = accessRights.IsEditDealForm;
                            //isDeleteDealForm = accessRights.IsDeleteDealForm;
                        }
                    }
                }

                //barBtnRefreshFromLetter.Enabled = isEditDealForm;
                //barBtnBulkReplacement.Enabled = isEditDealForm;

                //barBtnDel.Enabled = isDeleteDealForm;
                //barBtnRemovingEmptyTrades.Enabled = isDeleteDealForm;
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async System.Threading.Tasks.Task LoadSettingsForm()
        {
            try
            {
                //await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(layoutControlDeal, $"{this.Name}_{nameof(layoutControlDeal)}");
                //await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(gridView, $"{this.Name}_{nameof(gridView)}");
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async System.Threading.Tasks.Task SaveSettingsForms()
        {
            try
            {
                //await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(layoutControlDeal, $"{this.Name}_{nameof(layoutControlDeal)}");
                //await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(gridView, $"{this.Name}_{nameof(gridView)}");
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void cmbStatusDeal_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }
        }

        private void gridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (gridView.GetRow(e.RowHandle) is Deal obj)
                {
                    if (obj.DealStatus != null)
                    {
                        var color = obj.DealStatus?.Color;
                        if (!string.IsNullOrWhiteSpace(color))
                        {
                            e.Appearance.BackColor = ColorTranslator.FromHtml(color);
                        }
                    }
                }
            }
        }

        private async System.Threading.Tasks.Task<GroupOperator> GetFilter()
        {
            var groupOperator = new GroupOperator(GroupOperatorType.And);

            if (!isDisplayCompleted)
            {
                //var dealStatus = await _session.FindObjectAsync<DealStatus>(new BinaryOperator(nameof(DealStatus.Name), "Выполнена"));
                //if (dealStatus != null)
                //{
                //    var criteriaDealStatus = new NotOperator(new BinaryOperator($"{nameof(Deal.DealStatus)}.{nameof(DealStatus.Oid)}", dealStatus.Oid));
                //    groupOperator.Operands.Add(criteriaDealStatus);
                //}
            }

           
            //if (btnCustomer.EditValue is Customer customer)
            //{
            //    var criteriaCustomer = new BinaryOperator(nameof(Deal.Customer), customer);
            //    groupOperator.Operands.Add(criteriaCustomer);
            //}

            //if (btnStaff.EditValue is Staff staff)
            //{
            //    var criteriaStaff = new BinaryOperator(nameof(Deal.Staff), staff);
            //    groupOperator.Operands.Add(criteriaStaff);
            //}

            //if (cmbStatusDeal.EditValue is DealStatus dealStatus)
            //{
            //    var criteriaStatusDeal = new BinaryOperator(nameof(Deal.DealStatus), dealStatus);
            //    groupOperator.Operands.Add(criteriaStatusDeal);
            //}

            if (groupOperator.Operands.Count > 0)
            {
                return groupOperator;
            }
            else
            {
                return null;
            }
        }

        private async void DealForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await SaveSettingsForms();
        }

        private bool isDisplayCompleted;
        private void gridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private async void barBtnForm_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_year is null)
            {
                return;
            }

            await ReportChangeCustomerV2Controller.CreateNewReportsAsync(_year);
        }

        private async void barBtnImport_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var ofd = new XtraOpenFileDialog() { Filter = "Excel Files|*.xls;*.xlsx;*.xlsm" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var excelReportsController = new ExcelReportsController(ofd.FileName);
                    await excelReportsController.CreateReportAsync();
                    await excelReportsController.CreateReportChangeAsync();
                }
            }
        }

        private async void barBtnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = default(ReportChangeEditV2);
            if (gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) is CustomerDTOPartial customerDTO)
            {
                var customer = await _session.GetObjectByKeyAsync<Customer>(customerDTO.Oid); 
                if (customer != null)
                {
                    form = new ReportChangeEditV2(customer);
                }
            }

            if (form is null)
            {
                form = new ReportChangeEditV2();
            }

            form.ShowDialog();

            if (form.FlagSave)
            {
                ((XPCollection<ReportChangeCustomerV2>)gridControl.DataSource)?.Reload();
            }
        }

        private void barBtnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is ReportChangeCustomerV2 obj)
            {
                var form = new ReportChangeEditV2(obj);
                form.ShowDialog();

                if (form.FlagSave)
                {
                    obj?.Reload();
                }
            }
        }

        private void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedObj = gridView.GetSelectedRows();

            if (selectedObj.Length == 1)
            {
                if (gridView.GetRow(gridView.FocusedRowHandle) is ReportChangeCustomerV2 obj)
                {
                    if (XtraMessageBox.Show("Удалить выбранный отчет?",
                                            "Информационное сообщение",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        obj?.Delete();
                    }
                }
            }
            else if(selectedObj.Length > 1)
            {
                if (XtraMessageBox.Show($"Вы собираетесь удалить отчетов: {selectedObj.Length}. Продолжить?",
                                            "Информационное сообщение",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
                {
                    foreach (var focusedRowHandle in selectedObj)
                    {
                        if (gridView.GetRow(focusedRowHandle) is ReportChangeCustomerV2 obj)
                        {
                            obj.Delete();
                        }
                    }
                }
            }            
        }
    }
}