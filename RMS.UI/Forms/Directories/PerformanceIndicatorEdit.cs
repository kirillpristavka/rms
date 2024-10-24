using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Utils;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RMS.UI.Forms.Directories
{
    public partial class PerformanceIndicatorEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        public PerformanceIndicator PerformanceIndicator { get; }
        
        private TypePerformanceIndicator _typePerformanceIndicator;

        private PerformanceIndicatorEdit()
        {
            InitializeComponent();

            foreach (TypePerformanceIndicator item in Enum.GetValues(typeof(TypePerformanceIndicator)))
            {
                cmbTypePerformanceIndicator.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbTypePerformanceIndicator.SelectedIndex = 0;

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                PerformanceIndicator = new PerformanceIndicator(Session);
            }
        }

        public PerformanceIndicatorEdit(int id) : this()
        {
            if (id > 0)
            {
                PerformanceIndicator = Session.GetObjectByKey<PerformanceIndicator>(id);
            }
        }

        public PerformanceIndicatorEdit(PerformanceIndicator performanceIndicator) : this()
        {
            Session = performanceIndicator.Session;
            PerformanceIndicator = performanceIndicator;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            XPObject.AutoSaveOnEndEdit = false;

            // Разрешил изменение типа данных (25.09.20).            
            //if (PerformanceIndicator.Oid > 0)
            //{
            //    cmbTypePerformanceIndicator.Enabled = false;
            //}
            
            txtName.EditValue = PerformanceIndicator.Name;
            txtAbbreviatedName.EditValue = PerformanceIndicator.AbbreviatedName;

            memoDescription.EditValue = PerformanceIndicator.Description;

            checkIsUseWhenFormingAnInvoice.Checked = PerformanceIndicator.IsUseWhenFormingAnInvoice;
            checkIsUseWhenGeneratingInformationOnEmployees.Checked = PerformanceIndicator.IsUseWhenGeneratingInformationOnEmployees;

            btnGroupPerformanceIndicator.EditValue = PerformanceIndicator.GroupPerformanceIndicator;
            cmbTypePerformanceIndicator.SelectedIndex = (int)PerformanceIndicator.TypePerformanceIndicator;
            txtValue.EditValue = PerformanceIndicator.Value;
            txtUnit.EditValue = PerformanceIndicator.Unit;

            gridControl.DataSource = PerformanceIndicator.PerformanceIndicatorValues;
            if (gridView.Columns[nameof(PerformanceIndicator.Oid)] != null)
            {
                gridView.Columns[nameof(PerformanceIndicator.Oid)].Visible = false;
                gridView.Columns[nameof(PerformanceIndicator.Oid)].Width = 18;
                gridView.Columns[nameof(PerformanceIndicator.Oid)].OptionsColumn.FixedWidth = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveUserGroup())
            {
                id = PerformanceIndicator.Oid;
                flagSave = true;
                Close();
            }
        }

        /// <summary>
        /// Сохранение отчета.
        /// </summary>
        private bool SaveUserGroup()
        {
            if (PerformanceIndicator.Oid <= 0)
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    XtraMessageBox.Show($"Сохранение без наименования не возможно.",
                        "Ошибка сохранения",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    txtName.Focus();
                    return false;
                }
            }

            if (PerformanceIndicator.Name != txtName.Text)
            {
                if (Session.FindObject<PerformanceIndicator>(new BinaryOperator(nameof(PerformanceIndicator.Name), txtName.Text)) != null && !string.IsNullOrWhiteSpace(txtName.Text))
                {
                    XtraMessageBox.Show($"Показатель: {txtName.Text} уже существует в системе.",
                        "Ошибка сохранения",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
            }
            
            PerformanceIndicator.AbbreviatedName = txtAbbreviatedName.Text;
            PerformanceIndicator.Name = txtName.Text;
            PerformanceIndicator.Description = memoDescription.Text;
            PerformanceIndicator.IsUseWhenFormingAnInvoice = checkIsUseWhenFormingAnInvoice.Checked;
            PerformanceIndicator.IsUseWhenGeneratingInformationOnEmployees = checkIsUseWhenGeneratingInformationOnEmployees.Checked;
            PerformanceIndicator.Unit = txtUnit.Text;
            
            if (btnGroupPerformanceIndicator.EditValue is GroupPerformanceIndicator groupPerformanceIndicator)
            {
                PerformanceIndicator.GroupPerformanceIndicator = groupPerformanceIndicator;
            }
            else
            {
                PerformanceIndicator.GroupPerformanceIndicator = null;
            }

            if (cmbTypePerformanceIndicator.EditValue is null || cmbTypePerformanceIndicator.SelectedIndex == -1)
            {
                XtraMessageBox.Show($"Без выбора типа показателя сохранение не возможно.",
                        "Ошибка сохранения",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                cmbTypePerformanceIndicator.Focus();
                return false;
            }
            else
            {
                PerformanceIndicator.TypePerformanceIndicator = _typePerformanceIndicator;
            }
            
            if (txtValue.EditValue is decimal)
            {
                PerformanceIndicator.Value = Convert.ToDecimal(txtValue.Text);
            }
            else
            {
                PerformanceIndicator.Value = null;
            }

            if (_typePerformanceIndicator == TypePerformanceIndicator.Sample)
            {
                Session.Save(PerformanceIndicator.PerformanceIndicatorValues);
            }

            PerformanceIndicator.Save();
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGroupPerformanceIndicator_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<GroupPerformanceIndicator>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.GroupPerformanceIndicator, 1, null, null, false, null, string.Empty, false, true);
        }

        private void cmbTypePerformanceIndicator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBoxEdit comboBoxEdit)
            {
                foreach (TypePerformanceIndicator typePerformanceIndicator in Enum.GetValues(typeof(TypePerformanceIndicator)))
                {
                    if (typePerformanceIndicator.GetEnumDescription().Equals(comboBoxEdit.Text))
                    {
                        if (typePerformanceIndicator == TypePerformanceIndicator.Sample)
                        {
                            txtValue.EditValue = null;
                            layoutControlValue.Visibility = LayoutVisibility.Never;
                            xtraTabPageSample.PageVisible = true;
                        }
                        else if (typePerformanceIndicator == TypePerformanceIndicator.Base
                                || typePerformanceIndicator == TypePerformanceIndicator.Analysis)
                        {
                            txtValue.EditValue = null; 
                            layoutControlValue.Visibility = LayoutVisibility.Never;
                            xtraTabPageSample.PageVisible = false;
                        }
                        else
                        {
                            txtValue.EditValue = 0M;
                            layoutControlValue.Visibility = LayoutVisibility.Always;
                            xtraTabPageSample.PageVisible = false;
                        }

                        _typePerformanceIndicator = typePerformanceIndicator;
                        break;
                    }
                }
            }
        }

        private void PerformanceIndicatorEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            XPObject.AutoSaveOnEndEdit = true;
        }

        private void barBtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PerformanceIndicator.PerformanceIndicatorValues.Add(new PerformanceIndicatorValue(Session));
        }

        private void barBtnDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var performanceIndicatorValue = gridView.GetRow(gridView.FocusedRowHandle) as PerformanceIndicatorValue;
            if (performanceIndicatorValue != null)
            {
                PerformanceIndicator.PerformanceIndicatorValues.Remove(performanceIndicatorValue);
            }
        }

        private void gridView_MouseDown(object sender, MouseEventArgs e)
        {
            DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            GridView gridview = sender as GridView;
            _ = gridview.CalcHitInfo(dxMouseEventArgs.Location);
            //if ((gridHitInfo.InRow || gridHitInfo.InRowCell) && dxMouseEventArgs.Button == MouseButtons.Right)
            if (dxMouseEventArgs.Button == MouseButtons.Right)
            {
                popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }
    }
}