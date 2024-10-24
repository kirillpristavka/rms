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
    public partial class TaxImportEAEUEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        private Tax Tax { get; }
        public TaxImportEAEU TaxImportEAEU { get; }

        private TaxImportEAEUEdit()
        {
            InitializeComponent();

            foreach (Availability item in Enum.GetValues(typeof(Availability)))
            {
                cmbAvailability.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbAvailability.SelectedIndex = 0;
        }

        public TaxImportEAEUEdit(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            Tax = customer.Tax ?? new Tax(Session);
            TaxImportEAEU = Tax.TaxImportEAEU ?? new TaxImportEAEU(Session);
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

            if (TaxImportEAEU.Oid != -1 &&
                (
                    TaxImportEAEU.Date != dateDate.DateTime.Date ||
                    TaxImportEAEU.Availability != availability ||
                    TaxImportEAEU.Comment != memoComment.Text
                ))
            {
                TaxImportEAEU.ChronicleTaxImportEAEU.Add(new ChronicleTaxImportEAEU(Session)
                {
                    Date = TaxImportEAEU.Date,
                    IsUse = TaxImportEAEU.IsUse,
                    Comment = memoComment.Text,
                    Availability = TaxImportEAEU.Availability,
                    User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid),
                    DateCreate = DateTime.Now
                });
            }            

            TaxImportEAEU.Date = dateDate.DateTime;
            TaxImportEAEU.Availability = availability;
            TaxImportEAEU.Comment = memoComment.Text;

            TaxImportEAEU.Save();
            Tax.TaxImportEAEU = TaxImportEAEU;
            Tax.Save();
            Customer.Tax = Tax;
            Customer.Save();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            btnCustomer.EditValue = Customer;
            dateDate.EditValue = TaxImportEAEU.Date ?? DateTime.Now.Date;            
            memoComment.EditValue = TaxImportEAEU.Comment;

            if (TaxImportEAEU.Availability != null)
            {
                cmbAvailability.SelectedIndex = Convert.ToInt32(TaxImportEAEU.Availability);
            }
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnChronicle_Click(object sender, EventArgs e)
        {
            var form = new ChronicleTaxesForm<ChronicleTaxImportEAEU>(TaxImportEAEU.ChronicleTaxImportEAEU, "Налог на алкоголь");
            form.ShowDialog();
        }
    }
}