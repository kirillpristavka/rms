using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;




namespace PulsPlusSpace
{
    public partial class form_AnySpr2 : XtraForm
    {
        public form_AnySpr2(int iId = -1, Type type = null, CriteriaOperator criteria = null, SortProperty sort = null, string select_fields = "")
        {
            InitializeComponent();
            
            _iId = iId;
            _FlagOK = false;
            _fl_InitForm = false;

            //_ClassInfo = null;
            _Type = type;
            _Criteria = criteria;
            _Sorting = sort;
            _SelectFields = select_fields;

            _FieldComment = "";
            _FieldId = "Oid";
            _FlagSprEnumeration = false;
            _FlagSprDateRange = false;

            bool_VariantValue1 = false;
            bool_VariantValue2 = false;
            bool_VariantValue3 = false;
            int_VariantValue1 = -1;
            int_VariantValue2 = -1;
            int_VariantValue3 = -1;
            str_VariantValue1 = "";
            str_VariantValue2 = "";
            str_VariantValue3 = "";

            _Location = new System.Drawing.Point(-999, -999);

            _ImageIndexCollection = null;
            _ImageIndexField = "";
        }

        private bool _FlagOK;
        private int _iId;
        //private cls_App.SprVariants _SprVariant;

        private bool _fl_InitForm;

        //private XPClassInfo _ClassInfo;
        private Type _Type;
        private CriteriaOperator _Criteria;
        private SortProperty _Sorting;
        private string _SelectFields;
        private string _FieldId;
        private string _FieldComment;
        private bool _FlagSprEnumeration;
        private bool _FlagSprDateRange;

        private System.Drawing.Point _Location;

        public bool FlagOK { get { return _FlagOK; } }
        public int Id { get { return _iId; } }

        // + некие переменные параметров
        public bool bool_VariantValue1 { get; set; }
        public bool bool_VariantValue2 { get; set; }
        public bool bool_VariantValue3 { get; set; }
        public int int_VariantValue1 { get; set; }
        public int int_VariantValue2 { get; set; }
        public int int_VariantValue3 { get; set; }
        public string str_VariantValue1 { get; set; }
        public string str_VariantValue2 { get; set; }
        public string str_VariantValue3 { get; set; }
        // -

        private cls_App.SprVariants _SprVaiant;
        private DevExpress.Utils.ImageCollection _ImageIndexCollection;
        private string _ImageIndexField;

        public void Set_SprVariant(int spr_var) { _SprVaiant = (cls_App.SprVariants)spr_var; }
        public void Set_FieldId(string field) { _FieldId = field; }
        public void Set_FieldComment(string field) { _FieldComment = field; }
        public void Set_ImageIndexCollection(DevExpress.Utils.ImageCollection image_collection, string image_field = "ImageIndex") { _ImageIndexCollection = image_collection; _ImageIndexField = image_field; }
        public void Set_Type(Type type) { _Type = type; }
        public void Set_Criteria(CriteriaOperator criteria) { _Criteria = criteria; }
        public void Set_Sorting(SortProperty sort) { _Sorting = sort; }

        public void Set_Size(int _width, int _height = -1)
        {
            Width = cls_App.GetFormWidth(_width);
            if (_height != -1) Height = cls_App.GetFormHeight(_height);
        }
        public void Set_Location(System.Drawing.Point location) { _Location = location; }


        private void form_AnySpr_Load(object sender, EventArgs e)
        {
            if (_Location.X == -999) Location = cls_App.GetStartPositionPoint(Cursor.Position.X - 100, Cursor.Position.Y - 100, Width, Height);
            else Location = _Location;

            //if (_Location.X == -999) Location = cls_App.GetStartPositionPoint(100, 145, Width, Height);
            //else Location = _Location;

            //BVVGlobal.oXpo.SetSessionStoreLocal(BVVGlobal.oXpo.Get_SessionLocal());
            //btnNew.Visible = false;
            //btnEdit.Visible = false;
            //btnDelete.Visible = false;
            //btnCopy.Visible = false;
            //btnPrint.Visible = false;

            _fl_InitForm = InitForm();

            if (cls_BaseSpr.IsSprDateRanges((int)_SprVaiant)) _FlagSprDateRange = true;
            if (cls_BaseSpr.IsSprEnumeration((int)_SprVaiant)) _FlagSprEnumeration = true;

            chkFlAllDateRange.Visible = _FlagSprDateRange;

            FillGrid();

            if (_iId != -1)
                gridView1.FocusedRowHandle = gridView1.LocateByValue(_FieldId, _iId);
            else
                gridView1_FocusedRowChanged(null, null);
        }
        
