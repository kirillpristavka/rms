using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Reports;
using System;

namespace RMS.UI.Forms.Directories
{
    public partial class ReportChangeEditV2 : formEdit_BaseSpr
    {
        private Session _session;
        public ReportChangeCustomerV2 Report { get; private set; }

        public ReportChangeEditV2()
        {
            InitializeComponent();

            if (_session is null)
            {
                _session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
            }
        }

        public ReportChangeEditV2(int id) : this()
        {
            if (id > 0)
            {
                Report = _session.GetObjectByKey<ReportChangeCustomerV2>(id);
            }
        }

        public ReportChangeEditV2(ReportChangeCustomerV2 report) : this()
        {
            _session = report.Session;
            Report = report;
        }

        public ReportChangeEditV2(Customer customer) : this()
        {
            _session = customer.Session;
            Report = new ReportChangeCustomerV2(_session)
            {
                DeliveryTime = DateTime.Now.Date,
                Customer = customer
            };

            if (Report.Customer?.AccountantResponsible != null)
            {
                Report.AccountantResponsible = Report.Customer.AccountantResponsible;
            }

        }

        private void ReportEdit_Load(object sender, EventArgs e)
        {
            if (Report is null)
            {
                Report = new ReportChangeCustomerV2(_session)
                {
                    DeliveryTime = DateTime.Now.Date
                };
            }

            dateDeliveryTime.EditValue = Report.DeliveryTime;
            dateCompletion.EditValue = Report.DateCompletion;
            btnAccountantResponsible.EditValue = Report.AccountantResponsible;
            btnCustomer.EditValue = Report.Customer;
            btnReport.EditValue = Report.ReportV2;
            txtPeriod.EditValue = Report.Period;
            txtStatus.EditValue = Report.Status;
            memoComment.EditValue = Report.Comment;         
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveReport())
            {
                id = Report.Oid;
                flagSave = true;
                Close();
            }
        }

        /// <summary>
        /// Сохранение отчета.
        /// </summary>
        private bool SaveReport()
        {
            Report.DeliveryTime = PulsLibrary.Methods.Objects.GetRequiredObject<DateTime>(dateDeliveryTime.EditValue);
            Report.DateCompletion = PulsLibrary.Methods.Objects.GetRequiredObject<DateTime?>(dateCompletion.EditValue);

            Report.AccountantResponsible = PulsLibrary.Methods.Objects.GetRequiredObject<Staff>(btnAccountantResponsible.EditValue);
            Report.Customer = PulsLibrary.Methods.Objects.GetRequiredObject<Customer>(btnCustomer.EditValue);
            Report.ReportV2 = PulsLibrary.Methods.Objects.GetRequiredObject<ReportV2>(btnReport.EditValue);

            Report.Period = PulsLibrary.Methods.Objects.GetRequiredObject<string>(txtPeriod.EditValue);
            Report.Status = PulsLibrary.Methods.Objects.GetRequiredObject<string>(txtStatus.EditValue);
            Report.Comment = PulsLibrary.Methods.Objects.GetRequiredObject<string>(memoComment.EditValue);

            _session.Save(Report);

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAccountantResponsible_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (sender is ButtonEdit buttonEdit)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                    return;
                }
                else if (e.Button.Kind == ButtonPredefines.Ellipsis)
                {
                    cls_BaseSpr.ButtonEditButtonClickBase<Staff>(_session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
                }
            }
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(_session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnReport_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (sender is ButtonEdit buttonEdit)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                    return;
                }

                cls_BaseSpr.ButtonEditButtonClickBase<ReportV2>(_session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.ReportV2, 1, null, null, false, null, string.Empty, false, true);
            }
        }
    }
}