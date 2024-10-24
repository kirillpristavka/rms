using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using RMS.Core.Model;
using RMS.Core.TG.Core.Models;
using RMS.UI.Control.TG;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Telegram.Bot;
using TelegramBotRMS.Core.Models;

namespace RMS.UI.Forms
{
    public partial class TelegramForm : XtraForm
    {
        Telegram.Bot.Types.User _currentTGUser;
        private bool isUpdateMesage = false;
        private Timer _timer;
        private bool isTimerUse = false;
        private DateTime _currentDateTime = DateTime.Now;
        
        private Session _session;
        private XPCollection<TGMessage> _messages;
        private Dictionary<int, string> _users = new Dictionary<int, string>();
        
        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridView);
        }

        public TelegramForm(Session session)
        {
            InitializeComponent();

            _timer = new Timer()
            {
                Interval = 10000
            };
            _timer.Tick += _timer_Tick;
            
            FunctionalGridSetup();
            _session = session ?? BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
        }

        private async void _timer_Tick(object sender, EventArgs e)
        {
            if (isUpdateMesage)
            {
                return;
            }
            
            isUpdateMesage = true;
            await UpdateDateAsync();
        }
        
        private async void Form_Load(object sender, EventArgs e)
        {
            var user = await _session.GetObjectByKeyAsync<User>(DatabaseConnection.User?.Oid);
            user?.Reload();
            _currentTGUser = await TelegramBot.GetTelegramBotClientUserAsync(BVVGlobal.oXpo.GetSessionThreadSafeDataLayer());

            await LoadTGUsers();

            if (isTimerUse is false)
            {
                _timer.Start();
            }
        }

        private bool isFirstLoadGridMessage = false;
        private void GetMessage(CriteriaOperator criteriaOperator = null)
        {
            _messages = new XPCollection<TGMessage>(_session, criteriaOperator, new SortProperty(nameof(TGMessage.Date), DevExpress.Xpo.DB.SortingDirection.Ascending));
            gridControl.DataSource = _messages;
            gridView.MoveLast();
            
            if (isFirstLoadGridMessage is false)
            {
                if (gridView.Columns[nameof(TGMessage.Oid)] != null)
                {
                    gridView.Columns[nameof(TGMessage.Oid)].Visible = false;
                    gridView.Columns[nameof(TGMessage.Oid)].Width = 18;
                    gridView.Columns[nameof(TGMessage.Oid)].OptionsColumn.FixedWidth = true;
                }

                if (gridView.Columns[nameof(TGMessage.CreateDate)] != null)
                {
                    gridView.Columns[nameof(TGMessage.CreateDate)].Width = 200;
                    gridView.Columns[nameof(TGMessage.CreateDate)].OptionsColumn.FixedWidth = true;
                    gridView.Columns[nameof(TGMessage.CreateDate)].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    //gridView.Columns[nameof(TGMessage.CreateDate)].Visible = false;
                }

                if (gridView.Columns[nameof(TGMessage.UserRecipient)] != null)
                {
                    gridView.Columns[nameof(TGMessage.UserRecipient)].Width = 250;
                    gridView.Columns[nameof(TGMessage.UserRecipient)].OptionsColumn.FixedWidth = true;
                    gridView.Columns[nameof(TGMessage.UserRecipient)].Visible = false;
                }

                if (gridView.Columns[nameof(TGMessage.UserSender)] != null)
                {
                    gridView.Columns[nameof(TGMessage.UserSender)].Width = 250;
                    gridView.Columns[nameof(TGMessage.UserSender)].OptionsColumn.FixedWidth = true;
                    gridView.Columns[nameof(TGMessage.UserSender)].Visible = false;
                }

                if (gridView.Columns[nameof(TGMessage.Text)] != null)
                {
                    var repositoryItemMemoEdit = gridControl.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                    repositoryItemMemoEdit.WordWrap = true;
                    gridView.Columns[nameof(TGMessage.Text)].ColumnEdit = repositoryItemMemoEdit;
                }

                isFirstLoadGridMessage = true;
            }
        }

        private TGUserControl currentTGUserControl;
        private async System.Threading.Tasks.Task LoadTGUsers()
        {
            try
            {
                using (var uof = new UnitOfWork())
                {
                    var users = await new XPQuery<TGUser>(_session)
                        .Where(w => w.Id != _currentTGUser.Id)
                        .ToListAsync();

                    foreach (var currentUser in users)
                    {
                        if (!_users.ContainsKey(currentUser.Oid))
                        {
                            _users.Add(currentUser.Oid, currentUser?.ToString());

                            var control = new TGUserControl(currentUser) { Dock = DockStyle.Top };
                            control.ObjClick += Control_ObjClick;
                            xtraScrollableControl.Controls.Add(control);
                        }
                        else
                        {
                            await UpdateLastMessageTGUserControl(currentUser);
                        }
                    }

                    //if (currentTGUserControl is null)
                    //{
                    //    var useControl = xtraScrollableControl.Controls.Cast<TGUserControl>().LastOrDefault();
                    //    if (useControl != null)
                    //    {
                    //        currentTGUserControl = useControl;
                    //        Control_ObjClick(currentTGUserControl, currentTGUserControl.TGUser);
                    //    }
                    //}

                    currentTGUserControl?.SetUseColor();
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }

            isUpdateMesage = false;
        }

        private async System.Threading.Tasks.Task UpdateLastMessageTGUserControl(TGUser currentTGUser)
        {
            try
            {
                var control = xtraScrollableControl.Controls.Cast<TGUserControl>().FirstOrDefault(w => w.TGUser != null && w.TGUser.Id == currentTGUser.Id);
                if (control != null)
                {
                    await control.GetLastMessage();
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void Control_ObjClick(object sender, TGUser user)
        {
            if (sender is TGUserControl tgUserControl)
            {
                if (currentTGUserControl != null && currentTGUserControl.Equals(tgUserControl))
                {
                    return;
                }
                else
                {
                    currentTGUserControl?.SetDefaultColor();
                    currentTGUserControl = tgUserControl;
                }                

                using (var uof = new UnitOfWork())
                {
                    var tgUser = await new XPQuery<TGUser>(uof).FirstOrDefaultAsync(f => f.Oid == user.Oid);
                    if (tgUser != null)
                    {
                        var groupOperator = new GroupOperator(GroupOperatorType.Or);
                        groupOperator.Operands.Add(new BinaryOperator($"{nameof(TGMessage.TGUserSender)}.{nameof(TGUser.Oid)}", tgUser.Oid));
                        groupOperator.Operands.Add(new BinaryOperator($"{nameof(TGMessage.TGUserRecipient)}.{nameof(TGUser.Oid)}", tgUser.Oid));


                        GetMessage(groupOperator);
                        //_messages.Filter = groupOperator;
                        return;
                    }
                }
            }

            _messages.Filter = null;
        }

        private void gridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (e.MenuType != GridMenuType.Row)
                    {
                        barBtnEdit.Enabled = false;
                        barBtnDel.Enabled = false;
                    }
                    else
                    {
                        barBtnEdit.Enabled = true;
                        barBtnDel.Enabled = true;
                    }

                    popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private async System.Threading.Tasks.Task UpdateDateAsync()
        {
            _messages?.Reload();
            await LoadTGUsers();
        }
        
        private void barBtnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            //var user = DatabaseConnection.User;
            //user?.Reload();

            //if (user.Staff != null)
            //{
            //    var form = new VacationEdit(user.Staff);
            //    form.ShowDialog();
            //    UpdateDate();
            //}
        }

        private void barBtnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (gridView.GetRow(gridView.FocusedRowHandle) is Vacation vacation)
            //{
            //    var form = new VacationEdit(vacation);
            //    form.ShowDialog();
            //    UpdateDate();
            //}
        }

        private void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (gridView.GetRow(gridView.FocusedRowHandle) is Vacation vacation)
            //{
            //    var user = DatabaseConnection.User;
            //    user?.Reload();
                
            //    if (user.flagAdministrator || vacation.Staff == user.Staff)
            //    {
            //        if (vacation.IsConfirm is false)
            //        {
            //            vacation.Delete();
            //            UpdateDate();
            //        }
            //        else
            //        {
            //            XtraMessageBox.Show("Для удаления отпуска необходимо снять его подтверждение.", "Удаление отпуска", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //    }
            //    else
            //    {
            //        XtraMessageBox.Show("Удаление чужих отпусков доступно только администраторам.", "Удаление отпуска", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
        }
        
        private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (gridView.GetRow(e.RowHandle) is TGMessage message)
                {
                    if (e.Column == gridView.Columns[nameof(TGMessage.Text)])
                    {
                        if (message.TGUserSender?.Id == _currentTGUser.Id)
                        {
                            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                            e.Appearance.BackColor = Color.White;
                            e.Appearance.BackColor2 = Color.LightGreen;
                        }
                        else
                        {
                            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                            e.Appearance.BackColor = Color.LightCoral;
                            e.Appearance.BackColor2 = Color.White;
                        }
                    }
                }
            }
        }

        private void gridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            //if (sender is GridView gridView)
            //{
            //    if (gridView.GetRow(e.RowHandle) is TGMessage message)
            //    {
            //        if (message.TGUserSender?.Id == _currentTGUser.Id)
            //        {
            //            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            //        }
            //        else
            //        {
            //            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            //            e.Appearance.BackColor = Color.LightGreen;
            //        }
            //    }
            //}
        }

        private async void barBtnUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {
            await UpdateDateAsync();
        }        

        private async void memoMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.None)
            {                
                if (sender is MemoEdit memo)
                {
                    var text = memo.Text?.Trim();
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        return;
                    }

                    if (currentTGUserControl?.TGUser != null)
                    {
                        using (var uof = new UnitOfWork())
                        {
                            var tgUserRecipient = await new XPQuery<TGUser>(uof).FirstOrDefaultAsync(f => f.Oid == currentTGUserControl.TGUser.Oid);
                            if (tgUserRecipient != null)
                            {
                                var userSender = await TelegramBot.GetTelegramBotClientUserAsync(BVVGlobal.oXpo.GetSessionThreadSafeDataLayer());
                                var tgUserSender = await new XPQuery<TGUser>(uof).FirstOrDefaultAsync(f => f.Id == userSender.Id);

                                await SendMessageTelegram(tgUserRecipient, tgUserSender, text).ConfigureAwait(false);
                                
                                Invoke((Action)async delegate
                                {
                                    await UpdateDateAsync(); 
                                    memo.EditValue = null;
                                    memo.Reset();
                                });
                                
                                e.SuppressKeyPress = true;
                            }
                        }
                    }
                }
            }
        }

        private async System.Threading.Tasks.Task SendMessageTelegram(TGUser tgUserRecipient, TGUser tgUserSender, string message)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(message) || tgUserRecipient is null)
                {
                    return;
                }

                var currentStaff = DatabaseConnection.User?.Staff;
                if (currentStaff is null)
                {
                    return;
                }
                
                var client = TelegramBot.GetTelegramBotClient(BVVGlobal.oXpo.GetSessionThreadSafeDataLayer());
                message = $"{currentStaff}: {message}";

                var currentMessage = await client.SendTextMessageAsync(tgUserRecipient.Id, message, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
                if (currentMessage != null)
                {
                    await GetTGMessageAsync(currentMessage, tgUserRecipient.Id, tgUserSender?.Id);
                    await currentTGUserControl?.GetLastMessage();
                }
                
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        public static async System.Threading.Tasks.Task GetTGMessageAsync(Telegram.Bot.Types.Message message, long? tgUserRecipientId, long? tgUserSenderId)
        {
            if (tgUserRecipientId != null)
            {
                if (message != null)
                {
                    using (var uof = new UnitOfWork())
                    {
                        var tgUserRecipient = await new XPQuery<TGUser>(uof).FirstOrDefaultAsync(f => f.Id == tgUserRecipientId);
                        if (tgUserRecipient != null)
                        {
                            var tgMessage = await new XPQuery<TGMessage>(uof).FirstOrDefaultAsync(f => f.Id == message.MessageId);
                            if (tgMessage is null)
                            {
                                tgMessage = new TGMessage(uof);
                            }

                            if (!string.IsNullOrWhiteSpace(message.Text))
                            {
                                var currentMessage = new TGMessage()
                                {
                                    Id = message.MessageId,
                                    Date = message.Date,
                                    TGUserRecipient = tgUserRecipient, 
                                    TGUserSender = await new XPQuery<TGUser>(uof).FirstOrDefaultAsync(f => f.Id == tgUserSenderId)
                                };
                                currentMessage.SetObj(message.Text);

                                if (await tgMessage.Edit(currentMessage))
                                {
                                    await uof.CommitTransactionAsync().ConfigureAwait(false);
                                }
                            }
                        }
                    }
                }

            }
        }

        //Dictionary<int, RepositoryItemHypertextLabel> dict = new Dictionary<int, RepositoryItemHypertextLabel>();
        //private void gridView_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        //{
        //    if (e.Column.FieldName == nameof(TGMessage.Text))
        //    {
        //        if (sender is GridView gridView)
        //        {
        //            var index = gridView.GetDataSourceRowIndex(e.RowHandle);
        //            if (!dict.ContainsKey(index))
        //            {
        //                if (gridView.GetRow(index) is TGMessage message)
        //                {
        //                    var rep = new RepositoryItemHypertextLabel();
        //                    var img = new ImageCollection();

        //                    foreach (var photo in message.TGMessagePhotos)
        //                    {
        //                        if (photo.TGPhoto?.Obj != null)
        //                        {
        //                            using (var stream = new MemoryStream(photo.TGPhoto.Obj))
        //                            {
        //                                img.AddImage(Image.FromStream(stream));
        //                            }
        //                        }
        //                    }
        //                    rep.HtmlImages = img;
        //                    dict.Add(index, rep);
        //                }
        //            }
        //            e.RepositoryItem = dict[index];
        //        }
        //    }
        //}

        //private void gridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        //{
        //    if (e.IsGetData)
        //    {
        //        TGMessage item = e.Row as TGMessage;
        //        if (item == null) return;
        //        if (item.TGMessagePhotos.Count == 0) return;
        //        StringBuilder sb = new StringBuilder();
        //        for (int i = 0; i < item.TGMessagePhotos.Count; i++)
        //        {
        //            sb.Append($"<image={i}>");
        //            //sb.Append(item.Text);
        //            //if (i != item.TGMessagePhotos.Count - 1)
        //            //    sb.Append("<br>");
        //        }
        //        e.Value = sb.ToString();
        //    }
        //}
    }
}