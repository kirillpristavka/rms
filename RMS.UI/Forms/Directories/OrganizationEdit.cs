using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Controller;
using RMS.Core.Model;
using System;
using System.Windows.Forms;

namespace RMS.UI.Forms.Directories
{
    public partial class OrganizationEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private Organization Organization { get; }

        public OrganizationEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                Organization = new Organization(Session);
            }
        }

        public OrganizationEdit(int id) : this()
        {
            if (id > 0)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                Organization = Session.GetObjectByKey<Organization>(id);
            }
        }

        public OrganizationEdit(Organization organization) : this()
        {
            Session = organization.Session;
            Organization = organization;
        }

        private void FormEdit_Load(object sender, EventArgs e)
        {
            memoName.EditValue = Organization.Name;
            btnINN.EditValue = Organization.INN;
            txtKPP.EditValue = Organization.KPP;
            dateRegistration.EditValue = Organization.DateRegistration;
            dateLiquidation.EditValue = Organization.DateLiquidation;
            txtOKPO.EditValue = Organization.OKPO;
            txtOKVED.EditValue = Organization.OKVED;
            txtPSRN.EditValue = Organization.PSRN;
            datePSRN.EditValue = Organization.DatePSRN;
            memoFullName.EditValue = Organization.FullName;
            memoAbbreviatedName.EditValue = Organization.AbbreviatedName;
            txtTelephone.EditValue = Organization.Telephone;

            txtManagementSurname.EditValue = Organization.ManagementSurname;
            txtManagementName.EditValue = Organization.ManagementName;
            txtManagementPatronymic.EditValue = Organization.ManagementPatronymic;
            btnManagementPosition.EditValue = Organization.ManagementPosition;

            txtPrefix.Text = Organization.Prefix;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Organization.Name = memoName.Text;
            Organization.INN = btnINN.Text;
            Organization.KPP = txtKPP.Text;

            if (dateRegistration.EditValue is DateTime registrationDate)
            {
                Organization.DateRegistration = registrationDate;
            }
            else
            {
                Organization.DateRegistration = null;
            }
            
            if (dateLiquidation.EditValue is DateTime liquidationDate)
            {
                Organization.DateLiquidation = liquidationDate;
            }
            else
            {
                Organization.DateLiquidation = null;
            }
            
            Organization.OKPO = txtOKPO.Text;
            Organization.OKVED = txtOKVED.Text;
            Organization.PSRN = txtPSRN.Text;

            if (datePSRN.EditValue is DateTime PSRNDate)
            {
                Organization.DatePSRN = PSRNDate;
            }
            else
            {
                Organization.DatePSRN = null;
            }
            
            Organization.FullName = memoFullName.Text;
            Organization.AbbreviatedName = memoAbbreviatedName.Text;
            Organization.Telephone = txtTelephone.Text;

            Organization.ManagementSurname = txtManagementSurname.Text;
            Organization.ManagementName = txtManagementName.Text;
            Organization.ManagementPatronymic = txtManagementPatronymic.Text;

            if (btnManagementPosition.EditValue is Position position)
            {
                Organization.ManagementPosition = position;
            }
            else
            {
                Organization.ManagementPosition = null;
            }

            Organization.Prefix = txtPrefix.Text;

            Session.Save(Organization);
            id = Organization.Oid;
            flagSave = true;
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btnINN_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }
            else if (e.Button.Kind == ButtonPredefines.Search)
            {
                if (XtraMessageBox.Show("Если вы нажмете ОК, будет обновлена вся доступная информация по организации. Продолжить?",
                "Запрос на обновление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    var getInfoFromDaData = new GetInfoOrganizationFromDaData(
                    "a8cfa20b99fb660c53b8e0fa2d3de0f2204fb580", "080aefa3543cb56dfe122f26a16a04703cacb128", btnINN.Text);
                    await getInfoFromDaData.GetDataAsync();
                    
                    if (string.IsNullOrWhiteSpace(memoName.Text))
                    {
                        memoName.Text = getInfoFromDaData.Name;
                    }

                    memoFullName.Text = getInfoFromDaData.FullName ?? memoFullName.Text;
                    memoAbbreviatedName.Text = getInfoFromDaData.AbbreviatedName ?? memoAbbreviatedName.Text;

                    txtOKPO.Text = getInfoFromDaData.OKPO ?? txtOKPO.Text;
                    datePSRN.EditValue = getInfoFromDaData.DateOGRN ?? datePSRN.EditValue;
                    txtPSRN.Text = getInfoFromDaData.OGRN ?? txtPSRN.Text;
                    txtKPP.Text = getInfoFromDaData.KPP ?? txtKPP.Text;
                    txtManagementSurname.Text = getInfoFromDaData.ManagementSurname ?? txtManagementSurname.Text;
                    txtManagementName.Text = getInfoFromDaData.ManagementName ?? txtManagementName.Text;
                    txtManagementPatronymic.Text = getInfoFromDaData.ManagementPatronymic ?? txtManagementPatronymic.Text;

                    if (!string.IsNullOrWhiteSpace(getInfoFromDaData.ManagementPosition))
                    {
                        var position = Session.FindObject<Position>(new BinaryOperator(nameof(Position.Name), getInfoFromDaData.ManagementPosition));
                        if (position is null)
                        {
                            position = new Position(Session)
                            {
                                Name = getInfoFromDaData.ManagementPosition,
                                Description = getInfoFromDaData.ManagementPosition
                            };
                            position.Save();
                        }
                        btnManagementPosition.EditValue = position;
                    }

                    txtOKVED.Text = getInfoFromDaData.OKVED ?? txtOKVED.Text;

                    //if (!string.IsNullOrWhiteSpace(getInfoFromDaData.Address))
                    //{
                    //    if (Customer.CustomerAddress.FirstOrDefault(f => f.IsLegal) == null)
                    //    {
                    //        var suggestResponse = GetInfoAddressFromDaData.UpdateFromDaData("a8cfa20b99fb660c53b8e0fa2d3de0f2204fb580", getInfoFromDaData.Address);
                    //        var addressString = suggestResponse?.suggestions[0]?.unrestricted_value;

                    //        if (!string.IsNullOrWhiteSpace(addressString))
                    //        {
                    //            var customerAddress = new CustomerAddress(Session)
                    //            {
                    //                IsLegal = true,
                    //                AddressString = addressString
                    //            };
                    //            Customer.CustomerAddress.Add(customerAddress);
                    //        }
                    //    }
                    //}
                    
                    dateRegistration.EditValue = getInfoFromDaData.DateRegistration;
                    dateLiquidation.EditValue = getInfoFromDaData.DateLiquidation;
                }
            }
        }

        private void btnManagementPosition_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Position>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Position, 1, null, null, false, null, string.Empty, false, true);
        }
    }
}