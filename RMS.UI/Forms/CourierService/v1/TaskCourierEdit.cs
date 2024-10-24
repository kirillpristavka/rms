using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Windows.Forms;

namespace RMS.UI.Forms.CourierService.v1
{
    public partial class TaskCourierEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private RouteSheet RouteSheet { get; }
        public TaskCourier TaskCourier { get; }
        private DateTime Date { get; }
        private Individual Courier { get; }

        public TaskCourierEdit()
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
                TaskCourier = new TaskCourier(Session);
                Date = DateTime.Now.Date;
            }
        }


        public TaskCourierEdit(int id) : this()
        {
            if (id > 0)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                TaskCourier = Session.GetObjectByKey<TaskCourier>(id);
                RouteSheet = TaskCourier.RouteSheet;
                Date = TaskCourier.Date ?? DateTime.Now.Date;
                Courier = TaskCourier.Courier;
            }
        }

        public TaskCourierEdit(TaskCourier taskCourier) : this()
        {
            TaskCourier = taskCourier;
            RouteSheet = taskCourier.RouteSheet;
            Date = taskCourier.Date ?? DateTime.Now.Date;
            Courier = taskCourier.Courier;
            Session = taskCourier.Session;
        }

        public TaskCourierEdit(RouteSheet routeSheet) : this()
        {
            RouteSheet = routeSheet;
            Date = routeSheet.Date;
            Session = routeSheet.Session;
            Courier = routeSheet.Courier;
            TaskCourier = new TaskCourier(Session);

            btnCourier.Enabled = false;
            btnRouteSheet.Enabled = false;
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

            var courier = btnCourier.EditValue as Individual;
            if (courier == null)
            {
                XtraMessageBox.Show("Для сохранение курьерской задачи укажите курьера.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCourier.Focus();
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
                TaskCourier.DateTransfer = Convert.ToDateTime(dateTransfer.EditValue).Date;
            }
            else
            {
                TaskCourier.DateTransfer = null;
            }

            var routeSheet = btnRouteSheet.EditValue as RouteSheet;
            var accountantResponsible = btnAccountantResponsible.EditValue as Staff;
            var statusTaskCourier = (StatusTaskCourier)cmbStatusTaskCourier.SelectedIndex;
            var address = memoAddress.Text;
            var purposeTrip = memoPurposeTrip.Text;
            var value = Convert.ToDecimal(calcValue.EditValue);
            var valueNonCash = Convert.ToDecimal(calcValueNonCash.EditValue);

            if (TaskCourier.Oid != -1 &&
                (
                    TaskCourier.RouteSheet != routeSheet ||
                    TaskCourier.Customer != customer ||
                    TaskCourier.Courier != courier ||
                    TaskCourier.AccountantResponsible != accountantResponsible ||
                    TaskCourier.Address != address ||
                    TaskCourier.PurposeTrip != purposeTrip ||
                    TaskCourier.Date != date ||
                    TaskCourier.StatusTaskCourier != statusTaskCourier ||
                    TaskCourier.Value != value ||
                    TaskCourier.ValueNonCash != valueNonCash
                ))
            {
                TaskCourier.ChronicleTaskCouriers.Add(new ChronicleTaskCourier(Session)
                {
                    RouteSheet = TaskCourier.RouteSheet,
                    Customer = TaskCourier.Customer,
                    Courier = TaskCourier.Courier,
                    AccountantResponsible = TaskCourier.AccountantResponsible,
                    Address = TaskCourier.Address,
                    PurposeTrip = TaskCourier.PurposeTrip,
                    Date = TaskCourier.Date,
                    StatusTaskCourier = TaskCourier.StatusTaskCourier,
                    Value = TaskCourier.Value,
                    ValueNonCash = TaskCourier.ValueNonCash,
                    UserUpdate = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid),
                    DateUpdate = DateTime.Now
                });
            }

            if (routeSheet != null)
            {
                TaskCourier.RouteSheet = routeSheet;
            }

            if (accountantResponsible != null)
            {
                TaskCourier.AccountantResponsible = accountantResponsible;
            }

            TaskCourier.Customer = customer;
            TaskCourier.Courier = courier;
            TaskCourier.Date = date;
            TaskCourier.StatusTaskCourier = statusTaskCourier;
            TaskCourier.Address = address;
            TaskCourier.PurposeTrip = purposeTrip;
            TaskCourier.Value = value;
            TaskCourier.ValueNonCash = valueNonCash;

            if (TaskCourier.UserCreate is null)
            {
                TaskCourier.UserCreate = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid);
                TaskCourier.DateCreate = DateTime.Now;
            }

            TaskCourier.Save();
            id = TaskCourier.Oid;
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

        private void TaskCourierEdit_Load(object sender, EventArgs e)
        {
            btnAccountantResponsible.EditValue = TaskCourier.Customer?.AccountantResponsible;
            btnRouteSheet.EditValue = RouteSheet;
            btnCustomer.EditValue = TaskCourier.Customer;
            btnCourier.EditValue = Courier;
            dateDate.EditValue = Date;
            cmbStatusTaskCourier.SelectedIndex = (int)TaskCourier.StatusTaskCourier;
            calcValue.EditValue = TaskCourier.Value;
            calcValueNonCash.EditValue = TaskCourier.ValueNonCash;
            memoAddress.EditValue = TaskCourier.Address;
            memoPurposeTrip.EditValue = TaskCourier.PurposeTrip;
            dateTransfer.EditValue = TaskCourier.DateTransfer;

            gridControlChronicleTaskCourier.DataSource = TaskCourier.ChronicleTaskCouriers;

            if (gridViewChronicleTaskCourier.Columns[nameof(ChronicleTaskCourier.Oid)] != null)
            {
                gridViewChronicleTaskCourier.Columns[nameof(ChronicleTaskCourier.Oid)].Visible = false;
                gridViewChronicleTaskCourier.Columns[nameof(ChronicleTaskCourier.Oid)].Width = 18;
                gridViewChronicleTaskCourier.Columns[nameof(ChronicleTaskCourier.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridViewChronicleTaskCourier.Columns[nameof(ChronicleTaskCourier.AccountantResponsibleString)] != null)
            {
                gridViewChronicleTaskCourier.Columns[nameof(ChronicleTaskCourier.AccountantResponsibleString)].Visible = false;
            }

            if (gridViewChronicleTaskCourier.Columns[nameof(ChronicleTaskCourier.CustomerString)] != null)
            {
                gridViewChronicleTaskCourier.Columns[nameof(ChronicleTaskCourier.CustomerString)].Visible = false;
            }

            if (gridViewChronicleTaskCourier.Columns[nameof(ChronicleTaskCourier.Address)] != null)
            {
                gridViewChronicleTaskCourier.Columns[nameof(ChronicleTaskCourier.Address)].Visible = false;
            }

            if (gridViewChronicleTaskCourier.Columns[nameof(ChronicleTaskCourier.PurposeTrip)] != null)
            {
                gridViewChronicleTaskCourier.Columns[nameof(ChronicleTaskCourier.PurposeTrip)].Visible = false;
            }

            if (gridViewChronicleTaskCourier.Columns[nameof(ChronicleTaskCourier.CourierString)] != null)
            {
                gridViewChronicleTaskCourier.Columns[nameof(ChronicleTaskCourier.CourierString)].Visible = false;
            }

            if (gridViewChronicleTaskCourier.Columns[nameof(ChronicleTaskCourier.Value)] != null)
            {
                gridViewChronicleTaskCourier.Columns[nameof(ChronicleTaskCourier.Value)].Visible = false;
            }

            if (gridViewChronicleTaskCourier.Columns[nameof(ChronicleTaskCourier.ValueNonCash)] != null)
            {
                gridViewChronicleTaskCourier.Columns[nameof(ChronicleTaskCourier.ValueNonCash)].Visible = false;
            }
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

            var criteriaRouteSheet = new DevExpress.Data.Filtering.BinaryOperator(nameof(RouteSheet.IsClosed), false);
            cls_BaseSpr.ButtonEditButtonClickBase<RouteSheet>(Session, buttonEdit, (int)cls_App.ReferenceBooks.RouteSheet, 1, criteriaRouteSheet, null, false, null, string.Empty, false, true);
        }
    }
}