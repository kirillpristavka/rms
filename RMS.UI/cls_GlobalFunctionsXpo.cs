using DevExpress.Data.Filtering;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using PulsLibrary.Extensions.DevForm;
using RMS.Core.Enumerator;
using RMS.Core.Model;
using RMS.Core.Model.Mail;
using RMS.UI.Forms;
using RMS.UI.Forms.ReferenceBooks;
using RMS.UI.Forms.TemplateForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMS.UI
{
    class cls_GlobalFunctionsXpo
    {
        public cls_GlobalFunctionsXpo() { }

        public async Task<bool> FillSettingsParameters()
        {
            bool fl_return = true;

            try
            {
                BVVGlobal.oApp.User = await GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "User", "Пользователь");
                BVVGlobal.oApp.Password = await GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "Password", Mailbox.Encrypt("1"));
                await GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "oid_default_org", string.Empty, false, false, 1, BVVGlobal.oApp.User);

                
                var user = await DatabaseConnection.WorkSession.FindObjectAsync<User>(new GroupOperator(GroupOperatorType.And,
                    new CriteriaOperator[]
                    {
                            new BinaryOperator(nameof(User.Login), BVVGlobal.oApp.User),
                            //new BinaryOperator(nameof(User.Password), BVVGlobal.oApp.Password)
                    }));

                if (user != null)
                {
                    await DatabaseConnection.RememberWorkUser(user);
                }
                else
                {
                    throw new Exception("Не смог инициализировать Пользователя!");
                }

                UserLookAndFeel.Default.SetSkinStyle(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "tek_skin", "Visual Studio 2013 Light"));

                if (Convert.ToBoolean(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_ProgramSettings_chkUserFont", "false", false, false, 1, BVVGlobal.oApp.User)))
                {
                    var font_name = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_ProgramSettings_fontEditUserFont", "Verdana", false, false, 1, BVVGlobal.oApp.User);
                    var i_size = Convert.ToInt32(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "form_ProgramSettings_spinEditUserFontSize", "8", false, false, 1, BVVGlobal.oApp.User));
                    WindowsFormsSettings.DefaultFont = new Font(font_name, i_size);
                    WindowsFormsSettings.DefaultMenuFont = new Font(font_name, i_size);
                }
            }
            catch (Exception)
            {
                fl_return = false;
            }

            return fl_return;
        }

        public async Task<cls_AppParam> FillAppParam()
        {
            cls_AppParam oAppParam = new cls_AppParam();

            try
            {
                oAppParam.TmpDir = await GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "directory_temporary", BVVGlobal.oApp.BaseDirectory + "\\tmp");
                oAppParam.OutDir = await GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "directory_output", BVVGlobal.oApp.BaseDirectory + "\\out");
                oAppParam.TemplatesDir = await GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "directory_templates", BVVGlobal.oApp.BaseDirectory + "\\templates");

                oAppParam.PathUpdateService = await GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.PathUpdateService), "ftp://81.28.221.114/other/RMS/RMS.Update.xml");
                oAppParam.CountOfDaysToAcceptLetter = await GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.CountOfDaysToAcceptLetter), "1", user: BVVGlobal.oApp.User);
                oAppParam.CountOfLetterToSave = await GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.CountOfLetterToSave), "0", user: BVVGlobal.oApp.User);
                oAppParam.MailboxForSending = await GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.MailboxForSending), "");
                oAppParam.MyFolderPath = await GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.MyFolderPath), "", user: BVVGlobal.oApp.User);

                oAppParam.EnableOrDisableEmailPreview = await GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.EnableOrDisableEmailPreview), "0", user: BVVGlobal.oApp.User);
                oAppParam.EnableOrDisableGetEmails = await GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.EnableOrDisableGetEmails), "0", user: BVVGlobal.oApp.User);
            }
            catch (Exception ex)
            {
                throw new Exception("FillAppParam: " + ex.Message);
            }

            return oAppParam;
        }

        /// <summary>
        /// Получение объекта из настроек.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session">Активная сессия.</param>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Объект XPObject.</returns>
        public static async Task<T> GetParameter<T>(Session session, int id)
        {
            return await session.GetObjectByKeyAsync<T>(id);
        }

        /// <summary>
        /// Получение, изменение значения параметра из таблицы set_LocalSettings
        /// </summary>
        /// <param name="sess">Session</param>
        /// <param name="name">ParameterName</param>
        /// <param name="obj">DefaultValue or ValueForUpdate</param>
        /// <param name="isRecordUpdate">true - update; false - select</param>
        /// <param name="flUpdNewRec">true - если нет параметра при апдейте, то будет инсертится</param>
        /// <param name="workingField">1, 2 или 3 - с каким полем работать Value1, Value2 или Value3</param>
        /// <param name="user">для вариаций отбора (User)</param>
        /// <param name="organization">для вариаций отбора (Org)</param>
        /// <param name="task">для вариаций отбора (Task)</param>
        /// <param name="year">для вариаций отбора (Year)</param>
        /// <returns>Value1 or Value2 or Value3</returns>
        public async Task<string> GetLocalSettingsOptionsAsync(Session sess,
                                                          string name,
                                                          string obj = "",
                                                          bool isRecordUpdate = false,
                                                          bool flUpdNewRec = false,
                                                          int workingField = 1,
                                                          string user = "",
                                                          string organization = "",
                                                          string task = "",
                                                          string year = "")
        {
            string result = string.Empty;
            GroupOperator criteria = new GroupOperator(GroupOperatorType.And, new CriteriaOperator[] 
            { 
                new BinaryOperator(nameof(set_LocalSettings.Name), name), 
                new BinaryOperator(nameof(set_LocalSettings.User), user), 
                new BinaryOperator(nameof(set_LocalSettings.Org), organization), 
                new BinaryOperator(nameof(set_LocalSettings.Task), task), 
                new BinaryOperator(nameof(set_LocalSettings.Year), year) 
            });

            bool isNewRecord = false;

            var localSettings = await GetLocalSettingsObjectAsync(criteria);
            if ((localSettings == null) && (!(isRecordUpdate) || ((isRecordUpdate) && (flUpdNewRec))))
            {
                localSettings = new set_LocalSettings(sess) 
                {
                    Name = name, 
                    g_id = Guid.NewGuid().ToString(), 
                    User = user, 
                    Org = organization, 
                    Task = task, 
                    Year = year 
                };
                isNewRecord = true;
            }

            if (!localSettings.Equals(null))
            {
                if ((isNewRecord) || (isRecordUpdate))
                {
                    switch (workingField)
                    {
                        case 1:
                            localSettings.Value1 = obj;
                            break;
                        case 2:
                            localSettings.Value2 = obj;
                            break;
                        case 3:
                            localSettings.Value3 = obj;
                            break;
                    }
                    localSettings.Save();
                }

                switch (workingField)
                {
                    case 1:
                        result = localSettings.Value1;
                        break;
                    case 2:
                        result = localSettings.Value2;
                        break;
                    case 3:
                        result = localSettings.Value3;
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Получение или изменение значения параметра из таблицы set_LocalSettings.
        /// </summary>
        /// <param name="sess">Активная сессия.</param>
        /// <param name="name">Наименование параметра.</param>
        /// <param name="obj">Объект для записи или обновления.</param>
        /// <param name="isRecordUpdate">Если параметр [true] - UPDATE, если [false] - SELECT</param>
        /// <param name="flUpdNewRec">Если параметра при обновлении [true], то будет записан новый объект.</param>
        /// <param name="user">Отбор по указанному пользователю.</param>
        /// <param name="organization">Отбор по указанной организации.</param>
        /// <param name="task">Отбор по указанной задаче.</param>
        /// <param name="year">Отбор по указанному году.</param>
        /// <returns>Object byte[]</returns>
        public async Task<byte[]> GetObjLocalSettingsOptionsAsync(Session sess,
                                                                  string name,
                                                                  byte[] obj = default,
                                                                  bool isRecordUpdate = false,
                                                                  string user = "",
                                                                  string organization = "",
                                                                  string task = "",
                                                                  string year = "")
        {            
            GroupOperator criteria = new GroupOperator(GroupOperatorType.And, new CriteriaOperator[]
            {
                new BinaryOperator(nameof(set_LocalSettings.Name), name),
                new BinaryOperator(nameof(set_LocalSettings.User), user),
                new BinaryOperator(nameof(set_LocalSettings.Org), organization),
                new BinaryOperator(nameof(set_LocalSettings.Task), task),
                new BinaryOperator(nameof(set_LocalSettings.Year), year)
            });
            
            var localSettings = await GetLocalSettingsObjectAsync(criteria);
            if (localSettings is null)
            {
                localSettings = new set_LocalSettings(sess)
                {
                    Name = name,
                    g_id = Guid.NewGuid().ToString(),
                    User = user,
                    Org = organization,
                    Task = task,
                    Year = year
                };
            }
            
            if (isRecordUpdate)
            {
                localSettings.Obj = obj;
            }
            localSettings.Save();

            return localSettings.Obj;
        }

        private async Task<set_LocalSettings> GetLocalSettingsObjectAsync(CriteriaOperator criteria)
        {
            return await DatabaseConnection.LocalSession.FindObjectAsync<set_LocalSettings>(criteria, false);
        }        

        public void SetClipboardGridView(ref GridView gridView)
        {
            if (gridView != null)
            {
                gridView.KeyDown += SetClipboardGridViewKeyDown;
            }
        }

        private void SetClipboardGridViewKeyDown(object sender, KeyEventArgs e)
        {
            var gridView = sender as GridView;
            if (gridView != null)
            {
                if (e.Control && e.KeyCode == Keys.C)
                {
                    Clipboard.SetText(gridView.GetFocusedDisplayText());
                    e.Handled = true;
                }
            }            
        }

        /// <summary>
        /// Настройка отображения выделенной строки таблицы.
        /// </summary>
        /// <param name="gridView"></param>
        public void SettingsSelectedRowGridView(ref GridView gridView)
        {
            if (gridView != null)
            {
                gridView.RowStyle += GridView_RowStyle;

                gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridView.OptionsSelection.EnableAppearanceFocusedRow = false;
                gridView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus;
            }
        }

        public void SettingsSelectedRowGridView(ref AdvBandedGridView gridView)
        {
            if (gridView != null)
            {
                gridView.RowStyle += GridView_RowStyle;

                gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridView.OptionsSelection.EnableAppearanceFocusedRow = false;
                gridView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus;
            }
        }

        public void ButtoneEditDoubleClick<T>(Session session, ButtonEdit buttonEdit, cls_App.ReferenceBooks referenceBook, CriteriaOperator criteriaOperator = null, bool isEnable = true) 
            where T : IXPObject
        {
            try
            {
                if (isEnable)
                {
                    buttonEdit.DoubleClick += (sender, e) => ButtonEdit_DoubleClick<T>(sender, e, session, referenceBook, criteriaOperator);
                }                
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }            
        }

        private void ButtonEdit_DoubleClick<T>(object sender, EventArgs e, Session session, cls_App.ReferenceBooks referenceBook, CriteriaOperator criteriaOperator = null)
            where T : IXPObject
        {
            cls_BaseSpr.ButtonEditButtonClickBase<T>(session, (ButtonEdit)sender, (int)referenceBook, 1, criteriaOperator, null, false, null, string.Empty, false, true);
        }

        public delegate void FormClosedEventHandler(object sender);
        public event FormClosedEventHandler FormClosed;

        /// <summary>
        /// Открытие формы редактирования по нажатию Enter.
        /// </summary>
        /// <typeparam name="TClass">Класс который передается в форму.</typeparam>
        /// <typeparam name="TForm">Класс формы.</typeparam>
        /// <param name="gridView">Активная таблица.</param>
        public void PressEnterGrid<TClass, TClassDTO, TForm>(GridView gridView,
                                                  bool isdMenuCRUD = false,
                                                  bool isUseAdd = false,
                                                  Action action = null,
                                                  Action actionAdd = null,
                                                  bool isUseMassEdit = false,
                                                  Action actionMassEdit = null,
                                                  bool isUseSystemControl = true,
                                                  bool isUseProgramEvent = false,
                                                  bool isUseDelete = true)
            where TClass : XPObject
            where TForm : XtraForm
        {
            gridView.KeyDown += (sender, e) => GridView_KeyDown<TClass, TClassDTO, TForm>(sender, e, action);
            gridView.DoubleClick += (sender, e) => GridView_DoubleClick<TClass, TClassDTO, TForm>(sender, e, action);
        }
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        private async void GridView_KeyDown<TClass, TClassDTO, TForm>(object sender, KeyEventArgs e, Action action)
            where TClass : XPObject
            where TForm : XtraForm
        {
            var gridview = sender as GridView;
            if (gridview != null && !gridview.IsEmpty)
            {
                switch (e.KeyData)
                {
                    case Keys.Enter:
                        if (gridview.GetRow(gridview.FocusedRowHandle) is TClassDTO obj)
                        {
                            var objOid = GetPropValue(obj, "Oid");
                            if (int.TryParse(objOid?.ToString(), out int result))
                            {
                                var dataClass = await DatabaseConnection.GetWorkSession().GetObjectByKeyAsync<TClass>(result);
                                if (dataClass != null)
                                {
                                    dataClass.Reload();
                                    var form = (TForm)Activator.CreateInstance(typeof(TForm), dataClass);
                                    form.KeyPreview = true;
                                    form.KeyDown += Form_KeyDown;
                                    form.ShowDialog();
                                    FormClosed?.Invoke(form);

                                    if (action != null)
                                    {
                                        try
                                        {
                                            action();
                                            //await System.Threading.Tasks.Task.Run(() => action()).ConfigureAwait(false); 
                                        }
                                        catch (Exception ex)
                                        {
                                            RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case Keys.Home:
                        gridview.MoveFirst();
                        break;

                    case Keys.End:
                        gridview.MoveLast();
                        break;

                    case Keys.F5:
                        try
                        {
                            gridview.ShowLoadingPanel();
                            var data = (XPCollection<TClass>)gridview.DataSource;
                            data.Reload();
                        }
                        catch (Exception ex)
                        {
                            RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                        }
                        finally
                        {
                            gridview.HideLoadingPanel();
                        }
                        break;
                }
            }
        }

        private async void GridView_DoubleClick<TClass, TClassDTO, TForm>(object sender, EventArgs e, Action action)
            where TClass : XPObject
            where TForm : XtraForm
        {
            try
            {
                var dxMouseEventArgs = e as DXMouseEventArgs;
                var gridview = sender as GridView;
                var gridHitInfo = gridview.CalcHitInfo(dxMouseEventArgs.Location);

                if ((!gridHitInfo.InColumn && !gridHitInfo.InColumnPanel) && dxMouseEventArgs.Button == MouseButtons.Left)
                {
                    if (gridview.GetRow(gridview.FocusedRowHandle) is TClassDTO obj)
                    {
                        var objOid = GetPropValue(obj, "Oid");
                        if (int.TryParse(objOid?.ToString(), out int result))
                        {
                            var dataClass = await DatabaseConnection.GetWorkSession().GetObjectByKeyAsync<TClass>(result);
                            if (dataClass != null)
                            {
                                dataClass.Reload();
                                var form = (TForm)Activator.CreateInstance(typeof(TForm), dataClass);
                                form.KeyPreview = true;
                                form.KeyDown += Form_KeyDown;
                                form.ShowDialog();
                                FormClosed?.Invoke(form);

                                if (action != null)
                                {
                                    try
                                    {
                                        action();
                                        //await System.Threading.Tasks.Task.Run(() => action()).ConfigureAwait(false);
                                    }
                                    catch (Exception ex)
                                    {
                                        RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }


        /// <summary>
        /// Открытие формы редактирования по нажатию Enter.
        /// </summary>
        /// <typeparam name="TClass">Класс который передается в форму.</typeparam>
        /// <typeparam name="TForm">Класс формы.</typeparam>
        /// <param name="gridView">Активная таблица.</param>
        public void PressEnterGrid<TClass, TForm>(GridView gridView,
                                                  bool isdMenuCRUD = false,
                                                  bool isUseAdd = false,
                                                  Action action = null,
                                                  Action actionAdd = null,
                                                  bool isUseMassEdit = false,
                                                  Action actionMassEdit = null,
                                                  bool isUseSystemControl = true,
                                                  bool isUseProgramEvent = false,
                                                  bool isUseDelete = true,
                                                  bool isUseShow = false)
            where TClass : XPObject
            where TForm : XtraForm
        {

            gridView.KeyDown += (sender, e) => GridView_KeyDown<TClass, TForm>(sender, e, action);
            if (isUseShow)
            {
                gridView.DoubleClick += (sender, e) => GridViewShow_DoubleClick<TClass, TForm>(sender, e, action);                
            }
            else
            {
                gridView.DoubleClick += (sender, e) => GridView_DoubleClick<TClass, TForm>(sender, e, action);
            }

            if (isdMenuCRUD)
            {
                gridView.MouseDown += (sender, e) => GridView_MouseDown<TClass, TForm>(sender, e, isUseAdd, actionAdd, isUseMassEdit, actionMassEdit, isUseSystemControl, isUseProgramEvent, isUseDelete);
            }
        }

        public void CreateTask<TForm>(Session session, object obj = null, TypeTask typeTask = TypeTask.Task)
            where TForm : XtraForm
        {
            try
            {
                var comment = GetDescriptionOrComment(obj);

                var form = default(TForm);
                if (string.IsNullOrWhiteSpace(comment))
                {
                    form = (TForm)Activator.CreateInstance(typeof(TForm), session, typeTask);
                }
                else
                {
                    form = (TForm)Activator.CreateInstance(typeof(TForm), session, comment, typeTask);
                }

                form.ShowDialog();
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }
        
        private string GetDescriptionOrComment(object obj, string nameProperty = "Description")
        {
            try
            {
                if (obj != null)
                {
                    return obj?.GetType()?.InvokeMember(nameProperty, BindingFlags.GetField | BindingFlags.GetProperty, null, obj, null)?.ToString();
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }

            return default;
        }

        private class MyPopupMenu<TClass, TForm>
            where TClass : XPObject
            where TForm : XtraForm
        {
            private IContainer components = null;

            public PopupMenu PopupMenu { get; }
            private BarManager BarManager { get; }
            private BarDockControl BarDockControlTop { get; }
            private BarDockControl BarDockControlBottom { get; }
            private BarDockControl BarDockControlLeft { get; }
            private BarDockControl BarDockControlRight { get; }
            private BarButtonItem BarBtnAdd { get; }
            private BarButtonItem BarBtnEdit { get; }
            private BarButtonItem BarBtnMassEdit { get; }
            private BarButtonItem BarBtnDelete { get; }
            private BarButtonItem BarBtnSystemControl { get; }
            private BarButtonItem BarBtnProgramEvent { get; }
            public Form Form { get; }
            private GridView GridView { get; }

            public MyPopupMenu(Form form,
                               GridView gridView,
                               bool isUseAdd = false,
                               Action actionAdd = null,
                               bool isUseMassEdit = false,
                               Action actionMassEdit = null,
                               bool isUseSystemControl = true,
                               bool isUseProgramEvent = false,
                               bool isUseDelete = true)
            {
                Form = form ?? throw new ArgumentNullException(nameof(form));
                GridView = gridView ?? throw new ArgumentNullException(nameof(gridView));

                components = new Container();
                PopupMenu = new PopupMenu(components);
                BarManager = new BarManager(components);
                BarDockControlTop = new BarDockControl();
                BarDockControlBottom = new BarDockControl();
                BarDockControlLeft = new BarDockControl();
                BarDockControlRight = new BarDockControl();
                BarBtnAdd = new BarButtonItem();
                BarBtnEdit = new BarButtonItem();
                BarBtnDelete = new BarButtonItem();
                BarBtnMassEdit = new BarButtonItem();
                BarBtnSystemControl = new BarButtonItem();
                BarBtnProgramEvent = new BarButtonItem();

                InitializeComponent(isUseAdd, actionAdd, isUseMassEdit, actionMassEdit, isUseSystemControl, isUseProgramEvent, isUseDelete);
            }

            private void InitializeComponent(bool isUseAdd = false,
                                             Action actionAdd = null,
                                             bool isUseMassEdit = false,
                                             Action actionMassEdit = null,
                                             bool isUseSystemControl = true,
                                             bool isUseProgramEvent = false,
                                             bool isUseDelete = true)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(TemplateForm));

                ((ISupportInitialize)(PopupMenu)).BeginInit();
                ((ISupportInitialize)(BarManager)).BeginInit();
                // 
                // popupMenu
                // 
                PopupMenu.LinksPersistInfo.AddRange(new LinkPersistInfo[] {
                new LinkPersistInfo(BarBtnAdd),
                new LinkPersistInfo(BarBtnEdit),
                new LinkPersistInfo(BarBtnDelete),
                new LinkPersistInfo(BarBtnSystemControl),
                new LinkPersistInfo(BarBtnMassEdit)});
                PopupMenu.Manager = BarManager;
                PopupMenu.Name = "popupMenu";
                // 
                // barManager
                // 
                BarManager.DockControls.Add(BarDockControlTop);
                BarManager.DockControls.Add(BarDockControlBottom);
                BarManager.DockControls.Add(BarDockControlLeft);
                BarManager.DockControls.Add(BarDockControlRight);
                BarManager.Form = Form;
                BarManager.Items.AddRange(new BarItem[] {
                BarBtnAdd,
                BarBtnEdit,
                BarBtnMassEdit,
                BarBtnDelete});
                BarManager.MaxItemId = 3;
                // 
                // barDockControlTop
                // 
                BarDockControlTop.CausesValidation = false;
                BarDockControlTop.Dock = DockStyle.Top;
                BarDockControlTop.Location = new Point(0, 0);
                BarDockControlTop.Manager = BarManager;
                BarDockControlTop.Size = new Size(284, 0);
                // 
                // barDockControlBottom
                // 
                BarDockControlBottom.CausesValidation = false;
                BarDockControlBottom.Dock = DockStyle.Bottom;
                BarDockControlBottom.Location = new Point(0, 260);
                BarDockControlBottom.Manager = BarManager;
                BarDockControlBottom.Size = new Size(284, 0);
                // 
                // barDockControlLeft
                // 
                BarDockControlLeft.CausesValidation = false;
                BarDockControlLeft.Dock = DockStyle.Left;
                BarDockControlLeft.Location = new Point(0, 0);
                BarDockControlLeft.Manager = BarManager;
                BarDockControlLeft.Size = new Size(0, 260);
                // 
                // barDockControlRight
                // 
                BarDockControlRight.CausesValidation = false;
                BarDockControlRight.Dock = DockStyle.Right;
                BarDockControlRight.Location = new Point(284, 0);
                BarDockControlRight.Manager = BarManager;
                BarDockControlRight.Size = new Size(0, 260);
                // 
                // barBtnAdd
                // 
                BarBtnAdd.Caption = "Добавить";
                BarBtnAdd.Id = 0;
                BarBtnAdd.ImageOptions.Image = ((Image)(resources.GetObject("barBtnAdd.ImageOptions.Image")));
                BarBtnAdd.ImageOptions.LargeImage = ((Image)(resources.GetObject("barBtnAdd.ImageOptions.LargeImage")));
                BarBtnAdd.Name = "barBtnAdd";
                BarBtnAdd.ItemClick +=  (sender, e) => BarBtnAdd_ItemClick(sender, e, actionAdd);
                if (isUseAdd is false)
                {
                    BarBtnAdd.Visibility = BarItemVisibility.Never;
                }
                // 
                // barBtnEdit
                // 
                BarBtnEdit.Caption = "Изменить";
                BarBtnEdit.Id = 1;
                BarBtnEdit.ImageOptions.Image = ((Image)(resources.GetObject("barBtnEdit.ImageOptions.Image")));
                BarBtnEdit.ImageOptions.LargeImage = ((Image)(resources.GetObject("barBtnEdit.ImageOptions.LargeImage")));
                BarBtnEdit.Name = "barBtnEdit";
                BarBtnEdit.ItemClick += BarBtnEdit_ItemClick;                
                // 
                // barBtnDelete
                // 
                BarBtnDelete.Caption = "Удалить";
                BarBtnDelete.Id = 2;
                BarBtnDelete.ImageOptions.Image = ((Image)(resources.GetObject("barBtnDelete.ImageOptions.Image")));
                BarBtnDelete.ImageOptions.LargeImage = ((Image)(resources.GetObject("barBtnDelete.ImageOptions.LargeImage")));
                BarBtnDelete.Name = "barBtnDelete";
                BarBtnDelete.ItemClick += BarBtnDelete_ItemClick;
                if (isUseDelete is false)
                {
                    BarBtnDelete.Visibility = BarItemVisibility.Never;
                }
                // 
                // barBtnMassEdit
                // 
                BarBtnMassEdit.Caption = "Массовая замена";
                BarBtnMassEdit.Id = 3;
                BarBtnMassEdit.ImageOptions.Image = ((Image)(resources.GetObject("barBtnMassEdit.ImageOptions.Image")));
                BarBtnMassEdit.ImageOptions.LargeImage = ((Image)(resources.GetObject("barBtnMassEdit.ImageOptions.LargeImage")));
                BarBtnMassEdit.Name = "barBtnMassEdit";
                BarBtnMassEdit.ItemClick += (sender, e) => BarBtnMassEdit_ItemClick(sender, e, actionMassEdit); 
                if (isUseMassEdit is false)
                {
                    BarBtnMassEdit.Visibility = BarItemVisibility.Never;
                }
                // 
                // barBtnSystemControl
                // 
                BarBtnSystemControl.Caption = "Контроль";
                BarBtnSystemControl.Id = 4;
                BarBtnSystemControl.ImageOptions.Image = ((Image)(resources.GetObject("barBtnControlSystem.ImageOptions.Image")));
                BarBtnSystemControl.ImageOptions.LargeImage = ((Image)(resources.GetObject("barBtnControlSystem.ImageOptions.LargeImage")));
                BarBtnSystemControl.Name = "barBtnSystemControl";
                BarBtnSystemControl.ItemClick += (sender, e) => BarBtnSystemControl_ItemClick(sender, e);
                if (isUseSystemControl is false)
                {
                    BarBtnSystemControl.Visibility = BarItemVisibility.Never;
                }
                // 
                // barBtnProgramEvent
                // 
                BarBtnProgramEvent.Caption = "Задача ПО";
                BarBtnProgramEvent.Id = 5;
                BarBtnProgramEvent.ImageOptions.Image = ((Image)(resources.GetObject("barBtnProgramEvent.ImageOptions.Image")));
                BarBtnProgramEvent.ImageOptions.LargeImage = ((Image)(resources.GetObject("barBtnProgramEvent.ImageOptions.LargeImage")));
                BarBtnProgramEvent.Name = "barBtnProgramEvent";
                BarBtnProgramEvent.ItemClick += (sender, e) => BarBtnProgramEvent_ItemClick(sender, e);
                if (isUseProgramEvent is false)
                {
                    BarBtnProgramEvent.Visibility = BarItemVisibility.Never;
                }

                ((ISupportInitialize)(PopupMenu)).EndInit();
                ((ISupportInitialize)(BarManager)).EndInit();
            }

            private void BarBtnProgramEvent_ItemClick(object sender, ItemClickEventArgs e)
            {
                try
                {
                    if (GridView.IsEmpty)
                    {
                        return;
                    }

                    var dataClass = GridView.GetRow(GridView.FocusedRowHandle) as TClass;
                    if (dataClass != null)
                    {
                        dataClass.Reload();
                        var form = (ProgramEventEdit)Activator.CreateInstance(typeof(ProgramEventEdit), dataClass);
                        form.KeyPreview = true;
                        form.KeyDown += Form_KeyDown;
                        form.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }
            }

            private void BarBtnSystemControl_ItemClick(object sender, ItemClickEventArgs e)
            {
                try
                {
                    if (GridView.IsEmpty)
                    {
                        return;
                    }

                    var dataClass = GridView.GetRow(GridView.FocusedRowHandle) as TClass;
                    if (dataClass != null)
                    {
                        dataClass.Reload();
                        var form = (ControlSystemEdit)Activator.CreateInstance(typeof(ControlSystemEdit), dataClass);
                        form.KeyPreview = true;
                        form.KeyDown += Form_KeyDown;
                        form.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }         
            }

            private void BarBtnMassEdit_ItemClick(object sender, ItemClickEventArgs e, Action actionMassEdit = null)
            {
                try
                {
                    if (GridView.IsEmpty)
                    {
                        return;
                    }

                    var msg = default(string);
                    var selectedRows = GridView.GetSelectedRows();
                    var listData = new List<TClass>();

                    foreach (var data in selectedRows)
                    {
                        if (GridView.GetRow(data) is TClass obj)
                        {
                            listData.Add(obj);
                            msg += $"{obj}{Environment.NewLine}";
                        }
                    }

                    if (actionMassEdit is null)
                    {
                        if (listData.Count > 0)
                        {
                            var session = listData.FirstOrDefault().Session ?? BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();

                            if (session != null)
                            {
                                var form = (TForm)Activator.CreateInstance(typeof(TForm), session, listData);
                                form.KeyPreview = true;
                                form.KeyDown += Form_KeyDown;
                                form.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        actionMassEdit();
                    }
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                }
            }

            private void BarBtnDelete_ItemClick(object sender, ItemClickEventArgs e)
            {
                if (GridView.IsEmpty)
                {
                    return;
                }
                
                var msg = default(string);
                var selectedRows = GridView.GetSelectedRows();
                var listData = new List<TClass>();
                
                foreach (var data in selectedRows)
                {
                    if (GridView.GetRow(data) is TClass obj)
                    {
                        listData.Add(obj);
                        msg += $"{obj}{Environment.NewLine}";
                    }
                }

                if (listData.Count > 0)
                {
                    var session = listData.FirstOrDefault().Session ?? BVVGlobal.oXpo.GetSessionThreadSafeDataLayer(); 
                    if (XtraMessageBox.Show($"Вы хотите удалить объекты:{Environment.NewLine}{msg}Продолжить?",
                        "Удаление объекта",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        session.Delete(listData);
                    }
                }
            }

            private void BarBtnEdit_ItemClick(object sender, ItemClickEventArgs e)
            {
                if (GridView.IsEmpty)
                {
                    return;
                }

                var dataClass = GridView.GetRow(GridView.FocusedRowHandle) as TClass;
                if (dataClass != null)
                {
                    dataClass.Reload();
                    var form = (TForm)Activator.CreateInstance(typeof(TForm), dataClass);
                    form.KeyPreview = true;
                    form.KeyDown += Form_KeyDown;
                    form.ShowDialog();
                }
            }

            private void BarBtnAdd_ItemClick(object sender, ItemClickEventArgs e, Action actionAdd)
            {
                if (actionAdd != null)
                {
                    actionAdd();
                }                
            }
        }

        private void GridView_MouseDown<TClass, TForm>(object sender,
                                                       MouseEventArgs e,
                                                       bool isUseAdd = false,
                                                       Action actionAdd = null,
                                                       bool isUseMassEdit = false,
                                                       Action actionMassEdit = null,
                                                       bool isUseSystemControl = true,
                                                       bool isUseProgramEvent = false,
                                                       bool isUseDelete = true)
            where TClass : XPObject
            where TForm : XtraForm
        {
            var dxMouseEventArgs = e as DXMouseEventArgs;
            var gridview = sender as GridView;
            var gridHitInfo = gridview.CalcHitInfo(dxMouseEventArgs.Location);
            //if ((gridHitInfo.InRow || gridHitInfo.InRowCell) && dxMouseEventArgs.Button == MouseButtons.Right)
            if (gridHitInfo.HitTest != GridHitTest.Footer && gridHitInfo.HitTest != GridHitTest.Column)
            {
                if (dxMouseEventArgs.Button == MouseButtons.Right)
                {
                    var parent = gridview.GridControl.FindForm();
                    var MyPopupMenu = new MyPopupMenu<TClass, TForm>(parent, gridview, isUseAdd, actionAdd, isUseMassEdit, actionMassEdit, isUseSystemControl, isUseProgramEvent, isUseDelete);
                    MyPopupMenu.PopupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void GridView_KeyDown<TClass, TForm>(object sender, KeyEventArgs e, Action action)
            where TClass : XPObject
            where TForm : XtraForm
        {
            var gridview = sender as GridView;
            if (gridview != null && !gridview.IsEmpty)
            {
                switch (e.KeyData)
                {
                    case Keys.Enter:
                        var dataClass = gridview.GetRow(gridview.FocusedRowHandle) as TClass;
                        if (dataClass != null)
                        {
                            dataClass.Reload();
                            var form = (TForm)Activator.CreateInstance(typeof(TForm), dataClass);
                            form.KeyPreview = true;
                            form.KeyDown += Form_KeyDown;
                            form.ShowDialog();
                            FormClosed?.Invoke(form);

                            if (action != null)
                            {
                                try
                                {
                                    action();
                                    //await System.Threading.Tasks.Task.Run(() => action()).ConfigureAwait(false); 
                                }
                                catch (Exception ex)
                                {
                                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                                }                               
                            }
                        }
                        break;

                    case Keys.Home:
                        gridview.MoveFirst();
                        break;

                    case Keys.End:
                        gridview.MoveLast();
                        break;

                    case Keys.F5:
                        try
                        {
                            gridview.ShowLoadingPanel();
                            var data = (XPCollection<TClass>)gridview.DataSource;
                            data.Reload();
                        }
                        catch (Exception ex)
                        {
                            RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                        } 
                        finally
                        {
                            gridview.HideLoadingPanel();
                        }                      
                        break;
                }
            }
        }

        private async void GridViewShow_DoubleClick<TClass, TForm>(object sender, EventArgs e, Action action)
            where TClass : XPObject
            where TForm : XtraForm
        {
            try
            {
                var dxMouseEventArgs = e as DXMouseEventArgs;
                var gridview = sender as GridView;
                var gridHitInfo = gridview.CalcHitInfo(dxMouseEventArgs.Location);

                if ((!gridHitInfo.InColumn && !gridHitInfo.InColumnPanel) && dxMouseEventArgs.Button == MouseButtons.Left)
                {
                    var dataClass = gridview.GetRow(gridview.FocusedRowHandle) as TClass;
                    if (dataClass != null)
                    {
                        dataClass.Reload();
                        var form = (TForm)Activator.CreateInstance(typeof(TForm), dataClass);
                        form.KeyPreview = true;
                        form.KeyDown += Form_KeyDown;

                        if (form is XtraForm xtraForm) 
                        {
                            xtraForm.XtraFormShow();
                        }
                        else
                        {
                            form.ShowDialog();
                        }

                        FormClosed?.Invoke(form);

                        if (action != null)
                        {
                            try
                            {
                                action();
                                //await System.Threading.Tasks.Task.Run(() => action()).ConfigureAwait(false);
                            }
                            catch (Exception ex)
                            {
                                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void GridView_DoubleClick<TClass, TForm>(object sender, EventArgs e, Action action)
            where TClass : XPObject
            where TForm : XtraForm
        {
            try
            {
                var dxMouseEventArgs = e as DXMouseEventArgs;
                var gridview = sender as GridView;
                var gridHitInfo = gridview.CalcHitInfo(dxMouseEventArgs.Location);

                if ((!gridHitInfo.InColumn && !gridHitInfo.InColumnPanel) && dxMouseEventArgs.Button == MouseButtons.Left)
                {
                    var dataClass = gridview.GetRow(gridview.FocusedRowHandle) as TClass;
                    if (dataClass != null)
                    {
                        dataClass.Reload();
                        var form = (TForm)Activator.CreateInstance(typeof(TForm), dataClass);
                        form.KeyPreview = true;
                        form.KeyDown += Form_KeyDown;
                        form.ShowDialog();
                        FormClosed?.Invoke(form);

                        if (action != null)
                        {
                            try
                            {
                                action();
                                //await System.Threading.Tasks.Task.Run(() => action()).ConfigureAwait(false);
                            }
                            catch (Exception ex)
                            {
                                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }            
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                var xtraForm = sender as XtraForm;
                if (xtraForm != null)
                {
                    xtraForm.Close();
                }
            }
        }

        private void GridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gridView = sender as GridView;

            if (!gridView.IsRowSelected(e.RowHandle))
            {
                return;
            }

            e.Appearance.FontStyleDelta = FontStyle.Bold;
            e.HighPriority = true;
        }

        public async System.Threading.Tasks.Task RestoreLayoutToStreamAsync(object obj, string parametrName, string nameMethod = "RestoreLayoutFromStream")
        {
            var buffer = await BVVGlobal.oFuncXpo.GetObjLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, parametrName, null, user: BVVGlobal.oApp.User);
            if (buffer != null)
            {
                using (var stream = new MemoryStream(buffer))
                {
                    obj?.GetType()?.GetMethod(nameMethod,
                                                 BindingFlags.Public | BindingFlags.Instance,
                                                 null, new Type[] { typeof(MemoryStream) }, null)?.Invoke(obj, new object[] { stream });
                }
            }
        }

        public async System.Threading.Tasks.Task SaveLayoutToStreamAsync(object obj, string parametrName, string nameMethod = "SaveLayoutToStream")
        {
            try
            {
                using (var stream = new MemoryStream())
                {
                    obj?.GetType()?.GetMethod(nameMethod,
                                              BindingFlags.Public | BindingFlags.Instance,
                                              null, new Type[] { typeof(MemoryStream) }, null)?.Invoke(obj, new object[] { stream });

                    if (obj is GridView gridView && gridView.DataSource is null)
                    {
                        return;
                    }

                    await BVVGlobal.oFuncXpo.GetObjLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, parametrName, stream.GetBuffer(), true, user: BVVGlobal.oApp.User);
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }
    }
}

