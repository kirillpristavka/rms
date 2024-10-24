using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using PulsLibrary.Methods;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.PackagesDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.xUI.PackagesDocument.Controllers
{
    public partial class PackageDocumentInfoControl : XtraUserControl
    {
        private UnitOfWork _uof;
        private DateTime _dateTimeNow = DateTime.Now.Date;
        private List<Info> _infos = new List<Info>();
        private List<Info> _monitoring = new List<Info>();

        public delegate void ListItemCheckForDocumentEventHandler(object sender, CriteriaOperator criteriaDocument, CriteriaOperator criteriaDocumentType);
        public event ListItemCheckForDocumentEventHandler ListItemCheckForDocumentEvent;

        public delegate void ListItemCheckForCustomerStaffEventHandler(object sender, CriteriaOperator criteriaDocument, CriteriaOperator criteriaDocumentType);
        public event ListItemCheckForCustomerStaffEventHandler ListItemCheckForCustomerStaffEvent;

        public struct Info
        {
            public string Name { get; private set; }
            public int StatusOid { get; private set; }

            private int count;

            public Info(string name, int count, int statusOid)
            {
                this.count = count;
                StatusOid = statusOid;
                Name = name;
            }

            public override string ToString()
            {
                return $"{Name} - {count}";
            }
        }

        public PackageDocumentInfoControl()
        {
            InitializeComponent();
        }
        
        public void SetUnitOfWork(UnitOfWork uof)
        {
            _uof = uof;
        }

        public async void UpdateData(object obj)
        {
            _infos.Clear();
            checkedListInfo.Items.Clear();

            var packageDocumentsType = new List<PackageDocumentType>();

            if (obj is List<PackageDocument> packageDocuments)
            {                
                foreach (var item in packageDocuments)
                {
                    packageDocumentsType.AddRange(item.PackageDocumentsType);
                }                
            }
            else if (obj is List<CustomerStaff> customerStaffs)
            {

                foreach (var customerStaff in customerStaffs)
                {
                    var packageDocumentType = await new XPQuery<PackageDocumentType>(_uof)
                        ?.Where(w => w.CustomerStaff != null && w.CustomerStaff.Oid == customerStaff.Oid)
                        ?.ToListAsync();

                    packageDocumentsType.AddRange(packageDocumentType);
                }
            }

            var status = packageDocumentsType?.Select(s => s.PackageDocumentStatus)?.GroupBy(g => g);
            if (packageDocumentsType != null && status != null && status.Count() > 0)
            {
                foreach (var item in status.OrderBy(o => o.Key?.Name))
                {
                    var name = item?.Key?.Name;
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        continue;
                    }
                    _infos.Add(new Info(name,
                                        item.Count(),
                                        item.Key.Oid));
                }
            }

            foreach (var item in _infos)
            {
                checkedListInfo.Items.Add(item);
            }

            checkedListInfo.Refresh();
        }

        private async void PackageDocumentInfoControl_Load(object sender, EventArgs e)
        {
            _infos.Clear();
            checkedListInfo.Items.Clear();

            using (var uof = new UnitOfWork())
            {
                var documnets = await new XPQuery<PackageDocumentType>(uof)?.ToListAsync();
                var status = documnets?.Select(s => s.PackageDocumentStatus)?.GroupBy(g => g);

                if (documnets != null && status != null && status.Count() > 0)
                {
                    foreach (var obj in status.OrderBy(o => o.Key?.Name))
                    {
                        var name = obj?.Key?.Name;
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            continue;
                        }
                        _infos.Add(new Info(name,
                                            obj.Count(),
                                            obj.Key.Oid));
                    }
                }
            }

            foreach (var item in _infos)
            {
                checkedListInfo.Items.Add(item);
            }
        }

        private async void checkedListInfo_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            var control = Objects.GetRequiredObject<CheckedListBoxControl>(sender);
            if (control != null)
            {
                var checkedItems = control.Items.Where(w => w.CheckState == CheckState.Checked);
                if (checkedItems != null)
                {
                    var criretiaDocument = default(CriteriaOperator);
                    var criretiaDocumentType = default(CriteriaOperator);
                    var criretiaCustomerStaff = default(CriteriaOperator);

                    var groupOperatorDocument = new GroupOperator(GroupOperatorType.Or);
                    var groupOperatorDocumentType = new GroupOperator(GroupOperatorType.Or);
                    var groupOperatorCustomerStaff = new GroupOperator(GroupOperatorType.Or);

                    foreach (var item in checkedItems)
                    {
                        if (item.Value is Info info)
                        {
                            var criteria = new BinaryOperator($"{nameof(PackageDocumentType.PackageDocumentStatus)}.{nameof(PackageDocumentStatus.Oid)}", info.StatusOid);
                            groupOperatorDocumentType.Operands.Add(criteria);
                            groupOperatorDocument.Operands.Add(new ContainsOperator(nameof(PackageDocument.PackageDocumentsType), criteria));

                            if (_uof != null)
                            {
                                var customerStaffs = await new XPQuery<PackageDocumentType>(_uof)
                                    ?.Where(w => w.PackageDocumentStatus != null && w.PackageDocumentStatus.Oid == info.StatusOid)
                                    ?.Select(s => s.CustomerStaff)
                                    ?.GroupBy(g => g)
                                    ?.ToListAsync();

                                if (customerStaffs != null)
                                {
                                    foreach (var customerStaff in customerStaffs)
                                    {
                                        var customerStaffOid = customerStaff?.Key?.Oid;
                                        if (customerStaffOid != null)
                                        {
                                            groupOperatorCustomerStaff.Operands.Add(new BinaryOperator(nameof(XPObject.Oid), customerStaffOid));
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (groupOperatorDocument.Operands.Count > 0)
                    {
                        criretiaDocument = groupOperatorDocument;
                    }

                    if (groupOperatorDocumentType.Operands.Count > 0)
                    {
                        criretiaDocumentType = groupOperatorDocumentType;
                    }

                    if (groupOperatorCustomerStaff.Operands.Count > 0)
                    {
                        criretiaCustomerStaff = groupOperatorCustomerStaff;
                    }

                    ListItemCheckForDocumentEvent?.Invoke(this, criretiaDocument, criretiaDocumentType);
                    ListItemCheckForCustomerStaffEvent?.Invoke(this, criretiaCustomerStaff, criretiaDocumentType);
                }
            }
        }
    }
}
