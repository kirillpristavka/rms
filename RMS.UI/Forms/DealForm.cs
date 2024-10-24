using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using RMS.Setting.Model.ColorSettings;
using RMS.Setting.Model.GeneralSettings;
using RMS.UI.Control;
using RMS.UI.Control.Customers;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms
{
    public partial class DealForm : XtraForm
    {
        private Deal _currentDeal;
        private Session _session;
        private XPCollection<Deal> _deals;

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridView);
            //TODO: здесь происходит задержка при закрытии формы. Как исправить?
            //BVVGlobal.oFuncXpo.PressEnterGrid<Deal, DealEdit>(gridView, action: async () => await TreeListNodeUpdate().ConfigureAwait(false));
            BVVGlobal.oFuncXpo.PressEnterGrid<Deal, DealEdit>(gridView);
        }

        private static class StatusDealColor
        {
            public static Color ColorStatusDealNew;
            public static Color ColorStatusDealPostponed;
            public static Color ColorStatusDealCompleted;
            public static Color ColorStatusDealPrimary;
            public static Color ColorStatusDealAdministrator;
        }

        private async System.Threading.Tasks.Task GetStatusDealColor()
        {
            var colorStatus = await _session.FindObjectAsync<ColorStatus>(new BinaryOperator(nameof(ColorStatus.IsDefault), true));
            if (colorStatus != null)
            {
                StatusDealColor.ColorStatusDealNew = ColorTranslator.FromHtml(colorStatus.StatusDealNew);
                StatusDealColor.ColorStatusDealPostponed = ColorTranslator.FromHtml(colorStatus.StatusDealPostponed);
                StatusDealColor.ColorStatusDealCompleted = ColorTranslator.FromHtml(colorStatus.StatusDealCompleted);
                StatusDealColor.ColorStatusDealPrimary = ColorTranslator.FromHtml(colorStatus.StatusDealPrimary);
                StatusDealColor.ColorStatusDealAdministrator = ColorTranslator.FromHtml(colorStatus.StatusDealAdministrator);
            }
            else
            {
                StatusDealColor.ColorStatusDealNew = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusDealNew", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(152, 251, 152))));
                StatusDealColor.ColorStatusDealPostponed = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusDealPostponed", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(255, 250, 205))));
                StatusDealColor.ColorStatusDealCompleted = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusDealCompleted", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(230, 230, 250))));
                StatusDealColor.ColorStatusDealPrimary = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusDealPrimary", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(250, 128, 114))));
                StatusDealColor.ColorStatusDealAdministrator = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusDealAdministrator", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(135, 206, 235))));
            }
        }

        public DealForm(Session session)
        {
            InitializeComponent();
            FunctionalGridSetup();

            _session = session ?? DatabaseConnection.GetWorkSession();

            //var xpcollection = new XPCollection<DealStatus>(Session);
            //cmbStatusDeal.Properties.Items.AddRange(xpcollection);

            //var user = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid);
            //if (user?.Staff != null)
            //{
            //    btnStaff.EditValue = user.Staff;
            //}
        }

        private bool isEditDealForm = false;
        private bool isDeleteDealForm = false;

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
                            isDeleteDealForm = accessRights.IsDeleteDealForm;
                        }
                    }
                }

                barBtnRefreshFromLetter.Enabled = isEditDealForm;
                barBtnBulkReplacement.Enabled = isEditDealForm;

                barBtnDel.Enabled = isDeleteDealForm;
                barBtnRemovingEmptyTrades.Enabled = isDeleteDealForm;
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void DealForm_Load(object sender, EventArgs e)
        {
            await SetAccessRights();

            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Customer>(_session, btnCustomer, cls_App.ReferenceBooks.Customer);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Staff>(_session, btnStaff, cls_App.ReferenceBooks.Staff);

            await GetStatusDealColor();

            _deals = new XPCollection<Deal>(_session);
            _deals.Sorting = new SortingCollection(
                new SortProperty($"{nameof(Deal.Letter)}.{nameof(Letter.DateReceiving)}", SortingDirection.Descending),
                new SortProperty($"{nameof(Deal.DateUpdate)}", SortingDirection.Descending));
            _deals.Criteria = await cls_BaseSpr.GetCustomerCriteria(null, nameof(Deal.Customer));
            gridControl.DataSource = _deals;

            if (gridView.Columns[nameof(Deal.Oid)] != null)
            {
                gridView.Columns[nameof(Deal.Oid)].Visible = false;
                gridView.Columns[nameof(Deal.Oid)].Width = 20;
                gridView.Columns[nameof(Deal.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(Deal.DealStatusOidString)] != null)
            {
                RepositoryItemImageComboBox imgCmbStatus = gridControl.RepositoryItems.Add(nameof(ImageComboBoxEdit)) as RepositoryItemImageComboBox;

                var imageCollectionStatus = new ImageCollectionStatus();
                imgCmbStatus.SmallImages = imageCollectionStatus.imageCollection;

                var statusCollection = new XPCollection<DealStatus>(_session);
                foreach (var item in statusCollection)
                {
                    if (item.IndexIcon != null)
                    {
                        imgCmbStatus.Items.Add(new ImageComboBoxItem()
                        {
                            Value = item.Oid,
                            ImageIndex = Convert.ToInt32(item.IndexIcon)
                        });
                    }
                }

                imgCmbStatus.GlyphAlignment = HorzAlignment.Center;
                gridView.Columns[nameof(Deal.DealStatusOidString)].ColumnEdit = imgCmbStatus;
                gridView.Columns[nameof(Deal.DealStatusOidString)].Width = 18;
                gridView.Columns[nameof(Deal.DealStatusOidString)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(Deal.CustomerString)] is GridColumn columnCustomerString)
            {
                columnCustomerString.Width = 125;
                columnCustomerString.OptionsColumn.FixedWidth = true;
                columnCustomerString.Summary.Add(DevExpress.Data.SummaryItemType.Count, columnCustomerString.Name, "{0}");
            }

            if (gridView.Columns[nameof(Deal.StatusString)] != null)
            {
                gridView.Columns[nameof(Deal.StatusString)].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridView.Columns[nameof(Deal.StatusString)].Width = 125;
                gridView.Columns[nameof(Deal.StatusString)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(Deal.StaffString)] != null)
            {
                gridView.Columns[nameof(Deal.StaffString)].Width = 125;
                gridView.Columns[nameof(Deal.StaffString)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(Deal.DateUpdate)] != null)
            {
                gridView.Columns[nameof(Deal.DateUpdate)].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridView.Columns[nameof(Deal.DateUpdate)].Width = 125;
                gridView.Columns[nameof(Deal.DateUpdate)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(Deal.LetterDate)] != null)
            {
                gridView.Columns[nameof(Deal.LetterDate)].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridView.Columns[nameof(Deal.LetterDate)].Width = 100;
                gridView.Columns[nameof(Deal.LetterDate)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(Deal.LetterSenderAddress)] != null)
            {
                gridView.Columns[nameof(Deal.LetterSenderAddress)].Width = 125;
                gridView.Columns[nameof(Deal.LetterSenderAddress)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(Deal.Description)] != null)
            {
                gridView.Columns[nameof(Deal.Description)].Width = 125;
                gridView.Columns[nameof(Deal.Description)].OptionsColumn.FixedWidth = true;
            }

            gridView.BestFitColumns();

            var focusedNode = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"{this.Name}_{nameof(treeListCustomerFilter)}_{nameof(treeListCustomerFilter.FocusedNode)}", null, user: BVVGlobal.oApp.User);
            await TreeListUpdate(focusedNode);

            await LoadSettingsForm();
        }

        private async System.Threading.Tasks.Task LoadSettingsForm()
        {
            try
            {
                await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(layoutControlDeal, $"{this.Name}_{nameof(layoutControlDeal)}");
                await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(gridView, $"{this.Name}_{nameof(gridView)}");
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async System.Threading.Tasks.Task SaveSettingsForms()
        {
            try
            {
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(layoutControlDeal, $"{this.Name}_{nameof(layoutControlDeal)}");
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(gridView, $"{this.Name}_{nameof(gridView)}");
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void btnStaffAdd_Click(object sender, EventArgs e)
        {
            var form = new DealEdit();
            form.ShowDialog();

            var xpcollection = new XPCollection<Deal>(_session);
            gridControl.DataSource = xpcollection;
        }

        private void btnStaffEdit_Click(object sender, EventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var deal = gridView.GetRow(gridView.FocusedRowHandle) as Deal;
            if (deal != null)
            {
                var form = new DealEdit(deal);
                form.ShowDialog();
            }
        }

        private void btnStaffDel_Click(object sender, EventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var deal = gridView.GetRow(gridView.FocusedRowHandle) as Deal;
            deal.Delete();
        }

        private void gridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var gridView = sender as GridView;
            if (gridView is null || gridView.IsEmpty)
            {
                _currentDeal = null;
                return;
            }

            _currentDeal = gridView.GetRow(gridView.FocusedRowHandle) as Deal;
            if (_currentDeal != null)
            {
                _currentDeal.Reload();
            }
        }

        private void barBtnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            _deals?.Reload();
        }

        private async void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var list = new List<Deal>();

            foreach (var focusedRowHandle in gridView.GetSelectedRows())
            {
                var letter = gridView.GetRow(focusedRowHandle) as Deal;

                if (letter != null)
                {
                    list.Add(letter);
                }
            }

            if (XtraMessageBox.Show($"Будет удалено сделок: {list.Count()}{Environment.NewLine}Хотите продолжить?",
                    "Удаление сделок",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {

                foreach (var deal in list)
                {
                    try
                    {
                        if (deal.Letter != null)
                        {
                            deal.Letter.Deal = null;
                            deal.Letter.Save();
                        }
                        deal.Delete();
                    }
                    catch (Exception ex)
                    {
                        RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                    }
                }

                try
                {
                    await TreeListNodeUpdate().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                }
            }
        }

        private async void Filter(object sender, EventArgs e)
        {
            if (_deals != null)
            {
                _deals.Filter = await GetFilter();
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

            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(_session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnStaff_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(_session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
        }

        private void cmbStatusDeal_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }
        }

        private void gridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (gridView.GetRow(e.RowHandle) is Deal obj)
                {
                    if (obj.DealStatus != null)
                    {
                        var color = obj.DealStatus?.Color;
                        if (!string.IsNullOrWhiteSpace(color))
                        {
                            e.Appearance.BackColor = ColorTranslator.FromHtml(color);
                        }
                    }
                }
            }
        }

        private async void btnSettings_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new formProgramSettings(2);
            form.ShowDialog();

            await GetStatusDealColor();
        }

        private async void barBtnBulkReplacement_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var list = new List<Deal>();

            foreach (var focusedRowHandle in gridView.GetSelectedRows())
            {
                var letter = gridView.GetRow(focusedRowHandle) as Deal;

                if (letter != null)
                {
                    list.Add(letter);
                }
            }

            var form = new DealEdit(_session, true);
            var msg = $"Будет изменено сделок: {list.Count()}{Environment.NewLine}";

            if (form.ShowDialog() == DialogResult.OK)
            {
                var isEdit = false;

                var tuple = form.ValueTuple;

                var staff = default(Staff);
                var customer = default(Customer);
                var dealStatus = default(DealStatus);

                if (tuple.dealStatus != null)
                {
                    dealStatus = await _session.GetObjectByKeyAsync<DealStatus>(tuple.dealStatus.Oid);
                    msg += $"Статус сделки поменяет значение на [{dealStatus?.ToString()}]{Environment.NewLine}";
                    isEdit = true;
                }
                else
                {
                    if (form.checkIsNullArgs.Checked)
                    {
                        dealStatus = null;
                        msg += $"Произойдет удаление статуса сделки{Environment.NewLine}";
                        isEdit = true;
                    }
                }

                if (tuple.customer != null)
                {
                    customer = await _session.GetObjectByKeyAsync<Customer>(tuple.customer.Oid);
                    msg += $"Клиент поменяет значение на [{customer}]{Environment.NewLine}";
                    isEdit = true;
                }
                else
                {
                    if (form.checkIsNullArgs.Checked)
                    {
                        customer = null;
                        msg += $"Произойдет удаление клиента из сделки{Environment.NewLine}";
                        isEdit = true;
                    }
                }

                if (tuple.staff != null)
                {
                    staff = await _session.GetObjectByKeyAsync<Staff>(tuple.staff.Oid);
                    msg += $"Менеджер поменяет значение на [{staff}]{Environment.NewLine}";
                    isEdit = true;
                }
                else
                {
                    if (form.checkIsNullArgs.Checked)
                    {
                        staff = null;
                        msg += $"Произойдет удаление ответственного из сделки из сделки{Environment.NewLine}";
                        isEdit = true;
                    }
                }

                if (isEdit is false)
                {
                    XtraMessageBox.Show("Для массовой замены не задан ни один параметр", "Не найдены параметры для замены", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (XtraMessageBox.Show($"{msg}Хотите продолжить?",
                    "Массовое редактирование сделок",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
                {
                    foreach (var deal in list)
                    {
                        try
                        {
                            if (form.checkIsNullArgs.Checked)
                            {
                                deal.DealStatus = dealStatus;
                                deal.Staff = staff;
                                deal.Customer = customer;
                            }
                            else
                            {
                                if (dealStatus != null)
                                {
                                    deal.DealStatus = dealStatus;
                                }

                                if (staff != null)
                                {
                                    deal.Staff = staff;
                                }

                                if (customer != null)
                                {
                                    deal.Customer = customer;
                                }
                            }

                            deal.DateUpdate = DateTime.Now;
                            deal.Save();

                            try
                            {
                                var letter = deal.Letter;
                                if (letter != null && letter.IsRead is false)
                                {
                                    letter.IsRead = true;
                                    letter.Save();
                                }
                            }
                            catch (Exception ex)
                            {
                                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                        }
                    }
                }

                await TreeListNodeUpdate().ConfigureAwait(false);
            }
        }

        private async System.Threading.Tasks.Task RefreshFromLetter()
        {
            if (XtraMessageBox.Show($"При подтверждении будут обновлены все клиенты указанные в сделках.{Environment.NewLine}" +
                $"Если в сделке указан клиент, а в письме нет, то обновится клиент в письме.{Environment.NewLine}" +
                $"Если в письме указан клиент, а в сделке нет, то обновится клиент в сделке.{Environment.NewLine}" +
                $"Если клиент указан в обоих местах, то главным считает тот, который указан в сделках.",
                    "Массовое редактирование сделок",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {
                var countUseDeals = 0;
                var countRefreshDeal = 0;
                var countRefreshLetter = 0;
                var countRefreshStaff = 0;

                using (var uof = new UnitOfWork())
                {
                    using (var deals = new XPCollection<Deal>(uof))
                    {
                        var countDeals = deals.Count;
                        await ProgressRefreshFromLetter(countDeals, 0, "Обновление списка сделок");

                        foreach (var deal in deals)
                        {
                            countUseDeals++;
                            if (deal.Customer is null)
                            {
                                if (deal.Letter != null && deal.Letter.Customer is null)
                                {
                                    var customer = await Letter.GetCustomerAsync(uof, deal.Letter.LetterSenderAddress);
                                    if (customer != null)
                                    {
                                        deal.Letter.Customer = customer;
                                        deal.Customer = customer;
                                        deal.Letter.Save();
                                        deal.DateUpdate = DateTime.Now;

                                        countRefreshDeal++;
                                        countRefreshLetter++;
                                    }
                                }
                                else if (deal.Letter != null && deal.Letter.Customer != null)
                                {
                                    deal.Customer = deal.Letter.Customer;
                                    deal.DateUpdate = DateTime.Now;
                                    countRefreshDeal++;
                                }

                                deal.Save();
                            }
                            else
                            {
                                if (deal.Letter != null && deal.Letter.Customer is null)
                                {
                                    deal.Letter.Customer = deal.Customer;
                                    deal.Letter.Save();
                                    countRefreshLetter++;
                                }
                            }

                            if (deal.Staff is null && deal.Customer != null && deal.Customer.AccountantResponsible != null)
                            {
                                deal.Staff = deal.Customer.AccountantResponsible;
                                deal.Save();
                                countRefreshStaff++;
                            }

                            await ProgressRefreshFromLetter(countDeals, countUseDeals, "Обновление списка сделок");
                        }

                        var user = DatabaseConnection.User;
                        if (user != null)
                        {
                            var chronicleEvents = new ChronicleEvents(uof)
                            {
                                Act = Act.UPDATING_DEALS_FROM_LETTERS,
                                Date = DateTime.Now,
                                Name = Act.UPDATING_DEALS_FROM_LETTERS.GetEnumDescription(),
                                Description = $"Пользователь [{user}] произвел обновление сделок из данных писем.{Environment.NewLine}" +
                                    $"Обновлено сделок: {countRefreshDeal}{Environment.NewLine}" +
                                    $"Обновлено писем: {countRefreshLetter}{Environment.NewLine}" +
                                    $"Обновлено менеджеров: {countRefreshStaff}",
                                User = await uof.GetObjectByKeyAsync<User>(user.Oid)
                            };
                            chronicleEvents.Save();
                        }
                        await uof.CommitChangesAsync();
                    }
                }

                _deals?.Reload();

                XtraMessageBox.Show($"Обновлено сделок: {countRefreshDeal}{Environment.NewLine}Обновлено писем: {countRefreshLetter}{Environment.NewLine}Обновлено менеджеров: {countRefreshStaff}",
                    "Обновление окончено",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private async System.Threading.Tasks.Task ProgressRefreshFromLetter(int count, int number, string caption)
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    var form = Program.MainForm;

                    if (form != null && form.IsDisposed is false)
                    {
                        form.Progress_Start(0, count, caption);
                        form.Progress_Position(number);

                        if (count == number)
                        {
                            form.Progress_Stop();
                        }
                    }
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                    Program.MainForm?.Progress_Stop();
                }
            });
        }

        private async void barBtnRefreshFromLetter_ItemClick(object sender, ItemClickEventArgs e)
        {
            await RefreshFromLetter().ConfigureAwait(false);
        }

        private async void btnDirectoryCustomerFilter_Click(object sender, EventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.CustomerFilter, -1);
            await TreeListUpdate();
        }

        private async System.Threading.Tasks.Task SetValueNode(CriteriaOperator criteria, TreeListNode node)
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                using (var uof = new UnitOfWork())
                {
                    using (var collection = new XPCollection<Deal>(uof, criteria))
                    {
                        Invoke((Action)delegate
                        {
                            node.SetValue(1, collection.Count);
                        });
                    }
                }
            }).ConfigureAwait(false);
        }

        private async System.Threading.Tasks.Task TreeListNodeUpdate()
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                var treeList = treeListCustomerFilter;
                System.Threading.Tasks.Parallel.ForEach(treeList.Nodes, async node =>
                {
                    try
                    {
                        if (node.GetValue(0) is CustomerFilter customerFilter)
                        {
                            var criteria = customerFilter.GetGroupOperatorDeal();
                            if (criteria is null)
                            {
                                if (customerFilter.Name.Equals("*Все"))
                                {

                                }
                                else if (customerFilter.Name.Equals("Мои сделки"))
                                {
                                    var groupOperatorMyDeal = new GroupOperator(GroupOperatorType.Or);

                                    var user = await _session.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid);
                                    if (user.Staff != null)
                                    {
                                        var criteriaDealStaff = new BinaryOperator($"{nameof(Deal.Staff)}.{nameof(Staff.Oid)}", user.Staff?.Oid);
                                        groupOperatorMyDeal.Operands.Add(criteriaDealStaff);
                                    }

                                    if (groupOperatorMyDeal.Operands.Count > 0)
                                    {
                                        criteria = new GroupOperator();
                                        criteria.Operands.Add(groupOperatorMyDeal);
                                    }
                                }
                            }

                            await SetValueNode(criteria, node).ConfigureAwait(false);
                        }
                    }
                    catch (Exception ex)
                    {
                        RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                    }
                });
            }).ConfigureAwait(false);
        }

        private async System.Threading.Tasks.Task TreeListUpdate(object obj = null)
        {
            var customerFilters = await new XPQuery<CustomerFilter>(_session).ToListAsync();
            var sessionUser = await _session.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid);

            treeListCustomerFilter.Columns.Clear();
            treeListCustomerFilter.Columns.Add();
            treeListCustomerFilter.Columns.Add();
            //treeListCustomerFilter.Columns.Add();

            treeListCustomerFilter.Columns[0].Visible = true;
            treeListCustomerFilter.Columns[0].Width = 100;

            //treeListCustomerFilter.Columns[1].Caption = "В работе";
            //treeListCustomerFilter.Columns[1].Visible = true;
            //treeListCustomerFilter.Columns[1].Width = 75;
            //treeListCustomerFilter.Columns[1].OptionsColumn.FixedWidth = true;

            treeListCustomerFilter.Columns[1].Caption = "Всего";
            treeListCustomerFilter.Columns[1].Visible = true;
            treeListCustomerFilter.Columns[1].Width = 75;
            treeListCustomerFilter.Columns[1].OptionsColumn.FixedWidth = true;

            treeListCustomerFilter.OptionsView.AutoWidth = true;
            treeListCustomerFilter.ClearNodes();

            var count = default(int);
            //var countNotFulfilled = default(int);

            var settings = await _session.FindObjectAsync<Settings>(null);
            if (sessionUser?.UserGroups?.Select(s => s.UserGroup)?.FirstOrDefault(f => f.Oid == settings?.UserGroupEverything?.Oid) != null)
            {
                count = _deals.Count();
                //countNotFulfilled = _deals.Where(w => w.StatusString != "Выполнена").Count();
                treeListCustomerFilter.AppendNode(new object[] { new CustomerFilter() { Name = "*Все" }, count }, -1, -1, -1, -1);
            }

            var groupOperatorMyCustomer = new GroupOperator(GroupOperatorType.Or);
            groupOperatorMyCustomer = await cls_BaseSpr.GetStaffCriteria(_session, groupOperatorMyCustomer, nameof(Deal.Staff));
            _deals.Filter = groupOperatorMyCustomer;
            count = _deals.Count();
            //countNotFulfilled = _deals.Where(w => w.StatusString != "Выполнена").Count();
            //var user = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid);
            //if (user.Staff != null)
            //{
            //    var criteriaDealStaff = new BinaryOperator(nameof(Deal.Staff), user.Staff);
            //    groupOperatorMyCustomer.Operands.Add(criteriaDealStaff);

            //    _deals.Filter = groupOperatorMyCustomer;
            //    count = _deals.Count();
            //}            
            treeListCustomerFilter.AppendNode(new object[] { new CustomerFilter() { Name = "Мои сделки" }, count }, -1, -1, -1, -1);

            foreach (var customerFilter in customerFilters.OrderBy(o => o.Number))
            {
                if (customerFilter.IsVisibleDeal)
                {
                    customerFilter.Users.Reload();
                    customerFilter.UserGroups.Reload();

                    if (customerFilter.Users.FirstOrDefault(f => f.User.Oid == sessionUser.Oid) != null ||
                        customerFilter.UserGroups.FirstOrDefault(f => sessionUser.UserGroups.FirstOrDefault(sf => sf.UserGroup.Oid == f.UserGroup.Oid) != null) != null)
                    {
                        if (customerFilter.GetGroupOperatorDeal()?.Operands.Count > 0)
                        {
                            _deals.Filter = customerFilter.GetGroupOperatorDeal();
                        }

                        //countNotFulfilled = _deals.Where(w => w.StatusString != "Выполнена").Count();
                        count = _deals.Count();
                        treeListCustomerFilter.AppendNode(new object[] { customerFilter, count }, -1, -1, -1, -1);
                        _deals.Filter = null;
                    }
                }
                _deals.Filter = null;
            }
            _deals.Filter = null;

            try
            {
                if (obj != null)
                {
                    var node = default(TreeListNode);

                    if (int.TryParse(obj?.ToString(), out int oid))
                    {
                        node = treeListCustomerFilter.Nodes?.FirstOrDefault(f => f.RootNode?.GetValue(0) is CustomerFilter customerFilter && customerFilter.Oid == oid);
                    }
                    else if (obj is string name && !string.IsNullOrWhiteSpace(name))
                    {
                        node = treeListCustomerFilter.Nodes?.FirstOrDefault(f => f.RootNode?.GetValue(0) is string objName
                        && objName == name
                        || f.RootNode?.GetValue(0) is CustomerFilter customerFilter
                        && customerFilter.Name == name);
                    }

                    if (node != null)
                    {
                        treeListCustomerFilter.SetFocusedNode(node);
                    }
                }
                else
                {
                    _deals.Filter = await GetFilter();
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void btnDirectoryAdd_Click(object sender, EventArgs e)
        {
            var form = new CustomersFilterForm(_session, WorkZone.ModuleDeal);
            form.ShowDialog();

            if (form.FlagSave)
            {
                await TreeListUpdate(form?.CustomerFilter?.Oid);
            }
        }

        private async void treeListCustomerFilter_DoubleClick(object sender, EventArgs e)
        {
            var treList = sender as TreeList;

            if (treList != null && treList.FocusedNode?.GetValue(0) is CustomerFilter customerFilter)
            {
                if (customerFilter.Oid > 0)
                {
                    var form = new CustomersFilterForm(customerFilter, WorkZone.ModuleDeal);
                    form.ShowDialog();

                    if (form.FlagSave)
                    {
                        await TreeListUpdate(form?.CustomerFilter?.Oid);
                    }
                }
            }
        }

        private async System.Threading.Tasks.Task<GroupOperator> GetFilter()
        {
            var groupOperator = new GroupOperator(GroupOperatorType.And);

            if (!isDisplayCompleted)
            {
                var dealStatus = await _session.FindObjectAsync<DealStatus>(new BinaryOperator(nameof(DealStatus.Name), "Выполнена"));
                if (dealStatus != null)
                {
                    var criteriaDealStatus = new NotOperator(new BinaryOperator($"{nameof(Deal.DealStatus)}.{nameof(DealStatus.Oid)}", dealStatus.Oid));
                    groupOperator.Operands.Add(criteriaDealStatus);
                }
            }

            if (treeListCustomerFilter != null && treeListCustomerFilter.FocusedNode?.GetValue(0) is CustomerFilter customerFilter)
            {
                if (customerFilter.Name.Equals("*Все"))
                {

                }
                else if (customerFilter.Name.Equals("Мои сделки"))
                {
                    var groupOperatorMyDeal = new GroupOperator(GroupOperatorType.Or);

                    groupOperatorMyDeal = await cls_BaseSpr.GetStaffCriteria(_session, groupOperatorMyDeal, nameof(Deal.Staff));
                    if (groupOperatorMyDeal.Operands.Count > 0)
                    {
                        groupOperator.Operands.Add(groupOperatorMyDeal);
                    }

                    //var user = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid);
                    //if (user.Staff != null)
                    //{
                    //    var criteriaDealStaff = new BinaryOperator(nameof(Deal.Staff), user.Staff);
                    //    groupOperatorMyDeal.Operands.Add(criteriaDealStaff);
                    //}

                    //groupOperator.Operands.Add(groupOperatorMyDeal);
                }
                else
                {
                    if (customerFilter.GetGroupOperatorDeal()?.Operands.Count > 0)
                    {
                        groupOperator.Operands.Add(customerFilter.GetGroupOperatorDeal());
                    }
                }
            }

            //if (btnCustomer.EditValue is Customer customer)
            //{
            //    var criteriaCustomer = new BinaryOperator(nameof(Deal.Customer), customer);
            //    groupOperator.Operands.Add(criteriaCustomer);
            //}

            //if (btnStaff.EditValue is Staff staff)
            //{
            //    var criteriaStaff = new BinaryOperator(nameof(Deal.Staff), staff);
            //    groupOperator.Operands.Add(criteriaStaff);
            //}

            //if (cmbStatusDeal.EditValue is DealStatus dealStatus)
            //{
            //    var criteriaStatusDeal = new BinaryOperator(nameof(Deal.DealStatus), dealStatus);
            //    groupOperator.Operands.Add(criteriaStatusDeal);
            //}

            if (groupOperator.Operands.Count > 0)
            {
                return groupOperator;
            }
            else
            {
                return null;
            }
        }

        private async void treeListCustomerFilter_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            _deals.Filter = await GetFilter();

            gridView_FocusedRowChanged(gridView, new FocusedRowChangedEventArgs(-1, gridView.FocusedRowHandle));
        }

        private async void barBtnSaveLayoutToXmlMainGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            await SaveSettingsForms();
        }

        private void barBtnTaskAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            var obj = gridView.GetRow(gridView.FocusedRowHandle) as Deal;
            if (obj != null)
            {
                BVVGlobal.oFuncXpo.CreateTask<TaskEdit>(_session, obj);
            }

        }

        private async void barBtnRemovingEmptyTrades_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show($"При удалении учитываются сделки по следующим параметрам.{Environment.NewLine}" +
                    $"1. Не указан клиент.{Environment.NewLine}" +
                    $"2. Не указан отвественный.{Environment.NewLine}" +
                    $"3. Нет поставленной задачи.{Environment.NewLine}" +
                    $"Продолжить операцию массовой очистки сделок?",
                                    "Удаление пустых сделок",
                                    MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Question) == DialogResult.OK)
                {
                    using (var uof = new UnitOfWork())
                    {
                        var groupOperator = new GroupOperator(GroupOperatorType.And);

                        var critreiaDealCustomer = new NullOperator(nameof(Deal.Customer));
                        groupOperator.Operands.Add(critreiaDealCustomer);
                        var critreiaDealStaff = new NullOperator(nameof(Deal.Staff));
                        groupOperator.Operands.Add(critreiaDealStaff);
                        //var critreiaDealLetter = new NullOperator(nameof(Deal.Letter));
                        //groupOperator.Operands.Add(critreiaDealLetter);
                        var critreiaDealTask = new NullOperator(nameof(Deal.Task));
                        groupOperator.Operands.Add(critreiaDealTask);

                        var countUseDeals = 0;

                        using (var deals = new XPCollection<Deal>(uof, groupOperator))
                        {
                            countUseDeals = deals.Count;
                            uof.Delete(deals);

                            var user = DatabaseConnection.User;
                            if (user != null)
                            {
                                var chronicleEvents = new ChronicleEvents(uof)
                                {
                                    Act = Act.REMOVING_EMPTY_TRADES,
                                    Date = DateTime.Now,
                                    Name = Act.REMOVING_EMPTY_TRADES.GetEnumDescription(),
                                    Description = $"Пользователь [{user}] произвел удаление пустых сделок. Удалено: {countUseDeals}",
                                    User = await uof.GetObjectByKeyAsync<User>(user.Oid)
                                };
                                chronicleEvents.Save();
                            }
                            await uof.CommitChangesAsync();
                        }

                        XtraMessageBox.Show($"Удалено пустых сделок: {countUseDeals}",
                        "Удаление окончено",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }

                    _deals?.Reload();
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void barBtnControlSystemAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var obj = gridView.GetRow(gridView.FocusedRowHandle) as Deal;
            if (obj != null)
            {
                var form = new ControlSystemEdit(obj);
                form.ShowDialog();
            }
        }

        private async void barBtnDefault_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var node = treeListCustomerFilter.FocusedNode;
                if (node != null)
                {
                    var obj = default(object);
                    if (node.GetValue(0) is string name)
                    {
                        obj = name;
                    }
                    else if (node.GetValue(0) is CustomerFilter customerFilter)
                    {
                        if (customerFilter?.Oid > 0)
                        {
                            obj = customerFilter?.Oid;
                        }
                        else
                        {
                            obj = customerFilter?.Name;
                        }
                    }

                    await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"{this.Name}_{nameof(treeListCustomerFilter)}_{nameof(treeListCustomerFilter.FocusedNode)}", obj?.ToString(), true, user: BVVGlobal.oApp.User);
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void treeListCustomerFilter_PopupMenuShowing(object sender, DevExpress.XtraTreeList.PopupMenuShowingEventArgs e)
        {
            try
            {
                popupMenuTreeList.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void DealForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await SaveSettingsForms();
        }

        private bool isDisplayCompleted;
        private void gridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    barCheckIsCompleted.Checked = isDisplayCompleted;
                    popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void barCheckIsCompleted_ItemClick(object sender, ItemClickEventArgs e)
        {
            isDisplayCompleted = !isDisplayCompleted;
            treeListCustomerFilter_FocusedNodeChanged(null, null);
        }
    }
}