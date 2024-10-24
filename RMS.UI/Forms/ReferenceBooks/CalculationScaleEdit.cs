using DevExpress.Xpo;
using DevExpress.XtraGrid.Columns;
using RMS.Core.Model.InfoCustomer.Billing;
using System;
using System.Windows.Forms;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class CalculationScaleEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private CalculationScale CalculationScale { get; }

        public CalculationScaleEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                CalculationScale = new CalculationScale(Session);
            }
        }

        public CalculationScaleEdit(int id) : this()
        {
            if (id > 0)
            {
                CalculationScale = Session.GetObjectByKey<CalculationScale>(id);
            }
        }

        public CalculationScaleEdit(CalculationScale calculationScale) : this()
        {
            Session = calculationScale.Session;
            CalculationScale = calculationScale;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CalculationScale.Name = txtName.Text;
            CalculationScale.Description = txtDescription.Text;

            Session.Save(CalculationScale);
            id = CalculationScale.Oid;
            flagSave = true;
            Close();
        }

        private void TaxSystemEdit_Load(object sender, EventArgs e)
        {
            XPBaseObject.AutoSaveOnEndEdit = false;

            txtDescription.Text = CalculationScale.Description;
            txtName.Text = CalculationScale.Name;

            gridControl.DataSource = CalculationScale.CalculationScaleValues;
                        
            if (gridView.Columns[nameof(CalculationScale.Oid)] != null)
            {
                gridView.Columns[nameof(CalculationScale.Oid)].Visible = false;
                gridView.Columns[nameof(CalculationScale.Oid)].Width = 18;
                gridView.Columns[nameof(CalculationScale.Oid)].OptionsColumn.FixedWidth = true;
                gridView.Columns[nameof(CalculationScale.Oid)].OptionsColumn.AllowEdit = false;
                gridView.Columns[nameof(CalculationScale.Oid)].OptionsColumn.ReadOnly = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CalculationScale.CalculationScaleValues.Add(new CalculationScaleValue(Session));
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var calculationScaleValue = gridView.GetRow(gridView.FocusedRowHandle) as CalculationScaleValue;
            if (calculationScaleValue != null)
            {
                CalculationScale.CalculationScaleValues.Remove(calculationScaleValue);
            }
        }

        private void CalculationScaleEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            CalculationScale.Reload();
            CalculationScale.CalculationScaleValues.Reload();

            foreach (var calculationScaleValue in CalculationScale.CalculationScaleValues)
            {
                calculationScaleValue.Reload();
            }

            XPBaseObject.AutoSaveOnEndEdit = true;
        }
    }
}