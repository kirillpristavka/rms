using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using RMS.UI.Forms;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Control.Desktop
{
    public partial class BlockControl<T1, T2> : UserControl where T2 : Form
    {
        public Session session;
        public string name;
        private XPCollection<T1> collection;
        private T2 openForm;
        private CriteriaOperator criteriaOperator;

        private BlockControl()
        {
            InitializeComponent();
        }

        public BlockControl(Session session, string name, CriteriaOperator criteriaOperator = null) : this()
        {
            this.session = session;
            this.name = name;
            this.criteriaOperator = criteriaOperator;
            openForm = (T2)Activator.CreateInstance(typeof(T2), session);
        }

        public BlockControl(Session session, string name, T2 openForm = null, CriteriaOperator criteriaOperator = null) : this()
        {            
            this.session = session;
            this.name = name;
            this.openForm = openForm;
            this.criteriaOperator = criteriaOperator;
        }

        private void BlockControl_Load(object sender, EventArgs e)
        {
            if (name.Split('\n')?.Length == 1)
            {
                if (name.Length >= 15)
                {
                    name = $"{name.Substring(0, 12)}...";
                }
            }            
            
            lblName.Text = name;

            collection = new XPCollection<T1>(session, criteriaOperator);
            collection?.Reload();
            
            if (collection != null)
            {
                lblCount.Text = collection.Count().ToString();
            }
            else
            {
                lblCount.Text = "0";
            }
        }

        private void LabelMouseEnter(object sender, EventArgs e)
        {
            var label = sender as LabelControl;
            if (label != null)
            {
                lblCount.Font = new Font(lblCount.Font.Name, lblCount.Font.SizeInPoints, FontStyle.Underline);
            }            
        }

        private void LabelMouseLeave(object sender, EventArgs e)
        {
            var label = sender as LabelControl;
            if (label != null)
            {
                lblCount.Font = new Font(lblCount.Font.Name, lblCount.Font.SizeInPoints, FontStyle.Regular);
            }            
        }

        private void panelControl_MouseClick(object sender, MouseEventArgs e)
        {
            ClickObject(e);
        }

        private void lblName_Click(object sender, EventArgs e)
        {
            ClickObject(e);
        }

        private void lblCount_Click(object sender, EventArgs e)
        {
            ClickObject(e);
        }

        private void ClickObject(object e)
        {
            if (e is MouseEventArgs eventArgs && eventArgs.Button == MouseButtons.Left)
            {
                if (openForm != null)
                {
                    try
                    {
                        if (openForm.IsDisposed)
                        {
                            openForm = (T2)Activator.CreateInstance(typeof(T2), session);
                        }

                        MainForm.OpenForm(Program.MainForm, openForm);
                    }
                    catch (Exception ex)
                    {
                        RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                    }                    
                }
            }
        }
    }
}
