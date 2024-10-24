using DevExpress.Xpo;
using RMS.Core.Model.OKVED;

namespace RMS.Core.Model
{
    /// <summary>
    /// Вид деятельности.
    /// </summary>
    public class KindActivity : XPObject
    {
        public KindActivity() { }
        public KindActivity(Session session) : base(session) { }

        /// <summary>
        /// ОКВЭД2.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public ClassOKVED2 ClassOKVED2 { get; set; }

        /// <summary>
        /// Регистрация по месту нахождения организации.
        /// </summary>
        public bool IsRegistrationAtLocationOrganization { get; set; }

        /// <summary>
        /// Базовая доходность.
        /// </summary>
        public decimal BasicReturn { get; set; }

        /// <summary>
        /// Физический показатель.
        /// </summary>
        public PhysicalIndicator PhysicalIndicator { get; set; }

        public override string ToString()
        {
            return ClassOKVED2?.ToString();
        }
    }
}