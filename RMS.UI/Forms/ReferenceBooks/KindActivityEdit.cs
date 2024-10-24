using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Controller;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.OKVED;
using System;
using System.Linq;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class KindActivityEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        public KindActivity KindActivity { get; }

        public KindActivityEdit()
        {
            InitializeComponent();
            xtraTabKindActivity.ShowTabHeader = DefaultBoolean.False;

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                KindActivity = new KindActivity(Session);
            }
        }

        public KindActivityEdit(int id) : this()
        {
            if (id > 0)
            {
                KindActivity = Session.GetObjectByKey<KindActivity>(id);
            }
        }

        public KindActivityEdit(KindActivity kindActivity) : this()
        {
            Session = kindActivity.Session;
            KindActivity = kindActivity;
        }

        public KindActivityEdit(Customer customer) : this()
        {
            Session = customer.Session;
            KindActivity = customer.KindActivity ?? new KindActivity(Session);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnClassOKVED2.EditValue is ClassOKVED2 classOKVED2)
            {
                KindActivity.ClassOKVED2 = classOKVED2;
            }
            else
            {
                KindActivity.ClassOKVED2 = null;
            }

            if (btnPhysicalIndicator.EditValue is PhysicalIndicator physicalIndicator)
            {
                KindActivity.PhysicalIndicator = physicalIndicator;
            }
            else
            {
                KindActivity.PhysicalIndicator = null;
            }

            KindActivity.IsRegistrationAtLocationOrganization = checkIsRegistrationAtLocationOrganization.Checked;
            KindActivity.BasicReturn = Convert.ToDecimal(calcBasicReturn.EditValue);

            Session.Save(KindActivity);
            id = KindActivity.Oid;
            flagSave = true;
            Close();
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            btnClassOKVED2.EditValue = KindActivity.ClassOKVED2;
            
            checkIsRegistrationAtLocationOrganization.Checked = KindActivity.IsRegistrationAtLocationOrganization;
            calcBasicReturn.EditValue = KindActivity.BasicReturn;
            btnPhysicalIndicator.EditValue = KindActivity.PhysicalIndicator;
            memoPhysicalIndicatorDescription.EditValue = KindActivity.PhysicalIndicator?.Description;
        }

        private void btnPhysicalIndicator_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                memoPhysicalIndicatorDescription.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<PhysicalIndicator>(Session, buttonEdit, (int)cls_App.ReferenceBooks.PhysicalIndicator, 1, null, null, false, null, string.Empty, false, true);

            if (buttonEdit.EditValue is PhysicalIndicator physicalIndicator)
            {
                memoPhysicalIndicatorDescription.EditValue = physicalIndicator.Description;
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
            else if (e.Button.Kind == ButtonPredefines.Search)
            {
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

                        if (btnClassOKVED2.EditValue is ClassOKVED2 classOKVED2)
                        {
                            if (sectionOKVED2.ClassesOKVED.FirstOrDefault(f => f.Equals(classOKVED2)) == null)
                            {
                                btnClassOKVED2.EditValue = null;
                            }
                        }                        
                    }
                }
            }
            else if (e.Button.Kind == ButtonPredefines.Ellipsis)
            {
                cls_BaseSpr.ButtonEditButtonClickBase<SectionOKVED2>(Session, buttonEdit, (int)cls_App.ReferenceBooks.SectionOKVED2, 1, null, null, false, null, string.Empty, false, true);

                if (buttonEdit.EditValue is SectionOKVED2 sectionOKVED2)
                {
                    if (btnClassOKVED2.EditValue is ClassOKVED2 classOKVED2)
                    {
                        if (sectionOKVED2.ClassesOKVED.FirstOrDefault(f => f.Equals(classOKVED2)) == null)
                        {
                            btnClassOKVED2.EditValue = null;
                        }
                    }
                }
            }
        }

        private void btnClassOKVED2_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }
            else if (e.Button.Kind == ButtonPredefines.Search)
            {
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

                        btnClassOKVED2.EditValue = classOKVED2;
                    }
                }
            }
            else if (e.Button.Kind == ButtonPredefines.Ellipsis)
            {
                CriteriaOperator sectionCriteria = null;                
                cls_BaseSpr.ButtonEditButtonClickBase<ClassOKVED2>(Session, buttonEdit, (int)cls_App.ReferenceBooks.ClassOKVED2, 1, sectionCriteria, null, false, null, string.Empty, false, true);
            }
        }

        private void btnClassOKVED2_EditValueChanged(object sender, EventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;
            
            if (buttonEdit != null && buttonEdit.EditValue is ClassOKVED2 classOKVED2)
            {
                txtName.EditValue = classOKVED2.Name;
            }
            else
            {
                txtName.EditValue = null;
            }
        }
    }
}