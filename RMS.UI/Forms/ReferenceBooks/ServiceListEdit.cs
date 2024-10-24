using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.InfoContract.ContractAttachments;
using RMS.Core.Model.OKVED;
using System;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class ServiceListEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        public ServiceList ServiceList { get; }
        private TypeServiceList TypeServiceList { get; set; }

        private ServiceListEdit()
        {
            InitializeComponent();

            foreach (TypeServiceList item in Enum.GetValues(typeof(TypeServiceList)))
            {
                cmbTypeServiceList.Properties.Items.Add(item.GetEnumDescription());
            }
        }

        public ServiceListEdit(Session session) : this()
        {
            Session = session;
            ServiceList = new ServiceList(Session);
        }

        public ServiceListEdit(ServiceList serviceList) : this()
        {
            Session = serviceList.Session;
            ServiceList = serviceList;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbTypeServiceList.SelectedIndex == -1)
            {
                cmbTypeServiceList.Focus();
                return;
            }

            ServiceList.TypeServiceList = (TypeServiceList)cmbTypeServiceList.SelectedIndex;
            ServiceList.Value = calcValue.Value;
            ServiceList.Comment = memoComment.Text;

            switch (TypeServiceList)
            {
                case TypeServiceList.TaxSystem:
                    ServiceList.Mark = btnTaxSystem.EditValue?.ToString();
                    break;

                case TypeServiceList.KindActivity:
                    ServiceList.Mark = btnKindActivity.EditValue?.ToString();
                    break;

                case TypeServiceList.SourceDocuments:
                    ServiceList.Mark = memoSourceDocuments.Text;
                    break;

                case TypeServiceList.BankAccountsAndTransactions:
                    ServiceList.Mark = $"Количество банковских счетов: {textEdit1.Value}; Вносит: {spinEdit1.EditValue}";
                    break;

                case TypeServiceList.AvailabilityCurrencyTransactions:
                    break;
                case TypeServiceList.PresenceForeignTradeActivities:
                    break;
                case TypeServiceList.SaleOfGoodsWithDifferentVATRates:
                    break;
            }


            Session.Save(ServiceList);
            id = ServiceList.Oid;
            flagSave = true;
            Close();
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            cmbTypeServiceList.SelectedIndex = Convert.ToInt32(ServiceList.TypeServiceList);
        }

        private void btnTaxSystem_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<TaxSystem>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.TaxSystem, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnKindActivity_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<ClassOKVED2>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.ClassOKVED2, 1, null, null, false, null, string.Empty, false, true);
        }

        private void cmbTypeServiceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBoxEdit = sender as ComboBoxEdit;

            if (comboBoxEdit != null && comboBoxEdit.SelectedIndex != -1)
            {
                var pageIndex = comboBoxEdit.SelectedIndex;
                TypeServiceList = (TypeServiceList)comboBoxEdit.SelectedIndex;
                calcValue.EditValue = 0;

                if (pageIndex >= 0 && pageIndex < xtraTab.TabPages.Count)
                {
                    for (int i = 0; i < xtraTab.TabPages.Count; i++)
                    {
                        if (i == pageIndex)
                        {
                            xtraTab.TabPages[i].PageVisible = true;
                            xtraTab.SelectedTabPageIndex = i;
                        }
                        else
                        {
                            xtraTab.TabPages[i].PageVisible = false;
                        }
                    }
                }
            }
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            GetValueBankAccountsAndTransactions();
        }

        private void GetValueBankAccountsAndTransactions()
        {
            if (spinEdit1.SelectedIndex == 0)
            {
                calcValue.EditValue = textEdit1.Value * 1500;
            }
            else
            {
                calcValue.EditValue = textEdit1.Value * 2500;
            }
        }

        private void spinEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetValueBankAccountsAndTransactions();
        }
    }
}