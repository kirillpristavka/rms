using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model.Calculator;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMS.UI.Forms.Calculator
{
    public partial class CalculatorIndicatorEdit : formEdit_BaseSpr
    {
        private UnitOfWork uof = new UnitOfWork();
        public CalculatorIndicator currentCalculatorIndicator;
        
        private TypeCalculatorIndicator typeCalculatorIndicator;

        private CalculatorIndicatorEdit()
        {
            InitializeComponent();

            foreach (TypeCalculatorIndicator item in Enum.GetValues(typeof(TypeCalculatorIndicator)))
            {
                cmbTypeCalculatorIndicator.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbTypeCalculatorIndicator.SelectedIndex = 0;
        }

        public CalculatorIndicatorEdit(int id) : this()
        {
            if (id > 0)
            {
                currentCalculatorIndicator = uof.GetObjectByKey<CalculatorIndicator>(id);
            }
        }

        public CalculatorIndicatorEdit(CalculatorIndicator calculatorIndicator) : this()
        {
            currentCalculatorIndicator = uof.GetObjectByKey<CalculatorIndicator>(calculatorIndicator.Oid);
        }

        private void FormLoad(object sender, EventArgs e)
        {
            if (currentCalculatorIndicator is null)
            {
                currentCalculatorIndicator = new CalculatorIndicator(uof);
            }
            
            txtName.EditValue = currentCalculatorIndicator.Name;
            memoDescription.EditValue = currentCalculatorIndicator.Description;
            checkIsUseWhenForming.Checked = currentCalculatorIndicator.IsUseWhenForming;                        
            cmbTypeCalculatorIndicator.SelectedIndex = (int)currentCalculatorIndicator.TypeCalculatorIndicator;
            txtValue.EditValue = currentCalculatorIndicator.Value;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (await Save())
            {
                id = currentCalculatorIndicator.Oid;
                flagSave = true;
                Close();
            }
        }

        /// <summary>
        /// Сохранение отчета.
        /// </summary>
        private async Task<bool> Save()
        {
            if (currentCalculatorIndicator.Oid <= 0)
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    XtraMessageBox.Show($"Сохранение без наименования не возможно.",
                        "Ошибка сохранения",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    txtName.Focus();
                    return false;
                }
            }

            if (currentCalculatorIndicator.Name != txtName.Text)
            {
                if (uof.FindObject<CalculatorIndicator>(new BinaryOperator(nameof(currentCalculatorIndicator.Name), txtName.Text)) != null && !string.IsNullOrWhiteSpace(txtName.Text))
                {
                    XtraMessageBox.Show($"Показатель: {txtName.Text} уже существует в системе.",
                        "Ошибка сохранения",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
            }
            
            currentCalculatorIndicator.Name = txtName.Text;
            currentCalculatorIndicator.Description = memoDescription.Text;
            currentCalculatorIndicator.IsUseWhenForming = checkIsUseWhenForming.Checked;

            if (cmbTypeCalculatorIndicator.EditValue is null || cmbTypeCalculatorIndicator.SelectedIndex == -1)
            {
                XtraMessageBox.Show($"Без выбора типа показателя сохранение не возможно.",
                        "Ошибка сохранения",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                cmbTypeCalculatorIndicator.Focus();
                return false;
            }
            else
            {
                currentCalculatorIndicator.TypeCalculatorIndicator = typeCalculatorIndicator;
            }

            currentCalculatorIndicator.Value = txtValue.Text;            
            currentCalculatorIndicator.Save();

            await uof.CommitTransactionAsync();
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbTypePerformanceIndicator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBoxEdit comboBoxEdit)
            {
                foreach (TypeCalculatorIndicator typeCalculatorIndicator in Enum.GetValues(typeof(TypeCalculatorIndicator)))
                {
                    if (typeCalculatorIndicator.GetEnumDescription().Equals(comboBoxEdit.Text))
                    {
                        this.typeCalculatorIndicator = typeCalculatorIndicator;
                        break;
                    }
                }
            }
        }        
    }
}