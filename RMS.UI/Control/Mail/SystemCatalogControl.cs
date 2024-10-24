using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Menu;
using DevExpress.XtraTreeList.Nodes;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using RMS.UI.Forms.Mail;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace RMS.UI.Control.Mail
{
    public partial class SystemCatalogControl : XtraUserControl
    {
        private Session _session;

        public delegate void FocusedNodeChangedEventHandler(GroupOperator groupOperator, TreeListNode treeListNode);
        public event FocusedNodeChangedEventHandler FocusedNodeChanged;

        public delegate void CloseEventHandler(object sender);
        public event CloseEventHandler Close;

        public delegate void ChoiceEventHandler(object sender, int choice);
        public event ChoiceEventHandler Choice;

        private bool IsVisibleFooter { get; }
        public TreeList TreeList { get; }
        public XPCollection<LetterCatalog> LetterCatalogs { get; set; }

        public SystemCatalogControl(Session session = null, bool isVisible = false)
        {
            InitializeComponent();

            if (session is null)
            {
                _session = DatabaseConnection.GetWorkSession();
            }
            else
            {
                _session = session;
            }

            IsVisibleFooter = isVisible;

            if (isVisible)
            {
                treeListCustomerLetter.ShowFindPanel();
                treeListCustomerLetter.DoubleClick += TreeListCustomerLetter_DoubleClick;
            }
            
            panelControlFooter.Visible = IsVisibleFooter;
            TreeList = treeListCustomerLetter; 
            Dock = DockStyle.Fill;
            
            CreateNodes();            
        }

        private void TreeListCustomerLetter_DoubleClick(object sender, EventArgs e)
        {
            if (int.TryParse(TreeList?.FocusedNode?.GetValue(nameof(XPObject.Oid)).ToString(), out int catalogId))
            {
                Choice?.Invoke(this, catalogId);
                Close?.Invoke(this);
            }
        }

        private bool IsFirstCreateNode;
        public void NullFocusedNode()
        {
            IsFirstCreateNode = true;

            Invoke((Action)delegate
            {
                TreeList.FocusedNode = null;
                TreeList.FocusedColumn = null;
                TreeList.ClearFocusedColumn();
                TreeList.ClearSelection();
            });

            IsFirstCreateNode = false;
        }

        public async void CreateNodes(bool isExpandList = false, CancellationToken token = default)
        {
            try
            {
                IsFirstCreateNode = true;

                TreeList.BeginUnboundLoad();
                TreeList.ClearNodes();

                var criteriaLetterCatalogs = await cls_BaseSpr.GetCustomerCriteria(null, nameof(Customer), nameof(LetterCatalog.Letters));
                LetterCatalogs = new XPCollection<LetterCatalog>(_session, criteriaLetterCatalogs);
                LetterCatalogs.Sorting = new SortingCollection(new SortProperty(nameof(LetterCatalog.DisplayName), DevExpress.Xpo.DB.SortingDirection.Ascending));
                LetterCatalogs.DisplayableProperties = $"{nameof(LetterCatalog.Oid)};" +
                    $"{nameof(LetterCatalog.ParentCatalog)}!Key;" +
                    $"{nameof(LetterCatalog.DisplayName)};" +
                    $"{nameof(LetterCatalog.NotReadLettersCount)};" +
                    $"{nameof(LetterCatalog.LettersCount)}";

                TreeList.DataSource = LetterCatalogs;

                TreeList.KeyFieldName = $"{nameof(LetterCatalog.Oid)}";
                TreeList.ParentFieldName = $"{nameof(LetterCatalog.ParentCatalog)}!Key";

                TreeList.EndUnboundLoad();

                if (isExpandList)
                {
                    TreeList.ExpandAll();
                }

                if (TreeList.Columns[nameof(LetterCatalog.NotReadLettersCount)] != null)
                {
                    TreeList.Columns[nameof(LetterCatalog.NotReadLettersCount)].Width = 100;
                    TreeList.Columns[nameof(LetterCatalog.NotReadLettersCount)].OptionsColumn.FixedWidth = true;
                    TreeList.Columns[nameof(LetterCatalog.NotReadLettersCount)].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    TreeList.Columns[nameof(LetterCatalog.NotReadLettersCount)].OptionsColumn.AllowEdit = false;
                    TreeList.Columns[nameof(LetterCatalog.NotReadLettersCount)].OptionsColumn.ReadOnly = true;

                    TreeList.Columns[nameof(LetterCatalog.NotReadLettersCount)].Visible = !IsVisibleFooter;
                }

                if (TreeList.Columns[nameof(LetterCatalog.LettersCount)] != null)
                {
                    TreeList.Columns[nameof(LetterCatalog.LettersCount)].Width = 65;
                    TreeList.Columns[nameof(LetterCatalog.LettersCount)].OptionsColumn.FixedWidth = true;
                    TreeList.Columns[nameof(LetterCatalog.LettersCount)].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    TreeList.Columns[nameof(LetterCatalog.LettersCount)].OptionsColumn.AllowEdit = false;
                    TreeList.Columns[nameof(LetterCatalog.LettersCount)].OptionsColumn.ReadOnly = true;

                    TreeList.Columns[nameof(LetterCatalog.LettersCount)].Visible = !IsVisibleFooter;
                }

                if (IsVisibleFooter)
                {
                    TreeList.OptionsBehavior.Editable = false;
                }

                TreeList.FocusedNode = null;
                IsFirstCreateNode = false;
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }        

        public void UpdateNotReadLettersCoun()
        {
            try
            {
                if (int.TryParse(treeListCustomerLetter?.FocusedNode?.GetValue(nameof(XPObject.Oid)).ToString(), out int catalogId))
                {
                    var catalog = LetterCatalogs?.FirstOrDefault(f => f.Oid == catalogId);
                    if (catalog != null)
                    {
                        treeListCustomerLetter?.FocusedNode.SetValue(1, catalog.NotReadLettersCount);
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }            
        }

        private void treeListCustomerLetter_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (IsFirstCreateNode)
            {
                return;
            }
            
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
                    var groupOperator = new GroupOperator(GroupOperatorType.And);

                    if (int.TryParse(treeListNode?.GetValue(nameof(XPObject.Oid))?.ToString(), out int catalogId))
                    {
                        var criteriaCatalog = new BinaryOperator(nameof(Letter.LetterCatalog), catalogId);
                        groupOperator.Operands.Add(criteriaCatalog);
                        FocusedNodeChanged?.Invoke(groupOperator, treeListNode);
                    }
                }                
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }            
        }

        private void treeListCustomerLetter_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is TreeList treelist)
            {
                if (e.MenuType == TreeListMenuType.User || e.MenuType == TreeListMenuType.Node)
                {
                    popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void treeListCustomerLetter_MouseLeave(object sender, EventArgs e)
        {
            popupMenu?.HidePopup();
        }

        private void treeListCustomerLetter_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                popupMenu?.HidePopup();
            }
        }

        private async void barBtnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            var parentCatalog = default(LetterCatalog);
            if (int.TryParse(TreeList?.FocusedNode?.GetValue(nameof(XPObject.Oid)).ToString(), out int catalogId))
            {
                parentCatalog = await _session.GetObjectByKeyAsync<LetterCatalog>(catalogId);
            }

            var letterCatalog = new LetterCatalog(_session)
            {
                DisplayName = "Новый каталог",
                ParentCatalog = parentCatalog
            };
            letterCatalog.Save();

            LetterCatalogs.Add(letterCatalog);
        }

        private async void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (int.TryParse(TreeList?.FocusedNode?.GetValue(nameof(XPObject.Oid)).ToString(), out int catalogId))
            {
                var letterCatalog = await _session.GetObjectByKeyAsync<LetterCatalog>(catalogId);

                if (letterCatalog != null)
                {
                    if (letterCatalog.Letters != null && letterCatalog.Letters.Count > 0)
                    {
                        XtraMessageBox.Show("В каталоге присутствуют письма. Удаление не возможно.", "Информационное сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        letterCatalog.Delete();
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close?.Invoke(this);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (int.TryParse(TreeList?.FocusedNode?.GetValue(nameof(XPObject.Oid)).ToString(), out int catalogId))
            {
                Choice?.Invoke(this, catalogId);
                Close?.Invoke(this);
            }
        }

        private void barBtnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            LetterCatalogs?.Reload();
        }

        private async void barBtnFilter_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (int.TryParse(TreeList?.FocusedNode?.GetValue(nameof(XPObject.Oid)).ToString(), out int catalogId))
            {
                var letterCatalog = await _session.GetObjectByKeyAsync<LetterCatalog>(catalogId);

                if (letterCatalog != null)
                {
                    var letterFilterEdit = new LetterFilterEdit(letterCatalog);
                    letterFilterEdit.ShowDialog();
                }
            }            
        }
        
        private void treeListCustomerLetter_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            try
            {
                if (e?.Node?.GetValue(1) is int result && result > 0)
                {
                    if (e.Column.Name.Equals("colDisplayName"))
                    {
                        e.Appearance.FontStyleDelta = FontStyle.Bold;
                    }                   
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }            
        }

        private async void barBtnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (int.TryParse(TreeList?.FocusedNode?.GetValue(nameof(XPObject.Oid)).ToString(), out int catalogId))
            {
                var letterCatalog = await _session.GetObjectByKeyAsync<LetterCatalog>(catalogId);

                if (letterCatalog != null)
                {
                    var form = new LetterCatalogEdit(letterCatalog);
                    form.ShowDialog();
                }
            }
        }
    }
}
