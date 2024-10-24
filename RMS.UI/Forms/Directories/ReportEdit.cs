using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.Reports;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms.Directories
{
    public partial class ReportEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        public Report Report { get; }
        private XPCollection<TaxSystem> XpCollectionTaxSystem { get; set; }

        public ReportEdit()
        {
            InitializeComponent();

            foreach (Periodicity item in Enum.GetValues(typeof(Periodicity)))
            {
                cmbPeriodicity.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbPeriodicity.SelectedIndex = 0;

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                Report = new Report(Session);
            }
        }

        public ReportEdit(int id) : this()
        {
            if (id > 0)
            {
                Report = Session.GetObjectByKey<Report>(id);
            }
        }

        public ReportEdit(Report report) : this()
        {
            Session = report.Session;
            Report = report;
        }

        public ReportEdit(CustomerReport customerReport) : this()
        {
            Session = customerReport.Session;
            Report = customerReport.Report;
        }

        private void ReportEdit_Load(object sender, EventArgs e)
        {
            XPBaseObject.AutoSaveOnEndEdit = false;

            txtFormIndex.EditValue = Report.FormIndex;
            txtOKUD.EditValue = Report.OKUD;
            memoName.EditValue = Report.Name;
            cmbPeriodicity.SelectedIndex = Convert.ToInt32(Report.Periodicity);
            memoDeadline.EditValue = Report.Deadline;
            memoComment.EditValue = Report.Comment;
            checkIsInputTax.EditValue = Report.IsInputTax;

            dateDateTo.EditValue = Report.DateTo;

            checkIsReportAnnual.EditValue = Report.IsReportAnnual;
            checkIsReportSemiAnnual.EditValue = Report.IsReportSemiAnnual;
            checkIsReportQuarterly.EditValue = Report.IsReportQuarterly;
            checkIsReportMonthly.EditValue = Report.IsReportMonthly;

            checkIsReportSubmissionMonthInSameMonthAsCreation.EditValue = Report.IsReportSubmissionMonthInSameMonthAsCreation;

            gridControlReportSchedule.DataSource = Report.ReportSchedules;
            if (gridViewReportSchedule.Columns[nameof(ReportSchedule.Oid)] != null)
            {
                gridViewReportSchedule.Columns[nameof(ReportSchedule.Oid)].Visible = false;
                gridViewReportSchedule.Columns[nameof(ReportSchedule.Oid)].Width = 18;
                gridViewReportSchedule.Columns[nameof(ReportSchedule.Oid)].OptionsColumn.FixedWidth = true;
            }

            var criteriaReport = new ContainsOperator(nameof(TaxSystem.TaxSystemReports), new BinaryOperator(nameof(TaxSystemReport.Report), Report));
            XpCollectionTaxSystem = new XPCollection<TaxSystem>(Session, criteriaReport);
            gridControlTaxSystem.DataSource = XpCollectionTaxSystem;

            if (gridViewTaxSystem.Columns[nameof(TaxSystem.Oid)] != null)
            {
                gridViewTaxSystem.Columns[nameof(TaxSystem.Oid)].Visible = false;
                gridViewTaxSystem.Columns[nameof(TaxSystem.Oid)].Width = 18;
                gridViewTaxSystem.Columns[nameof(TaxSystem.Oid)].OptionsColumn.FixedWidth = true;
            }

            gridControlPerformanceIndicator.DataSource = Report.ReportPerformanceIndicators;
            if (gridViewPerformanceIndicator.Columns[nameof(ReportPerformanceIndicator.Oid)] != null)
            {
                gridViewPerformanceIndicator.Columns[nameof(ReportPerformanceIndicator.Oid)].Visible = false;
                gridViewPerformanceIndicator.Columns[nameof(ReportPerformanceIndicator.Oid)].Width = 18;
                gridViewPerformanceIndicator.Columns[nameof(ReportPerformanceIndicator.Oid)].OptionsColumn.FixedWidth = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveReport())
            {
                id = Report.Oid;
                flagSave = true;
                Close();
            }
        }

        /// <summary>
        /// Сохранение отчета.
        /// </summary>
        private bool SaveReport()
        {
            if (Report.Oid <= 0)
            {
                if (string.IsNullOrWhiteSpace(memoName.Text))
                {
                    XtraMessageBox.Show($"Сохранение без наименования не возможно.",
                        "Ошибка сохранения",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    memoName.Focus();
                    return false;
                }
            }

            if (Report.FormIndex != txtFormIndex.Text)
            {
                if (Session.FindObject<Report>(new BinaryOperator(nameof(Report.FormIndex), txtFormIndex.Text)) != null && !string.IsNullOrWhiteSpace(txtFormIndex.Text))
                {
                    XtraMessageBox.Show($"Отчет с индексом формы: {txtFormIndex.Text} уже существует в системе.",
                        "Ошибка сохранения",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
            }

            if (Report.OKUD != txtOKUD.Text)
            {
                if (Session.FindObject<Report>(new BinaryOperator(nameof(Report.OKUD), txtOKUD.Text)) != null && !string.IsNullOrWhiteSpace(txtOKUD.Text))
                {
                    XtraMessageBox.Show($"Отчет с ОКУД: {txtOKUD.Text} уже существует в системе.",
                        "Ошибка сохранения",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
            }

            Report.IsInputTax = checkIsInputTax.Checked;
            Report.FormIndex = txtFormIndex.Text;
            Report.OKUD = txtOKUD.Text;
            Report.Name = memoName.Text;
            Report.Periodicity = (Periodicity)cmbPeriodicity.SelectedIndex;
            Report.Deadline = memoDeadline.Text;
            Report.Comment = memoComment.Text;

            Report.IsReportAnnual = checkIsReportAnnual.Checked;
            Report.IsReportSemiAnnual = checkIsReportSemiAnnual.Checked;
            Report.IsReportQuarterly = checkIsReportQuarterly.Checked;
            Report.IsReportMonthly = checkIsReportMonthly.Checked;

            Report.IsReportSubmissionMonthInSameMonthAsCreation = checkIsReportSubmissionMonthInSameMonthAsCreation.Checked;

            if (dateDateTo.EditValue is DateTime dateTo)
            {
                Report.DateTo = dateTo;
            }
            else
            {
                Report.DateTo = null;
            }

            Session.Save(Report);

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReportScheduleAdd_Click(object sender, EventArgs e)
        {
            Report.ReportSchedules.Add(new ReportSchedule(Session));
        }

        private void btnReportScheduleDel_Click(object sender, EventArgs e)
        {
            if (gridViewReportSchedule.IsEmpty)
            {
                return;
            }

            var reportSchedule = gridViewReportSchedule.GetRow(gridViewReportSchedule.FocusedRowHandle) as ReportSchedule;
            reportSchedule.Delete();
        }

        private void ReportEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            XPBaseObject.AutoSaveOnEndEdit = true;
        }

        private void btnAddTaxSystem_Click(object sender, EventArgs e)
        {
            var isSave = true;

            if (Report.Oid <= 0)
            {
                if (XtraMessageBox.Show($"Перед заполнением списка Систем налогообложения необходимо сохранить отчет. Продолжить?.",
                        "Сохранение отчета",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Information) == DialogResult.OK)
                {
                    isSave = SaveReport();
                }
                else
                {
                    isSave = false;
                }

                if (isSave == false)
                {
                    return;
                }
            }

            var id = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.TaxSystem);

            if (id > 0)
            {
                var taxSystem = Session.GetObjectByKey<TaxSystem>(id);

                if (taxSystem != null)
                {
                    if (taxSystem.TaxSystemReports.FirstOrDefault(f => f.Report == Report) == null)
                    {
                        if (isSave)
                        {
                            taxSystem.TaxSystemReports.Add(new TaxSystemReport(Session)
                            {
                                Report = Report
                            });
                            taxSystem.Save();
                        }
                    }
                }
            }

            XpCollectionTaxSystem.Reload();
        }

        private void btnDelTaxSystem_Click(object sender, EventArgs e)
        {
            if (gridViewTaxSystem.IsEmpty)
            {
                return;
            }

            var taxSystem = gridViewTaxSystem.GetRow(gridViewTaxSystem.FocusedRowHandle) as TaxSystem;

            if (XtraMessageBox.Show($"Вы действительно хотите удалить привязку отчета {Report} из системы налогообложения {taxSystem}?",
                        "Удаление отчета",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (taxSystem != null)
                {
                    var taxSystemReports = taxSystem.TaxSystemReports.FirstOrDefault(f => f.Report == Report);
                    if (taxSystemReports != null)
                    {
                        taxSystemReports.Delete();
                        taxSystem.Save();

                        XtraMessageBox.Show($"Отчет {Report} успешно удален из системы налогообложения {taxSystem}?",
                        "Удаление отчета",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                }
            }

            XpCollectionTaxSystem.Reload();
        }

        private void checkIsInputTax_CheckedChanged(object sender, EventArgs e)
        {
            var checkEdit = sender as CheckEdit;

            if (checkEdit != null && checkEdit.Checked)
            {
                checkIsReportAnnual.Visible = true;
                checkIsReportSemiAnnual.Visible = true;
                checkIsReportQuarterly.Visible = true;
                checkIsReportMonthly.Visible = true;
                
                //checkIsReportAnnual.Checked = true;
                //checkIsReportSemiAnnual.Checked = true;
                //checkIsReportQuarterly.Checked = true;
                //checkIsReportMonthly.Checked = true;
            }
            else
            {
                checkIsReportAnnual.Visible = false;
                checkIsReportAnnual.Checked = false;
                checkIsReportSemiAnnual.Visible = false;
                checkIsReportSemiAnnual.Checked = false;
                checkIsReportQuarterly.Visible = false;
                checkIsReportQuarterly.Checked = false;
                checkIsReportMonthly.Visible = false;
                checkIsReportMonthly.Checked = false;
            }
        }

        private void btnAddPerformanceIndicator_Click(object sender, EventArgs e)
        {
            var isSave = true;

            if (Report.Oid <= 0)
            {
                if (XtraMessageBox.Show($"Перед заполнением списка Показателей необходимо сохранить отчет. Продолжить?.",
                        "Сохранение отчета",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Information) == DialogResult.OK)
                {
                    isSave = SaveReport();
                }
                else
                {
                    isSave = false;
                }

                if (isSave == false)
                {
                    return;
                }
            }

            var id = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.PerformanceIndicator);

            if (id > 0)
            {
                var obj = Session.GetObjectByKey<PerformanceIndicator>(id);

                if (obj != null)
                {
                    if (Report.ReportPerformanceIndicators.FirstOrDefault(f => f.PerformanceIndicator != null && f.PerformanceIndicator.Oid == obj.Oid) is null)
                    {
                        Report.ReportPerformanceIndicators.Add(new ReportPerformanceIndicator(Session) 
                        { 
                            PerformanceIndicator = obj 
                        });
                    }
                }
            }
        }

        private void btnDelPerformanceIndicator_Click(object sender, EventArgs e)
        {
            if (gridViewPerformanceIndicator.IsEmpty)
            {
                return;
            }

            var obj = gridViewPerformanceIndicator.GetRow(gridViewPerformanceIndicator.FocusedRowHandle) as ReportPerformanceIndicator;
            if (obj != null)
            {
                obj.Delete();
            }
        }
    }
}