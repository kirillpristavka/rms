using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using RMS.Core.Controllers.Letters;
using RMS.Core.Enumerator;
using RMS.Core.Model;
using RMS.Core.Model.Mail;
using RMS.UI.Forms.Vacations;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RMS.UI.Forms.Directories
{
    public partial class StaffEdit : formEdit_BaseSpr
    {
        private Session _session;
        private Staff _staff;

        public StaffEdit()
        {
            InitializeComponent();

            if (_session is null)
            {
                _session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                _staff = new Staff(_session);
            }
        }

        public StaffEdit(int id) : this()
        {
            if (id > 0)
            {
                _session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                _staff = _session.GetObjectByKey<Staff>(id);
            }
        }

        public StaffEdit(Staff staff) : this()
        {
            _session = staff.Session;
            _staff = staff;
        }

        private void StaffEdit_Load(object sender, EventArgs e)
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridView);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Position>(_session, txtPosition, cls_App.ReferenceBooks.Position);
            BVVGlobal.oFuncXpo.PressEnterGrid<Vacation, VacationEdit>(gridView);
            
            txtSurname.Text = _staff.Surname;
            txtName.Text = _staff.Name;
            txtPatronymic.Text = _staff.Patronymic;
            txtDateBirth.EditValue = _staff?.DateBirth;
            txtDateReceipt.EditValue = _staff?.DateReceipt;
            txtDateDismissal.EditValue = _staff?.DateDismissal;
            txtOrderNumber.Text = _staff.OrderNumber;
            txtContractNumber.Text = _staff.ContractNumber;
            txtPhoneNumber.Text = _staff.PhoneNumber;
            txtEmail.EditValue = _staff.Email;
            txtPosition.EditValue = _staff.Position;
            
            memoSignature.EditValue = _staff.Signature;
            
            btnTelegram.EditValue = _staff.TGUser;

            gridControl.DataSource = _staff.Vacations;
            
            if (gridView.Columns[nameof(Vacation.Oid)] != null)
            {
                gridView.Columns[nameof(Vacation.Oid)].Visible = false;
                gridView.Columns[nameof(Vacation.Oid)].Width = 18;
                gridView.Columns[nameof(Vacation.Oid)].OptionsColumn.FixedWidth = true;
            }            
            if (gridView.Columns[nameof(Vacation.StaffName)] != null)
            {
                gridView.Columns[nameof(Vacation.StaffName)].Visible = false;
            }
        }        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _staff.Surname = txtSurname.Text;
            _staff.Name = txtName.Text;
            _staff.Patronymic = txtPatronymic.Text;
            _staff.DateBirth = txtDateBirth.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(txtDateBirth.EditValue);
            _staff.DateReceipt = txtDateReceipt.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(txtDateReceipt.EditValue);
            _staff.DateDismissal = txtDateDismissal.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(txtDateDismissal.EditValue);
            _staff.OrderNumber = txtOrderNumber.Text;
            _staff.ContractNumber = txtContractNumber.Text;
            _staff.PhoneNumber = txtPhoneNumber.Text;
            _staff.Signature = memoSignature.Text;
            
            _staff.Email = txtEmail.Text;
            
            if (txtPosition.EditValue is Position position)
            {
                _staff.Position = position;
            }
            else
            {
                _staff.Position = null;
            }            

            _session.Save(_staff);
            id = _staff.Oid;
            flagSave = true;
            Close();
        }        
        
        private void txtPosition_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (sender is ButtonEdit buttonEdit)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                }
                else if ((e.Button.Kind == ButtonPredefines.Ellipsis))
                {
                    cls_BaseSpr.ButtonEditButtonClickBase<Position>(_session, buttonEdit, (int)cls_App.ReferenceBooks.Position, 1, null, null, false, null, string.Empty, false, true);
                }
            }
        }

        private async void btnTelegramGuid_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (_staff is null || _staff.Oid <= 0)
            {
                return;
            }

            var user = await _session.GetObjectByKeyAsync<User>(DatabaseConnection.User?.Oid);
            var userStaff = await new XPQuery<User>(_session).FirstOrDefaultAsync(f => f.Staff != null && f.Staff.Oid == _staff.Oid);
            
            if (user != null && (user.flagAdministrator || user?.Oid == userStaff?.Oid))
            {
                if (sender is ButtonEdit buttonEdit)
                {
                    if (e.Button.Kind == ButtonPredefines.Delete)
                    {
                        if (XtraMessageBox.Show("Вы действительно хотите удалить привязку сотрудника к Telegram?",
                                                "Удаление привязки Telegram",
                                                MessageBoxButtons.OKCancel,
                                                MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            buttonEdit.EditValue = null;
                            _staff.Guid = null;
                            _staff.TelegramUserId = null;
                            _staff.TGUser = null;
                            _staff.Save();
                        }
                    }
                    else if (e.Button.Kind == ButtonPredefines.Search)
                    {                        
                        var email = txtEmail.Text;

                        var guid = _staff.Guid;
                        if (string.IsNullOrWhiteSpace(guid))
                        {
                            guid = Guid.NewGuid().ToString();
                            _staff.Guid = guid;
                            _staff.Save();
                        }

                        if (string.IsNullOrWhiteSpace(email))
                        {
                            if (XtraMessageBox.Show("У сотрудника не задан email адрес, продолжить ручную регистрацию?",
                                                "Регистрация пользователя",
                                                MessageBoxButtons.OKCancel,
                                                MessageBoxIcon.Question) == DialogResult.OK)
                            {                                
                                Clipboard.SetText(guid);

                                var args = new XtraMessageBoxArgs();
                                args.AllowHtmlText = DefaultBoolean.True;
                                args.Text = $"Для авторизации необходимо перейти по ссылке: " +
                                    $"<href=https://t.me/algrasbot>https://t.me/algrasbot</href>{Environment.NewLine}" +
                                    $"или найти бота <href=https://t.me/algrasbot>@algrasbot</href>{Environment.NewLine}" +
                                    $"После чего необходимо ввести следующий ключ: <color=red>{guid}</color>{Environment.NewLine}{Environment.NewLine}" +
                                    $"Ключ скопирован в буфер обмена, доступна операция [CTRL + V]";
                                args.Caption = "Регистрация пользователя";
                                args.Buttons = new DialogResult[] { DialogResult.OK };
                                args.Icon = SystemIcons.Information;
                                args.HyperlinkClick += (s, e) => { System.Diagnostics.Process.Start(e.Link); };
                                XtraMessageBox.Show(args);
                            }
                        }
                        else
                        {
                            if (XtraMessageBox.Show("Отправить письмо с авторизацией сотруднику?",
                                                "Регистрация пользователя",
                                                MessageBoxButtons.OKCancel,
                                                MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                await LettersController.CreateAuthorizationLetterAsync(guid,
                                    BVVGlobal.oApp.User, 
                                    email,
                                    await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.MailboxForSending), ""));
                            }
                        }
                    }
                }
            }
        }

        private void gridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
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

                        var user = DatabaseConnection.User;
                        if (user != null && user.flagAdministrator)
                        {
                            if (gridView.GetRow(gridView.FocusedRowHandle) is Vacation vacation)
                            {
                                if (vacation.IsConfirm is false)
                                {
                                    barBntConfirm.Caption = "Утвердить";
                                }
                                else
                                {
                                    barBntConfirm.Caption = "Снять подтверждение";
                                }
                            }

                            barBntConfirm.Enabled = true;
                        }
                        else
                        {
                            barBntConfirm.Enabled = false;
                        }
                    }

                    popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void barBtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var form = new VacationEdit(_staff);
            form.ShowDialog();

            _staff.Vacations?.Reload();
        }

        private void barBtnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is Vacation vacation)
            {
                var form = new VacationEdit(vacation);
                form.ShowDialog();

                _staff.Vacations?.Reload();
            }
        }

        private void barBtnDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is Vacation vacation)
            {
                if (vacation.IsConfirm is false)
                {
                    vacation.Delete();
                }
            }
        }

        private void barBntConfirm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is Vacation vacation)
            {
                if (vacation.IsConfirm is false)
                {
                    vacation.IsConfirm = true;
                }
                else
                {
                    vacation.IsConfirm = false;
                }

                vacation.Save();
            }
        }        
    }
}