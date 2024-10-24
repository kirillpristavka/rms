using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms.Directories
{
    /// <summary>
    /// Форма редактирования показателей работы организации.
    /// </summary>
    public partial class OrganizationPerformanceEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        public OrganizationPerformance OrganizationPerformance { get; }

        private OrganizationPerformanceEdit()
        {
            InitializeComponent();

            foreach (Month item in Enum.GetValues(typeof(Month)))
            {
                cmbMonth.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbMonth.SelectedIndex = 0;
        }

        public OrganizationPerformanceEdit(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            OrganizationPerformance = new OrganizationPerformance(Session);
        }

        public OrganizationPerformanceEdit(OrganizationPerformance personalIncomeTax) : this()
        {
            OrganizationPerformance = personalIncomeTax;
            Customer = personalIncomeTax.Customer;
            Session = personalIncomeTax.Session;
        }

        private bool Save()
        {
            try
            {
                OrganizationPerformance.Month = (Month)cmbMonth.SelectedIndex + 1;

                if (OrganizationPerformance.Month == Month.January ||
                    OrganizationPerformance.Month == Month.February ||
                    OrganizationPerformance.Month == Month.March)
                {
                    OrganizationPerformance.PeriodReportChangeQuarter = PeriodReportChange.FIRSTQUARTER;
                }
                else if (OrganizationPerformance.Month == Month.April ||
                    OrganizationPerformance.Month == Month.May ||
                    OrganizationPerformance.Month == Month.June)
                {
                    OrganizationPerformance.PeriodReportChangeQuarter = PeriodReportChange.SECONDQUARTER;
                }
                else if (OrganizationPerformance.Month == Month.July ||
                   OrganizationPerformance.Month == Month.August ||
                   OrganizationPerformance.Month == Month.September)
                {
                    OrganizationPerformance.PeriodReportChangeQuarter = PeriodReportChange.THIRDQUARTER;
                }
                else if (OrganizationPerformance.Month == Month.October ||
                   OrganizationPerformance.Month == Month.November ||
                   OrganizationPerformance.Month == Month.December)
                {
                    OrganizationPerformance.PeriodReportChangeQuarter = PeriodReportChange.FOURTHQUARTER;
                }

                if (OrganizationPerformance.PeriodReportChangeQuarter == PeriodReportChange.FIRSTQUARTER ||
                    OrganizationPerformance.PeriodReportChangeQuarter == PeriodReportChange.SECONDQUARTER)
                {
                    OrganizationPerformance.PeriodReportChangeHalfYear = PeriodReportChange.FIRSTHALFYEAR;
                }
                else if (OrganizationPerformance.PeriodReportChangeQuarter == PeriodReportChange.THIRDQUARTER ||
                    OrganizationPerformance.PeriodReportChangeQuarter == PeriodReportChange.FOURTHQUARTER)
                {
                    OrganizationPerformance.PeriodReportChangeHalfYear = PeriodReportChange.SECONDQUARTER;
                }

                OrganizationPerformance.Comment = memoComment.Text;
                OrganizationPerformance.Year = Convert.ToInt32(cmbYear.Text);

                if (Customer != null)
                {
                    Customer.PersonalIncomeTaxis.Add(OrganizationPerformance);
                }

                Session.Save(OrganizationPerformance.CustomerPerformanceIndicators);
                OrganizationPerformance.Save();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private RepositoryItemCheckEdit repositoryItemCheckEdit;


        /// <summary>
        /// Закрыть объекты для редактирования.
        /// </summary>
        /// <param name="isClosed"></param>
        private void CloseObjectsForEditing(bool isClosed = true)
        {
            if (isClosed)
            {
                cmbMonth.Properties.ReadOnly = true;
                cmbYear.Properties.ReadOnly = true;
                gridView.OptionsBehavior.Editable = false;

                lblFAQ.Text = $"Счет № {OrganizationPerformance.Invoice.Number} от {OrganizationPerformance.Invoice.Date.ToShortDateString()}";

                btnGenerateInvoicePayment.Text = "Посмотреть счет";
            }
            else
            {
                cmbMonth.Properties.ReadOnly = false;
                cmbYear.Properties.ReadOnly = false;
                gridView.OptionsBehavior.Editable = true;

                lblFAQ.Text = string.Empty;
                btnGenerateInvoicePayment.Text = "Сформировать счет";
            }            
        }
        
        private void Form_Load(object sender, EventArgs e)
        {
            OrganizationPerformance?.Reload();
            OrganizationPerformance?.CustomerPerformanceIndicators?.Reload();

            XPBaseObject.AutoSaveOnEndEdit = false;
                        
            if (OrganizationPerformance.Oid > 0 && OrganizationPerformance.Invoice != null)
            {
                /* Если найден счет, который уже удален, обнулим значение. */
                if (OrganizationPerformance.Invoice.IsDeleted)
                {
                    OrganizationPerformance.Invoice = null;
                    OrganizationPerformance.Save();
                }
                else
                {
                    CloseObjectsForEditing();
                }
            }
            
            repositoryItemCheckEdit = new RepositoryItemCheckEdit();
            repositoryItemCheckEdit.ValueChecked = "true";
            repositoryItemCheckEdit.ValueUnchecked = "false";
            repositoryItemCheckEdit.CheckStyle = CheckStyles.Standard;

            var xpCollectionGroupPerformanceIndicator = new XPCollection<GroupPerformanceIndicator>(Session);
            
            if (OrganizationPerformance.Oid <= 0)
            {
                foreach (var group in xpCollectionGroupPerformanceIndicator)
                {
                    group.PerformanceIndicators.Reload();
                    foreach (var indicator in group.PerformanceIndicators.Where(w => w.IsUseWhenGeneratingInformationOnEmployees == true))
                    {
                        OrganizationPerformance.CustomerPerformanceIndicators.Add(new CustomerPerformanceIndicator(Session) 
                        {
                            PerformanceIndicator = indicator,
                            Value = null
                        });
                    }
                }
            }
            else if (OrganizationPerformance.Invoice is null)
            {
                foreach (var group in xpCollectionGroupPerformanceIndicator)
                {
                    group.PerformanceIndicators.Reload();
                    foreach (var indicator in group.PerformanceIndicators.Where(w => w.IsUseWhenGeneratingInformationOnEmployees == true))
                    {
                        if (OrganizationPerformance.CustomerPerformanceIndicators.FirstOrDefault(f => f.PerformanceIndicator == indicator) is null)
                        {
                            OrganizationPerformance.CustomerPerformanceIndicators.Add(new CustomerPerformanceIndicator(Session)
                            {
                                PerformanceIndicator = indicator,
                                Value = null
                            });
                        }
                    }
                }
            }

            gridControl.DataSource = OrganizationPerformance.CustomerPerformanceIndicators;

            foreach (GridColumn column in gridView.Columns)
            {
                column.OptionsColumn.AllowEdit = false;
                column.OptionsColumn.ReadOnly = true;
            }
            
            if (gridView.Columns[nameof(CustomerPerformanceIndicator.Oid)] != null)
            {
                gridView.Columns[nameof(CustomerPerformanceIndicator.Oid)].Visible = false;
                gridView.Columns[nameof(CustomerPerformanceIndicator.Oid)].Width = 18;
                gridView.Columns[nameof(CustomerPerformanceIndicator.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(CustomerPerformanceIndicator.GroupPerformanceIndicatorString)] != null)
            {
                gridView.Columns[nameof(CustomerPerformanceIndicator.GroupPerformanceIndicatorString)].Group();
                gridView.ExpandAllGroups();
            }

            if (gridView.Columns[nameof(CustomerPerformanceIndicator.Value)] != null)
            {
                gridView.Columns[nameof(CustomerPerformanceIndicator.Value)].OptionsColumn.AllowEdit = true;
                gridView.Columns[nameof(CustomerPerformanceIndicator.Value)].OptionsColumn.ReadOnly = false;
                gridView.Columns[nameof(CustomerPerformanceIndicator.Value)].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }

            cmbMonth.SelectedIndex = ((int)OrganizationPerformance.Month - 1 == -1) ? 0 : (int)OrganizationPerformance.Month - 1;
            cmbYear.EditValue = OrganizationPerformance.Year;
            memoComment.EditValue = OrganizationPerformance.Comment;
            
            if (Customer != null)
            {
                var customerString = $"Клиент: {Customer}";
                if (customerString.Length > 60)
                {
                    customerString = $"{customerString.Substring(0, 55)}...";
                }
                lblCustomer.Text = customerString;
            }
            else
            {
                lblCustomer.Visible = false;
            }            
        }

        private void OrganizationPerformanceEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            OrganizationPerformance.Reload();
            OrganizationPerformance.CustomerPerformanceIndicators.Reload();
            foreach (var customerPerformanceIndicators in OrganizationPerformance.CustomerPerformanceIndicators)
            {
                customerPerformanceIndicators.Reload();
                customerPerformanceIndicators.PerformanceIndicator.Reload();
            }
            XPBaseObject.AutoSaveOnEndEdit = true;
        }

        private void gridView_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName != nameof(CustomerPerformanceIndicator.Value)) 
            {
                return;
            }

            var gridview = sender as GridView;

            if (gridview != null)
            {
                var customerPerformanceIndicator = gridview.GetRow(e.RowHandle) as CustomerPerformanceIndicator;

                if (customerPerformanceIndicator != null 
                    && customerPerformanceIndicator.PerformanceIndicator != null
                    && customerPerformanceIndicator.PerformanceIndicator.TypePerformanceIndicator == TypePerformanceIndicator.Percent)
                {
                    e.RepositoryItem = repositoryItemCheckEdit;
                }
            }
        }

        private void btnGenerateInvoicePayment_Click(object sender, EventArgs e)
        {
            var invoice = OrganizationPerformance.Invoice;
            if (invoice is null)
            {
                Save();
                invoice = Invoice.GetInvoiceForPerformanceIndicators(OrganizationPerformance);
            }

            invoice.Reload();
            var form = new InvoiceEdit(invoice, OrganizationPerformance);
            form.ShowDialog();

            if (form.IsSave)
            {
                OrganizationPerformance.Invoice = invoice;
                OrganizationPerformance.Save();

                CloseObjectsForEditing();
            }
        }        
    }
}