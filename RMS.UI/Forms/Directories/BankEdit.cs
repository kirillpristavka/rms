using DevExpress.Xpo;
using DevExpress.XtraEditors;
using RMS.Core.Controller;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle;
using System;
using System.Windows.Forms;

namespace RMS.UI.Forms.Directories
{
    public partial class BankEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private Bank Bank { get; }

        public BankEdit()
        {
            InitializeComponent();

            foreach (StatusOrganization item in Enum.GetValues(typeof(StatusOrganization)))
            {
                cmbOrganizationStatus.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbOrganizationStatus.SelectedIndex = 0;

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                Bank = new Bank(Session);
            }
        }

        public BankEdit(int id) : this()
        {
            if (id > 0)
            {
                Bank = Session.GetObjectByKey<Bank>(id);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Bank.PaymentName = txtName.Text;
            Bank.OKPO = txtOKPO.Text;
            Bank.BIC = txtBIC.Text;
            Bank.CorrespondentAccount = txtCorrespondentAccount.Text;
            Bank.Town = txtTown.Text;
            Bank.Address = txtAddress.Text;
            Bank.RegistrationNumber = txtRegistrationNumber.Text;
            Bank.SWIFT = txtSWIFT.Text;
            Bank.Telephone = txtTelephone.Text;
            Bank.Status = (StatusOrganization)cmbOrganizationStatus.SelectedIndex;
            Bank.DateActuality = dateActuality.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(dateActuality.EditValue);
            Bank.DateLiquidation = dateLiquidation.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(dateLiquidation.EditValue);
            Bank.DateRegistration = dateRegistration.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(dateRegistration.EditValue);
            Session.Save(Bank);

            id = Bank.Oid;
            flagSave = true;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BankEdit_Load(object sender, EventArgs e)
        {
            txtName.Text = Bank.PaymentName;
            txtOKPO.Text = Bank.OKPO;
            txtBIC.Text = Bank.BIC;
            txtCorrespondentAccount.Text = Bank.CorrespondentAccount;
            txtTown.Text = Bank.Town;
            txtAddress.Text = Bank.Address;
            txtRegistrationNumber.Text = Bank.RegistrationNumber;
            txtSWIFT.Text = Bank.SWIFT;
            txtTelephone.Text = Bank.Telephone;
            cmbOrganizationStatus.SelectedIndex = (int)Bank.Status;
            dateActuality.EditValue = Bank.DateActuality;
            dateRegistration.EditValue = Bank.DateRegistration;
            dateLiquidation.EditValue = Bank.DateLiquidation;
        }

        private void txtBIC_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (XtraMessageBox.Show("Если вы нажмете ОК, будет обновлена вся доступная информация по организации. Продолжить?",
                "Запрос на обновление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                var getInfoFromDaData = new GetInfoBankFromDaData("a8cfa20b99fb660c53b8e0fa2d3de0f2204fb580", txtBIC.Text);

                txtName.Text = getInfoFromDaData.PaymentName;
                txtBIC.Text = getInfoFromDaData.BIC;
                txtTown.Text = getInfoFromDaData.Town;
                txtOKPO.Text = getInfoFromDaData.OKPO;
                txtCorrespondentAccount.Text = getInfoFromDaData.CorrespondentAccount;
                txtAddress.Text = getInfoFromDaData.Address;
                txtRegistrationNumber.Text = getInfoFromDaData.RegistrationNumber;
                txtSWIFT.Text = getInfoFromDaData.SWIFT;
                txtTelephone.Text = getInfoFromDaData.Telephone;
                cmbOrganizationStatus.SelectedIndex = (int)getInfoFromDaData.Status;
                dateActuality.EditValue = getInfoFromDaData.DateActuality;
                dateRegistration.EditValue = getInfoFromDaData.DateRegistration;
                dateLiquidation.EditValue = getInfoFromDaData.DateLiquidation;

                var description = $"Автоматическое заполнение карточки банка по БИК [{txtBIC.Text.Trim()}]";
                var chronicleEvents = new ChronicleEvents(Session)
                {
                    Act = Act.UPDATING_BANK_INFORMATION_HAND,
                    Date = DateTime.Now,
                    Description = description,
                    User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                };
                Session.Save(chronicleEvents);
            }
        }
    }
}