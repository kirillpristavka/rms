using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Taxes;
using RMS.UI.Forms.Chronicle;
using System;
using System.Windows.Forms;

namespace RMS.UI.Forms.Taxes
{
    public partial class LeasingEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        public Leasing Leasing { get; }

        private LeasingEdit()
        {
            InitializeComponent();

            foreach (Availability item in Enum.GetValues(typeof(Availability)))
            {
                cmbAvailability.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbAvailability.SelectedIndex = 0;
        }

        public LeasingEdit(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            Leasing = customer.Leasing ?? new Leasing(Session);
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

            if (Leasing.Oid != -1 &&
                (
                    Leasing.Date != dateDate.DateTime.Date ||
                    Leasing.Availability != availability ||
                    Leasing.Comment != memoComment.Text
                ))
            {
                Leasing.ChronicleLeasing.Add(new ChronicleLeasing(Session)
                {
                    Date = Leasing.Date,
                    IsUse = Leasing.IsUse,
                    Comment = memoComment.Text,
                    Availability = Leasing.Availability,
                    User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid),
                    DateCreate = DateTime.Now
                });
            }

            Leasing.Date = dateDate.DateTime;
            Leasing.Availability = availability;
            Leasing.Comment = memoComment.Text;

            Leasing.Save();
            Customer.Leasing = Leasing;
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
            dateDate.EditValue = Leasing.Date ?? DateTime.Now.Date;            
            memoComment.EditValue = Leasing.Comment;

            if (Leasing.Availability != null)
            {
                cmbAvailability.SelectedIndex = Convert.ToInt32(Leasing.Availability);
            }
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnChronicle_Click(object sender, EventArgs e)
        {
            var form = new ChronicleTaxesForm<ChronicleLeasing>(Leasing.ChronicleLeasing, "Лизинг");
            form.ShowDialog();
        }
    }
}