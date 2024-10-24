using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraLayout;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Control.Desktop
{
    public partial class PatentControl : UserControl
    {
        private Session _session;
        public int Count { get; private set; }

        private DateTime dateSince;
        private DateTime dateTo;
        
        public PatentControl(Session session)
        {
            InitializeComponent();
            _session = session;
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            dateSince = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dateTo = dateSince.AddMonths(2).AddDays(-1);

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
            
            using (var customers = new XPCollection<Customer>(_session, critreia))
            {
                layoutControlLogger.BeginUpdate();

                foreach (var customer in customers)
                {
                    try
                    {
                        var patentObject = customer.Tax?.Patent?.PatentObjects;
                        patentObject?.Reload();

                        if (patentObject != null)
                        {
                            foreach (var obj in patentObject.Where(w => w.DateTo >= dateSince && w.DateTo <= dateTo))
                            {                                
                                try
                                {
                                    var lastObj = patentObject.Where(w => w.ClassOKVED2 == obj.ClassOKVED2).FirstOrDefault(f => f.DateTo is null || f.DateTo > dateTo);
                                    if (lastObj != null)
                                    {
                                        continue;
                                    }

                                    var objectControl = new PatentObjectControl(customer, obj, dateTo) { Dock = DockStyle.Top };
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

                                        objectControl.PatentEditEvent += ObjectControl_PatentEditEvent;
                                    }
                                }
                                catch (Exception) { }
                            }
                        }
                    }
                    catch (Exception) { }
                }

                var emptySpaceItem = new EmptySpaceItem();
                layoutControlLogger.Root.AddItem(emptySpaceItem);

                layoutControlLogger.EndUpdate();
            }
        }

        private void ObjectControl_PatentEditEvent(object sender)
        {
            var objectControl = sender as PatentObjectControl;
            if (objectControl != null)
            {
                try
                {
                    objectControl.ReloadControl();
                    objectControl.Refresh();

                    if (objectControl.IsCongratulate is false)
                    {
                        objectControl?.Dispose();
                        layoutControlLogger?.Refresh();
                    }
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                }
            }
        }
    }
}
