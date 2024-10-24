using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using PulsLibrary.Extensions.DevForm;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Reports;
using System;
using System.Linq;

namespace RMS.UI.Forms.Directories
{
    public partial class MassReportChangeEdit : formEdit_BaseSpr
    {
        private Session _session;

        public MassReportChangeEdit()
        {
            InitializeComponent();

            Icon = Properties.Resources.IconRMS;
        }

        private async void MassReportChangeEdit_Load(object sender, EventArgs e)
        {
            _session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();

            checkedListBoxTaxSystem.DataSource = await new XPQuery<TaxSystem>(_session).ToDictionaryAsync(s => s.Oid, v => v.Name);
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnReport.EditValue is Report report)
                {
                    var taxSystemList = checkedListBoxTaxSystem.CheckedItems;

                    var customerCount = 0;
                    var taxSystemCount = 0;
                    var reportCount = 0;

                    foreach (System.Collections.Generic.KeyValuePair<int, string> item in taxSystemList)
                    {
                        var taxSystem = await new XPQuery<TaxSystem>(_session)
                            .FirstOrDefaultAsync(f => f.Oid == item.Key);

                        if (taxSystem != null)
                        {
                            taxSystemCount++;
                            if (taxSystem.TaxSystemReports.FirstOrDefault(f => f.Report != null && f.Report.Oid == report.Oid) is null)
                            {
                                taxSystem.TaxSystemReports.Add(new TaxSystemReport(_session)
                                {
                                    Report = await _session.GetObjectByKeyAsync<Report>(report.Oid)
                                });
                                taxSystem.Save();
                            }

                            if (checkEditTaxSystem.Checked)
                            {
                                using (var uof = new UnitOfWork())
                                {
                                    var _customers = new XPCollection<Customer>(uof);

                                    var customers = _customers
                                        .Where(w => w.TaxSystemCustomer != null
                                            && w.TaxSystemCustomer.TaxSystem != null
                                            && w.TaxSystemCustomer.TaxSystem.Oid == taxSystem.Oid);

                                    foreach (var customer in customers)
                                    {
                                        customerCount++;

                                        if (customer.CustomerReports
                                            .Where(w => w.Report != null)
                                            .FirstOrDefault(f => f.Report.Oid == report.Oid) == null)
                                        {
                                            customer.CustomerReports.Add(new CustomerReport(uof)
                                            {
                                                Report = await uof.GetObjectByKeyAsync<Report>(report.Oid)
                                            });
                                            reportCount++;
                                        }

                                        customer.Save();
                                    }

                                    await uof.CommitTransactionAsync();
                                }
                            }
                        }
                    }

                    DevXtraMessageBox.ShowXtraMessageBox($"Обработано систем налогообложения: {taxSystemCount}{Environment.NewLine}" +
                        $"Обработано клиентов: {customerCount}{Environment.NewLine}" +
                        $"Добавлено отчетов: {reportCount}");
                    Close();
                }
                else
                {
                    DevXtraMessageBox.ShowXtraMessageBox("Для заполнения необходимо выбрать отчет", btnReport);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReport_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (sender is ButtonEdit buttonEdit)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                    return;
                }

                cls_BaseSpr.ButtonEditButtonClickBase<Report>(_session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Report, 1, null, null, false, null, string.Empty, false, true);
            }
        }
    }
}