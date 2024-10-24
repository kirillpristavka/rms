using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer.Billing;
using RMS.Core.Model.Reports;
using RMS.UI.Forms.Directories;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class TaxSystemEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private TaxSystem TaxSystem { get; }

        public TaxSystemEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                TaxSystem = new TaxSystem(Session);
            }
        }

        public TaxSystemEdit(int id) : this()
        {
            if (id > 0)
            {
                TaxSystem = Session.GetObjectByKey<TaxSystem>(id);
            }
        }

        public TaxSystemEdit(TaxSystem taxSystem) : this()
        {
            Session = taxSystem.Session;
            TaxSystem = taxSystem;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TaxSystem.Name = txtName.Text;
            TaxSystem.Description = txtDescription.Text;

            if (btnCalculationScale.EditValue is CalculationScale calculationScale)
            {
                TaxSystem.CalculationScale = calculationScale;
            }
            else
            {
                XtraMessageBox.Show($"Сохранение не возможно без выбранной шкалы",
                                    "Ошибка сохранения",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                btnCalculationScale.Focus();
                return;
            }

            Session.Save(TaxSystem);
            id = TaxSystem.Oid;
            flagSave = true;
            Close();
        }

        private void TaxSystemEdit_Load(object sender, EventArgs e)
        {
            XPBaseObject.AutoSaveOnEndEdit = false;

            txtDescription.Text = TaxSystem.Description;
            txtName.Text = TaxSystem.Name;
            btnCalculationScale.EditValue = TaxSystem.CalculationScale;

            gridControlTaxSystemReports.DataSource = TaxSystem.TaxSystemReports;

            if (gridViewTaxSystemReports.Columns[nameof(TaxSystemReport.Oid)] != null)
            {
                gridViewTaxSystemReports.Columns[nameof(TaxSystemReport.Oid)].Visible = false;
                gridViewTaxSystemReports.Columns[nameof(TaxSystemReport.Oid)].Width = 18;
                gridViewTaxSystemReports.Columns[nameof(TaxSystemReport.Oid)].OptionsColumn.FixedWidth = true;
            }
        }

        private void btnTaxSystemReportsAdd_Click(object sender, EventArgs e)
        {
            var reportOid = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.Report, -1);

            if (reportOid != -1)
            {
                var report = Session.GetObjectByKey<Report>(reportOid);
                if (report != null)
                {
                    if (TaxSystem.TaxSystemReports.FirstOrDefault(f => f.Report == report) == null)
                    {
                        TaxSystem.TaxSystemReports.Add(new TaxSystemReport(Session)
                        {
                            Report = report
                        });
                    }
                    else
                    {
                        XtraMessageBox.Show($"[Отчет]: {report} уже входит в текущую систему налогообложения.",
                        "Определена принадлежность",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnTaxSystemReportsDel_Click(object sender, EventArgs e)
        {
            if (gridViewTaxSystemReports.IsEmpty)
            {
                return;
            }

            var taxSystemReport = gridViewTaxSystemReports.GetRow(gridViewTaxSystemReports.FocusedRowHandle) as TaxSystemReport;
            taxSystemReport.Delete();
        }

        private void TaxSystemEdit_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            XPBaseObject.AutoSaveOnEndEdit = true;
        }

        private void gridViewTaxSystemReports_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            GridView gridview = sender as GridView;
            GridHitInfo gridHitInfo = gridview.CalcHitInfo(dxMouseEventArgs.Location);
            if ((gridHitInfo.InRow || gridHitInfo.InRowCell) && dxMouseEventArgs.Button == MouseButtons.Left)
            {
                var taxSystemReport = gridview.GetRow(gridview.FocusedRowHandle) as TaxSystemReport;
                if (taxSystemReport != null)
                {
                    taxSystemReport.Reload();

                    if (taxSystemReport.Report != null)
                    {
                        var form = new ReportEdit(taxSystemReport.Report);
                        form.ShowDialog();
                    }
                }
            }
        }

        private void btnCalculationScale_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<CalculationScale>(Session, buttonEdit, (int)cls_App.ReferenceBooks.CalculationScale, 1, null, null, false, null, string.Empty, false, true);
        }
    }
}