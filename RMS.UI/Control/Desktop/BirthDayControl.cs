using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraLayout;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Control.Desktop
{
    public partial class BirthDayControl : UserControl
    {
        private Session _session;
        public int Count { get; private set; }
        
        public BirthDayControl(Session session)
        {
            InitializeComponent();
            _session = session;
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            var dateTimeNow = DateTime.Now.Date;

            var critreia = default(CriteriaOperator);

            var user = await _session.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid);
            var staff = user.Staff;

            if (user != null && user.flagAdministrator is false)
            {
                var groupOperator = new GroupOperator(GroupOperatorType.And);
                if (staff != null)
                {
                    var groupOperatorStaff = new GroupOperator(GroupOperatorType.Or);
                    var accountantResponsibleCriteria = new BinaryOperator(nameof(Customer.AccountantResponsible), staff);
                    groupOperatorStaff.Operands.Add(accountantResponsibleCriteria);
                    var primaryResponsibleCriteria = new BinaryOperator(nameof(Customer.PrimaryResponsible), staff);
                    groupOperatorStaff.Operands.Add(primaryResponsibleCriteria);
                    var bankResponsibleCriteria = new BinaryOperator(nameof(Customer.BankResponsible), staff);
                    groupOperatorStaff.Operands.Add(bankResponsibleCriteria);
                    groupOperator.Operands.Add(groupOperatorStaff);

                    var statusCustomer = await _session.FindObjectAsync<Status>(new BinaryOperator(nameof(Status.Name), "Обслуживаем"));
                    if (statusCustomer != null)
                    {
                        var criteriaStatus = new BinaryOperator($"{nameof(Customer.CustomerStatus)}.{nameof(CustomerStatus.Status)}", statusCustomer);
                        groupOperator.Operands.Add(criteriaStatus);
                    }

                    critreia = groupOperator;
                }
            }

            var customers = new XPCollection<Customer>(_session, critreia);

            layoutControlLogger.BeginUpdate();
            foreach (var customer in customers.Where(w => w.DateManagementBirth != null && w.DateManagementBirth.Value.Month == dateTimeNow.Month))
            {
                var objectControl = new BirthDayObjectControl(customer) { Dock = DockStyle.Top };
                objectControl.CreateControl();
                if (objectControl.IsCongratulate)
                {
                    var layoutControlItem = layoutControlLogger.Root.AddItem();
                    layoutControlItem.TextVisible = false;
                    layoutControlItem.AllowHotTrack = false;
                    layoutControlItem.MaxSize = new System.Drawing.Size(0, objectControl.Size.Height);
                    layoutControlItem.MinSize = new System.Drawing.Size(10, objectControl.Size.Height);
                    layoutControlItem.SizeConstraintsType = SizeConstraintsType.Custom;
                    layoutControlItem.TextSize = new System.Drawing.Size(0, 0);
                    layoutControlItem.Control = objectControl;
                    
                    Count++;
                }                
            }
            var emptySpaceItem = new EmptySpaceItem();
            layoutControlLogger.Root.AddItem(emptySpaceItem);

            layoutControlLogger.EndUpdate();                   
        }
    }
}
