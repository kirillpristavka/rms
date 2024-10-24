using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using RMS.Core.Enumerator;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class ResponsibleCustomerEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        public CustomerResponsible CustomerResponsible { get; private set; }
        private ResponsibleOption ResponsibleOption { get; }

        private ResponsibleCustomerEdit()
        {
            InitializeComponent();
            XPObject.AutoSaveOnEndEdit = false;
        }

        public ResponsibleCustomerEdit(Customer customer, ResponsibleOption responsibleOption) : this()
        {
            ResponsibleOption = responsibleOption;
            Customer = customer;
            Session = customer.Session;
            CustomerResponsible = customer.CustomerResponsible ?? new CustomerResponsible(Session);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {                        
            CustomerResponsible.Comment = memoComment.Text;
            CustomerResponsible.Save();
            Customer.CustomerResponsible = CustomerResponsible;
            
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

        private void ResponsibleCustomerEdit_Load(object sender, EventArgs e)
        {            
            btnCustomer.EditValue = Customer;

            var obj = default(Staff);

            btnCustomerResponsible.EditValue = obj;
            memoComment.EditValue = CustomerResponsible.Comment;

            var criteriaResponsibleOption = new BinaryOperator(nameof(CustomerResponsibleObject.ResponsibleOption), ResponsibleOption);
            CustomerResponsible.CustomerResponsibleObjects.Criteria = criteriaResponsibleOption;

            gridControl.DataSource = CustomerResponsible.CustomerResponsibleObjects;
            gridView.CellValueChanged += GridView_CellValueChanged;

            if (gridView.Columns[nameof(CustomerResponsibleObject.Oid)] != null)
            {
                gridView.Columns[nameof(CustomerResponsibleObject.Oid)].Visible = false;
                gridView.Columns[nameof(CustomerResponsibleObject.Oid)].Width = 18;
                gridView.Columns[nameof(CustomerResponsibleObject.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(CustomerResponsibleObject.StaffString)] != null)
            {
                var buttonEdit = gridControl.RepositoryItems.Add(nameof(ButtonEdit)) as RepositoryItemButtonEdit;
                buttonEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
                buttonEdit.ButtonPressed += ButtonEdit_ButtonPressed;
                buttonEdit.DoubleClick += ButtonEdit_DoubleClick;

                gridView.Columns[nameof(CustomerResponsibleObject.StaffString)].ColumnEdit = buttonEdit;
            }
        }

        private void GridView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var obj = default(Staff);
            
            btnCustomerResponsible.EditValue = obj;
        }        

        private void ButtonEdit_DoubleClick(object sender, EventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var customerResponsibleObject = gridView.GetRow(gridView.FocusedRowHandle) as CustomerResponsibleObject;
            if (customerResponsibleObject != null)
            {
                var oid = customerResponsibleObject.Staff?.Oid ?? -1;
                var id = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.Staff, oid);
                if (id > 0)
                {
                    var staff = Session.GetObjectByKey<Staff>(id);
                    customerResponsibleObject.Staff = staff;

                    var buttonEdit = sender as ButtonEdit;
                    if (buttonEdit != null)
                    {
                        buttonEdit.EditValue = customerResponsibleObject.StaffString;
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

            var customerResponsibleObject = gridView.GetRow(gridView.FocusedRowHandle) as CustomerResponsibleObject;
            if (customerResponsibleObject != null)
            {
                var oid = customerResponsibleObject.Staff?.Oid ?? -1;
                var id = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.Staff, oid);
                if (id > 0)
                {
                    var staff = Session.GetObjectByKey<Staff>(id);
                    customerResponsibleObject.Staff = staff;

                    var buttonEdit = sender as ButtonEdit;
                    if (buttonEdit != null)
                    {
                        buttonEdit.EditValue = customerResponsibleObject.StaffString;
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
            var buttonEdit = sender as ButtonEdit;
            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
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
            CustomerResponsible.CustomerResponsibleObjects.Add(
                new CustomerResponsibleObject(Session, ResponsibleOption) 
                { 
                    UserCreate = Session.GetObjectByKey<User>(DatabaseConnection.User?.Oid)
                }
            );
        }

        private void TaxSystemCustomerEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            XPObject.AutoSaveOnEndEdit = true;
            CustomerResponsible?.Reload();
            CustomerResponsible?.CustomerResponsibleObjects?.Reload();
        }

        private void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var obj = gridView.GetRow(gridView.FocusedRowHandle) as CustomerResponsibleObject;
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