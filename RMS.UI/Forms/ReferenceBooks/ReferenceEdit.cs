using DevExpress.Xpo;
using System;
using System.Reflection;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class ReferenceEdit<T> : formEdit_BaseSpr where T : XPObject, new()
    {
        private Session Session { get; }
        private T Data { get; }

        public ReferenceEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                /*тут разобраться с сессией */
                Data = (T)Activator.CreateInstance(typeof(T), Session);
            }
        }

        public ReferenceEdit(int id) : this()
        {
            if (id > 0)
            {
                Data = Session.GetObjectByKey<T>(id);
            }
        }

        public ReferenceEdit(T data) : this()
        {
            Session = data.Session;
            Data = data;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var nameField = typeof(T).GetProperty("Name", BindingFlags.Public | BindingFlags.Instance);
            nameField.SetValue(Data, txtName.Text);

            var descriptionField = typeof(T).GetProperty("Description", BindingFlags.Public | BindingFlags.Instance);
            descriptionField.SetValue(Data, memoDescription.Text);

            Session.Save(Data);
            id = Data.Oid;
            flagSave = true;
            Close();
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            txtName.Text = typeof(T).InvokeMember("Name", BindingFlags.GetField | BindingFlags.GetProperty, null, Data, null)?.ToString();
            memoDescription.Text = typeof(T).InvokeMember("Description", BindingFlags.GetField | BindingFlags.GetProperty, null, Data, null)?.ToString();
        }
    }
}