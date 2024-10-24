using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using RMS.Core.Enumerator;
using RMS.Core.Model;
using RMS.UI.Control;
using System;
using System.Windows.Forms;

namespace RMS.UI.Forms
{
    public partial class ProgramEventForm2 : XtraForm
    {
        private Session Session { get; }
        private XPCollection<ProgramEvent> ProgramEvents { get; set; }

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridView);
            BVVGlobal.oFuncXpo.PressEnterGrid<ProgramEvent, ProgramEventEdit>(gridView, isdMenuCRUD: true, isUseSystemControl: false);
        }

        public ProgramEventForm2(Session session)
        {
            InitializeComponent();
            FunctionalGridSetup();

            Session = session ?? BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            ProgramEvents = new XPCollection<ProgramEvent>(Session);
            gridControl.DataSource = ProgramEvents;

            if (gridView.Columns[nameof(ProgramEvent.Oid)] != null)
            {
                gridView.Columns[nameof(ProgramEvent.Oid)].Visible = false;
                gridView.Columns[nameof(ProgramEvent.Oid)].Width = 18;
                gridView.Columns[nameof(ProgramEvent.Oid)].OptionsColumn.FixedWidth = true;
            }

            if(gridView.Columns[nameof(ProgramEvent.Status)] != null)
            {
                var imageCollectionStatus = new ImageCollectionStatus();
                
                RepositoryItemImageComboBox imgStatusStatisticalReport = gridControl.RepositoryItems.Add(nameof(ImageComboBoxEdit)) as RepositoryItemImageComboBox;
                imgStatusStatisticalReport.SmallImages = imageCollectionStatus.imageCollection;
                imgStatusStatisticalReport.Items.Add(new ImageComboBoxItem() { Value = StatusProgramEvent.Done, ImageIndex = 0 });
                imgStatusStatisticalReport.Items.Add(new ImageComboBoxItem() { Value = StatusProgramEvent.Performed, ImageIndex = 9 });

                imgStatusStatisticalReport.GlyphAlignment = HorzAlignment.Center;
                gridView.Columns[nameof(ProgramEvent.Status)].ColumnEdit = imgStatusStatisticalReport;
                gridView.Columns[nameof(ProgramEvent.Status)].Width = 18;
                gridView.Columns[nameof(ProgramEvent.Status)].OptionsColumn.FixedWidth = true;
            }
        }
        
        private void barBtnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            ProgramEvents?.Reload();
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