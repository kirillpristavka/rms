using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using PulsLibrary.Extensions.DevForm;
using RMS.Core.Model.Mail;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.xUI.PostOffice.Controllers
{
    public partial class LetterControl : XtraUserControl
    {
        //private Contract _contract;
        private List<Letter> _listObj;

        public delegate void FocusedRowChangedEventHandler(Letter obj, int focusedRowHandle);
        public event FocusedRowChangedEventHandler FocusedRowChangedEvent;

        public LetterControl()
        {
            InitializeComponent();
            _listObj = new List<Letter>();
        }
        
        public LetterControl(List<Letter> listObj) : this()
        {
            _listObj = listObj;
        }
        
        /// <summary>
        /// Задать договор при работе с формой.
        /// </summary>
        /// <param name="contract"></param>
        //public void SetContrac(Contract contract)
        //{
        //    _contract = contract;
        //}

        public void UpdateData(object listObj)
        {
            
            if (listObj is List<Letter> list)
            {
                _listObj = list; 
                gridControl.DataSource = this._listObj;
            }
            else
            {
                gridControl.DataSource = new List<Letter>();
            }
        }

        private void Control_Load(object sender, EventArgs e)
        {
            UpdateData(_listObj);
            
            gridView.ColumnSetup($"{nameof(Letter.Oid)}", isVisible: false, caption: "[OID]", width: 150, isFixedWidth: true, horzAlignment: HorzAlignment.Center, visibleIndex: 0);
            gridView.ColumnSetup($"{nameof(Letter.IsLetterAttachments)}", caption: "A", width: 25, isFixedWidth: true, horzAlignment: HorzAlignment.Center, visibleIndex: 1);
            gridView.ColumnSetup($"{nameof(Letter.DealStatusString)}", caption: "D", width: 25, isFixedWidth: true, horzAlignment: HorzAlignment.Center, visibleIndex: 2);
            gridView.ColumnSetup($"{nameof(Letter.IsReplySent)}", caption: "S", width: 25, isFixedWidth: true, horzAlignment: HorzAlignment.Center, visibleIndex: 3);
            gridView.ColumnSetup($"{nameof(Letter.LetterSender)}", caption: "Отправитель\nписьма", width: 300, isFixedWidth: true, visibleIndex: 4);
            gridView.ColumnSetup($"{nameof(Letter.LetterRecipient)}", caption: "Получатель\nписьма", width: 300, isFixedWidth: true, visibleIndex: 5);
            gridView.ColumnSetup($"{nameof(Letter.TopicString)}", caption: "Тема", width: 350, isFixedWidth: true, visibleIndex: 6);
            gridView.ColumnSetup($"{nameof(Letter.DateReceiving)}", caption: "Дата\nполучения", width: 200, isFixedWidth: true, horzAlignment: HorzAlignment.Center, visibleIndex: 7);
            gridView.ColumnSetup($"{nameof(Letter.DateCreate)}", caption: "Дата\nсоздания", width: 200, isFixedWidth: true, horzAlignment: HorzAlignment.Center, visibleIndex: 8);
            gridView.ColumnSetup($"{nameof(Letter.CustomerString)}", caption: "Клиент", width: 350, isFixedWidth: true, visibleIndex: 9);

            gridView.ColumnSetup($"{nameof(Letter.UniqueId)}", isVisible: false, caption: "UniqueId", width: 100, isFixedWidth: true, horzAlignment: HorzAlignment.Center, visibleIndex: 10);
            gridView.ColumnSetup($"{nameof(Letter.MessageId)}", isVisible: false, caption: "MessageId", width: 250, isFixedWidth: true, visibleIndex: 11);
            
            gridView.ColumnDelete(nameof(Letter.AnswerLetter));
            gridView.ColumnDelete(nameof(Letter.DealStatus));
            gridView.ColumnDelete(nameof(Letter.Deal));
            gridView.ColumnDelete(nameof(Letter.TypeLetter));
            gridView.ColumnDelete(nameof(Letter.IsRead));
            gridView.ColumnDelete(nameof(Letter.LetterSenderAddress));
            gridView.ColumnDelete(nameof(Letter.User));
            gridView.ColumnDelete(nameof(Letter.UserString));
            gridView.ColumnDelete(nameof(Letter.LetterRecipientAddress));
            gridView.ColumnDelete(nameof(Letter.Topic));
            gridView.ColumnDelete(nameof(Letter.LetterSize));
            gridView.ColumnDelete(nameof(Letter.TextBody));
            gridView.ColumnDelete(nameof(Letter.HtmlBody));
            gridView.ColumnDelete(nameof(Letter.LetterCatalog));
            gridView.ColumnDelete(nameof(Letter.MailingAddress));
            gridView.ColumnDelete(nameof(Letter.Mailbox));
            gridView.ColumnDelete(nameof(Letter.Customer));
                        
            gridControl.GridControlSetup();
            gridView.GridViewSetup(isColumnAutoWidth: false, isShowFooter: false);
            
            gridView.BestFitColumns();
        }

        /// <summary>
        /// Открытие формы редактирования.
        /// </summary>
        /// <param name="obj">Операция для изменения.</param>
        private void OpenEditForm(object obj)
        {
            //var form = new RenterEdit(obj);
            //form.FormClosed += OperationEditFormClosed;
            //form.XtraFormShow();
        }

        private void OperationEditFormClosed(object sender, FormClosedEventArgs e)
        {
            //if (sender is RenterEdit renterEdit)
            //{
            //    if (renterEdit.IsSave)
            //    {
            //        var currentRenter = renterEdit.Renter;
            //        if (currentRenter?.Contract?.Oid == _contract?.Oid)
            //        {
            //            var renter = _listObj.FirstOrDefault(f => f.Oid == currentRenter.Oid);
            //            if (renter is null)
            //            {
            //                _listObj.Add(currentRenter);
            //                renter = currentRenter;
            //            }

            //            renter?.Reload();
            //            gridView.RefreshData();
            //            gridView.FocusedRowHandle = gridView.LocateByValue(nameof(Renter.Oid), renter?.Oid);
            //        }
            //        else if (_contract is null)
            //        {
            //            var renter = _listObj.FirstOrDefault(f => f.Oid == currentRenter.Oid);
            //            renter?.Reload();
            //            gridView.RefreshData();
            //            gridView.FocusedRowHandle = gridView.LocateByValue(nameof(Renter.Oid), renter?.Oid);
            //        }
            //    }
            //}
        }

        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            if (e is DXMouseEventArgs ea)
            {
                if (sender is GridView gridView)
                {
                    var info = gridView.CalcHitInfo(ea.Location);
                    if (info.InRow)
                    {
                        if (gridView.GetRow(gridView.FocusedRowHandle) is Letter obj)
                        {
                            OpenEditForm(obj);
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
                    }

                    barCheckFindPanel.Checked = gridView.IsFindPanelVisible;
                    barCheckAutoFilterRow.Checked = gridView.OptionsView.ShowAutoFilterRow;

                    popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void barBtnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            //OpenEditForm(_contract);
        }

        private void barBtnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is Letter obj)
            {
                OpenEditForm(obj);
            }
        }

        private async void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is Letter obj)
            {
                var text = $"Вы действительно хотите удалить арендатора: {obj}?";
                var caption = $"Удаление арендатора [OID:{obj.Oid}]";

                if (XtraMessageBox.Show(text,
                                        caption,
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {

                    //using (var uof = new UnitOfWork())
                    //{
                    //    var renter = await uof.GetObjectByKeyAsync<Letter>(obj.Oid);
                    //    if (renter != null)
                    //    {
                    //        renter.Delete();
                    //        await uof.CommitTransactionAsync().ConfigureAwait(false);

                    //        _listObj.Remove(obj);
                    //        gridView.FocusedRowHandle = gridView.FocusedRowHandle - 1;
                    //        gridView.RefreshData();
                    //    }
                    //}
                }
            }
        }

        private void barBtnUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridView.RefreshData();
        }

        private void barCheckFindPanel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.IsFindPanelVisible)
            {
                gridView.HideFindPanel();
            }
            else
            {
                gridView.ShowFindPanel();
            }
        }

        private void barCheckAutoFilterRow_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.OptionsView.ShowAutoFilterRow)
            {
                gridView.OptionsView.ShowAutoFilterRow = false;
            }
            else
            {
                gridView.OptionsView.ShowAutoFilterRow = true;
            }
        }

        private void gridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is Letter obj)
            {
                FocusedRowChangedEvent?.Invoke(obj, gridView.FocusedRowHandle);
            }
        }
    }
}
