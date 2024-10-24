using DevExpress.Xpo;

namespace RMS.Core.Model
{
    /// <summary>
    /// Платежи по счету.
    /// </summary>
    public class InvoicePayment : Payment
    {
        public InvoicePayment() { }
        public InvoicePayment(Session session) : base(session) { }

        [Association, MemberDesignTimeVisibility(false)]
        public Invoice Invoice { get; set; }
    }
}