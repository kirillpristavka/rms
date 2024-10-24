using DevExpress.Xpo;
using System;

namespace RMS.Core.Model.Salary
{
    public class SalaryAndAdvanceObject : XPObject
    {
        public SalaryAndAdvanceObject() { }
        public SalaryAndAdvanceObject(Session session) : base(session) { }

        [MemberDesignTimeVisibility(false)]
        public string ObjectString => Day?.ToString();
        
        [DisplayName("День")]
        public int? Day { get; set; }
        
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
        public SalaryAndAdvance AssociationObject { get; set; }

        public override string ToString()
        {
            return ObjectString;
        }
    }
}
