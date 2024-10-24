using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.InfoContract;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.OKVED;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Control.Customers
{
    public partial class CustomersFilterForm : formEdit_BaseSpr
    {
        private Session _session;
        public CustomerFilter CustomerFilter { get; }

        public bool IsEditVisible { get; set; } = false;
        
        public CustomersFilterForm()
        {
            InitializeComponent();

            if (_session is null)
            {
                _session = DatabaseConnection.GetWorkSession();
                CustomerFilter = new CustomerFilter(_session);
            }
        }
        private WorkZone workZone = WorkZone.ModuleCustomer;
        public CustomersFilterForm(Session session, WorkZone workZone = WorkZone.ModuleCustomer) : this()
        {
            _session = session;
            this.workZone = workZone;
            CustomerFilter = new CustomerFilter(_session);            
        }

        public CustomersFilterForm(int id) : this()
        {
            if (id > 0)
            {
                _session = DatabaseConnection.GetWorkSession();
                CustomerFilter = _session.GetObjectByKey<CustomerFilter>(id);
            }
        }

        public CustomersFilterForm(CustomerFilter customerFilter, WorkZone workZone = WorkZone.ModuleCustomer) : this()
        {
            _session = customerFilter.Session;
            CustomerFilter = customerFilter;
            this.workZone = workZone;
        }

        private void btnAccountantResponsible_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(_session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
        }

        private void btnBankResponsible_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(_session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
        }

        private void btnPrimaryResponsible_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(_session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
        }

        /// <summary>
        /// Заполнение CheckedComboBoxEdit.
        /// </summary>
        /// <typeparam name="T1">Тип коллекции по которой будет проходить отбор.</typeparam>
        /// <typeparam name="T2">Класс использующий в коллекции отбора.</typeparam>
        /// <param name="xpCollection">Коллекция указанных значений.</param>
        /// <param name="checkedComboBoxEdit">Активный объект.</param>
        private void FillingCheckedListBoxEnum<T1, T2>(XPCollection<T1> xpCollection, CheckedComboBoxEdit checkedComboBoxEdit, string nameProperty = default)
            where T1 : XPObject
            where T2 : Enum
        {
            if (checkedComboBoxEdit != null)
            {
                checkedComboBoxEdit.Properties.Items.Clear();

                foreach (T2 item in Enum.GetValues(typeof(T2)))
                {
                    checkedComboBoxEdit.Properties.Items.Add(item.GetEnumDescription());
                }
                
                var nameXpObject = typeof(T2).Name;

                if (!string.IsNullOrWhiteSpace(nameProperty))
                {
                    nameXpObject = nameProperty;
                }                

                foreach (var item in xpCollection)
                {
                    var enumObj = item.GetMemberValue(nameXpObject);

                    if (enumObj is T2 obj)
                    {
                        var checkedListBoxItem = checkedComboBoxEdit.Properties.Items.FirstOrDefault(f => f.Value.ToString().Equals(obj.GetEnumDescription()));

                        if (checkedListBoxItem != null)
                        {
                            checkedListBoxItem.CheckState = CheckState.Checked;
                        }
                    }
                }

                checkedComboBoxEdit.ButtonPressed += CheckedComboBoxEditUnchecked;
            }
        }

        /// <summary>
        /// Заполнение CheckedComboBoxEdit.
        /// </summary>
        /// <typeparam name="T1">Тип коллекции по которой будет проходить отбор.</typeparam>
        /// <typeparam name="T2">Класс использующий в коллекции отбора.</typeparam>
        /// <param name="xpCollection">Коллекция указанных значений.</param>
        /// <param name="checkedComboBoxEdit">Активный объект.</param>
        private void FillingCheckedListBox<T1, T2>(XPCollection<T1> xpCollection, CheckedComboBoxEdit checkedComboBoxEdit, string nameProperty = default, CriteriaOperator criteriaOperator = null)
            where T1 : XPObject
            where T2 : XPObject
        {
            if (checkedComboBoxEdit != null)
            {
                checkedComboBoxEdit.Properties.Items.Clear();

                var xpCollectionObject = new XPCollection<T2>(xpCollection.Session, criteriaOperator);
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
        private void SaveCheckedListBoxEnum<T1, T2>(XPCollection<T1> xpCollection, CheckedComboBoxEdit checkedComboBoxEdit, string nameProperty = default)
            where T1 : XPObject, new()
            where T2 : Enum
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
                    var enamValue = default(object);

                    foreach (T2 obj in Enum.GetValues(typeof(T2)))
                    {
                        if (obj.GetEnumDescription().Equals(item.Value))
                        {
                            enamValue = obj;
                            break;
                        }
                    }

                    if (enamValue is T2 enumCurrentObj)
                    {
                        var xpObjectXPCollection = xpCollection.FirstOrDefault(f => f.GetMemberValue(nameXpObject) is T2 enumXpObject && enumXpObject.Equals(enumCurrentObj));
                        if (item.CheckState == CheckState.Checked)
                        {
                            if (xpObjectXPCollection == null)
                            {
                                var obj = (T1)Activator.CreateInstance(typeof(T1), xpCollection.Session);
                                obj.SetMemberValue(nameXpObject, enumCurrentObj);
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

                try
                {
                    xpCollection?.Session?.Delete(xpCollection);
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
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
                    }
                }
            }
        }

        /// <summary>
        /// Заполнение RadioGroup.
        /// </summary>
        /// <param name="radioGroup"></param>
        /// <param name="criterionVariant"></param>
        private void FillingRadioGroup(RadioGroup radioGroup, CriterionVariant criterionVariant)
        {
            if (criterionVariant == CriterionVariant.And)
            {
                radioGroup.SelectedIndex = 0;
            }
            else
            {
                radioGroup.SelectedIndex = 1;
            }
        }

        /// <summary>
        /// Сохранение элемента RadioGroup.
        /// </summary>
        /// <param name="radioGroup"></param>
        /// <param name="criterionVariant"></param>
        private CriterionVariant SaveRadioGroup(RadioGroup radioGroup)
        {
            if (radioGroup.SelectedIndex == 0)
            {
                return CriterionVariant.And;
            }
            else
            {
                return CriterionVariant.Or;
            }
        }

        /// <summary>
        /// Заполнение CheckedListBoxControl.
        /// </summary>
        /// <param name="checkedListBoxControl"></param>
        /// <param name="customerFilter"></param>
        /// <param name="nameProperty"></param>
        private void FillingListBox(CheckedListBoxControl checkedListBoxControl, CustomerFilter customerFilter, string nameProperty)
        {
            try
            {
                var property = Convert.ToBoolean(customerFilter.GetMemberValue(nameProperty));
                if (property)
                {
                    var item = checkedListBoxControl.Items.FirstOrDefault(f => f.Tag.Equals(nameProperty));
                    if (item != null)
                    {
                        item.CheckState = CheckState.Checked;
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        /// <summary>
        /// Заполнение CheckedListBoxControl.
        /// </summary>
        /// <param name="checkedListBoxControl"></param>
        /// <param name="customerFilter"></param>
        /// <param name="nameProperty"></param>
        private void SaveListBox(CheckedListBoxControl checkedListBoxControl, CustomerFilter customerFilter, string nameProperty)
        {
            try
            {
                var item = checkedListBoxControl.Items.FirstOrDefault(f => f.Tag.Equals(nameProperty));
                if (item != null)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        customerFilter.SetMemberValue(nameProperty, true);
                    }
                    else
                    {
                        customerFilter.SetMemberValue(nameProperty, false);
                    }
                }                
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void CustomersFilterForm_Load(object sender, EventArgs e)
        {
            CustomerFilter?.Reload();
            CustomerFilter?.AccountantResponsible?.Reload();
            CustomerFilter?.PrimaryResponsible?.Reload();
            CustomerFilter?.BankResponsible?.Reload();
            CustomerFilter?.Status?.Reload();
            CustomerFilter?.TaxSystems?.Reload();
            CustomerFilter?.FormCorporations?.Reload();
            CustomerFilter?.ClassOKVED2?.Reload();
            CustomerFilter?.ElectronicReporting?.Reload();
            CustomerFilter?.Users?.Reload();
            CustomerFilter?.UserGroups?.Reload();
            CustomerFilter?.ContractStatus?.Reload();

            switch (workZone)
            {
                case WorkZone.ModuleCustomer:
                    xtraTabControlFilter.SelectedTabPage = xtraTabPageCustomer;
                    break;

                case WorkZone.ModuleContract:
                    xtraTabControlFilter.SelectedTabPage = xtraTabPageContract;
                    break;

                case WorkZone.ModuleTask:
                    xtraTabControlFilter.SelectedTabPage = xtraTabPageTask;
                    break;

                case WorkZone.ModuleDeal:
                    xtraTabControlFilter.SelectedTabPage = xtraTabPageDeal;
                    break;
            }

            checkIsGlobalFiltering.EditValue = CustomerFilter.IsGlobalFiltering;
            
            txtNumber.EditValue = CustomerFilter.Number;
            txtDescription.Text = CustomerFilter.Description;
            txtName.Text = CustomerFilter.Name;
            
            checkIsVisibleContract.Checked = CustomerFilter.IsVisibleContract;
            checkIsVisibleCustomer.Checked = CustomerFilter.IsVisibleCustomer;
            checkIsVisibleDeal.Checked = CustomerFilter.IsVisibleDeal;
            checkIsVisibleTask.Checked = CustomerFilter.IsVisibleTask;

            FillingRadioGroup(rgCriterionVariantFilter, CustomerFilter.CriterionVariantFilter);
            
            FillingRadioGroup(rgCriterionVariantFilterCustomer, CustomerFilter.CriterionVariantFilterCustomer);
            FillingRadioGroup(rgCriterionVariantFilterContract, CustomerFilter.CriterionVariantFilterContract);

            checkIsImprest.EditValue = CustomerFilter.IsImprest;
            txtImprestDay.EditValue = CustomerFilter.ImprestDay;
            checkIsSalary.EditValue = CustomerFilter.IsSalary;
            txtSalaryDay.EditValue = CustomerFilter.SalaryDay;

            var staffDateDismissalCriteria = new NullOperator(nameof(Staff.DateDismissal));
            
            FillingCheckedListBox<CustomerFilterAccountantResponsible, Staff>(CustomerFilter.AccountantResponsible, cmbAccountantResponsible, nameof(CustomerFilterAccountantResponsible.AccountantResponsible), staffDateDismissalCriteria);
            FillingRadioGroup(rgAccountantResponsible, CustomerFilter.CriterionVariantAccountantResponsible);

            FillingCheckedListBox<CustomerFilterPrimaryResponsible, Staff>(CustomerFilter.PrimaryResponsible, cmbPrimaryResponsible, nameof(CustomerFilterPrimaryResponsible.PrimaryResponsible), staffDateDismissalCriteria);
            FillingRadioGroup(rgPrimaryResponsible, CustomerFilter.CriterionVariantPrimaryResponsible);
            
            FillingCheckedListBox<CustomerFilterBankResponsible, Staff>(CustomerFilter.BankResponsible, cmbBankResponsible, nameof(CustomerFilterBankResponsible.BankResponsible), staffDateDismissalCriteria);
            FillingRadioGroup(rgBankResponsible, CustomerFilter.CriterionVariantBankResponsible);

            FillingCheckedListBox<CustomerFilterSalaryResponsible, Staff>(CustomerFilter.SalaryResponsible, cmbSalaryResponsible, nameof(CustomerFilterSalaryResponsible.SalaryResponsible), staffDateDismissalCriteria);
            FillingRadioGroup(rgSalaryResponsible, CustomerFilter.CriterionVariantSalaryResponsible);

            FillingCheckedListBox<CustomerFilterStatus, Status>(CustomerFilter.Status, cmbStatus);
            FillingRadioGroup(rgStatus, CustomerFilter.CriterionVariantStatus);
            
            FillingCheckedListBox<CustomerFilterTaxSystem, TaxSystem>(CustomerFilter.TaxSystems, cmbTaxSystem);
            FillingRadioGroup(rgTaxSystem, CustomerFilter.CriterionVariantTaxSystems);
            
            FillingCheckedListBox<CustomerFilterFormCorporation, FormCorporation>(CustomerFilter.FormCorporations, cmbFormCorporation);
            FillingRadioGroup(rgFormCorporation, CustomerFilter.CriterionVariantFormCorporations);
            
            FillingCheckedListBox<CustomerFilterClassOKVED2, ClassOKVED2>(CustomerFilter.ClassOKVED2, cmbClassOKVED2);
            FillingRadioGroup(rgClassOKVED2, CustomerFilter.CriterionVariantClassOKVED2);

            FillingCheckedListBox<CustomerFilterElectronicReporting, ElectronicReporting>(CustomerFilter.ElectronicReporting, cmbElectronicReporting);
            FillingRadioGroup(rgElectronicReporting, CustomerFilter.CriterionVariantElectronicReporting);

            FillingListBox(listTaxes, CustomerFilter, nameof(CustomerFilter.IsTaxAlcohol));
            FillingListBox(listTaxes, CustomerFilter, nameof(CustomerFilter.IsTaxIncome));
            FillingListBox(listTaxes, CustomerFilter, nameof(CustomerFilter.IsTaxIndirect));
            FillingListBox(listTaxes, CustomerFilter, nameof(CustomerFilter.IsTaxLand));
            FillingListBox(listTaxes, CustomerFilter, nameof(CustomerFilter.IsTaxProperty));
            FillingListBox(listTaxes, CustomerFilter, nameof(CustomerFilter.IsTaxTransport));
            FillingListBox(listTaxes, CustomerFilter, nameof(CustomerFilter.IsTaxSingleTemporaryIncome));
            FillingListBox(listTaxes, CustomerFilter, nameof(CustomerFilter.IsPatent));
            FillingRadioGroup(rgTaxes, CustomerFilter.CriterionVariantTaxes);

            FillingCheckedListBox<CustomerFilterUser, User>(CustomerFilter.Users, cmbUser);
            FillingCheckedListBox<CustomerFilterUserGroup, UserGroup>(CustomerFilter.UserGroups, cmbUserGroup);

            FillingCheckedListBox<CustomerFilterContractStatus, ContractStatus>(CustomerFilter.ContractStatus, cmbContractStatus);
            FillingRadioGroup(rgContractStatus, CustomerFilter.CriterionVariantContractStatus);

            #region Deal
            
            FillingRadioGroup(rgCriterionVariantFilterDeal, CustomerFilter.CriterionVariantFilterDeal);

            FillingCheckedListBox<CustomerFilterStatusDeal, DealStatus>(CustomerFilter.StatusDeal, cmbStatusDeal);
            FillingRadioGroup(rgStatusDeal, CustomerFilter.CriterionVariantStatusDeal);

            FillingCheckedListBox<CustomerFilterCustomerDeal, Customer>(CustomerFilter.CustomerDeal, cmbCustomerDeal);
            FillingRadioGroup(rgCustomerDeal, CustomerFilter.CriterionVariantCustomerDeal);

            FillingCheckedListBox<CustomerFilterStaffDeal, Staff>(CustomerFilter.StaffDeal, cmbStaffDeal);
            FillingRadioGroup(rgStaffDeal, CustomerFilter.CriterionVariantStaffDeal);

            #endregion

            #region Task

            FillingRadioGroup(rgCriterionVariantFilterTask, CustomerFilter.CriterionVariantFilterTask);

            FillingCheckedListBox<CustomerFilterStatusTask, TaskStatus>(CustomerFilter.StatusTask, cmbStatusTask);
            FillingRadioGroup(rgStatusTask, CustomerFilter.CriterionVariantStatusTask);

            FillingCheckedListBoxEnum<CustomerFilterTypeTask, TypeTask>(CustomerFilter.TypeTask, cmbTypeTask);
            FillingRadioGroup(rgTypeTask, CustomerFilter.CriterionVariantTypeTask);
            
            FillingCheckedListBox<CustomerFilterCustomerTask, Customer>(CustomerFilter.CustomerTask, cmbCustomerTask);
            FillingRadioGroup(rgCustomerTask, CustomerFilter.CriterionVariantCustomerTask);
            
            FillingCheckedListBox<CustomerFilterGivenStaffTask, Staff>(CustomerFilter.GivenStaffTask, cmbGivenStaffTask);
            FillingRadioGroup(rgGivenStaffTask, CustomerFilter.CriterionVariantGivenStaffTask);

            FillingCheckedListBox<CustomerFilterStaffTask, Staff>(CustomerFilter.StaffTask, cmbStaffTask);
            FillingRadioGroup(rgStaffTask, CustomerFilter.CriterionVariantStaffTask);

            FillingCheckedListBox<CustomerFilterCoExecutorTask, Staff>(CustomerFilter.CoExecutorTask, cmbCoExecutorTask, nameof(CustomerFilterCoExecutorTask.CoExecutor));
            FillingRadioGroup(rgCoExecutorTask, CustomerFilter.CriterionVariantCoExecutorTask);

            #endregion

            dateSince.EditValue = CustomerFilter.ContractDateConclusionSince;
            dateTo.EditValue = CustomerFilter.ContractDateConclusionTo;
            dateTerminationSince.EditValue = CustomerFilter.ContractTerminationSince;
            dateTerminationTo.EditValue = CustomerFilter.ContractTerminationTo;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((CustomerFilter.IsVisibleContract != checkIsVisibleContract.Checked) || (CustomerFilter.IsVisibleCustomer != checkIsVisibleCustomer.Checked))
            {
                IsEditVisible = true;
            }

            if (int.TryParse(txtNumber.Text, out int number))
            {
                CustomerFilter.Number = number;
            }
            else
            {
                CustomerFilter.Number = null;
            }


            CustomerFilter.IsGlobalFiltering = checkIsGlobalFiltering.Checked;

            CustomerFilter.Name = txtName.Text;
            CustomerFilter.Description = txtDescription.Text;
            
            CustomerFilter.IsVisibleContract = checkIsVisibleContract.Checked;
            CustomerFilter.IsVisibleCustomer = checkIsVisibleCustomer.Checked;
            CustomerFilter.IsVisibleDeal = checkIsVisibleDeal.Checked;
            CustomerFilter.IsVisibleTask = checkIsVisibleTask.Checked;

            CustomerFilter.CriterionVariantFilter = SaveRadioGroup(rgCriterionVariantFilter);
            CustomerFilter.CriterionVariantFilterCustomer = SaveRadioGroup(rgCriterionVariantFilterCustomer);
            CustomerFilter.CriterionVariantFilterContract = SaveRadioGroup(rgCriterionVariantFilterContract);
            CustomerFilter.CriterionVariantFilterDeal = SaveRadioGroup(rgCriterionVariantFilterDeal);
            
            SaveCheckedListBox<CustomerFilterAccountantResponsible, Staff>(CustomerFilter.AccountantResponsible, cmbAccountantResponsible, nameof(CustomerFilterAccountantResponsible.AccountantResponsible));
            CustomerFilter.CriterionVariantAccountantResponsible = SaveRadioGroup(rgAccountantResponsible);

            SaveCheckedListBox<CustomerFilterPrimaryResponsible, Staff>(CustomerFilter.PrimaryResponsible, cmbPrimaryResponsible, nameof(CustomerFilterPrimaryResponsible.PrimaryResponsible));
            CustomerFilter.CriterionVariantPrimaryResponsible = SaveRadioGroup(rgPrimaryResponsible);

            SaveCheckedListBox<CustomerFilterBankResponsible, Staff>(CustomerFilter.BankResponsible, cmbBankResponsible, nameof(CustomerFilterBankResponsible.BankResponsible));
            CustomerFilter.CriterionVariantBankResponsible = SaveRadioGroup(rgBankResponsible);

            SaveCheckedListBox<CustomerFilterSalaryResponsible, Staff>(CustomerFilter.SalaryResponsible, cmbSalaryResponsible, nameof(CustomerFilterSalaryResponsible.SalaryResponsible));
            CustomerFilter.CriterionVariantSalaryResponsible = SaveRadioGroup(rgSalaryResponsible);

            SaveCheckedListBox<CustomerFilterStatus, Status>(CustomerFilter.Status, cmbStatus);
            CustomerFilter.CriterionVariantStatus = SaveRadioGroup(rgStatus);
            
            SaveCheckedListBox<CustomerFilterTaxSystem, TaxSystem>(CustomerFilter.TaxSystems, cmbTaxSystem);
            CustomerFilter.CriterionVariantTaxSystems = SaveRadioGroup(rgTaxSystem);
            
            SaveCheckedListBox<CustomerFilterFormCorporation, FormCorporation>(CustomerFilter.FormCorporations, cmbFormCorporation);
            CustomerFilter.CriterionVariantFormCorporations = SaveRadioGroup(rgFormCorporation);
            
            SaveCheckedListBox<CustomerFilterClassOKVED2, ClassOKVED2>(CustomerFilter.ClassOKVED2, cmbClassOKVED2);
            CustomerFilter.CriterionVariantClassOKVED2 = SaveRadioGroup(rgClassOKVED2);

            SaveCheckedListBox<CustomerFilterElectronicReporting, ElectronicReporting>(CustomerFilter.ElectronicReporting, cmbElectronicReporting);
            CustomerFilter.CriterionVariantElectronicReporting = SaveRadioGroup(rgElectronicReporting);
            
            #region Deal

            CustomerFilter.CriterionVariantFilterDeal = SaveRadioGroup(rgCriterionVariantFilterDeal);
                
            SaveCheckedListBox<CustomerFilterStatusDeal, DealStatus>(CustomerFilter.StatusDeal, cmbStatusDeal);
            CustomerFilter.CriterionVariantStatusDeal = SaveRadioGroup(rgStatusDeal);

            SaveCheckedListBox<CustomerFilterCustomerDeal, Customer>(CustomerFilter.CustomerDeal, cmbCustomerDeal);
            CustomerFilter.CriterionVariantCustomerDeal = SaveRadioGroup(rgCustomerDeal);

            SaveCheckedListBox<CustomerFilterStaffDeal, Staff>(CustomerFilter.StaffDeal, cmbStaffDeal);
            CustomerFilter.CriterionVariantStaffDeal = SaveRadioGroup(rgStaffDeal);

            #endregion

            #region Task

            CustomerFilter.CriterionVariantFilterTask = SaveRadioGroup(rgCriterionVariantFilterTask);

            SaveCheckedListBox<CustomerFilterStatusTask, TaskStatus>(CustomerFilter.StatusTask, cmbStatusTask);
            CustomerFilter.CriterionVariantStatusTask = SaveRadioGroup(rgStatusTask);

            SaveCheckedListBoxEnum<CustomerFilterTypeTask, TypeTask>(CustomerFilter.TypeTask, cmbTypeTask);
            CustomerFilter.CriterionVariantTypeTask = SaveRadioGroup(rgTypeTask);

            SaveCheckedListBox<CustomerFilterCustomerTask, Customer>(CustomerFilter.CustomerTask, cmbCustomerTask);
            CustomerFilter.CriterionVariantCustomerTask = SaveRadioGroup(rgCustomerTask);

            SaveCheckedListBox<CustomerFilterGivenStaffTask, Staff>(CustomerFilter.GivenStaffTask, cmbGivenStaffTask);
            CustomerFilter.CriterionVariantGivenStaffTask = SaveRadioGroup(rgGivenStaffTask);

            SaveCheckedListBox<CustomerFilterStaffTask, Staff>(CustomerFilter.StaffTask, cmbStaffTask);
            CustomerFilter.CriterionVariantStaffTask = SaveRadioGroup(rgStaffTask);

            SaveCheckedListBox<CustomerFilterCoExecutorTask, Staff>(CustomerFilter.CoExecutorTask, cmbCoExecutorTask, nameof(CustomerFilterCoExecutorTask.CoExecutor));
            CustomerFilter.CriterionVariantCoExecutorTask = SaveRadioGroup(rgCoExecutorTask);

            #endregion

            SaveListBox(listTaxes, CustomerFilter, nameof(CustomerFilter.IsTaxAlcohol));
            SaveListBox(listTaxes, CustomerFilter, nameof(CustomerFilter.IsTaxIncome));
            SaveListBox(listTaxes, CustomerFilter, nameof(CustomerFilter.IsTaxIndirect));
            SaveListBox(listTaxes, CustomerFilter, nameof(CustomerFilter.IsTaxLand));
            SaveListBox(listTaxes, CustomerFilter, nameof(CustomerFilter.IsTaxProperty));
            SaveListBox(listTaxes, CustomerFilter, nameof(CustomerFilter.IsTaxTransport));
            SaveListBox(listTaxes, CustomerFilter, nameof(CustomerFilter.IsTaxSingleTemporaryIncome));
            SaveListBox(listTaxes, CustomerFilter, nameof(CustomerFilter.IsPatent));
            CustomerFilter.CriterionVariantTaxes = SaveRadioGroup(rgTaxes);
            
            CustomerFilter.IsImprest = checkIsImprest.Checked;            
            if (!string.IsNullOrWhiteSpace(txtImprestDay.Text) && int.TryParse(txtImprestDay.Text, out int imprestDay))
            {
                CustomerFilter.ImprestDay = imprestDay;
            }
            else
            {
                CustomerFilter.ImprestDay = null;
            }
            
            CustomerFilter.IsSalary = checkIsSalary.Checked;
            if (!string.IsNullOrWhiteSpace(txtSalaryDay.Text) && int.TryParse(txtSalaryDay.Text, out int salaryDay))
            {
                CustomerFilter.SalaryDay = salaryDay;
            }
            else
            {
                CustomerFilter.SalaryDay = null;
            }

            SaveCheckedListBox<CustomerFilterUser, User>(CustomerFilter.Users, cmbUser);
            SaveCheckedListBox<CustomerFilterUserGroup, UserGroup>(CustomerFilter.UserGroups, cmbUserGroup);

            SaveCheckedListBox<CustomerFilterContractStatus, ContractStatus>(CustomerFilter.ContractStatus, cmbContractStatus);
            CustomerFilter.CriterionVariantContractStatus = SaveRadioGroup(rgContractStatus);
            
            CustomerFilter.ContractDateConclusionSince = GetDateTime(dateSince.EditValue);
            CustomerFilter.ContractDateConclusionTo = GetDateTime(dateTo.EditValue);
            CustomerFilter.ContractTerminationSince = GetDateTime(dateTerminationSince.EditValue);
            CustomerFilter.ContractTerminationTo = GetDateTime(dateTerminationTo.EditValue);

            _session.Save(CustomerFilter);
            id = CustomerFilter.Oid;
            flagSave = true;
            Close();
        }
        
        private DateTime? GetDateTime(object obj)
        {
            if (obj is DateTime dateTime)
            {
                return dateTime;
            }
            else
            {
                return null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
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

        private void DeleteButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var dateEdit = sender as DateEdit;
            
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                dateEdit.EditValue = null;
                return;
            }
        }
    }
}