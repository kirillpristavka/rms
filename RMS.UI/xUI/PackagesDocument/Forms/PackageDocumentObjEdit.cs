using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using PulsLibrary.Extensions.DevForm;
using PulsLibrary.Methods;
using RMS.Core.Model;
using RMS.Core.Model.PackagesDocument;
using System;
using System.Diagnostics;

namespace RMS.UI.xUI.PackagesDocument.Forms
{
    public partial class PackageDocumentObjEdit : formEdit_BaseSpr
    {
        private UnitOfWork _uof;        
        public bool IsSave { get; private set; }
        public PackageDocumentObj PackageDocumentObj { get; private set; }

        public PackageDocumentObjEdit(UnitOfWork uof = null)
        {
            InitializeComponent();
            Icon = Properties.Resources.IconRMS;

            if (uof is null)
            {
                uof = new UnitOfWork();
            }
            _uof = uof;
        }        

        public PackageDocumentObjEdit(object obj, UnitOfWork uof = null) : this(uof)
        {
            if (obj is PackageDocumentObj packageDocumentObj)
            {
                this.id = packageDocumentObj.Oid;
            }
        } 
        
        private async void Form_Load(object sender, EventArgs e)
        {
            if (PackageDocumentObj is null)
            {
                var obj = await _uof.GetObjectByKeyAsync<PackageDocumentObj>(Id);
                if (obj is null)
                {
                    obj = new PackageDocumentObj(_uof);
                }
                
                PackageDocumentObj = obj;
            }
            
            FillingOutTheEditForm(PackageDocumentObj);
        }

        /// <summary>
        /// Заполнение формы редактирования.
        /// </summary>
        /// <param name="counterparty">Объект заполнения.</param>
        private void FillingOutTheEditForm(PackageDocumentObj packageDocumentObj)
        {
            if (packageDocumentObj is null)
            {
                packageDocumentObj = new PackageDocumentObj(_uof);
            }

            btnFile.EditValue = packageDocumentObj.File;
            dateReceiving.EditValue = packageDocumentObj.DateReceiving;
            checkIsScannedDocument.EditValue = packageDocumentObj.IsScannedDocument;
            dateDeparture.EditValue = packageDocumentObj.DateDeparture;
            checkIsOriginalDocument.EditValue = packageDocumentObj.IsOriginalDocument;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {                        
            var file = Objects.GetRequiredObject<File>(btnFile.EditValue);
            if (file is null)
            {
                DevXtraMessageBox.ShowXtraMessageBox("Для сохранения необходимо указать файл.", file);
                return;
            }

            var dateReceiving = Objects.GetRequiredObject<DateTime?>(this.dateReceiving.EditValue);
            var isScannedDocument = Objects.GetRequiredObject<bool>(checkIsScannedDocument.EditValue);
            var dateDeparture = Objects.GetRequiredObject<DateTime?>(this.dateDeparture.EditValue);
            var isOriginalDocument = Objects.GetRequiredObject<bool>(checkIsOriginalDocument.EditValue);

            if (PackageDocumentObj is null)
            {
                PackageDocumentObj = new PackageDocumentObj(_uof);
            }

            PackageDocumentObj.File = file;
            PackageDocumentObj.DateReceiving = dateReceiving;
            PackageDocumentObj.IsScannedDocument = isScannedDocument;
            PackageDocumentObj.DateDeparture = dateDeparture;
            PackageDocumentObj.IsOriginalDocument = isOriginalDocument;
            
            this.id = PackageDocumentObj.Oid;
            IsSave = true;
            flagSave = true;
            Close();
        }        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            PackageDocumentObj?.Reload();
            Close();
        }

        private void btnFile_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = Objects.GetRequiredObject<ButtonEdit>(sender);
            if (buttonEdit != null)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                }
                else if (e.Button.Kind == ButtonPredefines.Ellipsis)
                {
                    if (buttonEdit.EditValue is File file)
                    {
                        SaveFile(file);
                    }
                    else
                    {
                        using (var ofd = new XtraOpenFileDialog())
                        {
                            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                file = new File(_uof, ofd.FileName);
                                buttonEdit.EditValue = file;
                            }
                        }
                    }
                }
            }
        }

        private void SaveFile(File file)
        {
            using (var fbd = new XtraFolderBrowserDialog() { Description = "Сохранение файла" })
            {
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var fullPathFile = file.WriteFile(fbd.SelectedPath);

                    if (!string.IsNullOrWhiteSpace(fullPathFile))
                    {
                        if (XtraMessageBox.Show("Файл успешно сохранен. Открыть директорию?", string.Empty, System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                        {
                            Process.Start("explorer", fbd.SelectedPath);
                        }

                        Process.Start(fullPathFile);
                    }
                    else
                    {
                        XtraMessageBox.Show("Ошибка сохранения файла, возможно такой файл уже имеется в директории", string.Empty, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnFile_DoubleClick(object sender, EventArgs e)
        {
            var buttonEdit = Objects.GetRequiredObject<ButtonEdit>(sender);
            var file = Objects.GetRequiredObject<File>(buttonEdit?.EditValue);
            if (file != null)
            {
                SaveFile(file);
            }
        }
    }
}