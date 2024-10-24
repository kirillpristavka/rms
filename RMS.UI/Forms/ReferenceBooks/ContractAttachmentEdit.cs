using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Model.InfoContract;
using RMS.Core.Model.InfoContract.ContractAttachments;
using System;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class ContractAttachmentEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private ContractAttachment ContractAttachment { get; }
        private Contract Contract { get; }
        private OptionContractAttachment OptionContractAttachment { get; set; }

        private ContractAttachmentEdit()
        {
            InitializeComponent();
        }

        public ContractAttachmentEdit(int id) : this()
        {
            if (id > 0)
            {
                ContractAttachment = Session.GetObjectByKey<ContractAttachment>(id);
                Contract = ContractAttachment.Contract;
            }
        }

        public ContractAttachmentEdit(ContractAttachment contractAttachment) : this()
        {
            Session = contractAttachment.Session;
            ContractAttachment = contractAttachment;
            Contract = contractAttachment.Contract;
            OptionContractAttachment = ContractAttachment.OptionContractAttachment;
        }

        public ContractAttachmentEdit(Contract contract) : this()
        {
            Contract = contract;
            Session = contract.Session;
            ContractAttachment = new ContractAttachment(Session);

            if (OptionContractAttachment == OptionContractAttachment.ListTransmittedDocumentsAndInformation)
            {
                var xpcollectionStatutoryDocument = new XPCollection<StatutoryDocument>(Session);
                foreach (var item in xpcollectionStatutoryDocument)
                {
                    ContractAttachment.StatutoryDocumentsContract.Add(
                        new StatutoryDocumentContract(Session)
                        {
                            StatutoryDocument = item
                        });
                }

                var xpcollectionTitleDocument = new XPCollection<TitleDocument>(Session);
                foreach (var item in xpcollectionTitleDocument)
                {
                    ContractAttachment.TitleDocumentsContract.Add(
                        new TitleDocumentContract(Session)
                        {
                            TitleDocument = item
                        });
                }

                var xpcollectionTaxReportingDocument = new XPCollection<TaxReportingDocument>(Session);
                foreach (var item in xpcollectionTaxReportingDocument)
                {
                    ContractAttachment.TaxReportingDocumentsContract.Add(
                        new TaxReportingDocumentContract(Session)
                        {
                            TaxReportingDocument = item
                        });
                }

                var xpcollectionSourceDocument = new XPCollection<SourceDocument>(Session);
                foreach (var item in xpcollectionSourceDocument)
                {
                    ContractAttachment.SourceDocumentsContract.Add(
                        new SourceDocumentContract(Session)
                        {
                            SourceDocument = item
                        });
                }

                var xpcollectionEmployeeDetailsDocument = new XPCollection<EmployeeDetailsDocument>(Session);
                foreach (var item in xpcollectionEmployeeDetailsDocument)
                {
                    ContractAttachment.EmployeeDetailsDocumentsContract.Add(
                        new EmployeeDetailsDocumentContract(Session)
                        {
                            EmployeeDetailsDocument = item
                        });
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ContractAttachment.Name = txtName.Text;
            ContractAttachment.Description = memoDescription.Text;
            ContractAttachment.OptionContractAttachment = OptionContractAttachment;

            if (OptionContractAttachment == OptionContractAttachment.ListTransmittedDocumentsAndInformation)
            {
                Session.Delete(ContractAttachment.ServiceListContract);
            }
            else if (OptionContractAttachment == OptionContractAttachment.ListRenderedServicesAndAccountingServices)
            {
                Session.Delete(ContractAttachment.StatutoryDocumentsContract);
                Session.Delete(ContractAttachment.TitleDocumentsContract);
                Session.Delete(ContractAttachment.TaxReportingDocumentsContract);
                Session.Delete(ContractAttachment.SourceDocumentsContract);
                Session.Delete(ContractAttachment.EmployeeDetailsDocumentsContract);
            }

            Contract.ContractAttachments.Add(ContractAttachment);
            Session.Save(ContractAttachment);
            id = ContractAttachment.Oid;
            flagSave = true;
            Close();
        }

        private void ContractAttachmentEdit_Load(object sender, EventArgs e)
        {
            memoDescription.Text = ContractAttachment.Description;
            txtName.Text = ContractAttachment.Name;
            rgOptionContractAttachment.SelectedIndex = Convert.ToInt32(ContractAttachment.OptionContractAttachment);

            gridControlStatutoryDocumentContract.DataSource = ContractAttachment.StatutoryDocumentsContract;
            if (gridViewStatutoryDocumentContract.Columns[nameof(ContractAttachment.Oid)] != null)
            {
                gridViewStatutoryDocumentContract.Columns[nameof(ContractAttachment.Oid)].Visible = false;
                gridViewStatutoryDocumentContract.Columns[nameof(ContractAttachment.Oid)].Width = 18;
                gridViewStatutoryDocumentContract.Columns[nameof(ContractAttachment.Oid)].OptionsColumn.FixedWidth = true;
            }

            gridControlTitleDocumentContract.DataSource = ContractAttachment.TitleDocumentsContract;
            if (gridViewTitleDocumentContract.Columns[nameof(ContractAttachment.Oid)] != null)
            {
                gridViewTitleDocumentContract.Columns[nameof(ContractAttachment.Oid)].Visible = false;
                gridViewTitleDocumentContract.Columns[nameof(ContractAttachment.Oid)].Width = 18;
                gridViewTitleDocumentContract.Columns[nameof(ContractAttachment.Oid)].OptionsColumn.FixedWidth = true;
            }

            gridControlTaxReportingDocumentContract.DataSource = ContractAttachment.TaxReportingDocumentsContract;
            if (gridViewTaxReportingDocumentContract.Columns[nameof(ContractAttachment.Oid)] != null)
            {
                gridViewTaxReportingDocumentContract.Columns[nameof(ContractAttachment.Oid)].Visible = false;
                gridViewTaxReportingDocumentContract.Columns[nameof(ContractAttachment.Oid)].Width = 18;
                gridViewTaxReportingDocumentContract.Columns[nameof(ContractAttachment.Oid)].OptionsColumn.FixedWidth = true;
            }

            gridControlSourceDocumentContract.DataSource = ContractAttachment.SourceDocumentsContract;
            if (gridViewSourceDocumentContract.Columns[nameof(ContractAttachment.Oid)] != null)
            {
                gridViewSourceDocumentContract.Columns[nameof(ContractAttachment.Oid)].Visible = false;
                gridViewSourceDocumentContract.Columns[nameof(ContractAttachment.Oid)].Width = 18;
                gridViewSourceDocumentContract.Columns[nameof(ContractAttachment.Oid)].OptionsColumn.FixedWidth = true;
            }

            gridControlEmployeeDetailsDocumentContract.DataSource = ContractAttachment.EmployeeDetailsDocumentsContract;
            if (gridViewEmployeeDetailsDocumentContract.Columns[nameof(ContractAttachment.Oid)] != null)
            {
                gridViewEmployeeDetailsDocumentContract.Columns[nameof(ContractAttachment.Oid)].Visible = false;
                gridViewEmployeeDetailsDocumentContract.Columns[nameof(ContractAttachment.Oid)].Width = 18;
                gridViewEmployeeDetailsDocumentContract.Columns[nameof(ContractAttachment.Oid)].OptionsColumn.FixedWidth = true;
            }

            gridControlServiceListContract.DataSource = ContractAttachment.ServiceListContract;
            if (gridViewServiceListContract.Columns[nameof(ContractAttachment.Oid)] != null)
            {
                gridViewServiceListContract.Columns[nameof(ContractAttachment.Oid)].Visible = false;
                gridViewServiceListContract.Columns[nameof(ContractAttachment.Oid)].Width = 18;
                gridViewServiceListContract.Columns[nameof(ContractAttachment.Oid)].OptionsColumn.FixedWidth = true;
            }
        }

        private void btnObjectAdd_Click(object sender, EventArgs e)
        {
            var oid = default(int);

            if (xtraTab.SelectedTabPage == xtraTabPage)
            {
                oid = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.StatutoryDocument, -1);

                if (oid > 0)
                {
                    ContractAttachment.StatutoryDocumentsContract.Add(new StatutoryDocumentContract(Session)
                    {
                        StatutoryDocument = Session.GetObjectByKey<StatutoryDocument>(oid)
                    });
                }
            }
            else if (xtraTab.SelectedTabPage == xtraTabPage1)
            {
                oid = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.TitleDocument, -1);

                if (oid > 0)
                {
                    ContractAttachment.TitleDocumentsContract.Add(new TitleDocumentContract(Session)
                    {
                        TitleDocument = Session.GetObjectByKey<TitleDocument>(oid)
                    });
                }
            }
            else if (xtraTab.SelectedTabPage == xtraTabPage2)
            {
                oid = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.TaxReportingDocument, -1);

                if (oid > 0)
                {
                    ContractAttachment.TaxReportingDocumentsContract.Add(new TaxReportingDocumentContract(Session)
                    {
                        TaxReportingDocument = Session.GetObjectByKey<TaxReportingDocument>(oid)
                    });
                }
            }
            else if (xtraTab.SelectedTabPage == xtraTabPage3)
            {
                oid = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.SourceDocument, -1);

                if (oid > 0)
                {
                    ContractAttachment.SourceDocumentsContract.Add(new SourceDocumentContract(Session)
                    {
                        SourceDocument = Session.GetObjectByKey<SourceDocument>(oid)
                    });
                }
            }
            else if (xtraTab.SelectedTabPage == xtraTabPage4)
            {
                oid = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.EmployeeDetailsDocument, -1);

                if (oid > 0)
                {
                    ContractAttachment.EmployeeDetailsDocumentsContract.Add(new EmployeeDetailsDocumentContract(Session)
                    {
                        EmployeeDetailsDocument = Session.GetObjectByKey<EmployeeDetailsDocument>(oid)
                    });
                }
            }
            else if (xtraTab.SelectedTabPage == xtraTabPage5)
            {
                var form = new ServiceListEdit(Session);
                form.ShowDialog();

                if (form.FlagSave)
                {
                    ContractAttachment.ServiceListContract.Add(new ServiceListContract(Session)
                    {
                        ServiceList = form.ServiceList
                    });
                }
            }
        }

        private void btnObjectDelete_Click(object sender, EventArgs e)
        {
            if (xtraTab.SelectedTabPage == xtraTabPage)
            {
                if (gridViewStatutoryDocumentContract.IsEmpty)
                {
                    return;
                }

                var statutoryDocumentContract = gridViewStatutoryDocumentContract.GetRow(gridViewStatutoryDocumentContract.FocusedRowHandle) as StatutoryDocumentContract;
                statutoryDocumentContract.Delete();
            }
            else if (xtraTab.SelectedTabPage == xtraTabPage1)
            {
                if (gridViewTitleDocumentContract.IsEmpty)
                {
                    return;
                }

                var titleDocumentContract = gridViewTitleDocumentContract.GetRow(gridViewTitleDocumentContract.FocusedRowHandle) as TitleDocumentContract;
                titleDocumentContract.Delete();
            }
            else if (xtraTab.SelectedTabPage == xtraTabPage2)
            {
                if (gridViewTaxReportingDocumentContract.IsEmpty)
                {
                    return;
                }

                var taxReportingDocumentContract = gridViewTaxReportingDocumentContract.GetRow(gridViewTaxReportingDocumentContract.FocusedRowHandle) as TaxReportingDocumentContract;
                taxReportingDocumentContract.Delete();
            }
            else if (xtraTab.SelectedTabPage == xtraTabPage3)
            {
                if (gridViewSourceDocumentContract.IsEmpty)
                {
                    return;
                }

                var sourceDocumentContract = gridViewSourceDocumentContract.GetRow(gridViewSourceDocumentContract.FocusedRowHandle) as SourceDocumentContract;
                sourceDocumentContract.Delete();
            }
            else if (xtraTab.SelectedTabPage == xtraTabPage4)
            {
                if (gridViewEmployeeDetailsDocumentContract.IsEmpty)
                {
                    return;
                }

                var employeeDetailsDocumentContract = gridViewEmployeeDetailsDocumentContract.GetRow(gridViewEmployeeDetailsDocumentContract.FocusedRowHandle) as EmployeeDetailsDocumentContract;
                employeeDetailsDocumentContract.Delete();
            }
            else if (xtraTab.SelectedTabPage == xtraTabPage5)
            {
                if (gridViewServiceListContract.IsEmpty)
                {
                    return;
                }

                var serviceListContract = gridViewServiceListContract.GetRow(gridViewServiceListContract.FocusedRowHandle) as ServiceListContract;
                serviceListContract.Delete();
            }
        }

        private void rgOptionContractAttachment_SelectedIndexChanged(object sender, EventArgs e)
        {
            var radioGroup = sender as DevExpress.XtraEditors.RadioGroup;

            if (radioGroup != null)
            {
                OptionContractAttachment = (OptionContractAttachment)radioGroup.SelectedIndex;
            }

            if (OptionContractAttachment == OptionContractAttachment.ListTransmittedDocumentsAndInformation)
            {
                xtraTabPage.PageVisible = true;
                xtraTabPage1.PageVisible = true;
                xtraTabPage2.PageVisible = true;
                xtraTabPage3.PageVisible = true;
                xtraTabPage4.PageVisible = true;
                xtraTabPage5.PageVisible = false;
            }
            else if (OptionContractAttachment == OptionContractAttachment.ListRenderedServicesAndAccountingServices)
            {
                xtraTabPage.PageVisible = false;
                xtraTabPage1.PageVisible = false;
                xtraTabPage2.PageVisible = false;
                xtraTabPage3.PageVisible = false;
                xtraTabPage4.PageVisible = false;
                xtraTabPage5.PageVisible = true;
            }
        }
    }
}