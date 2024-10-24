using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using RMS.Core.ModelSbis;
using PulsLibrary.Extensions.DevForm;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;

namespace RMS.UI.Forms
{
    public partial class ReportSBISForm : XtraForm
    {        
        private Session _session { get; }
        private XPCollection<ReportSBIS> _collection { get; set; }        

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridView);
            //BVVGlobal.oFuncXpo.PressEnterGrid<Vacation, VacationEdit>(gridView, action: () => UpdateDate());
        }

        public ReportSBISForm(Session session)
        {
            InitializeComponent();
            FunctionalGridSetup();

            _session = session ?? BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
        }     

        private void Form_Load(object sender, EventArgs e)
        {
            _collection = new XPCollection<ReportSBIS>(_session);
            gridControl.DataSource = _collection;

            gridControl.GridControlSetup();
            gridView.ColumnSetup(nameof(ReportSBIS.Oid), isVisible: false);
            gridView.ColumnSetup(nameof(ReportSBIS.ReportAdditionalData), isVisible: false);
            gridView.GridViewSetup(isColumnAutoWidth: false);

            foreach (GridColumn gridColumn in gridView.Columns)
            {
                var columnName = gridColumn.Name;
                if (columnName.Contains("Date"))
                {
                    gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    gridColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gridColumn.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                    if (columnName.Contains(nameof(ReportSBIS.DateOfBirth)))
                    {
                        gridColumn.DisplayFormat.FormatString = @"dd.MM.yyyy";
                    }
                    else
                    {
                        gridColumn.DisplayFormat.FormatString = @"dd.MM.yyyy (HH:mm:ss)";
                    }
                }
            }

            var repositoryItemMemoEdit = gridControl.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
            repositoryItemMemoEdit.WordWrap = true;
            if (gridView.Columns[nameof(ReportSBIS.BaseObjReportAdditionalDataString)] is GridColumn columnDataReportAdditionalData)
            {
                columnDataReportAdditionalData.ColumnEdit = repositoryItemMemoEdit;
                columnDataReportAdditionalData.Width = 300;
                columnDataReportAdditionalData.OptionsColumn.FixedWidth = true;
                columnDataReportAdditionalData.Visible = false;
            }

            gridView.BestFitColumns();
        }
        
        private void barBtnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            _collection?.Reload();
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
            _collection?.Reload();
        }        

        private void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is ReportSBIS obj)
            {
                obj?.Delete();
            }
        } 
        
        private void barBtnUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {
            _collection?.Reload();
        }
    }
}