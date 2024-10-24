using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.Data.Filtering;




namespace PulsPlusSpace
{
    public partial class formEdit_SprEnumeration2 : formEdit_BaseSpr //DevExpress.XtraEditors.XtraForm
    {
        public formEdit_SprEnumeration2(int iId = -1)
        {
            InitializeComponent();

            _iId = iId;
            _FlagOK = false;
            _Type = null;
            _FlagSprEnumeration = false;
            _ParametersObject = null;
            _ImageIndexCollection = null;
            _delta_image_index = 0;
        }

        //public bool FlagOK { get { return _FlagOK; } }
        //public int Id { get { return _iId; } }

        private int _delta_image_index;     // разница в ImageIndex за вычетом зарезервированных начальных картинок

        //private bool _FlagOK;
        //private int _iId;
        private Type _Type;
        private XPObject _ParametersObject;
        private bool _FlagSprEnumeration;

        private cls_App.SprVariants _SprVaiant;
        private DevExpress.Utils.ImageCollection _ImageIndexCollection;

        public void Set_SprVariant(int spr_var) { _SprVaiant = (cls_App.SprVariants)spr_var; }
        public void Set_ImageIndexCollection(DevExpress.Utils.ImageCollection image_collection) { _ImageIndexCollection = image_collection; }
        public XPObject ParametersObject { get { return _ParametersObject; } set { _ParametersObject = value; } }


        private void formEdit_SprEnumeration_Load(object sender, EventArgs e)
        {
            //Location = new Point(Cursor.Position.X - 100, Cursor.Position.Y - 100);
            Location = cls_App.GetStartPositionPoint(Cursor.Position.X - 50, Cursor.Position.Y - 50, Width, Height);

            DateTime l_date_beg = DateTime.MinValue;
            DateTime l_date_end = DateTime.MaxValue;

            _Type = cls_BaseSpr.GetTypeSprVariants((int)_SprVaiant);

            if (cls_BaseSpr.IsSprEnumeration((int)_SprVaiant))
            {
                _FlagSprEnumeration = true;    // set_SprEnumeration

                groupControlPeriod.Visible = false;
                Height -= 75;
                _delta_image_index = 3;        // вначале стоят "Все", "Выбранные" и "Исключая Выбранные"
            }
            //else
            //{
                switch (_SprVaiant)
                {

                    //case cls_App.SprVariants.Spr_TypeKontragent:
                    //    groupControlPeriod.Visible = false;
                    //    Height -= 75;
                    //    break;

                    //case cls_App.SprVariants.Spr_Kosgu:
                    //    chkDefault.Visible = false;
                    //    //lblName.Text = "Код: ";
                    //    //lblFullName.Text = "Наименование: ";
                    //    break;
                    //case cls_App.SprVariants.Spr_Vfo:
                    //    chkDefault.Visible = false;
                    //    txtCode.Visible = true;
                    //    lblName2.Visible = true;
                    //    txtName.Left += 100;
                    //    txtName.Width -= 100;
                    //    lblName.Text = "Код: ";
                    //    lblName2.Text = "Аббревиатура: ";
                    //    lblFullName.Text = "Наименование: ";

                    //    txtCode.Properties.Appearance.Options.UseTextOptions = true;
                    //    txtCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    //    txtCode.Properties.Mask.EditMask = "9";
                    //    txtCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;

                    //    break;
                    //case cls_App.SprVariants.Spr_EnumPrizPodp:
                    //    txtCode.Visible = true;
                    //    lblName2.Visible = true;
                    //    txtName.Left += 100;
                    //    txtName.Width -= 100;
                    //    lblName.Text = "Код: ";
                    //    lblName2.Text = "Наименование: ";
                    //    lblFullName.Text = "Содержание: ";

                    //    txtCode.Properties.Appearance.Options.UseTextOptions = true;
                    //    txtCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    //    txtCode.Properties.Mask.EditMask = "9";
                    //    txtCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;

                    //    break;
                }
            //}

            if (_ImageIndexCollection != null)
            { // располагаем после определения переменной _delta_image_index
                lblImageIndex.Visible = true;
                imgImageIndex.Visible = true;

                imgImageIndex.Properties.SmallImages = _ImageIndexCollection;
                for (int i = _delta_image_index; i < _ImageIndexCollection.Images.Count; i++) // i от 2-х, первые 2 зарезервированы (все, выбранные)
                    imgImageIndex.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(i));

                imgImageIndex.SelectedIndex = 0;
            }

