using DevExpress.Xpo;
using RMS.Core.Controllers.PackagesDocument;
using RMS.Core.Extensions;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RMS.Core.Model.PackagesDocument
{
    /// <summary>
    /// Пакет документов.
    /// </summary>
    public class PackageDocument : XPObject
    {
        public PackageDocument() { }
        public PackageDocument(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Date = DateTime.Now.Date;
        }

        /// <summary>
        /// Дата.
        /// </summary>
        [DisplayName("Дата")]
        public DateTime? Date { get; set; }
        
        public bool IsCreateByCustomer { get; set; }
        public bool IsShowCustomer { get; set; }

        public string CustomerName => Customer?.ToString();
        public Customer Customer { get; set; }

        public string DocumentsName
        {
            get
            {
                var result = default(string);
                if (PackageDocumentsType != null)
                {
                    PackageDocumentsType?.Reload();
                    foreach (var item in PackageDocumentsType)
                    {
                        result += $"{item}{Environment.NewLine}";
                    }
                }

                return result?.Trim();
            }
        }
        
        public string StaffName => Staff?.ToString();
        public Staff Staff { get; set; }        

        /// <summary>
        /// Наименование.
        /// </summary>
        [DisplayName("Наименование")]
        public string Name { get; set; }
        
        [Size(2048)]
        [DisplayName("Описание")]
        public string Description { get; set; }

        public string CustomerStaff
        {
            get
            {
                if (PackageDocumentsType != null)
                {
                    var staffs = PackageDocumentsType?.GroupBy(g => g.CustomerStaff)?.Select(s => s.Key);
                    if (staffs != null)
                    {
                        var result = default(string);
                        foreach (var staff in staffs)
                        {
                            result += $"{staff}{Environment.NewLine}";
                        }

                        return result?.Trim();
                    }
                }

                return default;
            }
        }

        [Size(2048)]
        public string Period { get; set; }

        public string Status
        {
            get
            {
                if (_packageDocumentType != null)
                {
                    return _packageDocumentType?.Status;
                }

                return PackageDocumentStatus?.ToString();
            }
        }

        private PackageDocumentType _packageDocumentType;
        public PackageDocumentStatus PackageDocumentStatus
        {
            get
            {
                if (PackageDocumentsType != null)
                {
                    var status = PackageDocumentsType?.Select(s => s?.PackageDocumentStatus);
                    if (status.GroupBy(g => g)?.Count() == 1)
                    {
                        return status?.FirstOrDefault();
                    }
                    else
                    {
                        _packageDocumentType = PackageDocumentsType?.LastOrDefault(f => f.PackageDocumentStatus != null && f.PackageDocumentStatus.IsDefault is false);
                        return _packageDocumentType?.PackageDocumentStatus;
                    }
                }

                return default;
            }
        }

        public string GetColor()
        {
            if (PackageDocumentsType != null)
            {
                var colors = PackageDocumentsType?.Select(s => s?.GetStatusColor());
                if (colors?.Count() > 0)
                {
                    if (colors.GroupBy(g => g)?.Count() == 1)
                    {
                        return colors.FirstOrDefault().GetEnumDescription();
                    }
                    else
                    {
                        return colors.Min().GetEnumDescription();
                    }
                }
            }

            return default;
        }
        
        public async void FillDocuments(IEnumerable<PackageDocumentType> packageDocumentsType, User user = null)
        {
            var documents = packageDocumentsType.Select(s => s.Document)?.ToList();

            if (packageDocumentsType is null || packageDocumentsType.Count() == 0)
            {
                Session.Delete(PackageDocumentsType);
            }
            else
            {
                foreach (var currentPackageDocumentType in packageDocumentsType)
                {
                    var document = currentPackageDocumentType.Document;
                    if (document != null)
                    {
                        var currentDocument = PackageDocumentsType
                            .FirstOrDefault(f =>
                                f.Document != null
                                && f.Document.Equals(document));
                        
                        if (currentDocument is null)
                        {
                            currentDocument = currentPackageDocumentType;                           
                            PackageDocumentsType.Add(currentDocument);
                        }

                        currentDocument.PackageDocument = this;
                        currentDocument.Document = document;

                        currentDocument.IsFormSent = currentPackageDocumentType.IsFormSent;
                        currentDocument.DateReceivingForm = currentPackageDocumentType.DateReceivingForm;

                        currentDocument.DateReceivingScan = currentPackageDocumentType.DateReceivingScan;
                        currentDocument.IsSignedScanReceived = currentPackageDocumentType.IsSignedScanReceived;

                        currentDocument.DateReceivingOriginal = currentPackageDocumentType.DateReceivingOriginal;
                        currentDocument.IsSignedOriginalReceived = currentPackageDocumentType.IsSignedOriginalReceived;

                        currentDocument.DateSentRevision = currentPackageDocumentType.DateSentRevision;
                        currentDocument.IsSentRevision = currentPackageDocumentType.IsSentRevision;

                        currentDocument.DateScanAfterRevision = currentPackageDocumentType.DateScanAfterRevision;
                        currentDocument.IsScanAfterRevision = currentPackageDocumentType.IsScanAfterRevision;

                        currentDocument.DateOriginalAfterRevision = currentPackageDocumentType.DateOriginalAfterRevision;
                        currentDocument.IsOriginalAfterRevision = currentPackageDocumentType.IsOriginalAfterRevision;

                        var newPackageDocumentStatus = await currentDocument.GetStatusAsync();
                        if (newPackageDocumentStatus?.Oid != currentDocument.PackageDocumentStatus?.Oid)
                        {
                            await PackagesDocumentController.CreatePackageDocumentChronicleAsync(Session, user, currentDocument, newPackageDocumentStatus);
                            currentDocument.PackageDocumentStatus = newPackageDocumentStatus;
                        }

                        currentDocument.CustomerStaff = currentPackageDocumentType.CustomerStaff;

                        currentDocument.Save();
                    }
                }

                var use = PackageDocumentsType?.Select(s => s.Document)?.ToList();
                var delete = use.Except(documents)?.ToList();

                var deleteObjs = PackageDocumentsType
                    ?.Where(w => delete.Contains(w.Document))
                    ?.ToList();

                foreach (var obj in deleteObjs)
                {
                    obj.Delete();
                }
            }

            Save();
        }

        public void FillPackageDocumentObj(IEnumerable<PackageDocumentObj> packageDocumentObj)
        {
            var files = packageDocumentObj.Select(s => s.File)?.ToList();

            if (files is null || files.Count() == 0)
            {
                Session.Delete(PackageDocumentObjs);
            }
            else
            {
                foreach (var currentPackageDocumentObj in packageDocumentObj)
                {
                    var file = currentPackageDocumentObj.File;
                    if (file != null)
                    {
                        var currentFile = PackageDocumentObjs
                            .FirstOrDefault(f =>
                                f.File != null
                                && f.FileName.Equals(currentPackageDocumentObj.FileName ?? default));

                        if (currentFile is null)
                        {
                            currentFile = currentPackageDocumentObj;
                            PackageDocumentObjs.Add(currentFile);
                        }
                        
                        currentFile.File = currentPackageDocumentObj.File;
                        currentFile.PackageDocument = this;
                        currentFile.IsOriginalDocument = currentPackageDocumentObj.IsOriginalDocument;
                        currentFile.IsScannedDocument = currentPackageDocumentObj.IsScannedDocument;
                        currentFile.DateDeparture = currentPackageDocumentObj.DateDeparture;
                        currentFile.DateReceiving = currentPackageDocumentObj.DateReceiving;

                        currentFile.Save();
                    }
                }

                var use = PackageDocumentObjs?.Select(s => s.File)?.ToList();
                var delete = use.Except(files)?.ToList();

                var deleteObjs = PackageDocumentObjs
                    ?.Where(w => delete.Contains(w.File))
                    ?.ToList();

                foreach (var obj in deleteObjs)
                {
                    obj.Delete();
                }
            }

            Save();
        }

        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<PackageDocumentCustomerStaffObj> PackageDocumentCustomerStaffsObj
        {
            get
            {
                return GetCollection<PackageDocumentCustomerStaffObj>(nameof(PackageDocumentCustomerStaffsObj));
            }
        }

        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<PackageDocumentObj> PackageDocumentObjs
        {
            get
            {
                return GetCollection<PackageDocumentObj>(nameof(PackageDocumentObjs));
            }
        }

        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<PackageDocumentType> PackageDocumentsType
        {
            get
            {
                return GetCollection<PackageDocumentType>(nameof(PackageDocumentsType));
            }
        }

        public override string ToString()
        {
            return CustomerName;
        }
    }
}
