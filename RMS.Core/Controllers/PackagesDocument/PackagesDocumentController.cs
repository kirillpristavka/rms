using DevExpress.Xpo;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.PackagesDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMS.Core.Controllers.PackagesDocument
{
    /// <summary>
    /// Контроллер управления почтовыми сообщениями.
    /// </summary>
    public static class PackagesDocumentController
    {
        public static async Task<IEnumerable<PackageDocument>> GetPackagesDocumentAsync()
        {
            using (var uof = new UnitOfWork())
            {
                return await new XPQuery<PackageDocument>(uof)?.OrderBy(o => o.Date)?.ToListAsync();
            }
        }

        public static async Task<IEnumerable<PackageDocument>> GetPackagesDocumentAsync(Session session)
        {
            return await new XPQuery<PackageDocument>(session)?.OrderBy(o => o.Date)?.ToListAsync();
        }

        public static async Task<IEnumerable<PackageDocumentType>> GetPackagesDocumentTypeAsync(PackageDocument packageDocument)
        {
            using (var uof = new UnitOfWork())
            {
                return await new XPQuery<PackageDocumentType>(uof)
                    ?.Where(w => w.PackageDocument != null && w.PackageDocument.Oid == packageDocument.Oid)
                    ?.ToListAsync();
            }
        }

        public static async Task<IEnumerable<PackageDocumentType>> GetPackagesDocumentTypeAsync(CustomerStaff customerStaff)
        {
            return await new XPQuery<PackageDocumentType>(customerStaff.Session)
                     ?.Where(w => w.CustomerStaff != null && w.CustomerStaff.Oid == customerStaff.Oid)
                     ?.ToListAsync();
        }

        public static async Task<IEnumerable<PackageDocumentType>> GetPackagesDocumentTypeAsync(Session session, PackageDocument packageDocument)
        {
            if (packageDocument is null)
            {
                return new List<PackageDocumentType>();
            }
            
            return await new XPQuery<PackageDocumentType>(session)
                    ?.Where(w => w.PackageDocument != null && w.PackageDocument.Oid == packageDocument.Oid)
                    ?.ToListAsync();
        }

        public static async Task<IEnumerable<PackageDocumentType>> GetPackagesDocumentTypeAsync(Session session, CustomerStaff customerStaff)
        {
            if (customerStaff is null)
            {
                return new List<PackageDocumentType>();
            }

            return await new XPQuery<PackageDocumentType>(session)
                    ?.Where(w => w.CustomerStaff != null && w.CustomerStaff.Oid == customerStaff.Oid)
                    ?.ToListAsync();
        }

        public static async Task<PackageDocumentStatus> GetPackageDocumentStatusAsync(Session session, string name)
        {
            var status = await new XPQuery<PackageDocumentStatus>(session)
                ?.FirstOrDefaultAsync(f => f.Name != null && f.Name.Equals(name));

            if (status is null)
            {
                using (var uof = new UnitOfWork())
                {
                    status = new PackageDocumentStatus(uof)
                    {
                        Name = name,
                        Description = name
                    };
                    status.Save();

                    await uof.CommitTransactionAsync();
                }

                status = await session.GetObjectByKeyAsync<PackageDocumentStatus>(status?.Oid);
            }

            return status;
        }


        public static async System.Threading.Tasks.Task CreatePackageDocumentChronicleAsync(
            Session session,
            User user,
            PackageDocumentType currentDocument,
            PackageDocumentStatus newPackageDocumentStatus,
            string name = "Изменение статуса документа")
        {
            var userOid = user?.Oid ?? -1;
            var currentUser = await new XPQuery<User>(session)?.FirstOrDefaultAsync(f => f.Oid == userOid);
            var packageDocumentChronicle = new PackageDocumentChronicle(session)
            {
                PackageDocumentType = currentDocument,
                Document = currentDocument.Document,
                Name = name,
                User = currentUser,
                Staff = currentUser?.Staff,
                PackageDocumentStatusNew = newPackageDocumentStatus,
                PackageDocumentStatusOld = currentDocument.PackageDocumentStatus
            };

            var @event = default(string);

            if (packageDocumentChronicle.Staff != null)
            {
                @event = $"Сотрудник {packageDocumentChronicle.Staff} изменил статус документа";
            }
            else if (packageDocumentChronicle.User != null)
            {
                @event = $"Пользователь {packageDocumentChronicle.User} изменил статус документа";
            }
            else
            {
                @event = $"Система СКиД изменила статус документа";
            }

            if (currentDocument.Document != null)
            {
                @event += $" <{currentDocument.Document}>";
            }

            if (currentDocument.PackageDocumentStatus != null)
            {
                @event += $" с [{currentDocument.PackageDocumentStatus}]";
            }

            if (newPackageDocumentStatus != null)
            {
                @event += $" на [{newPackageDocumentStatus}]";
            }

            packageDocumentChronicle.SetEvent(@event);
        }

        public static async System.Threading.Tasks.Task CheckingDocumentStatusAsync(DateTime? date = null)
        {
            if (date is null)
            {
                date = DateTime.Now.Date;
            }

            using (var uof = new UnitOfWork())
            {
                var documents = await new XPQuery<PackageDocumentType>(uof)
                    ?.Where(w => w.PackageDocumentStatus != null && !w.PackageDocumentStatus.Name.Contains("Завершен"))
                    ?.ToListAsync();

                foreach (var document in documents)
                {
                    var lastAction = document.PackageDocumentChronicle?.LastOrDefault();
                    if (lastAction != null)
                    {
                        var dateLastAction = lastAction.Date.Date;
                        if (document.IsScanAfterRevision || document.IsSignedScanReceived)
                        {
                            if (dateLastAction.AddDays(30) <= date)
                            {
                                var newPackageDocumentStatus = await GetPackageDocumentStatusAsync(uof, "Задержка клиентом");                                                                
                                if (newPackageDocumentStatus?.Oid != document.PackageDocumentStatus?.Oid)
                                {
                                    await CreatePackageDocumentChronicleAsync(uof, null, document, newPackageDocumentStatus, "Задержка документа более 30 дней");
                                    document.PackageDocumentStatus = newPackageDocumentStatus;
                                }
                            }
                        }
                        else if (document.IsSentRevision || document.IsFormSent)
                        {
                            if (dateLastAction.AddDays(7) <= date)
                            {
                                var newPackageDocumentStatus = await GetPackageDocumentStatusAsync(uof, "Задержка клиентом");
                                if (newPackageDocumentStatus?.Oid != document.PackageDocumentStatus?.Oid)
                                {
                                    await CreatePackageDocumentChronicleAsync(uof, null, document, newPackageDocumentStatus, "Задержка документа более 7 дней");
                                    document.PackageDocumentStatus = newPackageDocumentStatus;
                                }
                            }
                        }
                        else
                        {
                            var newPackageDocumentStatus = await GetPackageDocumentStatusAsync(uof, "Задержка отправки уведомления");
                            if (newPackageDocumentStatus?.Oid != document.PackageDocumentStatus?.Oid)
                            {
                                await CreatePackageDocumentChronicleAsync(uof, null, document, newPackageDocumentStatus, "Задержка отправки уведомления (7 дней)");
                                document.PackageDocumentStatus = newPackageDocumentStatus;
                            }
                        }
                    }

                    document.Save();
                    await uof.CommitTransactionAsync().ConfigureAwait(false);
                }
            }
        }

        public static async Task<List<PackageDocumentChronicle>> GetPackageDocumentChronicleAsync(Session session, PackageDocument packageDocument)
        {
            var result = new List<PackageDocumentChronicle>();

            var packageDocumentOid = packageDocument?.Oid ?? -1;
            var currentPackageDocument = await new XPQuery<PackageDocument>(session).FirstOrDefaultAsync(f => f.Oid == packageDocumentOid);
            if (currentPackageDocument != null)
            {
                if (currentPackageDocument.PackageDocumentsType != null)
                {
                    foreach (var document in currentPackageDocument.PackageDocumentsType)
                    {
                        document?.PackageDocumentChronicle?.Reload();
                        if (document.PackageDocumentChronicle != null)
                        {
                            foreach (var chronicle in document.PackageDocumentChronicle)
                            {
                                result.Add(chronicle);
                            }
                        }
                    }
                }
            }

            result = result.OrderByDescending(o => o.Date).ToList();
            return result;
        }

        public static async Task<List<PackageDocumentChronicle>> GetPackageDocumentChronicleAsync(Session session, CustomerStaff customerStaff)
        {
            var result = new List<PackageDocumentChronicle>();

            var customerStaffOid = customerStaff?.Oid ?? -1;
            var currentPackageDocument = await new XPQuery<PackageDocumentType>(session)
                ?.Where(f => f.CustomerStaff != null && f.CustomerStaff.Oid == customerStaffOid)
                ?.ToListAsync();
            if (currentPackageDocument != null)
            {
                foreach (var document in currentPackageDocument)
                {
                    document?.PackageDocumentChronicle?.Reload();
                    if (document.PackageDocumentChronicle != null)
                    {
                        foreach (var chronicle in document.PackageDocumentChronicle)
                        {
                            result.Add(chronicle);
                        }
                    }
                }
            }

            result = result.OrderByDescending(o => o.Date).ToList();
            return result;
        }
    }
}
