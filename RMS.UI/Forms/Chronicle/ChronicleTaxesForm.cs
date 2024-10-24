using DevExpress.Xpo;
using DevExpress.XtraEditors;
using PulsLibrary.Extensions.DevForm;

namespace RMS.UI.Forms.Chronicle
{
    public partial class ChronicleTaxesForm<T> : XtraForm
    {
        public ChronicleTaxesForm(XPCollection<T> xpCollectionChronicle, string caption)
        {
            InitializeComponent();
            Text = $"{Text} {caption}";
            gridControlChronicle.DataSource = xpCollectionChronicle;

            if (gridViewChronicle.Columns[nameof(XPObject.Oid)] != null)
            {
                gridViewChronicle.Columns[nameof(XPObject.Oid)].Visible = false;
            }

            gridViewChronicle.GridViewSetup(isShowFooter: false, isColumnAutoWidth: false);

            gridViewChronicle.BestFitColumns();
        }

        private void ChronicleTaxesForm_Load(object sender, System.EventArgs e)
        {
            Icon = Properties.Resources.IconRMS;
        }
    }
}