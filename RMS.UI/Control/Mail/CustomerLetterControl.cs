using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace RMS.UI.Control.Mail
{
    public partial class CustomerLetterControl : XtraUserControl
    {
        public delegate void ButtonPressEventHandler(object sender, GroupOperator groupOperator, bool isUse);
        public event ButtonPressEventHandler ButtonPress;

        private Customer Customer { get; }
        
        public CheckButton CheckButton { get; set; }
        public GroupOperator GroupOperator { get; set; }
        public bool IsUse { get; set; }
        public Guid Guid { get; }

        public CustomerLetterControl(Customer customer)
        {
            InitializeComponent();
            Guid = Guid.NewGuid();
            Dock = DockStyle.Top;
            
            Customer = customer;
            CheckButton = checkBtnIsUse;

            GroupOperator = GetGroupOperator();
        }

        private void checkBtnIsUse_Click(object sender, EventArgs e)
        {
            var checkButton = sender as CheckButton;
            ClickCheckButton(checkButton);
        }

        private void ClickCheckButton(CheckButton checkButton)
        {
            if (checkButton != null)
            {
                if (checkButton.Checked is false)
                {
                    ButtonPress?.Invoke(this, GroupOperator, true);
                }
                else
                {
                    ButtonPress?.Invoke(this, GroupOperator, false);
                }
            }
        }

        private void CustomerLetterControl_Load(object sender, EventArgs e)
        {
            FillingCustomerLetterControl();
        }
        
        public void RefresControl()
        {
            try
            {
                checkBtnIsUse.Text = Customer.Name ?? Customer.ToString();

                if (GroupOperator.Operands.Count > 0)
                {
                    using (var session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer())
                    {
                        var xpcollection = new XPCollection<Letter>(session, GroupOperator);
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
        
        private GroupOperator GetGroupOperator()
        {
            try
            {
                var groupOperatorLetter = new GroupOperator(GroupOperatorType.Or);

                var criteriaCustomer = new BinaryOperator($"{nameof(Letter.Customer)}.{nameof(Letter.Customer.Oid)}", Customer.Oid);
                groupOperatorLetter.Operands.Add(criteriaCustomer);

                var groupOperatorLetterSenderAddress = new GroupOperator(GroupOperatorType.Or);

                foreach (var contact in Customer.CustomerEmails)
                {
                    if (!string.IsNullOrWhiteSpace(contact.Email))
                    {
                        var criteria = new BinaryOperator(nameof(Letter.LetterSenderAddress), contact.Email);
                        groupOperatorLetterSenderAddress.Operands.Add(criteria);

                        IsUse = true;
                    }
                }

                if (groupOperatorLetterSenderAddress.Operands.Count > 0)
                {
                    groupOperatorLetter.Operands.Add(groupOperatorLetterSenderAddress);
                }

                return groupOperatorLetter;
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                return null;
            }            
        }

        private void FillingCustomerLetterControl()
        {
            if (Customer != null)
            {
                RefresControl();
            }
        }
    }
}