        private bool InitForm()
        {
            try
            {
                BVVGlobal.oXpo.SetSessionStoreLocal(BVVGlobal.oXpo.Get_SessionLocal());
                using (Session sess = BVVGlobal.oXpo.Get_SessionLocal())
                {
                    chkbtnFind.Checked = Convert.ToBoolean(BVVGlobal.oFuncXpo.Get_LocalSettings_Param(sess, "form_AnySpr_chkbtnFind", "false"));
                    chkbtnFilter.Checked = Convert.ToBoolean(BVVGlobal.oFuncXpo.Get_LocalSettings_Param(sess, "form_AnySpr_chkbtnFilter", "false"));
                    chkbtnComment.Checked = Convert.ToBoolean(BVVGlobal.oFuncXpo.Get_LocalSettings_Param(sess, "form_AnySpr_chkbtnComment", "false"));
                    //chkbtnFind_CheckedChanged(null, null);
                    //chkbtnFilter_CheckedChanged(null, null);
                    //chkbtnComment_CheckedChanged(null, null);

                    //sess.Disconnect();
                }
                BVVGlobal.oXpo.DisconnectSessionStoreLocal();
            }
            catch (Exception ex)
            {
                BVVGlobal._logger.Debug(String.Format("{0}\r\n                         {1}", ex.Message, ex.StackTrace));
                XtraMessageBox.Show(ex.Message);
            }

            return true;
        }

        

