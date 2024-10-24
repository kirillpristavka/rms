using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Windows.Forms;

namespace RMS.UI.Forms.Directories
{
    public partial class CustomerServiceProvidedEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        public CustomerServiceProvided CustomerServiceProvided { get; }

        public CustomerServiceProvidedEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                CustomerServiceProvided = new CustomerServiceProvided(Session);
            }
        }

        public CustomerServiceProvidedEdit(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            CustomerServiceProvided = new CustomerServiceProvided(Session);
        }

        public CustomerServiceProvidedEdit(CustomerServiceProvided customerServiceProvided) : this()
        {
            CustomerServiceProvided = customerServiceProvided;
            Customer = customerServiceProvided.Customer;
            Session = customerServiceProvided.Session;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnCustomer.EditValue is Customer customer)
            {
                CustomerServiceProvided.Customer = customer;
            }
            else
            {
                btnCustomer.Focus();
                XtraMessageBox.Show("Сохранение не возможно без указания клиента.", "Не указан клиент", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (btnStaff.EditValue is Staff staff)
            {
                CustomerServiceProvided.Staff = Session.GetObjectByKey<Staff>(staff.Oid);
            }
            else
            {
                btnStaff.Focus();
                XtraMessageBox.Show("Сохранение не возможно без указания сотрудника.", "Не указан сотрудник", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dateDate.EditValue is DateTime date)
            {
                CustomerServiceProvided.Date = date;
            }
            else
            {
                dateDate.Focus();
                XtraMessageBox.Show("Сохранение не возможно без даты предоставления услуги.", "Не указана дата", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CustomerServiceProvided.IsServicePrice = checkIsServicePrice.Checked;

            if (CustomerServiceProvided.IsServicePrice)
            {
                CustomerServiceProvided.Name = null;

                if (btnPriceList.EditValue is PriceList priceList)
                {
                    CustomerServiceProvided.PriceList = priceList;
                }
                else
                {
                    btnPriceList.Focus();
                    XtraMessageBox.Show("В предоставляемой услуге должна присутствовать позиция из прайса.", "Не указана позиция из прайса", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (spinCount.EditValue is decimal count)
                {
                    CustomerServiceProvided.Count = count;
                }
                else
                {
                    spinCount.Focus();
                    XtraMessageBox.Show("Не указано количество позиций по прайсу.", "Не указана количество", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    txtName.Focus();
                    XtraMessageBox.Show("Не задано наименование предоставляемой услуги.", "Не задано наименование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    CustomerServiceProvided.Name = txtName.Text;
                }

                CustomerServiceProvided.PriceList = null;
                CustomerServiceProvided.Count = null;
            }

            if (calcPrice.Value is decimal price)
            {
                if (CustomerServiceProvided.IsServicePrice)
                {
                    var verificationPrice = CustomerServiceProvided.PriceList?.Price * CustomerServiceProvided.Count;

                    if (verificationPrice is decimal)
                    {
                        price = Convert.ToDecimal(verificationPrice);
                    }
                    else
                    {
                        XtraMessageBox.Show($"Возникла проблема с расчетом цены. Переменная {nameof(verificationPrice)} не является [decimal].\nОбратитесь к разработчику ПО.", "Ошибка переменной", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                CustomerServiceProvided.Price = price;
            }
            else
            {
                calcPrice.Focus();
                XtraMessageBox.Show("Не указана цена предоставляемых услуг.", "Не указана цена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CustomerServiceProvided.Customer = Customer;
            CustomerServiceProvided.Save();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CustomerServiceProvidedEdit_Load(object sender, EventArgs e)
        {
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Customer>(Session, btnCustomer, cls_App.ReferenceBooks.Customer);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Staff>(Session, btnStaff, cls_App.ReferenceBooks.Staff);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<PriceList> (Session, btnPriceList, cls_App.ReferenceBooks.PriceList);

            btnCustomer.EditValue = Customer;
            btnStaff.EditValue = CustomerServiceProvided.Staff ?? DatabaseConnection.User.Staff;
            dateDate.EditValue = (CustomerServiceProvided.Date == null || CustomerServiceProvided.Date == DateTime.MinValue)
                ? DateTime.Now.Date : CustomerServiceProvided.Date;
            checkIsServicePrice.EditValue = CustomerServiceProvided.IsServicePrice;
            txtName.EditValue = CustomerServiceProvided.Name;
            btnPriceList.EditValue = CustomerServiceProvided.PriceList;
            spinCount.EditValue = CustomerServiceProvided.Count ?? 0;
            calcPrice.EditValue = CustomerServiceProvided.Price;
        }

        private void checkIsServicePrice_CheckedChanged(object sender, EventArgs e)
        {
            var checkEdit = sender as CheckEdit;

            if (checkEdit.Checked)
            {
                txtName.Properties.ReadOnly = checkEdit.Checked;
                txtName.EditValue = null;
                calcPrice.Properties.ReadOnly = checkEdit.Checked;
                calcPrice.EditValue = 0;
                btnPriceList.Properties.ReadOnly = !checkEdit.Checked;
                spinCount.Properties.ReadOnly = !checkEdit.Checked;
                spinCount.EditValue = 0;
            }
            else
            {
                txtName.Properties.ReadOnly = checkEdit.Checked;
                calcPrice.Properties.ReadOnly = checkEdit.Checked;
                calcPrice.EditValue = 0;
                btnPriceList.Properties.ReadOnly = !checkEdit.Checked;
                btnPriceList.EditValue = null;
                spinCount.Properties.ReadOnly = !checkEdit.Checked;
                spinCount.EditValue = null;
            }
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnPriceList_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<PriceList>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.PriceList, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnStaff_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;
            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
        }

        private void spinCount_EditValueChanged(object sender, EventArgs e)
        {
            var spinEdit = sender as SpinEdit;
            if (checkIsServicePrice.Checked && btnPriceList.EditValue != null)
            {
                var priceList = btnPriceList.EditValue as PriceList;
                calcPrice.EditValue = spinEdit.Value * priceList.Price;
            }
        }

        private void btnPriceList_EditValueChanged(object sender, EventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (buttonEdit.EditValue is PriceList priceList && spinCount.EditValue != null)
            {
                calcPrice.EditValue = spinCount.Value * priceList.Price;
            }
        }
    }
}