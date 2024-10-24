using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Salary;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RMS.UI.Forms
{
    public partial class SalaryForm : XtraForm
    {
        private Session Session { get; }
        private XPCollection<Statement> Statements { get; set; }

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridView);
            //BVVGlobal.oFuncXpo.PressEnterGrid<Task, TaskEdit>(gridViewTasks);
        }

        public SalaryForm(Session session)
        {
            InitializeComponent();
            FunctionalGridSetup();

            Session = session ?? BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
        }

        private void TaskForm_Load(object sender, EventArgs e)
        {
            Statements = new XPCollection<Statement>(Session);
            gridControl.DataSource = Statements;

            if (gridView.Columns[nameof(Task.Oid)] != null)
            {
                gridView.Columns[nameof(Task.Oid)].Visible = false;
                gridView.Columns[nameof(Task.Oid)].Width = 18;
                gridView.Columns[nameof(Task.Oid)].OptionsColumn.FixedWidth = true;
            }
        }

        private Statement currentStatement;
        
        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            currentStatement = null;
            currentStatementCustomer = null;
            if (gridView.IsEmpty)
            {                
                return;
            }

            var statment = gridView.GetRow(gridView.FocusedRowHandle) as Statement;
            if (statment != null)
            {
                currentStatement = statment;

                gridControl1.DataSource = currentStatement.StatementCustomers;
                if (gridView1.Columns[nameof(XPObject.Oid)] != null)
                {
                    gridView1.Columns[nameof(XPObject.Oid)].Visible = false;
                    gridView1.Columns[nameof(XPObject.Oid)].Width = 18;
                    gridView1.Columns[nameof(XPObject.Oid)].OptionsColumn.FixedWidth = true;
                }

                if (gridView1.Columns[nameof(StatementCustomer.CustomerString)] != null)
                {
                    gridView1.Columns[nameof(StatementCustomer.CustomerString)].OptionsColumn.ReadOnly = true;
                    gridView1.Columns[nameof(StatementCustomer.CustomerString)].OptionsColumn.AllowEdit = false;
                }
            }
        }

        private StatementCustomer currentStatementCustomer;
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            currentStatementCustomer = null;
            if (gridView1.IsEmpty)
            {                
                return;
            }

            var statementCustomer = gridView1.GetRow(gridView1.FocusedRowHandle) as StatementCustomer;
            if (statementCustomer != null)
            {
                currentStatementCustomer = statementCustomer;

                gridControl2.DataSource = currentStatementCustomer.StatementCustomerPayments;
                if (gridView1.Columns[nameof(XPObject.Oid)] != null)
                {
                    gridView2.Columns[nameof(XPObject.Oid)].Visible = false;
                    gridView2.Columns[nameof(XPObject.Oid)].Width = 18;
                    gridView2.Columns[nameof(XPObject.Oid)].OptionsColumn.FixedWidth = true;
                }
            }
        }


        private StatementCustomerPayment currentStatementCustomerPayment;
        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            currentStatementCustomerPayment = null;
            if (gridView2.IsEmpty)
            {
                return;
            }

            var statementCustomerPayment = gridView2.GetRow(gridView2.FocusedRowHandle) as StatementCustomerPayment;
            if (statementCustomerPayment != null)
            {
                currentStatementCustomerPayment = statementCustomerPayment;
            }
            else
            {
                currentStatementCustomerPayment = null;
            }
        }

        private void gridView_MouseDown(object sender, MouseEventArgs e)
        {
            DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            if (dxMouseEventArgs.Button == MouseButtons.Right)
            {
                popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            if (dxMouseEventArgs.Button == MouseButtons.Right)
            {
                popupMenu1.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void gridView2_MouseDown(object sender, MouseEventArgs e)
        {
            DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            if (dxMouseEventArgs.Button == MouseButtons.Right)
            {
                popupMenu2.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void barBtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Statements.Add(new Statement(Session));
        }
        
        private void barBtnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        
        private void barBtnDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            currentStatement?.Delete();
            currentStatement = null;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (currentStatement != null)
            {
                var oid = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.Customer, -1);

                if (oid > 0)
                {
                    var customer = Session.GetObjectByKey<Customer>(oid);
                    if (customer != null)
                    {
                        currentStatement.StatementCustomers.Add(new StatementCustomer(Session) { Customer = customer });
                        currentStatement.Save();
                    }
                }                
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            currentStatementCustomer?.Delete();
            currentStatementCustomer = null;
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (currentStatementCustomer != null)
            {
                var oid = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.PayoutDictionary, -1);

                if (oid > 0)
                {
                    var payoutDictionary = Session.GetObjectByKey<PayoutDictionary>(oid);
                    if (payoutDictionary != null)
                    {
                        currentStatementCustomer.StatementCustomerPayments.Add(new StatementCustomerPayment(Session) { PayoutDictionary = payoutDictionary });
                        currentStatement.Save();
                    }
                }
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            currentStatementCustomerPayment?.Delete();
            currentStatementCustomerPayment = null;
        }
    }
}