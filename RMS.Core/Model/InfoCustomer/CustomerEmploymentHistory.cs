using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer;

namespace RMS.Core.Model
{
    /// <summary>
    /// Трудовая книжка.
    /// </summary>
    public class CustomerEmploymentHistory : EmploymentHistory
    {
        public CustomerEmploymentHistory() { }
        public CustomerEmploymentHistory(Session session) : base(session) { }
                
        [Association, MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        public override string ToString()
        {
            var result = default(string);

            if (!string.IsNullOrWhiteSpace(Surname))
            {
                result += Surname;

                if (!string.IsNullOrWhiteSpace(Name))
                {
                    result += $" {Name.Substring(0, 1).ToUpper()}.";
                }

                if (!string.IsNullOrWhiteSpace(Patronymic))
                {
                    result += $" {Patronymic.Substring(0, 1).ToUpper()}.";
                }
            }

            return result;
        }
    }
}