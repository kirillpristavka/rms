using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Model.Salary;
using RMS.Core.Model.SalaryStaff;
using System;
using System.Windows.Forms;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class СalculationEdit : formEdit_BaseSpr
    {        
        private Session Session { get; }
        private Сalculation Сalculation { get; }

        public СalculationEdit(Session session = null)
        {
            InitializeComponent();

            Session = session;
            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
            }

            Сalculation = new Сalculation(Session);
        }
                
        public СalculationEdit(int id) : this()
        {
            if (id > 0)
            {
                Сalculation = Session.GetObjectByKey<Сalculation>(id);
            }
        }

        public СalculationEdit(Сalculation calculation) : this(calculation.Session)
        {
            Session = calculation.Session;
            Сalculation = calculation;
        }        

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            btnPayoutDictionary.EditValue = Сalculation.PayoutDictionary;
            spinValue.EditValue = Сalculation.Value;
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (btnPayoutDictionary.EditValue is PayoutDictionary payoutDictionary)
            {
                Сalculation.PayoutDictionary = payoutDictionary;
            }
            else
            {
                XtraMessageBox.Show("Укажите выплату", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
                       

            if (decimal.TryParse(spinValue.Text, out decimal result))
            {
                Сalculation.Value = result;
            }
            else
            {
                Сalculation.Value = 0;
            }

            Session.Save(Сalculation);
            id = Сalculation.Oid;
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