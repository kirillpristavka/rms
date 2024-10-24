using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Salary;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RMS.UI.Forms.Directories
{
    public partial class CustomerSalaryAdvanceEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        public CustomerSalaryAdvance CustomerSalaryAdvance { get; }
        public List<CustomerSalaryAdvance> CustomerSalaryAdvances { get; set; }
        public bool IsMassChange { get; set; }

        public CustomerSalaryAdvanceEdit(Session session)
        {
            InitializeComponent();

            foreach (TypeAccrual item in Enum.GetValues(typeof(TypeAccrual)))
            {
                cmbTypeAccrual.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbTypeAccrual.SelectedIndex = 0;
            
            Session = session;
            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
            }
            
            if (CustomerSalaryAdvance is null)
            {
                CustomerSalaryAdvance = new CustomerSalaryAdvance(Session)
                {
                    StatusAccrual = session.FindObject<StatusAccrual>(new BinaryOperator(nameof(StatusAccrual.IsDefault), true))
                };
            }            
        }

        public CustomerSalaryAdvanceEdit(Session session, List<CustomerSalaryAdvance> customerSalaryAdvances) : this(session)
        {
            CustomerSalaryAdvances = customerSalaryAdvances;
            IsMassChange = true;
            btnSave.Text = "Применить";
        }

        public CustomerSalaryAdvanceEdit(CustomerSalaryAdvance customerSalaryAdvance) : this(customerSalaryAdvance.Session)
        {
            CustomerSalaryAdvance = customerSalaryAdvance;
            Customer = customerSalaryAdvance.Customer;
            Session = customerSalaryAdvance.Session;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsMassChange)
            {
                SaveMassCustomerSalaryAdvance();
            }
            else
            {
                SaveCustomerSalaryAdvance();
            }
            Close();
        }

        private void SaveMassCustomerSalaryAdvance()
        {
            var statusAccrual = default(StatusAccrual);
            var comment = memoComment.Text;
            
            if (btnStatusAccrual.EditValue is StatusAccrual _statusAccrual)
            {
                statusAccrual = _statusAccrual;
            }
            else
            {
                statusAccrual = null;
            }

            foreach (var customerSalaryAdvances in CustomerSalaryAdvances)
            {
                customerSalaryAdvances.StatusAccrual = statusAccrual;
                customerSalaryAdvances.Comment = $"{comment}{Environment.NewLine}{customerSalaryAdvances.Comment}";
                customerSalaryAdvances.Save();
            }
        }

        private void SaveCustomerSalaryAdvance()
        {
            var accountantResponsible = btnAccountantResponsible.EditValue as Staff;
            var customer = btnCustomer.EditValue as Customer;
            var statusAccrual = btnStatusAccrual.EditValue as StatusAccrual;
            var date = dateDate.DateTime;
            var actualdate = dateActualDate.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(dateActualDate.EditValue);
            var typeAccrual = (TypeAccrual)cmbTypeAccrual.SelectedIndex;            
            var comment = memoComment.Text;
            
            if (accountantResponsible != null)
            {
                CustomerSalaryAdvance.Staff = accountantResponsible;
            }
            else
            {
                CustomerSalaryAdvance.Staff = null;
            }

            if (customer != null)
            {
                CustomerSalaryAdvance.Customer = customer;
            }
            else
            {
                CustomerSalaryAdvance.Customer = null;
            }

            if (statusAccrual != null)
            {
                CustomerSalaryAdvance.StatusAccrual = statusAccrual;
            }
            else
            {
                CustomerSalaryAdvance.StatusAccrual = null;
            }

            CustomerSalaryAdvance.Date = date;
            CustomerSalaryAdvance.ActualDate = actualdate;
            CustomerSalaryAdvance.Comment = comment;
            CustomerSalaryAdvance.TypeAccrual = typeAccrual;

            CustomerSalaryAdvance.Save();
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCustomer_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private void ReportChangeEdit_Load(object sender, EventArgs e)
        {
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Staff>(Session, btnAccountantResponsible, cls_App.ReferenceBooks.Staff, isEnable: isEditSalaryReportForm);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Customer>(Session, btnCustomer, cls_App.ReferenceBooks.Customer, isEnable: isEditSalaryReportForm);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Customer>(Session, btnStatusAccrual, cls_App.ReferenceBooks.StatusAccrual, isEnable: isEditSalaryReportForm);

            LoadReportChange();
        }
        
        private bool isEditSalaryReportForm = false;
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
                            isEditSalaryReportForm = accessRights.IsEditSalaryReportForm;
                        }                        
                    }
                }

                btnSave.Enabled = isEditSalaryReportForm;

                CustomerEdit.CloseButtons(btnCustomer, isEditSalaryReportForm);
                CustomerEdit.CloseButtons(btnStatusAccrual, isEditSalaryReportForm);
                CustomerEdit.CloseButtons(btnAccountantResponsible, isEditSalaryReportForm);
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void LoadReportChange()
        {
            await SetAccessRights();
                
            btnAccountantResponsible.EditValue = CustomerSalaryAdvance.Staff;
            btnCustomer.EditValue = Customer;
            btnStatusAccrual.EditValue = CustomerSalaryAdvance.StatusAccrual;            
            dateActualDate.EditValue = CustomerSalaryAdvance.ActualDate;
            dateDate.EditValue = CustomerSalaryAdvance.Date;
            cmbTypeAccrual.SelectedIndex = (int)CustomerSalaryAdvance.TypeAccrual;            
            memoComment.EditValue = CustomerSalaryAdvance.Comment;
        }        

        private void btnReport_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<StatusAccrual>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.StatusAccrual, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnAccountantResponsible_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
        }        
    }
}