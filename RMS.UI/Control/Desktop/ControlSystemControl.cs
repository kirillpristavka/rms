using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraLayout;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Notifications;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace RMS.UI.Control.Desktop
{
    public partial class ControlSystemControl : UserControl
    {
        private Session _session;
        public int Count { get; private set; }

        private DateTime dateSince;
        private DateTime dateTo;

        public delegate void ControlSystemEditControlEventHandler(object sender, ControlSystem сontrolSystem);
        public event ControlSystemEditControlEventHandler ControlSystemEditControlEvent;

        public ControlSystemControl(Session session)
        {
            InitializeComponent();
            _session = session;

            lblLogger.Text = $"Контроль на {DateTime.Now.Date.ToShortDateString()}";
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            dateSince = DateTime.Now.Date.AddDays(-1);
            dateTo = DateTime.Now.Date.AddDays(1);
                        
            var user = await _session.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid);
            var staff = user.Staff;

            var groupOperator = new GroupOperator(GroupOperatorType.And);

            var groupOperatorDate = new GroupOperator(GroupOperatorType.Or);
            groupOperatorDate.Operands.Add(new NullOperator(nameof(ControlSystem.DateTo)));
            groupOperatorDate.Operands.Add(new BinaryOperator(nameof(ControlSystem.DateTo), dateTo, BinaryOperatorType.GreaterOrEqual));
            groupOperator.Operands.Add(groupOperatorDate);

            if (false && user != null && user.flagAdministrator is false)
            {                
                if (staff != null)
                {
                    var groupOperatorStaff = new GroupOperator(GroupOperatorType.Or);
                    var accountantResponsibleCriteria = new BinaryOperator($"{nameof(Task.Customer)}.{nameof(Customer.AccountantResponsible)}", staff);
                    groupOperatorStaff.Operands.Add(accountantResponsibleCriteria);
                    var primaryResponsibleCriteria = new BinaryOperator($"{nameof(Task.Customer)}.{nameof(Customer.PrimaryResponsible)}", staff);
                    groupOperatorStaff.Operands.Add(primaryResponsibleCriteria);
                    var bankResponsibleCriteria = new BinaryOperator($"{nameof(Task.Customer)}.{nameof(Customer.BankResponsible)}", staff);
                    groupOperatorStaff.Operands.Add(bankResponsibleCriteria);
                    groupOperator.Operands.Add(groupOperatorStaff);

                    var statusCustomer = await _session.FindObjectAsync<Status>(new BinaryOperator(nameof(Status.Name), "Обслуживаем"));
                    if (statusCustomer != null)
                    {
                        var criteriaStatus = new BinaryOperator($"{nameof(Task.Customer)}.{ nameof(Customer.CustomerStatus)}.{nameof(CustomerStatus.Status)}", statusCustomer);
                        groupOperator.Operands.Add(criteriaStatus);
                    }
                }

                var groupOperatorTaskStaff = new GroupOperator(GroupOperatorType.Or);
                var staffCriteria = new BinaryOperator(nameof(Task.Staff), staff);
                groupOperatorTaskStaff.Operands.Add(staffCriteria);
                var coExecutorCriteria = new BinaryOperator(nameof(Task.CoExecutor), staff);
                groupOperatorTaskStaff.Operands.Add(coExecutorCriteria);
                groupOperator.Operands.Add(groupOperatorTaskStaff);
            }

            using (var controlSystems = new XPCollection<ControlSystem>(_session, groupOperator))
            {
                layoutControlLogger.BeginUpdate();
                foreach (var controlSystem in controlSystems)
                {
                    try
                    {
                        var objectControl = new ControlSystemObjectControl(controlSystem) { Dock = DockStyle.Top };                       
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

                            objectControl.ControlSystemEditEvent += ObjectControl_ControlSystemEditEvent;
                        }
                    }
                    catch (Exception) { }
                }

                var emptySpaceItem = new EmptySpaceItem();
                layoutControlLogger.Root.AddItem(emptySpaceItem);

                layoutControlLogger.EndUpdate();
            }
        }

        private void ObjectControl_ControlSystemEditEvent(object sender, ControlSystem controlSystem)
        {
            var obj = sender as ControlSystemObjectControl;
            if (obj != null)
            {
                try
                {
                    obj.ReloadControl();
                    obj.Refresh();

                    if (obj.IsVisible is false)
                    {
                        obj?.Dispose();
                        layoutControlLogger?.Refresh();
                    }

                    ControlSystemEditControlEvent?.Invoke(this, controlSystem);
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                }
            }
        }
    }
}
