using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using RMS.Core.Model;
using RMS.Core.Model.Reports;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class FormCorporationEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private FormCorporation FormCorporation { get; }

        public FormCorporationEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                FormCorporation = new FormCorporation(Session);
            }
        }

        public FormCorporationEdit(int id) : this()
        {
            if (id > 0)
            {
                FormCorporation = Session.GetObjectByKey<FormCorporation>(id);
            }
        }

        public FormCorporationEdit(FormCorporation formCorporation) : this()
        {
            Session = formCorporation.Session;
            FormCorporation = formCorporation;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FormCorporation.Kod = txtKod.Text;
            FormCorporation.AbbreviatedName = txtAbbreviatedName.Text;
            FormCorporation.FullName = txtFullName.Text;
            FormCorporation.IsUseFormingIndividualEntrepreneursTax = checkIsUseFormingIndividualEntrepreneursTax.Checked;
            FormCorporation.IsUseFormingPreTax = checkIsUseFormingPreTax.Checked;

            Session.Save(FormCorporation);
            id = FormCorporation.Oid;
            flagSave = true;

            FormCorporation?.Reload();
            
            Close();
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            txtAbbreviatedName.Text = FormCorporation.AbbreviatedName;
            txtKod.Text = FormCorporation.Kod;
            txtFullName.Text = FormCorporation.FullName;
            checkIsUseFormingIndividualEntrepreneursTax.EditValue = FormCorporation.IsUseFormingIndividualEntrepreneursTax;
            checkIsUseFormingPreTax.EditValue = FormCorporation.IsUseFormingPreTax;

            gridControl.DataSource = FormCorporation.FormCorporationReports;

            if (gridView.Columns[nameof(FormCorporationReport.Oid)] != null)
            {
                gridView.Columns[nameof(FormCorporationReport.Oid)].Visible = false;
                gridView.Columns[nameof(FormCorporationReport.Oid)].Width = 18;
                gridView.Columns[nameof(FormCorporationReport.Oid)].OptionsColumn.FixedWidth = true;
            }
        }

        private void barBtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var reportOid = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.Report, -1);

            if (reportOid != -1)
            {
                var report = Session.GetObjectByKey<Report>(reportOid);
                if (report != null)
                {
                    if (FormCorporation.FormCorporationReports.FirstOrDefault(f => f.Report == report) == null)
                    {
                        FormCorporation.FormCorporationReports.Add(new FormCorporationReport(Session)
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

        private void barBtnDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var obj = gridView.GetRow(gridView.FocusedRowHandle) as FormCorporationReport;
            obj.Delete();
        }

        private void gridViewTaxSystemReports_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (e.MenuType != GridMenuType.Row)
                    {                        
                        barBtnDel.Enabled = false;
                    }
                    else
                    {
                        barBtnDel.Enabled = true;
                    }
                    
                    popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }
    }
}