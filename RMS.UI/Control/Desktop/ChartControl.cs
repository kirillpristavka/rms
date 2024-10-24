using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraCharts;
using RMS.Core.Model;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Control.Desktop
{
    public partial class ChartControl<T> : UserControl where T : Task
    {
        public Session session;
        public string name;
        public string nameProperty;
        private CriteriaOperator criteriaOperator;

        public ChartControl(Session session, string name, string nameProperty, CriteriaOperator criteriaOperator = null)
        {
            InitializeComponent();
            
            this.session = session;
            this.name = name;
            this.nameProperty = nameProperty;
            this.criteriaOperator = criteriaOperator;
        }

        private void ChartControl_Load(object sender, EventArgs e)
        {
            var chart = new ChartControl();
            
            var series = new Series("Задачи", ViewType.Doughnut);
            chart.Series.Add(series);
            
            var collection = new XPCollection<T>(session, criteriaOperator);
            chart.DataSource = CreateChartData(collection);
            
            series.ArgumentScaleType = ScaleType.Numerical;
            //series.ArgumentDataMember = "Argument";
            series.ValueScaleType = ScaleType.Numerical;
            series.ValueDataMembers.AddRange(new string[] { "Value" });
            
            //((SideBySideBarSeriesView)series.View).ColorEach = true;
            //((XYDiagram)chart.Diagram).AxisY.Visibility = DevExpress.Utils.DefaultBoolean.False;
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            chart.Dock = DockStyle.Fill;
            panelControl.Controls.Add(chart);
        }

        private DataTable CreateChartData(XPCollection<T> collection)
        {
            // Create an empty table.
            DataTable table = new DataTable("TaskTable");

            // Add two columns to the table.
            table.Columns.Add("Argument", typeof(String));
            table.Columns.Add("Value", typeof(Int32));

            foreach (var staff in collection.GroupBy(g => g.Staff).Select(s => s.Key))
            {
                var row = table.NewRow();
                //row["Argument"] = staff?.ToString();
                row["Value"] = collection.Where(w => w.Staff == staff).Count();
                table.Rows.Add(row);
            }
            
            return table;
        }
    }    
}
