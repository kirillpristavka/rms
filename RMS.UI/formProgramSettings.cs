using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraVerticalGrid.Rows;
using Newtonsoft.Json;
using RMS.Core.Enumerator;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using RMS.Core.Model.Reports;
using RMS.Core.Model.Taxes;
using RMS.Core.ModelSbis.Controllers;
using RMS.Core.TG.Core.Models;
using RMS.Setting.Model.ColorSettings;
using RMS.Setting.Model.CustomerSettings;
using RMS.Setting.Model.GeneralSettings;
using RMS.UI.Forms;
using RMS.UI.Forms.CourierService.v1;
using RMS.UI.Forms.Mail;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Telegram.Bot;
using TelegramBotRMS.Core.Models;

namespace RMS.UI
{
    public partial class formProgramSettings : XtraForm
    {
        private Session _workSession => DatabaseConnection.GetWorkSession();
        private CustomerSettings CustomerSettings { get; set; }
        private Settings Settings { get; set; }

        private User user;

        public formProgramSettings()
        {
            InitializeComponent();
        }

        public formProgramSettings(int pageIndex) : this()
        {
            if (pageIndex > 0 && pageIndex < xtraTabControlSettings.TabPages.Count)
            {
                for (int i = 0; i < xtraTabControlSettings.TabPages.Count; i++)
                {
                    if (i == pageIndex)
                    {
                        xtraTabControlSettings.TabPages[i].PageVisible = true;
                        xtraTabControlSettings.SelectedTabPageIndex = i;
                    }
                    else
                    {
                        xtraTabControlSettings.TabPages[i].PageVisible = false;
                    }
                }
            }
        }
        
