using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle.Taxes;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Taxes;
using RMS.UI.Forms.Chronicle;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RMS.UI.Forms.Taxes
{
    public partial class PatentEdit2 : XtraForm
    {
        private Session Session { get; }
        private Tax Tax { get; }
        private Customer Customer { get; }
        public Patent Patent { get; }
        public PatentObject CurrentPatentObject { get; private set; }

        private PatentEdit2(string btnSaveCaption = default)
        {
            InitializeComponent();

            if (!string.IsNullOrWhiteSpace(btnSaveCaption))
            {
                btnSave.Text = btnSaveCaption;
            }
            
            XPObject.AutoSaveOnEndEdit = false;
            BVVGlobal.oFuncXpo.PressEnterGrid<PatentObject, PatentObjectEdit>(gridView);
        }
        
        public PatentEdit2(Patent patent, Customer customer, string btnSaveCaption = default) : this(btnSaveCaption)
        {
            Session = patent.Session;
            Customer = customer;
            Tax = Customer.Tax;
            Patent = patent;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Patent.Comment = memoComment.Text;
            Patent.Save();
            Tax.Save();
            Customer.Save();

            var obj = gridView.GetRow(gridView.FocusedRowHandle) as PatentObject;
            if (obj != null)
            {
                CurrentPatentObject = obj;
            }
            
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            btnCustomer.EditValue = Customer;           
            
            gridControl.DataSource = Patent.PatentObjects;
            if (gridView.Columns[nameof(PatentObject.Oid)] != null)
            {
                gridView.Columns[nameof(PatentObject.Oid)].Visible = false;
                gridView.Columns[nameof(PatentObject.Oid)].Width = 18;
                gridView.Columns[nameof(PatentObject.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(PatentObject.AdvancePaymentValue)] != null)
            {
                gridView.Columns[nameof(PatentObject.AdvancePaymentValue)].DisplayFormat.FormatType = FormatType.Numeric;
                gridView.Columns[nameof(PatentObject.AdvancePaymentValue)].DisplayFormat.FormatString = "n2";
            }
            
            if (gridView.Columns[nameof(PatentObject.ActualPaymentValue)] != null)
            {
                gridView.Columns[nameof(PatentObject.ActualPaymentValue)].DisplayFormat.FormatType = FormatType.Numeric;
                gridView.Columns[nameof(PatentObject.ActualPaymentValue)].DisplayFormat.FormatString = "n2";
            }

            Patent.ChroniclePatents?.Reload();
            gridControlChronicle.DataSource = Patent.ChroniclePatents;
            if (gridViewChronicle.Columns[nameof(ChroniclePatent.Oid)] != null)
            {
                gridViewChronicle.Columns[nameof(ChroniclePatent.Oid)].Visible = false;
                gridViewChronicle.Columns[nameof(ChroniclePatent.Oid)].Width = 18;
                gridViewChronicle.Columns[nameof(ChroniclePatent.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridViewChronicle.Columns[nameof(PatentObject.AdvancePaymentValue)] != null)
            {
                gridViewChronicle.Columns[nameof(PatentObject.AdvancePaymentValue)].DisplayFormat.FormatType = FormatType.Numeric;
                gridViewChronicle.Columns[nameof(PatentObject.AdvancePaymentValue)].DisplayFormat.FormatString = "n2";
            }

            if (gridViewChronicle.Columns[nameof(PatentObject.ActualPaymentValue)] != null)
            {
                gridViewChronicle.Columns[nameof(PatentObject.ActualPaymentValue)].DisplayFormat.FormatType = FormatType.Numeric;
                gridViewChronicle.Columns[nameof(PatentObject.ActualPaymentValue)].DisplayFormat.FormatString = "n2";
            }

            memoComment.EditValue = Patent.Comment;
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnChronicle_Click(object sender, EventArgs e)
        {
            var form = new ChronicleTaxesForm<ChroniclePatent>(Patent.ChroniclePatents, "Патент");
            form.ShowDialog();
        }

        private void btnKindActivity_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            KindActivityEdit kindActivityEdit = default;
            if (buttonEdit.EditValue is KindActivity kindActivity)
            {
                kindActivityEdit = new KindActivityEdit(kindActivity);
            }
            else
            {
                kindActivityEdit = new KindActivityEdit(Customer);
            }
            kindActivityEdit.ShowDialog();

            if (kindActivityEdit.FlagSave)
            {
                buttonEdit.EditValue = kindActivityEdit.KindActivity;
            }            
        }

        private void gridView_MouseDown(object sender, MouseEventArgs e)
        {
            DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            if (dxMouseEventArgs.Button == MouseButtons.Right)
            {
                popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void barBtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var form = new PatentObjectEdit(Patent);
            form.ShowDialog();
        }

        private void barBtnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var obj = gridView.GetRow(gridView.FocusedRowHandle) as PatentObject;
            if (obj != null)
            {
                var form = new PatentObjectEdit(obj);
                form.ShowDialog();
            }
        }

        private void barBtnDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var obj = gridView.GetRow(gridView.FocusedRowHandle) as PatentObject;
            if (obj != null)
            {
                if (XtraMessageBox.Show($"Вы точно хотите удалить объект: {obj}",
                                        "Удаление объекта",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {
                    obj.Delete();
                }
            }
        }

        private void PatentEdit2_FormClosed(object sender, FormClosedEventArgs e)
        {
            XPObject.AutoSaveOnEndEdit = true;
            Patent?.Reload();
            Patent?.PatentObjects?.Reload();
        }
    }
}