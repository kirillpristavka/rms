using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout.Utils;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.Mail;
using System;
using System.Diagnostics;
using System.Drawing;
using Telegram.Bot;
using TelegramBotRMS.Core.Models;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class DealEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        public Deal Deal { get; private set; }
        public Task Task { get; private set; }

        private bool IsBulkReplacement { get; }

        private Staff firstStaff;
        
        public DealEdit(Session session = null, bool isBulkReplacement = false)
        {
            InitializeComponent();
            
            if (session != null)
            {
                Session = session;
            }
            
            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                Deal = new Deal(Session)
                {
                    DealStatus = Session.FindObject<DealStatus>(new BinaryOperator(nameof(DealStatus.IsDefault), true))
                };
            }

            IsBulkReplacement = isBulkReplacement;
        }

        public DealEdit(int id) : this()
        {
            if (id > 0)
            {
                Deal = Session.GetObjectByKey<Deal>(id);
            }
        }

        public DealEdit(Deal deal) : this()
        {
            Session = deal.Session;
            Deal = deal;
        }

        private bool isEditDealForm = false;
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
                            isEditDealForm = accessRights.IsEditDealForm;
                        }

                        btnSaveDeal.Enabled = isEditDealForm;
                        btnBulkReplacement.Enabled = isEditDealForm;
                        checkIsUseTask.Enabled = isEditDealForm;

                        CustomerEdit.CloseButtons(btnCustomer, isEditDealForm);
                        CustomerEdit.CloseButtons(btnStaff, isEditDealForm);
                        CustomerEdit.CloseButtons(btnDealStatus, isEditDealForm);
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            await SetAccessRights();

            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Customer>(Session, btnCustomer, cls_App.ReferenceBooks.Customer, isEnable: isEditDealForm);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Staff>(Session, btnStaff, cls_App.ReferenceBooks.Staff, isEnable: isEditDealForm);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<DealStatus>(Session, btnDealStatus, cls_App.ReferenceBooks.DealStatus, isEnable: isEditDealForm);

            Deal?.Reload();
            Deal?.Task?.Reload();

            checkIsUseTask.EditValue = Deal?.IsUseTask;

            if (Deal != null && (Deal.Task != null || Deal.IsUseTask))
            {
                Task = Deal?.Task;
                checkIsUseTask.EditValue = true;
                btnTask.Properties.ReadOnly = false;
            }            

            if (IsBulkReplacement)
            {
                layoutControlItemIsNullArgs.Visibility = LayoutVisibility.Always;
                layoutControlItemSaveDeal.Visibility = LayoutVisibility.Never;
                layoutControlItemBulkReplacement.Visibility = LayoutVisibility.Always;
                checkIsUseTask.Enabled = false;
                txtNumber.Properties.ReadOnly = true;
                txtName.Properties.ReadOnly = true;
                memoDescription.Properties.ReadOnly = true;
            }
            else
            {
                layoutControlItemIsNullArgs.Visibility = LayoutVisibility.Never;
                txtNumber.EditValue = Deal.Number;
                txtName.EditValue = Deal.Number;

                var customer = default(Customer);
                
                if (Deal.Customer is null && Deal.Letter != null)
                {
                    if (Deal.Letter.Customer is null)
                    {
                        customer = Letter.GetCustomer(Session, Deal.Letter.LetterSenderAddress);
                        if (customer != null)
                        {
                            Deal.Letter.Customer = customer;
                            Deal.Letter.Save();
                        }
                    }
                    else
                    {
                        customer = Deal.Letter.Customer;
                    }
                }
                else
                {
                    customer = Deal.Customer;
                }

                if (Deal.Customer is null)
                {
                    btnCustomer.EditValue = customer;
                }
                else
                {
                    btnCustomer.EditValue = Deal.Customer;
                }

                if (Deal.Staff is null)
                {
                    btnStaff.EditValue = customer?.AccountantResponsible;
                }
                else
                {
                    btnStaff.EditValue = Deal.Staff;
                }

                btnTask.EditValue = Task;
                memoDescription.EditValue = Deal.Description;
                btnLetter.EditValue = Deal.Letter;

                btnDealStatus.EditValue = Deal.DealStatus;

                if (btnStaff.EditValue is Staff obj)
                {
                    firstStaff = obj;
                }
            }            
        }

        private async System.Threading.Tasks.Task<bool> Save()
        {            
            try
            {
                if (Deal is null)
                {
                    return false;
                }

                var description = memoDescription.EditValue;

                Deal.Number = txtNumber.Text;
                Deal.Name = txtName.Text;               

                if (btnCustomer.EditValue is Customer customer)
                {
                    Deal.Customer = customer;
                }
                else
                {
                    Deal.Customer = null;
                }

                if (btnStaff.EditValue is Staff staff)
                {
                    Deal.Staff = staff;
                }
                else
                {
                    Deal.Staff = null;
                }

                if (btnLetter.EditValue is Letter letter)
                {
                    Deal.Letter = letter;

                    if (!Deal.Letter.IsRead)
                    {
                        Deal.Letter.IsRead = true;
                        Deal.Letter.Save();
                    }
                }
                else
                {
                    Deal.Letter = null;
                }
                
                var dealStatus = default(DealStatus);
                if (btnDealStatus.EditValue is DealStatus _dealStatus)
                {
                    dealStatus = _dealStatus;
                }
                else
                {
                    dealStatus = null;
                }

                var editStatusDeal = default(string);
                
                if (Deal.DealStatus != dealStatus)
                {
                    Deal.DateUpdate = DateTime.Now;

                    if (Deal?.Letter?.IsRead is false)
                    {
                        Deal.Letter.IsRead = true;
                        Deal.Letter.Save();
                    }

                    editStatusDeal = $"изменен с <b>[{Deal.DealStatus}]</b> на <b>[{dealStatus}]</b>";
                }

                Deal.Description = description?.ToString();
                Deal.DealStatus = dealStatus;
                Deal.Task = Task;
                Session.Save(Deal);
                id = Deal.Oid;

                var userStaff = DatabaseConnection.User?.Staff;
                if (userStaff?.Oid != Deal?.Staff?.Oid 
                    && firstStaff?.Oid != Deal?.Staff?.Oid || !string.IsNullOrWhiteSpace(editStatusDeal))
                {
                    await SendMessageTelegram(userStaff?.ToString(), editStatusDeal).ConfigureAwait(false);
                }
                
                return true;
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                return false;
            }
        }

        private async System.Threading.Tasks.Task SendMessageTelegram(string userName, string editStatusDeal)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userName))
                {
                    return;
                }

                var status = Deal.DealStatus?.ToString();
                if (!string.IsNullOrWhiteSpace(status) 
                    && (status.Equals("Снята задача") || status.Equals("Выполнена")))
                {
                    return;
                }

                var currentStaff = Deal?.Staff;
                var client = TelegramBot.GetTelegramBotClient(Session);
                if (currentStaff != null && currentStaff.TelegramUserId != null)
                {
                    currentStaff?.Reload();

                    var text = $"🆕 [{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] {userName} ";

                    if (string.IsNullOrWhiteSpace(editStatusDeal))
                    {
                        text += $"назначил вам следующую сделку:{Environment.NewLine}";
                    }
                    else
                    {
                        text += $"внес изменения в сделку:{Environment.NewLine}";
                    }                    
                        
                    text += $"[OID]: {Deal.Oid}";

                    if (Deal.LetterDate is DateTime date)
                    {
                        text += $"{Environment.NewLine}<u>Дата получения</u>: {date.ToShortDateString()}";
                    }

                    if (!string.IsNullOrWhiteSpace(editStatusDeal))
                    {
                        text += $"{Environment.NewLine}⚡<u>Статус сделки</u>: {editStatusDeal}";
                    }
                    else if (Deal.DealStatus != null)
                    {
                        text += $"{Environment.NewLine}<u>Статус сделки</u>: {Deal.StatusString}";
                    }

                    if (!string.IsNullOrWhiteSpace(Deal.LetterTopic))
                    {
                        text += $"{Environment.NewLine}<u>Тема</u>: {Deal.LetterTopic}";
                    }

                    if (Deal.Customer != null)
                    {
                        text += $"{Environment.NewLine}<u>Клиент</u>: {Deal.Customer}";
                    }

                    if (!string.IsNullOrWhiteSpace(Deal.Description))
                    {
                        text += $"{Environment.NewLine}<u>Описание</u>: {Deal.Description}";
                    }
                    
                    await client.SendTextMessageAsync(currentStaff.TelegramUserId, text, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void btnSaveDeal_Click(object sender, EventArgs e)
        {
            var isSave = await Save();
            if (isSave)
            {
                flagSave = true;
                Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btnLetter_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;
            
            if (e.Button.Kind == ButtonPredefines.Search)
            {
                if (Deal.Letter != null)
                {
                    if (await Save())
                    {
                        var form = new LetterEdit(Deal.Letter);
                        form.btnLayoutVisible.Visible = false;
                        form.ShowDialog();
                    }
                }
            }
        }
        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnStaff_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
        }

        public (Staff staff, DealStatus dealStatus, Customer customer) ValueTuple { get; set; }
        
        private void btnBulkReplacement_Click(object sender, EventArgs e)
        {            
            var staff = default(Staff);
            var dealStatus = default(DealStatus);
            var customer = default(Customer);

            if (btnStaff.EditValue is Staff _staff)
            {
                staff = _staff;
            }
            else
            {
                staff = null;
            }

            if (btnDealStatus.EditValue is DealStatus _dealStatus)
            {
                dealStatus = _dealStatus;
            }
            else
            {
                dealStatus = null;
            }

            if (btnCustomer.EditValue is Customer _customer)
            {
                customer = _customer;
            }
            else
            {
                customer = null;
            }

            ValueTuple = (staff, dealStatus, customer);
        }

        private async void btnTask_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Ellipsis)
            {
                if (await Save() && Task != null)
                {
                    var form = new TaskEdit(Task);                    
                    form.ShowDialog();

                    buttonEdit.EditValue = Task;
                }                
            }
        }

        private async void checkIsUseTask_CheckedChanged(object sender, EventArgs e)
        {
            var checkEdit = sender as CheckEdit;

            if (checkEdit != null && checkEdit.Checked)
            {
                btnTask.Properties.Buttons[0].Visible = true;
                btnTask.Properties.ReadOnly = false;

                if (Task is null)
                {
                    if (await Save())
                    {
                        Task = new Task(Deal) 
                        { 
                            Deadline = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1) 
                        };

                        try
                        {
                            var status = await Deal.Session.FindObjectAsync<TaskStatus>(new BinaryOperator(nameof(TaskStatus.IsDefault), true));

                            if (status != null)
                            {
                                Task.TaskStatus = status;
                            }
                        }
                        catch (Exception ex)
                        {
                            RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                        }
                    }
                }
            }
            else
            {
                btnTask.Properties.Buttons[0].Visible = false;

                if (Task != null)
                {
                    if (Task.Oid <= 0)
                    {
                        Task = null;
                    }
                    else
                    {
                        if (XtraMessageBox.Show("По данной сделке уже есть сформированная задача. Хотите ее удалить?",
                                                "Удаление задачи",
                                                System.Windows.Forms.MessageBoxButtons.OKCancel,
                                                System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                        {
                            Task.Delete();
                            Task = null;
                        }

                    }
                }                
            }
            
            btnTask.EditValue = Task;
        }

        private void btnDealStatus_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<DealStatus>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.DealStatus, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnDealStatus_EditValueChanged(object sender, EventArgs e)
        {
            if (sender is ButtonEdit buttonEdit)
            {
                if (buttonEdit.EditValue is DealStatus dealStatus)
                {
                    if (!string.IsNullOrWhiteSpace(dealStatus.Color))
                    {
                        var color = ColorTranslator.FromHtml(dealStatus.Color);

                        buttonEdit.BackColor = color;
                        layoutControlItemDealStatus.AppearanceItemCaption.BackColor = color;
                    }
                }
                else
                {
                    buttonEdit.BackColor = default;
                    layoutControlItemDealStatus.AppearanceItemCaption.BackColor = default;
                }
            }
        }
    }
}