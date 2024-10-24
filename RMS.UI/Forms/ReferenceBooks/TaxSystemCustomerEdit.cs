using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Taxes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class TaxSystemCustomerEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        private TaxSystemCustomer TaxSystemCustomer { get; }

        private TaxSystemCustomerEdit()
        {
            InitializeComponent();
            XPObject.AutoSaveOnEndEdit = false;
        }

        public TaxSystemCustomerEdit(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            TaxSystemCustomer = customer.TaxSystemCustomer ?? new TaxSystemCustomer(Session);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {                        
            TaxSystemCustomer.Comment = memoComment.Text;
            TaxSystemCustomer.Save();
            Customer.TaxSystemCustomer = TaxSystemCustomer;
            
            if (Customer.Oid != -1)
            {
                Customer.Save();
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TaxSystemCustomerEdit_Load(object sender, EventArgs e)
        {            
            btnCustomer.EditValue = Customer;
            btnTaxSystem.EditValue = TaxSystemCustomer.TaxSystem;
            memoComment.EditValue = TaxSystemCustomer.Comment;
            
            gridControl.DataSource = TaxSystemCustomer.TaxSystemCustomerObjects;
            gridView.CellValueChanged += GridView_CellValueChanged;


            if (gridView.Columns[nameof(TaxSystemCustomerObject.Oid)] != null)
            {
                gridView.Columns[nameof(TaxSystemCustomerObject.Oid)].Visible = false;
                gridView.Columns[nameof(TaxSystemCustomerObject.Oid)].Width = 18;
                gridView.Columns[nameof(TaxSystemCustomerObject.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(TaxSystemCustomerObject.TaxSystemString)] != null)
            {
                var buttonEdit = gridControl.RepositoryItems.Add(nameof(ButtonEdit)) as RepositoryItemButtonEdit;
                buttonEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
                buttonEdit.ButtonPressed += ButtonEdit_ButtonPressed;
                buttonEdit.DoubleClick += ButtonEdit_DoubleClick;

                gridView.Columns[nameof(TaxSystemCustomerObject.TaxSystemString)].ColumnEdit = buttonEdit;
            }
        }

        private void GridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            btnTaxSystem.EditValue = TaxSystemCustomer.TaxSystem;
        }        

        private void ButtonEdit_DoubleClick(object sender, EventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var taxSystemCustomerObject = gridView.GetRow(gridView.FocusedRowHandle) as TaxSystemCustomerObject;
            if (taxSystemCustomerObject != null)
            {
                var oid = taxSystemCustomerObject.TaxSystem?.Oid ?? -1;
                var id = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.TaxSystem, oid);
                if (id > 0)
                {
                    var taxSystem = Session.GetObjectByKey<TaxSystem>(id);
                    taxSystemCustomerObject.TaxSystem = taxSystem;

                    var buttonEdit = sender as ButtonEdit;
                    if (buttonEdit != null)
                    {
                        buttonEdit.EditValue = taxSystemCustomerObject.TaxSystemString;
                    }
                }
            }
        }

        private void ButtonEdit_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var taxSystemCustomerObject = gridView.GetRow(gridView.FocusedRowHandle) as TaxSystemCustomerObject;
            if (taxSystemCustomerObject != null)
            {
                var oid = taxSystemCustomerObject.TaxSystem?.Oid ?? -1;
                var id = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.TaxSystem, oid);
                if (id > 0)
                {
                    var taxSystem = Session.GetObjectByKey<TaxSystem>(id);
                    taxSystemCustomerObject.TaxSystem = taxSystem;

                    var buttonEdit = sender as ButtonEdit;
                    if (buttonEdit != null)
                    {
                        buttonEdit.EditValue = taxSystemCustomerObject.TaxSystemString;
                    }
                }
            }
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }
        
        private void btnTaxSystem_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<TaxSystem>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.TaxSystem, 1, null, null, false, null, string.Empty, false, true);
        }        

        private void gridControl_MouseDown(object sender, MouseEventArgs e)
        {
            DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            if (dxMouseEventArgs.Button == MouseButtons.Right)
            {
                popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void barBtnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            TaxSystemCustomer.TaxSystemCustomerObjects.Add(
                new TaxSystemCustomerObject(Session) 
                { 
                    UserCreate = Session.GetObjectByKey<User>(DatabaseConnection.User?.Oid)
                }
            );
        }

        private void TaxSystemCustomerEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            XPObject.AutoSaveOnEndEdit = true;
            TaxSystemCustomer?.Reload();
            TaxSystemCustomer?.TaxSystemCustomerObjects?.Reload();
        }

        private void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var obj = gridView.GetRow(gridView.FocusedRowHandle) as TaxSystemCustomerObject;
            if (obj != null)
            {
                if (XtraMessageBox.Show($"Вы точно хотите удалить объект: {obj}",
                                        "Удаление объекта",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {
                    obj.Delete();
                }
            }
        }
    }
}