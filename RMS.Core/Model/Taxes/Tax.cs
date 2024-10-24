using DevExpress.Xpo;

namespace RMS.Core.Model.Taxes
{
    /// <summary>
    /// Налоги.
    /// </summary>
    public class Tax : XPObject
    {
        public Tax() { }
        public Tax(Session session) : base(session) { }

        /// <summary>
        /// Налог на алкоголь.
        /// </summary>
        [Aggregated]
        public TaxAlcohol TaxAlcohol { get; set; }

        /// <summary>
        /// Налог на прибыль.
        /// </summary>
        [Aggregated]
        public TaxIncome TaxIncome { get; set; }

        /// <summary>
        /// Косвенные налоги.
        /// </summary>
        [Aggregated]
        public TaxIndirect TaxIndirect { get; set; }

        /// <summary>
        /// Земельный налог.
        /// </summary>
        [Aggregated]
        public TaxLand TaxLand { get; set; }

        /// <summary>
        /// Налог на имущество.
        /// </summary>
        [Aggregated]
        public TaxProperty TaxProperty { get; set; }

        /// <summary>
        /// Транспортный налог.
        /// </summary>
        [Aggregated]
        public TaxTransport TaxTransport { get; set; }

        /// <summary>
        /// Патент.
        /// </summary>
        [Aggregated]
        public Patent Patent { get; set; }

        /// <summary>
        /// Единый налог на временный доход (ЕНВД).
        /// </summary>
        [Aggregated]
        public TaxSingleTemporaryIncome TaxSingleTemporaryIncome { get; set; }

        /// <summary>
        /// Единый налог на временный доход (ЕНВД).
        /// </summary>
        [Aggregated]
        public TaxImportEAEU TaxImportEAEU { get; set; }

        public override string ToString()
        {
            var result = default(string);

            if (TaxAlcohol != null)
            {
                //result += $"Налог на алкоголь: {TaxAlcohol}; ";
                var isUse = (TaxAlcohol.IsUse == true) ? "+" : "-";
                result += $"[А: {isUse}]; ";
            }

            if (TaxIncome != null)
            {
                //result += $"Налог на прибыль: {TaxIncome}; ";
                var isUse = (TaxIncome.IsUse == true) ? "+" : "-";
                result += $"[П: {isUse}]; ";
            }

            if (TaxIndirect != null)
            {
                //result += $"Косвенный налог: {TaxIndirect}; ";
                var isUse = (TaxIndirect.IsUse == true) ? "+" : "-";
                result += $"[К: {isUse}]; ";
            }

            if (TaxProperty != null)
            {
                //result += $"Налог на имущество: {TaxProperty}; ";
                var isUse = (TaxProperty.IsUse == true) ? "+" : "-";
                result += $"[И: {isUse}]; ";
            }

            if (TaxTransport != null)
            {
                //result += $"Транспортный налог: {TaxTransport}; ";
                var isUse = (TaxTransport.IsUse == true) ? "+" : "-";
                result += $"[Т: {isUse}]; ";
            }

            if (TaxLand != null)
            {
                //result += $"Земельный налог: {TaxTransport}; ";
                var isUse = (TaxLand.IsUse == true) ? "+" : "-";
                result += $"[З: {isUse}]; ";
            }

            //if (TaxSingleTemporaryIncome != null)
            //{
            //    //result += $"ЕНВД: {TaxTransport}; ";
            //    var isUse = (TaxSingleTemporaryIncome.IsUse == true) ? "+" : "-";
            //    result += $"[ЕНВД: {isUse}]; ";
            //}

            return result;
        }
    }
}
