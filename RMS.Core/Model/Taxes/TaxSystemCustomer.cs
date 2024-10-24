using DevExpress.Xpo;
using System;
using System.Linq;

namespace RMS.Core.Model.Taxes
{
    /// <summary>
    /// Система налогообложения клиента.
    /// </summary>
    public class TaxSystemCustomer : XPObject
    {
        public TaxSystemCustomer() { }
        public TaxSystemCustomer(Session session) : base(session) { }
        
        [DisplayName("Текущая система налогообложения")]
        public string CurrentTaxSystemString => TaxSystem?.ToString();

        [MemberDesignTimeVisibility(false)]
        public TaxSystem TaxSystem
        {
            get
            {
                var taxSystem = default(TaxSystem);

                if (TaxSystemCustomerObjects!= null && TaxSystemCustomerObjects.Count > 0)
                {
                    //TODO: тут надо подумать как оптимизировать поиск интервалов дат.
                    var dateNow = DateTime.Now.Date;
                    taxSystem = TaxSystemCustomerObjects.FirstOrDefault(f => 
                        (f.DateSince == null && f.DateTo >= dateNow)
                        || (f.DateSince <= dateNow && f.DateTo >= dateNow)
                        || (f.DateSince <= dateNow && f.DateTo == null)
                        || (f.DateSince == null && f.DateTo == null)
                        )?.TaxSystem;
                }
                
                return taxSystem;
            }
        }
        
        /// <summary>
        /// Комментарий.
        /// </summary>
        [DisplayName("Комментарий"), Size(1024)]
        public string Comment { get; set; }

        /// <summary>
        /// Коллекция всех возможных систем налогообложения клиента.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<TaxSystemCustomerObject> TaxSystemCustomerObjects
        {
            get
            {
                return GetCollection<TaxSystemCustomerObject>(nameof(TaxSystemCustomerObjects));
            }
        }        

        public override string ToString()
        {
            var result = default(string);

            if (TaxSystem is null)
            {
                result = "Система налогообложения отсутствует";
            }
            else
            {
                result = TaxSystem?.ToString();
            }            

            return result;
        }
    }
}
