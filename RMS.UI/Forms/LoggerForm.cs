using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using RMS.Core.Model;
using System;
using System.Windows.Forms;
using System.Drawing;
using RMS.UI.Forms.Directories;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using RMS.Core.TG.Core.Models;

namespace RMS.UI.Forms
{
    public partial class LoggerForm : XtraForm
    {        
        private Session _session { get; }
        private XPCollection<TGLog> Logs { get; set; }        

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridView);
            //BVVGlobal.oFuncXpo.PressEnterGrid<Vacation, VacationEdit>(gridView, action: () => UpdateDate());
        }

        public LoggerForm(Session session)
        {
            InitializeComponent();
            FunctionalGridSetup();

            _session = session ?? BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
        }     

        private void Form_Load(object sender, EventArgs e)
        {
            Logs = new XPCollection<TGLog>(_session, null, new SortProperty(nameof(TGLog.Date), DevExpress.Xpo.DB.SortingDirection.Descending));
            gridControl.DataSource = Logs;
            
            if (gridView.Columns[nameof(TGLog.Oid)] != null)
            {
                gridView.Columns[nameof(TGLog.Oid)].Visible = false;
                gridView.Columns[nameof(TGLog.Oid)].Width = 18;
                gridView.Columns[nameof(TGLog.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(TGLog.Date)] != null)
            {
                gridView.Columns[nameof(TGLog.Date)].Width = 125;
                gridView.Columns[nameof(TGLog.Date)].OptionsColumn.FixedWidth = true;
            }            

            if (gridView.Columns[nameof(TGLog.Text)] != null)
            {
                var repositoryItemMemoEdit = gridControl.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                repositoryItemMemoEdit.WordWrap = true;
                gridView.Columns[nameof(TGLog.Text)].ColumnEdit = repositoryItemMemoEdit;
            }
        }
        
        private void barBtnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            Logs?.Reload();
        }        
        
        private void gridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (e.MenuType != GridMenuType.Row)
                    {
                        barBtnDel.Enabled = false;
                    }
                    else
                    {
                        barBtnDel.Enabled = true;
                    }

                    popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void UpdateDate()
        {
            Logs?.Reload();
        }        

        private void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is TGLog log)
            {
                log.Delete();
            }
        } 
        
        private void barBtnUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {
            Logs?.Reload();
        }
    }
}