using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.CourierService;
using RMS.Core.Model.InfoContract;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using RMS.Core.Model.Reports;
using RMS.Core.Model.Salary;
using RMS.Core.Model.Taxes;
using RMS.UI.Forms.CourierService.v1;
using RMS.UI.Forms.CourierService.v2;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.Mail;
using RMS.UI.Forms.ReferenceBooks;
using System;

namespace RMS.UI.Forms
{
    public partial class ProgramEventEdit : XtraForm
    {
        public ProgramEvent ProgramEvent { get; private set; }

        private XPObject obj;
        private Session session;

        private string nameModel;
        private Type objType;
        private Type objTypeEdit;
        private int referenceBookId = -1;

        public ProgramEventEdit()
        {
            InitializeComponent();

            if (session is null)
            {
                session = DatabaseConnection.GetWorkSession();
                ProgramEvent = new ProgramEvent(session)
                {
                    Staff = session.GetObjectByKey<Staff>(DatabaseConnection.User?.Staff?.Oid)
                };
            }
        }

        public ProgramEventEdit(object obj) : this()
        {
            if (obj is XPObject xpObj)
            {
                this.obj = xpObj;
                session = xpObj.Session;

                GetTypeObj(xpObj.GetType());
                
                ProgramEvent.TypeName = objType.Name;
                ProgramEvent.NameModel = nameModel;
                ProgramEvent.ControlSystemObjectId = xpObj.Oid;
            }
            else
            {
                throw new ArgumentException(nameof(obj));
            }
        }

        public ProgramEventEdit(ProgramEvent programEvent) : this()
        {
            ProgramEvent = programEvent;
            session = programEvent.Session;
            GetTypeObj(ProgramEvent.TypeName);
            obj = (XPObject)session.GetObjectByKey(objType, programEvent.ControlSystemObjectId);
        }

        private void GetTypeObj(Type type)
        {
            var name = type.Name;
            GetTypeObj(name);
        }

        private void GetTypeObj(string name)
        {
            switch (name)
            {
                case nameof(Customer):
                    objType = typeof(Customer);
                    objTypeEdit = typeof(CustomerEdit);
                    nameModel = "Клиенты";
                    referenceBookId = (int)cls_App.ReferenceBooks.Customer;
                    break;

                case nameof(BankAccess):
                    objType = typeof(BankAccess);
                    objTypeEdit = typeof(BankAccessEdit);
                    nameModel = "Доступ в банк";
                    break;

                case nameof(ForeignEconomicActivity):
                    objType = typeof(ForeignEconomicActivity);
                    objTypeEdit = typeof(ForeignEconomicActivityEdit);
                    nameModel = "Внешнеэкономическая деятельность";
                    break;

                case nameof(OrganizationPerformance):
                    objType = typeof(OrganizationPerformance);
                    objTypeEdit = typeof(OrganizationPerformanceEdit);
                    nameModel = "Показатели работы организации";
                    break;

                case nameof(StatisticalReport):
                    objType = typeof(StatisticalReport);
                    objTypeEdit = typeof(StatisticalReportEdit);
                    nameModel = "Статистические отчеты клиента";
                    break;

                case nameof(CustomerEmploymentHistory):
                    objType = typeof(CustomerEmploymentHistory);
                    objTypeEdit = typeof(EmploymentHistoryEdit);
                    nameModel = "Трудовые книжки";
                    break;

                case nameof(Contract):
                    objType = typeof(Contract);
                    objTypeEdit = typeof(ContractEdit);
                    nameModel = "Договора";
                    break;

                case nameof(Task):
                    objType = typeof(Task);
                    objTypeEdit = typeof(TaskEdit);
                    nameModel = "Задачи";
                    break;

                case nameof(Staff):
                    objType = typeof(Staff);
                    objTypeEdit = typeof(StaffEdit);
                    nameModel = "Сотрудники";
                    referenceBookId = (int)cls_App.ReferenceBooks.Staff;
                    break;

                case nameof(ReportChange):
                    objType = typeof(ReportChange);
                    objTypeEdit = typeof(ReportChangeEdit);
                    nameModel = "Отчеты";
                    break;

                case nameof(PreTax):
                    objType = typeof(PreTax);
                    objTypeEdit = typeof(PreTaxEdit);
                    nameModel = "Предварительные налоги";
                    break;

                case nameof(CustomerSalaryAdvance):
                    objType = typeof(CustomerSalaryAdvance);
                    objTypeEdit = typeof(CustomerSalaryAdvanceEdit);
                    nameModel = "ЗП / Аванс";
                    break;

                case nameof(IndividualEntrepreneursTax):
                    objType = typeof(IndividualEntrepreneursTax);
                    objTypeEdit = typeof(IndividualEntrepreneursTaxEdit);
                    nameModel = "ИП страховые/ ПФР / Патенты";
                    break;

                case nameof(Deal):
                    objType = typeof(Deal);
                    objTypeEdit = typeof(DealEdit);
                    nameModel = "Сделки";
                    break;

                case nameof(Invoice):
                    objType = typeof(Invoice);
                    objTypeEdit = typeof(InvoiceEdit);
                    nameModel = "Счета";
                    break;

                case nameof(Letter):
                    objType = typeof(Letter);
                    objTypeEdit = typeof(LetterEdit);
                    nameModel = "Письма";
                    break;

                case nameof(ArchiveFolderChange):
                    objType = typeof(ArchiveFolderChange);
                    objTypeEdit = typeof(ArchiveFolderChangeEdit);
                    nameModel = "Архивные папки";
                    break;

                case nameof(RouteSheet):
                    objType = typeof(RouteSheet);
                    objTypeEdit = typeof(RouteSheetEdit);
                    nameModel = "Маршрутные листы";
                    referenceBookId = (int)cls_App.ReferenceBooks.RouteSheet;
                    break;               

                case nameof(TaskCourier):
                    objType = typeof(TaskCourier);
                    objTypeEdit = typeof(TaskCourierEdit);
                    nameModel = "Курьерские задачи";
                    referenceBookId = (int)cls_App.ReferenceBooks.TaskCourier;
                    break;

                case nameof(RouteSheetv2):
                    objType = typeof(RouteSheetv2);
                    objTypeEdit = typeof(RouteSheetv2Edit);
                    nameModel = "Маршрутные листы";
                    referenceBookId = (int)cls_App.ReferenceBooks.RouteSheetv2;
                    break;

                case nameof(TaskRouteSheetv2):
                    objType = typeof(TaskRouteSheetv2);
                    objTypeEdit = typeof(TaskRouteSheetv2Edit);
                    nameModel = "Курьерские задачи";
                    referenceBookId = (int)cls_App.ReferenceBooks.TaskRouteSheetv2;
                    break;
            }
        }

