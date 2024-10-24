using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Model;
using RMS.Core.Model.Salary;
using RMS.Core.Model.SalaryStaff;
using System;
using System.Windows.Forms;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class BasisEdit : formEdit_BaseSpr
    {
        private Staff staff;
        
        private Session Session { get; }
        private Basis Basis { get; }

        public BasisEdit(Session session = null)
        {
            InitializeComponent();

            Session = session;
            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
            }

            Basis = new Basis(Session);
        }
                
        public BasisEdit(int id) : this()
        {
            if (id > 0)
            {
                Basis = Session.GetObjectByKey<Basis>(id);
                staff = Basis.Staff;
            }
        }

        public BasisEdit(Basis basis) : this(basis.Session)
        {
            Session = basis.Session;
            Basis = basis;
            staff = basis.Staff;
        }

        public BasisEdit(Staff staff) : this(staff.Session)
        {
            Session = staff.Session;
            this.staff = staff;
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            btnPayoutDictionary.EditValue = Basis.PayoutDictionary;
            dateSince.EditValue = Basis.DateSince;
            dateTo.EditValue = Basis.DateTo;
            spinRate.EditValue = Basis.Rate;
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (staff is null)
            {
                XtraMessageBox.Show("Не найден сотрудник", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else
            {
                Basis.Staff = staff;
            }
            
            if (btnPayoutDictionary.EditValue is PayoutDictionary payoutDictionary)
            {
                Basis.PayoutDictionary = payoutDictionary;
            }
            else
            {
                XtraMessageBox.Show("Укажите выплату", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if (dateSince.EditValue is DateTime _dateSince)
            {
                Basis.DateSince = _dateSince;
            }
            else
            {
                Basis.DateSince = null;
            }

            if (dateTo.EditValue is DateTime _dateTo)
            {
                Basis.DateTo = _dateTo;
            }
            else
            {
                Basis.DateTo = null;
            }

            if (decimal.TryParse(spinRate.Text, out decimal result))
            {
                Basis.Rate = result;
            }
            else
            {
                Basis.Rate = 0;
            }

            Session.Save(Basis);
            id = Basis.Oid;
            flagSave = true;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }        

        private void btnPayoutDictionary_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<PayoutDictionary>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.PayoutDictionary, 1, null, null, false, null, string.Empty, false, true);
        }
    }
}