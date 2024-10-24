using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout.Utils;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle.Taxes;
using RMS.Core.Model.OKVED;
using RMS.Core.Model.Taxes;
using System;

namespace RMS.UI.Forms.Taxes
{
    public partial class PatentObjectEdit : XtraForm
    {
        private Session Session { get; }
        private Tax Tax { get; }
        public Patent Patent { get; }        
        public PatentObject PatentObject { get; }
        public bool IsSave { get; private set; }

        private PatentObjectEdit()
        {
            InitializeComponent();
            layoutControlItemName.Visibility = LayoutVisibility.Never;
        }

        public PatentObjectEdit(PatentObject patentObject) : this()
        {
            Session = patentObject.Session;
            Patent = patentObject.Patent;
            PatentObject = patentObject;
        }
        
        public PatentObjectEdit(Patent patent) : this()
        {
            Session = patent.Session;
            Patent = patent;
            PatentObject = new PatentObject(Session) 
            { 
                AccountingInsuranceStatus = Session.FindObject<AccountingInsuranceStatus>(new BinaryOperator(nameof(AccountingInsuranceStatus.IsDefault), true)),
                AccountingInsuranceStatus2 = Session.FindObject<AccountingInsuranceStatus>(new BinaryOperator(nameof(AccountingInsuranceStatus.IsDefault), true)),
                PatentStatus = Session.FindObject<PatentStatus>(new BinaryOperator(nameof(PatentStatus.IsDefault), true)),
            };
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var advancePaymentValue = default(decimal?);
            var actualPaymentValue = default(decimal?);
            
            var _kindActivity = default(KindActivity);
            var _patentStatus = default(PatentStatus);
            var _classOKVED2 = default(ClassOKVED2);
            var _dateSince = default(DateTime?);
            var _dateTo = default(DateTime?);
            var _actualPaymentDate = default(DateTime?);
            var _advancePaymentDate = default(DateTime?);
            
            var comment = memoComment.Text;
            var taxAuthority = txtTaxAuthority.Text;

            var isPaymentAdvancePaymentValue = checkIsPaymentAdvancePaymentValue.Checked;
            var isPaymentActualPaymentValue = checkIsPaymentActualPaymentValue.Checked;

            var _accountingInsuranceStatus = default(AccountingInsuranceStatus);
            var _dateAccountingInsuranceStatus = default(DateTime?);

            var _accountingInsuranceStatus2 = default(AccountingInsuranceStatus);
            var _dateAccountingInsuranceStatus2 = default(DateTime?);

            if (!string.IsNullOrWhiteSpace(txtActualPaymentValue.Text))
            {
                actualPaymentValue = Convert.ToDecimal(txtActualPaymentValue.Text);
            }
            else
            {
                actualPaymentValue = null;
            }

            if (!string.IsNullOrWhiteSpace(txtAdvancePaymentValue.Text))
            {
                advancePaymentValue = Convert.ToDecimal(txtAdvancePaymentValue.Text);
            }
            else
            {
                advancePaymentValue = null;
            }

            if (btnKindActivity.EditValue is KindActivity kindActivity)
            {
                _kindActivity = kindActivity;
            }
            else
            {
                _kindActivity = null;                
            }            

            if (btnKindActivity.EditValue is ClassOKVED2 classOKVED2)
            {
                _classOKVED2 = classOKVED2;
            }
            else
            {
                _classOKVED2 = null;
            }            

            if (dateDateSince.EditValue is DateTime dateSince)
            {
                _dateSince = dateSince;
            }
            else
            {
                _dateSince = null;
            }

            if (dateDateTo.EditValue is DateTime dateTo)
            {
                _dateTo = dateTo;
            }
            else
            {
                _dateTo = null;
            }
            
            if (dateActualPaymentDate.EditValue is DateTime actualPaymentDate)
            {
                _actualPaymentDate = actualPaymentDate;
            }
            else
            {
                _actualPaymentDate = null;
            }

            if (dateAdvancePaymentDate.EditValue is DateTime advancePaymentDate)
            {
                _advancePaymentDate = advancePaymentDate;
            }
            else
            {
                _advancePaymentDate = null;
            }

            if (btnPatentStatus.EditValue is PatentStatus patentStatus)
            {
                _patentStatus = patentStatus;
            }
            else
            {
                _patentStatus = null;
            }

            if (btnAccountingInsuranceStatus1.EditValue is AccountingInsuranceStatus accountingInsuranceStatus)
            {
                _accountingInsuranceStatus = accountingInsuranceStatus;
            }
            else
            {
                _accountingInsuranceStatus = null;
            }

            if (dateAccountingInsuranceStatus1.EditValue is DateTime dAccountingInsuranceStatus)
            {
                _dateAccountingInsuranceStatus = dAccountingInsuranceStatus;
            }
            else
            {
                _dateAccountingInsuranceStatus = null;
            }

            if (btnAccountingInsuranceStatus2.EditValue is AccountingInsuranceStatus accountingInsuranceStatus2)
            {
                _accountingInsuranceStatus2 = accountingInsuranceStatus2;
            }
            else
            {
                _accountingInsuranceStatus2 = null;
            }

            if (dateAccountingInsuranceStatus2.EditValue is DateTime dAccountingInsuranceStatus2)
            {
                _dateAccountingInsuranceStatus2 = dAccountingInsuranceStatus2;
            }
            else
            {
                _dateAccountingInsuranceStatus2 = null;
            }

            if (PatentObject.Oid != -1 &&
                (PatentObject.ClassOKVED2 != _classOKVED2
                || PatentObject.DateSince != _dateSince
                || PatentObject.DateTo != _dateTo
                || PatentObject.ActualPaymentDate != _actualPaymentDate
                || PatentObject.AdvancePaymentDate != _advancePaymentDate
                || PatentObject.Comment != comment
                || PatentObject.TaxAuthority != taxAuthority
                || PatentObject.PatentStatus != _patentStatus
                || PatentObject.ActualPaymentValue != actualPaymentValue
                || PatentObject.AdvancePaymentValue != advancePaymentValue
                || PatentObject.IsPaymentActualPaymentValue != isPaymentActualPaymentValue
                || PatentObject.IsPaymentAdvancePaymentValue != isPaymentAdvancePaymentValue
                || PatentObject.AccountingInsuranceStatus != _accountingInsuranceStatus
                || PatentObject.DateAccountingInsuranceStatus != _dateAccountingInsuranceStatus
                || PatentObject.AccountingInsuranceStatus2 != _accountingInsuranceStatus2
                || PatentObject.DateAccountingInsuranceStatus2 != _dateAccountingInsuranceStatus2
                || PatentObject.KindActivity != _kindActivity))
            {
                Patent.ChroniclePatents.Add(new ChroniclePatent(Session)
                {
                    Comment = memoComment.Text,
                    DateSince = PatentObject.DateSince,
                    DateTo = PatentObject.DateTo,
                    ActualPaymentDate = PatentObject.ActualPaymentDate,
                    AdvancePaymentDate = PatentObject.AdvancePaymentDate,
                    KindActivity = PatentObject.KindActivity,
                    Name = PatentObject.Name,
                    TaxAuthority = PatentObject.TaxAuthority,
                    PatentStatus = PatentObject.PatentStatus,
                    User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid),
                    ClassOKVED2 = PatentObject.ClassOKVED2,
                    ActualPaymentValue = PatentObject.ActualPaymentValue,
                    AdvancePaymentValue = PatentObject.AdvancePaymentValue,
                    IsPaymentActualPaymentValue = PatentObject.IsPaymentActualPaymentValue,
                    IsPaymentAdvancePaymentValue = PatentObject.IsPaymentAdvancePaymentValue,
                    AccountingInsuranceStatus = PatentObject.AccountingInsuranceStatus,
                    DateAccountingInsuranceStatus = PatentObject.DateAccountingInsuranceStatus,
                    AccountingInsuranceStatus2 = PatentObject.AccountingInsuranceStatus2,
                    DateAccountingInsuranceStatus2 = PatentObject.DateAccountingInsuranceStatus2
                });
            }

