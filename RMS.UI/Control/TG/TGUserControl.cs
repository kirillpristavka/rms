using DevExpress.Xpo;
using DevExpress.XtraEditors;
using RMS.Core.TG.Core.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace RMS.UI.Control.TG
{
    public partial class TGUserControl : XtraUserControl
    {
        public bool IsUse { get; private set; }
        public TGUser TGUser { get; private set; }

        public delegate void ClickEventHandler(object sender, TGUser user);
        public event ClickEventHandler ObjClick;

        public TGUserControl(TGUser user)
        {            
            InitializeComponent();
            
            TGUser = user;
        }

        private async void TGUserControl_Load(object sender, EventArgs e)
        {
            if (TGUser != null)
            {
                lblUserName.Text = GetUserName();
                await GetLastMessage();

                var customer = GetCustomer();
                if (!string.IsNullOrWhiteSpace(customer))
                {
                    lblCustomer.Text = customer;
                }
                else
                {
                    lblUserName.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom;
                    lblCustomer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }

                if (TGUser.Avatar != null)
                {
                    pictureEdit.Properties.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.ByteArray;
                    pictureEdit.EditValue = TGUser.Avatar;
                }
            }
        }

        public async Task<string> GetLastMessage()
        {
            using (var uof = new UnitOfWork())
            {
                var messages = await new XPQuery<TGMessage>(uof)
                    .Where(w => w.TGUserRecipient.Oid == TGUser.Oid || w.TGUserSender.Oid == TGUser.Oid)
                    .ToListAsync();

                var text = messages?.LastOrDefault()?.Text;
                lblMessage.Text = text ?? " ";
                return text;
            }
        }

        private string GetCustomer()
        {
            return default;
        }

        private string GetUserName()
        {
            var userName = default(string);

            if (!string.IsNullOrWhiteSpace(TGUser.UserName))
            {
                userName = TGUser.UserName;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(TGUser.FirstName))
                {
                    userName = TGUser.FirstName;
                }

                if (!string.IsNullOrWhiteSpace(TGUser.LastName))
                {
                    userName += $" {TGUser.LastName}";
                }
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                return TGUser.Id.ToString();
            }
            else
            {
                userName = $"{userName.Trim()} ({TGUser.Id})";
            }
            
            return userName;
        }

        private Color _defaultBackColor = Color.FromArgb(246, 246, 246);
        private Color _useBackColor = Color.FromArgb(246, 246, 220);
        
        private void TGUserControl_Click(object sender, EventArgs e)
        {
            SetUseColor();
        }
        
        public void SetUseColor()
        {
            ObjClick?.Invoke(this, TGUser);

            IsUse = true;
            BackColor = _useBackColor;
            pictureEdit.BackColor = _useBackColor;
        }
        
        public void SetDefaultColor()
        {
            IsUse = false;
            BackColor = _defaultBackColor;
            pictureEdit.BackColor = _defaultBackColor;
        }        
    }
}
