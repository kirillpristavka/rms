using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.XtraEditors;
using RMS.UI.OutProject;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMS.UI
{
    public partial class form_AnySprTree : XtraForm
    {
        public form_AnySprTree(int iId = -1, object oKod = null, Type type = null, CriteriaOperator criteria = null)
        {
            InitializeComponent();

            _iId = iId;
            _FlagOK = false;
            _fl_InitForm = false;

            _oKod = oKod;

            _ClassInfo = null;
            _Type = type;
            _Criteria = criteria;
            _SelectFields = string.Empty; // select_fields;

            _FieldComment = string.Empty;
            _FieldId = "Oid";
            _FieldKod = "kod";

            _Location = new System.Drawing.Point(-999, -999);
        }

        private bool _FlagOK;
        private int _iId;

        private object _oKod;

        private bool _fl_InitForm;

        private XPClassInfo _ClassInfo;
        private Type _Type;
        private CriteriaOperator _Criteria;
        private string _SelectFields;
        private string _FieldId;
        private string _FieldComment;
        private string _FieldKod;

        private System.Drawing.Point _Location;

        public bool FlagOK { get { return _FlagOK; } }
        public int Id { get { return _iId; } }

        private cls_App.ReferenceBooks _SprVaiant;

        public void Set_SprVariant(int spr_var) { _SprVaiant = (cls_App.ReferenceBooks)spr_var; }
        public void Set_FieldId(string field) { _FieldId = field; }
        public void Set_FieldComment(string field) { _FieldComment = field; }
        public object Kod { get { return _oKod; } set { _oKod = value; } }
        public string FieldKod { set { _FieldKod = value; } }
        public void Set_Type(Type type) { _Type = type; }
        public void Set_Criteria(CriteriaOperator criteria) { _Criteria = criteria; }

        public void Set_Size(int _width, int _height = -1)
        {
            Width = cls_App.GetFormWidth(_width);
            if (_height != -1) Height = cls_App.GetFormHeight(_height);
        }
        public void Set_Location(System.Drawing.Point location) { _Location = location; }


        private async void form_AnySprTree_Load(object sender, EventArgs e)
        {
            //Location = new Point(Cursor.Position.X - 100, Cursor.Position.Y - 100);
            //Location = cls_App.GetStartPositionPoint(Cursor.Position.X - 100, Cursor.Position.Y - 100, Width, Height);

            if (_Location.X == -999) Location = cls_App.GetStartPositionPoint(Cursor.Position.X - 100, Cursor.Position.Y - 100, Width, Height);
            else Location = _Location;

            _fl_InitForm = await InitForm();

            FillGrid();

            if (_iId != -1)
                treeList1.FindNodeByFieldValue(_FieldId, _iId);
            else
                if (_oKod != null)
            {
                DevExpress.XtraTreeList.Nodes.TreeListNode node = treeList1.FindNodeByFieldValue(_FieldKod, _oKod);
                if (node != null) node.Selected = true;
            }
        }

        private async Task<bool> InitForm()
        {
            try
            {
                chkbtnFind.Checked = Convert.ToBoolean(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_AnySprTree_chkbtnFind", "false"));
                chkbtnFilter.Checked = Convert.ToBoolean(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_AnySprTree_chkbtnFilter", "false"));
                chkbtnComment.Checked = Convert.ToBoolean(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_AnySprTree_chkbtnComment", "false"));
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }

            return true;
        }



        private async void chkbtnFind_CheckedChanged(object sender, EventArgs e)
        {
            await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_AnySprTree_chkbtnFind", chkbtnFind.Checked.ToString(), true, true);
        }
        private async void chkbtnFilter_CheckedChanged(object sender, EventArgs e)
        {
            await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_AnySprTree_chkbtnFilter", chkbtnFilter.Checked.ToString(), true, true);
        }
        private async void chkbtnComment_CheckedChanged(object sender, EventArgs e)
        {
            await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_AnySprTree_chkbtnComment", chkbtnComment.Checked.ToString(), true, true);

            if (chkbtnComment.Checked)
            {
                if (!(memoComment.Visible))
                {
                    memoComment.Visible = true;
                    treeList1.Height = treeList1.Height - memoComment.Height - 1;
                }
            }
            else
            {
                if (memoComment.Visible)
                {
                    memoComment.Visible = false;
                    treeList1.Height = treeList1.Height + memoComment.Height + 1;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DevExpress.XtraTreeList.Nodes.TreeListNode node = treeList1.FocusedNode;
            if ((node != null) && (treeList1.Columns[_FieldId] != null))
            {
                if (node.HasChildren == false)
                {
                    _iId = (int)node.GetValue(_FieldId);
                    _oKod = node.GetValue(_FieldKod);
                    _FlagOK = true;
                    Close();
                }
                else
                    XtraMessageBox.Show("Выбор данного элемента противоречит бизнес-логике нашего приложения !", " Информация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                XtraMessageBox.Show("Не возможно выбрать значение !", " Информация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void FillGrid()
        {
            try
            {
                using (Session sess = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer())
                {
                    if (_Type == null) _Type = cls_BaseSpr.GetTypeSprVariants((int)_SprVaiant);
                    _ClassInfo = sess.GetClassInfo(_Type);

                    if (_SelectFields == String.Empty)
                    {
                        switch (_SprVaiant)
                        {
                            default:
                                break;
                        }
                    }

                    if (ReferenceEquals(_Criteria, null))
                    {
                        switch (_SprVaiant)
                        {
                            default:
                                break;
                        }
                    }

                    XPView xpView1 = new XPView(sess, _ClassInfo, _SelectFields, _Criteria);

                    if (xpView1 != null)
                    {
                        treeList1.OptionsBehavior.PopulateServiceColumns = true; // Показывать ключевые поля ("kod", "parent_kod")
                        treeList1.DataSource = xpView1;

                        switch (_SprVaiant)
                        {
                            default:
                                break;
                        }
                    }
                    sess.Disconnect();
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
            //gridView1.Focus();
        }


        private void btnNew_Click(object sender, EventArgs e)
        { // добавление строки
            int id_new = -1;

            id_new = EditRecord(id_new);

            if (id_new != -1)
            {
                FillGrid();
                //gridView1.FocusedRowHandle = gridView1.LocateByValue(_FieldId, id_new);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        { // редактирование строки
            int id_edit = -1;

            //if ((_FieldId != String.Empty) && (gridView1.Columns[_FieldId] != null))
            //{
            //    object id_obj = gridView1.GetFocusedRowCellValue(_FieldId);
            //    if (id_obj != null) id_edit = (int)id_obj;
            //}

            if (id_edit != -1)
            {
                id_edit = EditRecord(id_edit);
            }

            if (id_edit != -1)
            { // Нажали ОК - возможно нужно перечитать
                FillGrid();
                //gridView1.FocusedRowHandle = gridView1.LocateByValue(_FieldId, id_edit);
            }
        }

        private int EditRecord(int id_reccord)
        {
            int id_new = -1;

            //switch (_SprVaiant)
            {
                //
            }

            return id_new;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        { // копирование строки
            int id_current = -1;
            int id_new = -1;

            //if ((_FieldId != String.Empty) && (gridView1.Columns[_FieldId] != null))
            //{
            //    object id_obj = gridView1.GetFocusedRowCellValue(_FieldId);
            //    if (id_obj != null) id_current = (int)id_obj;
            //}

            if (id_current == -1) return;

            try
            {
                using (Session sess = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer())
                {
                    _ = new CloneIXPSimpleObjectHelper(sess, sess); //(null, sess);

                    //switch (_SprVaiant)
                    {
                        //
                    }

                    sess.Disconnect();
                }

                if (EditRecord(id_new) != -1)
                {
                    FillGrid();
                    //gridView1.FocusedRowHandle = gridView1.LocateByValue(_FieldId, id_new);
                }
                else
                { // откат
                    using (Session sess = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer())
                    {
                        //switch (_SprVaiant)
                        {
                            //
                        }

                        sess.Disconnect();
                    }
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }

            //gridView1.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        { // удаление строки
            int id_del = -1;
            bool flag_Del = false;

            //if ((_FieldId != String.Empty) && (gridView1.Columns[_FieldId] != null))
            //{
            //    object id_obj = gridView1.GetFocusedRowCellValue(_FieldId);
            //    if (id_obj != null) id_del = (int)id_obj;
            //}

            if (id_del == -1) return;
            if (XtraMessageBox.Show("Действительно удалить текущую запись ??", " Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) return;

            try
            {
                using (Session sess = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer())
                {
                    //switch (_SprVaiant)
                    {
                        //
                    }

                    sess.Disconnect();
                }

                if (flag_Del)
                { // Попробуем просто грохнуть строчку из gridView1

                    //int l_next = gridView1.GetNextVisibleRow(gridView1.FocusedRowHandle);
                    //int l_prev = gridView1.GetPrevVisibleRow(gridView1.FocusedRowHandle);
                    //int l_id_to_focus = -1;
                    //if (l_next >= 0) l_id_to_focus = (int)gridView1.GetRowCellValue(l_next, _FieldId);
                    //else if (l_prev >= 0) l_id_to_focus = (int)gridView1.GetRowCellValue(l_prev, _FieldId);

                    FillGrid();
                    //if (l_id_to_focus >= 0) gridView1.FocusedRowHandle = gridView1.LocateByValue(_FieldId, l_id_to_focus);
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }

            //gridView1.Focus();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        { // печать справочника (выборки, которая на экране)
            //gridControl1.ShowRibbonPrintPreview();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                //
            }
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                barChkFind.Checked = chkbtnFind.Checked;
                barChkFilter.Checked = chkbtnFilter.Checked;
                barChkComment.Checked = chkbtnComment.Checked;
                popupMenu1.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void barBtnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { btnNew_Click(null, null); }
        private void barBtnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { btnEdit_Click(null, null); }
        private void barBtnCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { btnCopy_Click(null, null); }
        private void barBtnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { btnDelete_Click(null, null); }

        private void barChkFind_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { chkbtnFind.Checked = !chkbtnFind.Checked; }
        private void barChkFilter_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { chkbtnFilter.Checked = !chkbtnFilter.Checked; }
        private void barChkComment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { chkbtnComment.Checked = !chkbtnComment.Checked; }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            DevExpress.XtraTreeList.Nodes.TreeListNode node = treeList1.FocusedNode;
            if (node != null)
                if ((_FieldComment != String.Empty) && (treeList1.Columns[_FieldComment] != null))
                    memoComment.Text = (node.GetValue(_FieldComment) == null) ? string.Empty : node.GetValue(_FieldComment).ToString();
        }




        private void treeList1_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.HasChildren)
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);

            if (e.Node.Expanded)
            {
                e.Node.ImageIndex = (e.Node.HasChildren) ? 4 : 0;
                e.Node.SelectImageIndex = (e.Node.HasChildren) ? 5 : 1;
            }
            else
            {
                e.Node.ImageIndex = (e.Node.HasChildren) ? 2 : 0;
                e.Node.SelectImageIndex = (e.Node.HasChildren) ? 3 : 1;
            }
        }






    }
}