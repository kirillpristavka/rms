using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle.OnesEs;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.OnesEs;
using RMS.UI.Forms.Chronicle;
using System;
using System.Windows.Forms;

namespace RMS.UI.Forms.OnesEs
{
    public partial class OneEsBookkeepingEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        private OneEs OneEs { get; }
        public OneEsBookkeeping OneEsBookkeeping { get; }

        private OneEsBookkeepingEdit()
        {
            InitializeComponent();

            foreach (Availability item in Enum.GetValues(typeof(Availability)))
            {
                cmbAvailability.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbAvailability.SelectedIndex = 0;
        }

        public OneEsBookkeepingEdit(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            OneEs = customer.OneEs ?? new OneEs(Session);
            OneEsBookkeeping = OneEs.OneEsBookkeeping ?? new OneEsBookkeeping(Session);
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

            if (OneEsBookkeeping.Oid != -1 &&
                (
                    OneEsBookkeeping.Date != DateTime.Now.Date ||
                    OneEsBookkeeping.Availability != availability ||
                    OneEsBookkeeping.Comment != memoComment.Text ||
                    OneEsBookkeeping.Path != txtPath.Text
                ))
            {
                OneEsBookkeeping.ChronicleOneEsBookkeeping.Add(new ChronicleOneEsBookkeeping(Session)
                {
                    Date = OneEsBookkeeping.Date,
                    IsUse = OneEsBookkeeping.IsUse,
                    Comment = memoComment.Text,
                    Path = OneEsBookkeeping.Path,
                    Availability = OneEsBookkeeping.Availability,
                    User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid),
                    DateCreate = DateTime.Now
                });
            }

            OneEsBookkeeping.Date = DateTime.Now.Date;
            OneEsBookkeeping.Comment = memoComment.Text;
            OneEsBookkeeping.Path = txtPath.Text;
            OneEsBookkeeping.Availability = availability;

            OneEsBookkeeping.Save();
            OneEs.OneEsBookkeeping = OneEsBookkeeping;
            OneEs.Save();
            Customer.OneEs = OneEs;
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
            memoComment.EditValue = OneEsBookkeeping.Comment;
            txtPath.Text = OneEsBookkeeping.Path;

            if (OneEsBookkeeping.Availability != null)
            {
                cmbAvailability.SelectedIndex = Convert.ToInt32(OneEsBookkeeping.Availability);
            }            
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnChronicle_Click(object sender, EventArgs e)
        {
            var form = new ChronicleTaxesForm<ChronicleOneEsBookkeeping>(OneEsBookkeeping.ChronicleOneEsBookkeeping, "1C БУХ");
            form.ShowDialog();
        }
    }
}