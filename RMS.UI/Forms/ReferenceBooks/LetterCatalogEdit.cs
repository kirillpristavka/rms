using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class LetterCatalogEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private LetterCatalog LetterCatalog { get; }

        public LetterCatalogEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                LetterCatalog = new LetterCatalog(Session);
            }
        }

        public LetterCatalogEdit(int id) : this()
        {
            if (id > 0)
            {
                LetterCatalog = Session.GetObjectByKey<LetterCatalog>(id);
            }
        }

        public LetterCatalogEdit(LetterCatalog letterCatalog) : this()
        {
            Session = letterCatalog.Session;
            LetterCatalog = letterCatalog;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            LetterCatalog.DisplayName = txtName.Text;

            if (btnStaff.EditValue is Staff staff)
            {
                LetterCatalog.Staff = staff;
            }
            else
            {
                LetterCatalog.Staff = null;
            }            
            
            Session.Save(LetterCatalog);
            id = LetterCatalog.Oid;
            flagSave = true;

            try
            {
                if (checkIsUseChild.Checked)
                {
                    EditStaffInCatalog(LetterCatalog.Oid);
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
            

            Close();
        }

        private void EditStaffInCatalog(int id)
        {
            using (var xpcollection = new XPCollection<LetterCatalog>(Session, new BinaryOperator(nameof(SystemCatalog.ParentCatalog), id)))
            {
                var listParentOid = new List<int>();
                foreach (var catalog in xpcollection)
                {
                    if (catalog.ParentCatalog != null)
                    {
                        listParentOid.Add(catalog.Oid);
                    }

                    catalog.Staff = LetterCatalog.Staff;
                    catalog.Save();
                } 

                if (listParentOid.Count > 0)
                {
                    foreach (var oid in listParentOid)
                    {
                        EditStaffInCatalog(oid);
                    }
                }
            }
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            txtName.EditValue = LetterCatalog.DisplayName;
            btnStaff.EditValue = LetterCatalog.Staff;
        }

        private void btnStaff_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);

        }
    }
}