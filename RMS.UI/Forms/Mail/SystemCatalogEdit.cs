using DevExpress.XtraEditors;
using DevExpress.Xpo;
using RMS.UI.Control.Mail;
using RMS.Core.Model;

namespace RMS.UI.Forms.Mail
{
    public partial class SystemCatalogEdit : XtraForm
    {
        public LetterCatalog LetterCatalog { get; private set; }
        public bool isChoice { get; private set; }
        private Session Session { get; }
        
        public SystemCatalogEdit(Session session)
        {
            InitializeComponent();
            Session = session;

            var systemCatalogControl = new SystemCatalogControl(Session, true);
            systemCatalogControl.Choice += SystemCatalogControl_Choice;
            systemCatalogControl.Close += SystemCatalogControl_Close;

            Controls.Add(systemCatalogControl);
        }

        private void SystemCatalogControl_Choice(object sender, int choice)
        {
            LetterCatalog = Session.GetObjectByKey<LetterCatalog>(choice);
        }

        private void SystemCatalogControl_Close(object sender)
        {
            Close();
        }
    }
}