        private void btnControlSystemObject_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            if (e.Button.Kind == ButtonPredefines.Ellipsis)
            {
                cls_BaseSpr.ButtonEditButtonClickBase(obj, session, buttonEdit, referenceBookId, 1, null, null, false, null, string.Empty, false, true);

                if (buttonEdit.EditValue != null && buttonEdit.EditValue is XPObject xpObject && objType == xpObject.GetType())
                {
                    obj = xpObject;
                    return;
                }
            }

            if (e.Button.Kind == ButtonPredefines.Search)
            {
                OpenFormEdit();
            }
        }

        private void OpenFormEdit()
        {
            try
            {
                var formMethod = objTypeEdit.GetMethod("ShowDialog", new Type[] { });
                if (formMethod != null)
                {
                    var form = Activator.CreateInstance(objTypeEdit, obj);
                    formMethod.Invoke(form, null);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private async void ProgramEventEdit_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (TypeProgramEvent item in Enum.GetValues(typeof(TypeProgramEvent)))
                {
                    cmbTypeProgramEvent.Properties.Items.Add(item.GetEnumDescription());
                }

                foreach (ActionProgramEvent item in Enum.GetValues(typeof(ActionProgramEvent)))
                {
                    cmbActionProgramEvent.Properties.Items.Add(item.GetEnumDescription());
                }

                if (referenceBookId == -1)
                {
                    btnControlSystemObject.Properties.Buttons[0].Visible = false;
                }
                
                txtName.EditValue = ProgramEvent.Name;
                memoDescription.EditValue = ProgramEvent.DescriptionString;
                
                txtNameModel.EditValue = ProgramEvent.NameModel;
                btnControlSystemObject.EditValue = await session.GetObjectByKeyAsync(objType, ProgramEvent?.ControlSystemObjectId);
                dateSince.EditValue = ProgramEvent.DateSince;
                dateTo.EditValue = ProgramEvent.DateTo;
                
                if (ProgramEvent.TypeProgramEvent is null)
                {
                    cmbTypeProgramEvent.SelectedIndex = 0;
                }
                else
                {
                    cmbTypeProgramEvent.EditValue = ProgramEvent.TypeProgramEvent?.GetEnumDescription();
                }

                if (ProgramEvent.ActionProgramEvent is null)
                {
                    cmbActionProgramEvent.SelectedIndex = 0;
                }
                else
                {
                    cmbActionProgramEvent.EditValue = ProgramEvent.ActionProgramEvent?.GetEnumDescription();
                }

                btnStaff.EditValue = ProgramEvent.Staff;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dateSince.EditValue is DateTime since)
                {
                    ProgramEvent.DateSince = since;
                }
                else
                {
                    ProgramEvent.DateSince = null;
                }

                if (dateTo.EditValue is DateTime to)
                {
                    ProgramEvent.DateTo = to;
                }
                else
                {
                    ProgramEvent.DateTo = null;
                }

                if (btnStaff.EditValue is Staff staff)
                {
                    ProgramEvent.Staff = staff;
                }
                else
                {
                    ProgramEvent.Staff = null;
                }

                ProgramEvent.Name = txtName.Text;
                ProgramEvent.Description = Letter.StringToByte(memoDescription.Text);
                ProgramEvent.ControlSystemObjectId = obj.Oid;

                var programEventType = default(TypeProgramEvent?);
                foreach (TypeProgramEvent typeProgramEvent in Enum.GetValues(typeof(TypeProgramEvent)))
                {
                    if (typeProgramEvent.GetEnumDescription().Equals(cmbTypeProgramEvent.Text))
                    {
                        programEventType = typeProgramEvent;
                        break;
                    }
                }

                if (programEventType is TypeProgramEvent programEventTypeObj)
                {
                    ProgramEvent.TypeProgramEvent = programEventTypeObj;
                }
                else
                {
                    ProgramEvent.TypeProgramEvent = null;
                }

                var actionProgramEventType = default(ActionProgramEvent?);
                foreach (ActionProgramEvent actionProgramEvent in Enum.GetValues(typeof(ActionProgramEvent)))
                {
                    if (actionProgramEvent.GetEnumDescription().Equals(cmbActionProgramEvent.Text))
                    {
                        actionProgramEventType = actionProgramEvent;
                        break;
                    }
                }

                if (actionProgramEventType is ActionProgramEvent actionProgramEventTypeObj)
                {
                    ProgramEvent.ActionProgramEvent = actionProgramEventTypeObj;
                }
                else
                {
                    ProgramEvent.ActionProgramEvent = null;
                }

                ProgramEvent.Save();

                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnStaff_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }
            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnControlSystemObject_DoubleClick(object sender, EventArgs e)
        {
            OpenFormEdit();
        }
    }
}