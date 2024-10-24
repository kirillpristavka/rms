using DevExpress.XtraEditors;
using RMS.Core.Controller.Print;
using System;

namespace RMS.UI.Forms
{
    public partial class ServiceCalculatorForm : XtraForm
    {
        private int Price { get; set; }
        private int FinalPrice { get; set; }

        public ServiceCalculatorForm()
        {
            InitializeComponent();

            rdGpTariff.SelectedIndex = 0;
            rdGpTaxSystem.SelectedIndex = 0;

            trackDocumentCount_EditValueChanged(trackDocumentCount, null);
        }

        private void trackStaffCount_EditValueChanged(object sender, EventArgs e)
        {
            var trackBar = sender as TrackBarControl;

            if (trackBar != null)
            {
                spinStaffCount.EditValue = trackBar.Value;
            }
        }

        private void spinStaffCount_EditValueChanged(object sender, EventArgs e)
        {
            var spinEdit = sender as SpinEdit;

            if (spinEdit != null)
            {
                trackStaffCount.Value = Convert.ToInt32(spinStaffCount.Value);

                FinalPrice = Price + (1000 * trackStaffCount.Value);
                //if (trackStaffCount.Value <= 5)
                //{
                //    FinalPrice = Price + (1000 * trackStaffCount.Value);
                //}
                //else
                //{
                //    FinalPrice = Price + (600 * trackStaffCount.Value);
                //}
                Sum();

                if (int.TryParse(txtWaybills.Text, out int resultWaybills))
                {
                    FinalPrice = FinalPrice + resultWaybills * 500;
                }

                if (int.TryParse(txtCorrectionDeclarationsOrCalculations.Text, out int count) && int.TryParse(txtCorrectionDeclarationsOrCalculationsSum.Text, out int price))
                {
                    FinalPrice = FinalPrice + count * price;
                }

                if (int.TryParse(txtOther.Text, out int other))
                {
                    FinalPrice = FinalPrice + other;
                }

                if (int.TryParse(txtDiscount.Text, out int result))
                {
                    FinalPrice = FinalPrice - (FinalPrice * result / 100);
                }

                lblPriceFinal.Text = FinalPrice.ToString();
            }
        }

        private void Sum()
        {
            if (checkСommissionTrade.Checked)
            {
                var del = Price * 30 / 100;
                FinalPrice = FinalPrice + del;
            }

            if (checkExciseTaxTtransactions.Checked)
            {
                var del = Price * 20 / 100;
                FinalPrice = FinalPrice + del;
            }

            if (checkSeparateUnits.Checked)
            {
                var del = Price * 15 / 100;
                FinalPrice = FinalPrice + del;
            }

            if (checkCombinationTaxationRegimes.Checked)
            {
                var del = Price * 20 / 100;
                FinalPrice = FinalPrice + del;
            }

            if (checkSeparateVATAccounting.Checked)
            {
                var del = Price * 20 / 100;
                FinalPrice = FinalPrice + del;
            }

            if (checkPBU1812.Checked)
            {
                var del = Price * 20 / 100;
                FinalPrice = FinalPrice + del;
            }

            if (checkBorrowedFunds.Checked)
            {
                var del = Price * 15 / 100;
                FinalPrice = FinalPrice + del;
            }

            if (checkDifferencesFromSalary.Checked)
            {
                var del = Price * 10 / 100;
                FinalPrice = FinalPrice + del;
            }

            if (checkFEA.Checked)
            {
                var del = Price * 30 / 100;
                FinalPrice = FinalPrice + del;
            }

            if (checkConstruction.Checked)
            {
                var del = Price * 25 / 100;
                FinalPrice = FinalPrice + del;
            }

            if (checkProduction.Checked)
            {
                var del = Price * 25 / 100;
                FinalPrice = FinalPrice + del;
            }
        }

