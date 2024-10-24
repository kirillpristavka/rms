using DevExpress.Data.Filtering;
using DevExpress.Office.Utils;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraRichEdit;
using RMS.Core.Enumerator;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMS.UI.Forms.Mail
{
    public partial class LetterHistoryForm : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; set; }
        private string Email { get; set; }
        private XPCollection<Letter> Letters { get; set; }

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewLetters);
            BVVGlobal.oFuncXpo.PressEnterGrid<Letter, LetterEdit>(gridViewLetters);
        }

        private LetterHistoryForm(Session session)
        {
            InitializeComponent();
            FunctionalGridSetup();

            AdjustSimpleViewPadding();
            AdjustDraftViewPadding();

            richLetter.ActiveViewType = RichEditViewType.Simple;
            Session = session ?? BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();            
        }

        private void AdjustMargins()
        {
            richLetter.Document.Sections[0].Margins.Left = Units.InchesToDocumentsF(2f);
        }
        private void AdjustSimpleViewPadding()
        {
            richLetter.Views.SimpleView.Padding = new DevExpress.Portable.PortablePadding(0);
        }

        private void AdjustDraftViewPadding()
        {
            richLetter.Views.DraftView.Padding = new DevExpress.Portable.PortablePadding(0);
        }

        public LetterHistoryForm(Session session, Customer customer) : this(session)
        {
            Customer = customer;
        }

        public LetterHistoryForm(Session session, string email) : this(session)
        {
            Email = email;
        }

        private void LetterForm_Load(object sender, EventArgs e)
        {
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Customer>(Session, btnCustomer, cls_App.ReferenceBooks.Customer);

            btnCustomer.EditValue = Customer;
            txtEmail.EditValue = Email;

            checkedListCustomerEmail.ItemCheck += CheckedListCustomerEmail_ItemCheck;            
            //GetXpCollectionLetters();
        }

        private void CheckedListCustomerEmail_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            GetXpCollectionLetters();
        }

        private void GetXpCollectionLetters()
        {
            var groupOperator = new GroupOperator(GroupOperatorType.Or);
            
            if (!string.IsNullOrWhiteSpace(Email))
            {
                var emailInCriteria = new BinaryOperator(nameof(Letter.LetterSenderAddress), Email);
                groupOperator.Operands.Add(emailInCriteria);
                var emailOutCriteria = new BinaryOperator(nameof(Letter.LetterRecipientAddress), Email);
                groupOperator.Operands.Add(emailOutCriteria);
            }

            if (Customer is null && btnCustomer.EditValue is Customer customer)
            {
                Customer = customer;
            }
            
            if (Customer != null)
            {
                if (!string.IsNullOrWhiteSpace(Customer.Email))
                {
                    var emailInCriteria = new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(nameof(Letter.LetterSenderAddress)), Customer.Email);
                    groupOperator.Operands.Add(emailInCriteria);
                    var emailOutCriteria = new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(nameof(Letter.LetterRecipientAddress)), Customer.Email);
                    groupOperator.Operands.Add(emailOutCriteria);
                }
                
                foreach (var customerEmail in Customer.CustomerEmails)
                {
                    if (!string.IsNullOrWhiteSpace(customerEmail.Email))
                    {
                        var emailInCriteria = new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(nameof(Letter.LetterSenderAddress)), customerEmail.Email);
                        groupOperator.Operands.Add(emailInCriteria);
                        var emailOutCriteria = new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(nameof(Letter.LetterRecipientAddress)), customerEmail.Email);
                        groupOperator.Operands.Add(emailOutCriteria);
                    }

                    if (!string.IsNullOrWhiteSpace(customerEmail.Email2))
                    {
                        var emailInCriteria = new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(nameof(Letter.LetterSenderAddress)), customerEmail.Email2);
                        groupOperator.Operands.Add(emailInCriteria);
                        var emailOutCriteria = new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(nameof(Letter.LetterRecipientAddress)), customerEmail.Email2);
                        groupOperator.Operands.Add(emailOutCriteria);
                    }
                }
            }

            foreach (CheckedListBoxItem email in checkedListCustomerEmail.Items)
            {
                if (email.CheckState == CheckState.Checked)
                {
                    var emailInCriteria = new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(nameof(Letter.LetterSenderAddress)), email?.ToString());
                    groupOperator.Operands.Add(emailInCriteria);
                    var emailOutCriteria = new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(nameof(Letter.LetterRecipientAddress)), email?.ToString());
                    groupOperator.Operands.Add(emailOutCriteria);
                }
            }

            if (groupOperator.Operands.Count == 0)
            {
                gridControlLetters.DataSource = null;
                gridViewLetters.Columns.Clear();
            }
            else
            {
                try
                {
                    Letters = new XPCollection<Letter>(Session, groupOperator, new SortProperty(nameof(Letter.DateReceiving), SortingDirection.Descending));
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());

                    groupOperator = new GroupOperator(GroupOperatorType.Or);
                    
                    if (string.IsNullOrWhiteSpace(Email))
                    {
                        if (Customer != null)
                        {
                            var customerCriteria = new BinaryOperator($"{nameof(Letter.Customer)}.{nameof(Customer.Oid)}", Customer?.Oid);
                            groupOperator.Operands.Add(customerCriteria);
                        }                       
                    }
                    else
                    {
                        var emailInCriteria = new BinaryOperator(nameof(Letter.LetterSenderAddress), Email);
                        groupOperator.Operands.Add(emailInCriteria);
                        var emailOutCriteria = new BinaryOperator(nameof(Letter.LetterRecipientAddress), Email);
                        groupOperator.Operands.Add(emailOutCriteria);
                    }

                    Letters = new XPCollection<Letter>(Session, groupOperator, new SortProperty(nameof(Letter.DateReceiving), SortingDirection.Descending));
                }               
                
                Letters.DisplayableProperties = $"{nameof(Letter.TypeLetter)};" +
                    $"{nameof(Letter.LetterSenderAddress)};" +
                    $"{nameof(Letter.LetterRecipientAddress)};" +
                    $"{nameof(Letter.DateReceiving)};" +
                    $"{nameof(Letter.TopicString)}";
                gridControlLetters.DataSource = Letters;

                if (gridViewLetters.Columns[nameof(Letter.TypeLetter)] != null)
                {
                    RepositoryItemImageComboBox imgTypeLetter = gridControlLetters.RepositoryItems.Add(nameof(ImageComboBoxEdit)) as RepositoryItemImageComboBox;
                    imgTypeLetter.SmallImages = imageCollectionTypeLetter;
                    imgTypeLetter.Items.Add(new ImageComboBoxItem() { Value = TypeLetter.InBox, ImageIndex = 0 });
                    imgTypeLetter.Items.Add(new ImageComboBoxItem() { Value = TypeLetter.Outgoing, ImageIndex = 1 });
                    imgTypeLetter.Items.Add(new ImageComboBoxItem() { Value = TypeLetter.Spam, ImageIndex = 2 });
                    imgTypeLetter.Items.Add(new ImageComboBoxItem() { Value = TypeLetter.Basket, ImageIndex = 3 });

                    imgTypeLetter.GlyphAlignment = HorzAlignment.Center;
                    gridViewLetters.Columns[nameof(Letter.TypeLetter)].ColumnEdit = imgTypeLetter;
                    gridViewLetters.Columns[nameof(Letter.TypeLetter)].Width = 18;
                    gridViewLetters.Columns[nameof(Letter.TypeLetter)].OptionsColumn.FixedWidth = true;
                }
            }

            gridViewLetters_FocusedRowChanged(gridViewLetters, new FocusedRowChangedEventArgs(-1, gridViewLetters.FocusedRowHandle));
        }
        
        private async Task RenderingLetterFormAsync(Letter letter, CancellationToken token = default)
        {
            await Task.Run(() =>
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
            }, cancellationToken: token);
        }

        private CancellationTokenSource cancelTokenSource;
        private CancellationToken token;

        private async void gridViewLetters_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gridViewLetters.IsEmpty)
                {
                    btnLetterSender.EditValue = null;
                    txtLetterRecipient.Text = null;
                    txtTopic.Text = null;
                    richLetter.HtmlText = null;
                    return;
                }

                cancelTokenSource?.Cancel();
                
                var letter = gridViewLetters.GetRow(gridViewLetters.FocusedRowHandle) as Letter;
                if (letter != null)
                {
                    cancelTokenSource = new CancellationTokenSource();
                    token = cancelTokenSource.Token;
                        
                    await RenderingLetterFormAsync(letter, token).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }
        
        private void btnRemoveLetter_Click(object sender, EventArgs e)
        {
            if (gridViewLetters.IsEmpty)
            {
                return;
            }

            var listLetter = new List<Letter>();

            foreach (var focusedRowHandle in gridViewLetters.GetSelectedRows())
            {
                var letter = gridViewLetters.GetRow(focusedRowHandle) as Letter;

                if (letter != null)
                {
                    listLetter.Add(letter);
                }
            }

            if (XtraMessageBox.Show($"Будет удалено писем: {listLetter.Count()}{Environment.NewLine}Хотите продолжить?",
                    "Удаление архивных папок",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {

                foreach (var item in listLetter)
                {
                    try
                    {
                        if (item.TypeLetter == TypeLetter.Basket)
                        {
                            if (item.Deal != null)
                            {
                                item.Deal.MessageDeleteLetter = $"Письмо [{item?.ToString()}] удалено пользователем {DatabaseConnection.User} - [{DateTime.Now.ToShortDateString()}]";
                                item.Deal.Letter = null;
                                item.Deal.Save();
                            }
                            item.Delete();
                        }
                        else
                        {
                            item.TypeLetter = TypeLetter.Basket;
                            item.Save();
                        }
                    }
                    catch (Exception ex)
                    {
                        RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                    }                    
                }
            }
        }

        private void gridViewLetters_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;

            if (view.Columns[nameof(Letter.IsRead)] != null)
            {
                var statusReport = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, view.Columns[nameof(Letter.IsRead)]));

                if (!statusReport)
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.BackColor = Color.LightGreen;
                }
            }
        }        

        private void gridViewLetters_MouseDown(object sender, MouseEventArgs e)
        {
            DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            GridView gridview = sender as GridView;
            GridHitInfo gridHitInfo = gridview.CalcHitInfo(dxMouseEventArgs.Location);
            if ((gridHitInfo.InRow || gridHitInfo.InRowCell) && dxMouseEventArgs.Button == MouseButtons.Right)
            {
                if (gridHitInfo.HitTest != GridHitTest.Footer && gridHitInfo.HitTest != GridHitTest.Column)
                {
                    if (dxMouseEventArgs.Button == MouseButtons.Right)
                    {
                        //popupMenuLetter.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                    }
                }
            }
        }

        private void btnCustomer_EditValueChanged(object sender, EventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;
            if (buttonEdit != null && buttonEdit.EditValue is Customer customer)
            {
                checkedListCustomerEmail.Items.Clear();
                checkedListCustomerEmail.Items.AddRange(customer.CustomerEmails?.ToArray());

                checkedListCustomerEmail.ItemCheck -= CheckedListCustomerEmail_ItemCheck;
                foreach (CheckedListBoxItem email in checkedListCustomerEmail.Items)
                {
                    email.CheckState = CheckState.Checked;
                }
                checkedListCustomerEmail.ItemCheck += CheckedListCustomerEmail_ItemCheck;
            }
            else
            {
                checkedListCustomerEmail.Items.Clear();
            }
            GetXpCollectionLetters();
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                Customer = null;

                GetXpCollectionLetters();
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);

            if (buttonEdit.EditValue is Customer customer)
            {
                Customer = customer;
            }
        }

        private void txtEmail_EditValueChanged(object sender, EventArgs e)
        {
            var textEdit = sender as TextEdit;
            if (textEdit != null)
            {
                Email = textEdit.Text.Trim();
            }
            GetXpCollectionLetters();
        }

        private void richLetter_DocumentLoaded(object sender, EventArgs e)
        {
            try
            {
                //AdjustMargins();

                //Invoke((Action)delegate
                //{
                //    richLetter.Focus();
                //});

                //richLetter.Document.CaretPosition = richLetter.Document.Range.Start;
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }
    }
}