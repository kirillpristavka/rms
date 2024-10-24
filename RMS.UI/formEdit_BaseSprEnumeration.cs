using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace RMS.UI
{
    public partial class formEdit_BaseSprEnumeration : formEdit_BaseSpr
    {
        public formEdit_BaseSprEnumeration(int iId = -1)
        {
            InitializeComponent();

            id = iId;
            flagSave = false;
            _Type = null;
            _ImageIndexCollection = null;
            _delta_image_index = 0;
        }

        //public bool FlagOK { get { return _FlagOK; } }
        //public int Id { get { return _iId; } }

        private int _delta_image_index;     // разница в ImageIndex за вычетом зарезервированных начальных картинок

        //private bool _FlagOK;
        //private int _iId;
        private Type _Type;
        //private XPObject _ParametersObject;
        //private bool _FlagSprEnumeration;

        private cls_App.ReferenceBooks _SprVaiant;
        private DevExpress.Utils.ImageCollection _ImageIndexCollection;

        //public void Set_SprVariant(int spr_var) { _SprVaiant = (cls_App.SprVariants)spr_var; }
        //public void Set_ImageIndexCollection(DevExpress.Utils.ImageCollection image_collection) { _ImageIndexCollection = image_collection; }
        //public XPObject ParametersObject { get { return _ParametersObject; } set { _ParametersObject = value; } }


        private void formEdit_BaseSprEnumeration_Load(object sender, EventArgs e)
        {
            //Location = new Point(Cursor.Position.X - 100, Cursor.Position.Y - 100);
            Location = cls_App.GetStartPositionPoint(Cursor.Position.X - 50, Cursor.Position.Y - 50, Width, Height);

            DateTime l_date_beg = DateTime.MinValue;
            DateTime l_date_end = DateTime.MaxValue;

            //_Type = cls_BaseSpr.GetTypeSprVariants((int)_SprVaiant);
            _Type = BaseSpr.GetTypeSpr();
            _SprVaiant = (cls_App.ReferenceBooks)BaseSpr.SprVariant;
            _ImageIndexCollection = BaseSpr.ImageCollection;
            //_FlagSprEnumeration = BaseSpr.FlagSprEnumeration;


            if (BaseSpr.FlagSprEnumeration) // (cls_BaseSpr.IsSprEnumeration((int)_SprVaiant))
            {
                _delta_image_index = 3;        // вначале стоят "Все", "Выбранные" и "Исключая Выбранные"
            }

            if (!(BaseSpr.FlagSprDateRange))
            {
                groupControlPeriod.Visible = false;
                Height -= 75;
            }

            if (BaseSpr.SprShablonViewEnumeration != null)
            {
                if (BaseSpr.SprShablonViewEnumeration.FieldKod != null && BaseSpr.SprShablonViewEnumeration.FieldKod != String.Empty)
                {
                    txtCode.Visible = true;
                    lblCode.Visible = true;
                    lblCode.Text = BaseSpr.SprShablonViewEnumeration.FieldKodCaption;

                    if (BaseSpr.SprShablonViewEnumeration.FlKodIsInt32) // Если Код цифровой
                    {
                        txtCode.Properties.Appearance.Options.UseTextOptions = true;
                        txtCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        txtCode.Properties.Mask.EditMask = BaseSpr.SprShablonViewEnumeration.FieldKodMaska; // "9";
                        txtCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
                    }
                }
                if (BaseSpr.SprShablonViewEnumeration.FieldName != null && BaseSpr.SprShablonViewEnumeration.FieldName != String.Empty)
                {
                    txtName.Visible = true;
                    lblName.Visible = true;
                    lblName.Text = BaseSpr.SprShablonViewEnumeration.FieldNameCaption;
                }
                if (BaseSpr.SprShablonViewEnumeration.FieldSoder != null && BaseSpr.SprShablonViewEnumeration.FieldSoder != String.Empty)
                {
                    memoSoder.Visible = true;
                    lblSoder.Visible = true;
                    lblSoder.Text = BaseSpr.SprShablonViewEnumeration.FieldSoderCaption;
                }

                if (BaseSpr.SprShablonView.FieldDef != null && BaseSpr.SprShablonView.FieldDef != String.Empty) // (BaseSpr.SprShablonViewEnumeration.FieldFlDef != null && (BaseSpr.SprShablonViewEnumeration.FieldFlDef))
                {
                    chkDefault.Visible = true;
                }
            }

            if (_ImageIndexCollection != null)
            { // располагаем после определения переменной _delta_image_index
                lblImageIndex.Visible = true;
                imgImageIndex.Visible = true;

                imgImageIndex.Properties.SmallImages = _ImageIndexCollection;
                for (int i = _delta_image_index; i < _ImageIndexCollection.Images.Count; i++) // i от 2-х, первые 2 зарезервированы (все, выбранные)
                    imgImageIndex.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(i));

                imgImageIndex.SelectedIndex = 0;
            }

            memoSoder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            if (id != -1)
            {
                object l_Code; // = 0;
                string l_Name = string.Empty, l_Soder = string.Empty;
                bool l_fl_def = false;
                int i_ImageIndex = -1;

                try
                {
                    if (BaseSpr.SprShablonViewEnumeration != null)
                    {
                        if (BaseSpr.SprShablonViewEnumeration.FlKodIsInt32) l_Code = 0;
                        else l_Code = string.Empty;

                        using (Session sess = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer())
                        {
                            var o_o = sess.GetObjectByKey(_Type, id, true);

                            if (o_o != null)
                            {
                                if (BaseSpr.SprShablonViewEnumeration.FieldKod != null && BaseSpr.SprShablonViewEnumeration.FieldKod != String.Empty)
                                {
                                    if (BaseSpr.SprShablonViewEnumeration.FlKodIsInt32)
                                    {
                                        //l_Code = (int)o_o.GetMemberValue(BaseSpr.SprShablonViewEnumeration.FieldKod);
                                        l_Code = (int)o_o.GetType().InvokeMember(BaseSpr.SprShablonViewEnumeration.FieldKod, System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.GetProperty, null, o_o, null);
                                    }
                                    else
                                    {
                                        //l_Code = (string)o_o.GetMemberValue(BaseSpr.SprShablonViewEnumeration.FieldKod);
                                        l_Code = (string)o_o.GetType().InvokeMember(BaseSpr.SprShablonViewEnumeration.FieldKod, System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.GetProperty, null, o_o, null);
                                    }
                                }

                                if (BaseSpr.SprShablonViewEnumeration.FieldName != null && BaseSpr.SprShablonViewEnumeration.FieldName != String.Empty)
                                {
                                    //l_Name = (string)o_o.GetMemberValue(BaseSpr.SprShablonViewEnumeration.FieldName);
                                    l_Name = (string)o_o.GetType().InvokeMember(BaseSpr.SprShablonViewEnumeration.FieldName, System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.GetProperty, null, o_o, null);
                                }

                                if (BaseSpr.SprShablonViewEnumeration.FieldSoder != null && BaseSpr.SprShablonViewEnumeration.FieldSoder != String.Empty)
                                {
                                    //l_Soder = (string)o_o.GetMemberValue(BaseSpr.SprShablonViewEnumeration.FieldSoder);
                                    l_Soder = (string)o_o.GetType().InvokeMember(BaseSpr.SprShablonViewEnumeration.FieldSoder, System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.GetProperty, null, o_o, null);
                                }

                                if (BaseSpr.FlagSprDateRange)
                                {
                                    var dateBegin = (DateTime)o_o.GetType().InvokeMember("date_beg", System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.GetProperty, null, o_o, null);
                                    var dateEnd = (DateTime)o_o.GetType().InvokeMember("date_end", System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.GetProperty, null, o_o, null);

                                    if (dateBegin != null)
                                    {
                                        //l_date_beg = (DateTime)o_o.GetMemberValue("date_beg");
                                        l_date_beg = dateBegin;
                                    }

                                    if (dateEnd != null)
                                    {
                                        l_date_end = dateEnd;
                                    }
                                }

                                if (BaseSpr.SprShablonView.FieldDef != null && BaseSpr.SprShablonView.FieldDef != String.Empty)
                                {
                                    //l_fl_def = (bool)o_o.GetMemberValue(BaseSpr.SprShablonView.FieldDef);
                                    l_fl_def = (bool)o_o.GetType().InvokeMember(BaseSpr.SprShablonView.FieldDef, System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.GetProperty, null, o_o, null);
                                }

                                if (_ImageIndexCollection != null)
                                {
                                    //i_ImageIndex = (int)o_o.GetMemberValue(BaseSpr.FieldImageIndex);
                                    i_ImageIndex = (int)o_o.GetType().InvokeMember(BaseSpr.FieldImageIndex, System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.GetProperty, null, o_o, null);
                                }
                            }

                            txtCode.EditValue = l_Code;
                            txtName.Text = l_Name;
                            memoSoder.Text = l_Soder;
                            dateEditDateBegin.DateTime = l_date_beg;
                            dateEditDateEnd.DateTime = l_date_end;
                            chkDefault.EditValue = l_fl_def;
                            imgImageIndex.SelectedIndex = i_ImageIndex - _delta_image_index;

                            sess.Disconnect();
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
                dateEditDateBegin.DateTime = l_date_beg;
                dateEditDateEnd.DateTime = l_date_end;
                chkDefault.EditValue = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверка на отрицательный период
                if (dateEditDateEnd.DateTime.Date < dateEditDateBegin.DateTime.Date)
                {
                    XtraMessageBox.Show("Отрицательный период действия!", " Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dateEditDateEnd.Focus();
                    return;
                }

                using (Session sess = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer())
                {
                    XPObject o_o, o_tmp;
                    DateTime d1 = dateEditDateBegin.DateTime.Date;
                    DateTime d2 = dateEditDateEnd.DateTime.Date;

                    object l_Code;
                    string l_Name = txtName.Text;

                    if (id != -1)
                    {
                        o_o = (XPObject)sess.GetObjectByKey(_Type, id, true);

                        if (o_o != null)
                        {
                            if (BaseSpr.SprShablonView.ModifyUsers) o_o.SetMemberValue("User_update", DatabaseConnection.User);
                            if (BaseSpr.SprShablonView.ModifyDates) o_o.SetMemberValue("Date_update", DateTime.Now);
                        }
                    }
                    else
                    {
                        o_o = (XPObject)sess.GetClassInfo(_Type).CreateNewObject(sess);
                        if (o_o != null)
                        {
                            if (BaseSpr.SprShablonView.FieldGuid != null && BaseSpr.SprShablonView.FieldGuid != String.Empty)
                            {
                                if (!BaseSpr.SprShablonView.FieldGuid.Contains("Oid"))
                                {
                                    o_o.SetMemberValue(BaseSpr.SprShablonView.FieldGuid, Guid.NewGuid().ToString());
                                }
                            }
                            if (BaseSpr.SprShablonView.ModifyUsers) o_o.SetMemberValue("User_create", DatabaseConnection.User);
                            if (BaseSpr.SprShablonView.ModifyDates) o_o.SetMemberValue("Date_create", DateTime.Now);

                            //Group = (int)_SprVaiant

                            if (BaseSpr.FlagSprEnumeration) o_o.SetMemberValue("Group", BaseSpr.SprVariant); // _ParametersObject.GetMemberValue("Group"));
                            else
                            {
                                if (BaseSpr.SprShablonView.IsFormEnumeration)
                                {
                                    //o_o.SetMemberValue("Org", _ParametersObject.GetMemberValue("Org"));
                                    //o_o.SetMemberValue("Task", _ParametersObject.GetMemberValue("Task"));
                                    //o_o.SetMemberValue("User", _ParametersObject.GetMemberValue("User"));
                                    //o_o.SetMemberValue("Year", _ParametersObject.GetMemberValue("Year"));

                                    if (BaseSpr.SprShablonViewEnumeration.FieldGroupI1 != null && BaseSpr.SprShablonViewEnumeration.FieldGroupI1 != String.Empty) // "Group"
                                        o_o.SetMemberValue(BaseSpr.SprShablonViewEnumeration.FieldGroupI1, BaseSpr.SprVariant);

                                    if (BaseSpr.SprShablonViewEnumeration.FieldGroupI2 != null && BaseSpr.SprShablonViewEnumeration.FieldGroupI2 != String.Empty) // "Year"
                                        o_o.SetMemberValue(BaseSpr.SprShablonViewEnumeration.FieldGroupI2, BVVGlobal.oApp.WorkDate.Year);

                                    if (BaseSpr.SprShablonViewEnumeration.FieldGroupS1 != null && BaseSpr.SprShablonViewEnumeration.FieldGroupS1 != String.Empty) // "User"
                                        o_o.SetMemberValue(BaseSpr.SprShablonViewEnumeration.FieldGroupS1, BVVGlobal.oApp.User);


                                }
                            }
                        }
                    }

                    if (o_o != null)
                    {
                        if (BaseSpr.SprShablonViewEnumeration != null)
                        {
                            CriteriaOperator criteria_daterange = null;

                            if (BaseSpr.FlagSprDateRange) criteria_daterange = cls_BaseSpr.GetCriteriaDateCheckPeriod(d1, d2);

                            if (BaseSpr.SprShablonViewEnumeration.FieldKod != null && BaseSpr.SprShablonViewEnumeration.FieldKod != String.Empty)
                            {
                                if (txtCode.Text == String.Empty)
                                {
                                    XtraMessageBox.Show("Код не определен!", " Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtCode.Focus();
                                    txtCode.Select(0, txtCode.Text.Length);
                                    return;
                                }

                                if (BaseSpr.SprShablonViewEnumeration.FlKodIsInt32)
                                    l_Code = Convert.ToInt32(txtCode.Text);
                                else l_Code = txtCode.Text;

                                if (sess.FindObject(_Type, new GroupOperator(GroupOperatorType.And, new CriteriaOperator[] { CriteriaOperator.Parse("(" + BaseSpr.SprShablonViewEnumeration.FieldKod + "=? and Oid!=?)", l_Code, id), criteria_daterange }), false) != null)
                                {
                                    if (BaseSpr.FlagSprDateRange)
                                        XtraMessageBox.Show("Новый временной диапазон данного кода пересекается с предыдущим диапазоном!", " Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    else XtraMessageBox.Show("Данный код уже существует !", " Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    txtCode.Focus();
                                    txtCode.Select(0, txtCode.Text.Length);

                                    return;
                                }

                                o_o.SetMemberValue(BaseSpr.SprShablonViewEnumeration.FieldKod, l_Code);
                            }

                            if (BaseSpr.SprShablonViewEnumeration.FieldName != null && BaseSpr.SprShablonViewEnumeration.FieldName != String.Empty)
                            {
                                if (sess.FindObject(_Type, new GroupOperator(GroupOperatorType.And, new CriteriaOperator[] { CriteriaOperator.Parse("(" + BaseSpr.SprShablonViewEnumeration.FieldName + "=? and Oid!=?)", l_Name, id), criteria_daterange }), false) != null)
                                {
                                    if (BaseSpr.FlagSprDateRange)
                                        XtraMessageBox.Show("Новый временной диапазон данного наименования пересекается с предыдущим диапазоном!", " Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    else XtraMessageBox.Show("Данное наименование уже существует !", " Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    txtName.Focus();
                                    txtName.Select(0, txtName.Text.Length);

                                    return;
                                }

                                o_o.SetMemberValue(BaseSpr.SprShablonViewEnumeration.FieldName, l_Name);
                            }

                            if (BaseSpr.SprShablonViewEnumeration.FieldSoder != null && BaseSpr.SprShablonViewEnumeration.FieldSoder != String.Empty)
                                o_o.SetMemberValue(BaseSpr.SprShablonViewEnumeration.FieldSoder, memoSoder.Text);

                            if (_ImageIndexCollection != null)
                                o_o.SetMemberValue(BaseSpr.FieldImageIndex, imgImageIndex.SelectedIndex + _delta_image_index);

                            if (BaseSpr.FlagSprDateRange)
                            {
                                o_o.SetMemberValue("date_beg", d1);
                                o_o.SetMemberValue("date_end", d2);
                            }

                            if (BaseSpr.SprShablonView.FieldDef != null && BaseSpr.SprShablonView.FieldDef != String.Empty) // (BaseSpr.SprShablonViewEnumeration.FieldFlDef != null && (BaseSpr.SprShablonViewEnumeration.FieldFlDef))
                            {
                                bool l_fl_def = (bool)chkDefault.EditValue;
                                string field_def = BaseSpr.SprShablonView.FieldDef;
                                o_o.SetMemberValue(field_def, l_fl_def);

                                if (l_fl_def) // если меняем флаг по умолчанию, с предыдущего нужно снять.
                                {
                                    string l_group = string.Empty;

                                    if (BaseSpr.FlagSprEnumeration) l_group = "Group";
                                    else
                                    {
                                        if (BaseSpr.SprShablonView.IsFormEnumeration)
                                        {
                                            if (BaseSpr.SprShablonViewEnumeration.FieldGroupI1 != null && BaseSpr.SprShablonViewEnumeration.FieldGroupI1 != String.Empty) // "Group"
                                                l_group = BaseSpr.SprShablonViewEnumeration.FieldGroupI1;
                                        }
                                    }

                                    CriteriaOperator criteria_group = (l_group == String.Empty) ? null : new BinaryOperator(l_group, BaseSpr.SprVariant);
                                    o_tmp = (XPObject)sess.FindObject(_Type, new GroupOperator(GroupOperatorType.And, criteria_group, new BinaryOperator(field_def, true), new BinaryOperator("Oid", id, BinaryOperatorType.NotEqual)), false);
                                    if (o_tmp != null)
                                    {
                                        o_tmp.SetMemberValue(field_def, false);
                                        o_tmp.Save();
                                    }
                                }
                            }
                        }
                        else
                        {
                            // По идее сюда попадать и не должен !

                            //switch (_SprVaiant)
                            //{
                            //    case cls_App.SprVariants.Spr_Vfo:

                            //        break;
                            //    default:

                            //        break;
                            //}
                        }

                        o_o.Save();
                        id = o_o.Oid;
                    }

                    sess.Disconnect();
                }

                flagSave = true;
                //if (!fl_for_copy) _FlagOK = true;
                Close();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }
    }
}

