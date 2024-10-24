using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Interface;
using RMS.Core.Model;
using RMS.Core.Model.InfoContract;
using RMS.Core.Model.Taxes;
using RMS.UI.Control;
using System;
using System.Drawing;
using System.Linq;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class StatusEdit<T> : formEdit_BaseSpr 
        where T : XPObject, IStatus
    {
        private Session Session { get; }
        private T Data { get; set; }

        public StatusEdit()
        {
            InitializeComponent();
            
            var imageCollectionStatus = new ImageCollectionStatus();
            cmbIcon.Properties.Items.AddImages(imageCollectionStatus.imageCollection);
            cmbIcon.Properties.SmallImages = imageCollectionStatus.imageCollection;
            
            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                Data = (T)Activator.CreateInstance(typeof(T), Session);
            }

            if (typeof(T) == typeof(ContractStatus))
            {
                Name = "Статус договора";
            }
            else if (typeof(T) == typeof(AccountingInsuranceStatus))
            {
                Name = "Статус страховых";
            }
            else if (typeof(T) == typeof(PatentStatus))
            {
                Name = "Статус патента";
            }
            else if (typeof(T) == typeof(IndividualEntrepreneursTaxStatus))
            {
                Name = "Статус ИП";
            }
            else if(typeof(T) == typeof(Status))
            {
                panelControlOption.Visible = true;
            }
        }

        public StatusEdit(int id) : this()
        {
            if (id > 0)
            {
                Data = Session.GetObjectByKey<T>(id);
            }
        }

        public StatusEdit(T data) : this()
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
            Data.Name = txtName.Text;
            Data.Description = memoDescription.Text;

            if (!string.IsNullOrWhiteSpace(txtIndex.Text) && int.TryParse(txtIndex.Text, out int result))
            {
                Data.Index = result;
            }
            else
            {
                Data.Index = null;
            }
            
            if (Data.IsDefault != checkIsDefault.Checked)
            {
                var xpcollection = new XPCollection<T>(Session);
                foreach (var obj in xpcollection.Where(w => w.IsDefault is true))
                {
                    obj.IsDefault = false;
                    obj.Save();
                }
                
                Data.IsDefault = checkIsDefault.Checked;
            }            
            
            if (cmbIcon.SelectedIndex == -1)
            {
                Data.IndexIcon = null;
            }
            else
            {
                Data.IndexIcon = cmbIcon.SelectedIndex;
            }

            if (typeof(T) == typeof(Status))
            {
                Data.SetMemberValue(nameof(Status.IsFormationArchiveFolder), checkIsFormationArchiveFolder.Checked);
                Data.SetMemberValue(nameof(Status.IsFormationReport), checkIsFormationReport.Checked);
            }

            Data.Color = ColorTranslator.ToHtml(colorStatus.Color);

            Session.Save(Data);
            id = Data.Oid;
            flagSave = true;
            Close();
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            if (Data.IsProtectionDelete)
            {
                txtName.Properties.ReadOnly = true;
            }
            
            memoDescription.Text = Data.Description;
            txtName.Text = Data.Name;
            checkIsDefault.EditValue = Data.IsDefault;
            txtIndex.EditValue = Data.Index;

            if (Data.IndexIcon is null)
            {
                cmbIcon.SelectedIndex = -1;
            }
            else
            {
                cmbIcon.SelectedIndex = Convert.ToInt32(Data.IndexIcon);
            }

            if (typeof(T) == typeof(Status))
            {
                checkIsFormationArchiveFolder.Checked = (bool)Data.GetMemberValue(nameof(Status.IsFormationArchiveFolder));
                checkIsFormationReport.Checked = (bool)Data.GetMemberValue(nameof(Status.IsFormationReport));
            }

            colorStatus.Color = ColorTranslator.FromHtml(Data.Color);
        }

        private void cmbIcon_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }
        }
    }
}