using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.Mail;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms.Directories
{
    public partial class FormFAQEdit : XtraForm
    {
        private Session Session { get; }
        public WorkZone? WorkZone { get; set; }
        private XPCollection<FAQ> FAQs { get; set; }
        
        public FormFAQEdit(Session session, WorkZone? workZone = null)
        {
            InitializeComponent();

            foreach (WorkZone item in Enum.GetValues(typeof(WorkZone)))
            {
                cmbWorkZone.Properties.Items.Add(item.GetEnumDescription());
            }

            Session = session;
            WorkZone = workZone;
        }

        private CriteriaOperator GetCriteriaOperator()
        {
            if (WorkZone is null)
            {
                return default;
            }
            else
            {
                return new BinaryOperator(nameof(FAQ.WorkZone), WorkZone);
            }
        }
        
        private void FormFAQEdit_Load(object sender, EventArgs e)
        {
            FAQs = new XPCollection<FAQ>(Session);
            FAQs.DisplayableProperties = $"{nameof(FAQ.IsBlock)};{nameof(FAQ.Question)};{nameof(FAQ.WorkZone)}";
            FAQs.Criteria = GetCriteriaOperator();
            gridControl.DataSource = FAQs;

            cmbWorkZone.EditValue = WorkZone?.GetEnumDescription();

            if (gridView.Columns[nameof(FAQ.IsBlock)] != null)
            {
                RepositoryItemImageComboBox imgStatusDeal = gridControl.RepositoryItems.Add(nameof(ImageComboBoxEdit)) as RepositoryItemImageComboBox;
                imgStatusDeal.SmallImages = imageCollection;
                imgStatusDeal.Items.Add(new ImageComboBoxItem() { Value = true, ImageIndex = 0 });

                imgStatusDeal.GlyphAlignment = HorzAlignment.Center;
                gridView.Columns[nameof(FAQ.IsBlock)].ColumnEdit = imgStatusDeal;
                gridView.Columns[nameof(FAQ.IsBlock)].Width = 20;
                gridView.Columns[nameof(FAQ.IsBlock)].OptionsColumn.FixedWidth = true;

                gridView.Columns[nameof(FAQ.IsBlock)].OptionsColumn.ReadOnly = true;
                gridView.Columns[nameof(FAQ.IsBlock)].OptionsColumn.AllowEdit = false;
            }

            TreeListUpdate();
        }

        private XPCollection<FAQCatalog> FAQCatalogs { get; set; }
        public void CreateNodes()
        {
            treeList.BeginUnboundLoad();
            treeList.ClearNodes();

            FAQCatalogs = new XPCollection<FAQCatalog>(Session);
            FAQCatalogs.DisplayableProperties = $"{nameof(FAQCatalog.Oid)};" +
                $"{nameof(FAQCatalog.ParentCatalog)}!Key;" +
                $"{nameof(FAQCatalog.DisplayName)}";

            treeList.DataSource = FAQCatalogs;

            treeList.KeyFieldName = $"{nameof(FAQCatalog.Oid)}";
            treeList.ParentFieldName = $"{nameof(FAQCatalog.ParentCatalog)}!Key";

            treeList.EndUnboundLoad();
            treeList.ExpandAll();            
            treeList.FocusedNode = null;
        }

        private void TreeListUpdate()
        {
            FAQCatalogs = new XPCollection<FAQCatalog>(Session);
            treeList.Columns.Clear();
            treeList.Columns.Add();
            treeList.Columns.Add();

            treeList.Columns[0].Visible = true;
            treeList.Columns[1].Visible = false;

            treeList.ClearNodes();
            
            treeList.AppendNode(new object[] { new FAQCatalog() { DisplayName = "*Все" }, -1 }, -1, -1, -1, -1);
                                    
            foreach (var catalog in FAQCatalogs)
            {
                treeList.AppendNode(new object[] { catalog, catalog.Oid}, -1, -1, -1, -1);
            }

            if (treeList.Columns[1] != null)
            {
                treeList.Columns[1].OptionsColumn.AllowEdit = false;
                treeList.Columns[1].OptionsColumn.ReadOnly = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {            
            FAQCatalogs.Add(new FAQCatalog(Session) 
            { 
                DisplayName = "Новый каталог" 
            });
            
            Session.Save(FAQCatalogs);
            TreeListUpdate();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (treeList.FocusedNode.GetValue(0) is FAQCatalog catalog)
            {
                if (catalog.Oid > 0 )
                {
                    if (XtraMessageBox.Show($"При удалении каталога удаляться и все хранящиеся в нем вопросы, продолжить?",
                                    "Удаление",
                                    MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Question) == DialogResult.OK)
                    {

                        var critreia = new BinaryOperator(nameof(FAQ.FAQCatalog), catalog);
                        var collectionCatalog = new XPCollection<FAQ>(Session, critreia);
                        catalog.Delete();
                        Session.Delete(collectionCatalog);
                        Session.Save(FAQCatalogs);
                    }                    
                    TreeListUpdate();
                }                
            }
        }

        private FAQ currentFAQ { get; set; }
        
        private void gridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            richEditControl.ReadOnly = false;

            if (gridView.IsEmpty)
            {
                richEditControl.HtmlText = default;
                return;
            }
            
            CheckingChangeObject();

            currentFAQ = gridView.GetRow(gridView.FocusedRowHandle) as FAQ;
            if (currentFAQ != null)
            {
                richEditControl.HtmlText = Letter.ByteToString(currentFAQ.Answer);

                if (currentFAQ.IsBlock)
                {
                    richEditControl.ReadOnly = true;
                }
            }
            else
            {
                richEditControl.HtmlText = default;
            }
        }

        private void CheckingChangeObject()
        {
            try
            {
                if (currentFAQ != null)
                {
                    if (!currentFAQ.IsDeleted)
                    {
                        var currentAnswer = Letter.StringToByte(richEditControl.HtmlText);

                        if (currentFAQ.Answer != null && !currentFAQ.Answer.SequenceEqual(currentAnswer))
                        {
                            if (XtraMessageBox.Show($"Вы не сохранили объект: [{currentFAQ.ToString().Trim()}] при перемещении. Применить сохранение?", "Сохранение объекта", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                currentFAQ.Answer = currentAnswer;
                                currentFAQ.Save();
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

        private void gridView_MouseDown(object sender, MouseEventArgs e)
        {
            DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            GridView gridview = sender as GridView;
            _ = gridview.CalcHitInfo(dxMouseEventArgs.Location);
            if (dxMouseEventArgs.Button == MouseButtons.Right)
            {
                popupMenu.ShowPopup(new Point(x: Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void barBtnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            var catalog = default(FAQCatalog);

            if (treeList.FocusedNode.GetValue(0) is FAQCatalog faqCatalog && faqCatalog.Oid > 0)
            {
                catalog = faqCatalog;
            }
            
            FAQs.Add(new FAQ(Session) { FAQCatalog = catalog , WorkZone = WorkZone});
        }

        private void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }
            
            if (currentFAQ != null)
            {
                if (XtraMessageBox.Show($"Вы действительно хотите удалить вопрос: [{currentFAQ}]?",
                                    "Удаление",
                                    MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Question) == DialogResult.OK)
                {
                    currentFAQ.Delete();
                }
            }           
        }

        private void barBtnBlock_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            if (currentFAQ != null)
            {
                if (currentFAQ.IsBlock)
                {
                    if (XtraMessageBox.Show($"Вы действительно хотите разблокировать вопрос: [{currentFAQ}]?",
                                       "Разблокировка",
                                       MessageBoxButtons.OKCancel,
                                       MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        currentFAQ.IsBlock = false;
                        richEditControl.ReadOnly = false;
                    }
                }
                else
                {
                    if (XtraMessageBox.Show($"Вы действительно хотите заблокировать вопрос: [{currentFAQ}]?",
                                       "Блокировка",
                                       MessageBoxButtons.OKCancel,
                                       MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        currentFAQ.IsBlock = true;
                        richEditControl.ReadOnly = true;
                    }
                }
                
                currentFAQ.Save();
            }            
        }

        private void treeList_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            var treList = sender as TreeList;

            if (treList != null && treList.FocusedNode?.GetValue(0) is FAQCatalog catalog)
            {
                if (catalog.DisplayName.Equals("*Все"))
                {
                    FAQs.Filter = null;
                }
                else
                {
                    var critreia = new BinaryOperator(nameof(FAQ.FAQCatalog), catalog);
                    FAQs?.Reload();
                    FAQs.Filter = critreia;
                }                
            }
            else if (treList != null && treList.FocusedNode?.GetValue(1) is int oid && oid > 0)
            {
                catalog = Session.GetObjectByKey<FAQCatalog>(oid);
                if (catalog != null)
                {
                    var critreia = new BinaryOperator(nameof(FAQ.FAQCatalog), catalog);
                    FAQs?.Reload();
                    FAQs.Filter = critreia;
                }
            }            
            else
            {
                FAQs.Filter = null;
            }

            gridView_FocusedRowChanged(gridView, new FocusedRowChangedEventArgs(-1, gridView.FocusedRowHandle));
        }

        private void barBtnCatalogEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (currentFAQ != null)
            {
                var oid = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.FAQCatalog, -1);

                if (oid > 0)
                {
                    var catalog = Session.GetObjectByKey<FAQCatalog>(oid);

                    if (catalog != null)
                    {
                        currentFAQ.FAQCatalog = catalog;
                        currentFAQ.Save();
                    }
                }                
            }            
        }

        private void treeList_ValidateNode(object sender, ValidateNodeEventArgs e)
        {
            var treList = sender as TreeList;

            if (e.Node.Id == 0 || e.Node?.GetValue(0) is string str && str.Contains("*Все"))
            {
                e.Node?.SetValue(0, "*Все");
                return;
            }
            else if (treeList != null && int.TryParse(treList.FocusedNode?.GetValue(1)?.ToString(), out int oid) && oid > 0)
            {
                var catalog = Session.GetObjectByKey<FAQCatalog>(oid);

                if (catalog != null && !catalog.DisplayName.Equals(e.Node?.GetValue(0)?.ToString()))
                {
                    catalog.DisplayName = e.Node?.GetValue(0)?.ToString();
                    catalog.Save();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (currentFAQ != null)
            {
                currentFAQ.Answer = Letter.StringToByte(richEditControl.HtmlText);
                currentFAQ.Save();

                XtraMessageBox.Show($"Объект [{currentFAQ.ToString().Trim()}] сохранен.", "Сохранение объекта", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbWorkZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CheckingChangeObject();
                currentFAQ = null;

                var comboBoxEdit = sender as ComboBoxEdit;                
                var workZone = default(WorkZone?);
                foreach (WorkZone item in Enum.GetValues(typeof(WorkZone)))
                {
                    if (item.GetEnumDescription().Equals(comboBoxEdit.Text))
                    {
                        workZone = item;
                        break;
                    }
                }

                WorkZone = workZone;
                
                FAQs.Criteria = GetCriteriaOperator();
                FAQs.Reload();
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void cmbWorkZone_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var comboBoxEdit = sender as ComboBoxEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                comboBoxEdit.SelectedIndex = -1;
                comboBoxEdit.EditValue = null;
                return;
            }
        }
    }
}