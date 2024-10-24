using DevExpress.Xpo;
using RMS.Core.Extensions;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RMS.Core.Controllers.PackagesDocument
{
    /// <summary>
    /// Контроллер управления почтовыми сообщениями.
    /// </summary>
    public static class PackagesDocumentInfoController
    {
        public static async Task GetInfoFileAsync(Customer customer, CustomerStaff customerStaff = null)
        {
            string fileName = GetTempFilePathWithExtension("html");
            System.IO.File.WriteAllText(fileName, await GetInfoFileToSendAsync(customer, customerStaff));
            System.Diagnostics.Process.Start(fileName);
        }

        public static async Task<string> GetInfoFilePathAsync(Customer customer, CustomerStaff customerStaff = null)
        {
            string fileName = GetTempFilePathWithExtension("html");
            System.IO.File.WriteAllText(fileName, await GetInfoFileToSendAsync(customer, customerStaff));
            return fileName;
        }

        private static async Task<string> GetInfoFileToSendAsync(Customer customer, CustomerStaff customerStaff = null, string replaceName = "<!--CUSTOMERSSTAFFS-->")
        {
            using (var uof = new UnitOfWork())
            {
                var title = "(ALGRAS) Информация по клиенту";
                var customerStaffs = await new XPQuery<CustomerStaff>(uof)
                    ?.Where(w => w.Customer != null && w.Customer.Oid == customer.Oid)
                    ?.ToListAsync();
                    
                if (customerStaff != null)
                {
                    customerStaffs = customerStaffs.Where(w => w.Oid == customerStaff.Oid).ToList();
                    title = "(ALGRAS) Информация по сотруднику";
                }

                var obj = default(string);
                foreach (var staff in customerStaffs)
                {
                    obj += await CreateAccordionItemCustomerStaffAsync(staff);
                }

                var result = default(string);
                result = Properties.Resources.PackageDocumentBootstrap.Replace("<!--TITLE-->", title);
                result = result.Replace("<!--CUSTOMER-->", GetCustomerInfo(customer));
                result = result.Replace(replaceName, obj?.Trim());
                return result;
            }
        }

        private static string GetCustomerInfo(Customer customer)
        {
            var dateTime = DateTime.Now;
            return
                $@"<div class='alert alert-primary' role='alert'>
                        <h4 class='alert-heading'>{customer.FullName ?? customer.Name}</h4>
                        <p>ИНН: {customer.INN}<br>КПП: {customer.KPP}</p>
                        <hr>
                        <p class='mb-0'>Дата создания: {dateTime.ToShortDateString()} ({dateTime:HH:mm:ss})</p>
                    </div>";
        }

        private static async Task<string> CreateAccordionItemCustomerStaffAsync(CustomerStaff customerStaff)
        {
            if (customerStaff is null)
            {
                return default;
            }

            var obj = $@"<div class='accordion-item'>
                            <h2 class='accordion-header' id='panelsStayOpen-{nameof(customerStaff)}-heading{customerStaff.Oid}'>
                                <button class='accordion-button' type='button' data-bs-toggle='collapse'
                                    data-bs-target='#panelsStayOpen-{nameof(customerStaff)}-collapse{customerStaff.Oid}' aria-expanded='true'
                                    aria-controls='panelsStayOpen-{nameof(customerStaff)}-collapse{customerStaff.Oid}'>
                                    {customerStaff}
                                </button>
                            </h2>
                            <div id='panelsStayOpen-{nameof(customerStaff)}-collapse{customerStaff.Oid}' class='accordion-collapse collapse show'
                                aria-labelledby='panelsStayOpen-headingOne'>
                                <div class='accordion-body'>
                                    {await GetPackageDocumentTypeInfoAsync(customerStaff)}
                                </div>
                            </div>
                        </div>";

            return obj;
        }

        private static async Task<string> GetPackageDocumentTypeInfoAsync(CustomerStaff customerStaff)
        {
            var replaceName = "<!--PACKAGESDOCUMENTS-->";
            var packagesDocuments= await PackagesDocumentController.GetPackagesDocumentTypeAsync(customerStaff);
            if (packagesDocuments is null || packagesDocuments.Count() == 0)
            {
                return default;
            }

            var obj = $@"<table class='table'>
                            <thead>
                                <tr>
                                    <th scope='col' class='text-center'>Документ</th>
                                    <th scope='col' class='text-center'>Статус</th>
                                    <th scope='col' class='text-center'>Бланк</th>
                                    <th scope='col' class='text-center'>Скан</th>
                                    <th scope='col' class='text-center'>Оригинал</th>
                                    <th scope='col' class='text-center'>Отправлено на доработку</th>
                                    <th scope='col' class='text-center'>Скан после доработки</th>
                                    <th scope='col' class='text-center'>Оригинал после доработки</th>
                                    <th scope='col' class='text-center'>Причина</th>
                                </tr>
                            </thead>
                            <tbody>
                                {replaceName}
                            </tbody>
                        </table>";

            var stringPackagesDocuments = default(string);
            foreach (var packageDocument in packagesDocuments)
            {
                stringPackagesDocuments += 
                                    $@"<tr style='background-color: {packageDocument.GetStatusColor().GetEnumDescription()};'>
                                        <td class='align-middle'>{packageDocument.Document}</td>
                                        <td class='align-middle'>{packageDocument.Status}</td>
                                        <td class='text-center align-middle'>{GetCheckBox(packageDocument.Oid, nameof(packageDocument.IsFormSent), packageDocument.IsFormSent, packageDocument.DateReceivingForm)}</td>
                                        <td class='text-center align-middle'>{GetCheckBox(packageDocument.Oid, nameof(packageDocument.IsSignedScanReceived), packageDocument.IsSignedScanReceived, packageDocument.DateReceivingScan)}</td>
                                        <td class='text-center align-middle'>{GetCheckBox(packageDocument.Oid, nameof(packageDocument.IsSignedOriginalReceived), packageDocument.IsSignedOriginalReceived, packageDocument.DateReceivingOriginal)}</td>
                                        <td class='text-center align-middle'>{GetCheckBox(packageDocument.Oid, nameof(packageDocument.IsSentRevision), packageDocument.IsSentRevision, packageDocument.DateSentRevision)}</td>
                                        <td class='text-center align-middle'>{GetCheckBox(packageDocument.Oid, nameof(packageDocument.IsScanAfterRevision), packageDocument.IsScanAfterRevision, packageDocument.DateScanAfterRevision)}</td>
                                        <td class='text-center align-middle'>{GetCheckBox(packageDocument.Oid, nameof(packageDocument.IsOriginalAfterRevision), packageDocument.IsOriginalAfterRevision, packageDocument.DateOriginalAfterRevision)}</td>
                                        <td class='align-middle'>{packageDocument.PackageDocument?.Description?.Trim()?.Replace("\r\n", "<br>")}</td>
                                    </tr>{Environment.NewLine}";
            }
                        
            return obj.Replace(replaceName, stringPackagesDocuments).Trim();
        }

        private static string GetCheckBox(int oid, string name, bool isChecked, DateTime? dateTime)
        {
            var @checked = default(string);
            if (isChecked)
            {
                @checked = "checked";
            }

            var newLine = default(string);
            var date = default(string);
            if (dateTime is DateTime _dateTime)
            {
                date = _dateTime.ToShortDateString();
                newLine = "<br>";
            }

            return 
                $@"<div class='custom-control custom-checkbox'>
                    <input type = 'checkbox' class='custom-control-input' name='{name}{oid}' id='{name}{oid}' {@checked}>{newLine}
                    <label class='custom-control-label' for='{name}{oid}'>{date}</label>
                </div>";
        }

        private static string GetTempFilePathWithExtension(string extension)
        {
            var path = System.IO.Path.GetTempPath();
            var fileName = System.IO.Path.ChangeExtension(Guid.NewGuid().ToString(), extension);
            return System.IO.Path.Combine(path, fileName);
        }
    }
}
