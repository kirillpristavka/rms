using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using RMS.Core.Enumerator;
using RMS.Core.Model;
using RMS.Core.Model.InfoContract;
using RMS.Core.Model.InfoCustomer;
using RMS.UI.Forms;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace RMS.UI.Control.Customers
{
    public partial class CustomersFilterControl : XtraUserControl
    {
        public delegate void ButtonPressEventHandler(object sender, CustomerFilter customerFilter, bool isUse);
        public event ButtonPressEventHandler ButtonPress;

        public delegate void DisplayCheckEventHandler(object sender, bool isEditVisible);
        public event DisplayCheckEventHandler DisplayCheck;

        private CustomerFilter CustomerFilter { get; }
        private bool IsContract { get; }

        public GroupOperator GroupOperator { get; set; }
        public bool IsUse { get; set; }
        public Guid Guid { get; }
        public CheckButton CheckButton { get; set; }

        public CustomersFilterControl(CustomerFilter customerFilter, bool isContract = false)
        {
            InitializeComponent();
            Guid = Guid.NewGuid();
            Dock = DockStyle.Top;

            CustomerFilter = customerFilter;           
            CheckButton = checkBtnIsUse;

            IsContract = isContract;

            GroupOperator = GetGroupOperator();
        }

        private GroupOperator GetGroupOperator()
        {
            try
            {                
                if (IsContract)
                {
                    return CustomerFilter.GetGroupOperatorContract();
                }
                else
                {
                    return CustomerFilter.GetGroupOperatorCustomer();
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                return null;
            }
        }

        private void CustomersFilterControl_Load(object sender, EventArgs e)
        {
            if (IsContract)
            {
                FillingCustomerFilter<Contract>();
            }
            else
            {
                FillingCustomerFilter<Customer>();
            }
        }

        private void FillingCustomerFilter<T>()
        {
            if (CustomerFilter != null)
            {
                RefresControl<T>();
            }
        }
        
        private void ClickCheckButton(CheckButton checkButton)
        {
            if (checkButton != null)
            {
                if (checkButton.Checked is false)
                {                    
                    ButtonPress?.Invoke(this, CustomerFilter, true); 
                }
                else
                {
                    ButtonPress?.Invoke(this, CustomerFilter, false);
                }
            }
        }

        private void checkBtnIsUse_Click(object sender, EventArgs e)
        {
            var checkButton = sender as CheckButton;
            ClickCheckButton(checkButton);
        }

        private void checkCount_Click(object sender, EventArgs e)
        {
            var checkButton = sender as CheckButton;
            ClickCheckButton(checkButton);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var form = new CustomersFilterForm(CustomerFilter, WorkZone.ModuleContract);
            form.ShowDialog();

            if (form.FlagSave)
            {
                GroupOperator = GetGroupOperator();
                
                if (IsContract)
                {                    
                    FillingCustomerFilter<Contract>();
                }
                else
                {
                    FillingCustomerFilter<Customer>();
                }

                checkBtnIsUse.Refresh();

                if (checkBtnIsUse.Checked)
                {
                    ButtonPress?.Invoke(this, CustomerFilter, true);
                }

                //TODO: Обновлять только при изменении видимости, пользователей или группы пользователей (2 последних объекта пока не проверяются).
                DisplayCheck?.Invoke(this, true);
            }
            
            //if (form.IsEditVisible)
            //{
            //    DisplayCheck?.Invoke(this, true);
            //}
        }

        public void RefresControl<T>()
        {
            try
            {
                checkBtnIsUse.Text = CustomerFilter.Name;

                if (GroupOperator?.Operands.Count > 0)
                {
                    using (var session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer())
                    {
                        var xpcollection = new XPCollection<T>(session, GroupOperator);
                        var count = xpcollection?.Count;

                        if (count != null)
                        {
                            Invoke((Action)delegate
                            {
                                lblCount.Text = count.ToString();
                            });
                        }

                        xpcollection.Dispose();
                    }                    
                }
                else
                {
                    Invoke((Action)delegate
                    {
                        lblCount.Text = "0";
                    });
                }

                Invoke((Action)delegate
                {
                    Refresh();
                });
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }
    }
}
