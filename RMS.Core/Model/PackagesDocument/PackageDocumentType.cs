using DevExpress.Xpo;
using RMS.Core.Controllers.PackagesDocument;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Linq;

namespace RMS.Core.Model.PackagesDocument
{
    public class PackageDocumentType : XPObject
    {
        public PackageDocumentType() { }
        public PackageDocumentType(Session session) : base(session) { }
        
        public string Customer => PackageDocument?.CustomerName;

        public string Name => Document?.ToString();
        public Document Document { get; set; }

        public string CustomerStaffName => CustomerStaff?.ToString();
        public CustomerStaff CustomerStaff { get; set; }

        public string Status
        {
            get
            {
                var packageDocumentStatus = PackageDocumentStatus?.ToString();

                if (!string.IsNullOrWhiteSpace(packageDocumentStatus) && packageDocumentStatus.Contains("Задержка"))
                {
                    var lastAction = PackageDocumentChronicle?.LastOrDefault(l => l.Name.Contains("Изменение"));
                    if (lastAction != null)
                    {
                        var dateTimeNow = DateTime.Now;
                        var dateAction = lastAction.Date;
                        
                        var countDelayDays = (dateTimeNow - dateAction).Days + 1;
                        return $"{packageDocumentStatus} ({countDelayDays} дн.)";
                    }
                }

                return packageDocumentStatus;
            }
        }
        public PackageDocumentStatus PackageDocumentStatus { get; set; }
        
        /// <summary>
        /// Бланк направлен заказчику.
        /// </summary>
        [DisplayName("Бланк направлен заказчику")]
        public bool IsFormSent { get; set; }
        /// <summary>
        /// Дата получения.
        /// </summary>
        [DisplayName("Дата направления")]
        public DateTime? DateReceivingForm { get; set; }

        /// <summary>
        /// Получен скан с подписью.
        /// </summary>
        [DisplayName("Получен скан с подписью")]
        public bool IsSignedScanReceived { get; set; }
        /// <summary>
        /// Дата получения.
        /// </summary>
        [DisplayName("Дата получения")]
        public DateTime? DateReceivingScan { get; set; }

        /// <summary>
        /// Получен оригинал.
        /// </summary>
        [DisplayName("Получен оригинал")]
        public bool IsSignedOriginalReceived { get; set; }
        /// <summary>
        /// Дата получения.
        /// </summary>
        [DisplayName("Дата получения")]
        public DateTime? DateReceivingOriginal { get; set; }

        /// <summary>
        /// Отправлено на доработку.
        /// </summary>
        [DisplayName("Отправлено на доработку")]
        public bool IsSentRevision { get; set; }
        /// <summary>
        /// Дата отправления.
        /// </summary>
        [DisplayName("Дата отправления")]
        public DateTime? DateSentRevision { get; set; }

        /// <summary>
        /// Скан после доработки.
        /// </summary>
        [DisplayName("Скан после доработки")]
        public bool IsScanAfterRevision { get; set; }
        /// <summary>
        /// Дата получения.
        /// </summary>
        [DisplayName("Дата получения")]
        public DateTime? DateScanAfterRevision { get; set; }

        /// <summary>
        /// Оригинал после доработки.
        /// </summary>
        [DisplayName("Оригинал после доработки")]
        public bool IsOriginalAfterRevision { get; set; }
        /// <summary>
        /// Дата получения.
        /// </summary>
        [DisplayName("Дата получения")]
        public DateTime? DateOriginalAfterRevision { get; set; }

        [Association]
        public PackageDocument PackageDocument { get; set; }
        
        public PackageDocumentTypeColor GetStatusColor()
        {
            if (IsOriginalAfterRevision)
            {
                return PackageDocumentTypeColor.LightGreen;
            }
            else if (IsScanAfterRevision)
            {
                return PackageDocumentTypeColor.LightOrange;
            }
            else if (IsSentRevision)
            {
                return PackageDocumentTypeColor.LightRed;
            }
            else if (IsSignedOriginalReceived)
            {
                return PackageDocumentTypeColor.LightGreen;
            }
            else if (IsSignedScanReceived)
            {
                return PackageDocumentTypeColor.LightOrange;
            }
            else if (IsFormSent)
            {
                return PackageDocumentTypeColor.LightRed;
            }

            return PackageDocumentTypeColor.LightGray;
        }

        public async System.Threading.Tasks.Task<PackageDocumentStatus> GetStatusAsync()
        {
            if (IsOriginalAfterRevision)
            {
                return await PackagesDocumentController.GetPackageDocumentStatusAsync(Session, "Завершен (доработка)");
            }
            else if (IsScanAfterRevision)
            {
                return await PackagesDocumentController.GetPackageDocumentStatusAsync(Session, "Ожидание оригинала (доработка)");
            }
            else if (IsSentRevision)
            {
                return await PackagesDocumentController.GetPackageDocumentStatusAsync(Session, "Ожидание копии (доработка)");
            }
            else if (IsSignedOriginalReceived)
            {
                return await PackagesDocumentController.GetPackageDocumentStatusAsync(Session, "Завершен");
            }
            else if (IsSignedScanReceived)
            {
                return await PackagesDocumentController.GetPackageDocumentStatusAsync(Session, "Ожидание оригинала");
            }
            else if (IsFormSent)
            {
                return await PackagesDocumentController.GetPackageDocumentStatusAsync(Session, "Ожидание копии");
            }

            return await PackagesDocumentController.GetPackageDocumentStatusAsync(Session, "Ожидание отправки бланка");
        }

        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<PackageDocumentChronicle> PackageDocumentChronicle
        {
            get
            {
                return GetCollection<PackageDocumentChronicle>(nameof(PackageDocumentChronicle));
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}