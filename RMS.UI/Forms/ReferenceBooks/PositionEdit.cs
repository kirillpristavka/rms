using DevExpress.Xpo;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using RMS.Core.Model;
using RMS.Core.Model.Access;
using System;
using System.Diagnostics;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class PositionEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private Position Position { get; }

        public PositionEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                Position = new Position(Session);
            }
        }

        public PositionEdit(int id) : this()
        {
            if (id > 0)
            {
                Position = Session.GetObjectByKey<Position>(id);
            }
        }

        public PositionEdit(Position position) : this()
        {
            Session = position.Session;
            Position = position;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Position.Name = txtName.Text;
            Position.Description = memoDescription.Text;

            Position.AccessRights.Save();
            Session.Save(Position);
            id = Position.Oid;
            flagSave = true;
            Close();
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            memoDescription.Text = Position.Description;
            txtName.Text = Position.Name;

            if (Position.AccessRights is null)
            {
                Position.AccessRights = new AccessRights(Session);
                Position.AccessRights.Save();
            }

            RepositoryItemCheckEdit riCheckEdit = new RepositoryItemCheckEdit();
            riCheckEdit.CheckStyle = CheckStyles.Standard;
            propertyGridAccess.DefaultEditors.Add(typeof(bool), riCheckEdit);

            propertyGridAccess.SelectedObject = Position.AccessRights;
            UpdatePropertyGrid();

        }

        private void btnAllowAll_Click(object sender, EventArgs e)
        {
            SetAccessRights(true);
        }

        private void btnBanEverything_Click(object sender, EventArgs e)
        {
            SetAccessRights(false);
        }

        private void SetAccessRights(bool value)
        {
            var property = Position.AccessRights.GetType().GetProperties();
            foreach (var item in property)
            {
                try
                {
                    if (item.PropertyType == typeof(bool) && item.GetSetMethod() != null)
                    {
                        item.SetValue(Position.AccessRights, Convert.ChangeType(value, item.PropertyType));
                    }
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                }
            }

            UpdatePropertyGrid();
        }

        private void UpdatePropertyGrid()
        {
            try
            {
                propertyGridAccess.UpdateData();

                var row = propertyGridAccess.GetRowByCaption(nameof(XPObject.Oid));
                if (row != null)
                {
                    row.Visible = false;
                }

                row = propertyGridAccess.GetRowByCaption("Прочее");
                if (row != null)
                {
                    row.Visible = false;
                }

                propertyGridAccess.RecordWidth = 25;
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }
    }
}