            PatentObject.KindActivity = _kindActivity;
            PatentObject.ClassOKVED2 = _classOKVED2;
            PatentObject.PatentStatus = _patentStatus;
            PatentObject.DateSince = _dateSince;
            PatentObject.DateTo = _dateTo;
            PatentObject.ActualPaymentDate = _actualPaymentDate;
            PatentObject.AdvancePaymentDate = _advancePaymentDate;            
            PatentObject.Comment = comment;
            PatentObject.TaxAuthority = taxAuthority;
            PatentObject.ActualPaymentValue = actualPaymentValue;
            PatentObject.AdvancePaymentValue = advancePaymentValue;
            PatentObject.IsPaymentActualPaymentValue = isPaymentActualPaymentValue;
            PatentObject.IsPaymentAdvancePaymentValue = isPaymentAdvancePaymentValue;
            PatentObject.AccountingInsuranceStatus = _accountingInsuranceStatus;
            PatentObject.DateAccountingInsuranceStatus = _dateAccountingInsuranceStatus;
            PatentObject.AccountingInsuranceStatus2 = _accountingInsuranceStatus2;
            PatentObject.DateAccountingInsuranceStatus2 = _dateAccountingInsuranceStatus2;

            Patent.PatentObjects.Add(PatentObject);
            IsSave = true;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {  
            memoComment.EditValue = PatentObject.Comment;
            txtName.EditValue = PatentObject.Name;
            btnKindActivity.EditValue = PatentObject.KindActivity;
            btnKindActivity.EditValue = PatentObject.ClassOKVED2;
            dateDateSince.EditValue = PatentObject.DateSince;
            dateDateTo.EditValue = PatentObject.DateTo;
            dateActualPaymentDate.EditValue = PatentObject.ActualPaymentDate;
            dateAdvancePaymentDate.EditValue = PatentObject.AdvancePaymentDate;
            txtTaxAuthority.EditValue = PatentObject.TaxAuthority;
            btnPatentStatus.EditValue = PatentObject.PatentStatus;

            txtActualPaymentValue.EditValue = PatentObject.ActualPaymentValue;
            txtAdvancePaymentValue.EditValue = PatentObject.AdvancePaymentValue;

            checkIsPaymentActualPaymentValue.EditValue = PatentObject.IsPaymentActualPaymentValue;
            checkIsPaymentAdvancePaymentValue.EditValue = PatentObject.IsPaymentAdvancePaymentValue;

            btnAccountingInsuranceStatus1.EditValue = PatentObject.AccountingInsuranceStatus;
            dateAccountingInsuranceStatus1.EditValue = PatentObject.DateAccountingInsuranceStatus;

            btnAccountingInsuranceStatus2.EditValue = PatentObject.AccountingInsuranceStatus2;
            dateAccountingInsuranceStatus2.EditValue = PatentObject.DateAccountingInsuranceStatus2;
        }        

        private void btnKindActivity_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<ClassOKVED2>(Session, buttonEdit, (int)cls_App.ReferenceBooks.ClassOKVED2, 1, null, null, false, null, string.Empty, false, true);        
        }

        private void btnKindActivity_DoubleClick(object sender, EventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<ClassOKVED2>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.ClassOKVED2, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnPatentStatus_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<PatentStatus>(Session, buttonEdit, (int)cls_App.ReferenceBooks.PatentStatus, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnAccountingInsuranceStatus_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (sender is ButtonEdit buttonEdit)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                    return;
                }

                cls_BaseSpr.ButtonEditButtonClickBase<AccountingInsuranceStatus>(Session, buttonEdit, (int)cls_App.ReferenceBooks.AccountingInsuranceStatus, 1, null, null, false, null, string.Empty, false, true);
            }
        }
    }
}