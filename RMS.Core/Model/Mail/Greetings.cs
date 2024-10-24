using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer;
using System;

namespace RMS.Core.Model.Mail
{
    /// <summary>
    /// Приветствие.
    /// </summary>
    public class Greetings : XPObject
    {
        public Greetings() { }
        public Greetings(Session session) : base(session) { }

        public DateTime Date { get; set; }

        public User User { get; set; }
        
        public string Email { get; set; }
    }
}
