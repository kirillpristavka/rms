using DevExpress.Xpo;
using System;

namespace RMS.Core.Model.XPO
{
    public class LoggerXPO : BaseObjectXPO<LoggerXPO>
    {
        public LoggerXPO() : base() { }
        public LoggerXPO(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            CreatedDate = DateTime.Now;
        }

        [Persistent]
        [DisplayName("Дата создания")]
        public DateTime CreatedDate { get; private set; }

        [Size(4000)]
        [DisplayName("Сообщение")]
        public string Message { get; set; }
    }
}
