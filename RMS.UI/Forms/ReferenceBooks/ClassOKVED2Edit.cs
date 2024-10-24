using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Controller;
using RMS.Core.Model.OKVED;
using System;
using System.Linq;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class ClassOKVED2Edit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private ClassOKVED2 ClassOKVED2 { get; }

        public ClassOKVED2Edit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                ClassOKVED2 = new ClassOKVED2(Session);
            }
        }

        public ClassOKVED2Edit(int id) : this()
        {
            if (id > 0)
            {
                ClassOKVED2 = Session.GetObjectByKey<ClassOKVED2>(id);
            }
        }

        public ClassOKVED2Edit(ClassOKVED2 classOKVED2) : this()
        {
            Session = classOKVED2.Session;
            ClassOKVED2 = classOKVED2;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ClassOKVED2.Code = txtCode.Text;
            ClassOKVED2.Name = txtName.Text;
            ClassOKVED2.Description = memoDescription.Text;

            if (btnSectionOKVED2.EditValue is SectionOKVED2 sectionOKVED2)
            {
                ClassOKVED2.SectionOKVED = sectionOKVED2;
            }
            else
            {
                ClassOKVED2.SectionOKVED = null;
            }
            
            Session.Save(ClassOKVED2);
            id = ClassOKVED2.Oid;
            flagSave = true;
            Close();
        }

        private void ClassOKVED2Edit_Load(object sender, EventArgs e)
        {
            memoDescription.Text = ClassOKVED2.Description;
            txtName.Text = ClassOKVED2.Name;
            txtCode.Text = ClassOKVED2.Code;

            btnSectionOKVED2.EditValue = ClassOKVED2.SectionOKVED;
        }

        private void txtCode_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (!string.IsNullOrWhiteSpace(buttonEdit.Text))
            {
                var okvedRecord = GetInfoOrganizationFromDaData.GetOkved2("a8cfa20b99fb660c53b8e0fa2d3de0f2204fb580", buttonEdit.Text);
                if (okvedRecord != null)
                {
                    var sectionOKVED2 = Session.FindObject<SectionOKVED2>(new BinaryOperator(nameof(SectionOKVED2.Code), okvedRecord.razdel));

                    if (sectionOKVED2 is null)
                    {
                        sectionOKVED2 = new SectionOKVED2(Session)
                        {
                            Code = okvedRecord.razdel,
                            Name = $"Раздел {okvedRecord.razdel}"
                        };
                        sectionOKVED2.Save();
                    }

                    var classOKVED2 = sectionOKVED2.ClassesOKVED.FirstOrDefault(f => f.Code.Equals(okvedRecord.kod));
                    if (classOKVED2 is null)
                    {
                        classOKVED2 = new ClassOKVED2(Session)
                        {
                            Code = okvedRecord.kod,
                            Name = okvedRecord.name
                        };
                        sectionOKVED2.ClassesOKVED.Add(classOKVED2);
                        classOKVED2.Save();
                    }

                    btnSectionOKVED2.EditValue = sectionOKVED2;
                    
                    txtCode.EditValue = classOKVED2;
                    txtName.EditValue = classOKVED2.Name;
                }
            }
        }

        private void btnSectionOKVED2_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }
            else if (e.Button.Kind == ButtonPredefines.Ellipsis)
            {
                cls_BaseSpr.ButtonEditButtonClickBase<SectionOKVED2>(Session, buttonEdit, (int)cls_App.ReferenceBooks.SectionOKVED2, 1, null, null, false, null, string.Empty, false, true);
            }
        }
    }
}