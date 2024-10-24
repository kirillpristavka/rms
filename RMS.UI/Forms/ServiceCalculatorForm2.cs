using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using PulsLibrary.Extensions.BaseObj;
using RMS.Core.Controller.Print;
using RMS.Core.Model.Calculator;
using RMS.UI.Forms.Calculator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms
{
    public partial class ServiceCalculatorForm2 : XtraForm
    {
        private UnitOfWork unitOfWork;
        private XPCollection<CalculatorIndicator> calculatorIndicators;
        private CalculatorTaxSystem currentCalculatorTaxSystem;
        private TariffScale currentTariffScale;

        /// <summary>
        /// Количество сотрудников.
        /// </summary>
        private int StaffCount
        {
            get
            {
                if (int.TryParse(spinStaffCount.Text, out int result))
                {
                    return result;
                }
                return default;
            }
        }
        
        /// <summary>
        /// Количество документов.
        /// </summary>
        private int DocumentCount
        {
            get
            {
                if (int.TryParse(spinDocumentCount.Text, out int result))
                {
                    return result;
                }
                return default;
            }
        }

        public ServiceCalculatorForm2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Заполнение RadioGroup по классу.
        /// </summary>
        /// <typeparam name="T">Класс для заполнения.</typeparam>
        /// <param name="radioGroup">Элемент выбора.</param>
        /// <returns>Коллекция объектов класса.</returns>
        private IEnumerable<T> FillRafioGroup<T>(RadioGroup radioGroup) where T : XPObject
        {
            var objName = default(string);
            if (radioGroup.EditValue is CalculatorTaxSystem calculatorTaxSystem)
            {
                objName = calculatorTaxSystem.Name;
            }

            radioGroup.SelectedIndex = -1;
            radioGroup.Properties.Items.Clear();

            var collection = new XPCollection<T>();

            var selectedIndex = 0;
            foreach (var obj in collection)
            {
                var name = obj.GetMemberValue("Name")?.ToString();
                radioGroup.Properties.Items.Add(new RadioGroupItem(obj, name));

                if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(objName) && name.Equals(objName))
                {
                    selectedIndex = radioGroup.Properties.Items.Count - 1;
                }
            }

            if (radioGroup.Properties.Items.Count > 0)
            {
                radioGroup.SelectedIndex = 0;
            }

            radioGroup.SelectedIndex = selectedIndex;
            return collection;
        }

        /// <summary>
        /// Заполнение RadioGroup из коллекции.
        /// </summary>
        /// <typeparam name="T">Класс для заполнения.</typeparam>
        /// <param name="collection">Коллекция для заполнения.</param>
        /// <param name="radioGroup">Элемент выбора.</param>
        /// <returns>Перечислитель объектов класса.</returns>
        private IEnumerable<T> FillRafioGroup<T>(IEnumerable<T> collection, RadioGroup radioGroup) where T : XPObject
        {

            var objName = default(string); 
            if (radioGroup.EditValue is TariffScale tariffScale)
            {
                objName = tariffScale.Name;
            }

            radioGroup.SelectedIndex = -1;
            radioGroup.Properties.Items.Clear();
            
            var selectedIndex = 0;
            foreach (var obj in collection)
            {
                var name = obj.GetMemberValue("Name")?.ToString();                
                radioGroup.Properties.Items.Add(new RadioGroupItem(obj, name));

                if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(objName) && name.Equals(objName))
                {
                    selectedIndex = radioGroup.Properties.Items.Count - 1;
                }
            }

            radioGroup.SelectedIndex = selectedIndex;
            return collection;
        }
        
        private void LoadForm()
        {
            unitOfWork = new UnitOfWork();
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridView);
            FillRafioGroup<CalculatorTaxSystem>(rdGpTaxSystem);

            calculatorIndicators = new XPCollection<CalculatorIndicator>(unitOfWork, new BinaryOperator(nameof(CalculatorIndicator.IsUseWhenForming), true));

            repositoryItemCheckEdit = new RepositoryItemCheckEdit();
            repositoryItemCheckEdit.CheckStyle = CheckStyles.Standard;
            repositoryItemCheckEdit.EditValueChanged += RepositoryItemCheckEdit_EditValueChanged;

            gridControl.DataSource = calculatorIndicators;
            gridView.RefreshData();


            if (gridView.Columns[nameof(CalculatorIndicator.IsUse)] != null)
            {
                gridView.Columns[nameof(CalculatorIndicator.IsUse)].Width = 25;
                gridView.Columns[nameof(CalculatorIndicator.IsUse)].OptionsColumn.FixedWidth = true;
                gridView.Columns[nameof(CalculatorIndicator.IsUse)].Caption = "...";
                gridView.Columns[nameof(CalculatorIndicator.IsUse)].ColumnEdit = repositoryItemCheckEdit;
            }

            if (gridView.Columns[nameof(CalculatorIndicator.Oid)] != null)
            {
                gridView.Columns[nameof(CalculatorIndicator.Oid)].Visible = false;
                gridView.Columns[nameof(CalculatorIndicator.Oid)].Width = 18;
                gridView.Columns[nameof(CalculatorIndicator.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(CalculatorIndicator.Name)] != null)
            {
                gridView.Columns[nameof(CalculatorIndicator.Name)].OptionsColumn.AllowEdit = false;
                gridView.Columns[nameof(CalculatorIndicator.Name)].OptionsColumn.ReadOnly = true;
            }

            if (gridView.Columns[nameof(CalculatorIndicator.TypeCalculatorIndicator)] != null)
            {
                gridView.Columns[nameof(CalculatorIndicator.TypeCalculatorIndicator)].Visible = false;
                gridView.Columns[nameof(CalculatorIndicator.TypeCalculatorIndicator)].OptionsColumn.AllowEdit = false;
                gridView.Columns[nameof(CalculatorIndicator.TypeCalculatorIndicator)].OptionsColumn.ReadOnly = true;

                gridView.Columns[nameof(CalculatorIndicator.TypeCalculatorIndicator)].Group();
                gridView.ExpandAllGroups();
            }

            if (gridView.Columns[nameof(CalculatorIndicator.Value)] != null)
            {
                gridView.Columns[nameof(CalculatorIndicator.Value)].Width = 100;
                gridView.Columns[nameof(CalculatorIndicator.Value)].OptionsColumn.FixedWidth = true;
                gridView.Columns[nameof(CalculatorIndicator.Value)].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }

            if (gridView.Columns[nameof(CalculatorIndicator.Count)] != null)
            {
                gridView.Columns[nameof(CalculatorIndicator.Count)].Width = 100;
                gridView.Columns[nameof(CalculatorIndicator.Count)].OptionsColumn.FixedWidth = true;
                gridView.Columns[nameof(CalculatorIndicator.Count)].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }

            if (gridView.Columns[nameof(CalculatorIndicator.Sum)] != null)
            {
                gridView.Columns[nameof(CalculatorIndicator.Sum)].Width = 125;
                gridView.Columns[nameof(CalculatorIndicator.Sum)].OptionsColumn.FixedWidth = true;
                gridView.Columns[nameof(CalculatorIndicator.Sum)].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }
        }
        
        private RepositoryItemCheckEdit repositoryItemCheckEdit;
        private void ServiceCalculatorForm2_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

        /// <summary>
        /// Установка значения для SpinEdit из TrackBarControl.
        /// </summary>
        private void SetEditValueSpin(SpinEdit spinEdit, TrackBarControl trackBar)
        {
            spinEdit.EditValue = trackBar.Value;
            MakeCalculation();
        }

        /// <summary>
        /// Установка значения для TrackBarControl из SpinEdit.
        /// </summary>
        private void SetEditValueTrackBar(TrackBarControl trackBar, SpinEdit spinEdit)
        {
            trackBar.Value = Convert.ToInt32(spinEdit.Value);
            MakeCalculation();
        }

        private void trackStaffCount_EditValueChanged(object sender, EventArgs e)
        {
            if (sender is TrackBarControl trackBar)
            {
                SetEditValueSpin(spinStaffCount, trackBar);
            }
        }

        private void spinStaffCount_EditValueChanged(object sender, EventArgs e)
        {
            if (sender is SpinEdit spinEdit)
            {
                SetEditValueTrackBar(trackStaffCount, spinEdit);
            }
        }

        private void trackDocumentCount_EditValueChanged(object sender, EventArgs e)
        {
            if (sender is TrackBarControl trackBar)
            {
                SetEditValueSpin(spinDocumentCount, trackBar);
            }
        }

        private void spinDocumentCount_EditValueChanged(object sender, EventArgs e)
        {
            if (sender is SpinEdit spinEdit)
            {
                SetEditValueTrackBar(trackDocumentCount, spinEdit);
            }
        }
        
        private void txtDiscount_EditValueChanged(object sender, EventArgs e)
        {
            MakeCalculation();
        }

        private void rdGpTaxSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is RadioGroup radioGroup)
            {
                if (radioGroup.EditValue is CalculatorTaxSystem calculatorTaxSystem)
                {
                    currentCalculatorTaxSystem = calculatorTaxSystem;
                    var colection = currentCalculatorTaxSystem.CalculatorTaxSystesmObj.Select(s => s.TariffScale);
                    FillRafioGroup(colection, rdGpTariffScale);
                }
            }
        }

        private void rdGpTariffScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is RadioGroup radioGroup)
            {
                if (radioGroup.EditValue is TariffScale tariffScale)
                {
                    currentTariffScale = tariffScale;
                    MakeCalculation();
                }
            }
        }

        private decimal basePrice;
        private decimal finalPrice;

        private void MakeCalculation()
        {
            basePrice = 0;
            finalPrice = 0;
            
            if (currentCalculatorTaxSystem != null && currentTariffScale != null)
            {
                basePrice = currentTariffScale.GetValueTariffScaleObj(DocumentCount);
                SetBasePrice(basePrice);

                finalPrice += currentCalculatorTaxSystem.GetValueTariffStaffObj(StaffCount);
                
                if (calculatorIndicators != null)
                {
                    foreach (var calculatorIndicator in calculatorIndicators.Where(w => w.IsUse))
                    {
                        calculatorIndicator.SetBasePrice(basePrice);
                        finalPrice += calculatorIndicator.Sum;
                    }
                }
                
                finalPrice += basePrice;
                if (int.TryParse(txtDiscount.Text, out int result) && result > 0)
                {
                    finalPrice = finalPrice - (finalPrice * result / 100);
                }
                finalPrice = finalPrice.GetRoundDecimal(0);
                finalPrice += 0.00M;
                SetFinalPrice(finalPrice);
            }
        }

        /// <summary>
        /// Установка на форму отображения базовой стоимости.
        /// </summary>
        /// <param name="value">Стоимость.</param>
        private void SetBasePrice(object value)
        {
            if (value is null)
            {
                lblBasePrice.Text = $"Базовая стоимость: 0 (руб)";
            }
            else
            {
                lblBasePrice.Text = $"Базовая стоимость: {value?.ToString().Replace(',', '.')} (руб)";
            }            
        }
        
        /// <summary>
        /// Установка на форму отображения итоговой стоимости.
        /// </summary>
        /// <param name="value">Стоимость.</param>
        private void SetFinalPrice(object value)
        {
            if (value is null)
            {
                lblFinalPrice.Text = $"Итоговая стоимость: <color=red>0</color> (руб)";
            }
            else
            {
                lblFinalPrice.Text = $"Итоговая стоимость: <color=red>{value?.ToString().Replace(',', '.')}</color> (руб)";
            }
        }

        private void btnCalculatorTaxSystem_Click(object sender, EventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.CalculatorTaxSystem, -1);

            LoadForm();
        }
        
        private void btnCalculatorCalculatorIndicator_Click(object sender, EventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.CalculatorIndicator, -1);

            LoadForm();
        }
        
        private void btnTemplate_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var commercialOfferObject = new CommercialOfferObject(finalPrice.ToString(), currentTariffScale?.ToString(), currentCalculatorTaxSystem?.ToString());

            foreach (var indicator in calculatorIndicators.Where(w => w.IsUse))
            {
                commercialOfferObject.AddTerms(indicator.Name, indicator.Count.ToString(), indicator.Value, indicator.Sum.ToString());
            }

            var commercialOffer = new CommercialOffer(commercialOfferObject);
        }

        private void gridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (sender is GridView gridView)
            {
                var columnIsUse = gridView.Columns[nameof(CalculatorIndicator.IsUse)];
                if (columnIsUse != null)
                {
                    var isUse = gridView.GetRowCellValue(e.RowHandle, columnIsUse);
                    if (isUse is bool result && result)
                    {
                        e.Appearance.BackColor = Color.LightGreen;
                    }
                }
            }
        }
        
        private void RepositoryItemCheckEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (gridView.PostEditor())
            {
                gridView.UpdateCurrentRow();
            }

            MakeCalculation();
        }

        private async void btnMassChangeValues_Click(object sender, EventArgs e)
        {
            var form = new MassChangeValuesEdit();
            form.UseTypeCalculatorIndicator();
            form.ShowDialog();

            if (form.IsSave && form.Operation != null && form.TypeCalculatorIndicator != null)
            {
                var isUseTypeCalculatorIndicator = form.IsUseTypeCalculatorIndicator;
                var isUseTariffStaffObj = form.IsUseTariffStaffObj;
                var isUseTariffScaleObj = form.IsUseTariffScaleObj;

                var typeCalculatorIndicator = form.TypeCalculatorIndicator;
                var operation = form.Operation;
                var value = form.Value;

                var message = $"Вы действительно хотите провести следующую операцию:{Environment.NewLine}" +
                        $"Операция: {operation.GetDescription()}{Environment.NewLine}" +
                        $"Значение: {value}{Environment.NewLine}";

                if (isUseTypeCalculatorIndicator)
                {
                    message += $"{Environment.NewLine}Произвести изменение значений у операций следующего типа: {typeCalculatorIndicator.GetDescription()}{Environment.NewLine}";
                }

                if (isUseTariffStaffObj)
                {
                    message += $"{Environment.NewLine}Произвести изменение значений шкал цен для сотрудников{Environment.NewLine}";
                }

                if (isUseTariffScaleObj)
                {
                    message += $"{Environment.NewLine}Произвести изменение значений тарифных сеток{Environment.NewLine}";
                }

                if (XtraMessageBox.Show(message,
                    "Информационное сообщение",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
                {

                    var outMessage = $"Произведена обработка в следующем объеме:{Environment.NewLine}";

                    if (isUseTypeCalculatorIndicator)
                    {
                        var count = 0;
                        var objCollection = await new XPQuery<CalculatorIndicator>(DatabaseConnection.GetWorkSession())
                            .Where(w => w.TypeCalculatorIndicator == typeCalculatorIndicator)
                            .ToListAsync();

                        foreach (var obj in objCollection)
                        {
                            var objValue = obj.GetDecimalValue();
                            switch (operation)
                            {
                                case Operation.Addition:
                                    objValue += value;
                                    break;

                                case Operation.Subtraction:
                                    objValue -= value;
                                    break;

                                case Operation.Multiplication:
                                    objValue *= value;
                                    break;

                                case Operation.Division:
                                    objValue /= value;
                                    break;
                            }

                            obj.Value = objValue.GetRoundDecimalToString();
                            obj.Save();
                            count++;
                        }
                        outMessage += $"Изменено значений показателей: {count}{Environment.NewLine}";
                    }

                    if (isUseTariffScaleObj)
                    {
                        var count = 0;
                        var objCollection = await new XPQuery<TariffScaleObj>(DatabaseConnection.GetWorkSession())
                            .ToListAsync();

                        foreach (var obj in objCollection)
                        {
                            var objValue = obj.Value;
                            switch (operation)
                            {
                                case Operation.Addition:
                                    objValue += value;
                                    break;

                                case Operation.Subtraction:
                                    objValue -= value;
                                    break;

                                case Operation.Multiplication:
                                    objValue *= value;
                                    break;

                                case Operation.Division:
                                    objValue /= value;
                                    break;
                            }

                            obj.Value = objValue.GetRoundDecimal();
                            obj.Save();
                            count++;
                        }
                        outMessage += $"Изменено значений тарифных сеток: {count}{Environment.NewLine}";
                    }

                    if (isUseTariffStaffObj)
                    {
                        var count = 0;
                        var objCollection = await new XPQuery<TariffStaffObj>(DatabaseConnection.GetWorkSession())
                            .ToListAsync();

                        foreach (var obj in objCollection)
                        {
                            var objValue = obj.Value;
                            switch (operation)
                            {
                                case Operation.Addition:
                                    objValue += value;
                                    break;

                                case Operation.Subtraction:
                                    objValue -= value;
                                    break;

                                case Operation.Multiplication:
                                    objValue *= value;
                                    break;

                                case Operation.Division:
                                    objValue /= value;
                                    break;
                            }

                            obj.Value = objValue.GetRoundDecimal();
                            obj.Save();
                            count++;
                        }
                        outMessage += $"Изменено значений шкал сотрудников: {count}{Environment.NewLine}";
                    }

                    XtraMessageBox.Show(outMessage,
                                        "Информационное сообщение",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                }
            }
            else
            {
                //XtraMessageBox.Show("Не удалось найти один из параметров для обработки. Обратитесь к администратору.",
                //                    "Информационное сообщение",
                //                    MessageBoxButtons.OK,
                //                    MessageBoxIcon.Warning);
            }

            LoadForm();
        }

        private void gridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (gridView.GetRow(e.RowHandle) is CalculatorIndicator calculatorIndicator)
                {
                    if (calculatorIndicator.IsUse is true)
                    {
                        MakeCalculation();
                    }
                }
            }
        }
    }
}