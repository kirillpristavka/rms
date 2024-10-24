using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace RMS.Core.Model.Access
{
    public class AccessRights : XPObject
    {
        public AccessRights() { }
        public AccessRights(Session session) : base(session) { }
        
        [Category("Рабочий стол")]
        [System.ComponentModel.DisplayName("Просмотр модуля")]
        public bool IsViewDesktopForm { get; set; } = true;

        [Category("Клиенты")]
        [System.ComponentModel.DisplayName("Просмотр модуля")]
        public bool IsViewCustomersForm { get; set; } = true;

        [Category("Клиенты")]
        [System.ComponentModel.DisplayName("Редактирование информации")]
        public bool IsEditCustomersForm { get; set; } = true;
        
        [Category("Клиенты")]
        [System.ComponentModel.DisplayName("Удаление клиента")]
        public bool IsDeleteCustomersForm { get; set; } = true;

        [Category("Клиенты")]
        [System.ComponentModel.DisplayName("Доступ в банк")]
        public bool IsViewCustomersFormPageBankAccess { get; set; } = true;

        [Category("Клиенты")]
        [System.ComponentModel.DisplayName("Задачи")]
        public bool IsViewCustomersFormPageTasks { get; set; } = true;
        
        [Category("Клиенты")]
        [System.ComponentModel.DisplayName("Ежемесячная информация")]
        public bool IsViewCustomersFormPageOrganizationPerfomance { get; set; } = true;

        [Category("Клиенты")]
        [System.ComponentModel.DisplayName("Счета")]
        public bool IsViewCustomersFormPageInvoice { get; set; } = true;

        [Category("Клиенты")]
        [System.ComponentModel.DisplayName("Трудовые книжки")]
        public bool IsViewCustomersFormPageEploymentHistory { get; set; } = true;

        [Category("Клиенты")]
        [System.ComponentModel.DisplayName("Контакты")]
        public bool IsViewCustomersFormPageContact { get; set; } = true;

        [Category("Клиенты")]
        [System.ComponentModel.DisplayName("Отображение платежной информации")]
        public bool IsShowCustomersFormBillingInformation { get; set; } = true;

        [Category("Договора")]
        [System.ComponentModel.DisplayName("Просмотр модуля")]
        public bool IsViewContractForm { get; set; } = true;
        
        [Category("Договора")]
        [System.ComponentModel.DisplayName("Редактирование информации")]
        public bool IsEditContractForm { get; set; } = true;

        [Category("Задачи")]
        [System.ComponentModel.DisplayName("Просмотр модуля")]
        public bool IsViewTaskForm { get; set; } = true;

        [Category("Задачи")]
        [System.ComponentModel.DisplayName("Редактирование информации")]
        public bool IsEditTaskForm { get; set; } = true;
        
        [Category("Задачи")]
        [System.ComponentModel.DisplayName("Удаление задачи")]
        public bool IsDeleteTaskForm { get; set; } = true;

        [Category("Отчеты")]
        [System.ComponentModel.DisplayName("Просмотр модуля")]
        public bool IsViewReportChangeForm { get; set; } = true;

        [Category("Отчеты")]
        [System.ComponentModel.DisplayName("Редактирование отчетов")]
        public bool IsEditReportChangeForm { get; set; } = true;

        [Category("Отчеты")]
        [System.ComponentModel.DisplayName("Удаление отчетов")]
        public bool IsDeleteReportChangeForm { get; set; } = true;

        [Category("Отчеты")]
        [System.ComponentModel.DisplayName("Редактирование ЗП/Аванс")]
        public bool IsEditSalaryReportForm { get; set; } = true;
        
        [Category("Отчеты")]
        [System.ComponentModel.DisplayName("Удаление ЗП/Аванс")]
        public bool IsDeleteSalaryReportForm { get; set; } = true;

        [Category("Отчеты")]
        [System.ComponentModel.DisplayName("Редактирование ИП страховые/ПФР")]
        public bool IsEditIndividualReportForm { get; set; } = true;

        [Category("Отчеты")]
        [System.ComponentModel.DisplayName("Удаление ИП страховые/ПФР")]
        public bool IsDeleteIndividualReportForm { get; set; } = true;

        [Category("Отчеты")]
        [System.ComponentModel.DisplayName("Редактирование ИП патенты")]
        public bool IsEditIndividualPatentReportForm { get; set; } = true;

        [Category("Отчеты")]
        [System.ComponentModel.DisplayName("Удаление ИП патенты")]
        public bool IsDeleteIndividualPatentReportForm { get; set; } = true;

        [Category("Отчеты")]
        [System.ComponentModel.DisplayName("Редактирование предварительных налогов")]
        public bool IsPreTaxChange { get; set; } = true;
        
        [Category("Отчеты")]
        [System.ComponentModel.DisplayName("Удаление предварительных налогов")]
        public bool IsDeletePreTax { get; set; } = true;

        [Category("Сделки")]
        [System.ComponentModel.DisplayName("Просмотр модуля")]
        public bool IsViewDealForm { get; set; } = true;

        [Category("Сделки")]
        [System.ComponentModel.DisplayName("Редактирование информации")]
        public bool IsEditDealForm { get; set; } = true;
        [Category("Сделки")]
        [System.ComponentModel.DisplayName("Удаление сделки")]
        public bool IsDeleteDealForm { get; set; } = true;

        [Category("Счета")]
        [System.ComponentModel.DisplayName("Просмотр модуля")]
        public bool IsViewInvoiceForm { get; set; } = true;

        [Category("Счета")]
        [System.ComponentModel.DisplayName("Редактирование информации")]
        public bool IsEditInvoiceForm { get; set; } = true;

        [Category("Почта")]
        [System.ComponentModel.DisplayName("Просмотр модуля")]
        public bool IsViewLetterForm { get; set; } = true;

        [Category("Почта")]
        [System.ComponentModel.DisplayName("Редактирование информации, отправка сообщений")]
        public bool IsEditLetterForm { get; set; } = true;

        [Category("Почта")]
        [System.ComponentModel.DisplayName("Удаление сообщений")]
        public bool IsDeleteLetterForm { get; set; } = true;

        [Category("Почта")]
        [System.ComponentModel.DisplayName("Отображение контактов")]
        public bool IsShowContactLetterForm { get; set; } = true;

        [Category("Заработная плата")]
        [System.ComponentModel.DisplayName("Просмотр модуля")]
        public bool IsViewSalaryForm { get; set; } = true;

        [Category("Архивные папки")]
        [System.ComponentModel.DisplayName("Просмотр модуля")]
        public bool IsViewArchiveFolderChangeForm { get; set; } = true;

        [Category("Архивные папки")]
        [System.ComponentModel.DisplayName("Редактирование информации")]
        public bool IsEditArchiveFolderChangeForm { get; set; } = true;

        [Category("Маршрутные листы")]
        [System.ComponentModel.DisplayName("Просмотр модуля")]
        public bool IsViewRouteSheetForm { get; set; } = true;

        [Category("Курьерские задачи")]
        [System.ComponentModel.DisplayName("Просмотр модуля")]
        public bool IsViewTaskCourierForm { get; set; } = true;

        [Category("Система контроля")]
        [System.ComponentModel.DisplayName("Просмотр модуля")]
        public bool IsViewControlSystemForm { get; set; } = true;

        [Category("События")]
        [System.ComponentModel.DisplayName("Просмотр модуля")]
        public bool IsViewProgramEventForm2 { get; set; } = true;

        [Category("Отпуск")]
        [System.ComponentModel.DisplayName("Просмотр модуля")]
        public bool IsViewVacationForm { get; set; } = true;

        [Category("Сотрудники")]
        [System.ComponentModel.DisplayName("Просмотр модуля")]
        public bool IsViewStaffForm { get; set; } = true;
        
        [Category("Сотрудники")]
        [System.ComponentModel.DisplayName("Просмотр модуля ЗП")]
        public bool IsViewSalaryStaffForm { get; set; } = true;

        public bool ReturnValueView<T>(string formName = null) where T : Form
        {
            try
            {
                var nameForm = typeof(T).Name;
                if (!string.IsNullOrWhiteSpace(formName))
                {
                    nameForm = formName;
                }
                
                var property = GetType().GetProperties()?.FirstOrDefault(f => f.Name.Contains(nameForm));
                if (property != null)
                {
                    if (property.GetValue(this) is bool result)
                    {
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }

            return false;
        }

        public bool SetAccessRights(AccessRights accessRights)
        {
            try
            {
                if (accessRights != null)
                {
                    var property = accessRights.GetType().GetProperties();
                    foreach (var item in property)
                    {
                        try
                        {                            
                            if (item.PropertyType == typeof(bool) && item.GetSetMethod() != null)
                            {
                                var obj = GetType().GetProperty(item.Name);
                                if (obj != null)
                                {
                                    obj.SetValue(this, accessRights.GetType().GetProperty(item.Name).GetValue(accessRights, null));
                                }                               
                            }
                        }
                        catch (Exception ex)
                        {
                            RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }

            return false;
        }
    }
}
