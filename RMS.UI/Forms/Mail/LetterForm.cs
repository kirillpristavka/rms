using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Helpers;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTreeList.Nodes;
using PulsLibrary.Extensions.DevForm;
using RMS.Core.Controllers;
using RMS.Core.Enumerator;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using RMS.Setting.Model.GeneralSettings;
using RMS.UI.Control;
using RMS.UI.Control.Mail;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using TelegramBotRMS.Core.Models;

namespace RMS.UI.Forms.Mail
{
    public partial class LetterForm : XtraForm
    {
        private Session _session;

        private XPCollection<Letter> Letters { get; set; } = new XPCollection<Letter>();
        private XPCollection<Letter> FindLetters { get; set; }

        public MailBoxControl MailBoxControl { get; set; }
        public SystemCatalogControl SystemCatalogControl { get; set; }

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewLetters);
            BVVGlobal.oFuncXpo.PressEnterGrid<Letter, LetterEdit>(gridViewLetters, isUseShow:true, action: async () => await UpdateLetter().ConfigureAwait(false));
        }
        
        private async System.Threading.Tasks.Task UpdateLetter()
        {
            try
            {
                if (gridViewLetters.GetRow(gridViewLetters.FocusedRowHandle) is Letter letter)
                {
                    await RefresMailBoxControll(true, letter.TypeLetter).ConfigureAwait(false);
                    await RefresLetterLayoutControl(true, letter.TypeLetter).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }            
        }

        public LetterForm(Session session)
        {
            InitializeComponent();
            FunctionalGridSetup();
            _session = session ?? DatabaseConnection.GetWorkSession();
        }

        private void LoadAsyncContractsMainGridControl(XPCollection<Letter> xpCollection)
        {
            gridViewLetters.ShowLoadingPanel();
            xpCollection.LoadAsync(new AsyncLoadObjectsCallback(CallBackLoadMainGridControl));
        }

        private void CallBackLoadMainGridControl(ICollection[] result, Exception ex)
        {
            if (gridViewLetters.Columns[nameof(Letter.Oid)] != null)
            {
                gridViewLetters.Columns[nameof(Letter.Oid)].Visible = false;
                gridViewLetters.Columns[nameof(Letter.Oid)].Width = 18;
                gridViewLetters.Columns[nameof(Letter.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridViewLetters.Columns[nameof(Letter.IsRead)] != null)
            {
                gridViewLetters.Columns[nameof(Letter.IsRead)].Visible = false;
                gridViewLetters.Columns[nameof(Letter.IsRead)].Width = 18;
                gridViewLetters.Columns[nameof(Letter.IsRead)].OptionsColumn.FixedWidth = true;
            }

            if (gridViewLetters.Columns[nameof(Letter.MailingAddress)] != null)
            {
                gridViewLetters.Columns[nameof(Letter.MailingAddress)].Visible = false;
            }
            
            var imageCollection = new ImageCollectionStatus();

            if (gridViewLetters.Columns[nameof(Letter.IsLetterAttachments)] != null)
            {
                RepositoryItemImageComboBox imgIsLetterAttachments = gridControlLetters.RepositoryItems.Add(nameof(ImageComboBoxEdit)) as RepositoryItemImageComboBox;
                imgIsLetterAttachments.SmallImages = imageCollection.imageCollectionLetters;
                imgIsLetterAttachments.Items.Add(new ImageComboBoxItem() { Value = true, ImageIndex = 0 });
                imgIsLetterAttachments.Items.Add(new ImageComboBoxItem() { Value = false, ImageIndex = -1 });

                imgIsLetterAttachments.GlyphAlignment = HorzAlignment.Center;
                gridViewLetters.Columns[nameof(Letter.IsLetterAttachments)].ColumnEdit = imgIsLetterAttachments;
                gridViewLetters.Columns[nameof(Letter.IsLetterAttachments)].Width = 18;
                gridViewLetters.Columns[nameof(Letter.IsLetterAttachments)].OptionsColumn.FixedWidth = true;
            }
            
            if (gridViewLetters.Columns[nameof(Letter.DealStatusString)] != null)
            {
                RepositoryItemImageComboBox imgCmbStatus = gridControlLetters.RepositoryItems.Add(nameof(ImageComboBoxEdit)) as RepositoryItemImageComboBox;

                var imageCollectionStatus = new ImageCollectionStatus();
                imgCmbStatus.SmallImages = imageCollectionStatus.imageCollection;

                var statusCollection = new XPCollection<Core.Model.DealStatus>(_session);
                foreach (var item in statusCollection)
                {
                    if (item.IndexIcon != null)
                    {
                        imgCmbStatus.Items.Add(new ImageComboBoxItem()
                        {
                            Value = item.ToString(),
                            ImageIndex = Convert.ToInt32(item.IndexIcon)
                        });
                    }
                }

                imgCmbStatus.GlyphAlignment = HorzAlignment.Center;
                gridViewLetters.Columns[nameof(Letter.DealStatusString)].ColumnEdit = imgCmbStatus;
                gridViewLetters.Columns[nameof(Letter.DealStatusString)].Width = 18;
                gridViewLetters.Columns[nameof(Letter.DealStatusString)].OptionsColumn.FixedWidth = true;                
            }

            if (gridViewLetters.Columns[nameof(Letter.IsReplySent)] != null)
            {
                RepositoryItemImageComboBox imgIsLetterAttachments = gridControlLetters.RepositoryItems.Add(nameof(ImageComboBoxEdit)) as RepositoryItemImageComboBox;
                imgIsLetterAttachments.SmallImages = imageCollection.imageCollectionLetters;
                imgIsLetterAttachments.Items.Add(new ImageComboBoxItem() { Value = true, ImageIndex = 6 });
                imgIsLetterAttachments.Items.Add(new ImageComboBoxItem() { Value = false, ImageIndex = -1 });

                imgIsLetterAttachments.GlyphAlignment = HorzAlignment.Center;
                gridViewLetters.Columns[nameof(Letter.IsReplySent)].ColumnEdit = imgIsLetterAttachments;
                gridViewLetters.Columns[nameof(Letter.IsReplySent)].Width = 18;
                gridViewLetters.Columns[nameof(Letter.IsReplySent)].OptionsColumn.FixedWidth = true;
            }

            SetColumnSettings(gridViewLetters, nameof(Letter.DateCreate), 110, true, HorzAlignment.Center);
            SetColumnSettings(gridViewLetters, nameof(Letter.DateReceiving), 110, true, HorzAlignment.Center);
            SetColumnSettings(gridViewLetters, nameof(Letter.TopicString));
            SetColumnSettings(gridViewLetters, nameof(Letter.LetterRecipient));
            SetColumnSettings(gridViewLetters, nameof(Letter.LetterSender));
            SetColumnSettings(gridViewLetters, nameof(Letter.CustomerString));

            gridViewLetters.HideLoadingPanel();

            if (ex != null)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void SetColumnSettings(GridView gridView, string columnName, int? width = null, bool? isFixedWidth = null, HorzAlignment? horzAlignment = null)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(columnName) && gridView.Columns[columnName] != null)
                {
                    if (gridView.OptionsView.RowAutoHeight is true)
                    {
                        RepositoryItemMemoEdit memoEdit = gridView.GridControl.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                        gridView.Columns[columnName].ColumnEdit = memoEdit;
                    }

                    if (width is int widthResult)
                    {
                        gridView.Columns[columnName].Width = widthResult;
                    }

                    if (isFixedWidth is true)
                    {
                        gridView.Columns[columnName].OptionsColumn.FixedWidth = true;
                    }

                    if (horzAlignment is HorzAlignment horzAlignmentResult)
                    {
                        gridView.Columns[columnName].AppearanceCell.TextOptions.HAlignment = horzAlignmentResult;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async System.Threading.Tasks.Task RenderingMailBoxControl()
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                MailBoxControl = new MailBoxControl(_session);
                MailBoxControl.FocusedNodeChanged += MailboxControl_FocusedNodeChanged;

                Invoke((Action)delegate
                {
                    splitContainerControlMailbox.Panel1.Controls.Add(MailBoxControl);
                });
            });
        }

        private async System.Threading.Tasks.Task RenderingSystemCatalogControl()
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                SystemCatalogControl = new SystemCatalogControl(_session);
                SystemCatalogControl.FocusedNodeChanged += SystemCatalogControl_FocusedNodeChanged;

                Invoke((Action)delegate
                {
                    splitContainerControlMailbox.Panel2.Controls.Add(SystemCatalogControl);
                });
            });
        }

        private bool isShowContactLetterForm = false;
        private bool isEditLetterForm = false;
        private bool isDeleteLetterForm = false;
        private bool isEditCustomersForm = false;
        private async System.Threading.Tasks.Task SetAccessRights()
        {
            try
            {
                using (var uof = new UnitOfWork())
                {
                    var user = await uof.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid);
                    if (user != null)
                    {
                        var accessRights = user.AccessRights;
                        if (accessRights != null)
                        {
                            isShowContactLetterForm = accessRights.IsShowContactLetterForm;
                            isEditLetterForm = accessRights.IsEditLetterForm;
                            isDeleteLetterForm = accessRights.IsDeleteLetterForm;

                            isEditCustomersForm = accessRights.IsEditCustomersForm;
                        }
                    }
                }

                barBtnAddCustomerEmail.Enabled = isEditCustomersForm;
                
                barBtnMarkUnread.Enabled = isEditLetterForm;
                btnRefreshLetterCustomer.Enabled = isEditLetterForm;
                barSubItem1.Enabled = isEditLetterForm;

                btnFilters.Enabled = isShowContactLetterForm;
                barBtnFilter.Enabled = isShowContactLetterForm;
                if (isShowContactLetterForm is false)
                {
                    barBtnHistory.Visibility = BarItemVisibility.Never;
                }

                btnRemoveLetter.Enabled = isDeleteLetterForm;
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void LetterForm_Load(object sender, EventArgs e)
        {
            await SetAccessRights();

            try
            {
                FindLetters = new XPCollection<Letter>(_session, null, new SortProperty(nameof(Letter.DateReceiving), SortingDirection.Descending));

                var criteriaCustomer = await cls_BaseSpr.GetCustomerCriteria(null, nameof(Letter.Customer));
                if (!string.IsNullOrWhiteSpace(criteriaCustomer?.LegacyToString()))
                {
                    var groupOperatorCustomer = new GroupOperator(GroupOperatorType.Or);
                    groupOperatorCustomer.Operands.Add(criteriaCustomer);
                    var criteriaCustomerIsNull = new NullOperator(nameof(Letter.Customer));
                    groupOperatorCustomer.Operands.Add(criteriaCustomerIsNull);

                    FindLetters.Criteria = groupOperatorCustomer;
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }

            var enableOrDisableEmailPreview = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.EnableOrDisableEmailPreview), user: BVVGlobal.oApp.User);
            if (!string.IsNullOrWhiteSpace(enableOrDisableEmailPreview))
            {
                if (enableOrDisableEmailPreview.Equals("0"))
                {
                    splitContainerControlLetters.PanelVisibility = SplitPanelVisibility.Panel1;
                }
            }

            await RenderingMailBoxControl();
            await RenderingSystemCatalogControl();

            try
            {
                await OpenAutoSaveLetterAsync();
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async System.Threading.Tasks.Task OpenAutoSaveLetterAsync()
        {
            var autoSaveLetter = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"autoSaveLetter", workingField: 1, user: BVVGlobal.oApp.User);
            if (int.TryParse(autoSaveLetter, out int result))
            {
                var letter = await new XPQuery<Letter>(_session)?.FirstOrDefaultAsync(f => f.Oid == result);
                if (letter != null && letter.TypeLetter == TypeLetter.Draft)
                {
                    if (DevXtraMessageBox.ShowQuestionXtraMessageBox("У вас есть письмо, с которым вы работали, открыть его?"))
                    {
                        var form = new LetterEdit(letter);
                        form.ShowDialog();
                    }
                    else
                    {
                        await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"autoSaveLetter", "-1", true, true, 1, BVVGlobal.oApp.User);
                    }
                }
            }
        }

        /// <summary>
        /// Получение писем всех активных почтовых ящиков.
        /// </summary>
        private async Task<bool> GetLetter(object obj = default, int countToSave = 0)
        {
            await System.Threading.Tasks.Task.Run(async() =>
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
                        mailClients.Update += MailClients_Update;
                        mailClients.GetLetter += MailClients_GetLetter;
                        mailClients.SaveLetter += MailClients_SaveLetter;

                        if (obj is DateTime date)
                        {
                            await mailClients.GetAllEmailsByDate(date.Date, countToSave, await GetEmailFilteringDate()).ConfigureAwait(false);
                        }
                        else
                        {
                            await mailClients.GetAllEmailsByDate(countToSave: countToSave, emailFilteringDate: await GetEmailFilteringDate()).ConfigureAwait(false);
                        }

                        mailClients.Update -= MailClients_Update;
                        mailClients.GetLetter -= MailClients_GetLetter;
                        mailClients.SaveLetter -= MailClients_SaveLetter;

                        await UpdateLetter().ConfigureAwait(false);
                    }
                }                
            });

            return true;
        }

        public static async Task<DateTime?> GetEmailFilteringDate()
        {
            try
            {
                using (var uof = new UnitOfWork())
                {
                    var settings = await uof.FindObjectAsync<Settings>(null);
                    if (settings != null)
                    {
                        return settings.EmailFilteringDate;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }

            return default;
        }

        public static void GetPopupForm(Letter letter)
        {
            //try
            //{
            //    if (letter != null)
            //    {
            //        var customer = letter.Customer?.ToString() ?? letter.LetterSenderAddress;
            //        var folder = letter.LetterCatalog?.ToString() ?? letter.Mailbox?.ToString();

            //        var popup = new Tulpep.NotificationWindow.PopupNotifier();

            //        popup.AnimationDuration = 1000;
            //        popup.AnimationInterval = 1;
            //        popup.BodyColor =  Color.FromArgb(0, 0, 0);
            //        popup.BorderColor = Color.FromArgb(0, 0, 0);
            //        popup.ContentColor = Color.FromArgb(255, 255, 255);
            //        popup.ContentFont = new Font("Tahoma", 8F);
            //        popup.ContentHoverColor = Color.FromArgb(255, 255, 255);
            //        popup.ContentPadding = new Padding(0);
            //        popup.Delay = 5000;
            //        popup.GradientPower = 100;
            //        popup.HeaderHeight = 1;
            //        popup.ShowCloseButton = false;
            //        popup.ShowGrip = false;
            //        popup.ShowOptionsButton = true;

            //        popup.Image = Properties.Resources.RMS;
            //        popup.ImageSize = new Size(96, 96);

            //        popup.TitleText = $"Новое сообщение {letter.DateReceiving}";
            //        popup.ContentText = $"Получено от: {customer}{Environment.NewLine}" +
            //            $"Тема: {Letter.ByteToString(letter.Topic)}{Environment.NewLine}" +
            //            $"Каталог: {folder}";
            //        popup.Popup();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            //}            
        }
        
        private async void MailClients_SaveLetter(object sender, Letter letter)
        {
            try
            {
                Invoke((Action)delegate 
                {
                    Letters?.Reload();
                });

                if (letter.LetterCatalog != null)
                {

                }
                else
                {
                    foreach (var control in splitContainerControlMailbox.Panel1.Controls)
                    {
                        try
                        {
                            if (control is MailBoxControl mailBoxControl)
                            {
                                await mailBoxControl.RefresControlAsync(letter.TypeLetter).ConfigureAwait(false);
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            LoggerController.WriteLog(ex?.ToString());
                        }
                    }
                }
                
                await RefresMailBoxControll().ConfigureAwait(false);
                await RefresLetterLayoutControl().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
            
            await SendTelegramMessageLetter(letter);

            if (bool.TryParse(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_IsUsePopupWindow", "false", false, false, 1, BVVGlobal.oApp.User), out bool result) && result is true)
            {
                Invoke((Action)delegate
                {
                    GetPopupForm(letter);
                });
            }
        }

        public static async System.Threading.Tasks.Task SendTelegramMessageLetter(Letter letter, System.Threading.CancellationToken token = default)
        {
            try
            {
                var client = TelegramBot.GetTelegramBotClient();
                var staff = letter?.Customer?.AccountantResponsible;
                if (staff != null && staff.TelegramUserId != null)
                {
                    var text = $"📩 Получено новое письмо от <i>{letter?.Customer}</i>";
                    
                    text += $"{Environment.NewLine}<u>Письмо создано</u>: {letter.DateCreate.ToShortDateString()} ({letter.DateCreate.ToString("HH:mm:ss")})";
                    text += $"{Environment.NewLine}<u>Письмо получено</u>: {letter.DateReceiving.ToShortDateString()} ({letter.DateReceiving.ToString("HH:mm:ss")})";

                    if (letter.LetterCatalog != null)
                    {
                        text += $"{Environment.NewLine}<u>Каталог</u>: {letter.LetterCatalog}";
                    }

                    if (!string.IsNullOrWhiteSpace(letter.TopicString))
                    {
                        text += $"{Environment.NewLine}<u>Тема</u>: {letter.TopicString}";
                    }

                    await client.SendTextMessageAsync(staff.TelegramUserId, text, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html, cancellationToken: token);
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        public static void MailClients_GetLetter(object sender, string folderName, int count, int number)
        {
            try
            {
                var mailClient = sender as MailClients;
                var form = Program.MainForm;

                if (mailClient != null && form != null && form.IsDisposed is false)
                {
                    form.Progress_Start(0, count, $"Обработка почтового ящика {mailClient.Mailbox} <-> Каталог: {folderName} ");
                    form.Progress_Position(number);

                    if (count == number)
                    {
                        form.Progress_Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
                Program.MainForm?.Progress_Stop();
            }
        }

        private async void MailClients_Update(bool isUpdate)
        {
            if (isUpdate)
            {
                Invoke((Action)delegate
                {
                    Letters.Reload();
                    SystemCatalogControl?.LetterCatalogs?.Reload();
                });

                await RefresMailBoxControll().ConfigureAwait(false);
                await RefresLetterLayoutControl().ConfigureAwait(false);
            }
        }
        
        private async System.Threading.Tasks.Task RefresMailBoxControll(bool isNodeFocused = false, TypeLetter? typeLetter = null)
        {
            await System.Threading.Tasks.Task.Run(async () =>
            {
                foreach (var control in splitContainerControlMailbox.Panel1.Controls)
                {
                    try
                    {
                        if (control is MailBoxControl mailBoxControl)
                        {
                            if (isNodeFocused is true && mailBoxControl.TreeList.FocusedNode != null)
                            {
                                await mailBoxControl.RefresTreeListNodeFocused(mailBoxControl.TreeList.FocusedNode, typeLetter);
                            }
                            else if (isNodeFocused is false)
                            {
                                await mailBoxControl.RefresControl();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LoggerController.WriteLog(ex?.ToString());
                    }
                }
            });
        }

        private async System.Threading.Tasks.Task RefresLetterLayoutControl(bool isNodeFocused = false, TypeLetter? typeLetter = null)
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                foreach (var control in splitContainerControlMailbox.Panel2.Controls)
                {
                    try
                    {
                        if (control is CustomerLetterLayoutControl customerLetterLayoutControl)
                        {
                            if (isNodeFocused is true && customerLetterLayoutControl.TreeList.FocusedNode != null)
                            {
                                customerLetterLayoutControl.RefresTreeListNodeFocused(customerLetterLayoutControl.TreeList.FocusedNode, typeLetter);
                            }
                            else if (isNodeFocused is false)
                            {
                                customerLetterLayoutControl.RefresControl();
                            }
                        }
                        else if (control is SystemCatalogControl systemCatalogControl)
                        {
                            systemCatalogControl.UpdateNotReadLettersCoun();
                        }
                    }
                    catch (Exception ex)
                    {
                        LoggerController.WriteLog(ex?.ToString());
                    }
                }
            });
        }
        
        private void SystemCatalogControl_FocusedNodeChanged(GroupOperator groupOperator, TreeListNode treeListNode)
        {            
            MailBoxControl?.NullFocusedNode();
            Letters = new XPCollection<Letter>(_session, groupOperator, new SortProperty(nameof(Letter.DateReceiving), SortingDirection.Descending));
            LoadAsyncContractsMainGridControl(Letters);
            gridControlLetters.DataSource = Letters;

            if (gridViewLetters.Columns[nameof(Letter.UserString)] != null)
            {
                gridViewLetters.Columns[nameof(Letter.UserString)].Visible = false;
            }
        }

        private void MailboxControl_FocusedNodeChanged(GroupOperator groupOperator, TreeListNode treeListNode)
        {
            SystemCatalogControl?.NullFocusedNode();
            Letters = new XPCollection<Letter>(_session, groupOperator, new SortProperty(nameof(Letter.DateReceiving), SortingDirection.Descending));

            try
            {
                if (treeListNode.GetValue(0).Equals("Все отправленные"))
                {
                    
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
            
            LoadAsyncContractsMainGridControl(Letters);
            gridControlLetters.DataSource = Letters;

            if (gridViewLetters.Columns[nameof(Letter.UserString)] != null)
            {
                if (treeListNode.GetValue(0).Equals("Все отправленные"))
                {
                    gridViewLetters.Columns[nameof(Letter.UserString)].Visible = true;
                }
                else
                {
                    gridViewLetters.Columns[nameof(Letter.UserString)].Visible = false;
                }                
            }
        }
        
        private void btnMailboxSetup_Click(object sender, EventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.Mailbox, -1);
        }
                
        private async System.Threading.Tasks.Task IsReadLetterAsync(Letter letter)
        {
            if (!letter.IsRead)
            {
                using (var uof = new UnitOfWork())
                {
                    var letterUOF = await uof.GetObjectByKeyAsync<Letter>(letter.Oid);
                    if (letterUOF != null)
                    {
                        letterUOF.IsRead = true;
                        letterUOF.Save();
                        await uof.CommitChangesAsync();

                        Invoke((Action)delegate
                        {
                            letter.Reload();
                        });
                    }
                }

                await RefresMailBoxControll(true, letter.TypeLetter).ConfigureAwait(false);
                await RefresLetterLayoutControl(true, letter.TypeLetter).ConfigureAwait(false);
            }
        }

        private async System.Threading.Tasks.Task RenderingLetterFormAsync(Letter letter)
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                var letterSender = default(string);
                var richLetterString = default(string);

                if (letter.Customer is null)
                {
                    letterSender = letter.LetterSender;
                }
                else
                {
                    letterSender = letter.Customer?.ToString();
                }                

                var letterBody = Letter.ByteToString(letter.TextBody);
                var letterHtmlBody = Letter.ByteToString(letter.HtmlBody);                

                if (string.IsNullOrWhiteSpace(letterHtmlBody))
                {
                    richLetterString = letterBody;
                }
                else
                {
                    richLetterString = letterHtmlBody;
                }

                Invoke((Action)delegate
                {
                    btnLetterSender.EditValue = letterSender;
                    txtLetterRecipient.Text = letter.LetterRecipient;
                    txtTopic.Text = letter.TopicString;
                    richLetter.HtmlText = richLetterString;
                });
            });
        }       

        private async void gridViewLetters_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                if (splitContainerControlLetters.Panel2.Visible is false)
                {
                    return;
                }
                
                if (gridViewLetters.IsEmpty)
                {
                    btnLetterSender.EditValue = null;
                    txtLetterRecipient.Text = null;
                    txtTopic.Text = null;
                    richLetter.HtmlText = null;
                    return;
                }
                
                var letter = gridViewLetters.GetRow(gridViewLetters.FocusedRowHandle) as Letter;
                if (letter != null)
                {
                    letter?.DealStatus?.Reload();
                    if (letter.Customer != null)
                    {
                        var customer = await _session.GetObjectByKeyAsync<Customer>(letter.Customer.Oid);

                        if (customer is null || customer.IsDeleted)
                        {
                            letter.Customer = null;
                            letter.Save();
                        }
                    }

                    //await IsReadLetterAsync(letter).ConfigureAwait(false);
                    await RenderingLetterFormAsync(letter).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        /// <summary>
        /// Скрыть или показать кнопку клиента.
        /// </summary>
        /// <param name="isVisible"></param>
        private void CustomerButtonVisible(bool isVisible)
        {
            foreach (EditorButton button in btnLetterSender.Properties.Buttons)
            {
                if (button.Kind == ButtonPredefines.Ellipsis)
                {
                    btnLetterSender.Properties.Buttons[0].Visible = isVisible;
                }
            }
        }

        private bool isFinishGetLetter = true;

        private async void btnGetLetters_Click(object sender, EventArgs e)
        {
            try
            {
                if (MailClients.IsFinishGetLetter is false || isFinishGetLetter is false)
                {
                    XtraMessageBox.Show("Сборщик писем работает. Дождитесь завершения приема сообщений!", "Получение почты", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                isFinishGetLetter = false;
                
                var dayString = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.CountOfDaysToAcceptLetter), user: BVVGlobal.oApp.User);
                var countLetterToSave = 0;
                var countString = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.CountOfLetterToSave), "0", user: BVVGlobal.oApp.User);

                if (int.TryParse(countString, out int count))
                {
                    countLetterToSave = count;
                }
                
                if (int.TryParse(dayString, out int day))
                {
                    await GetLetter(DateTime.Now.AddDays(day * -1), countLetterToSave).ConfigureAwait(false);
                }
                else
                {
                    await GetLetter(DateTime.Now.AddDays(-3), countLetterToSave).ConfigureAwait(false);
                }

                await RefresMailBoxControll().ConfigureAwait(false);
                await RefresLetterLayoutControl().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());                
            }
            finally
            {
                isFinishGetLetter = true;
            }
        }

        private void btnNewLetter_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new LetterEdit();
                //form.ShowDialog();
                form.XtraFormShow();
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }     

        private async void btnRemoveLetter_Click(object sender, EventArgs e)
        {
            if (gridViewLetters.IsEmpty)
            {
                return;
            }

            var listLetter = new List<Letter>();
            var selectionRows = gridViewLetters.GetSelectedRows();
            
            foreach (var focusedRowHandle in selectionRows)
            {
                if (gridViewLetters.GetRow(focusedRowHandle) is Letter letter)
                {
                    listLetter.Add(letter);
                }
            }

            if (XtraMessageBox.Show($"Будет удалено писем: {listLetter.Count()}{Environment.NewLine}Хотите продолжить?",
                    "Удаление писем",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {

                gridViewLetters.ClearSelection();
                gridViewLetters.SelectRow((selectionRows?.FirstOrDefault() ?? -1) - 1);
                
                await System.Threading.Tasks.Task.Run(async () =>
                {
                    using (var uof = new UnitOfWork())
                    {
                        foreach (var item in listLetter)
                        {
                            try
                            {
                                var letter = await uof.GetObjectByKeyAsync<Letter>(item.Oid);

                                if (letter != null)
                                {
                                    if (letter.TypeLetter == TypeLetter.Basket || letter.TypeLetter == TypeLetter.Spam || letter.LetterCatalog != null)
                                    {
                                        var deal = letter.Deal;
                                        if (deal != null)
                                        {
                                            deal?.Delete();
                                        }
                                        letter?.Delete();
                                    }
                                    else
                                    {
                                        letter.TypeLetter = TypeLetter.Basket;
                                        letter.Save();
                                    }

                                    await uof.CommitTransactionAsync().ConfigureAwait(false);

                                    Invoke((Action)delegate
                                    {
                                        Letters?.Reload();
                                        FindLetters?.Reload();
                                    });
                                    
                                    await RefresMailBoxControll().ConfigureAwait(false);
                                    await RefresLetterLayoutControl().ConfigureAwait(false);
                                }
                            }
                            catch (Exception ex)
                            {
                                LoggerController.WriteLog(ex?.ToString());
                            }
                        }
                    }

                    Invoke((Action)delegate 
                    {
                        Letters?.Reload();
                        FindLetters?.Reload();
                    });
                });
            }
        }

        private Core.Model.DealStatus DealStatusNew { get; }
        
        private void gridViewLetters_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gridView = sender as GridView;

            if (gridView.Columns[nameof(Letter.IsRead)] != null)
            {
                var statusReport = Convert.ToBoolean(gridView.GetRowCellValue(e.RowHandle, gridView.Columns[nameof(Letter.IsRead)]));

                if (!statusReport)
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.BackColor = Color.LightGreen;
                }
                else
                {
                    try
                    {                        
                        var obj = gridView.GetRow(e.RowHandle) as Letter;

                        if (obj != null)
                        {
                            var color = obj.DealStatus?.Color;
                            if (!string.IsNullOrWhiteSpace(color))
                            {
                                e.Appearance.BackColor = ColorTranslator.FromHtml(color);
                            }
                        }                        
                        
                        //var column = gridView.Columns[nameof(Letter.DealStatusString)];
                        //if (column != null && gridView.GetRowCellValue(e.RowHandle, column) is string result)
                        //{
                        //    if (!string.IsNullOrWhiteSpace(result) && (result.Equals("Выполнена") || result.Equals("Снята задача")))
                        //    {
                        //        e.Appearance.BackColor = Color.LightGray;
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        LoggerController.WriteLog(ex?.ToString());
                    }                    
                }
            }
        }

        private async void barBtnAddCustomerEmail_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewLetters.IsEmpty)
            {
                return;
            }

            var customerOid = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.Customer, -1);
            if (customerOid != -1)
            {
                var letter = gridViewLetters.GetRow(gridViewLetters.FocusedRowHandle) as Letter;
                var customer = await _session.GetObjectByKeyAsync<Customer>(customerOid);
                
                if (customer != null)
                {
                    if (!string.IsNullOrWhiteSpace(letter.LetterSenderAddress))
                    {
                        btnRefreshLetterCustomer.Enabled = false;
                        var contact = await _session.FindObjectAsync<CustomerEmail>(new BinaryOperator(nameof(CustomerEmail.Email), letter.LetterSenderAddress));

                        if (contact is null)
                        {
                            contact = new CustomerEmail(_session)
                            {
                                Email = letter.LetterSenderAddress
                            };
                            customer.CustomerEmails.Add(contact);
                            customer.Save();

                            if (XtraMessageBox.Show($"{letter.LetterSenderAddress} успешно добавлен клиенту {customer}. Хотите отредактировать личную информацию?", "Редактирование контакта",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                            {
                                var form = new ContactEdit(contact);
                                form.ShowDialog();
                            }

                            await RefreshLetterCustomer(letter.LetterSenderAddress).ConfigureAwait(false);

                            XtraMessageBox.Show($"Email успешно добавлен клиенту {customer} ", "Сохранение успешно завершено",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //SystemCatalogControl?.CreateNodes();
                        }
                        else
                        {
                            if (contact.Customer.Oid != customer.Oid)
                            {
                                XtraMessageBox.Show($"Такой Email присутствует у другого у клиента {contact.Customer}", "Сохранение запрещено",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                await RefreshLetterCustomer(letter.LetterSenderAddress).ConfigureAwait(false);
                                XtraMessageBox.Show($"Такой Email присутствует у клиента {customer} ", "Сохранение отклонено",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show($"В письме отсутствует адрес отправителя", "Ошибка адреса",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void gridViewLetters_MouseDown(object sender, MouseEventArgs e)
        {
            DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            GridView gridview = sender as GridView;
            GridHitInfo gridHitInfo = gridview.CalcHitInfo(dxMouseEventArgs.Location);
            if (dxMouseEventArgs.Button == MouseButtons.Right)
            {
                if (gridHitInfo.HitTest != GridHitTest.Footer && gridHitInfo.HitTest != GridHitTest.Column)
                {
                    popupMenuLetter.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
            if (dxMouseEventArgs.Button == MouseButtons.Left)
            {
                try
                {
                    var letter = gridview.GetRow(gridview.FocusedRowHandle) as Letter;
                    if (letter != null)
                    {
                        letter?.Deal?.Reload();
                    }
                }
                catch (Exception ex)
                {
                    LoggerController.WriteLog(ex?.ToString());
                }
            }
        }

        private async void btnRefreshLetterCustomer_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnRefreshLetterCustomer.Enabled = false;

            await System.Threading.Tasks.Task.Run(async() =>
            {
                await RefreshLetterCustomer().ConfigureAwait(false);
            });            
        }
        
        private async System.Threading.Tasks.Task RefreshLetterCustomer(string letterSenderAddress = default)
        {
            try
            {                
                using (var uof = new UnitOfWork())
                {
                    var criteriaOperatorSender = default(CriteriaOperator);
                    if (!string.IsNullOrWhiteSpace(letterSenderAddress))
                    {
                        criteriaOperatorSender = new BinaryOperator(nameof(Letter.LetterSenderAddress), letterSenderAddress);
                    }

                    var xpCollectionLetter = new XPCollection<Letter>(uof, criteriaOperatorSender);

                    var count = xpCollectionLetter?.Count ?? 0;
                    Program.MainForm.Progress_Start(0, count, $"Обновление принадлежности клиентов к письмам ");
                    
                    var number = 1;                    
                    foreach (var letter in xpCollectionLetter)
                    {
                        if (!string.IsNullOrWhiteSpace(letter.LetterSenderAddress))
                        {
                            var groupOperator = new GroupOperator(GroupOperatorType.Or);

                            var critreiaEmail = new ContainsOperator(nameof(Customer.CustomerEmails), new BinaryOperator(nameof(CustomerEmail.Email), letter.LetterSenderAddress.Trim()));
                            groupOperator.Operands.Add(critreiaEmail);

                            var critreiaEmail2 = new ContainsOperator(nameof(Customer.CustomerEmails), new BinaryOperator(nameof(CustomerEmail.Email2), letter.LetterSenderAddress.Trim()));
                            groupOperator.Operands.Add(critreiaEmail2);

                            var customer = await uof.FindObjectAsync<Customer>(groupOperator);

                            if (customer != null)
                            {
                                try
                                {
                                    if (letter.Customer != customer)
                                    {
                                        letter.Customer = customer;
                                        letter.Save();
                                        await uof.CommitChangesAsync().ConfigureAwait(false);
                                    }                                   
                                }
                                catch (Exception ex)
                                {
                                    LoggerController.WriteLog(ex?.ToString());
                                }
                            }
                        }

                        Program.MainForm.Progress_Position(number);
                        if (count <= number)
                        {
                            Program.MainForm.Progress_Stop();
                        }
                        number++;
                    }

                    xpCollectionLetter.Dispose();                    
                }

                Invoke((Action)delegate
                {
                    Letters?.Reload();
                    btnRefreshLetterCustomer.Enabled = true;
                });
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }
        
        private async void LetterTransfer(TypeLetter typeLetter, bool isDeleteDeal = false)
        {
            try
            {
                if (gridViewLetters.IsEmpty)
                {
                    return;
                }

                var letter = gridViewLetters.GetRow(gridViewLetters.FocusedRowHandle) as Letter;
                if (letter != null)
                {
                    letter.TypeLetter = typeLetter;

                    if (isDeleteDeal)
                    {
                        var deal = letter.Deal;
                        if (deal != null)
                        {
                            deal.Letter = null;
                            deal.Save();
                            deal.Delete();
                        }

                        letter.Deal = null;
                        letter.LetterCatalog = null;
                    }

                    letter.Save();

                    Invoke((Action)delegate
                    {
                        try
                        {
                            Letters?.Reload();
                            FindLetters?.Reload();
                        }
                        catch (Exception ex)
                        {
                            LoggerController.WriteLog(ex?.ToString());
                        }
                    });

                    await RefresMailBoxControll().ConfigureAwait(false);
                    await RefresLetterLayoutControl().ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void barBtnTransferBasket_ItemClick(object sender, ItemClickEventArgs e)
        {
            LetterTransfer(TypeLetter.Basket);
        }

        private void barBtnTransferSpam_ItemClick(object sender, ItemClickEventArgs e)
        {
            LetterTransfer(TypeLetter.Spam, true);
        }

        private void barBtnTransferDraft_ItemClick(object sender, ItemClickEventArgs e)
        {
            LetterTransfer(TypeLetter.Draft);
        }

        private async void barBtnChoice_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewLetters.IsEmpty)
            {
                return;
            }

            var form = new SystemCatalogEdit(_session);
            form.ShowDialog();

            if (form.LetterCatalog != null)
            {
                var listLetter = new List<Letter>();

                foreach (var focusedRowHandle in gridViewLetters.GetSelectedRows())
                {
                    var letter = gridViewLetters.GetRow(focusedRowHandle) as Letter;

                    if (letter != null)
                    {
                        listLetter.Add(letter);
                    }
                }

                if (XtraMessageBox.Show($"Вы точно хотите перенести выделенные письма в каталог: {form.LetterCatalog}?",
                    "Перемещение",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
                {
                    foreach (var item in listLetter)
                    {
                        var idCatalog = item.LetterCatalog?.Oid;
                        item.LetterCatalog = null;
                        item.Save();

                        if (idCatalog != null && idCatalog > 0)
                        {
                            var catalog = await _session.GetObjectByKeyAsync<LetterCatalog>(idCatalog);
                            catalog?.Letters?.Reload();
                        }                        
                    }
                    
                    form.LetterCatalog.Letters.AddRange(listLetter);
                    form.LetterCatalog.Save();
                    
                    Letters?.Reload();
                    //SystemCatalogControl?.LetterCatalogs?.Reload();
                    await RefresMailBoxControll().ConfigureAwait(false);
                }
            }
        }

        private void barBtnFilter_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewLetters.IsEmpty)
            {
                return;
            }

            var letter = gridViewLetters.GetRow(gridViewLetters.FocusedRowHandle) as Letter;
            if (letter != null)
            {
                var letterFilterEdit = new LetterFilterEdit(letter.LetterSenderAddress);
                letterFilterEdit.ShowDialog();
                barBtnRefresh_ItemClick(null, null);
            }                
        }

        private void btnFilters_Click(object sender, EventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.LetterFilter, -1);
            barBtnRefresh_ItemClick(null, null);
        }

        private async void barBtnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Invoke((Action)delegate
                {
                    Letters?.Reload();
                    FindLetters?.Reload();
                });

                await RefresMailBoxControll().ConfigureAwait(false);
                await RefresLetterLayoutControl().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void barBtnHistory_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewLetters.IsEmpty)
            {
                return;
            }

            var letter = gridViewLetters.GetRow(gridViewLetters.FocusedRowHandle) as Letter;

            if (letter != null)
            {
                var form = default(LetterHistoryForm);

                if (letter.Customer != null)
                {
                    form = new LetterHistoryForm(_session, letter.Customer);
                }
                else
                {
                    form = new LetterHistoryForm(_session, letter.LetterSenderAddress);
                }
                form.Show();
            }
        }

        private void barBtnTaskAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            BVVGlobal.oFuncXpo.CreateTask<TaskEdit>(_session);
        }

        private void barBtnTaskAdd_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            BVVGlobal.oFuncXpo.CreateTask<TaskEdit>(_session);
        }

        private async void barBtnMarkUnread_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewLetters.IsEmpty)
            {
                return;
            }

            var letter = gridViewLetters.GetRow(gridViewLetters.FocusedRowHandle) as Letter;
            if (letter != null)
            {
                letter.IsRead = false;
                letter.Save();
                
                await RefresMailBoxControll(true, letter.TypeLetter).ConfigureAwait(false);
                await RefresLetterLayoutControl(true, letter.TypeLetter).ConfigureAwait(false);
            }
        }

        private bool showAutoFilterRow = false;
        
        private void btnFind_Click(object sender, EventArgs e)
        {
            if (showAutoFilterRow)
            {
                showAutoFilterRow = false;
                lblAllLetters.Visible = false;
                
                LoadAsyncContractsMainGridControl(Letters);
                gridControlLetters.DataSource = Letters;
            }
            else
            {
                showAutoFilterRow = true;
                lblAllLetters.Visible = true;

                LoadAsyncContractsMainGridControl(FindLetters);
                gridControlLetters.DataSource = FindLetters;
            }

            gridViewLetters.OptionsFind.AlwaysVisible = showAutoFilterRow;
        }

        private void gridViewLetters_ColumnFilterChanged(object sender, EventArgs e)
        {
            var gridview = sender as GridView;
            try
            {
                gridViewLetters_FocusedRowChanged(gridview, new FocusedRowChangedEventArgs(-1, gridview.FocusedRowHandle));
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void LetterForm_Activated(object sender, EventArgs e)
        {
            try
            {
                if (gridControlLetters.DataSource != null && !gridViewLetters.IsEmpty)
                {
                    var info = (GridViewInfo)gridViewLetters.GetViewInfo();

                    foreach (var rowInfo in info.RowsInfo)
                    {
                        int rowHandle = rowInfo.RowHandle;

                        if (gridViewLetters.IsDataRow(rowHandle))
                        {
                            var letter = gridViewLetters.GetRow(rowHandle) as Letter;
                            if (letter != null)
                            {
                                letter?.Reload();
                                letter?.Deal?.Reload();
                            }
                        }
                    }
                                       
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }            
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Letters?.Reload();
                FindLetters?.Reload();

                await RefresMailBoxControll().ConfigureAwait(false);
                await RefresLetterLayoutControl().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }            
        }

        private void barBtnControlSystemAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewLetters.IsEmpty)
            {
                return;
            }

            var obj = gridViewLetters.GetRow(gridViewLetters.FocusedRowHandle) as Letter;
            if (obj != null)
            {
                var form = new ControlSystemEdit(obj);
                form.ShowDialog();
            }
        }

        private async void btnAnswer_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewLetters.IsEmpty)
            {
                return;
            }

            var obj = gridViewLetters.GetRow(gridViewLetters.FocusedRowHandle) as Letter;
            if (obj != null)
            {
                try
                {
                    if (obj.TypeLetter == TypeLetter.InBox)
                    {
                        var topic = $"RE: {Letter.ByteToString(obj.Topic)}";

                        var form = new LetterEdit(obj.Mailbox, obj.LetterSenderAddress, topic, obj.HtmlBody, answerLetter: obj);
                        form.ShowDialog();
                    }

                    Letters?.Reload();
                }
                catch (Exception ex)
                {
                    LoggerController.WriteLog(ex?.ToString());
                }

                await UpdateLetter().ConfigureAwait(false);
            }
        }

        private void gridViewLetters_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            try
            {
                if (isShowContactLetterForm is false)
                {
                    if (e.Column.FieldName.Contains("LetterSender"))
                    {
                        e.DisplayText = Letter.DelEmailFromText(e.Value?.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }
    }
}