using DevExpress.Data.Filtering;
using DevExpress.DataProcessing;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using RMS.Core.Enumerator;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.InfoCustomer.Billing;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms.Directories
{
    public partial class BillingInformationEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        private BillingInformation BillingInformation { get; }

        private BillingInformationEdit()
        {
            InitializeComponent();
        }

        public BillingInformationEdit(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            BillingInformation = customer.BillingInformation ?? new BillingInformation(Session);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnCustomer.EditValue is Customer customer)
            {
                
            }
            else
            {
                XtraMessageBox.Show($"Сохранение не возможно без выбранного клиента", 
                                    "Ошибка сохранения",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                btnCustomer.Focus();
                return;
            }

            if (checkIsFixedBaseRate.Checked)
            {
                BillingInformation.IsFixedBaseRate = true;

                if (txtFixedBaseRateValue.EditValue is decimal fixedBaseRateValue)
                {
                    BillingInformation.FixedBaseRateValue = fixedBaseRateValue;
                }
                else
                {
                    BillingInformation.FixedBaseRateValue = 0M;
                }
                BillingInformation.IsSettlementOfTransactions = false;
                BillingInformation.CalculationScale = null;
            }
            else
            {
                BillingInformation.IsFixedBaseRate = false;
                BillingInformation.FixedBaseRateValue = null;
                
                BillingInformation.IsSettlementOfTransactions = true;

                if (btnCalculationScale.EditValue is CalculationScale calculationScale)
                {
                    BillingInformation.CalculationScale = calculationScale;
                }
                else
                {
                    BillingInformation.CalculationScale = null;
                }
            }

            if (checkIsPreparationPrimaryDocuments.Checked)
            {
                BillingInformation.IsPreparationPrimaryDocuments = true;
                
                if(txtPreparationPrimaryDocumentsValue.EditValue is decimal preparationPrimaryDocumentsValue)
                {
                    BillingInformation.PreparationPrimaryDocumentsValue = preparationPrimaryDocumentsValue;
                }
                else
                {
                    BillingInformation.PreparationPrimaryDocumentsValue = 0M;
                }
            }
            else
            {
                BillingInformation.IsPreparationPrimaryDocuments = false;
                BillingInformation.PreparationPrimaryDocumentsValue = null;
            }

            if (checkIsCustomerBankService.Checked)
            {
                BillingInformation.IsCustomerBankService = true;

                if (txtCustomerBankServiceValue.EditValue is decimal customerBankServiceValue)
                {
                    BillingInformation.CustomerBankServiceValue = customerBankServiceValue;
                }
                else
                {
                    BillingInformation.CustomerBankServiceValue = 0M;
                }                
            }
            else
            {
                BillingInformation.IsCustomerBankService = false;
                BillingInformation.CustomerBankServiceValue = null;
            }

            if (checkIsBillingGroupPerformanceIndicators.Checked)
            {
                BillingInformation.IsBillingGroupPerformanceIndicators = true;                
            }
            else
            {
                BillingInformation.IsBillingGroupPerformanceIndicators = false;                
            }

            SaveCheckedListBox<BillingGroupPerformanceIndicator, GroupPerformanceIndicator>(BillingInformation.BillingGroupPerformanceIndicators, cmbBillingGroupPerformanceIndicators);
            Session.Save(BillingInformation.BillingPerformanceIndicators);
            BillingInformation.Save();
            
            Customer.BillingInformation = BillingInformation;
            Customer.Save();
            Close();
        }

        private void ContactEdit_Load(object sender, EventArgs e)
        {
            XPObject.AutoSaveOnEndEdit = false;
            
            btnCustomer.EditValue = Customer;

            if (BillingInformation.IsFixedBaseRate)
            {
                checkIsFixedBaseRate.Checked = BillingInformation.IsFixedBaseRate;
                txtFixedBaseRateValue.EditValue = BillingInformation.FixedBaseRateValue;
            }
            else
            {
                checkIsSettlementOfTransactions.Checked = BillingInformation.IsSettlementOfTransactions;
                btnCalculationScale.EditValue = BillingInformation.CalculationScale;
            }

            checkIsPreparationPrimaryDocuments.Checked = BillingInformation.IsPreparationPrimaryDocuments;
            txtPreparationPrimaryDocumentsValue.EditValue = BillingInformation.PreparationPrimaryDocumentsValue;

            checkIsCustomerBankService.Checked = BillingInformation.IsCustomerBankService;
            txtCustomerBankServiceValue.EditValue = BillingInformation.CustomerBankServiceValue;

            checkIsBillingGroupPerformanceIndicators.Checked = BillingInformation.IsBillingGroupPerformanceIndicators;
            FillingCheckedListBox<BillingGroupPerformanceIndicator, GroupPerformanceIndicator>(BillingInformation.BillingGroupPerformanceIndicators, cmbBillingGroupPerformanceIndicators);

            /* Здесь проверяем есть ли в настройке показатели у которых не должно быть значений и после этого удаляем их. */
            var listPerformanceIndicatorsDelete = new System.Collections.Generic.List<BillingPerformanceIndicator>();
            foreach (var billingPerformanceIndicators in BillingInformation.BillingPerformanceIndicators)
            {
                var performanceIndicator = billingPerformanceIndicators.PerformanceIndicator;
                if (performanceIndicator != null)
                {
                    performanceIndicator.Reload();

                    if (performanceIndicator.TypePerformanceIndicator == TypePerformanceIndicator.Base
                        || performanceIndicator.TypePerformanceIndicator == TypePerformanceIndicator.Analysis)
                    {
                        listPerformanceIndicatorsDelete.Add(billingPerformanceIndicators);
                    }                    
                }
            }
                        
            if (listPerformanceIndicatorsDelete.Count > 0)
            {
                var result = default(string);
                int i = 1;
                
                result += $"Были обнаружены показатели по которым значения не могут быть установлены.{Environment.NewLine}Скорее всего у них было изменено поле [Тип показателя].{Environment.NewLine}{Environment.NewLine}";
                
                foreach (var indicator in listPerformanceIndicatorsDelete)
                {
                    BillingInformation.BillingPerformanceIndicators.Remove(indicator);
                    result += $"{i}. {indicator.PerformanceIndicatorString} - значение: [{indicator.Value}]. <- Удален из коллекции.{Environment.NewLine}";
                    i++;
                }

                result += $"{Environment.NewLine}Для отмены удаления закройте окно без сохранения и исправьте тип показателей.";

                XtraMessageBox.Show(result, "Удаление записей", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            gridControl.DataSource = BillingInformation.BillingPerformanceIndicators;
            
            foreach (GridColumn column in gridView.Columns)
            {
                column.OptionsColumn.AllowEdit = false;
                column.OptionsColumn.ReadOnly = true;
            }
            
            if (gridView.Columns[nameof(BillingPerformanceIndicator.Oid)] != null)
            {
                gridView.Columns[nameof(BillingPerformanceIndicator.Oid)].Visible = false;
                gridView.Columns[nameof(BillingPerformanceIndicator.Oid)].Width = 18;
                gridView.Columns[nameof(BillingPerformanceIndicator.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(BillingPerformanceIndicator.Value)] != null)
            {
                gridView.Columns[nameof(BillingPerformanceIndicator.Value)].OptionsColumn.AllowEdit = true;
                gridView.Columns[nameof(BillingPerformanceIndicator.Value)].OptionsColumn.ReadOnly = false;
                gridView.Columns[nameof(BillingPerformanceIndicator.Value)].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }

        }
        
        /// <summary>
        /// Заполнение CheckedComboBoxEdit.
        /// </summary>
        /// <typeparam name="T1">Тип коллекции по которой будет проходить отбор.</typeparam>
        /// <typeparam name="T2">Класс использующий в коллекции отбора.</typeparam>
        /// <param name="xpCollection">Коллекция указанных значений.</param>
        /// <param name="checkedComboBoxEdit">Активный объект.</param>
        private void FillingCheckedListBox<T1, T2>(XPCollection<T1> xpCollection, CheckedComboBoxEdit checkedComboBoxEdit, string nameProperty = default)
            where T1 : XPObject
            where T2 : XPObject
        {
            if (checkedComboBoxEdit != null)
            {
                checkedComboBoxEdit.Properties.Items.Clear();

                var xpCollectionObject = new XPCollection<T2>(xpCollection.Session);
                checkedComboBoxEdit.Properties.Items.AddRange(xpCollectionObject.ToArray());

                var nameXpObject = typeof(T2).Name;

                if (!string.IsNullOrWhiteSpace(nameProperty))
                {
                    nameXpObject = nameProperty;
                }

                foreach (var item in xpCollection)
                {
                    var checkedListBoxItem = checkedComboBoxEdit.Properties.Items.FirstOrDefault(f => f.Value is T2 xpObject && xpObject == item.GetMemberValue(nameXpObject));

                    if (checkedListBoxItem != null)
                    {
                        checkedListBoxItem.CheckState = CheckState.Checked;
                    }
                }

                checkedComboBoxEdit.ButtonPressed += CheckedComboBoxEditUnchecked;
            }
        }

        /// <summary>
        /// Сохранение элементов CheckedComboBoxEdit.
        /// </summary>
        /// <typeparam name="T1">Тип коллекции по которой будет проходить отбор.</typeparam>
        /// <typeparam name="T2">Класс использующий в коллекции отбора.</typeparam>
        /// <param name="xpCollection">Коллекция искомых объектов.</param>
        /// <param name="checkedComboBoxEdit">Активный объект.</param>
        private void SaveCheckedListBox<T1, T2>(XPCollection<T1> xpCollection, CheckedComboBoxEdit checkedComboBoxEdit, string nameProperty = default)
            where T1 : XPObject, new()
            where T2 : XPObject
        {
            if (checkedComboBoxEdit != null)
            {
                var nameXpObject = typeof(T2).Name;

                if (!string.IsNullOrWhiteSpace(nameProperty))
                {
                    nameXpObject = nameProperty;
                }

                foreach (CheckedListBoxItem item in checkedComboBoxEdit.Properties.Items)
                {
                    if (item.Value is T2 xpObject)
                    {
                        var xpObjectXPCollection = xpCollection.FirstOrDefault(f => f.GetMemberValue(nameXpObject) == xpObject);
                        if (item.CheckState == CheckState.Checked)
                        {
                            if (xpObjectXPCollection == null)
                            {
                                var obj = (T1)Activator.CreateInstance(typeof(T1), xpCollection.Session);
                                obj.SetMemberValue(nameXpObject, xpObject);
                                xpCollection.Add(obj);
                            }
                        }
                        else
                        {
                            xpObjectXPCollection?.Delete();
                        }
                    }
                }
            }
        }

        private void CheckedComboBoxEditUnchecked(object sender, ButtonPressedEventArgs e)
        {
            var checkedComboBoxEdit = sender as CheckedComboBoxEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                foreach (CheckedListBoxItem checkedListBoxItemtem in checkedComboBoxEdit.Properties.Items)
                {
                    checkedListBoxItemtem.CheckState = CheckState.Unchecked;
                }
                return;
            }
        }

        private void gridView_MouseDown(object sender, MouseEventArgs e)
        {
            DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            GridView gridview = sender as GridView;
            _ = gridview.CalcHitInfo(dxMouseEventArgs.Location);
            //if ((gridHitInfo.InRow || gridHitInfo.InRowCell) && dxMouseEventArgs.Button == MouseButtons.Right)
            if (dxMouseEventArgs.Button == MouseButtons.Right)
            {
                popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private async void barBtnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            var groupOperator = new GroupOperator(GroupOperatorType.And);
            
            var criteriaPerformanceIndicatorBase = new BinaryOperator(nameof(PerformanceIndicator.TypePerformanceIndicator), TypePerformanceIndicator.Base, BinaryOperatorType.NotEqual);
            groupOperator.Operands.Add(criteriaPerformanceIndicatorBase);
            
            var criteriaPerformanceIndicatorAnalysis = new BinaryOperator(nameof(PerformanceIndicator.TypePerformanceIndicator), TypePerformanceIndicator.Analysis, BinaryOperatorType.NotEqual);
            groupOperator.Operands.Add(criteriaPerformanceIndicatorAnalysis);

            var performanceIndicatorOid = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.PerformanceIndicator, criteria: groupOperator);

            if (performanceIndicatorOid > 0)
            {
                var performanceIndicator = await Session.GetObjectByKeyAsync<PerformanceIndicator>(performanceIndicatorOid);

                if (performanceIndicator != null)
                {
                    if (BillingInformation.BillingPerformanceIndicators.FirstOrDefault(f => f.Oid == performanceIndicator.Oid) == null)
                    {
                        BillingInformation.BillingPerformanceIndicators.Add(new BillingPerformanceIndicator(Session)
                        {
                            PerformanceIndicator = performanceIndicator,
                            Value = 0
                        });
                    }
                }
            }
        }

        private void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var billingPerformanceIndicator = gridView.GetRow(gridView.FocusedRowHandle) as BillingPerformanceIndicator;
            if (billingPerformanceIndicator != null)
            {
                BillingInformation.BillingPerformanceIndicators.Remove(billingPerformanceIndicator);
            }
        }

        private void BillingInformationEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            BillingInformation.Reload();
            BillingInformation.BillingPerformanceIndicators.Reload();
            foreach (var billingPerformanceIndicator in BillingInformation.BillingPerformanceIndicators)
            {
                billingPerformanceIndicator.Reload();
            }
            BillingInformation.BillingGroupPerformanceIndicators.Reload();
            foreach (var billingGroupPerformanceIndicators in BillingInformation.BillingGroupPerformanceIndicators)
            {
                billingGroupPerformanceIndicators.Reload();
            }
            XPObject.AutoSaveOnEndEdit = true;
        }

        private void checkIsFixedBaseRate_CheckedChanged(object sender, EventArgs e)
        {
            var checkEdit = sender as CheckEdit;
            if (checkEdit != null && checkEdit.Checked)
            {
                txtFixedBaseRateValue.Enabled = true;
                checkIsSettlementOfTransactions.Checked = false;

                btnCalculationScale.EditValue = null;
            }
            else
            {
                txtFixedBaseRateValue.Enabled = false;
                checkIsSettlementOfTransactions.Checked = true;
            }
        }

        private void checkIsSettlementOfTransactions_CheckedChanged(object sender, EventArgs e)
        {
            var checkEdit = sender as CheckEdit;
            if (checkEdit != null && checkEdit.Checked)
            {
                btnCalculationScale.Enabled = true;
                checkIsFixedBaseRate.Checked = false;

                txtFixedBaseRateValue.EditValue = null;
            }
            else
            {
                btnCalculationScale.Enabled = false;                
                checkIsFixedBaseRate.Checked = true;
            }
        }

        private void checkIsPreparationPrimaryDocuments_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckEdit checkEdit && checkEdit.Checked)
            {
                txtPreparationPrimaryDocumentsValue.Enabled = true;

                if (BillingInformation.PreparationPrimaryDocumentsValue is null)
                {
                    txtPreparationPrimaryDocumentsValue.EditValue = Convert.ToDecimal(20);
                }
                else
                {
                    txtPreparationPrimaryDocumentsValue.EditValue = BillingInformation.PreparationPrimaryDocumentsValue;
                }
            }
            else
            {
                txtPreparationPrimaryDocumentsValue.Enabled = false;
                txtPreparationPrimaryDocumentsValue.EditValue = null;
            }
        }

        private void checkIsCustomerBankService_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckEdit checkEdit && checkEdit.Checked)
            {
                txtCustomerBankServiceValue.Enabled = true;

                if (BillingInformation.CustomerBankServiceValue is null)
                {
                    txtCustomerBankServiceValue.EditValue = Convert.ToDecimal(30);
                }
                else
                {
                    txtCustomerBankServiceValue.EditValue = BillingInformation.CustomerBankServiceValue;
                }  
            }
            else
            {
                txtCustomerBankServiceValue.Enabled = false;
                txtCustomerBankServiceValue.EditValue = null;
            }
        }

        private void checkIsBillingGroupPerformanceIndicators_CheckedChanged(object sender, EventArgs e)
        {
            var checkEdit = sender as CheckEdit;
            if (checkEdit != null && checkEdit.Checked)
            {
                cmbBillingGroupPerformanceIndicators.Enabled = true;
            }
            else
            {
                cmbBillingGroupPerformanceIndicators.Enabled = false;
                
                foreach (CheckedListBoxItem checkedListBoxItemtem in cmbBillingGroupPerformanceIndicators.Properties.Items)
                {
                    checkedListBoxItemtem.CheckState = CheckState.Unchecked;
                }
            }
        }

        private void btnCalculationScale_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<CalculationScale>(Session, buttonEdit, (int)cls_App.ReferenceBooks.CalculationScale, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }
    }
}