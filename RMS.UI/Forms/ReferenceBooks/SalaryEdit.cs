using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Salary;
using RMS.Core.Model.Taxes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class SalaryEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        public SalaryAndAdvance SalaryAndAdvance { get; }

        private SalaryEdit()
        {
            InitializeComponent();
            XPObject.AutoSaveOnEndEdit = false;
        }

        public SalaryEdit(Customer customer, SalaryAndAdvance salaryAndAdvance) : this()
        {
            Customer = customer;
            Session = customer.Session;
            SalaryAndAdvance = salaryAndAdvance;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {                        
            SalaryAndAdvance.Comment = memoComment.Text;
            SalaryAndAdvance.Save();
            
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
            btnDay.EditValue = SalaryAndAdvance.CurrentObjectString;
            memoComment.EditValue = SalaryAndAdvance.Comment;
            
            gridControl.DataSource = SalaryAndAdvance.SalaryObjects;
            gridView.CellValueChanged += GridView_CellValueChanged;
            
            if (gridView.Columns[nameof(SalaryAndAdvanceObject.Oid)] != null)
            {
                gridView.Columns[nameof(SalaryAndAdvanceObject.Oid)].Visible = false;
                gridView.Columns[nameof(SalaryAndAdvanceObject.Oid)].Width = 18;
                gridView.Columns[nameof(SalaryAndAdvanceObject.Oid)].OptionsColumn.FixedWidth = true;
            }
        }

        private void GridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            btnDay.EditValue = SalaryAndAdvance.CurrentObjectString;
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
            SalaryAndAdvance.SalaryObjects.Add(
                new SalaryAndAdvanceObject(Session) 
                { 
                    UserCreate = Session.GetObjectByKey<User>(DatabaseConnection.User?.Oid)
                }
            );
        }

        private void TaxSystemCustomerEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            XPObject.AutoSaveOnEndEdit = true;
            SalaryAndAdvance?.Reload();
            SalaryAndAdvance?.SalaryObjects?.Reload();
        }

        private void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var obj = gridView.GetRow(gridView.FocusedRowHandle) as SalaryAndAdvanceObject;
            if (obj != null)
            {
                if (XtraMessageBox.Show($"Вы точно хотите удалить объект: {obj}",
                                        "Удаление объекта",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {
                    obj.Delete();
                    btnDay.EditValue = SalaryAndAdvance.CurrentObjectString;
                }
            }
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }
    }
}