using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle.Taxes;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Taxes;
using RMS.UI.Forms.Chronicle;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms.Taxes
{
    public partial class TaxSingleTemporaryIncomeEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        private Tax Tax { get; }
        public TaxSingleTemporaryIncome TaxSingleTemporaryIncome { get; }

        private TaxSingleTemporaryIncomeEdit()
        {
            InitializeComponent();

            foreach (Availability item in Enum.GetValues(typeof(Availability)))
            {
                cmbAvailability.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbAvailability.SelectedIndex = 0;
        }

        public TaxSingleTemporaryIncomeEdit(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            Tax = customer.Tax ?? new Tax(Session);
            TaxSingleTemporaryIncome = Tax.TaxSingleTemporaryIncome ?? new TaxSingleTemporaryIncome(Session);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var availability = default(Availability);

            if (!string.IsNullOrWhiteSpace(cmbAvailability.Text) && cmbAvailability.SelectedIndex != -1)
            {
                foreach (Availability item in Enum.GetValues(typeof(Availability)))
                {
                    if (item.GetEnumDescription().Equals(cmbAvailability.Text))
                    {
                        availability = item;
                        break;
                    }
                }
            }
            else
            {
                cmbAvailability.Focus();
                XtraMessageBox.Show("Сохранение не возможно. Укажите наличие.", "Не указано наличие", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dateDate.EditValue is null)
            {
                dateDate.Focus();
                XtraMessageBox.Show("Сохранение не возможно без указания даты.", "Не указана дата", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }          

            if (TaxSingleTemporaryIncome.Oid != -1 &&
                (
                    TaxSingleTemporaryIncome.Date != dateDate.DateTime.Date ||
                    TaxSingleTemporaryIncome.Availability != availability ||
                    TaxSingleTemporaryIncome.PhysicalIndicator != txtPhysicalIndicator.Text ||
                    TaxSingleTemporaryIncome.Comment != memoComment.Text
                ))
            {
                TaxSingleTemporaryIncome.ChronicleTaxSingleTemporaryIncome.Add(new ChronicleTaxSingleTemporaryIncome(Session)
                {
                    Date = TaxSingleTemporaryIncome.Date,
                    IsUse = TaxSingleTemporaryIncome.IsUse,
                    Comment = memoComment.Text,
                    Availability = TaxSingleTemporaryIncome.Availability,
                    PhysicalIndicator = TaxSingleTemporaryIncome.PhysicalIndicator,
                    User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid),
                    DateCreate = DateTime.Now
                });
            }

            SaveCheckedListBox<CustomerEntrepreneurialActivityCodesUTII, EntrepreneurialActivityCodesUTII>
                (TaxSingleTemporaryIncome.CustomerEntrepreneurialActivityCodesUTII, cmbEntrepreneurialActivityCodesUTII);

            TaxSingleTemporaryIncome.Date = dateDate.DateTime;
            TaxSingleTemporaryIncome.Availability = availability;
            TaxSingleTemporaryIncome.Comment = memoComment.Text;
            TaxSingleTemporaryIncome.PhysicalIndicator = txtPhysicalIndicator.Text;

            TaxSingleTemporaryIncome.Save();
            Tax.TaxSingleTemporaryIncome = TaxSingleTemporaryIncome;
            Tax.Save();
            Customer.Tax = Tax;
            Customer.Save();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            btnCustomer.EditValue = Customer;
            dateDate.EditValue = TaxSingleTemporaryIncome.Date ?? DateTime.Now.Date;            
            memoComment.EditValue = TaxSingleTemporaryIncome.Comment;
            txtPhysicalIndicator.EditValue = TaxSingleTemporaryIncome.PhysicalIndicator;

            if (TaxSingleTemporaryIncome.Availability != null)
            {
                cmbAvailability.SelectedIndex = Convert.ToInt32(TaxSingleTemporaryIncome.Availability);
            }

            FillingCheckedListBox<CustomerEntrepreneurialActivityCodesUTII, EntrepreneurialActivityCodesUTII>
                (TaxSingleTemporaryIncome.CustomerEntrepreneurialActivityCodesUTII, cmbEntrepreneurialActivityCodesUTII);
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
                checkedComboBoxEdit.CustomDisplayText += CheckedComboBoxEdit_CustomDisplayText<T1, T2>;
                checkedComboBoxEdit.Properties.Items.Clear();

                var xpCollectionObject = new XPCollection<T2>(xpCollection.Session);
                foreach (var item in xpCollectionObject)
                {
                    var description = $"[{item}] - {item.GetMemberValue("Name")}";
                    checkedComboBoxEdit.Properties.Items.Add(item, description);
                }
                
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

        private void CheckedComboBoxEdit_CustomDisplayText<T1, T2>(object sender, CustomDisplayTextEventArgs e)
            where T1 : XPObject
            where T2 : XPObject
        {
            if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString()))
                return;

            e.DisplayText = "";
            string sPattern = @"\d{2}";

            if (System.Text.RegularExpressions.Regex.IsMatch(e.Value.ToString(), sPattern))
            {
                var matchs = System.Text.RegularExpressions.Regex.Matches(e.Value.ToString(), sPattern);
                var count = matchs.Count;

                foreach (var item in matchs)
                {
                    count--;

                    if (count == 0)
                    {
                        e.DisplayText += $"{item}";
                    }
                    else
                    {
                        e.DisplayText += $"{item}, ";
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

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnChronicle_Click(object sender, EventArgs e)
        {
            var form = new ChronicleTaxesForm<ChronicleTaxSingleTemporaryIncome>(TaxSingleTemporaryIncome.ChronicleTaxSingleTemporaryIncome, "Хроника изменений ЕНВД");
            form.ShowDialog();
        }

        private void btnCode_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<EntrepreneurialActivityCodesUTII>(Session, buttonEdit, (int)cls_App.ReferenceBooks.EntrepreneurialActivityCodesUTII, 1, null, null, false, null, string.Empty, false, true);
        }
    }
}