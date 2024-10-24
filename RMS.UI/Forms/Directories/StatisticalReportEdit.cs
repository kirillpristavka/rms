using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Reports;
using System;
using System.Windows.Forms;

namespace RMS.UI.Forms.Directories
{
    public partial class StatisticalReportEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        public StatisticalReport CustomerReport { get; }

        public StatisticalReportEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                CustomerReport = new StatisticalReport(Session);
            }
        }

        public StatisticalReportEdit(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            CustomerReport = new StatisticalReport(Session);
        }

        public StatisticalReportEdit(StatisticalReport statisticalReport) : this()
        {
            CustomerReport = statisticalReport;
            Customer = statisticalReport.Customer;
            Session = statisticalReport.Session;
        }

        private void CustomerReportEdit_Load(object sender, EventArgs e)
        {
            btnCustomer.EditValue = Customer;
            btnResponsible.EditValue = CustomerReport.Responsible;

            btnReport.EditValue = CustomerReport.Report;
            txtFormIndex.EditValue = CustomerReport.Report?.FormIndex;
            txtOKUD.EditValue = CustomerReport.Report?.OKUD;
            memoName.EditValue = CustomerReport.Report?.Name;
            txtPeriodicity.EditValue = CustomerReport.Report?.Periodicity.GetEnumDescription();
            memoDeadline.EditValue = CustomerReport.Report?.Deadline;
            memoComment.EditValue = CustomerReport.Report?.Comment;
            txtYear.EditValue = CustomerReport.Year == 0 ? DateTime.Now.Year : CustomerReport.Year;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (Customer is null)
            {
                XtraMessageBox.Show("Без клиента сохранение невозможно", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCustomer.Focus();
                return;
            }

            if (btnReport.EditValue is Report report)
            {
                CustomerReport.Report = report;
            }
            else
            {
                XtraMessageBox.Show("Без выбранного отчета сохранение невозможно.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnReport.Focus();
                return;
            }

            var year = 2000;

            if (string.IsNullOrWhiteSpace(txtYear.Text) || txtYear.Text.Equals("0"))
            {
                XtraMessageBox.Show("Укажите год сдачи отчета.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtYear.Focus();
                return;
            }
            else
            {
                if (int.TryParse(txtYear.Text, out int result))
                {
                    if (result < 2000)
                    {
                        if (XtraMessageBox.Show($"Вы указали год сдачи отчета: {result}. Хотите продолжить?", "Проверка года", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            year = result;
                        }
                        else
                        {
                            txtYear.Focus();
                            return;
                        }
                    }
                    else
                    {
                        year = result;
                    }

                    CustomerReport.Year = year;
                }
                else
                {
                    XtraMessageBox.Show("Ошибка преобразования строки в год.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtYear.Focus();
                    return;
                }
            }

            if (btnResponsible.EditValue is Staff responsible)
            {
                CustomerReport.Responsible = responsible;
            }
            else
            {
                CustomerReport.Responsible = null;
            }

            CustomerReport.SetCreateObj(await Session.GetObjectByKeyAsync<User>(DatabaseConnection.User?.Oid));
            Customer.StatisticalReports.Add(CustomerReport);
            CustomerReport.Save();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnResponsible_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
        }

        private void btnReport_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                txtFormIndex.EditValue = null;
                txtOKUD.EditValue = null;
                memoName.EditValue = null;
                txtPeriodicity.EditValue = null;
                memoDeadline.EditValue = null;
                memoComment.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Report>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Report, 1, null, null, false, null, string.Empty, false, true);

            if (buttonEdit.EditValue is Report report)
            {
                txtFormIndex.EditValue = report.FormIndex;
                txtOKUD.EditValue = report.OKUD;
                memoName.EditValue = report.Name;
                txtPeriodicity.EditValue = report.Periodicity.GetEnumDescription();
                memoDeadline.EditValue = report.Deadline;
                memoComment.EditValue = report.Comment;
            }
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