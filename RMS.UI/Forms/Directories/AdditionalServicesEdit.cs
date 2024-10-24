using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RMS.UI.Forms.Directories
{
    public partial class AdditionalServicesEdit : formEdit_BaseSpr
    {
        private Session _session;
        private Staff _staff;
        
        public AdditionalServices AdditionalServices { get; }

        private bool isClosed => checkIsPaidStaff.Checked;

        public AdditionalServicesEdit()
        {
            InitializeComponent();

            foreach (Month item in Enum.GetValues(typeof(Month)))
            {
                cmbMonth.Properties.Items.Add(item.GetEnumDescription());
            }

            if (_session is null)
            {
                _session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                AdditionalServices = new AdditionalServices(_session);
            }

            XPObject.AutoSaveOnEndEdit = false;
        }

        public AdditionalServicesEdit(int id) : this()
        {
            if (id > 0)
            {
                _session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                AdditionalServices = _session.GetObjectByKey<AdditionalServices>(id);
            }
        }

        public AdditionalServicesEdit(Staff staff) : this()
        {
            _session = staff.Session;
            _staff = staff;
            AdditionalServices = new AdditionalServices(_session);
        }

        public AdditionalServicesEdit(AdditionalServices additionalServices) : this()
        {
            AdditionalServices = additionalServices;
            _staff = additionalServices.Staff;
            _session = additionalServices.Session;
        }
        
        private void RouteSheetEdit_Load(object sender, EventArgs e)
        {
            btnStaff.EditValue = _staff;
            cmbMonth.EditValue = AdditionalServices.Month.GetEnumDescription();
            txtYear.EditValue = AdditionalServices.Year;
            btnCustomer.EditValue = AdditionalServices.Customer;
            memoDescription.EditValue = Letter.ByteToString(AdditionalServices.Description);
            checkIsPaid.EditValue = AdditionalServices.IsPaid;
            calcValue.EditValue = AdditionalServices.Value;
            memoComment.EditValue = Letter.ByteToString(AdditionalServices.Comment);
            calcPaymentPercentage.EditValue = AdditionalServices.PaymentPercentage;
            calcValueStaff.EditValue = AdditionalServices.ValueStaff;
            checkIsPaidStaff.EditValue = AdditionalServices.IsPaidStaff;
            dateDatePaid.EditValue = AdditionalServices.DatePaid;
            
            gridControlPayments.DataSource = AdditionalServices.AdditionalServicesObj;
            
            if (gridViewPayments.Columns[nameof(AdditionalServicesObj.Oid)] is GridColumn columnOid)
            {
                columnOid.Visible = false;
                columnOid.Width = 18;
                columnOid.OptionsColumn.FixedWidth = true;
            }

            if (gridViewPayments.Columns[nameof(AdditionalServicesObj.Name)] is GridColumn columnName)
            {                
                var repositoryItemButtonEdit = new RepositoryItemButtonEdit();
                repositoryItemButtonEdit.Buttons.Add(new EditorButton()
                {
                    Kind = ButtonPredefines.Delete
                });
                repositoryItemButtonEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
                repositoryItemButtonEdit.ButtonPressed += Button_ButtonPressed;

                columnName.ColumnEdit = repositoryItemButtonEdit;
                columnName.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                //columnName.AppearanceCell.TextOptions.Trimming = Trimming.EllipsisWord;
                columnName.OptionsColumn.ReadOnly = true;
            }

            if (gridViewPayments.Columns[nameof(AdditionalServicesObj.Count)] is GridColumn columnCount)
            {
                columnCount.Width = 100;
                columnCount.OptionsColumn.FixedWidth = true;
                columnCount.DisplayFormat.FormatType = FormatType.Numeric;
                columnCount.DisplayFormat.FormatString = "d";
                columnCount.RealColumnEdit.EditFormat.FormatType = FormatType.Numeric;
                columnCount.RealColumnEdit.EditFormat.FormatString = "d";
                columnCount.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }

            if (gridViewPayments.Columns[nameof(AdditionalServicesObj.Value)] is GridColumn columnValue)
            {
                columnValue.DisplayFormat.FormatType = FormatType.Numeric;
                columnValue.DisplayFormat.FormatString = "n2";
                columnValue.RealColumnEdit.EditFormat.FormatType = FormatType.Numeric;
                columnValue.RealColumnEdit.EditFormat.FormatString = "n2";
                columnValue.Width = 150;
                columnValue.OptionsColumn.FixedWidth = true;
                
                //columnValue.OptionsColumn.ReadOnly = true;
            }

            if (AdditionalServices?.AdditionalServicesObj != null)
            {
                AdditionalServices.AdditionalServicesObj.ListChanged += AdditionalServicesObj_ListChanged;
            }

            isFormLoad = true;
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            var staff = default(Staff);
            var month = (Month)cmbMonth.SelectedIndex + 1;
            var year = DateTime.Now.Year;
            var customer = default(Customer);
            var description = memoDescription.Text;

            var isPaid = checkIsPaid.Checked;
            var value = calcValue.Value;

            var comment = memoComment.Text;

            var valueStaff = calcValueStaff.Value;
            var paymentPercentage = calcPaymentPercentage.Value;
            var isPaidStaff = checkIsPaidStaff.Checked;
            var datePaid = default(DateTime?);

            if (btnStaff.EditValue is Staff _staff)
            {
                staff = _staff;
            }
            else
            {
                XtraMessageBox.Show("Поле сотрудник обязательно для заполнения.", "Обработка объекта", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnStaff.Focus();
                return;   
            }

            if (int.TryParse(txtYear.Text, out int result) && result.ToString().Length == 4)
            {
                year = result;
            }
            else
            {
                XtraMessageBox.Show("Укажите правильный год.", "Обработка объекта", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtYear.Focus();
                return;
            }

            if (btnCustomer.EditValue is Customer _customer)
            {
                customer = _customer;
            }

            if (dateDatePaid.EditValue is DateTime _datePaid)
            {
                datePaid = _datePaid;
            }

            AdditionalServices.Staff = staff;
            AdditionalServices.Month = month;
            AdditionalServices.Year = year;
            AdditionalServices.Customer = customer;
            AdditionalServices.Description = Letter.StringToByte(description);
            AdditionalServices.IsPaid = isPaid;
            AdditionalServices.Value = value;
            AdditionalServices.ValueStaff = valueStaff;
            AdditionalServices.Comment = Letter.StringToByte(comment);
            AdditionalServices.PaymentPercentage = paymentPercentage;
            AdditionalServices.IsPaidStaff = isPaidStaff;
            AdditionalServices.DatePaid = datePaid;
            
            if (AdditionalServices?.AdditionalServicesObj != null)
            {
                foreach (var item in AdditionalServices?.AdditionalServicesObj)
                {
                    item.Save();
                }
            }
            AdditionalServices.Save();

            id = AdditionalServices.Oid;
            flagSave = true;
            Close();
        }

        public void GetValueStaff()
        {
            if (isClosed)
            {
                return;
            }
            
            var result = calcValue.Value;

            var additionalServicesObj = AdditionalServices.AdditionalServicesObj;
            if (additionalServicesObj != null && additionalServicesObj.Count > 0)
            {
                foreach (var item in additionalServicesObj)
                {
                    result += item.Value;
                }
            }

            result *= (calcPaymentPercentage.Value / 100);
            calcValueStaff.Value = result;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
                
        private void Button_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (gridViewPayments.GetRow(gridViewPayments.FocusedRowHandle) is AdditionalServicesObj obj)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    obj.PriceList = null;
                    gridViewPayments.UpdateCurrentRow();
                    return;
                }

                if (e.Button.Kind == ButtonPredefines.Ellipsis)
                {
                    var id = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.PriceList);
                    if (id > 0)
                    {
                        var priceList = _session.GetObjectByKey<PriceList>(id);
                        if (priceList != null)
                        {
                            obj.PriceList = priceList;
                        }
                    }
                    gridViewPayments.UpdateCurrentRow();
                    return;
                }
            }
        }
        
        private void barBtnDelPayment_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewPayments.IsEmpty)
            {
                return;
            }
            if (gridViewPayments.GetRow(gridViewPayments.FocusedRowHandle) is AdditionalServicesObj obj)
            {
                if (XtraMessageBox.Show($"Вы действительно хотите удалить следующий платеж:{Environment.NewLine}{obj}?",
                                        "Удаление объекта",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {
                    AdditionalServices.AdditionalServicesObj.Remove(obj);
                }
            }
        }

        private void barBtnAddPayment_ItemClick(object sender, ItemClickEventArgs e)
        {
            var id = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.PriceList);
            if (id > 0)
            {
                var priceList = _session.GetObjectByKey<PriceList>(id);
                if (priceList != null)
                {
                    var obj = new AdditionalServicesObj(_session)
                    {
                        PriceList = priceList 
                    };
                    obj.Value = obj.GetValue();
                    AdditionalServices.AdditionalServicesObj.Add(obj);
                }
            }            
        }

        private void RouteSheetEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            AdditionalServices.Reload();
            AdditionalServices.AdditionalServicesObj.Reload();
            foreach (var obj in AdditionalServices.AdditionalServicesObj)
            {
                obj.Reload();
            }
            XPObject.AutoSaveOnEndEdit = true;
        }

        private void gridViewPayments_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (isClosed)
                {
                    return;
                }

                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (gridView.GetRow(gridView.FocusedRowHandle) is AdditionalServicesObj)
                    {
                        barBtnDelPayment.Enabled = true;
                    }
                    else
                    {
                        barBtnDelPayment.Enabled = false;
                    }

                    
                    
                    popupMenuPayments.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void btnStaff_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (sender is ButtonEdit buttonEdit)
            {
                if (e?.Button?.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                    return;
                }

                cls_BaseSpr.ButtonEditButtonClickBase<Staff>(_session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, null, null, false, null, string.Empty, false, true);
            }
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (sender is ButtonEdit buttonEdit)
            {
                if (e?.Button?.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                    return;
                }

                cls_BaseSpr.ButtonEditButtonClickBase<Customer>(_session, buttonEdit, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
            }
        }

        private bool isFormLoad;
        private void calcValue_EditValueChanged(object sender, EventArgs e)
        {
            if (isFormLoad)
            {
                GetValueStaff();
            }
        }

        private void calcPaymentPercentage_EditValueChanged(object sender, EventArgs e)
        {
            if (isFormLoad)
            {
                GetValueStaff();
            }
        }

        private void AdditionalServicesObj_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            if (isFormLoad)
            {
                GetValueStaff();
            }
        }

        private void checkIsPaidStaff_CheckedChanged(object sender, EventArgs e)
        {
            if (isClosed)
            {
                cmbMonth.Enabled = false;
                txtYear.Enabled = false;
                btnStaff.Enabled = false;
                btnCustomer.Enabled = false;
                memoDescription.Enabled = false;
                calcValue.Enabled = false;
                memoComment.Enabled = false;
                calcPaymentPercentage.Enabled = false;
                
                calcValueStaff.ReadOnly = true;

                gridViewPayments.OptionsBehavior.Editable = false;
            }
            else
            {
                cmbMonth.Enabled = true;
                txtYear.Enabled = true;
                btnStaff.Enabled = true;
                btnCustomer.Enabled = true;
                memoDescription.Enabled = true;
                calcValue.Enabled = true;
                memoComment.Enabled = true;
                calcPaymentPercentage.Enabled = true;

                calcValueStaff.ReadOnly = false;

                gridViewPayments.OptionsBehavior.Editable = true;
            }
        }

        private void gridViewPayments_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.Column.FieldName == nameof(AdditionalServicesObj.Count))
                {
                    if (gridView.GetRow(e.RowHandle) is AdditionalServicesObj obj)
                    {
                        obj.Value = obj.GetValue();
                    }
                }
            }
        }
    }
}