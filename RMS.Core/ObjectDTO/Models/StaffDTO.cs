using System;

namespace RMS.Core.ObjectDTO.Models
{
    public class StaffDTO : BaseObjectDTO
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime? DateBirth { get; set; }
        public DateTime? DateReceipt { get; set; }
        public DateTime? DateDismissal { get; set; }
        public string OrderNumber { get; set; }
        public string ContractNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
