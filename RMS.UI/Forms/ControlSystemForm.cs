using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using RMS.Core.Model.Notifications;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Windows.Forms;

namespace RMS.UI.Forms
{
    public partial class ControlSystemForm : XtraForm
    {
        private Session Session { get; }
        private XPCollection<ControlSystem> ControlSystems { get; set; }

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridView);
            BVVGlobal.oFuncXpo.PressEnterGrid<ControlSystem, ControlSystemEdit>(gridView, isdMenuCRUD: true, isUseSystemControl: false);
        }

        public ControlSystemForm(Session session)
        {
            InitializeComponent();
            FunctionalGridSetup();

            Session = session ?? BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
        }

        private void TaskForm_Load(object sender, EventArgs e)
        {
            ControlSystems = new XPCollection<ControlSystem>(Session);
            gridControl.DataSource = ControlSystems;

            if (gridView.Columns[nameof(ControlSystem.Oid)] != null)
            {
                gridView.Columns[nameof(ControlSystem.Oid)].Visible = false;
                gridView.Columns[nameof(ControlSystem.Oid)].Width = 18;
                gridView.Columns[nameof(ControlSystem.Oid)].OptionsColumn.FixedWidth = true;
            }
        }
        
        private void barBtnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            ControlSystems?.Reload();
        }

        private void gridViewTasks_MouseDown(object sender, MouseEventArgs e)
        {
            //DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            //GridView gridview = sender as GridView;
            //GridHitInfo gridHitInfo = gridview.CalcHitInfo(dxMouseEventArgs.Location);
            ////if ((gridHitInfo.InRow || gridHitInfo.InRowCell) && dxMouseEventArgs.Button == MouseButtons.Right)

            //if (gridHitInfo.HitTest != GridHitTest.Footer && gridHitInfo.HitTest != GridHitTest.Column)
            //{
            //    if (!gridViewTasks.IsEmpty)
            //    {
            //        Tasks?.Reload();
            //    }

            //    if (dxMouseEventArgs.Button == MouseButtons.Right)
            //    {
            //        popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            //    }
            //}
        }
    }
}