using System;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms.ConnectionForm
{
    public partial class AuthorizationForm : Form
    {
        public AuthorizationForm()
        {
            InitializeComponent();
        }

        private void AuthorizationForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                lblAbbreviatedName.Capture = false;
                lblFullName.Capture = false;
                Capture = false;
                Opacity = 0.6;
                Message m = Message.Create(Handle, 161, new IntPtr(2), IntPtr.Zero);
                WndProc(ref m);
                if (e.Button != MouseButtons.Right)
                {
                    Opacity = 1;
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
