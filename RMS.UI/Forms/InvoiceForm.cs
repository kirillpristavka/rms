using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using RMS.Core.Controller.Print;
using RMS.Core.Enumerator;
using RMS.Core.Model;
using RMS.Core.Model.Mail;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms
{
    public partial class InvoiceForm : XtraForm
    {
        private Session _session;
        private XPCollection<Invoice> _invoices { get; set; }

        /// <summary>
        /// Текущий счет.
        /// </summary>
        private Invoice _currentInvoice;

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewInvoices);
            BVVGlobal.oFuncXpo.PressEnterGrid<Invoice, InvoiceEdit>(gridViewInvoices);
        }

        public InvoiceForm(Session session)
        {
            InitializeComponent();
            FunctionalGridSetup();
            _session = session ?? DatabaseConnection.GetWorkSession();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            _invoices = new XPCollection<Invoice>(_session);
            _invoices.Criteria = await cls_BaseSpr.GetCustomerCriteria(null, nameof(Invoice.Customer));

            gridControlInvoices.DataSource = _invoices;
            if (gridViewInvoices.Columns[nameof(Invoice.Oid)] != null)
            {
                gridViewInvoices.Columns[nameof(Invoice.Oid)].Visible = false;
                gridViewInvoices.Columns[nameof(Invoice.Oid)].Width = 18;
                gridViewInvoices.Columns[nameof(Invoice.Oid)].OptionsColumn.FixedWidth = true;
            }
            if (gridViewInvoices.Columns[nameof(Invoice.IsSent)] != null)
            {
                RepositoryItemImageComboBox imgIsSent = gridControlInvoices.RepositoryItems.Add(nameof(ImageComboBoxEdit)) as RepositoryItemImageComboBox;
                imgIsSent.SmallImages = imgInvoice;
                imgIsSent.Items.Add(new ImageComboBoxItem() { Value = true, ImageIndex = 0 });
                imgIsSent.Items.Add(new ImageComboBoxItem() { Value = false, ImageIndex = -1 });

                imgIsSent.GlyphAlignment = HorzAlignment.Center;
                gridViewInvoices.Columns[nameof(Invoice.IsSent)].ColumnEdit = imgIsSent;
                gridViewInvoices.Columns[nameof(Invoice.IsSent)].Width = 18;
                gridViewInvoices.Columns[nameof(Invoice.IsSent)].OptionsColumn.FixedWidth = true;
            }
        }

        private void btnAddInvoice_Click(object sender, EventArgs e)
        {
            var form = new InvoiceEdit(_session);
            form.ShowDialog();

            if (form.Invoice != null && form.IsSave)
            {
                _invoices?.Reload();
                gridViewInvoices.FocusedRowHandle = gridViewInvoices.LocateByValue(nameof(Invoice.Oid), form.Invoice.Oid);
            }
        }

        private void btnEditInvoice_Click(object sender, EventArgs e)
        {
            if (gridViewInvoices.IsEmpty)
            {
                return;
            }

            var invoice = gridViewInvoices.GetRow(gridViewInvoices.FocusedRowHandle) as Invoice;
            if (invoice != null)
            {
                var form = new InvoiceEdit(invoice);
                form.ShowDialog();
            }
        }

        private void btnDelInvoice_Click(object sender, EventArgs e)
        {
            if (gridViewInvoices.IsEmpty)
            {
                return;
            }

            var invoice = gridViewInvoices.GetRow(gridViewInvoices.FocusedRowHandle) as Invoice;
            if (invoice != null)
            {
                invoice.Delete();
            }
        }

        private void gridViewInvoices_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            _currentInvoice = null;
            gridControlInvoiceInformation.DataSource = null;

            var gridview = sender as GridView;
            if (gridview == null || gridViewInvoices.IsEmpty)
            {
                return;
            }

            _invoices?.Reload();
            _currentInvoice = gridview.GetRow(gridview.FocusedRowHandle) as Invoice;
            if (_currentInvoice != null)
            {
                gridControlInvoiceInformation.DataSource = _currentInvoice.InvoiceInformations;
                if (gridViewInvoiceInformation.Columns[nameof(InvoiceInformation.Oid)] != null)
                {
                    gridViewInvoiceInformation.Columns[nameof(InvoiceInformation.Oid)].Visible = false;
                    gridViewInvoiceInformation.Columns[nameof(InvoiceInformation.Oid)].Width = 18;
                    gridViewInvoiceInformation.Columns[nameof(InvoiceInformation.Oid)].OptionsColumn.FixedWidth = true;
                }
            }
        }

        private void gridViewInvoices_MouseDown(object sender, MouseEventArgs e)
        {
            DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            GridView gridview = sender as GridView;
            _ = gridview.CalcHitInfo(dxMouseEventArgs.Location);
            //if ((gridHitInfo.InRow || gridHitInfo.InRowCell) && dxMouseEventArgs.Button == MouseButtons.Right)
            if (dxMouseEventArgs.Button == MouseButtons.Right)
            {
                popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private async void barBtnSend_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewInvoices.IsEmpty)
            {
                return;
            }

            var invoice = gridViewInvoices.GetRow(gridViewInvoices.FocusedRowHandle) as Invoice;
            if (invoice != null && invoice.SentByEmailDate is null)
            {
                var mailbox = default(Mailbox);
                var mailingAddressName = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.MailboxForSending), string.Empty);

                if (string.IsNullOrWhiteSpace(mailingAddressName))
                {
                    mailbox = _session.FindObject<Mailbox>(new BinaryOperator(nameof(Mailbox.StateMailbox), StateMailbox.Working));
                }
                else
                {
                    mailbox = _session.FindObject<Mailbox>(new BinaryOperator(nameof(Mailbox.MailingAddress), mailingAddressName));
                }

                var letter = new Letter(_session)
                {
                    LetterSender = mailbox.MailingAddress,
                    LetterSenderAddress = mailbox.MailingAddress,
                    Customer = invoice.Customer,
                    TypeLetter = TypeLetter.Outgoing,
                    Topic = Letter.StringToByte(invoice.ToString()),
                    DateCreate = DateTime.Now,
                    LetterRecipient = invoice.Customer.Email,
                    IsRead = true
                };
                letter.SetMailBox(mailbox);

                var invoiceView = new InvoiceView(invoice, false);

                letter.LetterAttachments.Add(new LetterAttachment(_session, invoiceView.PathFile));
                letter.Save();

                Letter.SendMailKit(letter.LetterRecipient,
                        Letter.ByteToString(letter.Topic),
                        Letter.ByteToString(letter.Topic),
                        mailbox.MailingAddress,
                        BVVGlobal.oApp.User,
                        Mailbox.Decrypt(mailbox.Password),
                        mailbox.MailboxSetup.OutgoingMailServerSMTP,
                        mailbox.MailboxSetup.PortSMTP,
                        letter.LetterAttachments.ToList(),
                        mailbox.MailingAddressCopy);

                invoice.SentByEmailDate = DateTime.Now;
                invoice.Save();
            }
        }

        private void barBtnTaskAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            BVVGlobal.oFuncXpo.CreateTask<TaskEdit>(_session);
        }

        private void barBtnControlSystemAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewInvoices.IsEmpty)
            {
                return;
            }

            var obj = gridViewInvoices.GetRow(gridViewInvoices.FocusedRowHandle) as Invoice;
            if (obj != null)
            {
                var form = new ControlSystemEdit(obj);
                form.ShowDialog();
            }
        }
    }
}