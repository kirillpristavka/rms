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
    public partial class TaxIndirectEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        private Tax Tax { get; }
        public TaxIndirect TaxIndirect { get; }

        private TaxIndirectEdit()
        {
            InitializeComponent();

            foreach (Availability item in Enum.GetValues(typeof(Availability)))
            {
                cmbAvailability.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbAvailability.SelectedIndex = 0;
        }

        public TaxIndirectEdit(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            Tax = customer.Tax ?? new Tax(Session);
            TaxIndirect = Tax.TaxIndirect ?? new TaxIndirect(Session);
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

            if (TaxIndirect.Oid != -1 &&
                (
                    TaxIndirect.Date != dateDate.DateTime.Date ||
                    TaxIndirect.Availability != availability ||
                    TaxIndirect.Comment != memoComment.Text
                ))
            {
                TaxIndirect.ChronicleTaxIndirect.Add(new ChronicleTaxIndirect(Session)
                {
                    Date = TaxIndirect.Date,
                    IsUse = TaxIndirect.IsUse,
                    Comment = memoComment.Text,
                    Availability = TaxIndirect.Availability,
                    User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid),
                    DateCreate = DateTime.Now
                });
            }            

            TaxIndirect.Date = dateDate.DateTime;
            TaxIndirect.Availability = availability;
            TaxIndirect.Comment = memoComment.Text;

            TaxIndirect.Save();
            Tax.TaxIndirect = TaxIndirect;
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
            dateDate.EditValue = TaxIndirect.Date ?? DateTime.Now.Date;            
            memoComment.EditValue = TaxIndirect.Comment;

            if (TaxIndirect.Availability != null)
            {
                cmbAvailability.SelectedIndex = Convert.ToInt32(TaxIndirect.Availability);
            }
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnChronicle_Click(object sender, EventArgs e)
        {
            var form = new ChronicleTaxesForm<ChronicleTaxIndirect>(TaxIndirect.ChronicleTaxIndirect, "Косвенные налоги");
            form.ShowDialog();
        }
    }
}