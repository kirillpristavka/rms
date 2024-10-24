using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using PulsLibrary.Extensions.DevForm;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model.Calculator;
using System;
using System.Windows.Forms;

namespace RMS.UI.Forms.Calculator
{
    public partial class MassChangeValuesEdit : XtraForm
    {
        public bool IsSave { get; private set; }

        public decimal Value
        {
            get
            {
                if (decimal.TryParse(spinValue.Text, out decimal result))
                {
                    return result;
                }

                return default;
            }
        }

        public Operation? Operation
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(cmbOperation.Text))
                {
                    return cmbOperation.GetEnumItem<Operation>();
                }

                return default;
            }
        }

        public bool IsUseTypeCalculatorIndicator
        {
            get
            {
                return checkIsUseTypeCalculatorIndicator.Checked;
            }
        }

        public bool IsUseTariffStaffObj
        {
            get
            {
                return checkIsUseTariffStaffObj.Checked;
            }
        }
        public bool IsUseTariffScaleObj
        {
            get
            {
                return checkIsUseTariffScaleObj.Checked;
            }
        }

        public TypeCalculatorIndicator? TypeCalculatorIndicator
        {
            get
            {
                if (IsUseTypeCalculatorIndicator && cmbTypeCalculatorIndicator.EditValue != null)
                {
                    foreach (TypeCalculatorIndicator typeCalculatorIndicator in Enum.GetValues(typeof(TypeCalculatorIndicator)))
                    {
                        if (typeCalculatorIndicator.GetEnumDescription().Equals(cmbTypeCalculatorIndicator.Text))
                        {
                            return typeCalculatorIndicator;
                        }
                    }
                }

                return default;
            }
        }

        public MassChangeValuesEdit()
        {
            InitializeComponent();            
        }

        public void UseTypeCalculatorIndicator()
        {
            layoutControlItemIsUseTypeCalculatorIndicator.Visibility = LayoutVisibility.Always;
            layoutControlItemTypeCalculatorIndicator.Visibility = LayoutVisibility.Always;
            foreach (TypeCalculatorIndicator item in Enum.GetValues(typeof(TypeCalculatorIndicator)))
            {
                cmbTypeCalculatorIndicator.Properties.Items.Add(item.GetEnumDescription());
            }

            layoutControlItemIsUseTariffScaleObj.Visibility = LayoutVisibility.Always;
            layoutControlItemIsUseTariffStaffObj.Visibility = LayoutVisibility.Always;
        }

        private void MassChangeValuesEdit_Load(object sender, EventArgs e)
        {
            cmbOperation.AddItemsFromEnum<Operation>();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (IsUseTypeCalculatorIndicator)
            {
                if (cmbTypeCalculatorIndicator.EditValue is null)
                {
                    XtraMessageBox.Show("Укажите значение типа операции.",
                                        "Информационное сообщение",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    cmbTypeCalculatorIndicator.Focus();
                    return;
                }
            }

            IsSave = true;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}