        private async void formProgramSettings_Load(object sender, EventArgs e)
        {
            try
            {
                user = await _workSession.GetObjectByKeyAsync<User>(DatabaseConnection.User?.Oid);

                dateMail.EditValue = DateTime.Now.AddYears(-1).Date;

                chkUserFont.EditValue = Convert.ToBoolean(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync
                        (DatabaseConnection.LocalSession, "form_ProgramSettings_chkUserFont", "false", false, false, 1, BVVGlobal.oApp.User));
                chkUserFont_CheckedChanged(null, null);

                fontEditUserFont.EditValue = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync
                    (DatabaseConnection.LocalSession, "form_ProgramSettings_fontEditUserFont", "Verdana", false, false, 1, BVVGlobal.oApp.User);

                spinEditUserFontSize.Value = Convert.ToInt32(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync
                    (DatabaseConnection.LocalSession, "form_ProgramSettings_spinEditUserFontSize", "8", false, false, 1, BVVGlobal.oApp.User));

                checkEditIsUsePopupWindow.EditValue = Convert.ToBoolean(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync
                        (DatabaseConnection.LocalSession, "formProgramSettings_IsUsePopupWindow", "false", false, false, 1, BVVGlobal.oApp.User));

                colorStatusReportNew.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportNew)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(152, 251, 152))));
                colorStatusReportSurrendered.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportSurrendered)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(255, 250, 205))));
                colorStatusReportNotSurrendered.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportNotSurrendered)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(230, 230, 250))));
                colorStatusReportSent.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportSent)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(250, 128, 114))));
                colorStatusReportPrepared.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportPrepared)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(135, 206, 235))));
                colorStatusReportNotAccepted.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportNotAccepted)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(255, 0, 0))));
                colorStatusReportRentedByTheClient.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportRentedByTheClient)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(155, 100, 228))));
                colorStatusReportDoesnotgiveup.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportDoesnotgiveup)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(238, 0, 238))));

                colorStatusReportAdjustmentRequired.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportAdjustmentRequired)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(221, 0, 223))));
                colorStatusReportAdjustmentIsReady.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportAdjustmentIsReady)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(204, 0, 223))));
                colorStatusReportCorrectionSent.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportCorrectionSent)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(187, 0, 225))));
                colorStatusReportCorrectionSubmitted.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportCorrectionSubmitted)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(170, 0, 125))));

                colorStatusArchiveFolderNew.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusArchiveFolderNew)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(152, 251, 152))));
                colorStatusArchiveFolderIsCompleted.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusArchiveFolderIsCompleted)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(255, 250, 205))));
                colorStatusArchiveFolderSortout.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusArchiveFolderSortout)}", workingField: 1, user: BVVGlobal.oApp.User));
                colorStatusArchiveFolderIssued.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusArchiveFolderIssued)}", workingField: 1, user: BVVGlobal.oApp.User));
                colorStatusArchiveFolderDone.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusArchiveFolderDone)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));
                colorStatusArchiveFolderReturned.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusArchiveFolderReturned)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));

                colorStatusArchiveFolderDestroyed.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusArchiveFolderDestroyed)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));

                colorStatusArchiveFolderIsSuedEDS.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusArchiveFolderIsSuedEDS)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));
                colorStatusArchiveFolderReceivedEDS.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusArchiveFolderReceivedEDS)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));
                colorStatusArchiveFolderIsSuedTK.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusArchiveFolderIsSuedTK)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));
                colorStatusArchiveFolderReceivedTK.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusArchiveFolderReceivedTK)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));

                colorStatusTaskCourierPerformed.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusTaskCourierPerformed)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));
                colorStatusTaskCourierCanceled.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusTaskCourierCanceled)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(255, 218, 185))));
                colorStatusTaskCourierNew.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusTaskCourierNew)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(152, 251, 152))));

                colorStatusDealNew.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusDealNew)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(152, 251, 152))));
                colorStatusDealPostponed.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusDealPostponed)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(255, 250, 205))));
                colorStatusDealCompleted.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusDealCompleted)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(230, 230, 250))));
                colorStatusDealPrimary.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusDealPrimary)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));
                colorStatusDealAdministrator.Color = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusDealAdministrator)}", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(152, 251, 152))));

                checkDesktopForm.Checked = await GetSettingsValue(nameof(DesktopForm));
                checkCustomersForm.Checked = await GetSettingsValue(nameof(CustomersForm));
                checkContractForm.Checked = await GetSettingsValue(nameof(ContractForm));
                checkTaskForm.Checked = await GetSettingsValue(nameof(TaskForm));
                checkStaffForm.Checked = await GetSettingsValue(nameof(StaffForm));
                checkReportChangeForm.Checked = await GetSettingsValue(nameof(ReportChangeForm));
                checkDealForm.Checked = await GetSettingsValue(nameof(DealForm));
                checkInvoiceForm.Checked = await GetSettingsValue(nameof(InvoiceForm));
                checkLetterForm.Checked = await GetSettingsValue(nameof(LetterForm));
                checkSalaryForm.Checked = await GetSettingsValue(nameof(SalaryForm));
                checkArchiveFolderChangeForm.Checked = await GetSettingsValue(nameof(ArchiveFolderChangeForm));
                checkRouteSheetForm.Checked = await GetSettingsValue(nameof(RouteSheetForm));
                checkTaskCourierForm.Checked = await GetSettingsValue(nameof(TaskCourierForm));
                checkControlSystemForm.Checked = await GetSettingsValue(nameof(ControlSystemForm));
                checkProgramEventForm2.Checked = await GetSettingsValue(nameof(ProgramEventForm2));

                if (await DatabaseConnection.LocalSession.FindObjectAsync<CustomerSettings>(null) == null)
                {
                    CustomerSettings = new CustomerSettings(DatabaseConnection.LocalSession)
                    {
                        Name = $"Шаблон пользователя {DatabaseConnection.User?.ToString()}",
                        Description = "Шаблон создан автоматически при первичном входе пользователя в систему настроек",
                        IsDefault = true,
                        IsVisibleStatus = true,
                        IsVisibleStatusStatisticalReport = true,
                        IsVisibleINN = true,
                        IsVisibleName = true,
                        IsVisibleProcessedName = true,
                        IsVisibleDefaultName = true,
                        IsVisibleDateRegistration = true,
                        IsVisibleDateLiquidation = true,
                        IsVisibleOrganizationStatus = true,
                        IsVisibleManagementString = true,
                        IsVisibleTelephone = true,
                        IsVisibleEmail = true,
                        IsVisibleLegalAddress = true,
                        IsVisibleFormCorporation = true,
                        IsVisibleContract = true
                    };
                }
                else
                {
                    CustomerSettings = await DatabaseConnection.LocalSession.FindObjectAsync<CustomerSettings>(null);
                }

                propertyGridCustomerSettings.SelectedObject = CustomerSettings;
                RepositoryItemCheckEdit riCheckEdit = new RepositoryItemCheckEdit();
                riCheckEdit.CheckStyle = CheckStyles.Standard;
                propertyGridCustomerSettings.DefaultEditors.Add(typeof(bool), riCheckEdit);

                FillAppParam();

                Settings = await _workSession.FindObjectAsync<Settings>(null);
                if (Settings is null)
                {
                    Settings = new Settings(_workSession) { IsUseDeliveryYearReport = false, IsUseYearReport = false };
                    Settings.Save();
                }

                checkIsUseYearReport.EditValue = Settings.IsUseYearReport;
                checkIsUseDeliveryYearReport.EditValue = Settings.IsUseDeliveryYearReport;
                btnEverything.EditValue = Settings.UserGroupEverything;

                if (!string.IsNullOrWhiteSpace(Settings.TelegramBotToken))
                {
                    btnTelegramBotToken.EditValue = Settings.TelegramBotToken;
                    btnTelegramBotToken.Properties.ReadOnly = true;
                    btnTelegramBotToken.Properties.UseSystemPasswordChar = true;
                    btnTelegramBotToken.Properties.Buttons[0].Visible = true;
                }
                else
                {
                    btnTelegramBotToken.Properties.Buttons[0].Visible = false;
                }

                dateEmailFiltering.EditValue = Settings.EmailFilteringDate;
                
                await FillCheckedListBox<ElectronicReporting>(checkedListBoxDigitalSignature, Settings.ElectronicReportingList);
                await FillCheckedListBox<PriceList>(checkedListBoxControlAdditionalServices, Settings.PriceList);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }

            try
            {
                var windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();
                if (windowsIdentity != null)
                {
                    var localSetting = await new XPQuery<set_LocalSettings>(DatabaseConnection.LocalSession)
                        .FirstOrDefaultAsync(f => f.Name == "IsOpenCustomerNotes"
                            && f.User == DatabaseConnection.User.Login
                            && f.Value3 == windowsIdentity.Name);
                    if (localSetting != null)
                    {
                        if (bool.TryParse(localSetting.Value1, out bool result))
                        {
                            checkEditIsOpenCustomerNotes.EditValue = result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async System.Threading.Tasks.Task FillCheckedListBox<T>(CheckedListBoxControl checkedListBox, byte[] obj) where T : XPObject
        {
            var listBaseElectronicReporting = await new XPQuery<T>(_workSession).ToListAsync();
            try
            {               
                var listElectronicReporting = JsonConvert.DeserializeObject<List<int>>(Letter.ByteToString(obj));

                foreach (var item in listBaseElectronicReporting)
                {
                    var isCkecked = false;
                    if (listElectronicReporting.Contains(item.Oid))
                    {
                        isCkecked = true;
                    }

                    checkedListBox.Items.Add(item, isCkecked);
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());

                if (listBaseElectronicReporting != null && listBaseElectronicReporting.Count > 0)
                {
                    foreach (var item in listBaseElectronicReporting)
                    {
                        checkedListBox.Items.Add(item);
                    }
                }
            }
        }

        private async System.Threading.Tasks.Task<bool> GetSettingsValue(string nameForm)
        {
            try
            {
                var obj = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync
                            (DatabaseConnection.LocalSession, $"MainForm_OpenForms_{nameForm}", bool.FalseString, false, false, 1, BVVGlobal.oApp.User);

                if (bool.TryParse(obj, out bool result))
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }

            return default;
        }

        private async System.Threading.Tasks.Task SetSettingsValue(string nameForm, bool obj)
        {
            try
            {
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync
                            (DatabaseConnection.LocalSession, $"MainForm_OpenForms_{nameForm}", obj.ToString(), true, true, 1, BVVGlobal.oApp.User);
                
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_ProgramSettings_chkUserFont", chkUserFont.EditValue.ToString(), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_ProgramSettings_fontEditUserFont", fontEditUserFont.EditValue.ToString(), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_ProgramSettings_spinEditUserFontSize", spinEditUserFontSize.Value.ToString(), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_IsUsePopupWindow", checkEditIsUsePopupWindow.EditValue.ToString(), true, true, 1, BVVGlobal.oApp.User);

                await SetSettingsValue(nameof(DesktopForm), checkDesktopForm.Checked);
                await SetSettingsValue(nameof(CustomersForm), checkCustomersForm.Checked);
                await SetSettingsValue(nameof(ContractForm), checkContractForm.Checked);
                await SetSettingsValue(nameof(TaskForm), checkTaskForm.Checked);
                await SetSettingsValue(nameof(StaffForm), checkStaffForm.Checked);
                await SetSettingsValue(nameof(ReportChangeForm), checkReportChangeForm.Checked);
                await SetSettingsValue(nameof(DealForm), checkDealForm.Checked);
                await SetSettingsValue(nameof(InvoiceForm), checkInvoiceForm.Checked);
                await SetSettingsValue(nameof(LetterForm), checkLetterForm.Checked);
                await SetSettingsValue(nameof(SalaryForm), checkSalaryForm.Checked);
                await SetSettingsValue(nameof(ArchiveFolderChangeForm), checkArchiveFolderChangeForm.Checked);
                await SetSettingsValue(nameof(RouteSheetForm), checkRouteSheetForm.Checked);
                await SetSettingsValue(nameof(TaskCourierForm), checkTaskCourierForm.Checked);
                await SetSettingsValue(nameof(ControlSystemForm), checkControlSystemForm.Checked);
                await SetSettingsValue(nameof(ProgramEventForm2), checkProgramEventForm2.Checked);

                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportNew)}", ColorTranslator.ToHtml(colorStatusReportNew.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportSurrendered)}", ColorTranslator.ToHtml(colorStatusReportSurrendered.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportNotSurrendered)}", ColorTranslator.ToHtml(colorStatusReportNotSurrendered.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportSent)}", ColorTranslator.ToHtml(colorStatusReportSent.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportPrepared)}", ColorTranslator.ToHtml(colorStatusReportPrepared.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportNotAccepted)}", ColorTranslator.ToHtml(colorStatusReportNotAccepted.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportDoesnotgiveup)}", ColorTranslator.ToHtml(colorStatusReportDoesnotgiveup.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportRentedByTheClient)}", ColorTranslator.ToHtml(colorStatusReportRentedByTheClient.Color), true, true, 1, BVVGlobal.oApp.User);

                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportAdjustmentRequired)}", ColorTranslator.ToHtml(colorStatusReportAdjustmentRequired.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportAdjustmentIsReady)}", ColorTranslator.ToHtml(colorStatusReportAdjustmentIsReady.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportCorrectionSent)}", ColorTranslator.ToHtml(colorStatusReportCorrectionSent.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusReportCorrectionSubmitted)}", ColorTranslator.ToHtml(colorStatusReportCorrectionSubmitted.Color), true, true, 1, BVVGlobal.oApp.User);

                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusArchiveFolderNew)}", ColorTranslator.ToHtml(colorStatusArchiveFolderNew.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusArchiveFolderIsCompleted)}", ColorTranslator.ToHtml(colorStatusArchiveFolderIsCompleted.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusArchiveFolderSortout)}", ColorTranslator.ToHtml(colorStatusArchiveFolderSortout.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusArchiveFolderIssued)}", ColorTranslator.ToHtml(colorStatusArchiveFolderIssued.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusArchiveFolderDone)}", ColorTranslator.ToHtml(colorStatusArchiveFolderDone.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusArchiveFolderReturned)}", ColorTranslator.ToHtml(colorStatusArchiveFolderReturned.Color), true, true, 1, BVVGlobal.oApp.User);

                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusArchiveFolderDestroyed)}", ColorTranslator.ToHtml(colorStatusArchiveFolderDestroyed.Color), true, true, 1, BVVGlobal.oApp.User);

                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusArchiveFolderIsSuedEDS)}", ColorTranslator.ToHtml(colorStatusArchiveFolderIsSuedEDS.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusArchiveFolderReceivedEDS)}", ColorTranslator.ToHtml(colorStatusArchiveFolderReceivedEDS.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusArchiveFolderIsSuedTK)}", ColorTranslator.ToHtml(colorStatusArchiveFolderIsSuedTK.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusArchiveFolderReceivedTK)}", ColorTranslator.ToHtml(colorStatusArchiveFolderReceivedTK.Color), true, true, 1, BVVGlobal.oApp.User);

                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusTaskCourierPerformed)}", ColorTranslator.ToHtml(colorStatusTaskCourierPerformed.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusTaskCourierCanceled)}", ColorTranslator.ToHtml(colorStatusTaskCourierCanceled.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusTaskCourierNew)}", ColorTranslator.ToHtml(colorStatusTaskCourierNew.Color), true, true, 1, BVVGlobal.oApp.User);

                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusDealNew)}", ColorTranslator.ToHtml(colorStatusDealNew.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusDealPostponed)}", ColorTranslator.ToHtml(colorStatusDealPostponed.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusDealCompleted)}", ColorTranslator.ToHtml(colorStatusDealCompleted.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusDealPrimary)}", ColorTranslator.ToHtml(colorStatusDealPrimary.Color), true, true, 1, BVVGlobal.oApp.User);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"formProgramSettings_{nameof(colorStatusDealAdministrator)}", ColorTranslator.ToHtml(colorStatusDealAdministrator.Color), true, true, 1, BVVGlobal.oApp.User);

                var fontName = "Verdana";
                var size = 10;

                if ((bool)chkUserFont.EditValue)
                {
                    fontName = fontEditUserFont.EditValue.ToString();
                    size = Convert.ToInt32(spinEditUserFontSize.Value);
                }

                WindowsFormsSettings.DefaultFont = new Font(fontName, size);
                WindowsFormsSettings.DefaultMenuFont = new Font(fontName, size);

                CustomerSettings.Save();

                BVVGlobal.oApp.AppParam = await BVVGlobal.oFuncXpo.FillAppParam();

                if (Settings != null)
                {
                    Settings.IsUseYearReport = checkIsUseYearReport.Checked;
                    Settings.IsUseDeliveryYearReport = checkIsUseDeliveryYearReport.Checked;

                    if (btnEverything.EditValue is UserGroup userGroup)
                    {
                        Settings.UserGroupEverything = await _workSession.GetObjectByKeyAsync<UserGroup>(userGroup.Oid);
                    }
                    else
                    {
                        Settings.UserGroupEverything = null;
                    }

                    if (btnTelegramBotToken.ReadOnly is false)
                    {
                        if (!string.IsNullOrWhiteSpace(btnTelegramBotToken.Text))
                        {
                            Settings.TelegramBotToken = btnTelegramBotToken.Text;
                        }
                    }

                    if (dateEmailFiltering.EditValue is DateTime date)
                    {
                        Settings.EmailFilteringDate = date;
                    }
                    else
                    {
                        Settings.EmailFilteringDate = null;
                    }

                    Settings.ElectronicReportingList = SavingCheckedListBox<ElectronicReporting>(checkedListBoxDigitalSignature);
                    Settings.PriceList = SavingCheckedListBox<PriceList>(checkedListBoxControlAdditionalServices);
                    Settings.Save();
                }               
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }

            try
            {
                var windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();
                if (windowsIdentity != null)
                {
                    var localSetting = await new XPQuery<set_LocalSettings>(DatabaseConnection.LocalSession)
                        .FirstOrDefaultAsync(f => f.Name == "IsOpenCustomerNotes"
                            && f.User == DatabaseConnection.User.Login
                            && f.Value3 == windowsIdentity.Name);
                    if (localSetting is null)
                    {
                        localSetting = new set_LocalSettings(DatabaseConnection.LocalSession);
                        localSetting.Name = "IsOpenCustomerNotes";
                        localSetting.g_id = Guid.NewGuid().ToString();
                        localSetting.User = DatabaseConnection.User.Login;
                    }

                    localSetting.Value1 = checkEditIsOpenCustomerNotes.EditValue.ToString();
                    localSetting.Value3 = windowsIdentity.Name;
                    localSetting.Save();
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private byte[] SavingCheckedListBox<T>(CheckedListBoxControl checkedListBox) where T : XPObject
        {
            var listElectronicReporting = new List<int>();
            
            foreach (var item in checkedListBox.Items.GetCheckedValues())
            {
                if (item is T obj)
                {
                    listElectronicReporting.Add(obj.Oid);
                }
            }
            
            return Letter.StringToByte(JsonConvert.SerializeObject(listElectronicReporting));
        }

        private void formProgramSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BVVGlobal.oApp.Get_flagProcess())
            {
                e.Cancel = true;
                XtraMessageBox.Show(this, "This operation in not completed yet. Please wait.", "Cannot close this form");
            }
        }        
        
        private async void FillAppParam()
        {
            try
            {
                cls_AppParam tmpAppParam = await BVVGlobal.oFuncXpo.FillAppParam();
                
                RepositoryItemImageComboBox cmbBoolean = propertyGridControl1.RepositoryItems.Add(nameof(ImageComboBoxEdit)) as RepositoryItemImageComboBox;
                cmbBoolean.Items.Add(new ImageComboBoxItem("Отключено", "0"));
                cmbBoolean.Items.Add(new ImageComboBoxItem("Включено", "1"));

                RepositoryItemButtonEdit buttonEdit = propertyGridControl1.RepositoryItems.Add(nameof(ButtonEdit)) as RepositoryItemButtonEdit;
                buttonEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
                buttonEdit.ButtonPressed += ButtonEdit_ButtonPressed;

                propertyGridControl1.SelectedObject = null;
                propertyGridControl1.Rows.Clear();

                CategoryRow rowCategory;
                EditorRow rowEdit;

                rowCategory = new CategoryRow("Локальные параметры приложения");
                propertyGridControl1.Rows.Add(rowCategory);

                rowEdit = new EditorRow("TmpDir");
                rowEdit.Properties.Caption = "Временная директория";
                rowEdit.Properties.RowEdit = repositoryItemButtonEdit;
                rowCategory.ChildRows.Add(rowEdit);

                rowEdit = new EditorRow("OutDir");
                rowEdit.Properties.Caption = "Выводная директория";
                rowEdit.Properties.RowEdit = repositoryItemButtonEdit;
                rowCategory.ChildRows.Add(rowEdit);

                rowEdit = new EditorRow("TemplatesDir");
                rowEdit.Properties.Caption = "Шаблоны документов";
                rowEdit.Properties.RowEdit = repositoryItemButtonEdit;
                rowCategory.ChildRows.Add(rowEdit);

                rowEdit = new EditorRow(nameof(cls_AppParam.MyFolderPath));
                rowEdit.Properties.Caption = "Рабочий каталог с файлами";
                rowEdit.Properties.RowEdit = repositoryItemButtonEdit;
                rowCategory.ChildRows.Add(rowEdit);

                rowEdit = new EditorRow(nameof(cls_AppParam.MailboxForSending));
                rowEdit.Properties.Caption = "Почтовый клиент";
                rowEdit.Properties.RowEdit = buttonEdit;
                rowCategory.ChildRows.Add(rowEdit);

                rowEdit = new EditorRow(nameof(cls_AppParam.CountOfDaysToAcceptLetter));
                rowEdit.Properties.Caption = "За сколько дней принимать письма";
                rowCategory.ChildRows.Add(rowEdit);

                rowEdit = new EditorRow(nameof(cls_AppParam.CountOfLetterToSave));
                rowEdit.Properties.Caption = "Количество писем для сохранения в коллекции";
                rowCategory.ChildRows.Add(rowEdit);

                rowEdit = new EditorRow(nameof(cls_AppParam.PathUpdateService));
                rowEdit.Properties.Caption = "Сервис обновлений";
                rowCategory.ChildRows.Add(rowEdit);

                rowEdit = new EditorRow(nameof(cls_AppParam.EnableOrDisableEmailPreview));
                rowEdit.Properties.Caption = "Включение/отключение предварительного просмотра писем";
                rowEdit.Properties.RowEdit = cmbBoolean;
                rowCategory.ChildRows.Add(rowEdit);

                rowEdit = new EditorRow(nameof(cls_AppParam.EnableOrDisableGetEmails));
                rowEdit.Properties.Caption = "Включение/отключение приема писем при старте программы";
                rowEdit.Properties.RowEdit = cmbBoolean;
                rowCategory.ChildRows.Add(rowEdit);

                propertyGridControl1.SelectedObject = tmpAppParam;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private async void ButtonEdit_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            using (var uof = new UnitOfWork())
            {
                var idMailBox = default(int?);
                
                var buttonEdit = sender as ButtonEdit;
                if (buttonEdit != null)
                {                    
                    if (!string.IsNullOrWhiteSpace(buttonEdit.Text))
                    {
                        var mailbox = await uof.FindObjectAsync<Mailbox>(new BinaryOperator(nameof(Mailbox.MailingAddress), buttonEdit.Text));
                        if (mailbox != null)
                        {
                            idMailBox = mailbox.Oid;
                        }
                    }
                    
                    var id = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.Mailbox, idMailBox ?? -1);
                    if (id > 0)
                    {
                        var mailbox = await uof.GetObjectByKeyAsync<Mailbox>(id);
                        if (mailbox != null)
                        {
                            buttonEdit.Text = mailbox.MailingAddress;
                        }
                    }
                }                           
            }
        }

        private void repositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (buttonEdit != null)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                    return;
                }

                if (e.Button.Kind == ButtonPredefines.Ellipsis)
                {
                    var path = default(string);

                    if (buttonEdit.EditValue != null)
                    {
                        try
                        {
                            path = buttonEdit.EditValue?.ToString();

                            if (string.IsNullOrWhiteSpace(path))
                            {
                                path = BVVGlobal.oApp.BaseDirectory;
                            }

                            if (string.IsNullOrWhiteSpace(path))
                            {
                                path = Environment.CurrentDirectory;
                            }
                        }
                        catch (Exception ex)
                        {
                            RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                        }
                    }

                    using (var fbd = new FolderBrowserDialog() { SelectedPath = path, Description = "Выберите директорию"})
                    {
                        if (fbd.ShowDialog() == DialogResult.OK)
                        {
                            buttonEdit.EditValue = fbd.SelectedPath;
                        }
                    }

                    return;
                }
            }
        }

        private async void propertyGridControl1_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            string l_FieldName = this.propertyGridControl1.FocusedRow.Properties.FieldName;
            object l_FieldValue = this.propertyGridControl1.FocusedRow.Properties.Value;

            string parameter_name = string.Empty;
            string parameter_value = string.Empty;
            bool flInBase = false;

            switch (l_FieldName)
            {
                case "TmpDir":
                    parameter_name = "directory_temporary";
                    parameter_value = (string)l_FieldValue;
                    break;

                case "OutDir":
                    parameter_name = "directory_output";
                    parameter_value = (string)l_FieldValue;
                    break;

                case "TemplatesDir":
                    parameter_name = "directory_templates";
                    parameter_value = (string)l_FieldValue;
                    break;

                case nameof(cls_AppParam.MyFolderPath):
                    parameter_name = nameof(cls_AppParam.MyFolderPath);
                    parameter_value = (string)l_FieldValue;
                    break;

                case nameof(cls_AppParam.MailboxForSending):
                    parameter_name = nameof(cls_AppParam.MailboxForSending);
                    parameter_value = (string)l_FieldValue;
                    break;

                case nameof(cls_AppParam.CountOfDaysToAcceptLetter):
                    parameter_name = nameof(cls_AppParam.CountOfDaysToAcceptLetter);
                    parameter_value = (string)l_FieldValue;
                    break;

                case nameof(cls_AppParam.CountOfLetterToSave):
                    parameter_name = nameof(cls_AppParam.CountOfLetterToSave);
                    parameter_value = (string)l_FieldValue;
                    break;

                case nameof(cls_AppParam.PathUpdateService):
                    parameter_name = nameof(cls_AppParam.PathUpdateService);
                    parameter_value = (string)l_FieldValue;
                    break;
                    
                case nameof(cls_AppParam.EnableOrDisableEmailPreview):
                    parameter_name = nameof(cls_AppParam.EnableOrDisableEmailPreview);
                    parameter_value = (string)l_FieldValue;
                    break;

                case nameof(cls_AppParam.EnableOrDisableGetEmails):
                    parameter_name = nameof(cls_AppParam.EnableOrDisableGetEmails);
                    parameter_value = (string)l_FieldValue;
                    break;
            }

            if (flInBase)
            {
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(BVVGlobal.oXpo.GetSessionThreadSafeDataLayer(), parameter_name, parameter_value, true, true, 1, user: BVVGlobal.oApp.User);
            }
            else
            {
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, parameter_name, parameter_value, true, true, user: BVVGlobal.oApp.User);
            }
        }
        
        private void chkUserFont_CheckedChanged(object sender, EventArgs e)
        {
            bool fl_enable = (bool)chkUserFont.EditValue;
            fontEditUserFont.Enabled = fl_enable;
            spinEditUserFontSize.Enabled = fl_enable;
        }

        private void btnSaveColorStatus_Click(object sender, EventArgs e)
        {
            using (var session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer())
            {
                var colorStatus = new ColorStatus(session)
                {
                    StatusReportNew = ColorTranslator.ToHtml(colorStatusReportNew.Color),
                    StatusReportSurrendered = ColorTranslator.ToHtml(colorStatusReportSurrendered.Color),
                    StatusReportNotSurrendered = ColorTranslator.ToHtml(colorStatusReportNotSurrendered.Color),
                    StatusReportSent = ColorTranslator.ToHtml(colorStatusReportSent.Color),
                    StatusReportPrepared = ColorTranslator.ToHtml(colorStatusReportPrepared.Color),
                    StatusReportNotAccepted = ColorTranslator.ToHtml(colorStatusReportNotAccepted.Color),
                    StatusReportRentedByTheClient = ColorTranslator.ToHtml(colorStatusReportRentedByTheClient.Color),
                    StatusReportDoesnotgiveup = ColorTranslator.ToHtml(colorStatusReportDoesnotgiveup.Color),

                    StatusReportAdjustmentRequired = ColorTranslator.ToHtml(colorStatusReportAdjustmentRequired.Color),
                    StatusReportAdjustmentIsReady = ColorTranslator.ToHtml(colorStatusReportAdjustmentIsReady.Color),
                    StatusReportCorrectionSent = ColorTranslator.ToHtml(colorStatusReportCorrectionSent.Color),
                    StatusReportCorrectionSubmitted = ColorTranslator.ToHtml(colorStatusReportCorrectionSubmitted.Color),

                    StatusArchiveFolderNew = ColorTranslator.ToHtml(colorStatusArchiveFolderNew.Color),
                    StatusArchiveFolderIsCompleted = ColorTranslator.ToHtml(colorStatusArchiveFolderIsCompleted.Color),
                    StatusArchiveFolderSortout = ColorTranslator.ToHtml(colorStatusArchiveFolderSortout.Color),
                    StatusArchiveFolderIssued = ColorTranslator.ToHtml(colorStatusArchiveFolderIssued.Color),
                    StatusArchiveFolderDone = ColorTranslator.ToHtml(colorStatusArchiveFolderDone.Color),
                    StatusArchiveFolderReturned = ColorTranslator.ToHtml(colorStatusArchiveFolderReturned.Color),

                    StatusArchiveFolderDestroyed = ColorTranslator.ToHtml(colorStatusArchiveFolderDestroyed.Color),

                    StatusArchiveFolderIsSuedEDS = ColorTranslator.ToHtml(colorStatusArchiveFolderIsSuedEDS.Color),
                    StatusArchiveFolderReceivedEDS = ColorTranslator.ToHtml(colorStatusArchiveFolderReceivedEDS.Color),
                    StatusArchiveFolderIsSuedTK = ColorTranslator.ToHtml(colorStatusArchiveFolderIsSuedTK.Color),
                    StatusArchiveFolderReceivedTK = ColorTranslator.ToHtml(colorStatusArchiveFolderReceivedTK.Color),
                    
                    StatusTaskCourierPerformed = ColorTranslator.ToHtml(colorStatusTaskCourierPerformed.Color),
                    StatusTaskCourierCanceled = ColorTranslator.ToHtml(colorStatusTaskCourierCanceled.Color),
                    StatusTaskCourierNew = ColorTranslator.ToHtml(colorStatusTaskCourierNew.Color),
                    StatusDealNew = ColorTranslator.ToHtml(colorStatusDealNew.Color),
                    StatusDealPostponed = ColorTranslator.ToHtml(colorStatusDealPostponed.Color),
                    StatusDealCompleted = ColorTranslator.ToHtml(colorStatusDealCompleted.Color),
                    StatusDealPrimary = ColorTranslator.ToHtml(colorStatusDealPrimary.Color),
                    StatusDealAdministrator = ColorTranslator.ToHtml(colorStatusDealAdministrator.Color)
                };

                var form = new ColorStatusEdit(colorStatus);
                form.ShowDialog();
            }
        }

        private async void btnLoadColorStatus_Click(object sender, EventArgs e)
        {
            var colorStatusOid = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.ColorStatus, -1);

            if (colorStatusOid != -1)
            {
                using (var session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer())
                {
                    var colorStatus = await session.GetObjectByKeyAsync<ColorStatus>(colorStatusOid);

                    if (colorStatus != null)
                    {
                        colorStatusReportNew.Color = ColorTranslator.FromHtml(colorStatus.StatusReportNew);
                        colorStatusReportSurrendered.Color = ColorTranslator.FromHtml(colorStatus.StatusReportSurrendered);
                        colorStatusReportNotSurrendered.Color = ColorTranslator.FromHtml(colorStatus.StatusReportNotSurrendered);
                        colorStatusReportSent.Color = ColorTranslator.FromHtml(colorStatus.StatusReportSent);
                        colorStatusReportPrepared.Color = ColorTranslator.FromHtml(colorStatus.StatusReportPrepared);
                        colorStatusReportNotAccepted.Color = ColorTranslator.FromHtml(colorStatus.StatusReportNotAccepted);
                        colorStatusReportRentedByTheClient.Color = ColorTranslator.FromHtml(colorStatus.StatusReportRentedByTheClient);
                        colorStatusReportDoesnotgiveup.Color = ColorTranslator.FromHtml(colorStatus.StatusReportDoesnotgiveup);

                        colorStatusReportAdjustmentRequired.Color = ColorTranslator.FromHtml(colorStatus.StatusReportAdjustmentRequired);
                        colorStatusReportAdjustmentIsReady.Color = ColorTranslator.FromHtml(colorStatus.StatusReportAdjustmentIsReady);
                        colorStatusReportCorrectionSent.Color = ColorTranslator.FromHtml(colorStatus.StatusReportCorrectionSent);
                        colorStatusReportCorrectionSubmitted.Color = ColorTranslator.FromHtml(colorStatus.StatusReportCorrectionSubmitted);
                        
                        colorStatusArchiveFolderNew.Color = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderNew);
                        colorStatusArchiveFolderIsCompleted.Color = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderIsCompleted);
                        colorStatusArchiveFolderSortout.Color = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderSortout);
                        colorStatusArchiveFolderIssued.Color = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderIssued);
                        colorStatusArchiveFolderDone.Color = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderDone);

                        colorStatusArchiveFolderDestroyed.Color = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderDestroyed);

                        colorStatusArchiveFolderIsSuedEDS.Color = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderIsSuedEDS);
                        colorStatusArchiveFolderReceivedEDS.Color = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderReceivedEDS);
                        colorStatusArchiveFolderIsSuedTK.Color = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderIsSuedTK);
                        colorStatusArchiveFolderReceivedTK.Color = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderReceivedTK);

                        colorStatusTaskCourierPerformed.Color = ColorTranslator.FromHtml(colorStatus.StatusTaskCourierPerformed);
                        colorStatusTaskCourierCanceled.Color = ColorTranslator.FromHtml(colorStatus.StatusTaskCourierCanceled);
                        colorStatusTaskCourierNew.Color = ColorTranslator.FromHtml(colorStatus.StatusTaskCourierNew);

                        colorStatusDealNew.Color = ColorTranslator.FromHtml(colorStatus.StatusDealNew);
                        colorStatusDealPostponed.Color = ColorTranslator.FromHtml(colorStatus.StatusDealPostponed);
                        colorStatusDealCompleted.Color = ColorTranslator.FromHtml(colorStatus.StatusDealCompleted);
                        colorStatusDealPrimary.Color = ColorTranslator.FromHtml(colorStatus.StatusDealPrimary);
                        colorStatusDealAdministrator.Color = ColorTranslator.FromHtml(colorStatus.StatusDealAdministrator);
                    }
                }
            }
        }

        private void btnSaveCustomerSettings_Click(object sender, EventArgs e)
        {
            var form = new CustomerSettingsEdit(CustomerSettings);
            form.ShowDialog();
        }

        private void btnLoadCustomerSettings_Click(object sender, EventArgs e)
        {
            var customerSettingsOid = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.CustomerSettings, -1);

            if (customerSettingsOid > 0)
            {
                using (var session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer())
                {
                    var customerSettingsBase = session.GetObjectByKey<CustomerSettings>(customerSettingsOid);

                    if (customerSettingsBase != null)
                    {
                        CustomerSettings.Edit(customerSettingsBase);
                        propertyGridCustomerSettings.RefreshAllProperties();
                    }
                }
            }
        }
                
        private void btnOpenLetter_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtLetterOid.Text, out int oid))
            {
                using (var uof = new UnitOfWork())
                {
                    var form = new LetterEdit(oid);
                    form.ShowDialog();
                }
            }
        }
        
        private async void btnLetterDelete_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtLetterOid.Text, out int oid))
            {
                using (var uof = new UnitOfWork())
                {
                    var letter = await uof.GetObjectByKeyAsync<Letter>(oid);
                    if (letter != null)
                    {
                        if(XtraMessageBox.Show($"Хотите удалить письмо: {letter}",
                            "Удаление письма по номеру",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            letter.Delete();
                            await uof.CommitChangesAsync();

                            XtraMessageBox.Show($"Удаление успешно окончено.", "Удаление письма по номеру",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void btnEverything_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            if (e.Button.Kind == ButtonPredefines.Ellipsis)
            {
                cls_BaseSpr.ButtonEditButtonClickBase<UserGroup>(null, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.UserGroup, 1, null, null, false, null, string.Empty, false, true);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                await UpdateCustomerContractAsync();
                //await UpdateTaxTypeCustomerAsync();
                //await RemovingElectronicReportingBrokenObjectsAsync();
                //await RemovingTGLogObjectsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static async System.Threading.Tasks.Task UpdateCustomerContractAsync()
        {
            try
            {
                using var uof = new UnitOfWork();
                var contracts = await new XPQuery<Core.Model.InfoContract.Contract>(uof).ToListAsync();

                foreach (var contract in contracts)
                {
                    if (contract.File != null)
                    {
                        if (contract.ContractFiles.FirstOrDefault(f => f.File != null && f.File.Oid == contract.File.Oid) is null)
                        {
                            var contractFile = new Core.Model.InfoContract.ContractFile(uof)
                            {
                                File = contract.File
                            };
                            contractFile.Save();
                            contract.ContractFiles.Add(contractFile);
                            contract.File = null;
                            contract.Save();
                        }
                    }
                }

                await uof.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private static async System.Threading.Tasks.Task UpdateTaxTypeCustomerAsync()
        {
            try
            {
                using var uof = new UnitOfWork();
                var customers = await new XPQuery<Customer>(uof).ToListAsync();

                foreach (var customer in customers)
                {
                    var taxSystem = customer.TaxSystemCustomerString;

                    if (!string.IsNullOrWhiteSpace(taxSystem) && taxSystem.ToLower().Contains("усн"))
                    {
                        customer.TaxType = "УСН Доход";
                        customer.TaxTypePercent = GetTaxTypePercent(taxSystem);
                        customer.Save();
                    }
                }

                await uof.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private static string GetTaxTypePercent(string taxSystem)
        {
            var result = default(string);
            for (int i = 0; i < taxSystem.Length; i++)
            {
                if (taxSystem[i] == '%')
                {
                    var basePosition = i - 1;
                    while (basePosition >= 0 && int.TryParse(taxSystem[basePosition].ToString(), out int value))
                    {
                        result = $"{value}{result}";
                        basePosition--;
                    }
                    break;
                }
            }
            return result;
        }

        private static async System.Threading.Tasks.Task RemovingElectronicReportingBrokenObjectsAsync()
        {
            try
            {
                using var uof = new UnitOfWork();
                var customers = await new XPQuery<Customer>(uof).ToListAsync();

                var useCustomer1 = customers.Where(w => w.ElectronicReporting != null && w.ElectronicReporting.IsDeleted).ToList();
                var useCustomer2 = customers.Where(w => w.ElectronicReportingCustomer != null && w.ElectronicReportingCustomer.IsDeleted).ToList();
                var useCustomer3 = customers.Where(w => w.ElectronicReportingCustomer != null);

                var deleteObjs = new List<ElectronicReportingСustomerObject>();
                foreach (var item1 in useCustomer3)
                {
                    foreach (var item2 in item1.ElectronicReportingCustomer.ElectronicReportingСustomerObjects)
                    {
                        if (item2.ElectronicReporting?.IsDeleted is true)
                        {
                            deleteObjs.Add(item2);
                        }
                    }
                }

                uof.Delete(deleteObjs);
                await uof.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private static async System.Threading.Tasks.Task RemovingTGLogObjectsAsync()
        {
            try
            {
                using var uof = new UnitOfWork();
                var deleteObjs = await new XPQuery<TGLog>(uof).ToListAsync();
                uof.Delete(deleteObjs);
                await uof.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void btnCleaningMail_Click(object sender, EventArgs e)
        {
            try
            {
                var dateTimeNow = DateTime.Now.Date;
                if (dateMail.EditValue is DateTime dateTime && dateTime <= new DateTime(dateTimeNow.Year, 1, 1))
                {
                    if (XtraMessageBox.Show($"Вы действительно хотите произвести удаление всех писем до {dateTime.ToShortDateString()}?",
                                            "Очистка почтового модуля",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        var countLetters = 0;
                        try
                        {
                            using (var uof = new UnitOfWork())
                            {
                                try
                                {
                                    var countDelete = 200;

                                    var letters = await new XPQuery<Letter>(uof)
                                        .Where(w => w.DateCreate < dateTime)
                                        .ToListAsync();
                                    countLetters = letters.Count;

                                    var count = 0;
                                    while (count < countLetters)
                                    {
                                        var index = letters.Count - count;
                                        if (index > countDelete)
                                        {
                                            index = countDelete;
                                        }

                                        var temp = letters.GetRange(count, index);
                                        uof.Delete(temp);
                                        await uof.CommitChangesAsync();

                                        count += countDelete;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());

                                    try
                                    {
                                        var letters = await new XPQuery<Letter>(uof)
                                            .Where(w => w.DateCreate < dateTime)
                                            .ToListAsync();

                                        uof.Delete(letters);
                                        await uof.CommitChangesAsync();
                                    }
                                    catch (Exception) { }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                        }

                        var countDeals = 0;
                        var countTasks = 0;
                        
                        using (var uof = new UnitOfWork())
                        {
                            var deals = await new XPQuery<Deal>(uof).ToListAsync();
                            deals = deals.Where(w => w.Letter == null || (w.Letter != null && w.Letter.IsDeleted)).ToList();
                            var tasks = deals.Select(s => s.Task).ToList();

                            try
                            {
                                countDeals = deals.Count;
                                uof.Delete(deals);
                                await uof.CommitChangesAsync();
                            }
                            catch (Exception ex)
                            {
                                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                            }

                            try
                            {
                                countTasks = tasks.Count;
                                uof.Delete(tasks);
                                await uof.CommitChangesAsync();
                            }
                            catch (Exception ex)
                            {
                                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                            }
                        }

                        using (var uof = new UnitOfWork())
                        {
                            try
                            {
                                var letters = await new XPQuery<Letter>(uof)
                                    .Where(w => w.AnswerLetter != null)
                                    .ToListAsync();

                                var deleteLetters = letters.Where(w => w.AnswerLetter.IsDeleted);

                                foreach (var letter in deleteLetters)
                                {
                                    letter.AnswerLetter = null;
                                    letter.Save();
                                }

                                await uof.CommitChangesAsync();
                            }
                            catch (Exception ex)
                            {
                                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                            }
                        }

                        using (var uof = new UnitOfWork())
                        {
                            try
                            {
                                var letterAttachments = await new XPQuery<LetterAttachment>(uof)
                                    .Where(w => w.Letter == null)
                                    .ToListAsync();

                                uof.Delete(letterAttachments);
                                await uof.CommitChangesAsync();
                            }
                            catch (Exception ex)
                            {
                                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                            }
                        }

                        using (var uof = new UnitOfWork())
                        {
                            try
                            {
                                var letterAttachments = await new XPQuery<LetterAttachment>(uof)
                                    .ToListAsync();

                                var deleteList = letterAttachments.Where(w => w.Letter != null && w.Letter.IsDeleted).ToList();

                                uof.Delete(deleteList);
                                await uof.CommitChangesAsync();
                            }
                            catch (Exception ex)
                            {
                                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                            }
                        }

                        XtraMessageBox.Show(
                            $"Удалено писем: {countLetters}. Удалено сделок: {countDeals}. Удалено задач: {countTasks}",
                            "Очистка почтового модуля",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
                else
                {
                    XtraMessageBox.Show(
                        $"Невозможно выполнить очистку. Возможные причины:" +
                        $"{Environment.NewLine}1. Не указана дата для очистки." +
                        $"{Environment.NewLine}2. Дата очистки должна быть меньше даты начала текущего года.",
                        "Очистка почтового модуля",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }            
        }

        private async void btnDeleteDublicateLetters_Click(object sender, EventArgs e)
        {
            var countLetter = 0;
            var countLetterAttachment = 0;

            try
            {
                using (var uof = new UnitOfWork())
                {
                    var messageIds = await new XPQuery<Letter>(uof)
                        .Select(s => s.MessageId)
                        .ToListAsync();

                    var duplicates = messageIds.GroupBy(x => x).Where(g => g.Count() > 1).Select(g => g.Key)
                        .Where(w => !string.IsNullOrWhiteSpace(w));

                    foreach (var item in duplicates)
                    {
                        using (var collection = new XPCollection<Letter>(uof, new BinaryOperator(nameof(Letter.MessageId), item)))
                        {
                            var list = collection.Skip(1).Select(s => s).ToList();
                            countLetter += list.Count;
                            uof.Delete(list);
                        }
                    }

                    await uof.CommitChangesAsync();
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }


            try
            {
                using (var uof = new UnitOfWork())
                {
                    var dateTimeNow = DateTime.Now.AddDays(-14);
                    var letters = await new XPQuery<Letter>(uof)
                        .Where(w => w.DateCreate >= dateTimeNow.Date)
                        .Where(w => w.LetterAttachments.Count > 0)
                        .ToListAsync();

                    foreach (var item in letters)
                    {
                        var duplicates = item.LetterAttachments.GroupBy(x => x.FullFileName).Where(g => g.Count() > 1).Select(g => g.Key).Where(w => !string.IsNullOrWhiteSpace(w));
                        foreach (var duplicate in duplicates)
                        {
                            var list = item.LetterAttachments.Where(w => w.FullFileName == duplicate).Skip(1).Select(s => s).ToList();
                            countLetterAttachment += list.Count;
                            uof.Delete(list);
                        }
                    }

                    await uof.CommitChangesAsync();
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }

            XtraMessageBox.Show(
                $"Удалено дублей писем: {countLetter}{Environment.NewLine}" +
                    $"Удалено дубликатов вложений: {countLetterAttachment}",
                "Удаление дубликатов",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnTelegramBotToken_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (XtraMessageBox.Show("Вы уверены что хотите удалить токен телеграмм бота?", "Удаление токена", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (Settings != null)
                    {
                        Settings.TelegramBotToken = null;
                        Settings.Save();

                        buttonEdit.EditValue = null;
                        buttonEdit.Properties.ReadOnly = false;
                        buttonEdit.Properties.UseSystemPasswordChar = false;
                        buttonEdit.Properties.Buttons[0].Visible = false;
                    }
                }
            }
        }

        private async void btnSent_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Отправить сообщение пользователя?",
                                    "Отправка сообщения",
                                    MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Question) == DialogResult.OK)
            {
                var message = memoMessage.Text;
                if (!string.IsNullOrWhiteSpace(message))
                {
                    var dateTime = DateTime.Now;
                    var client = TelegramBot.GetTelegramBotClient(XpoDefault.Session);
                    foreach (var item in checkedListBoxControlUser.Items.Where(w => w.CheckState == CheckState.Checked))
                    {
                        try
                        {
                            if (item.Value is User user)
                            {
                                if (user.Staff != null)
                                {
                                    await client.SendTextMessageAsync(user.Staff.TelegramUserId, message, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html).ConfigureAwait(false);
                                    continue;
                                }
                                
                                if (user.Staff is null && user.TelegramUserId != null)
                                {
                                    await client.SendTextMessageAsync(user.TelegramUserId, message, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html).ConfigureAwait(false);
                                }                                
                            }
                        }
                        catch (Exception ex)
                        {
                            RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                        }
                    }

                    Invoke((Action)delegate 
                    {
                        memoHistory.Text += $"[{dateTime.ToShortDateString()} ({dateTime:HH:mm:ss})] -> {message}{Environment.NewLine}";
                        memoMessage.EditValue = null;
                    });                    
                }
            }            
        }

        private void xtraTabControlSettings_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == xtraTabPageTelegram)
            {
                try
                {
                    var users = new XPCollection<User>(DatabaseConnection.WorkSession);
                    users?.Reload();
                    checkedListBoxControlUser.Items.Clear();

                    foreach (var user in users.Where(w => (w.Staff != null && w.Staff.TelegramUserId != null) || (w.TelegramUserId != null )))
                    {
                        checkedListBoxControlUser.Items.Add(user);
                    }
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                }
            }
        }
                
        private void formProgramSettings_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                user?.Reload();
                if (user != null && user.flagAdministrator)
                {
                    if (e.KeyCode == Keys.L && e.Modifiers == Keys.Shift)
                    {
                        xtraTabPageTelegram.PageVisible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void btnUpdateDictionary_Click(object sender, EventArgs e)
        {
            using (var uof = new UnitOfWork())
            {
                var dictionary = await new XPQuery<set_SprShablonView>(uof).ToListAsync();
                var dictionaryDetail = await new XPQuery<set_SprShablonViewDetail>(uof).ToListAsync();

                uof.Delete(dictionaryDetail);
                uof.Delete(dictionary);
                await uof.CommitChangesAsync();
                
                cls_BaseSpr.FirstFillSprShablonTables(uof);
                await uof.CommitChangesAsync();

                XtraMessageBox.Show(
                    $"Все настройки словарей обновлены",
                    "Регламентные операции",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private async void btnUpdatePriceList_Click(object sender, EventArgs e)
        {
            using (var uof = new UnitOfWork())
            {
                await UpdatePriceList(uof, "Наличие сотрудников с особыми условиями труда (вредность, инвалиды, алименты)", 700, "Учет кадров и расчет заработной платы");
                await UpdatePriceList(uof, "Расчет декретных", 1000, "Учет кадров и расчет заработной платы");
                await UpdatePriceList(uof, "Уведомление о принятии/увольнении иностранного сотрудника", 700, "Учет кадров и расчет заработной платы");
                await UpdatePriceList(uof, "Подготовка справок для сотрудников по запросу", 500, "Учет кадров и расчет заработной платы");

                await UpdatePriceList(uof, "Подготовка 3-НДФЛ", 3500, "Подготовка и разработка различных документов и отчетности");
                await UpdatePriceList(uof, "Подготовка корректирующей декларации/отчета (не наша вина)", 5000, "Подготовка и разработка различных документов и отчетности");
                await UpdatePriceList(uof, "Подтверждение основного вида деятельности 1 раз в год", 1000, "Подготовка и разработка различных документов и отчетности");

                await UpdatePriceList(uof, "Подготовка 1 комплекта для получения лизинга/кредита, для банка, для участия в тендере, для встречной проверки, для заключения договора", 3500, "Подготовка документов по текущей деятельности");
                await UpdatePriceList(uof, "Подготовка 1 комплекта документов за поставщика", 700, "Подготовка документов по текущей деятельности");
                await UpdatePriceList(uof, "Проведение сверок с контрагентами (запрос документов, созвон, обмен)", 700, "Подготовка документов по текущей деятельности");
                await UpdatePriceList(uof, "Подготовка путевых листов (один лист по одной машине ежедневно)", 500, "Подготовка документов по текущей деятельности");

                await UpdatePriceList(uof, "Подготовка ответа на требование/решение налоговой инспекции о предоставлении документов и/или пояснений", 1500, "Подготовка документов для налоговой, пенсионного, ФСС, банка и иных органов");
                await UpdatePriceList(uof, "Смена данных ОКВЭД через личный кабинет налогоплательщика", 3500, "Подготовка документов для налоговой, пенсионного, ФСС, банка и иных органов");
                await UpdatePriceList(uof, "Мероприятия по разблокированию банковского счета (не наша вина)", 5000, "Подготовка документов для налоговой, пенсионного, ФСС, банка и иных органов");
                await UpdatePriceList(uof, "Уточнение сведений в налоговых /банках /фондах, не связанных с отчетным периодом, с тек. деятельность и/или возникли по вине клиента", 3500, "Подготовка документов для налоговой, пенсионного, ФСС, банка и иных органов");
                await UpdatePriceList(uof, "Уточнение платежей, зачет недоимок, на возврат средств из бюджета", 700, "Подготовка документов для налоговой, пенсионного, ФСС, банка и иных органов");
                await UpdatePriceList(uof, "Составление отчетности по данным клиента/по выписке банка", 12000, "Подготовка документов для налоговой, пенсионного, ФСС, банка и иных органов");
                await UpdatePriceList(uof, "Подготовка отчетности в Росстат (для тех, кто попал в выборку)", 2500, "Подготовка документов для налоговой, пенсионного, ФСС, банка и иных органов");

                await UpdatePriceList(uof, "Консультация устная (1 час)", 3000, "Консультации");
                await UpdatePriceList(uof, "Разработка договора или других документов (Учетная политика, положение о з/п, и другие ЛНА) (1 документ)", 7000, "Консультации");

                await UpdatePriceList(uof, "Курьерские услуги (поездка)", 500, "Иные услуги");
                await UpdatePriceList(uof, "Сопровождение выездной проверки, передачи дел, участие в переговорах (1 час)", 5000, "Иные услуги");
                await UpdatePriceList(uof, "Стоимость одного нормо-часа работы бухгалтера", 2500, "Иные услуги");
                await UpdatePriceList(uof, "Доступ к системе 1С БУХ", 2000, "Иные услуги");

                XtraMessageBox.Show(
                    $"Прайс обновлен.",
                    "Регламентные операции",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private async System.Threading.Tasks.Task UpdatePriceList(UnitOfWork uof, string name, decimal value, string group = default)
        {
            var priceList = await new XPQuery<PriceList>(uof)
                ?.FirstOrDefaultAsync(f => f.Name == name.Trim());

            var priceGroup = default(PriceGroup);
            if (!string.IsNullOrWhiteSpace(group))
            {
                priceGroup = await new XPQuery<PriceGroup>(uof)
                ?.FirstOrDefaultAsync(f => f.Name == group.Trim());

                if (priceGroup is null)
                {
                    priceGroup = new PriceGroup(uof)
                    {
                        Name = group,
                        Description = group
                    };
                    priceGroup.Save();
                }
            }            
            
            if (priceList is null)
            {
                var code = await new XPQuery<PriceList>(uof)?.MaxAsync(m => m.Kod);

                priceList = new PriceList(uof)
                {
                    Kod = code + 1,
                    Name = name?.Trim(),
                    Description = name?.Trim(),
                    Price = value,
                    PriceGroup = priceGroup
                };

                priceList.Save();
            }

            await uof.CommitChangesAsync();
        }

        private void dateMail_EditValueChanged(object sender, EventArgs e)
        {
            if (sender is DateEdit dateEdit)
            {
                if (dateEdit.EditValue is DateTime date)
                {
                    btnGetMailOverDate.Text = $"Получить (за {date.ToShortDateString()})";
                    btnCleaningMail.Text = $"Удалить (до {date.ToShortDateString()})";
                }
                else
                {
                    btnGetMailOverDate.Text = $"Получение почты";
                    btnCleaningMail.Text = $"Очистка почты";
                }
            }
        }
        
        private async void btnGetMailOverDate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dateMail.EditValue is DateTime dateTime)
                {
                    if (XtraMessageBox.Show($"Вы действительно хотите получить письма за {dateTime.ToShortDateString()}?",
                                            "Операции почтового модуля",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        try
                        {
                            using (var uof = new UnitOfWork())
                            {
                                var groupOperatorStateMailbox = new GroupOperator(GroupOperatorType.Or);

                                var criteriaStateMailboxWorking = new BinaryOperator(nameof(Mailbox.StateMailbox), StateMailbox.Working);
                                groupOperatorStateMailbox.Operands.Add(criteriaStateMailboxWorking);

                                var criteriaStateMailboxReceivingLetters = new BinaryOperator(nameof(Mailbox.StateMailbox), StateMailbox.ReceivingLetters);
                                groupOperatorStateMailbox.Operands.Add(criteriaStateMailboxReceivingLetters);

                                var xpcollectionMailbox = new XPCollection<Mailbox>(uof, groupOperatorStateMailbox);
                                MailClients.FillingListMailClients(xpcollectionMailbox);
                            }

                            foreach (var mailClients in MailClients.ListMailClients)
                            {
                                if (mailClients.IsReceivingLetters is false)
                                {
                                    mailClients.GetLetter += LetterForm.MailClients_GetLetter;
                                    await mailClients.GetAllEmailsByDate(dateTime.Date, 0, await LetterForm.GetEmailFilteringDate(), true).ConfigureAwait(false);                                    
                                    mailClients.GetLetter -= LetterForm.MailClients_GetLetter;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                        }

                        XtraMessageBox.Show(
                            $"Закончено получение почтовых отправлений за {dateTime.ToShortDateString()}",
                            "Операции почтового модуля",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Укажите дату получения писем.",
                        "Операции почтового модуля",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    dateMail.Focus();
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void btnReceiveByEmail_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text?.Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                XtraMessageBox.Show("Укажите корректный email.",
                       "Операции почтового модуля",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
                return;
            }

            try
            {
                if (XtraMessageBox.Show($"Вы действительно хотите получить письма от {email}?",
                                            "Операции почтового модуля",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
                {
                    try
                    {
                        using (var uof = new UnitOfWork())
                        {
                            var mailClientCollection = await new XPQuery<Mailbox>(uof)
                                .Where(w => w.StateMailbox == StateMailbox.Working
                                    || w.StateMailbox == StateMailbox.ReceivingLetters)
                                .ToListAsync();

                            MailClients.FillingListMailClients(mailClientCollection);
                        }

                        foreach (var mailClients in MailClients.ListMailClients)
                        {
                            if (mailClients.IsReceivingLetters is false)
                            {
                                mailClients.GetLetter += LetterForm.MailClients_GetLetter;
                                await mailClients.GetAllEmailsByEmail(email, 0, await LetterForm.GetEmailFilteringDate(), true).ConfigureAwait(false);
                                mailClients.GetLetter -= LetterForm.MailClients_GetLetter;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                    }

                    XtraMessageBox.Show(
                        $"Закончено получение почтовых отправлений от {email}",
                        "Операции почтового модуля",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void btnDataBaseCleanup_Click(object sender, EventArgs e)
        {
            try
            {
                using var uof = new UnitOfWork();
                uof.PurgeDeletedObjects();

                XtraMessageBox.Show(
                  $"Очистка базы данных успешно окончена.",
                  "Очистка базы данных",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                  $"{ex.Message}",
                  "Очистка базы данных",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error);

                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void btnLinkCleaning_Click(object sender, EventArgs e)
        {
            try
            {
                using var uof = new UnitOfWork();
                Core.Controllers.XPObjects.XpoPurgeHelper.ClearDeadReferences(uof);

                XtraMessageBox.Show(
                  $"Операция по поиску и исправлению битых ссылок успешно завершена.",
                  "Чистка ссылок",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                  $"{ex.Message}",
                  "Чистка ссылок",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error);

                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void btnClearReportV2_Click(object sender, EventArgs e)
        {            
            if (PulsLibrary.Extensions.DevForm.DevXtraMessageBox.ShowQuestionXtraMessageBox("Удалить все отчеты?"))
            {
                using (var uof = new UnitOfWork())
                {
                    var reposts = await new XPQuery<ReportChangeCustomerV2>(uof).ToListAsync();
                    var count = reposts.Count;
                    await uof.DeleteAsync(reposts);
                    await uof.CommitTransactionAsync();

                    PulsLibrary.Extensions.DevForm.DevXtraMessageBox.ShowXtraMessageBox($"Удалено отчетов: {count}");
                }
            }
        }

        private async void btnLoadSbis_Click(object sender, EventArgs e)
        {
            using (var ofd = new XtraOpenFileDialog() { Multiselect = true })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var collection = ModelSbisController.GetTemperaturesByArchives(ofd.FileNames);

                    using (var uof = new UnitOfWork())
                    {
                        await collection.CreateReportSBISAsync(uof);
                        await uof.CommitTransactionAsync();
                    }
                }
            }
        }
    }
}