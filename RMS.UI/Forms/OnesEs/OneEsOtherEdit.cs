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
    public partial class OneEsOtherEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        private OneEs OneEs { get; }
        public OneEsOther OneEsOther { get; }

        private OneEsOtherEdit()
        {
            InitializeComponent();

            foreach (Availability item in Enum.GetValues(typeof(Availability)))
            {
                cmbAvailability.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbAvailability.SelectedIndex = 0;
        }

        public OneEsOtherEdit(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            OneEs = customer.OneEs ?? new OneEs(Session);
            OneEsOther = OneEs.OneEsOther ?? new OneEsOther(Session);
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

            if (OneEsOther.Oid != -1 &&
                (
                    OneEsOther.Date != DateTime.Now.Date ||
                    OneEsOther.Availability != availability ||
                    OneEsOther.Comment != memoComment.Text ||
                    OneEsOther.Path != txtPath.Text
                ))
            {
                OneEsOther.ChronicleOneEsOther.Add(new ChronicleOneEsOther(Session)
                {
                    Date = OneEsOther.Date,
                    IsUse = OneEsOther.IsUse,
                    Comment = memoComment.Text,
                    Path = OneEsOther.Path,
                    Availability = OneEsOther.Availability,
                    User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid),
                    DateCreate = DateTime.Now
                });
            }

            OneEsOther.Date = DateTime.Now.Date;
            OneEsOther.Comment = memoComment.Text;
            OneEsOther.Path = txtPath.Text;
            OneEsOther.Availability = availability;

            OneEsOther.Save();
            OneEs.OneEsOther = OneEsOther;
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
            memoComment.EditValue = OneEsOther.Comment;
            txtPath.Text = OneEsOther.Path;

            if (OneEsOther.Availability != null)
            {
                cmbAvailability.SelectedIndex = Convert.ToInt32(OneEsOther.Availability);
            }            
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnChronicle_Click(object sender, EventArgs e)
        {
            var form = new ChronicleTaxesForm<ChronicleOneEsOther>(OneEsOther.ChronicleOneEsOther, "1C Иное");
            form.ShowDialog();
        }
    }
}