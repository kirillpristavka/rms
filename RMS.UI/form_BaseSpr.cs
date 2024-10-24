using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle;
using RMS.UI.OutProject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMS.UI
{
    public partial class form_BaseSpr : XtraForm
    {
        private Session _session => DatabaseConnection.GetWorkSession();

        public form_BaseSpr(int iSprVariant = -1, int iId = -1, int iVerSpr = 1, CriteriaOperator criteria = null, SortingCollection sort = null,
            DevExpress.Utils.ImageCollection image_collection = null, string image_field = "", object oPar1 = null, object oPar2 = null, object oPar3 = null)
        {
            InitializeComponent();

            _iId = iId;
            _FlagOK = false;
            _fl_InitForm = false;

            if (iSprVariant != -1) _SprVaiant = (cls_App.ReferenceBooks)iSprVariant;
            _Type = null; // type;
            _Criteria = criteria;
            _Sorting = sort;
            _SelectFields = string.Empty; // select_fields;

            _ImageIndexCollection = image_collection;
            _FieldImageIndex = image_field;

            _FieldComment = string.Empty;
            _FieldId = string.Empty; // "Oid";
            _FlagSprEnumeration = false;
            _FlagSprDateRange = false;

            // + некие переменные параметров (Нужны для передачи в форму редактирования)
            _oPar1 = oPar1;
            _oPar2 = oPar2;
            _oPar3 = oPar3;
            //bool_VariantValue1 = false;
            //bool_VariantValue2 = false;
            //bool_VariantValue3 = false;
            //int_VariantValue1 = -1;
            //int_VariantValue2 = -1;
            //int_VariantValue3 = -1;
            //str_VariantValue1 = "";
            //str_VariantValue2 = "";
            //str_VariantValue3 = "";
            // -

            _Location = new System.Drawing.Point(-999, -999);

            _ImageIndexCollection = null;
            _FieldImageIndex = string.Empty;
        }

        private bool _FlagOK;
        private int _iId;
        private List<int> _oids = new List<int>();
        //private cls_App.SprVariants _SprVariant;

        private bool _fl_InitForm;

        //private XPClassInfo _ClassInfo;
        private Type _Type;
        private CriteriaOperator _Criteria;
        //private SortProperty _Sorting;
        private SortingCollection _Sorting;
        private string _SelectFields;
        private string _FieldId;
        private string _FieldComment;
        private bool _FlagSprEnumeration;
        private bool _FlagSprDateRange;

        private System.Drawing.Point _Location;

        public bool FlagOK { get { return _FlagOK; } }
        public int Id { get { return _iId; } }
        public List<int> Oids { get { return _oids; } }

        // + некие переменные параметров (Нужны для передачи в форму редактирования)
        public object _oPar1 { get; set; }
        public object _oPar2 { get; set; }
        public object _oPar3 { get; set; }
        //public bool bool_VariantValue1 { get; set; }
        //public bool bool_VariantValue2 { get; set; }
        //public bool bool_VariantValue3 { get; set; }
        //public int int_VariantValue1 { get; set; }
        //public int int_VariantValue2 { get; set; }
        //public int int_VariantValue3 { get; set; }
        //public string str_VariantValue1 { get; set; }
        //public string str_VariantValue2 { get; set; }
        //public string str_VariantValue3 { get; set; }
        // -

        private cls_App.ReferenceBooks _SprVaiant;
        private DevExpress.Utils.ImageCollection _ImageIndexCollection;
        private string _FieldImageIndex;

        private cls_BaseSpr _BaseSpr;

        public void Set_SprVariant(int spr_var) { _SprVaiant = (cls_App.ReferenceBooks)spr_var; }
        public void Set_FieldId(string field) { _FieldId = field; }
        public void Set_FieldComment(string field) { _FieldComment = field; }
        public void Set_ImageIndexCollection(DevExpress.Utils.ImageCollection image_collection, string image_field = "ImageIndex") { _ImageIndexCollection = image_collection; _FieldImageIndex = image_field; }
        public void Set_Type(Type type) { _Type = type; }
        public void Set_SelectFields(string select_fields) { _SelectFields = select_fields; }
        public void Set_Criteria(CriteriaOperator criteria) { _Criteria = criteria; }
        public void Set_Sorting(SortingCollection sort) { _Sorting = sort; }

        public void Set_Size(int _width, int _height = -1)
        {
            Width = cls_App.GetFormWidth(_width);
            if (_height != -1) Height = cls_App.GetFormHeight(_height);
        }
        public void Set_Location(Point location) { _Location = location; }

        private bool isAdministrator = false;
        private async System.Threading.Tasks.Task SetAccessRights()
        {
            try
            {
                using (var uof = new UnitOfWork())
                {
                    var user = await uof.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid);
                    if (user != null)
                    {
                        isAdministrator = user.flagAdministrator;
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        public void SetMultiSelectGridView()
        {
            gridView.OptionsSelection.MultiSelect = true;
        }

        private async void form_BaseSpr_Load(object sender, EventArgs e)
        {
            await SetAccessRights();

            _fl_InitForm = await InitForm();

            _BaseSpr = new cls_BaseSpr((int)_SprVaiant);

            Set_Size(_BaseSpr.GetSizeW(), _BaseSpr.GetSizeH());

            if (_Location.X == -999) Location = cls_App.GetStartPositionPoint(Cursor.Position.X - 100, Cursor.Position.Y - 100, Width, Height);
            else Location = _Location;

            this.Text = _BaseSpr.SprShablonView.SprText;

            if (_BaseSpr.SprShablonView.EditButtons == false)
            {
                btnNew.Enabled = false;
                btnEdit.Enabled = false;
                btnCopy.Enabled = false;
                btnDelete.Enabled = false;

                barBtnNew.Enabled = false;
                barBtnEdit.Enabled = false;
                barBtnCopy.Enabled = false;
                barBtnDelete.Enabled = false;
            }

            _Type = _BaseSpr.GetTypeSpr();
            if (_FieldId == String.Empty) _FieldId = _BaseSpr.FieldId;
            if (_FieldComment == String.Empty) _FieldComment = _BaseSpr.FieldComment;

            _FlagSprDateRange = _BaseSpr.FlagSprDateRange;
            _FlagSprEnumeration = _BaseSpr.FlagSprEnumeration;


            if (_SelectFields == String.Empty) _SelectFields = _BaseSpr.GetFieldsString();

            if (ReferenceEquals(_Sorting, null)) _Sorting = _BaseSpr.GetSorting();

            if (ReferenceEquals(_Criteria, null)) _Criteria = _BaseSpr.GetCriteria();

            if (ReferenceEquals(_ImageIndexCollection, null))
            {
                _ImageIndexCollection = _BaseSpr.ImageCollection;
                _FieldImageIndex = _BaseSpr.FieldImageIndex;
            }
            else // если коллекция определена (вручную), а Поле ImageIndex нет - ставим по умолчанию
            {
                if (_FieldImageIndex == String.Empty)
                {
                    _FieldImageIndex = "ImageIndex";
                }
            }

            chkFlAllDateRange.Visible = _FlagSprDateRange;

            FillGrid();

            if (_iId != -1)
            {
                gridView.FocusedRowHandle = gridView.LocateByValue(_FieldId, _iId);
            }
            else
            {
                gridView1_FocusedRowChanged(null, null);
            }

            btnDelete.Enabled = isAdministrator;
        }

        private async Task<bool> InitForm()
        {
            try
            {
                chkbtnFind.Checked = Convert.ToBoolean(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_BaseSpr_chkbtnFind", "false"));
                chkbtnFilter.Checked = Convert.ToBoolean(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_BaseSpr_chkbtnFilter", "false"));
                chkbtnComment.Checked = Convert.ToBoolean(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_BaseSpr_chkbtnComment", "false"));
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }

            return true;
        }

        private async void chkbtnFind_CheckedChanged(object sender, EventArgs e)
        {
            await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_BaseSpr_chkbtnFind", chkbtnFind.Checked.ToString(), true, true);

            if (chkbtnFind.Checked)
            {
                gridView.ShowFindPanel();
            }
            else
            {
                gridView.HideFindPanel();
            }
        }

        private async void chkbtnFilter_CheckedChanged(object sender, EventArgs e)
        {
            await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_BaseSpr_chkbtnFilter", chkbtnFilter.Checked.ToString(), true, true);
            gridView.OptionsView.ShowAutoFilterRow = chkbtnFilter.Checked;
        }

        private async void chkbtnComment_CheckedChanged(object sender, EventArgs e)
        {
            await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_BaseSpr_chkbtnComment", chkbtnComment.Checked.ToString(), true, true);

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
            if ((gridView.Columns[_FieldId] != null) && (gridView.GetFocusedRowCellValue(_FieldId) != null)) // gridView1.FocusedValue != null))
            {
                var selectedRows = gridView.GetSelectedRows();
                foreach (var row in selectedRows)
                {
                    if (int.TryParse(gridView.GetRowCellValue(row, _FieldId)?.ToString(), out int result))
                    {
                        _oids.Add(result);
                    }
                }

                _iId = (int)gridView.GetFocusedRowCellValue(_FieldId);
                _FlagOK = true;
                Close();
            }
            else
            {
                XtraMessageBox.Show("Не возможно выбрать значение !", " Информация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FillGrid()
        {
            try
            {
                CriteriaOperator criteria_final = _Criteria;

                // при активной галочке "все временные диапазоны" (у таблиц где есть период дат)
                if ((_FlagSprDateRange) && (bool)chkFlAllDateRange.EditValue == false)
                { // условие на вхождение во временной период
                    criteria_final = new GroupOperator(GroupOperatorType.And, new CriteriaOperator[] { _Criteria, cls_BaseSpr.GetCriteriaDateRange() });
                }

                XPView xpView1 = new XPView(_session, _Type, _SelectFields, criteria_final);

                if (xpView1 != null)
                {
                    xpView1.Sorting = _Sorting;

                    gridControl1.DataSource = xpView1;

                    _BaseSpr.FillGridView(gridView);
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
            gridView.Focus();
            gridView1_FocusedRowChanged(null, null); // обновить Комментарий снизу
            // Зачем это при открытии ?
            //gridView1.FocusedRowHandle = gridView1.LocateByValue("fl_def", true); //встать на строку "по умолчанию"
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if ((_FieldComment != String.Empty) && (gridView.Columns[_FieldComment] != null))
            {
                object comment = gridView.GetFocusedRowCellValue(_FieldComment);
                if (comment != null)
                {
                    memoComment.Text = comment.ToString();
                }
                else
                {
                    memoComment.Text = string.Empty;
                }
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = view.CalcHitInfo(view.GridControl.PointToClient(MousePosition));

            if (info.InRow || info.InRowCell)
            {
                //if (btnEdit.Enabled)
                //{
                //    btnEdit_Click(null, null);
                //}
                btnOK_Click(null, null);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            int id_new = -1;

            id_new = EditRecord(id_new);

            if (id_new != -1)
            {
                FillGrid();
                gridView.FocusedRowHandle = gridView.LocateByValue(_FieldId, id_new);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id_edit = -1;

            if ((_FieldId != String.Empty) && (gridView.Columns[_FieldId] != null))
            {
                object id_obj = gridView.GetFocusedRowCellValue(_FieldId);
                if (id_obj != null)
                {
                    id_edit = (int)id_obj;
                }
            }

            if (id_edit != -1) id_edit = EditRecord(id_edit);

            if (id_edit != -1)
            {
                FillGrid();
                gridView.FocusedRowHandle = gridView.LocateByValue(_FieldId, id_edit);
            }
        }

        private int EditRecord(int id_reccord)
        {
            int id_new = -1;

            formEdit_BaseSpr frm = _BaseSpr.GetFormEdit(id_reccord, _oPar1, _oPar2, _oPar3);

            if (frm != null)
            {
                frm.ShowDialog();
                if (frm.FlagSave) id_new = frm.Id;
            }

            return id_new;
        }

        private async void btnCopy_Click(object sender, EventArgs e)
        {
            var currentObjOid = -1;
            var cloneObjOid = -1;

            if (!string.IsNullOrWhiteSpace(_FieldId) && (gridView.Columns[_FieldId] != null))
            {
                if (int.TryParse(gridView.GetFocusedRowCellValue(_FieldId)?.ToString(), out int objOid))
                {
                    currentObjOid = objOid;
                }
            }

            if (currentObjOid == -1)
            {
                return;
            }

            if (_Type.Name == typeof(Core.Model.Calculator.CalculatorTaxSystem).Name 
                || _Type.Name == typeof(Core.Model.Calculator.CalculatorIndicator).Name)
            {
                try
                {
                    using (var uof = new UnitOfWork())
                    {
                        var cloneHelper = new CloneIXPSimpleObjectHelper(uof, uof);

                        var currentObj = await uof.GetObjectByKeyAsync(_Type, currentObjOid, true) as XPObject;
                        var cloneObj = cloneHelper.Clone(currentObj, false, true);
                        if (cloneObj != null)
                        {
                            var fieldGuid = default(string);
                            var fieldDefault = default(string);
                            if (_FlagSprEnumeration)
                            {
                                fieldGuid = "g_id";
                                fieldDefault = "fl_def";
                            }
                            else
                            {
                                if (_BaseSpr.SprShablonView.FieldGuid != null && !string.IsNullOrWhiteSpace(_BaseSpr.SprShablonView.FieldGuid))
                                {
                                    fieldGuid = _BaseSpr.SprShablonView.FieldGuid;
                                }

                                if (_BaseSpr.SprShablonView.FieldDef != null && !string.IsNullOrWhiteSpace(_BaseSpr.SprShablonView.FieldDef))
                                {
                                    fieldDefault = _BaseSpr.SprShablonView.FieldDef;
                                }
                            }

                            if (!string.IsNullOrWhiteSpace(fieldGuid))
                            {
                                var property = cloneObj.GetType().GetProperty(fieldGuid);
                                if (property != null && property.PropertyType == typeof(Guid))
                                {

                                    cloneObj.SetMemberValue(fieldGuid, Guid.NewGuid().ToString());
                                }
                            }

                            if (!string.IsNullOrWhiteSpace(fieldDefault))
                            {
                                var property = cloneObj.GetType().GetProperty(fieldGuid);
                                if (property != null)
                                {
                                    cloneObj.SetMemberValue(fieldDefault, false);
                                }
                            }

                            //if (_BaseSpr.SprShablonView.ModifyUsers)
                            //{
                            //    cloneObj.SetMemberValue("User_create", DatabaseConnection.User);
                            //    cloneObj.SetMemberValue("User_update", null);
                            //}
                            //if (_BaseSpr.SprShablonView.ModifyDates)
                            //{
                            //    cloneObj.SetMemberValue("Date_create", DateTime.Now);
                            //    cloneObj.SetMemberValue("Date_update", DateTime.MinValue);
                            //}

                            cloneObj.Save();
                            await uof.CommitTransactionAsync();
                        }

                        cloneObjOid = cloneObj.Oid;
                    }

                    if (EditRecord(cloneObjOid) != -1)
                    {
                        FillGrid();
                        gridView.FocusedRowHandle = gridView.LocateByValue(_FieldId, cloneObjOid);
                    }
                    else
                    {
                        using (var uof = new UnitOfWork())
                        {
                            if (cloneObjOid != -1)
                            {
                                var obj = await uof.GetObjectByKeyAsync(_Type, cloneObjOid, true);
                                if (obj is XPObject xpObj)
                                {
                                    xpObj.Delete();
                                    await uof.CommitTransactionAsync();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }
            }
            else
            {
                PulsLibrary.Extensions.DevForm.DevXtraMessageBox.ShowXtraMessageBox("Данный объект не может быть клонирован. Обратитесь к разработчику");
            }

            gridView.Focus();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            int currentDeleteObj = -1;
            bool isDelete = false;

            if (!string.IsNullOrWhiteSpace(_FieldId) && gridView.Columns[_FieldId] != null)
            {
                object objId = gridView.GetFocusedRowCellValue(_FieldId);
                if (objId != null && int.TryParse(objId?.ToString(), out int result))
                {
                    currentDeleteObj = result;
                }
            }

            if (currentDeleteObj == -1)
            {
                return;
            }

            if (XtraMessageBox.Show("Действительно удалить текущую запись?", " Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            try
            {
                var currentObj = _session.GetObjectByKey(_Type, currentDeleteObj, true);

                if (currentObj != null)
                {
                    isDelete = await _BaseSpr.CheckForDeleteAsync(_session, currentDeleteObj, true);

                    if (isDelete)
                    {
                        _session.Delete(currentObj);

                        using (var uof = new UnitOfWork())
                        {
                            var user = await uof.GetObjectByKeyAsync<User>(DatabaseConnection.User?.Oid);
                            if (user != null)
                            {
                                var chronicleEvents = new ChronicleEvents(uof)
                                {
                                    Act = Act.OBJECT_DELETED,
                                    Date = DateTime.Now,
                                    Name = Act.OBJECT_DELETED.GetEnumDescription(),
                                    Description = $"Пользователь [{user}] произвел удаление объекта из общего словаря.{Environment.NewLine}" +
                                    $"Тип объекта: {_Type.Name}.{Environment.NewLine}" +
                                    $"Уникальный идентификатор: {currentDeleteObj}.",
                                    User = await uof.GetObjectByKeyAsync<User>(user.Oid)
                                };
                                chronicleEvents.Save();
                            }
                            await uof.CommitChangesAsync().ConfigureAwait(false);
                        }
                    }
                }

                if (isDelete)
                {
                    int l_next = gridView.GetNextVisibleRow(gridView.FocusedRowHandle);
                    int l_prev = gridView.GetPrevVisibleRow(gridView.FocusedRowHandle);
                    int l_id_to_focus = -1;

                    if (l_next >= 0)
                    {
                        l_id_to_focus = (int)gridView.GetRowCellValue(l_next, _FieldId);
                    }
                    else
                    {
                        if (l_prev >= 0)
                        {
                            l_id_to_focus = (int)gridView.GetRowCellValue(l_prev, _FieldId);
                        }
                    }

                    FillGrid();

                    if (l_id_to_focus >= 0)
                    {
                        gridView.FocusedRowHandle = gridView.LocateByValue(_FieldId, l_id_to_focus);
                    }

                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }

            gridView.Focus();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                //if (btnEdit.Enabled)
                //{
                //    btnEdit_Click(null, null);
                //}
                btnOK_Click(null, null);
            }
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
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

        private void form_BaseSpr_Activated(object sender, EventArgs e)
        { // потому что при form_Load не хочет далеко бежать
            if (_iId != -1)
            {
                if (gridView.FocusedRowHandle == gridView.LocateByValue(_FieldId, _iId))
                {
                    gridView.FocusedRowHandle = 0;
                    gridView.FocusedRowHandle = gridView.LocateByValue(_FieldId, _iId);
                }
            }
        }

        private void chkFlAllDateRange_CheckedChanged(object sender, EventArgs e)
        {
            int id_edit = -1;

            if ((_FieldId != String.Empty) && (gridView.Columns[_FieldId] != null))
            {
                object id_obj = gridView.GetFocusedRowCellValue(_FieldId);
                if (id_obj != null) id_edit = (int)id_obj;
            }

            if (id_edit != -1)
            {
                FillGrid();
                gridView.FocusedRowHandle = gridView.LocateByValue(_FieldId, id_edit);
            }
        }
    }
}