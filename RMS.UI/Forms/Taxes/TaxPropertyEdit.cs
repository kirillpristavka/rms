using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle.Taxes;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Taxes;
using RMS.UI.Forms.Chronicle;
using System;
using System.Windows.Forms;

namespace RMS.UI.Forms.Taxes
{
    public partial class TaxPropertyEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        private Tax Tax { get; }
        public TaxProperty TaxProperty { get; }

        private TaxPropertyEdit()
        {
            InitializeComponent();

            foreach (Availability item in Enum.GetValues(typeof(Availability)))
            {
                cmbAvailability.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbAvailability.SelectedIndex = 0;
        }

        public TaxPropertyEdit(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            Tax = customer.Tax ?? new Tax(Session);
            TaxProperty = Tax.TaxProperty ?? new TaxProperty(Session) { TotalRate = 0.022M };
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var availability = default(Availability);

            if (!string.IsNullOrWhiteSpace(cmbAvailability.Text) && cmbAvailability.SelectedIndex != -1)
            {
                foreach (Availability item in Enum.GetValues(typeof(Availability)))
                {
                    if (item.GetEnumDescription().Equals(cmbAvailability.Text))
                    {
                        availability = item;
                        break;
                    }
                }
            }
            else
            {
                cmbAvailability.Focus();
                XtraMessageBox.Show("Сохранение не возможно. Укажите наличие.", "Не указано наличие", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dateDate.EditValue is null)
            {
                dateDate.Focus();
                XtraMessageBox.Show("Сохранение не возможно без указания даты.", "Не указана дата", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (checkIsPropertyExemptFromTax.Checked is true)
            {
                if (btnPrivilege.EditValue is null)
                {
                    btnPrivilege.Focus();
                    XtraMessageBox.Show("Необходимо указать льготу, указывающую на освобождение от налога.", "Не указана льгота", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            var privilege = default(Privilege);

            if (btnPrivilege.EditValue != null)
            {
                privilege = (Privilege)btnPrivilege.EditValue;
            }

            if (TaxProperty.Oid != -1 &&
                (
                    TaxProperty.Date != dateDate.DateTime ||
                    TaxProperty.TotalRate != Convert.ToDecimal(spinTotalRate.EditValue) ||
                    TaxProperty.ReducedBy != Convert.ToDecimal(spinReducedBy.EditValue) ||
                    TaxProperty.IsReducedRate != checkIsReducedRate.Checked ||
                    TaxProperty.IsPropertyExemptFromTax != checkIsPropertyExemptFromTax.Checked ||
                    TaxProperty.IsTaxReduced != checkIsTaxReduced.Checked ||
                    TaxProperty.IsFillingSecondSectionWithOnePrivilege != checkIsFillingSecondSectionWithOnePrivilege.Checked ||
                    TaxProperty.Privilege != privilege ||
                    TaxProperty.Availability != availability ||
                    TaxProperty.Comment != memoComment.Text
                ))
            {
                TaxProperty.ChronicleTaxProperty.Add(new ChronicleTaxProperty(Session)
                {
                    Privilege = TaxProperty.Privilege,
                    Date = TaxProperty.Date,
                    TotalRate = TaxProperty.TotalRate,
                    ReducedBy = TaxProperty.ReducedBy,
                    IsReducedRate = TaxProperty.IsReducedRate,
                    IsPropertyExemptFromTax = TaxProperty.IsPropertyExemptFromTax,
                    IsTaxReduced = TaxProperty.IsTaxReduced,
                    IsFillingSecondSectionWithOnePrivilege = TaxProperty.IsFillingSecondSectionWithOnePrivilege,
                    IsUse = TaxProperty.IsUse,
                    Comment = TaxProperty.Comment,
                    Availability = TaxProperty.Availability,
                    User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid),
                    DateCreate = DateTime.Now
                });
            }

            TaxProperty.Privilege = privilege;
            TaxProperty.Date = dateDate.DateTime;
            TaxProperty.TotalRate = Convert.ToDecimal(spinTotalRate.EditValue);
            TaxProperty.ReducedBy = Convert.ToDecimal(spinReducedBy.EditValue);
            TaxProperty.IsReducedRate = checkIsReducedRate.Checked;
            TaxProperty.IsPropertyExemptFromTax = checkIsPropertyExemptFromTax.Checked;
            TaxProperty.IsTaxReduced = checkIsTaxReduced.Checked;
            TaxProperty.IsFillingSecondSectionWithOnePrivilege = checkIsFillingSecondSectionWithOnePrivilege.Checked;
            TaxProperty.Comment = memoComment.Text;
            TaxProperty.Availability = availability;

            TaxProperty.Save();
            Tax.TaxProperty = TaxProperty;
            Tax.Save();
            Customer.Tax = Tax;
            Customer.Save();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CustomerServiceProvidedEdit_Load(object sender, EventArgs e)
        {            
            btnCustomer.EditValue = Customer;
            dateDate.EditValue = TaxProperty.Date ?? DateTime.Now.Date;
            spinTotalRate.EditValue = TaxProperty.TotalRate;
            checkIsReducedRate.EditValue = TaxProperty.IsReducedRate;
            checkIsPropertyExemptFromTax.EditValue = TaxProperty.IsPropertyExemptFromTax;
            checkIsTaxReduced.EditValue = TaxProperty.IsTaxReduced;
            checkIsFillingSecondSectionWithOnePrivilege.EditValue = TaxProperty.IsFillingSecondSectionWithOnePrivilege;
            spinReducedBy.EditValue = TaxProperty.ReducedBy;
            btnPrivilege.EditValue = TaxProperty.Privilege;
            memoComment.EditValue = TaxProperty.Comment;

            if (TaxProperty.Availability != null)
            {
                cmbAvailability.SelectedIndex = Convert.ToInt32(TaxProperty.Availability);
            }
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnPrivilege_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Privilege>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Privilege, 1, null, null, false, null, string.Empty, false, true);
        }

        private void checkIsPropertyExemptFromTax_CheckedChanged(object sender, EventArgs e)
        {
            var checkEdit = sender as CheckEdit;

            if (checkEdit?.Checked is true)
            {
                btnPrivilege.Enabled = true;
                checkIsReducedRate.Checked = false;
                checkIsReducedRate.Enabled = false;
                checkIsTaxReduced.Checked = false;
                checkIsTaxReduced.Enabled = false;
                spinReducedBy.EditValue = 0;
            }
            else
            {
                btnPrivilege.Enabled = false;
                btnPrivilege.EditValue = null;
                checkIsReducedRate.Enabled = true;
                checkIsTaxReduced.Enabled = true;
            }
        }

        private void checkIsTaxReduced_CheckedChanged(object sender, EventArgs e)
        {
            var checkEdit = sender as CheckEdit;

            if (checkEdit?.Checked is true)
            {
                spinReducedBy.Enabled = true;
                spinReducedBy.EditValue = TaxProperty.ReducedBy;
            }
            else
            {
                spinReducedBy.Enabled = false;
                spinReducedBy.EditValue = 0;
            }
        }

        private void btnChronicle_Click(object sender, EventArgs e)
        {
            var form = new ChronicleTaxesForm<ChronicleTaxProperty>(TaxProperty.ChronicleTaxProperty, "Налог на имущество");
            form.ShowDialog();
        }

        private void checkIsUse_CheckedChanged(object sender, EventArgs e)
        {
            var checkEdit = sender as CheckEdit;

            if (checkEdit?.Checked is true)
            {
                gpRate.Enabled = true;
                gpPrivilege.Enabled = true;
            }
            else
            {
                gpRate.Enabled = false;
                gpPrivilege.Enabled = false;
            }
        }

        private void cmbAvailability_SelectedIndexChanged(object sender, EventArgs e)
        {
            var availability = default(Availability);

            if (!string.IsNullOrWhiteSpace(cmbAvailability.Text) && cmbAvailability.SelectedIndex != -1)
            {
                foreach (Availability item in Enum.GetValues(typeof(Availability)))
                {
                    if (item.GetEnumDescription().Equals(cmbAvailability.Text))
                    {
                        availability = item;
                        break;
                    }
                }
            }

            if (availability == Availability.True)
            {
                gpRate.Enabled = true;
                gpPrivilege.Enabled = true;
            }
            else
            {
                gpRate.Enabled = false;
                gpPrivilege.Enabled = false;
            }
        }
    }
}