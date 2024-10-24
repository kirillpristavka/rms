using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using RMS.Core.Controller.Print;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms.Directories
{
    public partial class InvoiceEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        public Invoice Invoice { get; }
        private OrganizationPerformance OrganizationPerformance { get; }
        public bool IsSave { get; set; }

        public InvoiceEdit(Session session)
        {
            if (session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
            }
            else
            {
                Session = session;
            }
            
            XPObject.AutoSaveOnEndEdit = false;
            InitializeComponent();
            Invoice = new Invoice(Session);
        }

        public InvoiceEdit(Customer customer) : this(customer.Session)
        {
            Customer = customer;
            Session = customer.Session;            
            Invoice = new Invoice(Session);
        }
        
        public InvoiceEdit(Invoice invoice) : this(invoice.Session)
        {
            Invoice = invoice;
            Customer = invoice.Customer;
            Session = invoice.Session;
        }

        public InvoiceEdit(Invoice invoice, OrganizationPerformance organizationPerformance) : this(invoice.Session)
        {
            Invoice = invoice;
            Customer = invoice.Customer;
            Session = invoice.Session;
            OrganizationPerformance = Session.GetObjectByKey<OrganizationPerformance>(organizationPerformance.Oid);
        }

        private bool Save()
        {
            if (btnCustomer.EditValue is Customer customer)
            {
                Invoice.Customer = customer;
            }
            else
            {
                XtraMessageBox.Show("Без выбранного клиента сохранение не возможно", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCustomer.Focus();
                return false;
            }

            if (dateDate.EditValue is DateTime date)
            {
                Invoice.Date = date;
            }
            else
            {
                XtraMessageBox.Show("Без даты сохранение не возможно", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dateDate.Focus();
                return false;
            }
            
            Invoice.Number = txtAccountNumber.Text;

            if (checkIsApproved.Checked)
            {
                if (btnApprovedStaff.EditValue is Staff staff)
                {
                    Invoice.ApprovedStaff = staff;
                }
                else
                {
                    XtraMessageBox.Show("Без выбранного сотрудника сохранение не возможно", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnApprovedStaff.Focus();
                    return false;
                }

                if (dateApprovedDate.EditValue is DateTime dateTime)
                {
                    Invoice.ApprovedDate = dateTime;
                }
                else
                {
                    XtraMessageBox.Show("Без даты подтверждения сохранение не возможно", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dateApprovedDate.Focus();
                    return false;
                }

                Invoice.IsApproved = true;
            }
            else
            {
                Invoice.IsApproved = false;
                Invoice.ApprovedStaff = null;
                Invoice.ApprovedDate = null;
            }

            if (dateDeadlineDate.EditValue is DateTime deadlineDate)
            {
                Invoice.DeadlineDate = deadlineDate;
            }
            else
            {
                XtraMessageBox.Show("Укажите дату оплаты до", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dateDeadlineDate.Focus();
                return false;
            }

            Session.Save(Invoice.InvoiceInformations);
            Session.Save(Invoice.Payments);
            customer.Invoices.Add(Invoice);
            customer.Save();

            if (OrganizationPerformance != null)
            {
                OrganizationPerformance.Invoice = Invoice;
                OrganizationPerformance.Save();
            }

            IsSave = true;
            
            return true;
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                Close();
            }            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool isEditInvoiceForm = false;
        private async System.Threading.Tasks.Task SetAccessRights()
        {
            try
            {
                using (var uof = new UnitOfWork())
                {
                    var user = await uof.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid);
                    if (user != null)
                    {
                        var accessRights = user.AccessRights;
                        if (accessRights != null)
                        {
                            isEditInvoiceForm = accessRights.IsEditInvoiceForm;
                        }

                        btnSave.Enabled = isEditInvoiceForm;

                        barBtnAddPayment.Enabled = isEditInvoiceForm;
                        barBtnDelPayment.Enabled = isEditInvoiceForm;
                        
                        barBtnAddInvoiceInformation.Enabled = isEditInvoiceForm;
                        barBtnInvoiceInformationEdit.Enabled = isEditInvoiceForm;
                        barBtnDelInvoiceInformation.Enabled = isEditInvoiceForm;

                        CustomerEdit.CloseButtons(btnCustomer, isEditInvoiceForm);
                        CustomerEdit.CloseButtons(btnApprovedStaff, isEditInvoiceForm);
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            await SetAccessRights();
            
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Customer>(Session, btnCustomer, cls_App.ReferenceBooks.Customer, isEnable: isEditInvoiceForm);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Staff>(Session, btnApprovedStaff, cls_App.ReferenceBooks.Staff, isEnable: isEditInvoiceForm);

            if (Customer is null)
            {
                btnCustomer.Properties.ReadOnly = false;
            }
            
            Invoice.InvoiceInformations.ListChanged += InvoiceInformations_ListChanged;

            if (Invoice.Oid > 0)
            {
                Invoice.Payments.Reload();
                Invoice.InvoiceInformations.Reload();
            }
            
            if (Customer is null)
            {
                btnCustomer.Properties.ReadOnly = false;
            }
            else
            {
                btnCustomer.EditValue = Customer;
            }
            
            dateDate.EditValue = Invoice.Date;
            txtAccountNumber.EditValue = Invoice.Number;
            checkIsApproved.Checked = Invoice.IsApproved;
            btnApprovedStaff.EditValue = Invoice.ApprovedStaff;
            dateApprovedDate.EditValue = Invoice.ApprovedDate;
            dateDeadlineDate.EditValue = Invoice.DeadlineDate;
            dateSentByEmailDate.EditValue = Invoice.SentByEmailDate;
            
            lblSum.Text = $"Сумма счета: {Invoice.Value}";

            gridControlPayments.DataSource = Invoice.Payments;
            if (gridViewPayments.Columns[nameof(Payment.Oid)] != null)
            {
                gridViewPayments.Columns[nameof(Payment.Oid)].Visible = false;
                gridViewPayments.Columns[nameof(Payment.Oid)].Width = 18;
                gridViewPayments.Columns[nameof(Payment.Oid)].OptionsColumn.FixedWidth = true;
                gridViewPayments.BestFitColumns();
            }
            if (gridViewPayments.Columns[nameof(Payment.Description)] != null)
            {
                gridViewPayments.Columns[nameof(Payment.Description)].Visible = false;
            }

            gridControlInvoiceInformations.DataSource = Invoice.InvoiceInformations;

            foreach (GridColumn column in gridViewInvoiceInformations.Columns)
            {
                column.OptionsColumn.AllowEdit = false;
                column.OptionsColumn.ReadOnly = true;
            }
            
            RepositoryItemMemoEdit memoEdit = gridControlInvoiceInformations.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
            
            if (gridViewInvoiceInformations.Columns[nameof(InvoiceInformation.Oid)] != null)
            {
                gridViewInvoiceInformations.Columns[nameof(InvoiceInformation.Oid)].Visible = false;
                gridViewInvoiceInformations.Columns[nameof(InvoiceInformation.Oid)].Width = 18;
                gridViewInvoiceInformations.Columns[nameof(InvoiceInformation.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridViewInvoiceInformations.Columns[nameof(InvoiceInformation.Sum)] != null)
            {
                gridViewInvoiceInformations.Columns[nameof(InvoiceInformation.Sum)].OptionsColumn.AllowEdit = true;
                gridViewInvoiceInformations.Columns[nameof(InvoiceInformation.Sum)].OptionsColumn.ReadOnly = false;
            }
            
            if (gridViewInvoiceInformations.Columns[nameof(InvoiceInformation.Unit)] != null)
            {
                gridViewInvoiceInformations.Columns[nameof(InvoiceInformation.Unit)].OptionsColumn.AllowEdit = true;
                gridViewInvoiceInformations.Columns[nameof(InvoiceInformation.Unit)].OptionsColumn.ReadOnly = false;
            }

            if (gridViewInvoiceInformations.Columns[nameof(InvoiceInformation.Description)] != null)
            {
                gridViewInvoiceInformations.Columns[nameof(InvoiceInformation.Description)].OptionsColumn.AllowEdit = true;
                gridViewInvoiceInformations.Columns[nameof(InvoiceInformation.Description)].OptionsColumn.ReadOnly = false;
                gridViewInvoiceInformations.Columns[nameof(InvoiceInformation.Description)].ColumnEdit = memoEdit;
            }

            if (gridViewInvoiceInformations.Columns[nameof(InvoiceInformation.Comment)] != null)
            {
                gridViewInvoiceInformations.Columns[nameof(InvoiceInformation.Comment)].ColumnEdit = memoEdit;
            }
        }

        private void InvoiceInformations_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            var collection = sender as XPCollection<InvoiceInformation>;

            if (collection != null && collection.Count > 0)
            {
                var sum = collection.Sum(s => s.Sum);
                lblSum.Text = $"Сумма счета: {sum}";
                Invoice.Value = sum;
            }
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnApprovedStaff_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;
            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
        }

        private void InvoiceEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            Invoice.Reload();            
            Invoice.Payments.Reload();
            foreach (var payment in Invoice.Payments)
            {
                payment.Reload();
            }
            Invoice.InvoiceInformations.Reload();
            foreach (var invoiceInformation in Invoice.InvoiceInformations)
            {
                invoiceInformation.Reload();
            }
            XPObject.AutoSaveOnEndEdit = true;
        }

        private void checkIsApproved_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckEdit checkEdit && checkEdit.Checked)
            {
                btnApprovedStaff.Enabled = true;
                dateApprovedDate.Enabled = true;
                dateApprovedDate.EditValue = DateTime.Now.Date;
            }
            else
            {
                btnApprovedStaff.EditValue = null;
                dateApprovedDate.EditValue = null;
                btnApprovedStaff.Enabled = false;
                dateApprovedDate.Enabled = false;
            }
        }

        private void gridViewPayments_MouseDown(object sender, MouseEventArgs e)
        {
            DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            GridView gridview = sender as GridView;
            _ = gridview.CalcHitInfo(dxMouseEventArgs.Location);
            //if ((gridHitInfo.InRow || gridHitInfo.InRowCell) && dxMouseEventArgs.Button == MouseButtons.Right)
            if (dxMouseEventArgs.Button == MouseButtons.Right)
            {
                popupMenuPayments.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void gridViewInvoiceInformations_MouseDown(object sender, MouseEventArgs e)
        {
            DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            GridView gridview = sender as GridView;
            _ = gridview.CalcHitInfo(dxMouseEventArgs.Location);
            //if ((gridHitInfo.InRow || gridHitInfo.InRowCell) && dxMouseEventArgs.Button == MouseButtons.Right)
            if (dxMouseEventArgs.Button == MouseButtons.Right)
            {
                popupMenuInvoiceInformation.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void barBtnDelPayment_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewPayments.IsEmpty)
            {
                return;
            }

            var payment = gridViewPayments.GetRow(gridViewPayments.FocusedRowHandle) as InvoicePayment;
            if (payment != null)
            {
                Invoice.Payments.Remove(payment);
            }
        }

        private void barBtnAddPayment_ItemClick(object sender, ItemClickEventArgs e)
        {
            Invoice.Payments.Add(new InvoicePayment(Session));
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if(Save())
            {
                var invoiceView = new InvoiceView(Invoice);
            }
        }

        private async void barBtnAddInvoiceInformation_ItemClick(object sender, ItemClickEventArgs e)
        {
            var customer = default(Customer);

            if (Customer is null)
            {
                if (btnCustomer.EditValue is Customer _customer)
                {
                    customer = await Session.GetObjectByKeyAsync<Customer>(_customer.Oid);
                }
            }
            else
            {
                customer = Customer;
            }
            
            if (customer != null)
            {
                var form = new CustomerServiceProvidedEdit(customer);
                form.ShowDialog();

                var customerServiceProvided = form.CustomerServiceProvided;
                if (customerServiceProvided != null && customerServiceProvided.Oid > 0)
                {
                    var price = default(decimal);
                    var description = default(string);
                    if (customerServiceProvided.IsServicePrice)
                    {
                        description = customerServiceProvided.PriceList.Name;
                        price = customerServiceProvided.PriceList.Price;
                    }
                    else
                    {
                        description = customerServiceProvided.Name;
                        price = customerServiceProvided.Price;
                    }

                    var count = default(int);
                    if (customerServiceProvided.Count is null)
                    {
                        count = 1;
                    }
                    else
                    {
                        count = Convert.ToInt32(customerServiceProvided.Count);
                    }

                    var invoiceInformation = new InvoiceInformation(Session)
                    {
                        Name = "Дополнительная услуга (ручная)",
                        Description = description,
                        Comment = "Ручное добавление услуги",
                        Sum = customerServiceProvided.Price.GetDecimalRound(),
                        Count = count,
                        Price = price,
                        Unit = "шт",
                        CustomerServiceProvided = customerServiceProvided,
                        CustomerPerformanceIndicator = null
                    };
                    Invoice.InvoiceInformations.Add(invoiceInformation);
                }
            }            
        }

        private void barBtnInvoiceInformationEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewInvoiceInformations.IsEmpty)
            {
                return;
            }

            var invoiceInformation = gridViewInvoiceInformations.GetRow(gridViewInvoiceInformations.FocusedRowHandle) as InvoiceInformation;
            
            if (invoiceInformation != null && invoiceInformation.CustomerServiceProvided != null)
            {
                var form = new CustomerServiceProvidedEdit(invoiceInformation.CustomerServiceProvided);
                form.ShowDialog();
            }
        }

        private void barBtnDelInvoiceInformation_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewInvoiceInformations.IsEmpty)
            {
                return;
            }

            var invoiceInformation = gridViewInvoiceInformations.GetRow(gridViewInvoiceInformations.FocusedRowHandle) as InvoiceInformation;
            if (invoiceInformation != null)
            {
                Invoice.InvoiceInformations.Remove(invoiceInformation);
            }
        }
    }
}