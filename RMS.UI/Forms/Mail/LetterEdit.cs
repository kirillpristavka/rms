using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using HtmlAgilityPack;
using PulsLibrary.Extensions.DevForm;
using RMS.Core.Controllers;
using RMS.Core.Enumerator;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using RMS.UI.Control.Mail;
using RMS.UI.Forms.Directories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace RMS.UI.Forms.Mail
{
    public partial class LetterEdit : formEdit_BaseSpr
    {
        private bool _isUseSubstitutionDictionary;
        private bool _isTransmute;
        private LetterEditControl _letterEditControl;
        private RichEditControl _richLetter;
        private Session _session;
        private LetterTemplate _letterTemplate;
        private Mailbox _mailbox;
        private Letter _answerLetter;
      
        public Letter Letter { get; }
        public Customer Customer { get; set; }
        public Deal Deal { get; set; }

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewLetterAttachment);
        }

        public LetterEdit()
        {
            InitializeComponent();
                        
            _letterEditControl = new LetterEditControl();
            _letterEditControl.Dock = DockStyle.Fill;
            panelControlRichEdit.Controls.Add(_letterEditControl);
            _richLetter = _letterEditControl.RichLetter;

            if (_session is null)
            {
                _session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();

                Letter = new Letter(_session)
                {
                    TypeLetter = TypeLetter.Outgoing,
                    IsRead = true
                };
            }

            FunctionalGridSetup();
        }

        public LetterEdit(Customer customer, string topic, LetterTemplate letterTemplate = null) : this()
        {
            Letter.Customer = customer;
            _letterTemplate = letterTemplate;
            Letter.Topic = Letter.StringToByte(topic);

            Letter.LetterRecipient = customer.Email;
            Letter.LetterRecipientAddress = customer.Email;

            _isUseSubstitutionDictionary = true;
        }

        public LetterEdit(Mailbox mailbox,
                          string letterRecipient,
                          string topic,
                          byte[] text = null,
                          bool isTransmute = false,
                          List<LetterAttachment> letterAttachments = default, 
                          Letter answerLetter = null) : this()
        {
            _mailbox = mailbox;
            this._isTransmute = isTransmute;
            this._answerLetter = answerLetter;
            _session = mailbox.Session;

            if (Letter.Session != _session)
            {
                Letter = new Letter(_session)
                {
                    TypeLetter = TypeLetter.Outgoing,
                    IsRead = true                    
                };

                if (text != null)
                {
                    Letter.HtmlBody = text;
                }

                if (letterAttachments != null && letterAttachments.Count > 0)
                {
                    foreach (var attachment in letterAttachments)
                    {
                        Letter.LetterAttachments.Add(attachment);
                    }
                    //Letter.LetterAttachments.AddRange(letterAttachments);
                }
            }

            Letter.Topic = Letter.StringToByte(topic);
            Letter.LetterSender = _mailbox.MailingAddress;

            if (this._isTransmute is false)
            {
                Letter.LetterRecipientAddress = letterRecipient;
            }            
        }

        public LetterEdit(int id) : this()
        {
            if (id > 0)
            {
                Letter = _session.GetObjectByKey<Letter>(id);

                if (Letter != null)
                {
                    _mailbox = Letter.Mailbox;
                    Customer = Letter.Customer;
                }
            }
        }

        public LetterEdit(Letter letter) : this()
        {
            Letter = letter;
            _session = letter.Session;
            _mailbox = letter.Mailbox;
        }
        
        public string ToHtml(string s, bool nofollow)
        {
            s = System.Web.HttpUtility.HtmlEncode(s);
            string[] paragraphs = s.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);
            StringBuilder sb = new StringBuilder();
            foreach (string par in paragraphs)
            {
                sb.AppendLine("<p>");
                string p = par.Replace(Environment.NewLine, "<br />\r\n");
                if (nofollow)
                {
                    p = Regex.Replace(p, @"\[\[(.+)\]\[(.+)\]\]", "<a href=\"$2\" rel=\"nofollow\">$1</a>");
                    p = Regex.Replace(p, @"\[\[(.+)\]\]", "<a href=\"$1\" rel=\"nofollow\">$1</a>");
                }
                else
                {
                    p = Regex.Replace(p, @"\[\[(.+)\]\[(.+)\]\]", "<a href=\"$2\">$1</a>");
                    p = Regex.Replace(p, @"\[\[(.+)\]\]", "<a href=\"$1\">$1</a>");
                    sb.AppendLine(p);
                }
                sb.AppendLine("</p>");
            }
            return sb.ToString();
        }
              
        
        private Dictionary<string, string> emails = new Dictionary<string, string>();
        private void UpdateEmails(object oidCustomer = null)
        {
            var criteriaCustomer = default(BinaryOperator);
            if (oidCustomer != null)
            {
                criteriaCustomer = new BinaryOperator(nameof(CustomerEmail.Customer), oidCustomer);
            }
            
            using (var address = new XPCollection<CustomerEmail>(_session, criteriaCustomer))
            {
                address?.Reload();
                
                txtLetterRecipientCustomer.Properties.DataSource = null;
                txtLetterRecipientCustomer.Properties.Columns.Clear();
                emails.Clear();

                foreach (var item in address)
                {
                    AddObjectInDictionaryEmails(item, nameof(CustomerEmail.Email));
                    AddObjectInDictionaryEmails(item, nameof(CustomerEmail.Email2));
                }

                txtLetterRecipientCustomer.Properties.DataSource = emails;

                txtLetterRecipientCustomer.Properties.ValueMember = "Key";
                txtLetterRecipientCustomer.Properties.DisplayMember = "Value";

                txtLetterRecipientCustomer.Properties.SearchMode = SearchMode.AutoFilter;
                txtLetterRecipientCustomer.Properties.AutoSearchColumnIndex = 0;

                txtCopy.Properties.Items.Clear();
                txtCopy.Properties.Items.AddRange(emails.Keys);
            }
        }

        private void AddObjectInDictionaryEmails(CustomerEmail customerEmail, string nameProperty)
        {
            var obj = customerEmail.GetMemberValue(nameProperty);

            if (obj != null && obj is string result)
            {
                if (!string.IsNullOrWhiteSpace(result) && emails.ContainsKey(result) is false)
                {
                    var description = $"{customerEmail.Customer}";

                    if (!string.IsNullOrWhiteSpace(customerEmail.FullName) && !description.Contains(customerEmail.FullName))
                    {
                        description += $" [{customerEmail.FullName}]";
                    }

                    var fullDescription = $"{result} <{description.Trim()}>";

                    if (isShowContactLetterForm is false)
                    {
                        fullDescription = $"<{description.Trim()}>";
                    }

                    if (customerEmail.IsDefault)
                    {
                        fullDescription += " [ПО УМОЛЧАНИЮ]";
                    }
                    
                    if (!string.IsNullOrWhiteSpace(customerEmail.Comment))
                    {
                        fullDescription += $" - {customerEmail.Comment}";
                    }

                    emails.Add(result, fullDescription);
                }
            }            
        }

        private bool isEditLetterForm = false;
        private bool isShowContactLetterForm = false;
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
                            isEditLetterForm = accessRights.IsEditLetterForm;
                            isShowContactLetterForm = accessRights.IsShowContactLetterForm;
                            
                            isEditDealForm = accessRights.IsEditDealForm;
                        }                        
                    }
                }

                btnSendLetter.Enabled = isEditLetterForm;
                btnDelete.Enabled = isEditLetterForm;
                btnAddLetterAttachment.Enabled = isEditLetterForm;
                btnDelLetterAttachment.Enabled = isEditLetterForm;
                
                barBtnAddLetterTemplate.Enabled = isEditLetterForm;
                barBtnEditLetterTemplate.Enabled = isEditLetterForm;
                barBtnDelLetterTemplate.Enabled = isEditLetterForm;
                
                txtLetterRecipientCustomer.Enabled = isEditLetterForm;

                CustomerEdit.CloseButtons(btnCustomerLetter, isEditLetterForm);
                CustomerEdit.CloseButtons(btnLetterSender, isEditLetterForm);
                CustomerEdit.CloseButtons(txtLetterRecipientCustomer, isEditLetterForm);

                btnSaveDeal.Enabled = isEditDealForm;
                CustomerEdit.CloseButtons(btnCustomer, isEditDealForm);
                CustomerEdit.CloseButtons(btnStaff, isEditDealForm);
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        private XPCollection<LetterTemplate> LetterTemplates { get; set; }
        private void GetLetterTemplate()
        {
            try
            {
                LetterTemplates = new XPCollection<LetterTemplate>(_session);
                LetterTemplates.DisplayableProperties = "Name";
                
                gridControlLetterTemplate.DataSource = LetterTemplates;
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }            
        }
        
        private async void gridViewLetterTemplate_DoubleClick(object sender, EventArgs e)
        {
            var gridview = sender as GridView;
            if (gridview != null)
            {
                var obj = gridview.GetRow(gridview.FocusedRowHandle) as LetterTemplate;
                if (obj != null)
                {
                    if (XtraMessageBox.Show($"Вы точно хотите применить шаблон [{obj}] для текущего письма?",
                                            "Изменение письма",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        await UseLetterTemplate(obj.Oid);
                    }                    
                }
            }
        }

        private void barBtnAddLetterTemplate_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var form = new LetterTemplateEdit();
                form.ShowDialog();
                if (form.LetterTemplate != null && form.LetterTemplate.Oid > 0)
                {
                    LetterTemplates.Reload();
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void barBtnEditLetterTemplate_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var obj = gridViewLetterTemplate.GetRow(gridViewLetterTemplate.FocusedRowHandle) as LetterTemplate;
                if (obj != null)
                {
                    var form = new LetterTemplateEdit(obj);
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void barBtnDelLetterTemplate_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var obj = gridViewLetterTemplate.GetRow(gridViewLetterTemplate.FocusedRowHandle) as LetterTemplate;
                if (obj != null)
                {

                    if (XtraMessageBox.Show($"Вы точно хотите удалить шаблон [{obj}]?",
                                            "Изменение письма",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        obj.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void gridViewLetterTemplate_MouseDown(object sender, MouseEventArgs e)
        {
            DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            GridView gridview = sender as GridView;
            _ = gridview.CalcHitInfo(dxMouseEventArgs.Location);

            if (dxMouseEventArgs.Button == MouseButtons.Right)
            {
                popupMenuLetterTemplate.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private bool _isShowRelatedEmails = false;
        private IList<LetterAttachment> GetLetterAttachments(Letter letter)
        {
            if (letter is null)
            {
                return new List<LetterAttachment>();
            }

            var result = new List<LetterAttachment>();
            if (letter.LetterAttachments != null)
            {
                result.AddRange(letter.LetterAttachments);
            }

            if (_isShowRelatedEmails)
            {
                if (letter.AnswerLetter != null)
                {
                    result.AddRange(GetLetterAttachments(letter.AnswerLetter));
                }
            }

            gridControlLetterAttachment.DataSource = result;
            return result;
        }

        private void barCheckItemShowRelatedEmails_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            _isShowRelatedEmails = true;
            GetLetterAttachments(Letter);
        }

        private async void LetterEdit_Load(object sender, EventArgs e)
        {
            await SetAccessRights();
            GetLetterTemplate();

            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Customer>(_session, btnCustomer, cls_App.ReferenceBooks.Customer, isEnable: isEditDealForm);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Customer>(_session, btnCustomerLetter, cls_App.ReferenceBooks.Customer, isEnable: isEditLetterForm);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Staff>(_session, btnStaff, cls_App.ReferenceBooks.Staff, isEnable: isEditDealForm);

            try
            {
                var deals = await new XPQuery<DealStatus>(_session).ToListAsync();
                cmbStatusDeal.Properties.Items.AddRange(deals);
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }            

            if (Letter.TypeLetter == TypeLetter.Outgoing || Letter.TypeLetter == TypeLetter.Draft)
            {
                layoutControlGroupDeal.Visibility = LayoutVisibility.Never;
                splitterItemDeal.Visibility = LayoutVisibility.Never;
                
                layoutControlItemLetterRecipientCustomer.Visibility = LayoutVisibility.Always;
                layoutControlItemCopy.Visibility = LayoutVisibility.Always;
            }
            else
            {
                layoutControlGroupLetterTemplate.Visibility = LayoutVisibility.Never;
                splitterItemLetterAttachment.Visibility = LayoutVisibility.Never;
                layoutControlItemIsCustomer.Visibility = LayoutVisibility.Never;
                emptySpaceItemIsCustomer.Visibility = LayoutVisibility.Never;
                
                splitterItemDeal.Visibility = LayoutVisibility.Always;
                layoutControlItemLetterRecipient.Visibility = LayoutVisibility.Always;
                layoutControlItemResponsible.Visibility = LayoutVisibility.Always;
                layoutControlItemCustomerLetter.Visibility = LayoutVisibility.Always;

                if (Letter.Deal is null)
                {
                    Letter.Deal = new Deal(_session)
                    {
                        Letter = Letter,
                        Customer = Letter.Customer,
                        Staff = Letter.Customer?.AccountantResponsible,
                        DealStatus = await _session.FindObjectAsync<DealStatus>(new BinaryOperator(nameof(DealStatus.IsDefault), true))
                    };
                    layoutControlItemSaveDeal.Visibility = LayoutVisibility.Always;
                }
                else
                {
                    Letter.Deal?.Reload();
                }

                Deal = Letter.Deal;

                txtNumber.EditValue = Deal.Number;
                txtName.EditValue = Deal.Number;
                cmbStatusDeal.EditValue = Deal.DealStatus;
                memoDescription.EditValue = Deal.Description;
                btnCustomer.EditValue = Deal.Customer;
                btnStaff.EditValue = Deal.Staff;
            }           

            txtNumber.EditValueChanged += DealEditValueChanged;
            txtName.EditValueChanged += DealEditValueChanged;
            btnCustomer.EditValueChanged += DealEditValueChanged;
            btnStaff.EditValueChanged += DealEditValueChanged;
            cmbStatusDeal.EditValueChanged += DealEditValueChanged;
            memoDescription.EditValueChanged += DealEditValueChanged;
            
            _mailbox = Letter.Mailbox;
            Customer = Letter.Customer;

            if (Letter.TypeLetter == TypeLetter.InBox)
            {
                panelControlRichEdit.Visible = true;
                btnLetterSender.EditValue = Letter.LetterSender;
            }
            else if (Letter.TypeLetter == TypeLetter.Outgoing || Letter.TypeLetter == TypeLetter.Draft)
            {
                UpdateEmails();
                checkIsCustomer.Checked = true;

                if (Letter.Mailbox != null)
                {
                    btnLetterSender.EditValue = $"{Letter.Mailbox.Login} <{Letter.Mailbox.MailingAddress}>";
                }
                else
                {
                    var mailbox = default(Mailbox);
                    var mailingAddressName = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.MailboxForSending), "");

                    if (string.IsNullOrWhiteSpace(mailingAddressName))
                    {
                        mailbox = _session.FindObject<Mailbox>(new BinaryOperator(nameof(_mailbox.StateMailbox), StateMailbox.SendingLetters));
                    }
                    else
                    {
                        mailbox = _session.FindObject<Mailbox>(new BinaryOperator(nameof(_mailbox.MailingAddress), mailingAddressName));
                    }

                    if (mailbox is null)
                    {
                        mailbox = _session.FindObject<Mailbox>(new BinaryOperator(nameof(_mailbox.StateMailbox), StateMailbox.Working));
                    }

                    if (mailbox != null)
                    {
                        Letter.SetMailBox(mailbox);
                        _mailbox = mailbox;
                        btnLetterSender.EditValue = $"{mailbox.Login} <{mailbox.MailingAddress}>";
                    }
                }
            }

            txtLetterRecipientCustomer.EditValue = Letter.LetterRecipientAddress;

            var letterRecipient = Letter.LetterRecipientAddress;
            if (!string.IsNullOrWhiteSpace(Letter.CopiesAddresses))
            {
                letterRecipient += $";{Letter.CopiesAddresses}";
            }
            txtLetterRecipient.EditValue = letterRecipient;
            txtTopic.EditValue = Letter.ByteToString(Letter.Topic);

            if (string.IsNullOrWhiteSpace(btnLetterSender.Text))
            {
                btnLetterSender.Text = Letter.LetterSender;
            }
            
            txtLetterRecipient.Text = Letter.LetterRecipient;
            txtResponsible.Text = Letter.Customer?.AccountantResponsible?.ToString() ?? string.Empty;

            btnCustomerLetter.EditValue = Customer;

            //gridControlLetterAttachment.DataSource = Letter.LetterAttachments;
            gridControlLetterAttachment.DataSource = GetLetterAttachments(Letter);
            //GetLetterAttachments(Letter);
            foreach (GridColumn column in gridViewLetterAttachment.Columns)
            {
                column.Visible = false;
            }
            gridViewLetterAttachment.ColumnSetup(nameof(LetterAttachment.FullFileName), caption: "Файл");

            if (gridViewLetterAttachment.Columns[nameof(LetterAttachment.Oid)] != null)
            {
                gridViewLetterAttachment.Columns[nameof(LetterAttachment.Oid)].Visible = false;
                gridViewLetterAttachment.Columns[nameof(LetterAttachment.Oid)].Width = 18;
                gridViewLetterAttachment.Columns[nameof(LetterAttachment.Oid)].OptionsColumn.FixedWidth = true;
            }
            
            if (Letter.TypeLetter == TypeLetter.InBox)
            {
                btnLetterSender.Properties.ReadOnly = true;
                btnLetterSender.Properties.TextEditStyle = TextEditStyles.Standard;
                foreach (EditorButton editorButton in btnLetterSender.Properties.Buttons)
                {
                    editorButton.Visible = false;
                }
                txtLetterRecipient.Properties.ReadOnly = true;
                txtLetterRecipientCustomer.Properties.ReadOnly = true;
                txtResponsible.Properties.ReadOnly = true;
                txtTopic.Properties.ReadOnly = true;
                btnSendLetter.ToolTip = "Ответить";

                layoutControlItemTransmute.Visibility = LayoutVisibility.Always;
                btnSendLetter.ImageOptions.Image = imageCollection.Images["newmail_32x32.png"];
                Text = $"Входящее письмо";
                
                if (!string.IsNullOrWhiteSpace(Letter?.LetterSender))
                {
                    Text += $" [{Letter?.LetterSender}]";
                }
            }
            else
            {
                btnSendLetter.ToolTip = "Отправить";

                layoutControlItemTransmute.Visibility = LayoutVisibility.Never;
                btnTransmute.Visible = false;
                btnSendLetter.ImageOptions.Image = imageCollection.Images["send_32x32.png"];
                Text = "Исходящее письмо";
            }

            txtCopy.EditValue = Letter.CopiesAddresses;

            var letterBody = Letter.ByteToString(Letter.TextBody);
            var letterHtmlBody = Letter.ByteToString(Letter.HtmlBody);
            
            if (Letter.TypeLetter == TypeLetter.Outgoing)
            {                
                if (string.IsNullOrWhiteSpace(letterHtmlBody))
                {
                    _richLetter.HtmlText = letterBody;
                }
                else
                {
                    if (_isTransmute is false)
                    {
                        try
                        {
                            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                            doc.LoadHtml(letterHtmlBody);
                            HtmlNode bodyNode = doc.DocumentNode.SelectSingleNode("/html/body");

                            if (bodyNode != null)
                            {
                                letterHtmlBody = bodyNode.InnerHtml;
                            }
                        }
                        catch (Exception ex)
                        {
                            LoggerController.WriteLog(ex?.ToString());
                        }

                        letterHtmlBody = $"<br><br><table bgcolor=\"#efefef\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"width: 100%; height: 100%;margin:0; padding:0;\">" +
                            $"<tbody><tr border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"width: 100%; height: 100%;margin:0; padding:0\">" +
                            $"<blockquote id=\"mail-app-auto-quote\" style=\"border-left-width: 3px; border-left-style: solid; border-left-color: rgb(0, 95, 249); margin: 5px 5px 5px 10px; padding: 10px 10px 10px 10px; display: inherit;\">" +
                            $"{letterHtmlBody}" +
                            $"</blockquote></tr></tbody></table>";

                        //letterHtmlBody = $"<table bgcolor=\"#efefef\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"width: 100%; height: 100%;margin:0; padding:0;\">" +
                        //    $"<tbody><tr border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"width: 100%; height: 100%;margin:0; padding:0\">" +
                        //    $"{letterHtmlBody}" +
                        //    $"</tr></tbody></table>";
                    }

                    _richLetter.HtmlText = letterHtmlBody;
                }
                
                if (isShowContactLetterForm is false && (Letter.TypeLetter != TypeLetter.Outgoing && Letter.TypeLetter != TypeLetter.Draft))
                {
                    btnLetterSender.Text = Letter.DelEmailFromText(btnLetterSender.Text, new string[] { "<", ">" });
                    _richLetter.HtmlText = Letter.DelEmailFromText(_richLetter.HtmlText);
                    _richLetter.HtmlText = Letter.DelPhoneFromText(_richLetter.HtmlText);
                }

                AddSignatureToLetter();
                AddGreetingsToLetterAsync();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(letterHtmlBody))
                {
                    _richLetter.HtmlText = letterBody;
                }
                else
                {
                    _richLetter.HtmlText = letterHtmlBody;
                }

                if (isShowContactLetterForm is false && (Letter.TypeLetter != TypeLetter.Outgoing && Letter.TypeLetter != TypeLetter.Draft))
                {
                    btnLetterSender.Text = Letter.DelEmailFromText(btnLetterSender.Text, new string[] { "<", ">" });
                    _richLetter.HtmlText = Letter.DelEmailFromText(_richLetter.HtmlText);
                    _richLetter.HtmlText = Letter.DelPhoneFromText(_richLetter.HtmlText);
                }
            }

            if (Letter.Oid > 0 && Letter.IsRead == false)
            {
                Letter.IsRead = true;
                Letter.Save();
            }

            if (_letterTemplate != null)
            {
                await UseLetterTemplate(_letterTemplate.Oid);
            }

            if (_isUseSubstitutionDictionary)
            {
                _richLetter.HtmlText = _richLetter.HtmlText?.Replace("<CUSTOMER>", Letter.Customer.ManagementNameAndPatronymicString);
                _richLetter.Text = _richLetter.Text?.Replace("<CUSTOMER>", Letter.Customer.ManagementNameAndPatronymicString);
            }

            try
            {
                var timerCallback = new TimerCallback(AutoSave);
                timer = new System.Threading.Timer(timerCallback, "auto", 30000, 30000);
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        private System.Threading.Timer timer;

        private async void AutoSave(object obj)
        {
            try
            {
                var letterOid = -1;
                Invoke((Action)delegate
                {                    
                    if (Letter?.Oid <= 0)
                    {
                        letterOid = LetterSave();
                    }
                    else if (Letter != null && Letter.Oid > 0 && Letter.TypeLetter == TypeLetter.Draft)
                    {
                        letterOid = LetterSave();
                    }                   
                });

                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"autoSaveLetter", letterOid.ToString(), true, true, 1, BVVGlobal.oApp.User);
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void AddGreetingsToLetterAsync()
        {
            try
            {
                var customerEmail = txtLetterRecipientCustomer.EditValue?.ToString();

                if (!string.IsNullOrWhiteSpace(customerEmail))
                {
                    using (var uof = new UnitOfWork())
                    {
                        var customer = await MailClients.GetCustomerEmailAsync(uof, customerEmail);

                        if (customer != null)
                        {
                            var name = customerEmail;
                            if (!string.IsNullOrWhiteSpace(customer.Name))
                            {
                                name = customer.Name;
                            }
                            else if (!string.IsNullOrWhiteSpace(customer.FullNameString))
                            {
                                name = customer.FullNameString;
                            }
                            else if (!string.IsNullOrWhiteSpace(customer.FullName))
                            {
                                var firstName = GetNameForFullName(customer.FullName);

                                if (string.IsNullOrWhiteSpace(firstName))
                                {
                                    name = customer.FullName;
                                }
                                else
                                {
                                    name = firstName;
                                }
                            }
                            
                            var message = $"{name}";
                            message += await GetTimesOfDay();
                            var messageHtml = ToHtml(message, false);
                            _richLetter.Document.InsertHtmlText(_richLetter.Document.Range.Start, messageHtml);
                        }
                    }
                }               
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void AddSignatureToLetter()
        {
            try
            {
                DatabaseConnection.User?.Staff?.Reload();
                var signature = DatabaseConnection.User?.Staff?.Signature;

                if (!string.IsNullOrWhiteSpace(signature))
                {
                    var obj = default(string);
                    obj += $"{Environment.NewLine}";
                    obj += $"{Environment.NewLine}";
                    obj += $"{Environment.NewLine}";
                    obj += $"{Environment.NewLine}";
                    obj += $"{Environment.NewLine}";
                    obj += $"----------";
                    obj += $"{Environment.NewLine}";
                    obj += signature;

                    obj = ToHtml(obj, false);

                    _richLetter.Document.InsertHtmlText(_richLetter.Document.Range.End, obj);
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }
                        
        private void btnDealVisible_Click(object sender, EventArgs e)
        {
            if (splitterItemLayoutVisible.Visibility == LayoutVisibility.Always)
            {
                splitterItemLayoutVisible.Visibility = LayoutVisibility.Never;

                splitterItemLetterAttachment.Visibility = LayoutVisibility.Never;
                layoutControlGroupLetterAttachment.Visibility = LayoutVisibility.Never;

                if (Letter.TypeLetter == TypeLetter.InBox)
                {
                    splitterItemDeal.Visibility = LayoutVisibility.Never;
                    layoutControlGroupDeal.Visibility = LayoutVisibility.Never;
                }
                else
                {
                    layoutControlGroupLetterTemplate.Visibility = LayoutVisibility.Never;
                }
            }
            else
            {
                splitterItemLayoutVisible.Visibility = LayoutVisibility.Always;
                splitterItemLetterAttachment.Visibility = LayoutVisibility.Always;
                layoutControlGroupLetterAttachment.Visibility = LayoutVisibility.Always;

                if (Letter.TypeLetter == TypeLetter.InBox)
                {
                    splitterItemDeal.Visibility = LayoutVisibility.Always;
                    layoutControlGroupDeal.Visibility = LayoutVisibility.Always;

                    splitterItemLetterAttachment.Visibility = LayoutVisibility.Never;
                }
                else
                {
                    layoutControlGroupLetterTemplate.Visibility = LayoutVisibility.Always;
                }
            }            
        }

        private void btnSaveDeal_Click(object sender, EventArgs e)
        {
            Deal.Number = txtNumber.Text;
            Deal.Name = txtName.Text;
            Deal.Description = memoDescription.Text;

            if (cmbStatusDeal.EditValue is DealStatus dealStatus)
            {
                Deal.DealStatus = dealStatus;
            }
            else
            {
                Deal.DealStatus = null;
            }

            if (btnCustomer.EditValue is Customer customer)
            {
                Deal.Customer = customer;
                Letter.Customer = customer;
                btnCustomerLetter.EditValue = customer;
            }
            else
            {
                btnCustomerLetter.EditValue = null;
                Letter.Customer = null;
                Deal.Customer = null;
            }

            if (btnStaff.EditValue is Staff staff)
            {
                Deal.Staff = staff;
            }
            else
            {
                Deal.Customer = null;
            }

            Deal.DateUpdate = DateTime.Now;

            Deal.Save();
            Letter.Save();
            layoutControlItemSaveDeal.Visibility = LayoutVisibility.Never;
        }

        private void DealEditValueChanged(object sender, EventArgs e)
        {
            var isEditValue = false;
            
            if (!txtNumber.Text.Equals(Deal.Number))
            {
                isEditValue = true;
            }

            if (!txtName.Text.Equals(Deal.Name))
            {
                isEditValue = true;
            }

            if (btnCustomer.EditValue is Customer customer && customer.Oid != Deal.Customer?.Oid)
            {
                isEditValue = true;
            }
            else
            {
                if (btnCustomer.EditValue is null && Deal.Customer?.Oid != null)
                {
                    isEditValue = true;
                }
            }

            if (btnStaff.EditValue is Staff staff && staff.Oid != Deal.Staff?.Oid)
            {
                isEditValue = true;
            }
            else
            {
                if (btnStaff.EditValue is null && Deal.Staff?.Oid != null)
                {
                    isEditValue = true;
                }
            }

            if (cmbStatusDeal.EditValue is DealStatus dealStatus && dealStatus.Oid != Deal.DealStatus?.Oid)
            {
                isEditValue = true;
            }
            else
            {
                if (cmbStatusDeal.EditValue is null && Deal.DealStatus?.Oid != null)
                {
                    isEditValue = true;
                }
            }

            if (!memoDescription.Text.Equals(Deal.Description))
            {
                isEditValue = true;
            }

            if (isEditValue)
            {
                layoutControlItemSaveDeal.Visibility = LayoutVisibility.Always;
            }
            else
            {
                layoutControlItemSaveDeal.Visibility = LayoutVisibility.Never;
            }
        }        

        private async void btnSendLetters_Click(object sender, EventArgs e)
        {
            if (Letter.TypeLetter == TypeLetter.InBox)
            {
                var topic = $"RE: {Letter.ByteToString(Letter.Topic)}";

                var form = new LetterEdit(Letter.Mailbox, Letter.LetterSenderAddress, topic, Letter.HtmlBody, answerLetter: Letter);
                form.Name = $"{form.Name}Out";
                //form.ShowDialog();
                form.XtraFormShow();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(btnLetterSender.Text))
                {
                    btnLetterSender.Focus();
                    return;
                }

                var recipient = default(string); 

                if (checkIsCustomer.Checked)
                {
                    if (string.IsNullOrWhiteSpace(txtLetterRecipientCustomer.Text))
                    {
                        XtraMessageBox.Show("Не указан отправитель.",
                                            "Отправка письма",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Asterisk);
                        txtLetterRecipientCustomer.Focus();
                        return;
                    }
                    else
                    {
                        recipient = txtLetterRecipientCustomer.EditValue.ToString();
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(txtLetterRecipient.Text))
                    {
                        XtraMessageBox.Show("Не указан отправитель.",
                                            "Отправка письма",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Asterisk);
                        txtLetterRecipient.Focus();
                        return;
                    }
                    else
                    {
                        recipient = txtLetterRecipient.Text;
                    }
                }

                if (string.IsNullOrWhiteSpace(txtTopic.Text))
                {
                    XtraMessageBox.Show("Не указана тема исходящего письма.",
                                        "Отправка письма",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Asterisk);
                    txtTopic.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(_richLetter.Text))
                {
                    XtraMessageBox.Show("Исходящее письмо - пустое.",
                                       "Отправка письма",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Asterisk);
                    _richLetter.Focus();
                    return;
                }

                if (XtraMessageBox.Show($"Отправить письмо?",
                        "Отправка письма",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question) == DialogResult.OK)
                {
                    var text = default(string);

                    if (!string.IsNullOrWhiteSpace(_richLetter.HtmlText))
                    {
                        text = _richLetter.HtmlText;
                    }
                    else
                    {
                        text = _richLetter.Text;
                    }

                    var mailbox = default(Mailbox);
                    var mailingAddressName = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.MailboxForSending), "");

                    if (string.IsNullOrWhiteSpace(mailingAddressName))
                    {
                        mailbox = _session.FindObject<Mailbox>(new BinaryOperator(nameof(_mailbox.StateMailbox), StateMailbox.SendingLetters));
                    }
                    
                    if (mailbox is null)
                    {
                        mailbox = _session.FindObject<Mailbox>(new BinaryOperator(nameof(_mailbox.MailingAddress), mailingAddressName));
                    }
                    
                    if (mailbox is null && _mailbox != null)
                    {
                        mailbox = _mailbox;
                    }

                    if (mailbox is null)
                    {
                        XtraMessageBox.Show("Не удалось определить адрес отправителя",
                                               "Отправка письма",
                                               MessageBoxButtons.OK,
                                               MessageBoxIcon.Asterisk);
                        btnLetterSender.Focus();
                        return;
                    }

                    var addressCopy = GetEmailAddress($"{txtCopy.Text}");

                    try
                    {
                        Letter.SendMailKit(recipient,
                            txtTopic.Text,
                            text,
                            mailbox.MailingAddress,
                            BVVGlobal.oApp.User,
                            Mailbox.Decrypt(mailbox.Password),
                            mailbox.MailboxSetup.OutgoingMailServerSMTP,
                            mailbox.MailboxSetup.PortSMTP,
                            Letter.LetterAttachments.ToList(),
                            recipientsCC: addressCopy);

                        Letter.User = _session.GetObjectByKey<User>(DatabaseConnection.User.Oid);

                        if (addressCopy.Count > 0)
                        {
                            Letter.CopiesAddresses = string.Join(";", addressCopy);
                        }
                    }
                    catch (Exception ex)
                    {
                        LoggerController.WriteLog(ex?.ToString());
                    }

                    //TODO: отправка копии письма себе.
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(mailbox?.MailingAddressCopy))
                        {
                            Letter.SendMailKit(mailbox?.MailingAddressCopy,
                            //$"Копия [{recipient}] ({txtTopic.Text})",
                            $"{txtTopic.Text}",
                            text,
                            mailbox.MailingAddress,
                            BVVGlobal.oApp.User,
                            Mailbox.Decrypt(mailbox.Password),
                            mailbox.MailboxSetup.OutgoingMailServerSMTP,
                            mailbox.MailboxSetup.PortSMTP,
                            Letter.LetterAttachments.ToList());
                        }                        
                    }
                    catch (Exception ex)
                    {
                        LoggerController.WriteLog(ex?.ToString());
                    }

                    if (Customer != null)
                    {
                        Letter.Customer = Customer;
                    }

                    if (Letter.TypeLetter == TypeLetter.Draft)
                    {
                        Letter.TypeLetter = TypeLetter.Outgoing;
                    }

                    Letter.LetterRecipient = txtLetterRecipientCustomer.Text;
                    Letter.LetterRecipientAddress = recipient;

                    Letter.SetMailBox(_mailbox);
                    Letter.LetterSender = $"{_mailbox.Login} <{_mailbox.MailingAddress}>";
                    Letter.LetterSenderAddress = _mailbox.MailingAddress;

                    Letter.TextBody = Letter.StringToByte(_richLetter.Text);
                    Letter.HtmlBody = Letter.StringToByte(_richLetter.HtmlText);
                    Letter.Topic = Letter.StringToByte(txtTopic.Text);
                    Letter.DateCreate = DateTime.Now;
                    Letter.DateReceiving = DateTime.Now;

                    //TODO: где-то не правильно встает сессия клиента, временное решение, убрать в дальнейшем.
                    Letter.Customer = _session.GetObjectByKey<Customer>(Letter.Customer?.Oid);

                    Letter.Save();

                    id = Letter.Oid;
                    flagSave = true;

                    await SaveAnswerLetter();

                    if (Greetings != null)
                    {
                        await GreetingsSave(Letter.Session);
                    }

                    if (_isUseSubstitutionDictionary)
                    {
                        var customer = Letter.Customer;
                        if (customer != null)
                        {
                            var chronicleCustomer = new ChronicleCustomer(customer.Session)
                            {
                                Act = Act.HAPPY_BIRTHDAY,
                                Date = DateTime.Now,
                                Description = $"Пользователь [{customer.Session.GetObjectByKey<User>(DatabaseConnection.User?.Oid)}] отправил поздравление клиенту.",
                                User = customer.Session.GetObjectByKey<User>(DatabaseConnection.User?.Oid),
                                Customer = customer
                            };
                            chronicleCustomer.Save();
                        }
                    }

                    Close();
                }
            }
        }

        private async System.Threading.Tasks.Task SaveAnswerLetter()
        {
            try
            {
                if (_answerLetter != null)
                {
                    using (var uow = new UnitOfWork())
                    {
                        var letter = await uow.GetObjectByKeyAsync<Letter>(_answerLetter.Oid);
                        if (letter != null)
                        {
                            letter.IsRead = true;
                            letter.IsReplySent = true;
                            letter.AnswerLetter = await uow.GetObjectByKeyAsync<Letter>(id);
                            letter.Save();
                        }

                        await uow.CommitTransactionAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        public static List<string> GetEmailAddress(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return default;
            }
            
            var result = new List<string>();
            var pattern = "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$";
            var split = line.Split(' ', ';', ',');

            foreach (var splitItem in split)
            {
                var email = splitItem?.Trim()?.ToLower();
                if (Regex.IsMatch(email, pattern))
                {
                    result.Add(email);
                }
            }
            
            return result;
        }

        private void txtLetterSender_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            var mailboxOid = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.Mailbox, -1);
            var mailbox = _session.GetObjectByKey<Mailbox>(mailboxOid);

            if (mailbox != null)
            {
                buttonEdit.EditValue = $"{mailbox.Login} <{mailbox.MailingAddress}>";
                _mailbox = mailbox;
            }
        }

        private void txtLetterRecipient_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var lookUpEdit = sender as LookUpEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                UpdateEmails();
                lookUpEdit.EditValue = null;
                return;
            }
            else if (e.Button.Kind == ButtonPredefines.Ellipsis)
            {
                var customerOid = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.Customer, -1);
                var customer = _session.GetObjectByKey<Customer>(customerOid);

                if (customer != null)
                {
                    UpdateEmails(customer);
                    //foreach (var email in customer.CustomerEmails)
                    //{
                    //    if (!string.IsNullOrWhiteSpace(email.Email) && lookUpEdit.Properties.Items.Contains(email.Email) is false)
                    //    {
                    //        lookUpEdit.Properties.Items.Add(email.Email);
                    //    }

                    //    if (!string.IsNullOrWhiteSpace(email.Email2) && lookUpEdit.Properties.Items.Contains(email.Email2) is false)
                    //    {
                    //        lookUpEdit.Properties.Items.Add(email.Email2);
                    //    }
                    //}

                    lookUpEdit.EditValue = customer.Email;
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (_richLetter.IsPrintingAvailable)
            {
                _richLetter.ShowPrintDialog();
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            _richLetter.ShowSearchForm();
        }

        /// <summary>
        /// Добавление вложения к письму.
        /// </summary>
        private async void AddLetterAttachment()
        {
            using (var ofd = new XtraOpenFileDialog() { KeepPosition = false, Multiselect = true })
            {
                var myFolderPath = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.MyFolderPath), user: BVVGlobal.oApp.User);

                if (!string.IsNullOrWhiteSpace(myFolderPath))
                {
                    try
                    {                       
                        ofd.CustomPlaces.Add($@"{myFolderPath}");
                    }
                    catch (Exception ex)
                    {
                        LoggerController.WriteLog(ex?.ToString());
                        ofd.CustomPlaces.Clear();
                    }
                }
                
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (var fileName in ofd.FileNames)
                    {
                        Letter.LetterAttachments.Add(new LetterAttachment(_session, fileName));
                    }

                    //if (splitContainerControlLetter.PanelVisibility == SplitPanelVisibility.Panel2)
                    //{
                    //    splitContainerControlLetter.PanelVisibility = SplitPanelVisibility.Both;
                    //    splitContainerControlLetter.SplitterPosition = splitContainerControlLetter.Width / 4;
                    //}
                }
            }

            GetLetterAttachments(Letter);
        }

        private void btnAddLetterAttachment_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddLetterAttachment();
        }

        private void btnAddAttachment_Click(object sender, EventArgs e)
        {
            AddLetterAttachment();
        }

        private void btnSaveLetterAttachment_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewLetterAttachment.IsEmpty)
            {
                return;
            }

            var letterAttachment = gridViewLetterAttachment.GetRow(gridViewLetterAttachment.FocusedRowHandle) as LetterAttachment;
            if (letterAttachment != null)
            {
                using (var fbd = new XtraFolderBrowserDialog() { Description = "Сохранение файла" })
                {
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        var isSaveFile = letterAttachment.WriteFile(fbd.SelectedPath);

                        if (isSaveFile)
                        {
                            if (XtraMessageBox.Show("Файл успешно сохранен. Открыть директорию?", string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                Process.Start("explorer", fbd.SelectedPath);
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show("Ошибка сохранения файла, возможно такой файл уже имеется в директории", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void btnDelLetterAttachment_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewLetterAttachment.IsEmpty)
            {
                return;
            }

            var letterAttachment = gridViewLetterAttachment.GetRow(gridViewLetterAttachment.FocusedRowHandle) as LetterAttachment;
            if (letterAttachment != null)
            {
                letterAttachment.Delete();
            }
        }
        
        private void OpenLetterAttachment()
        {
            if (gridViewLetterAttachment.IsEmpty)
            {
                return;
            }

            var letterAttachment = gridViewLetterAttachment.GetRow(gridViewLetterAttachment.FocusedRowHandle) as LetterAttachment;
            if (letterAttachment != null)
            {
                var fileExtension = default(string);
                if (string.IsNullOrWhiteSpace(letterAttachment.FileExtension))
                {
                    fileExtension = letterAttachment.FullFileNameExtension;
                }
                else
                {
                    fileExtension = letterAttachment.FileExtension;
                }
                
                var tempPath = Path.GetTempFileName().Replace(".tmp", $"{fileExtension}");
                var isSaveFile = letterAttachment.WriteFile(tempPath, true);

                if (isSaveFile)
                {
                    Process process = new Process();
                    ProcessStartInfo processStartInfo = new ProcessStartInfo();
                    processStartInfo.UseShellExecute = true;
                    processStartInfo.FileName = $"{tempPath}";
                    process.StartInfo = processStartInfo;

                    try
                    {
                        process.Start();
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show(Ex.Message);
                    }
                }
            }
        }

        private void btnOpenLetterAttachment_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenLetterAttachment();
        }

        private void gridViewLetterAttachment_DoubleClick(object sender, EventArgs e)
        {
            OpenLetterAttachment();
        }

        private async System.Threading.Tasks.Task UseLetterTemplate(int letterTemplateOid)
        {
            var letterTemplate = await _session.GetObjectByKeyAsync<LetterTemplate>(letterTemplateOid);

            if (letterTemplate != null)
            {
                var message = default(string);
                
                if (letterTemplate.HtmlBody != null)
                {
                    message = Letter.ByteToString(letterTemplate.HtmlBody);
                }
                else if (letterTemplate.TextBody != null)
                {
                    message = Letter.ByteToString(letterTemplate.TextBody);
                }

                if (!string.IsNullOrWhiteSpace(message))
                {
                    if (string.IsNullOrWhiteSpace(firstCustomerEmail))
                    {
                        _richLetter.Document.InsertHtmlText(_richLetter.Document.Range.Start, message);
                        htmlBodyLetter = _richLetter.HtmlText;
                    }
                    else
                    {
                        var range = _richLetter.Document.FindAll(firstCustomerEmail?.Replace("\r\n", ""), SearchOptions.CaseSensitive)?.FirstOrDefault();
                        if (range != null)
                        {
                            _richLetter.Document.Replace(range, "");
                        }
                        _richLetter.Document.InsertHtmlText(_richLetter.Document.Range.Start, message);
                        var messageHtml = ToHtml(firstCustomerEmail, false);
                        _richLetter.Document.InsertHtmlText(_richLetter.Document.Range.Start, messageHtml);
                    }                    
                }

                if (XtraMessageBox.Show("Добавить подпись пользователя в конец письма?",
                                        "Добавление подписи пользователя",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {
                    AddSignatureToLetter();
                }                
            }
        }

        private void checkIsCustomer_CheckedChanged(object sender, EventArgs e)
        {
            var checkEdit = sender as CheckEdit;
            var isVisible = default(bool);

            if (checkEdit.Checked)
            {
                isVisible = true;
                txtLetterRecipient.EditValue = null;
                UpdateEmails();
            }
            else
            {
                Customer = null;
                txtLetterRecipientCustomer.Properties.DataSource = null;
                txtLetterRecipientCustomer.Properties.SearchMode = SearchMode.OnlyInPopup;
                
                isVisible = false;
            }

            txtLetterRecipientCustomer.EditValue = null;
            foreach (EditorButton buttonEdit in txtLetterRecipientCustomer.Properties.Buttons)
            {
                buttonEdit.Visible = isVisible;
            }

            if (isVisible)
            {
                layoutControlItemLetterRecipient.Visibility = LayoutVisibility.Never;
                layoutControlItemLetterRecipientCustomer.Visibility = LayoutVisibility.Always;
            }
            else
            {
                layoutControlItemLetterRecipient.Visibility = LayoutVisibility.Always;
                layoutControlItemLetterRecipientCustomer.Visibility = LayoutVisibility.Never;
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
            
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(_session, buttonEdit, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);

            if (buttonEdit.EditValue is Customer customer)
            {
                btnCustomerLetter.EditValue = customer;

                if (Letter != null)
                {
                    Letter.Customer = customer;
                    Letter.Save();
                    txtResponsible.Text = Letter.Customer?.AccountantResponsible?.ToString() ?? string.Empty;                    
                }

                if (Deal != null)
                {
                    btnSaveDeal_Click(null, null);
                }
            }
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

        private void btnCustomerLetter_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                Letter.Customer = null;
                Letter.Save();
                buttonEdit.EditValue = null;
                txtResponsible.Text = Letter.Customer?.AccountantResponsible?.ToString() ?? string.Empty;

                if (Deal != null)
                {
                    Deal.Customer = null;
                    Deal.Save();
                    btnCustomer.EditValue = null;
                }
                
                return;
            }

            if (e.Button.Kind == ButtonPredefines.Search)
            {
                var customerSearch = Letter.GetCustomer(_session, Letter.LetterSenderAddress);
                if (customerSearch != null)
                {
                    Letter.Customer = customerSearch;
                    Letter.Save();
                }
                buttonEdit.EditValue = customerSearch;
                txtResponsible.Text = Letter.Customer?.AccountantResponsible?.ToString() ?? string.Empty;
                
                if (Deal != null)
                {
                    Deal.Customer = customerSearch;
                    Deal.Save();
                    btnCustomer.EditValue = customerSearch;
                }

                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(_session, buttonEdit, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);

            if (buttonEdit.EditValue is Customer customer)
            {
                Letter.Customer = customer;
                Letter.Save();

                if (Deal != null)
                {
                    Deal.Customer = customer;
                    Deal.Save();
                    btnCustomer.EditValue = customer;
                }

                if (Letter.Deal != null)
                {
                    btnSaveDeal_Click(null, null);
                }
            }
            txtResponsible.Text = Letter.Customer?.AccountantResponsible?.ToString() ?? string.Empty;
        }

        private void LetterEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Letter?.Oid <= 0)
            {
                if (XtraMessageBox.Show("Сохранить сообщение в черновик?",
                                        "Сохранение в черновик",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {
                    LetterSave();
                };
            }
            else if (Letter != null && Letter.Oid > 0 && Letter.TypeLetter == TypeLetter.Draft)
            {
                LetterSave();
            }
            else if (Letter != null && Letter.Oid > 0 && Letter.TypeLetter == TypeLetter.Outgoing && Letter.LetterAttachments?.Count > 0)
            {
                if (flagSave is false)
                {
                    if (XtraMessageBox.Show("Сохранить сообщение в черновик?",
                                            "Сохранение в черновик",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        LetterSave();
                    };
                }                
            }
        }

        private int LetterSave()
        {
            try
            {
                var letterRecipient = default(string);

                if (Customer != null)
                {
                    Letter.Customer = Customer;
                    letterRecipient = txtLetterRecipientCustomer.Text;
                }
                else
                {
                    letterRecipient = txtLetterRecipient.Text;
                }

                Letter.TypeLetter = TypeLetter.Draft;
                Letter.LetterRecipient = letterRecipient;
                Letter.LetterRecipientAddress = txtLetterRecipientCustomer.Text;

                if (_mailbox != null)
                {
                    Letter.SetMailBox(_mailbox);
                    Letter.LetterSender = $"{_mailbox.Login} <{_mailbox.MailingAddress}>";
                    Letter.LetterSenderAddress = _mailbox.MailingAddress;
                }

                //TODO: где-то не правильно встает сессия клиента, временное решение, убрать в дальнейшем.
                Letter.Customer = _session.GetObjectByKey<Customer>(Letter.Customer?.Oid);                
                
                Letter.TextBody = Letter.StringToByte(_richLetter.Text);
                Letter.HtmlBody = Letter.StringToByte(_richLetter.HtmlText);
                Letter.Topic = Letter.StringToByte(txtTopic.Text);
                Letter.DateCreate = DateTime.Now;
                Letter.DateReceiving = DateTime.Now;

                Letter.Save();

                return Letter.Oid;
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }

            return -1;
        }

        private void btnTransmute_Click(object sender, EventArgs e)
        {
            if (Letter != null)
            {
                if (Letter.TypeLetter == TypeLetter.InBox)
                {
                    var topic = $"[TRANSMUTE]: {Letter.ByteToString(Letter.Topic)}";

                    var form = new LetterEdit(Letter.Mailbox, Letter.LetterSenderAddress, topic, Letter.HtmlBody, true, Letter.LetterAttachments?.ToList());
                    form.Name = $"{form.Name}_TRANSMUTE";
                    //form.ShowDialog();
                    form.XtraFormShow();
                }
            }            
        }

        private void Attachment_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (Letter.TypeLetter != TypeLetter.InBox)
                {
                    var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    foreach (var fileName in files)
                    {
                        Letter.LetterAttachments.Add(new LetterAttachment(_session, fileName));
                    }

                    GetLetterAttachments(Letter);
                }                    
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }            
        }

        private void Attachment_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(
                "Вы действительно хотите удалить письмо?",
                "Удаление письма",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    if (Letter != null)
                    {
                        if (Letter.TypeLetter == TypeLetter.Basket)
                        {
                            if (Letter.Deal != null)
                            {
                                Letter.Deal.MessageDeleteLetter = $"Письмо [{Letter?.ToString()}] удалено пользователем {DatabaseConnection.User} - [{DateTime.Now.ToShortDateString()}]";
                                Letter.Deal.Letter = null;
                                Letter.Deal.Save();
                            }
                            Letter.Delete();
                        }
                        else
                        {
                            Letter.TypeLetter = TypeLetter.Basket;
                            Letter.Save();
                        }
                    }                    
                }
                catch (Exception ex)
                {
                    LoggerController.WriteLog(ex?.ToString());
                }

                Close();
            }
        }

        private void btnSaveObjLetterAttachment_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewLetterAttachment.IsEmpty)
            {
                return;
            }

            var letterAttachment = gridViewLetterAttachment.GetRow(gridViewLetterAttachment.FocusedRowHandle) as LetterAttachment;
            if (letterAttachment != null)
            {
                try
                {
                    using (var ofd = new XtraSaveFileDialog() { FileName = letterAttachment.FullFileName })
                    {
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            SaveAttachment(letterAttachment, ofd.FileName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.ToString());
                }                
            }
        }

        private void barBtnSaveAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridControlLetterAttachment.DataSource is IList<LetterAttachment> attachments)
            {
                var count = 0;
                using (var fbd = new FolderBrowserDialog())
                {
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        var folderPath = fbd.SelectedPath;
                        foreach (var letterAttachment in attachments)
                        {
                            try
                            {
                                var filePath = Path.Combine(folderPath, letterAttachment.FullFileName);
                                SaveAttachment(letterAttachment, filePath);
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.ToString());
                            }
                            count++;
                        }

                        XtraMessageBox.Show($"Сохранено файлов: {count}", "Информационное сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }            
        }

        private static void SaveAttachment(LetterAttachment letterAttachment, string fileName)
        {
            System.IO.File.WriteAllBytes(fileName, letterAttachment.FileByte);
        }

        private string htmlBodyLetter;
        private string firstCustomerEmail;

        private async void txtLetterRecipientCustomer_EditValueChanged(object sender, EventArgs e)
        {
            var customerEmail = txtLetterRecipientCustomer.EditValue?.ToString();

            if (!string.IsNullOrWhiteSpace(customerEmail))
            {
                using (var uof = new UnitOfWork())
                {
                    var customer = await MailClients.GetCustomerEmailAsync(uof, customerEmail);

                    if (customer != null)
                    {
                        var name = customerEmail;
                        if (!string.IsNullOrWhiteSpace(customer.Name))
                        {
                            name = customer.Name;
                        }
                        else if (!string.IsNullOrWhiteSpace(customer.FullNameString))
                        {
                            name = customer.FullNameString;
                        }
                        else if (!string.IsNullOrWhiteSpace(customer.FullName))
                        {
                            var firstName = GetNameForFullName(customer.FullName);

                            if (string.IsNullOrWhiteSpace(firstName))
                            {
                                name = customer.FullName;
                            }
                            else
                            {
                                name = firstName;
                            }                           
                        }

                        if (string.IsNullOrWhiteSpace(htmlBodyLetter))
                        {
                            htmlBodyLetter = _richLetter.HtmlText;
                        }

                        var message = $"{name}";
                        message += await GetTimesOfDay();

                        var messageHtml = ToHtml(message, false);

                        if (string.IsNullOrWhiteSpace(firstCustomerEmail))
                        {
                            firstCustomerEmail = message;
                            _richLetter.HtmlText = htmlBodyLetter;
                            _richLetter.Document.InsertHtmlText(_richLetter.Document.Range.Start, messageHtml);
                        }
                        else
                        {
                            var range = _richLetter.Document.FindAll(firstCustomerEmail?.Replace("\r\n", ""), SearchOptions.CaseSensitive)?.FirstOrDefault();
                            if (range != null)
                            {
                                _richLetter.Document.Replace(range, message?.Replace("\r\n", ""));
                            }
                            else
                            {
                                _richLetter.HtmlText = htmlBodyLetter;
                                _richLetter.Document.InsertHtmlText(_richLetter.Document.Range.Start, messageHtml);
                            }

                            firstCustomerEmail = message;
                        }
                    }
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(htmlBodyLetter))
                {
                    _richLetter.HtmlText = htmlBodyLetter;
                }
            }
        }

        private string GetNameForFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return default;
            }

            var splits = fullName.Split(' ');
            if (splits.Length == 3)
            {
                return splits[1];
            }

            return default;
        }

        private Greetings Greetings { get; set; }

        private async System.Threading.Tasks.Task GreetingsSave(Session session)
        {
            new Greetings(session)
            {
                Date = Greetings.Date,
                Email = Greetings.Email,
                User = await session.GetObjectByKeyAsync<User>(Greetings.User.Oid)
            }
            .Save();
        }

        /// <summary>
        /// Get times of day.
        /// </summary>
        /// <param name="dateTime">Current date time</param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<string> GetTimesOfDay(DateTime? dateTime = null)
        {
            var date = dateTime ?? DateTime.Now;
            
            var recipient = default(string);
            
            if (checkIsCustomer.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtLetterRecipientCustomer.Text))
                {
                }
                else
                {
                    recipient = txtLetterRecipientCustomer.EditValue.ToString();
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtLetterRecipient.Text))
                {
                }
                else
                {
                    recipient = txtLetterRecipient.Text;
                }
            }
            
            if (!string.IsNullOrWhiteSpace(recipient))
            {
                var greeting = new Greetings(_session)
                {
                    Date = date.Date,
                    User = DatabaseConnection.User,
                    Email = recipient
                };

                if (greeting.User != null)
                {
                    var currentGreetings = await new XPQuery<Greetings>(_session)
                    .Where(w => w.Date == greeting.Date
                            && w.User != null && w.User.Oid == greeting.User.Oid
                            && w.Email == greeting.Email)
                    .FirstOrDefaultAsync();
                    if (currentGreetings is null)
                    {
                        Greetings = greeting;
                    }
                    else
                    {
                        return default;
                    }
                }
            }
            
            if (date.Hour >= 0 && date.Hour <= 11 || (date.Hour <= 11 && date.Minute <= 59))
            {
                return $", доброе утро{Environment.NewLine}";
            }
            else if (date.Hour >= 12 && date.Hour <= 16 || (date.Hour <= 16 && date.Minute <= 59))
            {
                return $", добрый день{Environment.NewLine}";
            }
            else
            {
                return $", добрый вечер{Environment.NewLine}";
            }
        }

        private void cmbStatusDeal_SelectedValueChanged(object sender, EventArgs e)
        {
            if (sender is ComboBoxEdit cmb)
            {
                if (cmb.EditValue is DealStatus dealStatus)
                {
                    if (!string.IsNullOrWhiteSpace(dealStatus.Color))
                    {
                        var color = ColorTranslator.FromHtml(dealStatus.Color);

                        cmb.BackColor = color;
                        layoutControlItemStatusDeal.AppearanceItemCaption.BackColor = color;
                    }
                }
                else
                {
                    cmb.BackColor = default;
                    layoutControlItemStatusDeal.AppearanceItemCaption.BackColor = default;
                }
            }
        }

        private async void btnGetOfferLetterAttachment_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (MailClients.ListMailClients?.Count() <= 0)
                {
                    using (var uof = new UnitOfWork())
                    {
                        var groupOperatorStateMailbox = new GroupOperator(GroupOperatorType.Or);

                        var criteriaStateMailboxWorking = new BinaryOperator(nameof(_mailbox.StateMailbox), StateMailbox.Working);
                        groupOperatorStateMailbox.Operands.Add(criteriaStateMailboxWorking);

                        var criteriaStateMailboxReceivingLetters = new BinaryOperator(nameof(_mailbox.StateMailbox), StateMailbox.ReceivingLetters);
                        groupOperatorStateMailbox.Operands.Add(criteriaStateMailboxReceivingLetters);

                        var xpcollectionMailbox = new XPCollection<Mailbox>(uof, groupOperatorStateMailbox);
                        MailClients.FillingListMailClients(xpcollectionMailbox);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }

            try
            {
                var mailClient = MailClients.ListMailClients?.FirstOrDefault(f => f.Mailbox != null && f.Mailbox.Oid == Letter?.Mailbox?.Oid);
                if (mailClient != null)
                {
                    gridControlLetterAttachment.Enabled = false;
                    gridViewLetterAttachment.ShowLoadingPanel();
                    await mailClient.GetAttachments(Letter);
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
            finally
            {
                gridViewLetterAttachment.HideLoadingPanel();
                gridControlLetterAttachment.Enabled = true;
                GetLetterAttachments(Letter);
                //Letter?.LetterAttachments?.Reload();
            }
        }

        private async void gridViewLetterAttachment_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (e.MenuType != GridMenuType.Row)
                    {
                        
                    }
                    else
                    {
                        
                    }

                    if (Letter.TypeLetter == TypeLetter.InBox)
                    {
                        btnAddLetterAttachment.Visibility = BarItemVisibility.Never;

                        var currentUserOid = DatabaseConnection.User?.Oid ?? -1;
                        var user = await new XPQuery<User>(_session)
                            ?.FirstOrDefaultAsync(f => f != null && f.Oid == currentUserOid);
                        if (user != null && user.flagAdministrator)
                        {
                            btnDelLetterAttachment.Visibility = BarItemVisibility.Always;
                        }
                        else
                        {

                            btnDelLetterAttachment.Visibility = BarItemVisibility.Never;
                        }
                        
                        btnSaveLetterAttachment.Enabled = true;
                    }
                    else
                    {
                        btnAddLetterAttachment.Visibility = BarItemVisibility.Always;
                        btnDelLetterAttachment.Visibility = BarItemVisibility.Always;
                        btnSaveLetterAttachment.Enabled = false;
                    }

                    if (Letter.UniqueId is null)
                    {
                        btnGetOfferLetterAttachment.Visibility = BarItemVisibility.Never;
                    }
                    else
                    {
                        btnGetOfferLetterAttachment.Visibility = BarItemVisibility.Always;
                    }

                    popupMenuLetterAttachment.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void gridViewLetterAttachment_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (gridView.GetRow(e.RowHandle) is LetterAttachment letterAttachment)
                {
                    if (letterAttachment.Letter?.Oid != Letter?.Oid)
                    {
                        e.Appearance.BackColor = Color.LightYellow;
                    }
                    else
                    {
                        e.Appearance.BackColor = Color.LightGreen;
                    }
                }
            }
        }
    }
}