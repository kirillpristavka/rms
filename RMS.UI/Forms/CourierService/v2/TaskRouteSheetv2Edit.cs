using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.CourierService;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Windows.Forms;

namespace RMS.UI.Forms.CourierService.v2
{
    public partial class TaskRouteSheetv2Edit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private RouteSheetv2 RouteSheetv2 { get; }
        public TaskRouteSheetv2 TaskRouteSheetv2 { get; }
        private DateTime Date { get; }

        public TaskRouteSheetv2Edit()
        {
            InitializeComponent();

            foreach (StatusTaskCourier item in Enum.GetValues(typeof(StatusTaskCourier)))
            {
                cmbStatusTaskCourier.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbStatusTaskCourier.SelectedIndex = 0;

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                TaskRouteSheetv2 = new TaskRouteSheetv2(Session);
                Date = DateTime.Now.Date;
            }
        }


        public TaskRouteSheetv2Edit(int id) : this()
        {
            if (id > 0)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                TaskRouteSheetv2 = Session.GetObjectByKey<TaskRouteSheetv2>(id);
                RouteSheetv2 = TaskRouteSheetv2.RouteSheetv2;
                Date = TaskRouteSheetv2.Date ?? DateTime.Now.Date;
            }
        }

        public TaskRouteSheetv2Edit(TaskRouteSheetv2 taskCourier) : this()
        {
            TaskRouteSheetv2 = taskCourier;
            RouteSheetv2 = taskCourier.RouteSheetv2;
            Date = taskCourier.Date ?? DateTime.Now.Date;
            Session = taskCourier.Session;
        }

        public TaskRouteSheetv2Edit(RouteSheetv2 routeSheet) : this()
        {
            RouteSheetv2 = routeSheet;
            Date = routeSheet.Date;
            Session = routeSheet.Session;
            TaskRouteSheetv2 = new TaskRouteSheetv2(Session);
            
            btnRouteSheetv2.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var customer = btnCustomer.EditValue as Customer;
            if (customer == null)
            {
                XtraMessageBox.Show("Для сохранение курьерской задачи укажите клиента.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCustomer.Focus();
                return;
            } 
            
            var date = default(DateTime);
            if (dateDate.EditValue != null)
            {
                date = Convert.ToDateTime(dateDate.EditValue).Date;
            }
            else
            {
                XtraMessageBox.Show("Для сохранение курьерской задачи укажите дату.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dateDate.Focus();
                return;
            }

            if (dateTransfer.EditValue != null)
            {
                TaskRouteSheetv2.DateTransfer = Convert.ToDateTime(dateTransfer.EditValue).Date;
            }
            else
            {
                TaskRouteSheetv2.DateTransfer = null;
            }

            var routeSheet = btnRouteSheetv2.EditValue as RouteSheetv2;
            var accountantResponsible = btnAccountantResponsible.EditValue as Staff;
            var statusTaskCourier = (StatusTaskCourier)cmbStatusTaskCourier.SelectedIndex;
            var address = memoAddress.Text;
            var purposeTrip = memoPurposeTrip.Text;
            var value = Convert.ToDecimal(calcValue.EditValue);
            var valueNonCash = Convert.ToDecimal(calcValueNonCash.EditValue);
            var courier = btnCourier.EditValue as Individual;

            if (TaskRouteSheetv2.Oid != -1 &&
                (
                    TaskRouteSheetv2.RouteSheetv2 != routeSheet ||
                    TaskRouteSheetv2.Customer != customer ||
                    TaskRouteSheetv2.AccountantResponsible != accountantResponsible ||
                    TaskRouteSheetv2.Address != address ||
                    TaskRouteSheetv2.PurposeTrip != purposeTrip ||
                    TaskRouteSheetv2.Date != date ||
                    TaskRouteSheetv2.StatusTaskCourier != statusTaskCourier ||
                    TaskRouteSheetv2.Value != value ||
                    TaskRouteSheetv2.ValueNonCash != valueNonCash ||
                    TaskRouteSheetv2.Courier != courier
                ))
            {
                TaskRouteSheetv2.ChronicleTaskRouteSheetv2.Add(new ChronicleTaskRouteSheetv2(Session)
                {
                    RouteSheetv2 = TaskRouteSheetv2.RouteSheetv2,
                    Customer = TaskRouteSheetv2.Customer,
                    AccountantResponsible = TaskRouteSheetv2.AccountantResponsible,
                    Address = TaskRouteSheetv2.Address,
                    PurposeTrip = TaskRouteSheetv2.PurposeTrip,
                    Date = TaskRouteSheetv2.Date,
                    StatusTaskCourier = TaskRouteSheetv2.StatusTaskCourier,
                    Value = TaskRouteSheetv2.Value,
                    ValueNonCash = TaskRouteSheetv2.ValueNonCash,
                    Courier = TaskRouteSheetv2.Courier,
                    UserUpdate = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid),
                    DateUpdate = DateTime.Now                    
                });
            }

            if (routeSheet != null)
            {
                TaskRouteSheetv2.RouteSheetv2 = routeSheet;
            }

            if (accountantResponsible != null)
            {
                TaskRouteSheetv2.AccountantResponsible = accountantResponsible;
            }

            TaskRouteSheetv2.Customer = customer;
            TaskRouteSheetv2.Date = date;
            TaskRouteSheetv2.StatusTaskCourier = statusTaskCourier;
            TaskRouteSheetv2.Address = address;
            TaskRouteSheetv2.PurposeTrip = purposeTrip;
            TaskRouteSheetv2.Value = value;
            TaskRouteSheetv2.ValueNonCash = valueNonCash;
            TaskRouteSheetv2.Courier = courier;

            if (TaskRouteSheetv2.UserCreate is null)
            {
                TaskRouteSheetv2.UserCreate = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid);
                TaskRouteSheetv2.DateCreate = DateTime.Now;
            }

            TaskRouteSheetv2.Save();
            id = TaskRouteSheetv2.Oid;
            flagSave = true;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCustomer_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            btnAccountantResponsible.EditValue = TaskRouteSheetv2.Customer?.AccountantResponsible;
            btnRouteSheetv2.EditValue = RouteSheetv2;
            btnCustomer.EditValue = TaskRouteSheetv2.Customer;
            dateDate.EditValue = Date;
            cmbStatusTaskCourier.SelectedIndex = (int)TaskRouteSheetv2.StatusTaskCourier;
            calcValue.EditValue = TaskRouteSheetv2.Value;
            calcValueNonCash.EditValue = TaskRouteSheetv2.ValueNonCash;
            memoAddress.EditValue = TaskRouteSheetv2.Address;
            memoPurposeTrip.EditValue = TaskRouteSheetv2.PurposeTrip;
            dateTransfer.EditValue = TaskRouteSheetv2.DateTransfer;
            btnCourier.EditValue = TaskRouteSheetv2.Courier;

            gridControlChronicleTaskRouteSheetv2.DataSource = TaskRouteSheetv2.ChronicleTaskRouteSheetv2;

            if (gridViewChronicleTaskRouteSheetv2.Columns[nameof(ChronicleTaskRouteSheetv2.Oid)] != null)
            {
                gridViewChronicleTaskRouteSheetv2.Columns[nameof(ChronicleTaskRouteSheetv2.Oid)].Visible = false;
                gridViewChronicleTaskRouteSheetv2.Columns[nameof(ChronicleTaskRouteSheetv2.Oid)].Width = 18;
                gridViewChronicleTaskRouteSheetv2.Columns[nameof(ChronicleTaskRouteSheetv2.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridViewChronicleTaskRouteSheetv2.Columns[nameof(ChronicleTaskRouteSheetv2.AccountantResponsibleString)] != null)
            {
                gridViewChronicleTaskRouteSheetv2.Columns[nameof(ChronicleTaskRouteSheetv2.AccountantResponsibleString)].Visible = false;
            }

            if (gridViewChronicleTaskRouteSheetv2.Columns[nameof(ChronicleTaskRouteSheetv2.CustomerString)] != null)
            {
                gridViewChronicleTaskRouteSheetv2.Columns[nameof(ChronicleTaskRouteSheetv2.CustomerString)].Visible = false;
            }

            if (gridViewChronicleTaskRouteSheetv2.Columns[nameof(ChronicleTaskRouteSheetv2.Address)] != null)
            {
                gridViewChronicleTaskRouteSheetv2.Columns[nameof(ChronicleTaskRouteSheetv2.Address)].Visible = false;
            }

            if (gridViewChronicleTaskRouteSheetv2.Columns[nameof(ChronicleTaskRouteSheetv2.PurposeTrip)] != null)
            {
                gridViewChronicleTaskRouteSheetv2.Columns[nameof(ChronicleTaskRouteSheetv2.PurposeTrip)].Visible = false;
            }

            if (gridViewChronicleTaskRouteSheetv2.Columns[nameof(ChronicleTaskRouteSheetv2.Value)] != null)
            {
                gridViewChronicleTaskRouteSheetv2.Columns[nameof(ChronicleTaskRouteSheetv2.Value)].Visible = false;
            }

            if (gridViewChronicleTaskRouteSheetv2.Columns[nameof(ChronicleTaskRouteSheetv2.ValueNonCash)] != null)
            {
                gridViewChronicleTaskRouteSheetv2.Columns[nameof(ChronicleTaskRouteSheetv2.ValueNonCash)].Visible = false;
            }

            if (gridViewChronicleTaskRouteSheetv2.Columns[nameof(ChronicleTaskRouteSheetv2.Courier)] != null)
            {
                gridViewChronicleTaskRouteSheetv2.Columns[nameof(ChronicleTaskRouteSheetv2.Courier)].Visible = false;
            }
        }
        
        private void btnAccountantResponsible_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
        }

        private void btnRouteSheet_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            var criteriaRouteSheet = new BinaryOperator(nameof(RouteSheetv2.IsClosed), false);
            cls_BaseSpr.ButtonEditButtonClickBase<RouteSheetv2>(Session, buttonEdit, (int)cls_App.ReferenceBooks.RouteSheetv2, 1, criteriaRouteSheet, null, false, null, string.Empty, false, true);
        }

        private void btnCourier_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Individual>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Individual, 1, null, null, false, null, string.Empty, false, true);
        }
    }
}