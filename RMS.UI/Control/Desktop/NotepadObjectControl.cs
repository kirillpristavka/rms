using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using RMS.Core.Model.Reports;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Control.Desktop
{
    public partial class NotepadObjectControl<T> : UserControl
    {
        private Session session;
        private string caption;
        private XPCollection<T> xpCollection;
        
        public NotepadObjectControl(Session session, string caption)
        {
            InitializeComponent();
            this.session = session;
            this.caption = caption;
        }

        private void Control_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(caption))
                {
                    lblName.Text = caption;
                }

                var groupCriteria = new GroupOperator(GroupOperatorType.And);
                var criteriaDateSince = new BinaryOperator(nameof(ReportChange.LastDayDelivery), new DateTime(DateTime.Now.Year, 1, 1), BinaryOperatorType.GreaterOrEqual);
                groupCriteria.Operands.Add(criteriaDateSince);
                var criteriaDateTo = new BinaryOperator(nameof(ReportChange.LastDayDelivery), new DateTime(DateTime.Now.Year, 12, 31), BinaryOperatorType.LessOrEqual);
                groupCriteria.Operands.Add(criteriaDateTo);

                xpCollection = new XPCollection<T>(session, groupCriteria);

                if (xpCollection is XPCollection<ReportChange> reportChanges)
                {
                    foreach (var item in reportChanges.GroupBy(g => g.Report))
                    {
                        foreach (var item2 in item.GroupBy(g => g.LastDayDelivery))
                        {
                            if (item2.Key is DateTime date)
                            {
                                var obj = $"{item.Key} до {date.ToShortDateString()} - {item.Count()}";
                                listBoxControl.Items.Add(obj);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }
    }
}
