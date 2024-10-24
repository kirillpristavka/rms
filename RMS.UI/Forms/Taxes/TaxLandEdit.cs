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
    public partial class TaxLandEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        private Tax Tax { get; }
        public TaxLand TaxLand { get; }

        private TaxLandEdit()
        {
            InitializeComponent();

            foreach (Availability item in Enum.GetValues(typeof(Availability)))
            {
                cmbAvailability.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbAvailability.SelectedIndex = 0;
        }

        public TaxLandEdit(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            Tax = customer.Tax ?? new Tax(Session);
            TaxLand = Tax.TaxLand ?? new TaxLand(Session);
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

            if (TaxLand.Oid != -1 &&
                (
                    TaxLand.Date != dateDate.DateTime.Date ||
                    TaxLand.Availability != availability ||
                    TaxLand.Comment != memoComment.Text
                ))
            {
                TaxLand.ChronicleTaxLand.Add(new ChronicleTaxLand(Session)
                {
                    Date = TaxLand.Date,
                    IsUse = TaxLand.IsUse,
                    Comment = memoComment.Text,
                    Availability = TaxLand.Availability,
                    User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid),
                    DateCreate = DateTime.Now
                });
            }            

            TaxLand.Date = dateDate.DateTime;
            TaxLand.Availability = availability;
            TaxLand.Comment = memoComment.Text;

            TaxLand.Save();
            Tax.TaxLand = TaxLand;
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
            dateDate.EditValue = TaxLand.Date ?? DateTime.Now.Date;            
            memoComment.EditValue = TaxLand.Comment;

            if (TaxLand.Availability != null)
            {
                cmbAvailability.SelectedIndex = Convert.ToInt32(TaxLand.Availability);
            }
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnChronicle_Click(object sender, EventArgs e)
        {
            var form = new ChronicleTaxesForm<ChronicleTaxLand>(TaxLand.ChronicleTaxLand, "Земельный налог");
            form.ShowDialog();
        }
    }
}