        private void trackDocumentCount_EditValueChanged(object sender, EventArgs e)
        {
            FinalPrice = 0;
            Price = 0;

            var trackBar = sender as TrackBarControl;

            if (trackBar != null)
            {
                spinDocumentCount.EditValue = trackBar.Value;

                if (trackBar.Value <= 7)
                {
                    if (rdGpTaxSystem.SelectedIndex == 0)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 4000;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 4800;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 5200;
                        }
                    }
                    else if (rdGpTaxSystem.SelectedIndex == 1)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 3400;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 4080;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 4420;
                        }
                    }
                }
                else if (trackBar.Value > 7 && trackBar.Value <= 15)
                {
                    if (rdGpTaxSystem.SelectedIndex == 0)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 8400;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 10080;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 10920;
                        }
                    }
                    else if (rdGpTaxSystem.SelectedIndex == 1)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 7200;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 8640;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 9360;
                        }
                    }
                }
                else if (trackBar.Value > 15 && trackBar.Value <= 30)
                {
                    if (rdGpTaxSystem.SelectedIndex == 0)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 11300;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 13560;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 14690;
                        }
                    }
                    else if (rdGpTaxSystem.SelectedIndex == 1)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 9600;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 11520;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 12480;
                        }
                    }
                }
                else if (trackBar.Value > 30 && trackBar.Value <= 50)
                {
                    if (rdGpTaxSystem.SelectedIndex == 0)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 14200;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 17040;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 18460;
                        }
                    }
                    else if (rdGpTaxSystem.SelectedIndex == 1)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 12000;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 14400;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 15600;
                        }
                    }
                }
                else if (trackBar.Value > 50 && trackBar.Value <= 70)
                {
                    if (rdGpTaxSystem.SelectedIndex == 0)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 16200;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 19440;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 21060;
                        }
                    }
                    else if (rdGpTaxSystem.SelectedIndex == 1)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 13800;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 16560;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 17940;
                        }
                    }
                }
                else if (trackBar.Value > 70 && trackBar.Value <= 100)
                {
                    if (rdGpTaxSystem.SelectedIndex == 0)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 18400;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 22080;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 23920;
                        }
                    }
                    else if (rdGpTaxSystem.SelectedIndex == 1)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 15600;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 18720;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 20280;
                        }
                    }
                }
                else if (trackBar.Value > 100 && trackBar.Value <= 130)
                {
                    if (rdGpTaxSystem.SelectedIndex == 0)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 21600;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 25920;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 28080;
                        }
                    }
                    else if (rdGpTaxSystem.SelectedIndex == 1)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 18400;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 22080;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 23920;
                        }
                    }
                }
                else if (trackBar.Value > 130 && trackBar.Value <= 160)
                {
                    if (rdGpTaxSystem.SelectedIndex == 0)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 24700;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 29640;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 32110;
                        }
                    }
                    else if (rdGpTaxSystem.SelectedIndex == 1)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 21000;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 25200;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 27300;
                        }
                    }
                }
                else if (trackBar.Value > 160 && trackBar.Value <= 200)
                {
                    if (rdGpTaxSystem.SelectedIndex == 0)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 28000;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 33600;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 36400;
                        }
                    }
                    else if (rdGpTaxSystem.SelectedIndex == 1)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 23800;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 28560;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 30940;
                        }
                    }
                }
                else if (trackBar.Value > 200 && trackBar.Value <= 250)
                {
                    if (rdGpTaxSystem.SelectedIndex == 0)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 31400;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 37680;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 40820;
                        }
                    }
                    else if (rdGpTaxSystem.SelectedIndex == 1)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 26800;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 32160;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 34840;
                        }
                    }
                }
                else if (trackBar.Value > 250 && trackBar.Value <= 300)
                {
                    if (rdGpTaxSystem.SelectedIndex == 0)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 33800;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 40560;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 43940;
                        }
                    }
                    else if (rdGpTaxSystem.SelectedIndex == 1)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 28800;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 34560;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 37440;
                        }
                    }
                }
                else if (trackBar.Value > 300 && trackBar.Value <= 400)
                {
                    if (rdGpTaxSystem.SelectedIndex == 0)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 42500;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 51000;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 55250;
                        }
                    }
                    else if (rdGpTaxSystem.SelectedIndex == 1)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 36000;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 43200;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 46800;
                        }
                    }
                }
                else if (trackBar.Value > 400 && trackBar.Value <= 500)
                {
                    if (rdGpTaxSystem.SelectedIndex == 0)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 51400;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 61680;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 66820;
                        }
                    }
                    else if (rdGpTaxSystem.SelectedIndex == 1)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 43700;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 52440;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 56810;
                        }
                    }
                }
                else if (trackBar.Value > 500 && trackBar.Value <= 600)
                {
                    if (rdGpTaxSystem.SelectedIndex == 0)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 59200;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 71040;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 76960;
                        }
                    }
                    else if (rdGpTaxSystem.SelectedIndex == 1)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 50400;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 60480;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 65520;
                        }
                    }
                }
                else if (trackBar.Value > 600 && trackBar.Value <= 750)
                {
                    if (rdGpTaxSystem.SelectedIndex == 0)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 72400;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 86880;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 94120;
                        }
                    }
                    else if (rdGpTaxSystem.SelectedIndex == 1)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 61600;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 73920;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 80080;
                        }
                    }
                }
                else if (trackBar.Value > 750 && trackBar.Value <= 900)
                {
                    if (rdGpTaxSystem.SelectedIndex == 0)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 82900;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 99480;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 107770;
                        }
                    }
                    else if (rdGpTaxSystem.SelectedIndex == 1)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 70600;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 84720;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 91780;
                        }
                    }
                }
                else if (trackBar.Value > 900 && trackBar.Value <= 1100)
                {
                    if (rdGpTaxSystem.SelectedIndex == 0)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 98800;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 118560;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 128440;
                        }
                    }
                    else if (rdGpTaxSystem.SelectedIndex == 1)
                    {
                        if (rdGpTariff.SelectedIndex == 0)
                        {
                            Price = 84000;
                        }
                        else if (rdGpTariff.SelectedIndex == 1)
                        {
                            Price = 100800;
                        }
                        else if (rdGpTariff.SelectedIndex == 2)
                        {
                            Price = 109200;
                        }
                    }
                }
                else if (trackBar.Value > 1100)
                {
                    XtraMessageBox.Show("Количество первичных документов более 1100.");
                    return;
                }
            }

            Price = Price + (Price * 20 / 100);
            lblPrice.Text = Price.ToString();

            if (int.TryParse(txt1.Text, out int result1))
            {
                Price += result1 * 1300;
            }

            if (int.TryParse(txt2.Text, out int result2))
            {
                Price += result2 * 500;
            }

            if (int.TryParse(txt3.Text, out int result3))
            {
                Price += result3 * 1000;
            }

            spinStaffCount_EditValueChanged(spinStaffCount, null);
        }

        private void spinDocumentCount_EditValueChanged(object sender, EventArgs e)
        {
            var spinEdit = sender as SpinEdit;

            if (spinEdit != null)
            {
                trackDocumentCount.Value = Convert.ToInt32(spinDocumentCount.Value);
            }
        }

        private void rdGpTariff_SelectedIndexChanged(object sender, EventArgs e)
        {
            trackDocumentCount_EditValueChanged(trackDocumentCount, null);
        }

        private void rdGpTaxSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            trackDocumentCount_EditValueChanged(trackDocumentCount, null);
        }

        private void checkCombinationTaxationRegimes_CheckedChanged(object sender, EventArgs e)
        {
            trackDocumentCount_EditValueChanged(trackDocumentCount, null);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var document = rdGpTariff.Properties.Items[rdGpTariff.SelectedIndex]?.Description;
            var tax = rdGpTaxSystem.Properties.Items[rdGpTaxSystem.SelectedIndex]?.Description;

            var commercialOfferObject = new CommercialOfferObject(FinalPrice.ToString(), document, tax);

            foreach (var item in gpAdditionalConditions.Controls)
            {
                if (item is CheckEdit checkEdit)
                {
                    if (checkEdit.Checked)
                    {
                        commercialOfferObject.AddTerms(checkEdit.Text);
                    }
                }
            }            
            
            var commercialOffer = new CommercialOffer(commercialOfferObject);
        }
                
        private void txtDiscount_EditValueChanged(object sender, EventArgs e)
        {
            trackDocumentCount_EditValueChanged(trackDocumentCount, null);
        }

        private void txt1_EditValueChanged(object sender, EventArgs e)
        {
            trackDocumentCount_EditValueChanged(trackDocumentCount, null);
        }

        private void txt2_EditValueChanged(object sender, EventArgs e)
        {
            trackDocumentCount_EditValueChanged(trackDocumentCount, null);
        }

        private void txt3_EditValueChanged(object sender, EventArgs e)
        {
            trackDocumentCount_EditValueChanged(trackDocumentCount, null);
        }

        private void txtWaybills_EditValueChanged(object sender, EventArgs e)
        {
            trackDocumentCount_EditValueChanged(trackDocumentCount, null);
        }

        private void txtCorrectionDeclarationsOrCalculations_EditValueChanged(object sender, EventArgs e)
        {
            trackDocumentCount_EditValueChanged(trackDocumentCount, null);
        }

        private void txtOther_EditValueChanged(object sender, EventArgs e)
        {
            trackDocumentCount_EditValueChanged(trackDocumentCount, null);
        }

        private void txtCorrectionDeclarationsOrCalculationsSum_EditValueChanged(object sender, EventArgs e)
        {
            trackDocumentCount_EditValueChanged(trackDocumentCount, null);
        }
    }
}