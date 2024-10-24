using DevExpress.PivotGrid.OLAP.SchemaEntities;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using RMS.Core.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RMS.UI.xUI.PostOffice.Controllers
{
    public partial class LetterCatalogControl : XtraUserControl
    {
        private List<LetterCatalog> _listObj;

        public delegate void FocusedNodeChangedEventHandler(int letterCatalogOid);
        public event FocusedNodeChangedEventHandler FocusedNodeChanged;
        
        public LetterCatalogControl()
        {
            InitializeComponent();
            _listObj = new List<LetterCatalog>();
        }
        
        public void UpdateData(object listObj)
        {

            if (listObj is List<LetterCatalog> list)
            {
                _listObj = list;
                treeList.DataSource = this._listObj;
            }
            else
            {
                treeList.DataSource = new List<LetterCatalog>();
            }
        }
        
        private void LetterCatalogControl_Load(object sender, EventArgs e)
        {
            UpdateData(_listObj);

            treeList.BeginUnboundLoad();
            
            treeList.OptionsBehavior.Editable = false;
            treeList.KeyFieldName = $"{nameof(LetterCatalog.Oid)}";
            treeList.ParentFieldName = $"{nameof(LetterCatalog.ParentCatalog)}";
            
            treeList.EndUnboundLoad();
            
            foreach (var item in treeList.Columns)
            {
                switch (item.FieldName)
                {
                    case nameof(LetterCatalog.DisplayName):
                        item.Caption = "Наименование";
                        item.VisibleIndex = 0;
                        break;

                    case nameof(LetterCatalog.NotReadLettersCount):
                        item.Caption = "Не прочитано";
                        item.VisibleIndex = 1;
                        item.Width = 85;
                        item.OptionsColumn.FixedWidth = true;
                        item.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        item.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
                        break;

                    case nameof(LetterCatalog.LettersCount):
                        item.Caption = "Всего";
                        item.VisibleIndex = 2;
                        item.Width = 85;
                        item.OptionsColumn.FixedWidth = true;
                        item.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        item.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
                        break;
                        
                    default:
                        item.Visible = false;
                        break;
                }
            }            
        }
        
        private void treeList_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            try
            {
                UseFocusedNodeChange(e.Node);
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void UseFocusedNodeChange(TreeListNode treeListNode)
        {
            try
            {
                if (treeListNode != null)
                {
                    if (int.TryParse(treeListNode?.GetValue(nameof(XPObject.Oid))?.ToString(), out int catalogId))
                    {
                        FocusedNodeChanged?.Invoke(catalogId);
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void treeListCustomerLetter_MouseDown(object sender, MouseEventArgs e)
        {            
            var tree = sender as TreeList;
            TreeListHitInfo hi = tree.CalcHitInfo(e.Location);
            if (e.Button == MouseButtons.Right) 
            {
                popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
            else
            {
                popupMenu.HidePopup();
            }
        }

        private async void barBtnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            //var parentCatalog = default(LetterCatalog);
            //if (int.TryParse(TreeList?.FocusedNode?.GetValue(nameof(XPObject.Oid)).ToString(), out int catalogId))
            //{
            //    parentCatalog = await Session.GetObjectByKeyAsync<LetterCatalog>(catalogId);
            //}

            //var letterCatalog = new LetterCatalog(Session)
            //{
            //    DisplayName = "Новый каталог",
            //    ParentCatalog = parentCatalog
            //};
            //letterCatalog.Save();

            //LetterCatalogs.Add(letterCatalog);
        }

        private async void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (int.TryParse(TreeList?.FocusedNode?.GetValue(nameof(XPObject.Oid)).ToString(), out int catalogId))
            //{
            //    var letterCatalog = await Session.GetObjectByKeyAsync<LetterCatalog>(catalogId);

            //    if (letterCatalog != null)
            //    {
            //        if (letterCatalog.Letters != null && letterCatalog.Letters.Count > 0)
            //        {
            //            return;
            //        }
            //        else
            //        {
            //            letterCatalog.Delete();
            //        }
            //    }
            //}
        }
        
        private void barBtnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            //LetterCatalogs?.Reload();
        }

        private async void barBtnFilter_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (int.TryParse(TreeList?.FocusedNode?.GetValue(nameof(XPObject.Oid)).ToString(), out int catalogId))
            //{
            //    var letterCatalog = await Session.GetObjectByKeyAsync<LetterCatalog>(catalogId);

            //    if (letterCatalog != null)
            //    {
            //        var letterFilterEdit = new LetterFilterEdit(letterCatalog);
            //        letterFilterEdit.ShowDialog();
            //    }
            //}            
        }

        private void treeList_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            if (e?.Node?.GetValue(1) is int result && result > 0)
            {
                if (e.Column.Name.Equals("colDisplayName"))
                {
                    e.Appearance.FontStyleDelta = FontStyle.Bold;
                }
            }
        }
        
        private void treeListCustomerLetter_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e?.Node?.GetValue(1) is int result && result > 0)
            {
                if (e.Column.Name.Equals("colDisplayName"))
                {
                    e.Appearance.FontStyleDelta = FontStyle.Bold;
                }
            }
            //var columnCaption = e?.Column?.Caption;
            //if (!string.IsNullOrWhiteSpace(columnCaption)
            //    && columnCaption.Equals("наименование", StringComparison.OrdinalIgnoreCase))
            //{
            //    var node = e.Node;
            //    var columnIndex = e.Column.AbsoluteIndex;



            //    if (int.TryParse(node.GetValue(columnIndex)?.ToString(), out int result) && result > 0)
            //    {
            //        e.Appearance.FontStyleDelta = FontStyle.Bold;
            //        //e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            //    }
            //}
        }

        private async void barBtnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (int.TryParse(TreeList?.FocusedNode?.GetValue(nameof(XPObject.Oid)).ToString(), out int catalogId))
            //{
            //    var letterCatalog = await Session.GetObjectByKeyAsync<LetterCatalog>(catalogId);

            //    if (letterCatalog != null)
            //    {
            //        var form = new LetterCatalogEdit(letterCatalog);
            //        form.ShowDialog();
            //    }
            //}
        }
    }
}
