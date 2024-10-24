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
    public partial class OneEsSalaryEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        private OneEs OneEs { get; }
        public OneEsSalary OneEsSalary { get; }

        private OneEsSalaryEdit()
        {
            InitializeComponent();

            foreach (Availability item in Enum.GetValues(typeof(Availability)))
            {
                cmbAvailability.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbAvailability.SelectedIndex = 0;
        }

        public OneEsSalaryEdit(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            OneEs = customer.OneEs ?? new OneEs(Session);
            OneEsSalary = OneEs.OneEsSalary ?? new OneEsSalary(Session);
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

            if (OneEsSalary.Oid != -1 &&
                (
                    OneEsSalary.Date != DateTime.Now.Date ||
                    OneEsSalary.Availability != availability ||
                    OneEsSalary.Comment != memoComment.Text ||
                    OneEsSalary.Path != txtPath.Text
                ))
            {
                OneEsSalary.ChronicleOneEsSalary.Add(new ChronicleOneEsSalary(Session)
                {
                    Date = OneEsSalary.Date,
                    IsUse = OneEsSalary.IsUse,
                    Comment = memoComment.Text,
                    Path = OneEsSalary.Path,
                    Availability = OneEsSalary.Availability,
                    User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid),
                    DateCreate = DateTime.Now
                });
            }

            OneEsSalary.Date = DateTime.Now.Date;
            OneEsSalary.Comment = memoComment.Text;
            OneEsSalary.Path = txtPath.Text;
            OneEsSalary.Availability = availability;

            OneEsSalary.Save();
            OneEs.OneEsSalary = OneEsSalary;
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
            memoComment.EditValue = OneEsSalary.Comment;
            txtPath.Text = OneEsSalary.Path;

            if (OneEsSalary.Availability != null)
            {
                cmbAvailability.SelectedIndex = Convert.ToInt32(OneEsSalary.Availability);
            }            
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnChronicle_Click(object sender, EventArgs e)
        {
            var form = new ChronicleTaxesForm<ChronicleOneEsSalary>(OneEsSalary.ChronicleOneEsSalary, "1C Зарплата");
            form.ShowDialog();
        }
    }
}