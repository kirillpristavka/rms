using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using RMS.UI.OutProject;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMS.UI
{
    public partial class form_AnySelect : XtraForm
    {
        public form_AnySelect(int iId = -1, int var_view = -1)
        {
            InitializeComponent();

            _iId = iId;
            _FlagOK = false;
            _fl_InitForm = false;

            _VarView = var_view;
            _Type = null;
            _Criteria = null;
            _SelectFields = string.Empty;
            _Sorting = null;

            _FieldComment = string.Empty;
            _FieldId = "Oid";

            _EditFormVaiant = cls_App.EditFormVariants.None;

            _Location = new System.Drawing.Point(-999, -999);
        }

        private bool _FlagOK;
        private int _iId;

        private bool _fl_InitForm;

        private Type _Type;
        private CriteriaOperator _Criteria;
        private string _SelectFields;
        private string _FieldId;
        private string _FieldComment;
        private int _VarView;
        private SortingCollection _Sorting;

        private System.Drawing.Point _Location;

        public bool FlagOK { get { return _FlagOK; } }
        public int Id { get { return _iId; } }

        private cls_App.EditFormVariants _EditFormVaiant;

        public void Set_EditFormVariant(int spr_var) { _EditFormVaiant = (cls_App.EditFormVariants)spr_var; }
        public void Set_VarView(int var_view) { _VarView = var_view; }
        public void Set_FieldId(string field) { _FieldId = field; }
        public void Set_FieldComment(string field) { _FieldComment = field; }
        public void Set_Type(Type type) { _Type = type; }
        public void Set_SelectFields(string select_fields) { _SelectFields = select_fields; }
        public void Set_Criteria(CriteriaOperator criteria) { _Criteria = criteria; }
        public void Set_Sorting(SortingCollection sorting) { _Sorting = sorting; }

        public void Set_Size(int _width, int _height = -1)
        {
            Width = cls_App.GetFormWidth(_width);
            if (_height != -1) Height = cls_App.GetFormHeight(_height);
        }
        public void Set_Location(System.Drawing.Point location) { _Location = location; }

        private async void form_AnySelect_Load(object sender, EventArgs e)
        {
            //Location = new Point(Cursor.Position.X - 100, Cursor.Position.Y - 100);
            //Location = cls_App.GetStartPositionPoint(Cursor.Position.X - 100, Cursor.Position.Y - 100, Width, Height);

            if (_Location.X == -999) Location = cls_App.GetStartPositionPoint(Cursor.Position.X - 100, Cursor.Position.Y - 100, Width, Height);
            else Location = _Location;

            //BVVGlobal.oXpo.SetSessionStoreLocal(BVVGlobal.oXpo.Get_SessionLocal());

            _fl_InitForm = await InitForm();

            //memoComment.Text = "dsgdjud" + Environment.NewLine + "sagdj urt utr" + Environment.NewLine + " <b>rgerr thgth</b>" + Environment.NewLine + "dsgdfjhd";

            FillGrid();

            if (_iId != -1)
                gridView1.FocusedRowHandle = gridView1.LocateByValue(_FieldId, _iId);
            else
                gridView1_FocusedRowChanged(null, null);


            gridView1.OptionsView.ShowFooter = true;

            //gridView1_FocusedRowChanged(null, null);
            //BVVGlobal.oXpo.DisconnectSessionStoreLocal();
        }

        private async Task<bool> InitForm()
        {
            try
            {

                chkbtnFind.Checked = Convert.ToBoolean(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_AnySelect_chkbtnFind", "false"));
                chkbtnFilter.Checked = Convert.ToBoolean(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_AnySelect_chkbtnFilter", "false"));
                chkbtnComment.Checked = Convert.ToBoolean(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_AnySelect_chkbtnComment", "false"));
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }

            return true;
        }

        private async void chkbtnFind_CheckedChanged(object sender, EventArgs e)
        {
            await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_AnySelect_chkbtnFind", chkbtnFind.Checked.ToString(), true, true);
            if (chkbtnFind.Checked)
                gridView1.ShowFindPanel();
            else
                gridView1.HideFindPanel();
        }
        
        private async void chkbtnFilter_CheckedChanged(object sender, EventArgs e)
        {
            await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_AnySelect_chkbtnFilter", chkbtnFilter.Checked.ToString(), true, true);
            gridView1.OptionsView.ShowAutoFilterRow = chkbtnFilter.Checked;
        }
        
        private async void chkbtnComment_CheckedChanged(object sender, EventArgs e)
        {
            await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_AnySelect_chkbtnComment", chkbtnComment.Checked.ToString(), true, true);

            if (chkbtnComment.Checked)
            {
                if (!(memoComment.Visible))
                {
                    memoComment.Visible = true;
                    gridControl1.Height = gridControl1.Height - memoComment.Height - 1;
                }
            }
            else
            {
                if (memoComment.Visible)
                {
                    memoComment.Visible = false;
                    gridControl1.Height = gridControl1.Height + memoComment.Height + 1;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if ((gridView1.Columns[_FieldId] != null) && (gridView1.GetFocusedRowCellValue(_FieldId) != null)) // gridView1.FocusedValue != null))
            {
                _iId = (int)gridView1.GetFocusedRowCellValue(_FieldId);
                _FlagOK = true;
                Close();
            }
            else
                XtraMessageBox.Show("Не возможно выбрать значение !", " Информация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //public object GetObjectById(Session sess = null, int id = -1)
        //{
        //    if (id == -1) id = _iId;

        //    object oo = BVVGlobal.oXpo.GetObjectById(_ClassInfo, id, sess);

        //    return oo;
        //}

        private void FillGrid()
        {
            try
            {
                using (Session sess = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer())
                {

                    //CriteriaOperator criteriaUser = new BinaryOperator("User", sess.GetObjectByKey<set_User>(BVVGlobal.oXpo.User.Oid, true));
                    //CriteriaOperator criteriaTask = new BinaryOperator("Task", sess.GetObjectByKey<set_Task>(BVVGlobal.oXpo.Task.Oid, true));
                    //GroupOperator criteria_full = new GroupOperator(GroupOperatorType.And, new CriteriaOperator[] { criteriaUser, criteriaTask, criteriaOrgs });

                    //XPCollection coll = new XPCollection(sess, sess.GetClassInfo<set_Report>(), criteria, new SortProperty("Date_create", SortingDirection.Ascending), new SortProperty("Org", SortingDirection.Ascending));
                    //XPCollection coll = new XPCollection(sess, sess.GetClassInfo<TestObject>());//, criteria, new SortProperty("Date_create", SortingDirection.Ascending), new SortProperty("Org", SortingDirection.Ascending));
                    //BVVGlobal.oXpo.xpcoll = coll;
                    ////BVVGlobal.oXpo.xpcoll.DisplayableProperties = "Name, Date_create";
                    ////DataTable dt = BVVGlobal.oXpo.XPCollectionToDataTable(BVVGlobal.oXpo.xpcoll);
                    //if (coll != null)
                    //    gridControlHead.DataSource = BVVGlobal.oXpo.xpcoll;

                    //"Oid;Date_create;Name;Prim;Date_send;Date_check;Prim_check;Org.Name"

                    //if (_ClassInfo == null)
                    //{ // пока с ClassInfo работать не умею
                    //switch (_SprVaiant)
                    //{
                    //    case cls_App.SprVariants.SprEnumerationSpr1:
                    //        _ClassInfo = sess.GetClassInfo<set_SprEnumeration>();
                    //        break;
                    //    case cls_App.SprVariants.SprEnumerationSpr2:
                    //        _ClassInfo = sess.GetClassInfo<set_SprEnumeration>();
                    //        break;
                    //    case cls_App.SprVariants.SprEnumerationSpr3:
                    //        _ClassInfo = sess.GetClassInfo<set_SprEnumeration>();
                    //        break;
                    //    case cls_App.SprVariants.Spr_Org:
                    //        _ClassInfo = sess.GetClassInfo<set_Org>();
                    //        break;
                    //    case cls_App.SprVariants.Spr_User:
                    //        _ClassInfo = sess.GetClassInfo<set_User>();
                    //        break;
                    //    case cls_App.SprVariants.Spr_Task:
                    //        _ClassInfo = sess.GetClassInfo<set_Task>();
                    //        break;
                    //    case cls_App.SprVariants.Spr_Raion:
                    //        _ClassInfo = sess.GetClassInfo<set_Raion>();
                    //        break;
                    //    case cls_App.SprVariants.Spr_IFNS:
                    //        _ClassInfo = sess.GetClassInfo<set_SprIFNS>();
                    //        break;
                    //    case cls_App.SprVariants.Spr_Bank:
                    //        _ClassInfo = sess.GetClassInfo<set_SprBank>();
                    //        break;
                    //    case cls_App.SprVariants.Spr_PrintDocumentType:
                    //        _ClassInfo = sess.GetClassInfo<set_PrintDocumentType>();
                    //        break;
                    //}
                    //}

                    if (_SelectFields == String.Empty)
                    {
                        //switch (_SprVaiant)
                        //{
                        //    case cls_App.SprVariants.SprEnumerationSpr1:
                        //        _SelectFields = _FieldId+";Name;FullName";
                        //        break;
                        //    case cls_App.SprVariants.SprEnumerationSpr2:
                        //        _SelectFields = _FieldId+";Name;FullName";
                        //        break;
                        //    case cls_App.SprVariants.SprEnumerationSpr3:
                        //        _SelectFields = _FieldId+";Name;FullName";
                        //        break;
                        //    case cls_App.SprVariants.Spr_Org:
                        //        _SelectFields = _FieldId + ";Raion.Name;INN;Name;FullName;OrgInfo.Phone";
                        //        break;
                        //    case cls_App.SprVariants.Spr_User:
                        //        _SelectFields = _FieldId + ";Name;FullName";
                        //        break;
                        //    case cls_App.SprVariants.Spr_Task:
                        //        _SelectFields = _FieldId + ";Name;FullName";
                        //        break;
                        //    case cls_App.SprVariants.Spr_Raion:
                        //        _SelectFields = _FieldId + ";Oblast.Index;Oblast.Name;Name";
                        //        break;
                        //    case cls_App.SprVariants.Spr_IFNS:
                        //        _SelectFields = _FieldId + ";Kod;Name;Comment";
                        //        break;
                        //    case cls_App.SprVariants.Spr_Bank:
                        //        _SelectFields = _FieldId + ";BIK;Name;Gorod;KSch";
                        //        break;
                        //    case cls_App.SprVariants.Spr_PrintDocumentType:
                        //        _SelectFields = _FieldId + ";Name;FullName;PathToTemplate";
                        //        break;
                        //}
                    }

                    if (ReferenceEquals(_Criteria, null))
                    {
                        //switch (_SprVaiant)
                        //{
                        //    case cls_App.SprVariants.SprEnumerationSpr1:
                        //        _Criteria = new BinaryOperator("Group", (int)cls_App.GroupSprEnumeration.Spr1);
                        //        break;
                        //    case cls_App.SprVariants.SprEnumerationSpr2:
                        //        _Criteria = new BinaryOperator("Group", (int)cls_App.GroupSprEnumeration.Spr2);
                        //        break;
                        //    case cls_App.SprVariants.SprEnumerationSpr3:
                        //        _Criteria = new BinaryOperator("Group", (int)cls_App.GroupSprEnumeration.Spr3);
                        //        break;
                        //    case cls_App.SprVariants.Spr_Org:
                        //        _Criteria = new BinaryOperator("fl_visible", true);
                        //        break;
                        //    case cls_App.SprVariants.Spr_User:
                        //        //_Criteria = new BinaryOperator("Group", (int)cls_App.GroupSprEnumeration.Spr3);
                        //        break;
                        //    case cls_App.SprVariants.Spr_Task:
                        //        //_Criteria = new BinaryOperator("Group", (int)cls_App.GroupSprEnumeration.Spr3);
                        //        break;
                        //    case cls_App.SprVariants.Spr_Raion:
                        //        //_Criteria = new BinaryOperator("Group", (int)cls_App.GroupSprEnumeration.Spr3);
                        //        break;
                        //    case cls_App.SprVariants.Spr_IFNS:
                        //        //_Criteria = new BinaryOperator("Group", (int)cls_App.GroupSprEnumeration.Spr3);
                        //        break;
                        //    case cls_App.SprVariants.Spr_Bank:
                        //        //_Criteria = new BinaryOperator("Group", (int)cls_App.GroupSprEnumeration.Spr3);
                        //        break;
                        //    case cls_App.SprVariants.Spr_PrintDocumentType:
                        //        //_Criteria = new BinaryOperator("Group", (int)cls_App.GroupSprEnumeration.Spr3);
                        //        break;
                        //}
                    }

                    XPView xpView1 = new XPView(sess, _Type, _SelectFields, _Criteria);

                    ////CriteriaOperatorCollection dd = new CriteriaOperatorCollection();
                    //CriteriaOperator daterange = CriteriaOperator.And(
                    if (xpView1 != null)
                    {
                        ////XPView xpView1 = new XPView(sess, sess.GetClassInfo<set_Report>(),new CriteriaOperatorCollection(),criteria);
                        //xpView1.Properties.Add(new DevExpress.Xpo.ViewProperty("Name", DevExpress.Xpo.SortDirection.None, "[Name]", true, true));
                        //xpView1.Properties.Add(new DevExpress.Xpo.ViewProperty("Date_create", DevExpress.Xpo.SortDirection.None, "[Date_create]", true, true));
                        ////////xpView1.Properties.Add(new DevExpress.Xpo.ViewProperty("Date_create", DevExpress.Xpo.SortDirection.None, "[Date_create]", true, true));

                        //CriteriaOperator daterange = CriteriaOperator.And(new BinaryOperator(new OperandProperty("$created"), new OperandValue(minValue), BinaryOperatorType.GreaterOrEqual), new BinaryOperator(new OperandProperty("$created"), new OperandValue(maxValue), BinaryOperatorType.LessOrEqual));
                        //_xpview.Properties.Add(new ViewProperty("erfdatum", sort, "[Erfdatum]", true, true));
                        //_xpview.Properties.Add(new ViewProperty("$created", sort, "[$created]", true, true));
                        //_xpview.Filter = daterange;

                        //xpView1.Properties[0].Name = "New ColumnName";

                        //xpView1.AddProperty("Name");
                        //xpView1.AddProperty("Date_create");
                        //xpView1.AddProperty("Org","Org.Name");


                        // Sorting
                        //switch (_SprVaiant)
                        //{
                        //    case cls_App.SprVariants.SprEnumerationSpr1:
                        //        xpView1.Sorting = new SortingCollection(new SortProperty("Name", DevExpress.Xpo.DB.SortingDirection.Ascending));
                        //        break;
                        //    case cls_App.SprVariants.Spr_Org:
                        //        xpView1.Sorting = new SortingCollection(new SortProperty("Raion.Name", DevExpress.Xpo.DB.SortingDirection.Ascending), new SortProperty("Name", DevExpress.Xpo.DB.SortingDirection.Ascending));
                        //        break;
                        //    case cls_App.SprVariants.Spr_User:
                        //        xpView1.Sorting = new SortingCollection(new SortProperty("Name", DevExpress.Xpo.DB.SortingDirection.Ascending));
                        //        break;
                        //    case cls_App.SprVariants.Spr_Task:
                        //        xpView1.Sorting = new SortingCollection(new SortProperty("Name", DevExpress.Xpo.DB.SortingDirection.Ascending));
                        //        break;
                        //    case cls_App.SprVariants.Spr_Raion:
                        //        xpView1.Sorting = new SortingCollection(new SortProperty("Oblast.Index", DevExpress.Xpo.DB.SortingDirection.Ascending), new SortProperty("Name", DevExpress.Xpo.DB.SortingDirection.Ascending));
                        //        break;
                        //    case cls_App.SprVariants.Spr_IFNS:
                        //        xpView1.Sorting = new SortingCollection(new SortProperty("Kod", DevExpress.Xpo.DB.SortingDirection.Ascending), new SortProperty("Name", DevExpress.Xpo.DB.SortingDirection.Ascending));
                        //        break;
                        //    case cls_App.SprVariants.Spr_Bank:
                        //        xpView1.Sorting = new SortingCollection(new SortProperty("Gorod", DevExpress.Xpo.DB.SortingDirection.Ascending), new SortProperty("BIK", DevExpress.Xpo.DB.SortingDirection.Ascending));
                        //        break;
                        //    case cls_App.SprVariants.Spr_PrintDocumentType:
                        //        //xpView1.Sorting = new SortingCollection(new SortProperty("Gorod", DevExpress.Xpo.DB.SortingDirection.Ascending), new SortProperty("BIK", DevExpress.Xpo.DB.SortingDirection.Ascending));
                        //        break;
                        //}


                        if (_Sorting != null) xpView1.Sorting = _Sorting;

                        gridControl1.DataSource = xpView1;


                        if (_FieldId == "Oid") gridView1.Columns[_FieldId].Visible = false;


                        // наведение красоты:
                        //switch (_SprVaiant)
                        //{
                        //    case cls_App.SprVariants.Spr_Org:
                        //        gridView1.Columns[_FieldId].Visible = false;
                        //        gridView1.Columns["Name"].Caption = "Наименование";
                        //        gridView1.Columns["INN"].Caption = "ИНН";
                        //        gridView1.Columns["FullName"].Caption = "Полное Наименование";
                        //        gridView1.Columns["Raion.Name"].Caption = "Район";
                        //        gridView1.Columns["OrgInfo.Phone"].Caption = "Телефоны";

                        //        gridView1.Columns["Raion.Name"].Width = 90;
                        //        gridView1.Columns["Raion.Name"].OptionsColumn.FixedWidth = true;
                        //        gridView1.Columns["INN"].Width = 70;
                        //        gridView1.Columns["INN"].OptionsColumn.FixedWidth = true;
                        //        gridView1.Columns["Name"].Width = 90;
                        //        gridView1.Columns["Name"].OptionsColumn.FixedWidth = true;

                        //        if (_FieldComment == String.Empty) _FieldComment = "FullName";

                        //        break;
                        //}
                    }
                    sess.Disconnect();
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
            gridView1.Focus();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if ((_FieldComment != String.Empty) && (gridView1.Columns[_FieldComment] != null))
            {
                object comment = gridView1.GetFocusedRowCellValue(_FieldComment);
                if (comment != null)
                    memoComment.Text = comment.ToString();
                else
                    memoComment.Text = string.Empty;
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            //Point pt = view.GridControl.PointToClient(Control.MousePosition);
            //DoRowDoubleClick(sender, pt);
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = view.CalcHitInfo(view.GridControl.PointToClient(MousePosition));

            if (info.InRow || info.InRowCell) { btnEdit_Click(null, null); }
        }

        private void btnNew_Click(object sender, EventArgs e)
        { // добавление строки
            int id_new = -1;

            id_new = EditRecord(id_new);

            if (id_new != -1)
            {
                FillGrid();
                gridView1.FocusedRowHandle = gridView1.LocateByValue(_FieldId, id_new);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        { // редактирование строки
            int id_edit = -1;
            //bool flag_OK = false;

            if ((_FieldId != String.Empty) && (gridView1.Columns[_FieldId] != null))
            {
                object id_obj = gridView1.GetFocusedRowCellValue(_FieldId);
                if (id_obj != null) id_edit = (int)id_obj;
            }

            if (id_edit != -1) id_edit = EditRecord(id_edit);

            if (id_edit != -1)
            { // Нажали ОК - возможно нужно перечитать
                //gridView1.RefreshData(); // не работает (наверное только на живых данных, типа XPCollection)
                FillGrid();
                gridView1.FocusedRowHandle = gridView1.LocateByValue(_FieldId, id_edit);
            }
        }

        private int EditRecord(int id_reccord)
        {
            int id_new = -1;

            switch (_EditFormVaiant)
            {
                case cls_App.EditFormVariants.Edit_Enumeration:
                    //if (id_reccord == -1) return id_new; // пока новые заводить нельзя
                    //using (formEdit_Licenz frm = new formEdit_Licenz(id_reccord, _VarView))
                    //{
                    //    frm.ShowDialog();
                    //    if (frm.FlagOK) id_new = frm.Id; // id_reccord;
                    //}
                    break;
                case cls_App.EditFormVariants.Edit_TablePrintSettings:
                    //if (id_reccord == -1) return id_new; // пока новые заводить нельзя

                    break;
                    //case cls_App.EditFormVariants.Edit_GlobalMessage:
                    //    //using (formEdit_SprEnumeration frm = new formEdit_SprEnumeration(id_reccord) { ParametersObject = new set_SprEnumeration() { Group = (int)cls_App.GroupSprEnumeration.Spr1 } })
                    //    using (formEdit_GlobalMessage frm = new formEdit_GlobalMessage(id_reccord))
                    //    {
                    //        frm.ShowDialog();
                    //        if (frm.FlagOK) id_new = frm.Id; // id_reccord;
                    //    }
                    //    break;
                    //case cls_App.EditFormVariants.formOrgDocDetails:
                    //    if (id_reccord == -1) return id_new; // новые заводить нет необходимости
                    //    using (form_OrgDocDetails frm = new form_OrgDocDetails() { Id = id_reccord })
                    //    {
                    //        frm.ShowDialog();
                    //        //if (frm.FlagOK) id_new = frm.Id; // id_reccord;
                    //    }
                    //    break;
            }

            return id_new;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        { // копирование строки
            int id_current = -1;
            int id_new = -1;

            if ((_FieldId != String.Empty) && (gridView1.Columns[_FieldId] != null))
            {
                object id_obj = gridView1.GetFocusedRowCellValue(_FieldId);
                if (id_obj != null) id_current = (int)id_obj;
            }

            if (id_current == -1) return;

            try
            {
                using (Session sess = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer())
                {
                    _ = new CloneIXPSimpleObjectHelper(sess, sess); //(null, sess);


                    switch (_EditFormVaiant)
                    {
                        case cls_App.EditFormVariants.Edit_Enumeration:
                            //o_old = sess.GetObjectByKey<set_Licenz>(id_current, true);
                            //o_clone = cloneHelper.Clone(o_old, false, true) as set_Licenz; // 2-й параметр false - не синхронизировать основной объект (сделать клона)
                            //// 3-й параметр true  - синхронизировать дочерние объекты (не делать клонов);    если есть ссылки на другие объекты
                            //if (o_clone != null)
                            //{
                            //    //((set_Licenz)o_clone).g_id = Guid.NewGuid().ToString();
                            //    o_clone.Save();
                            //    id_new = o_clone.Oid;
                            //}
                            break;
                            //case cls_App.EditFormVariants.Edit_GlobalMessage:
                            //    o_old = sess.GetObjectByKey<set_GlobalMessage>(id_current, true);
                            //    o_clone = cloneHelper.Clone(o_old) as set_GlobalMessage;
                            //    if (o_clone != null)
                            //    {
                            //        //((set_Licenz)o_clone).g_id = Guid.NewGuid().ToString();
                            //        o_clone.Save();
                            //        id_new = o_clone.Oid;
                            //    }
                            //    break;
                    }

                    //switch (_SprVaiant)
                    //{
                    //    case cls_App.SprVariants.SprEnumerationSpr1:
                    //        set_SprEnumeration o_old = sess.GetObjectByKey<set_SprEnumeration>(id_current,true);
                    //        set_SprEnumeration o_clone = cloneHelper.Clone(o_old) as set_SprEnumeration;
                    //        if (o_clone != null)
                    //        {
                    //            o_clone.g_id = Guid.NewGuid().ToString();
                    //            o_clone.Save();
                    //            id_new = o_clone.Oid;
                    //        }
                    //        break;
                    //}

                    sess.Disconnect();
                }

                if (EditRecord(id_new) != -1)
                {
                    FillGrid();
                    gridView1.FocusedRowHandle = gridView1.LocateByValue(_FieldId, id_new);
                }
                else
                { // откат
                    using (Session sess = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer())
                    {
                        switch (_EditFormVaiant)
                        {
                            case cls_App.EditFormVariants.Edit_Enumeration:
                                //sess.GetObjectByKey<set_Licenz>(id_new, true).Delete();
                                break;
                                //case cls_App.EditFormVariants.Edit_GlobalMessage:
                                //    sess.GetObjectByKey<set_GlobalMessage>(id_new, true).Delete();
                                //    break;
                        }

                        sess.Disconnect();
                    }
                }

                //if (flag_Del)
                //{ // Попробуем просто грохнуть строчку из gridView1

                //    int l_next = gridView1.GetNextVisibleRow(gridView1.FocusedRowHandle);
                //    int l_prev = gridView1.GetPrevVisibleRow(gridView1.FocusedRowHandle);
                //    int l_id_to_focus = -1;
                //    if (l_next >= 0) l_id_to_focus = (int)gridView1.GetRowCellValue(l_next, _FieldId);
                //    else if (l_prev >= 0) l_id_to_focus = (int)gridView1.GetRowCellValue(l_prev, _FieldId);

                //    FillGrid();
                //    if (l_id_to_focus >= 0) gridView1.FocusedRowHandle = gridView1.LocateByValue(_FieldId, l_id_to_focus);

                //    //gridView1.RefreshData(); // не работает (наверное только на живых данных)
                //    //gridView1.DeleteRow(gridView1.FocusedRowHandle); // тоже не работает
                //}
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }

            gridView1.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        { // удаление строки
            int id_del = -1;
            bool flag_Del = false;

            if ((_FieldId != String.Empty) && (gridView1.Columns[_FieldId] != null))
            {
                object id_obj = gridView1.GetFocusedRowCellValue(_FieldId);
                if (id_obj != null) id_del = (int)id_obj;
            }

            if (id_del == -1) return;
            if (XtraMessageBox.Show("Действительно удалить текущую запись ??", " Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) return;

            try
            {
                using (Session sess = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer())
                {
                    switch (_EditFormVaiant)
                    {
                        case cls_App.EditFormVariants.Edit_Enumeration:
                            //o_o = sess.GetObjectByKey<set_GlobalMessage>(id_del, true);
                            //if (o_o != null)
                            //{ // TODO: Check for id_del in otherTables

                            //    o_o.Delete();
                            //    flag_Del = true; // Все таки удалить
                            //}
                            break;
                            //case cls_App.EditFormVariants.Edit_Licenz:
                            //    o_o = sess.GetObjectByKey<set_Licenz>(id_del, true);
                            //    if (o_o != null)
                            //    { // TODO: Check for id_del in otherTables

                            //        o_o.Delete();
                            //        flag_Del = true; // Все таки удалить
                            //    }
                            //    break;
                    }

                    sess.Disconnect();
                }

                if (flag_Del)
                { // Попробуем просто грохнуть строчку из gridView1

                    int l_next = gridView1.GetNextVisibleRow(gridView1.FocusedRowHandle);
                    int l_prev = gridView1.GetPrevVisibleRow(gridView1.FocusedRowHandle);
                    int l_id_to_focus = -1;
                    if (l_next >= 0) l_id_to_focus = (int)gridView1.GetRowCellValue(l_next, _FieldId);
                    else if (l_prev >= 0) l_id_to_focus = (int)gridView1.GetRowCellValue(l_prev, _FieldId);

                    FillGrid();
                    if (l_id_to_focus >= 0) gridView1.FocusedRowHandle = gridView1.LocateByValue(_FieldId, l_id_to_focus);

                    //gridView1.RefreshData(); // не работает (наверное только на живых данных)
                    //gridView1.DeleteRow(gridView1.FocusedRowHandle); // тоже не работает
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }

            gridView1.Focus();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        { // печать справочника (выборки, которая на экране)
            //gridControl1.Print();

            //gridView1.FocusedRowHandle = gridView1.LocateByValue(_FieldId, _iId);
            gridControl1.ShowRibbonPrintPreview();
            //gridControl1.ExportToXls("file_path");
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                //if (XtraMessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                //  DialogResult.Yes)
                //    return;
                //DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                //int i = view.FocusedRowHandle;
                //view.DeleteRow(i);
                ////((XPView)gridControl1.DataSource).Reload(); // ликвидированный объект
                //view.UpdateCurrentRow();
                //view.RefreshData();
                //gridControl1.Refresh();
            }
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                //DevExpress.XtraGrid.Menu.GridViewMenu menu = e.Menu as DevExpress.XtraGrid.Menu.GridViewMenu;
                //Erasing the default menu items 
                //menu.Items.Clear();
                //if (menu.Items.Column != null)
                //{
                //    //Adding new items 
                //    menu.Items.Add(CreateCheckItem("Fixed None", menu.Column, FixedStyle.None,
                //      imageList2.Images[0]));
                //    menu.Items.Add(CreateCheckItem("Fixed Left", menu.Column, FixedStyle.Left,
                //      imageList2.Images[1]));
                //    menu.Items.Add(CreateCheckItem("Fixed Right", menu.Column, FixedStyle.Right,
                //      imageList2.Images[2]));
                //}

                //DevExpress.XtraGrid.Views.Grid.GridView view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                //DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo = view.CalcHitInfo(e.Point);
                //if (hitInfo.InRow || hitInfo.InRowCell)
                //{
                //    view.FocusedRowHandle = hitInfo.RowHandle;
                //    //ContextMenu1.Show(view.GridControl, e.Point);
                //}

                barChkFind.Checked = chkbtnFind.Checked;
                barChkFilter.Checked = chkbtnFilter.Checked;
                barChkComment.Checked = chkbtnComment.Checked;
                popupMenu1.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y)); //e.Point);
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

        private void barBtnSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { btnOK_Click(null, null); }

        private void form_AnySelect_Activated(object sender, EventArgs e)
        { // потому что при form_Load не хочет далеко бежать
            if (_iId != -1)
            {
                if (gridView1.FocusedRowHandle == gridView1.LocateByValue(_FieldId, _iId))
                {
                    gridView1.FocusedRowHandle = 0;
                    gridView1.FocusedRowHandle = gridView1.LocateByValue(_FieldId, _iId);
                }
            }
        }




    }
}