        private void chkbtnFind_CheckedChanged(object sender, EventArgs e)
        {
            Session sess = BVVGlobal.oXpo.Get_SessionLocal(); // не могу сделать using - глобально запоминается сессия
            BVVGlobal.oFuncXpo.Get_LocalSettings_Param(sess, "form_AnySpr_chkbtnFind", chkbtnFind.Checked.ToString(), true, true);
            if (chkbtnFind.Checked)
                gridView1.ShowFindPanel();
            else
                gridView1.HideFindPanel();

            if (_fl_InitForm)
                sess.Disconnect();
        }
        private void chkbtnFilter_CheckedChanged(object sender, EventArgs e)
        {
            Session sess = BVVGlobal.oXpo.Get_SessionLocal(); // не могу сделать using - глобально запоминается сессия
            BVVGlobal.oFuncXpo.Get_LocalSettings_Param(sess, "form_AnySpr_chkbtnFilter", chkbtnFilter.Checked.ToString(), true, true);
            gridView1.OptionsView.ShowAutoFilterRow = chkbtnFilter.Checked;

            if (_fl_InitForm)
                sess.Disconnect();
        }
        private void chkbtnComment_CheckedChanged(object sender, EventArgs e)
        {
            Session sess = BVVGlobal.oXpo.Get_SessionLocal(); // не могу сделать using - глобально запоминается сессия
            BVVGlobal.oFuncXpo.Get_LocalSettings_Param(sess, "form_AnySpr_chkbtnComment", chkbtnComment.Checked.ToString(), true, true);

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

            if (_fl_InitForm)
                sess.Disconnect();
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
                XtraMessageBox.Show("Не возможно выбрать значение !"," Информация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void FillGrid()
        {
            try
            {
                using (Session sess = BVVGlobal.oXpo.Get_Session())
                {
                    _Type = cls_BaseSpr.GetTypeSprVariants((int)_SprVaiant);

                    //_ClassInfo = sess.GetClassInfo(_Type);

                    if (_FlagSprEnumeration)
                    {
                        if (ReferenceEquals(_ImageIndexCollection, null))
                        { _SelectFields = _FieldId + ";fl_def;Name;FullName"; }
                        else { _SelectFields = _FieldId + ";fl_def;ImageIndex;Name;FullName"; }
                    }
                    else
                    {
                        if (_SelectFields == String.Empty)
                        {
                            switch (_SprVaiant)
                            {
                                case cls_App.SprVariants.Spr_Org:
                                    _SelectFields = _FieldId + ";Inn;Name;FullName";
                                    break;
                                case cls_App.SprVariants.Spr_User:
                                    _SelectFields = _FieldId + ";Name;FullName";
                                    break;
                            }
                        }
                    }

                    if (ReferenceEquals(_Criteria, null))
                    {
                        if (_FlagSprEnumeration)
                        { _Criteria = new BinaryOperator("Group", (int)_SprVaiant); }
                        else
                        {
                            switch (_SprVaiant)
                            {
                                case cls_App.SprVariants.Spr_Org:
                                    //_Criteria = new BinaryOperator("Group", (int)cls_App.GroupSprEnumeration.Spr3);
                                    break;
                                case cls_App.SprVariants.Spr_User:
                                    //_Criteria = new BinaryOperator("Group", (int)cls_App.GroupSprEnumeration.Spr3);
                                    break;
                            }
                        }
                    }

                    CriteriaOperator criteria_final = _Criteria;
                    string fields_final = _SelectFields;

                    // при активной галочке "все временные диапазоны" (у таблиз где есть период дат)
                    if ((_FlagSprDateRange) && (bool)chkFlAllDateRange.EditValue == false)
                    { // условие на вхождение во временной период
                        //DateTime ot_date = new DateTime(BVVGlobal.oApp.AppParam.OtchYear, BVVGlobal.oApp.AppParam.OtchMonth, 1); // берем не текущую дату и не рабочую, а отчетную (состояние на момент формирований отчетов)
                        //criteria_final = new GroupOperator(GroupOperatorType.And, new CriteriaOperator[] { _Criteria, CriteriaOperator.Parse("(GetYear(date_beg)<=? or IsNullOrEmpty(date_beg)) and (GetYear(date_end)>=? or IsNullOrEmpty(date_end))", BVVGlobal.oApp.AppParam.OtchYear, BVVGlobal.oApp.AppParam.OtchYear) });
                        criteria_final = new GroupOperator(GroupOperatorType.And, new CriteriaOperator[] { _Criteria, cls_BaseSpr.GetCriteriaDateRange() });
                        //new DateTime(BVVGlobal.oApp.WorkDate.Year, BVVGlobal.oApp.WorkDate.Month, 1).AddMonths(1).AddDays(-1) - последний день месяца
                        //fields_final = _SelectFields + ";date_beg;date_end";
                    }
                    
                    XPView xpView1 = new XPView(sess, _Type, fields_final, criteria_final);
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
                        if (ReferenceEquals(_Sorting, null))
                        {
                            if (_FlagSprEnumeration)
                            { xpView1.Sorting = new SortingCollection(new SortProperty("Name", DevExpress.Xpo.DB.SortingDirection.Ascending)); }
                            else
                            {
                                switch (_SprVaiant)
                                {
                                    case cls_App.SprVariants.Spr_Org:
                                        xpView1.Sorting = new SortingCollection(new SortProperty("Index", DevExpress.Xpo.DB.SortingDirection.Ascending), new SortProperty("Name", DevExpress.Xpo.DB.SortingDirection.Ascending));
                                        break;
                                    case cls_App.SprVariants.Spr_User:
                                        xpView1.Sorting = new SortingCollection(new SortProperty("Name", DevExpress.Xpo.DB.SortingDirection.Ascending));
                                        break;
                                }
                            }
                        }
                        else xpView1.Sorting = new SortingCollection(_Sorting);

                        gridControl1.DataSource = xpView1;


                        if (_ImageIndexCollection != null)
                        {
                            RepositoryItemImageComboBox imageCombo = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as RepositoryItemImageComboBox;
                            imageCombo.SmallImages = _ImageIndexCollection;
                            for (int i = 0; i < _ImageIndexCollection.Images.Count; i++) imageCombo.Items.Add(new ImageComboBoxItem(i));
                            imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
                            gridView1.Columns[_ImageIndexField].ColumnEdit = imageCombo;
                        }

                        #region НаведениеКрасоты
                        if (_FlagSprEnumeration)
                        {
                            gridView1.Columns[_FieldId].Visible = false;
                            gridView1.Columns["fl_def"].Caption = "v";
                            gridView1.Columns["fl_def"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            gridView1.Columns["Name"].Caption = "Наименование";
                            gridView1.Columns["FullName"].Caption = "Полное Наименование";
                            gridView1.Columns["fl_def"].Width = 20;
                            gridView1.Columns["fl_def"].OptionsColumn.FixedWidth = true;

                            if (!ReferenceEquals(_ImageIndexCollection, null))
                            {
                                gridView1.Columns["ImageIndex"].Caption = "!";
                                gridView1.Columns["ImageIndex"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                gridView1.Columns["ImageIndex"].Width = 20;
                                gridView1.Columns["ImageIndex"].OptionsColumn.FixedWidth = true;
                            }

                            if (_FieldComment == String.Empty) _FieldComment = "FullName";
                        }
                        else
                        {
                            switch (_SprVaiant)
                            {
                                case cls_App.SprVariants.Spr_Org:
                                    gridView1.Columns[_FieldId].Visible = false;
                                    gridView1.Columns["Inn"].Caption = "ИНН";
                                    //gridView1.Columns["RSch"].Caption = "р.счет";
                                    gridView1.Columns["Name"].Caption = "Наименование";
                                    gridView1.Columns["FullName"].Caption = "Полное Наименование";

                                    gridView1.Columns["Inn"].Width = 70;
                                    gridView1.Columns["Inn"].OptionsColumn.FixedWidth = true;
                                    //gridView1.Columns["RSch"].Width = 90;
                                    //gridView1.Columns["RSch"].OptionsColumn.FixedWidth = true;

                                    if (_FieldComment == String.Empty) _FieldComment = "FullName";
                                    break;
                                case cls_App.SprVariants.Spr_User:
                                    gridView1.Columns[_FieldId].Visible = false;
                                    gridView1.Columns["Name"].Caption = "Наименование";
                                    gridView1.Columns["FullName"].Caption = "Полное Наименование";

                                    gridView1.Columns["Name"].Width = 90;
                                    gridView1.Columns["Name"].OptionsColumn.FixedWidth = true;

                                    if (_FieldComment == String.Empty) _FieldComment = "FullName";
                                    break;
                                //case cls_App.SprVariants.Spr_Vfo:
                                //    gridView1.Columns[_FieldId].Visible = false;

                                //    gridView1.Columns["CodeVfo"].Caption = "Код";
                                //    gridView1.Columns["Name"].Caption = "Аббревиатура";
                                //    gridView1.Columns["FullName"].Caption = "Наименование";

                                //    gridView1.Columns["CodeVfo"].Width = 30;
                                //    gridView1.Columns["Name"].Width = 60;
                                //    gridView1.Columns["CodeVfo"].OptionsColumn.FixedWidth = true;
                                //    gridView1.Columns["Name"].OptionsColumn.FixedWidth = true;

                                //    gridView1.Columns["CodeVfo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                                //    if (_FieldComment == String.Empty) _FieldComment = "Name";

                                //    if ((bool)chkFlAllDateRange.EditValue == true)
                                //    {
                                //        gridView1.Columns["date_beg"].Width = 52;
                                //        gridView1.Columns["date_beg"].Visible = true;
                                //        gridView1.Columns["date_beg"].Caption = "Начало";
                                //        gridView1.Columns["date_beg"].OptionsColumn.FixedWidth = true;
                                //        gridView1.Columns["date_beg"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                //        gridView1.Columns["date_beg"].DisplayFormat.FormatString = "dd.MM.yy";
                                //        gridView1.Columns["date_end"].Width = 52;
                                //        gridView1.Columns["date_end"].Visible = true;
                                //        gridView1.Columns["date_end"].Caption = "Окончание";
                                //        gridView1.Columns["date_end"].OptionsColumn.FixedWidth = true;
                                //        gridView1.Columns["date_end"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                //        gridView1.Columns["date_end"].DisplayFormat.FormatString = "dd.MM.yy";
                                //    }
                                //    else
                                //    {
                                //        gridView1.Columns["date_beg"].Visible = false;
                                //        gridView1.Columns["date_end"].Visible = false;
                                //    }

                                //    break;
                            }
                        }
                        #endregion
                    }
                    sess.Disconnect();
                }
            }
            catch (Exception ex)
            {
                BVVGlobal._logger.Debug(String.Format("{0}\r\n                         {1}", ex.Message, ex.StackTrace));
                XtraMessageBox.Show(ex.Message);
            }
            gridView1.Focus();
            gridView1_FocusedRowChanged(null, null); // обновить Комментарий снизу
            // Зачем это при открытии ?
            //gridView1.FocusedRowHandle = gridView1.LocateByValue("fl_def", true); //встать на строку "по умолчанию"
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if ((_FieldComment != String.Empty) && (gridView1.Columns[_FieldComment] != null))
            {
                object comment = gridView1.GetFocusedRowCellValue(_FieldComment);
                if (comment != null)
                    memoComment.Text = comment.ToString();
                else
                    memoComment.Text = "";
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            //Point pt = view.GridControl.PointToClient(Control.MousePosition);
            //DoRowDoubleClick(sender, pt);
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = view.CalcHitInfo(view.GridControl.PointToClient(Control.MousePosition));

            if (info.InRow || info.InRowCell) { btnEdit_Click(null, null); }
        }

        private void btnNew_Click(object sender, EventArgs e)
        { // добавление строки
            int id_new = -1;

            id_new = EditRecord(id_new);

            //switch (_SprVaiant)
            //{
            //    case cls_App.SprVariants.SprEnumerationSpr1:
            //        using (formEdit_SprEnumeration frm = new formEdit_SprEnumeration() { ParametersObject = new set_SprEnumeration() { Group = (int)cls_App.GroupSprEnumeration.Spr1 } })
            //        {
            //            frm.ShowDialog();
            //            if (frm.FlagOK) id_new = frm.Id;
            //        }
            //        break;
            //}

            if (id_new != -1)
            {
                //gridView1.RefreshData(); // не работает (наверное только на живых данных, типа XPCollection)
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

            if (id_edit != -1)
            {
                id_edit = EditRecord(id_edit);

                //switch (_SprVaiant)
                //{
                //    case cls_App.SprVariants.SprEnumerationSpr1:
                //        using (formEdit_SprEnumeration frm = new formEdit_SprEnumeration(id_edit) { ParametersObject = new set_SprEnumeration() { Group = (int)cls_App.GroupSprEnumeration.Spr1 } })
                //        {
                //            frm.ShowDialog();
                //            if (frm.FlagOK) flag_OK = true;
                //        }
                //        break;
                //}
            }

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

            if (_FlagSprEnumeration)
            {
                using (formEdit_SprEnumeration frm = new formEdit_SprEnumeration(id_reccord) { ParametersObject = new set_SprEnumeration() { Group = (int)_SprVaiant } })
                {
                    //frm.ParametersObject = (XPObject)_ClassInfo.CreateNewObject(BVVGlobal.oXpo.Get_Session());
                    //frm.ParametersObject.SetMemberValue("Group", (int)_SprVaiant);
                    frm.Set_SprVariant((int)_SprVaiant);
                    frm.Set_ImageIndexCollection(_ImageIndexCollection);
                    frm.ShowDialog();
                    if (frm.FlagOK) id_new = frm.Id; // id_reccord;
                }
            }
            else
            {
                switch (_SprVaiant)
                {
                    case cls_App.SprVariants.Spr_Org:
                        using (formEdit_Org frm = new formEdit_Org(id_reccord))
                        {
                            //frm.Set_SprVariant((int)_SprVaiant);

                            //frm.Set_ImageIndexCollection(_ImageIndexCollection);
                            frm.ShowDialog();
                            if (frm.FlagOK) id_new = frm.Id; // id_reccord;
                        }
                        break;
                }
            }

            //if (flag_OK)
            //{ // Нажали ОК - возможно нужно перечитать
            //    //gridView1.RefreshData(); // не работает (наверное только на живых данных, типа XPCollection)
            //    FillGrid();
            //    gridView1.FocusedRowHandle = gridView1.LocateByValue(_FieldId, id_edit);
            //}
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
                using (Session sess = BVVGlobal.oXpo.Get_Session())
                {
                    CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(sess, sess); //(null, sess);

                    XPObject o_old, o_clone;

                    if (_FlagSprEnumeration)
                    {
                        o_old = sess.GetObjectByKey(_Type, id_current, true) as XPObject;
                        o_clone = cloneHelper.Clone(o_old); // as set_SprEnumeration;
                        if (o_clone != null)
                        {
                            o_clone.SetMemberValue("g_id", Guid.NewGuid().ToString());
                            o_clone.SetMemberValue("fl_def", false);
                            o_clone.Save();
                            id_new = o_clone.Oid;
                        }
                    }
                    else
                    {
                        switch (_SprVaiant)
                        {
                            case cls_App.SprVariants.Spr_Org:
                                o_old = sess.GetObjectByKey<set_Org>(id_current, true);
                                o_clone = cloneHelper.Clone(o_old, false, true) as set_Org;
                                if (o_clone != null)
                                {
                                    ((set_Org)o_clone).g_id = Guid.NewGuid().ToString();
                                    //((set_Org)o_clone).Bank = ((set_Org)o_old).Bank;

                                    o_clone.Save();
                                    id_new = o_clone.Oid;
                                }
                                break;
                            //case cls_App.SprVariants.Spr_Vfo:
                            //    o_old = sess.GetObjectByKey(_Type, id_current, true) as XPObject;
                            //    o_clone = cloneHelper.Clone(o_old, false, true);
                            //    if (o_clone != null)
                            //    {
                            //        //((set_Vfo)o_clone).g_id = Guid.NewGuid().ToString();
                            //        o_clone.SetMemberValue("g_id", Guid.NewGuid().ToString());

                            //        o_clone.Save();
                            //        id_new = o_clone.Oid;
                            //    }
                            //    break;
                        }
                    }
                    sess.Disconnect();
                }

                if (EditRecord(id_new) != -1)
                {
                    FillGrid();
                    gridView1.FocusedRowHandle = gridView1.LocateByValue(_FieldId, id_new);
                }
                else
                { // откат
                    using (Session sess = BVVGlobal.oXpo.Get_Session())
                    {
                        if (id_new != -1)
                            ((XPObject)sess.GetObjectByKey(_Type, id_new, true)).Delete();

                        //switch (_SprVaiant)
                        //{
                        //    case cls_App.SprVariants.Spr_Dogovor:
                        //        sess.GetObjectByKey<set_Dogovor>(id_new, true).Delete();
                        //        break;
                        //    case cls_App.SprVariants.Spr_Org:
                        //        sess.GetObjectByKey<set_Org>(id_new, true).Delete();
                        //        break;
                        //}

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
                BVVGlobal._logger.Debug(String.Format("{0}\r\n                         {1}", ex.Message, ex.StackTrace));
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
            if (XtraMessageBox.Show("Действительно удалить текущую запись?", " Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) return;

            try
            {
                using (Session sess = BVVGlobal.oXpo.Get_Session())
                {
                    XPObject o_o;

                    switch (_SprVaiant)
                    {
                        case cls_App.SprVariants.Spr_Org:
                            //set_SprEnumeration o_o1 = sess.GetObjectByKey<set_SprEnumeration>(id_del, true);
                            o_o = sess.GetObjectByKey<set_Org>(id_del, true);
                            if (o_o != null)
                            {
                                //if (sess.FindObject<set_Dogovor>(new BinaryOperator("Org", (set_Org)o_o), false) == null)
                                //    flag_Del = true;
                                //else
                                //    flag_Del = false;
                                
                                if (flag_Del)
                                    o_o.Delete();
                                else
                                    XtraMessageBox.Show("По организации имеются данные."+Environment.NewLine+"Невозможно удалить организацию.","Сообщение",
                                        MessageBoxButtons.OK,MessageBoxIcon.Information);
                            }
                            break;
                        //case cls_App.SprVariants.Spr_Vfo:
                        //    o_o = sess.GetObjectByKey(_Type, id_del, true) as XPObject;
                        //    if (o_o != null)
                        //    {
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
                BVVGlobal._logger.Debug(String.Format("{0}\r\n                         {1}", ex.Message, ex.StackTrace));
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
        private void barBtnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { btnPrint_Click(null, null); }

        private void form_AnySpr_Activated(object sender, EventArgs e)
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

        private void chkFlAllDateRange_CheckedChanged(object sender, EventArgs e)
        {
            int id_edit = -1;

            if ((_FieldId != String.Empty) && (gridView1.Columns[_FieldId] != null))
            {
                object id_obj = gridView1.GetFocusedRowCellValue(_FieldId);
                if (id_obj != null) id_edit = (int)id_obj;
            }

            if (id_edit != -1)
            { // Нажали ОК - возможно нужно перечитать
                //gridView1.RefreshData(); // не работает (наверное только на живых данных, типа XPCollection)
                FillGrid();
                gridView1.FocusedRowHandle = gridView1.LocateByValue(_FieldId, id_edit);
            }
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.GridView View1 = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            //switch (_SprVaiant)
            //{
            //    case cls_App.SprVariants.Spr_Vfo:
            //        if (e.Column.FieldName == "CodeVfo")
            //        {
            //            //if ((bool)View1.GetRowCellValue(e.RowHandle, "Flag") == true) e.Appearance.BackColor = Color.LavenderBlush;
            //            //e.Appearance.BackColor = Color.LightYellow;
            //            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //        }
            //        break;
            //}
        }
    }
}