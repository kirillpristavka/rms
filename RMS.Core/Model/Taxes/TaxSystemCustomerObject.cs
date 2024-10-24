using DevExpress.Xpo;
using System;

namespace RMS.Core.Model.Taxes
{
    /// <summary>
    /// Объекты клиентской системы налогообложения.
    /// </summary>
    public class TaxSystemCustomerObject : XPObject
    {
        public TaxSystemCustomerObject() { }
        public TaxSystemCustomerObject(Session session) : base(session) { }

        [DisplayName("Система налогообложения")]
        public string TaxSystemString => TaxSystem?.ToString();

        [MemberDesignTimeVisibility(false)]
        public TaxSystem TaxSystem { get; set; }
                
        /// <summary>
        /// Пользователь, который создал.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public User UserCreate { get; set; }

        /// <summary>
        /// Дата создания.
        /// </summary>        
        [DisplayName("Дата создания")]
        [MemberDesignTimeVisibility(false)]
        public DateTime DateCreate { get; set; } = DateTime.Now;        

        [DisplayName("Дата начала действия")]
        public DateTime? DateSince { get; set; }

        [DisplayName("Дата окончания действия")]
        public DateTime? DateTo { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public TaxSystemCustomer TaxSystemCustomer { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}