            memoFullName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            if (_iId != -1)
            {
                object l_Code = 0;
                string l_Name = "", l_FullName = "";
                bool l_fl_def = false;
                int i_ImageIndex = -1;

                try
                {
                    using (Session sess = BVVGlobal.oXpo.Get_Session())
                    {
                        XPObject o_o;

                        if (_FlagSprEnumeration)
                        {
                            o_o = (XPObject)sess.GetObjectByKey(_Type, _iId, true);
                            if (o_o != null)
                            {
                                l_Code = (int)o_o.GetMemberValue("Kod");
                                l_Name = (string)o_o.GetMemberValue("Name");
                                l_FullName = (string)o_o.GetMemberValue("FullName");
                                l_date_beg = (DateTime)o_o.GetMemberValue("date_beg");
                                l_date_end = (DateTime)o_o.GetMemberValue("date_end");
                                l_fl_def = (bool)o_o.GetMemberValue("fl_def");
                                i_ImageIndex = (int)o_o.GetMemberValue("ImageIndex");
                            }
                        }
                        else
                        {
                            switch (_SprVaiant)
                            {
                                //case cls_App.SprVariants.Spr_Vr:
                                //    o_o = sess.GetObjectByKey<set_Vr>(_iId);
                                //    if (o_o != null)
                                //    {
                                //        l_Name = ((set_Vr)o_o).kod;
                                //        l_FullName = ((set_Vr)o_o).name;
                                //    }
                                //    break;
                                //case cls_App.SprVariants.Spr_Vfo:
                                //    o_o = (XPObject)sess.GetObjectByKey(_Type, _iId, true);
                                //    if (o_o != null)
                                //    {
                                //        l_Code = (int)o_o.GetMemberValue("CodeVfo");
                                //        l_Name = (string)o_o.GetMemberValue("Name");
                                //        l_FullName = (string)o_o.GetMemberValue("FullName");
                                        
                                //        if (o_o.GetMemberValue("date_beg") != null)
                                //            l_date_beg = (DateTime)o_o.GetMemberValue("date_beg");
                                //        if (o_o.GetMemberValue("date_end") != null)
                                //            l_date_end = (DateTime)o_o.GetMemberValue("date_end");

                                //        //l_fl_def = (bool)o_o.GetMemberValue("fl_def");
                                //        //i_ImageIndex = (int)o_o.GetMemberValue("ImageIndex");
                                //    }

                                //    //DateTime d = (DateTime)tmp_o;
                                //    break;
                            }
                        }

                        txtCode.EditValue = l_Code;
                        txtName.Text = l_Name;
                        memoFullName.Text = l_FullName;
                        dateEditDateBegin.DateTime = l_date_beg;
                        dateEditDateEnd.DateTime = l_date_end;
                        chkDefault.EditValue = l_fl_def;
                        imgImageIndex.SelectedIndex = i_ImageIndex - _delta_image_index;

                        sess.Disconnect();
                    }
                }
                catch (Exception ex)
                {
                    BVVGlobal._logger.Debug(String.Format("{0}\r\n                         {1}", ex.Message, ex.StackTrace));
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

                //bool fl_for_copy = false;
                using (Session sess = BVVGlobal.oXpo.Get_Session())
                {
                    XPObject o_o, o_tmp;
                    DateTime d1 = dateEditDateBegin.DateTime.Date;
                    DateTime d2 = dateEditDateEnd.DateTime.Date;

                    int l_code = Convert.ToInt32(txtCode.Text);

                    if (_FlagSprEnumeration)
                    {
                        if (_iId != -1)
                            o_o = (XPObject)sess.GetObjectByKey(_Type, _iId, true);
                        else
                        {
                            o_o = (XPObject)sess.GetClassInfo(_Type).CreateNewObject(sess);

                            //Group = (int)_SprVaiant

                            o_o.SetMemberValue("Group", _ParametersObject.GetMemberValue("Group"));
                            o_o.SetMemberValue("Org", _ParametersObject.GetMemberValue("Org"));
                            o_o.SetMemberValue("Task", _ParametersObject.GetMemberValue("Task"));
                            o_o.SetMemberValue("User", _ParametersObject.GetMemberValue("User"));
                            o_o.SetMemberValue("Year", _ParametersObject.GetMemberValue("Year"));
                        }

                        if (o_o != null)
                        {
                            o_o.SetMemberValue("Kod", l_code);
                            o_o.SetMemberValue("Name", txtName.Text);
                            o_o.SetMemberValue("FullName", memoFullName.Text);
                            o_o.SetMemberValue("date_beg", dateEditDateBegin.DateTime.Date);
                            o_o.SetMemberValue("date_end", dateEditDateEnd.DateTime.Date);
                            o_o.SetMemberValue("fl_def", (bool)chkDefault.EditValue);
                            o_o.SetMemberValue("ImageIndex", imgImageIndex.SelectedIndex + _delta_image_index);
                            if ((bool)o_o.GetMemberValue("fl_def") == true)
                            { // если меняем флаг по умолчанию, с предыдущего нужно снять.
                                o_tmp = (XPObject)sess.FindObject(_Type, new GroupOperator(GroupOperatorType.And, new BinaryOperator("Group", o_o.GetMemberValue("Group")), new BinaryOperator("fl_def", true), new BinaryOperator("Oid", _iId, BinaryOperatorType.NotEqual)), false);
                                if (o_tmp != null)
                                {
                                    o_tmp.SetMemberValue("fl_def", false);
                                    o_tmp.Save();
                                }
                            }

                            o_o.Save();
                            _iId = o_o.Oid;
                        }
                    }
                    else
                    {
                        switch (_SprVaiant)
                        {
                            //case cls_App.SprVariants.Spr_Vfo:
                            //    if (_iId != -1) o_o = (XPObject)sess.GetObjectByKey(_Type, _iId, true);
                            //    else
                            //    {
                            //        o_o = (XPObject)sess.GetClassInfo(_Type).CreateNewObject(sess);
                            //        o_o.SetMemberValue("g_id", Guid.NewGuid().ToString());
                            //    }

                            //    if (o_o != null)
                            //    {
                            //        if (txtCode.Text == String.Empty)
                            //        {
                            //            XtraMessageBox.Show("Код не определен!", " Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //            txtCode.Focus();
                            //            txtCode.Select(0, txtCode.Text.Length);
                            //            return;
                            //        }

                            //        int l_id = (int)o_o.GetMemberValue("Oid");

                            //        // Плохие ситуации:                (ищем их)
                            //        // isnull(beg)    - isnull(end)
                            //        //  d1 <= beg     -      end <= d2
                            //        //        beg  <= d1 <=  end
                            //        //        beg  <= d2 <=  end
                            //        if (sess.FindObject(_Type, new GroupOperator(GroupOperatorType.And, new CriteriaOperator[] { 
                            //                    CriteriaOperator.Parse("(CodeVfo=? and Oid!=? and ((IsNullOrEmpty(date_beg) and IsNullOrEmpty(date_end)) or (GetDate(date_beg)>=? and GetDate(date_end)<=?) or ((GetDate(date_beg)<=? or IsNullOrEmpty(date_beg)) and (GetDate(date_end)>=? or IsNullOrEmpty(date_end))) or ((GetDate(date_beg)<=? or IsNullOrEmpty(date_beg)) and (GetDate(date_end)>=? or IsNullOrEmpty(date_end)))))",
                            //                    l_code, l_id, d1,d2, d1,d1, d2,d2) }), false) != null)
                            //        {
                            //            XtraMessageBox.Show("Новый временной диапазон данного кода пересекается с предыдущим диапазоном!", " Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //            //fl_for_copy = true;
                            //            txtName.Focus();
                            //            txtName.Select(0, txtName.Text.Length);

                            //            return;
                            //        }

                            //        o_o.SetMemberValue("CodeVfo", l_code);
                            //        o_o.SetMemberValue("Name", txtName.Text);
                            //        o_o.SetMemberValue("FullName", memoFullName.Text);
                            //        o_o.SetMemberValue("date_beg", d1);
                            //        o_o.SetMemberValue("date_end", d2);
                            //        //o_o.SetMemberValue("fl_def", (bool)chkDefault.EditValue);
                            //        //o_o.SetMemberValue("ImageIndex", imgImageIndex.SelectedIndex + _delta_image_index);
                            //        o_o.Save();
                            //        _iId = o_o.Oid;
                            //    }
                            //    break;
                            //-----------------------------------------------------
                            //case cls_App.SprVariants.Spr_KVR:
                            //    if (_iId != -1) o_o = sess.GetObjectByKey<set_KVR>(_iId);
                            //    else o_o = new set_KVR(sess) { g_id = Guid.NewGuid().ToString() };

                            //    if (o_o != null)
                            //    {
                            //        int n;
                            //        if (txtName.Text.Trim().Length != 3 || !int.TryParse((txtName.Text.Trim()), out n))
                            //        {
                            //            XtraMessageBox.Show("Код должен состоять из 3 цифр", " Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //            fl_for_copy = true;
                            //            txtName.Focus();
                            //            txtName.Select(0, txtName.Text.Length);
                            //            return;
                            //        }

                            //        if (sess.FindObject<set_KVR>(new GroupOperator(GroupOperatorType.And, new CriteriaOperator[] 
                            //        { CriteriaOperator.Parse("(Kod = ? and Name = ? and (begin_date=? or IsNullOrEmpty(begin_date))and (end_date=? or IsNullOrEmpty(end_date)))",
                            //            txtName.Text.Trim(), memoFullName.Text.Trim(), dateEditDateBegin.DateTime,dateEditDateEnd.DateTime) }), false) == null)
                            //        {

                            //            if (sess.FindObject<set_KVR>(new GroupOperator(GroupOperatorType.And, new CriteriaOperator[] { CriteriaOperator.Parse("(Kod = ? and g_id !=?)", 
                            //            txtName.Text.Trim(), ((set_KVR)o_o).g_id) }), false) == null)
                            //            {
                            //                ((set_KVR)o_o).Kod = txtName.Text.Trim();
                            //                ((set_KVR)o_o).Name = memoFullName.Text.Trim();
                            //                ((set_KVR)o_o).begin_date = dateEditDateBegin.DateTime;
                            //                if (dateEditDateEnd.DateTime != DateTime.MinValue)
                            //                    ((set_KVR)o_o).end_date = dateEditDateEnd.DateTime;
                            //                else
                            //                    ((set_KVR)o_o).end_date = DateTime.MaxValue;
                            //                ((set_KVR)o_o).ImageIndex = 3;
                            //                ((set_KVR)o_o).fl_imp = false;
                            //                ((set_KVR)o_o).Save();
                            //                _iId = o_o.Oid;
                            //            }
                            //            else
                            //            {
                            //                XtraMessageBox.Show("Запись с данным кодом уже существует", " Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //                fl_for_copy = true;
                            //                txtName.Focus();
                            //                txtName.Select(0, txtName.Text.Length);
                            //                return;
                            //            }
                            //        }
                            //        else
                            //        {
                            //            XtraMessageBox.Show("Такая запись уже существует", " Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //            fl_for_copy = true;
                            //            txtName.Focus();
                            //            txtName.Select(0, txtName.Text.Length);

                            //            return;
                            //        }
                            //    }
                            //    break;
                            //-----------------------------------------------------
                        }
                    }

                    sess.Disconnect();
                }

                _FlagOK = true;
                //if (!fl_for_copy) _FlagOK = true;
                Close();
            }
            catch (Exception ex)
            {
                BVVGlobal._logger.Debug(String.Format("{0}\r\n                         {1}", ex.Message, ex.StackTrace));
                XtraMessageBox.Show(ex.Message);
            }
        }
    }
}

