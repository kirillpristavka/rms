using DevExpress.Data.Filtering;
using DevExpress.Office.Utils;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using RMS.Core.Controller.Print;
using RMS.Core.Enumerator;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle;
using RMS.Core.Model.InfoContract;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using RMS.Core.Reports.OpenXmlHandler;
using RMS.UI.Control;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms.Directories
{
    public partial class ContractEdit : XtraForm
    {
        private Session Session { get; }
        public Contract Contract { get; }
        private Customer Customer { get; }

        private ContractEdit()
        {
            InitializeComponent();
            tabbedControlGroup.Owner.OptionsFocus.AllowFocusTabbedGroups = false;
        }

        public ContractEdit(Customer customer) : this()
        {
            Session = customer.Session;
            Customer = customer;
            Contract = new Contract(Session) { Customer = customer };
        }

        public ContractEdit(Contract contract) : this()
        {
            Contract = contract;
            Session = contract.Session;
            Customer = contract.Customer;
        }

        private bool isEditContractForm = false;
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
                            isEditContractForm = accessRights.IsEditContractForm;
                        }

                        btnSave.Enabled = isEditContractForm;

                        CustomerEdit.CloseButtons(btnCustomer, isEditContractForm);
                        CustomerEdit.CloseButtons(btnPlateTemplate, isEditContractForm);
                        CustomerEdit.CloseButtons(btnContractStatus, isEditContractForm);
                        CustomerEdit.CloseButtons(btnOrganization, isEditContractForm);
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void ContractEdit_Load(object sender, EventArgs e)
        {
            await SetAccessRights();

            txtNumber.EditValue = Contract.Number;
            txtPrefix.EditValue = Contract.Prefix;
            txtYear.EditValue = Contract.Year;
            txtTown.EditValue = Contract.Town ?? "Санкт-Петербург";
            btnContractStatus.EditValue = Contract.ContractStatus;
            btnOrganization.EditValue = Contract.Organization;
            if (Contract.Organization != null)
            {
                txtPrefix.ReadOnly = true;
            }
            else
            {
                txtPrefix.ReadOnly = false;
            }
            btnCustomer.EditValue = Customer;
            if (Customer != null)
            {
                txtManagement.EditValue = Customer.ManagementString;
                txtManagementPosition.EditValue = Customer.ManagementPositionString;
            }
                        
            txtDate.EditValue = Contract.Date;
            txtDateTermination.EditValue = Contract.DateTermination;
            dateSince.EditValue = Contract.DateSince;
            dateTo.EditValue = Contract.DateTo;

            btnPlateTemplate.EditValue = Contract.PlateTemplate;
            btnPlateTemplate1.EditValue = Contract.PlateTemplate1;
            btnPlateTemplate2.EditValue = Contract.PlateTemplate2;
            btnPlateTemplate3.EditValue = Contract.PlateTemplate3;

            gridControlGeneratedFiles.DataSource = Contract.ContractFiles;

            gridViewGeneratedFiles.OptionsSelection.CheckBoxSelectorColumnWidth = 35;

            if (gridViewGeneratedFiles.Columns[nameof(ContractFile.Oid)] != null)
            {
                gridViewGeneratedFiles.Columns[nameof(ContractFile.Oid)].Visible = false;
                gridViewGeneratedFiles.Columns[nameof(ContractFile.Oid)].Width = 18;
                gridViewGeneratedFiles.Columns[nameof(ContractFile.Oid)].OptionsColumn.FixedWidth = true;
            }
            if (gridViewGeneratedFiles.Columns[nameof(ContractFile.DateCreate)] != null)
            {
                gridViewGeneratedFiles.Columns[nameof(ContractFile.DateCreate)].Width = 155;
                gridViewGeneratedFiles.Columns[nameof(ContractFile.DateCreate)].OptionsColumn.FixedWidth = true;

                gridViewGeneratedFiles.Columns[nameof(ContractFile.DateCreate)].DisplayFormat.FormatType = FormatType.DateTime;
                gridViewGeneratedFiles.Columns[nameof(ContractFile.DateCreate)].DisplayFormat.FormatString = $"dd.MM.yyyy HH:mm:ss";

                gridViewGeneratedFiles.Columns[nameof(ContractFile.DateCreate)].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                gridViewGeneratedFiles.Columns[nameof(ContractFile.DateCreate)].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            }

            memoComment.EditValue = Contract.Comment;

            gridControlAttachment.DataSource = Contract.ContractAttachments;
            if (gridViewAttachment.Columns[nameof(ContractAttachment.Oid)] != null)
            {
                gridViewAttachment.Columns[nameof(ContractAttachment.Oid)].Visible = false;
                gridViewAttachment.Columns[nameof(ContractAttachment.Oid)].Width = 18;
                gridViewAttachment.Columns[nameof(ContractAttachment.Oid)].OptionsColumn.FixedWidth = true;
            }

            gridControlSupplementaryAgreement.DataSource = Contract.SupplementaryAgreements;
            if (gridViewSupplementaryAgreement.Columns[nameof(Customer.Oid)] != null)
            {
                gridViewSupplementaryAgreement.Columns[nameof(Customer.Oid)].Visible = false;
                gridViewSupplementaryAgreement.Columns[nameof(Customer.Oid)].Width = 18;
                gridViewSupplementaryAgreement.Columns[nameof(Customer.Oid)].OptionsColumn.FixedWidth = true;
            }

            gridControlChronicle.DataSource = Contract.ChronicleContract;
            if (gridViewChronicle.Columns[nameof(ChronicleContract.Oid)] != null)
            {
                gridViewChronicle.Columns[nameof(ChronicleContract.Oid)].Visible = false;
                gridViewChronicle.Columns[nameof(ChronicleContract.Oid)].Width = 18;
                gridViewChronicle.Columns[nameof(ChronicleContract.Oid)].OptionsColumn.FixedWidth = true;
            }

            var imageCollection = new ImageCollectionStatus();

            if (gridViewChronicle.Columns[nameof(ChronicleContract.IsNotificationSent)] != null)
            {
                RepositoryItemImageComboBox imgIsNotificationSent = gridControlChronicle.RepositoryItems.Add(nameof(ImageComboBoxEdit)) as RepositoryItemImageComboBox;
                imgIsNotificationSent.SmallImages = imageCollection.imageCollection;
                imgIsNotificationSent.Items.Add(new ImageComboBoxItem() { Value = true, ImageIndex = 15 });
                imgIsNotificationSent.Items.Add(new ImageComboBoxItem() { Value = false, ImageIndex = -1 });

                imgIsNotificationSent.GlyphAlignment = HorzAlignment.Center;
                gridViewChronicle.Columns[nameof(ChronicleContract.IsNotificationSent)].ColumnEdit = imgIsNotificationSent;
                gridViewChronicle.Columns[nameof(ChronicleContract.IsNotificationSent)].Width = 18;
                gridViewChronicle.Columns[nameof(ChronicleContract.IsNotificationSent)].OptionsColumn.FixedWidth = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ContractSave();
            Close();
        }

        private async void ContractSave()
        {
            Contract.Prefix = txtPrefix.Text;
            Contract.Number = txtNumber.Text;
            Contract.Date = txtDate.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(txtDate.EditValue);
            Contract.DateTermination = txtDateTermination.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(txtDateTermination.EditValue);
            Contract.Comment = memoComment.Text;

            if (btnCustomer.EditValue is Customer customer)
            {
                customer?.Reload();
                Contract.Customer = customer;
            }
            else
            {
                Contract.Customer = null;
            }

            var contractStatus = default(ContractStatus);
            var sinceDate = default(DateTime?);
            var toDate = default(DateTime?);

            if (btnContractStatus.EditValue is ContractStatus)
            {
                contractStatus = (ContractStatus)btnContractStatus.EditValue;
            }

            if (dateSince.EditValue is DateTime)
            {
                sinceDate = Convert.ToDateTime(dateSince.EditValue);
            }

            if (dateTo.EditValue is DateTime)
            {
                toDate = Convert.ToDateTime(dateTo.EditValue);
            }

            if (Contract.Oid > 0 && Contract.ContractStatus != contractStatus)
            {
                var IsNotificationSent = false;

                if (XtraMessageBox.Show("Уведомить клиента о изменении статуса договора?",
                                        "Уведомление клиента",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(Contract.Customer?.Email))
                    {
                        var mailbox = default(Mailbox);
                        var mailingAddressName = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.MailboxForSending), "");

                        if (string.IsNullOrWhiteSpace(mailingAddressName))
                        {
                            mailbox = Session.FindObject<Mailbox>(new BinaryOperator(nameof(Mailbox.StateMailbox), StateMailbox.SendingLetters));
                        }
                        else
                        {
                            mailbox = Session.FindObject<Mailbox>(new BinaryOperator(nameof(Mailbox.MailingAddress), mailingAddressName));
                        }

                        var letter = new Letter(Session)
                        {
                            IsRead = true,
                            LetterSender = mailbox.MailingAddress,
                            LetterSenderAddress = mailbox.MailingAddress,
                            Customer = Contract.Customer,
                            Topic = Letter.StringToByte($"Уведомление о состоянии договора {Contract}"),
                            HtmlBody = Letter.StringToByte($"Уважаемый клиент! Статус вашего договора был изменен с {Contract.ContractStatus} на {contractStatus}. С Уважением БК Альграс."),
                            DateCreate = DateTime.Now,
                            LetterRecipient = Customer.Email,
                            LetterRecipientAddress = Customer.Email,
                            TypeLetter = TypeLetter.Outgoing,
                            DateReceiving = DateTime.Now,
                            TextBody = Letter.StringToByte($"Уважаемый клиент! Статус вашего договора был изменен с {Contract.ContractStatus} на {contractStatus}. С Уважением БК Альграс."),
                        };
                        letter.SetMailBox(mailbox);

                        Letter.SendMailKit(letter.LetterRecipient,
                            Letter.ByteToString(letter.Topic),
                            Letter.ByteToString(letter.HtmlBody),
                            letter.Mailbox.MailingAddress,
                            BVVGlobal.oApp.User,
                            Mailbox.Decrypt(letter.Mailbox.Password),
                            letter.Mailbox.MailboxSetup.OutgoingMailServerSMTP,
                            letter.Mailbox.MailboxSetup.PortSMTP,
                            null,
                            letter.Mailbox.MailingAddressCopy);

                        IsNotificationSent = true;
                    }
                    else
                    {
                        XtraMessageBox.Show($"Не найден Email клиента [{Contract.Customer}].",
                                            "Не найден email",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                    }
                }

                Contract.ChronicleContract.Add(new ChronicleContract(Session)
                {
                    User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid),
                    Date = DateTime.Now,
                    Description = $"Изменен статус договора с {Contract.ContractStatus} на {contractStatus}",
                    ContractStatus = contractStatus,
                    IsNotificationSent = IsNotificationSent,
                    DateSince = sinceDate,
                    DateTo = toDate
                });
            }

            if (Contract.Oid > 0 && (Contract.DateSince != sinceDate || Contract.DateTo != toDate))
            {
                Contract.ChronicleContract.Add(new ChronicleContract(Session)
                {
                    User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid),
                    Date = DateTime.Now,
                    Description = $"Изменен период действия договора [с {Contract.DateSince?.ToShortDateString()} по {Contract.DateTo?.ToShortDateString()}] на [с {sinceDate?.ToShortDateString()} по {toDate?.ToShortDateString()}]",
                    ContractStatus = contractStatus,
                    DateSince = sinceDate,
                    DateTo = toDate
                });
            }

            Contract.ContractStatus = contractStatus;
            Contract.DateSince = sinceDate;
            Contract.DateTo = toDate;

            Contract.Town = txtTown.Text;

            var plateTemplate = Contract.PlateTemplate;
            EditPlateTemplate(btnPlateTemplate.EditValue, ref plateTemplate);
            Contract.PlateTemplate = plateTemplate;

            var plateTemplate1 = Contract.PlateTemplate1;
            EditPlateTemplate(btnPlateTemplate1.EditValue, ref plateTemplate1);
            Contract.PlateTemplate1 = plateTemplate1;

            var plateTemplate2 = Contract.PlateTemplate2;
            EditPlateTemplate(btnPlateTemplate2.EditValue, ref plateTemplate2);
            Contract.PlateTemplate2 = plateTemplate2;

            var plateTemplate3 = Contract.PlateTemplate3;
            EditPlateTemplate(btnPlateTemplate3.EditValue, ref plateTemplate3);
            Contract.PlateTemplate3 = plateTemplate3;

            if (btnOrganization.EditValue is Organization organization)
            {
                Contract.Organization = organization;
            }
            else
            {
                Contract.Organization = null;
            }

            Contract.Save();
        }

        private void EditPlateTemplate(object buttonTemplateEditValue, ref PlateTemplate objPlateTemplate)
        {
            if (buttonTemplateEditValue is PlateTemplate plateTemplate)
            {
                objPlateTemplate = plateTemplate;
            }
            else
            {
                objPlateTemplate = null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                txtManagement.EditValue = null;
                txtManagementPosition.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);

            if (buttonEdit.EditValue is Customer customer)
            {
                txtManagement.EditValue = customer.ManagementString;
                txtManagementPosition.EditValue = customer.ManagementPositionString;
            }
        }

        private void GetPlateTemplate_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<PlateTemplate>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.PlateTemplate, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ContractSave();

            var lastTemplate = Contract?.ContractFiles?.LastOrDefault();
            if (lastTemplate != null && lastTemplate.File?.FileByte != null)
            {
                if (XtraMessageBox.Show(
                    $"Если вы согласитесь, будет создан новый документ Word.{Environment.NewLine}" +
                        $"Если хотите открыть ранее созданный документ, найдите его в списке готовых файлов и воспользуйтесь функцией просмотра.",
                    "Информационное сообщение",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
                {
                    new ContractOpenXml(lastTemplate.File.FileByte, false);
                    return;
                }
            }

            new ContractOpenXml(Contract);
        }

        private void ContractReport_SaveQuitEvent(object sender, string tempFile, byte[] wordDocument, string fileName)
        {
            if (Session.IsConnected)
            {
                var contractReport = sender as ContractReport;

                var contractNumber = default(string);

                if (int.TryParse(Contract.Number, out int result))
                {
                    contractNumber = result.ToString();
                }

                try
                {
                    var fileContract = new ContractFile(Session)
                    {
                        DateCreate = DateTime.Now,
                        Contract = Contract,
                        File = new File(Session, tempFile)
                        {
                            FileName = fileName
                        }
                    };
                    fileContract.Save();

                    if (System.IO.File.Exists(tempFile))
                    {
                        System.IO.File.Delete(tempFile);
                    }

                    contractReport.SaveQuitEvent -= ContractReport_SaveQuitEvent;

                    gridViewGeneratedFiles.BeginDataUpdate();
                    Contract.ContractFiles?.Reload();
                    gridViewGeneratedFiles.EndDataUpdate();
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                }
            }
        }

        private void btnContractStatus_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<ContractStatus>(Session, buttonEdit, (int)cls_App.ReferenceBooks.ContractStatus, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnOrganization_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                txtPrefix.EditValue = null;
                txtPrefix.ReadOnly = false;
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Organization>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Organization, 1, null, null, false, null, string.Empty, false, true);

            if (buttonEdit.EditValue is Organization organization)
            {
                txtPrefix.EditValue = organization.Prefix;
                txtPrefix.ReadOnly = true;

                try
                {
                    if (Contract.Oid <= 0)
                    {
                        var contractCollection = new XPCollection<Contract>(Session, new BinaryOperator(nameof(Contract.Organization), organization));

                        var number = contractCollection.LastOrDefault(l => int.TryParse(l.Number, out int result));
                        if (number is null)
                        {
                            txtNumber.EditValue = 1;
                        }
                        else
                        {
                            txtNumber.EditValue = Convert.ToInt32(number.Number) + 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                }                
            }
        }

        private void btnFileWord_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (Contract.File != null)
            {
                Contract.File.Delete();
                Contract.File = null;
                ((ButtonEdit)sender).EditValue = null;
                Contract.Save();
            }            
        }

        private void gridViewSupplementaryAgreement_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView)
            {
                popupMenuSupplementaryAgreement.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void barBtnSupplementaryAgreementAdd_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barBtnSupplementaryAgreementEdit_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barBtnSupplementaryAgreementDel_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void gridViewAttachment_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {

            if (sender is GridView)
            {
                popupMenuAttachment.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void barBtnAttachmentAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new ContractAttachmentEdit(Contract);
            form.ShowDialog();
        }

        private void barBtnAttachmentEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewAttachment.GetRow(gridViewAttachment.FocusedRowHandle) is ContractAttachment contractAttachment)
            {
                var form = new ContractAttachmentEdit(contractAttachment);
                form.ShowDialog();
            }
        }

        private void barBtnAttachmentDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewAttachment.GetRow(gridViewAttachment.FocusedRowHandle) is ContractAttachment contractAttachment)
            {
                if (XtraMessageBox.Show(
                    "Хотите удалить выбранное приложение к договору?",
                    "Информационное сообщение",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
                {
                    contractAttachment.Delete();
                    contractAttachment.Save();
                }
            }
        }

        private void gridViewGeneratedFiles_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView)
            {
                popupMenuGeneratedFile.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void ClearEditValue(object sender, ButtonPressedEventArgs e)
        {
            if (sender is ButtonEdit buttonEdit)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                }
            }
        }

        private void barBtnGeneratedFileView_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var focusedRowHandles = gridViewGeneratedFiles.GetSelectedRows();

                if (focusedRowHandles.Length > 0)
                {
                    foreach (var focusedRowHandle in focusedRowHandles)
                    {
                        if (gridViewGeneratedFiles.GetRow(focusedRowHandle) is ContractFile contractFile)
                        {
                            var contractFileByte = contractFile.File?.FileByte;
                            if (contractFileByte != null)
                            {
                                var contractOpenXml = new ContractOpenXml(contractFileByte, false);
                            }
                        }
                    }
                }

                else
                {
                    XtraMessageBox.Show("Необходимо выбрать договора для просмотра.",
                                        "Информационное сообщение",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void barBtnGeneratedFileDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var focusedRowHandles = gridViewGeneratedFiles.GetSelectedRows();
                if (focusedRowHandles.Length > 0)
                {
                    if (XtraMessageBox.Show(
                                           "Хотите окончательно удалить выбранные договоры?",
                                           "Информационное сообщение",
                                           MessageBoxButtons.OKCancel,
                                           MessageBoxIcon.Question) == DialogResult.OK)
                    {

                        var deleteList = new List<ContractFile>();
                        foreach (var focusedRowHandle in focusedRowHandles)
                        {
                            if (gridViewGeneratedFiles.GetRow(focusedRowHandle) is ContractFile contractFile)
                            {
                                deleteList.Add(contractFile);
                            }
                        }

                        foreach (var contractFile in deleteList)
                        {
                            contractFile.Delete();
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show("Необходимо выбрать договора для удаления.",
                                        "Информационное сообщение",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Hand);
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }
    }
}