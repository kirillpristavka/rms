using DevExpress.XtraEditors;
using System.Drawing;

namespace RMS.UI
{
    public partial class formEdit_BaseSpr : XtraForm
    {
        public Icon BaseIcon { get { return Icon; } }
        
        public bool FlagSave { get { return flagSave; } }
        public int Id { get { return id; } }

        protected bool flagSave;
        protected int id = -1;

        public cls_BaseSpr BaseSpr { get; set; }
    }
}