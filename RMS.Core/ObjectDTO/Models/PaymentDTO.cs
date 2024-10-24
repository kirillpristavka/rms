using RMS.Core.Enumerator;
using System;

namespace RMS.Core.ObjectDTO.Models
{
    public class PaymentDTO : BaseObjectDTO
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public TypePayment? TypePayment { get; set; }
    }
}