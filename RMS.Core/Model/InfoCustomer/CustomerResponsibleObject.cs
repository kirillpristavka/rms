using DevExpress.Xpo;
using RMS.Core.Enumerator;
using System;

namespace RMS.Core.Model.InfoCustomer
{
    public class CustomerResponsibleObject : XPObject
    {
        private CustomerResponsibleObject() { }
        private CustomerResponsibleObject(Session session) : base(session) { }
        public CustomerResponsibleObject(Session session, ResponsibleOption responsibleOption) : base(session) 
        {
            ResponsibleOption = responsibleOption;
        }

        [DisplayName("Ответственный")]
        public string StaffString => Staff?.ToString();

        [MemberDesignTimeVisibility(false)]
        public Staff Staff { get; set; }

        /// <summary>
        /// Вариант ответственного.
        /// </summary>
        [DisplayName("Дата создания")]
        [MemberDesignTimeVisibility(false)]
        public ResponsibleOption ResponsibleOption { get; set; }

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
        public CustomerResponsible CustomerResponsible { get; set; }

        public override string ToString()
        {
            return StaffString;
        }
    }
}
