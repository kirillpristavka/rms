using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using PulsLibrary.Extensions.GeneralType;
using RMS.Core.Model.Calculator;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms.Calculator
{
    public partial class CalculatorTaxSystemEdit : formEdit_BaseSpr
    {
        private UnitOfWork uof = new UnitOfWork();
        private CalculatorTaxSystem CalculatorTaxSystem { get; set; }

        private CalculatorTaxSystemEdit()
        {
            InitializeComponent();
        }

        public CalculatorTaxSystemEdit(CalculatorTaxSystem calculatorTaxSystem) : this()
        {
            CalculatorTaxSystem = uof.GetObjectByKey<CalculatorTaxSystem>(calculatorTaxSystem.Oid);
        }

        public CalculatorTaxSystemEdit(int id) : this()
        {
            if (id > 0)
            {
                CalculatorTaxSystem = uof.GetObjectByKey<CalculatorTaxSystem>(id);
            }
        }

        private void CreateTariffScale(CalculatorTaxSystem calculatorTaxSystem)
        {
            var tariffScale = new TariffScale(uof)
            {
                Name = "Новая шкала",
                Description = "Автоматически созданная шкала"
            };

            tariffScale.TariffScalesObj.Add(new TariffScaleObj(uof) { Start = 0, End = 7, Value = 0 });
            tariffScale.TariffScalesObj.Add(new TariffScaleObj(uof) { Start = 8, End = 15, Value = 0 });
            tariffScale.TariffScalesObj.Add(new TariffScaleObj(uof) { Start = 15, End = 30, Value = 0 });
            tariffScale.TariffScalesObj.Add(new TariffScaleObj(uof) { Start = 30, End = 50, Value = 0 });
            tariffScale.TariffScalesObj.Add(new TariffScaleObj(uof) { Start = 51, End = 70, Value = 0 });
            tariffScale.TariffScalesObj.Add(new TariffScaleObj(uof) { Start = 71, End = 100, Value = 0 });
            tariffScale.TariffScalesObj.Add(new TariffScaleObj(uof) { Start = 101, End = 130, Value = 0 });
            tariffScale.TariffScalesObj.Add(new TariffScaleObj(uof) { Start = 131, End = 160, Value = 0 });
            tariffScale.TariffScalesObj.Add(new TariffScaleObj(uof) { Start = 161, End = 200, Value = 0 });
            tariffScale.TariffScalesObj.Add(new TariffScaleObj(uof) { Start = 201, End = 250, Value = 0 });
            tariffScale.TariffScalesObj.Add(new TariffScaleObj(uof) { Start = 251, End = 300, Value = 0 });
            tariffScale.TariffScalesObj.Add(new TariffScaleObj(uof) { Start = 301, End = 400, Value = 0 });
            tariffScale.TariffScalesObj.Add(new TariffScaleObj(uof) { Start = 401, End = 500, Value = 0 });
            tariffScale.TariffScalesObj.Add(new TariffScaleObj(uof) { Start = 501, End = 600, Value = 0 });
            tariffScale.TariffScalesObj.Add(new TariffScaleObj(uof) { Start = 601, End = 750, Value = 0 });
            tariffScale.TariffScalesObj.Add(new TariffScaleObj(uof) { Start = 751, End = 900, Value = 0 });
            tariffScale.TariffScalesObj.Add(new TariffScaleObj(uof) { Start = 901, End = 1100, Value = 0 });

            calculatorTaxSystem.CalculatorTaxSystesmObj.Add(new CalculatorTaxSystemObj(uof)
            {
                TariffScale = tariffScale
            });

            calculatorTaxSystem.TariffStaffObj.Add(new TariffStaffObj(uof)
            {
                Start = 1,
                End = 1000,
                Value = 1000
            });

            gridControlTariffScale.DataSource = CalculatorTaxSystem.CalculatorTaxSystesmObj.Select(s => s.TariffScale);
        }


        private void TaxSystemCustomerEdit_Load(object sender, EventArgs e)
        {
            if (CalculatorTaxSystem is null)
            {
                CalculatorTaxSystem = new CalculatorTaxSystem(uof);
                CreateTariffScale(CalculatorTaxSystem);
            }
            
            txtName.EditValue = CalculatorTaxSystem.Name;
            memoDescription.EditValue = CalculatorTaxSystem.Description;

            gridControlTariffScale.DataSource = CalculatorTaxSystem.CalculatorTaxSystesmObj.Select(s => s.TariffScale);
            HideColumnOid(gridControlTariffScale, gridViewTariffScale);

            gridControlTariffStaffObj.DataSource = CalculatorTaxSystem.TariffStaffObj;
            HideColumnOid(gridControlTariffStaffObj, gridViewTariffStaffObj);
        }       

        private async void btnSave_Click(object sender, EventArgs e)
        {                        
            CalculatorTaxSystem.Name = txtName.Text;
            CalculatorTaxSystem.Description = memoDescription.Text;
            CalculatorTaxSystem.Save();

            await uof.CommitTransactionAsync();

            id = CalculatorTaxSystem.Oid;
            flagSave = true;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        } 
        
        private void barBtnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            CreateTariffScale(CalculatorTaxSystem);
        }        

        private void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewTariffScale.IsEmpty)
            {
                return;
            }

            if (gridViewTariffScale.GetRow(gridViewTariffScale.FocusedRowHandle) is TariffScale tariffScale)
            {
                if (XtraMessageBox.Show($"Вы точно хотите удалить объект: {tariffScale}",
                                        "Удаление объекта",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {

                    var calculatorTaxSystesmObj = CalculatorTaxSystem.CalculatorTaxSystesmObj.FirstOrDefault(f => f.TariffScale == tariffScale);
                    calculatorTaxSystesmObj?.Delete();
                    gridControlTariffScale.DataSource = CalculatorTaxSystem.CalculatorTaxSystesmObj.Select(s => s.TariffScale);
                }
            }
        }

        private void gridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (gridView.GetRow(gridView.FocusedRowHandle) is TariffScale tariffScale)
                {
                    gridControlTariffScaleObj.DataSource = tariffScale.TariffScalesObj;
                    HideColumnOid(gridControlTariffScale, gridViewTariffScaleObj);
                }
            }
        }

        /// <summary>
        /// Сокрытие столбца Oid.
        /// </summary>
        /// <param name="gridView">Текущий GridView.</param>
        private void HideColumnOid(GridControl gridControl, GridView gridView, bool isRowAutoHeight = true)
        {
            if (gridView.Columns[nameof(XPObject.Oid)] != null)
            {
                gridView.Columns[nameof(XPObject.Oid)].Visible = false;
                gridView.Columns[nameof(XPObject.Oid)].Width = 18;
                gridView.Columns[nameof(XPObject.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (isRowAutoHeight)
            {
                gridView.OptionsView.RowAutoHeight = true;

                var repositoryItemMemoEdit = gridControl.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                repositoryItemMemoEdit.WordWrap = true;

                if (gridView.Columns[nameof(TariffScale.Name)] != null)
                {
                    gridView.Columns[nameof(TariffScale.Name)].ColumnEdit = repositoryItemMemoEdit;
                }

                if (gridView.Columns[nameof(TariffScale.Description)] != null)
                {
                    gridView.Columns[nameof(TariffScale.Description)].ColumnEdit = repositoryItemMemoEdit;
                }
            }           
        }

        private void barBtnAddTariffScaleObj_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewTariffScale.GetRow(gridViewTariffScale.FocusedRowHandle) is TariffScale tariffScale)
            {
                tariffScale.TariffScalesObj.Add(new TariffScaleObj(uof)
                {
                    Start = 0,
                    End = 0,
                    Value = 0
                });
            }
        }

        private void barBtnDelTariffScaleObj_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewTariffScaleObj.GetRow(gridViewTariffScaleObj.FocusedRowHandle) is TariffScaleObj tariffScaleObj)
            {
                if (XtraMessageBox.Show($"Вы точно хотите удалить объект: {tariffScaleObj}",
                                        "Удаление объекта",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {
                    tariffScaleObj?.Delete();
                }
            }
        }

        private void barBtnDelTariffScaleObjAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show($"Вы точно хотите удалить все объекты?",
                                        "Удаление объекта",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
            {
                uof.Delete((XPCollection<TariffScaleObj>)gridViewTariffScaleObj.DataSource);
            }
        }

        private void gridControlTariffScale_MouseDown(object sender, MouseEventArgs e)
        {
            if (e is DXMouseEventArgs dxMouseEventArgs && dxMouseEventArgs.Button == MouseButtons.Right)
            {
                if (dxMouseEventArgs.Button == MouseButtons.Right)
                {
                    popupMenuTariffScale.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void gridControlTariffScaleObj_MouseDown(object sender, MouseEventArgs e)
        {
            if (e is DXMouseEventArgs dxMouseEventArgs && dxMouseEventArgs.Button == MouseButtons.Right)
            {
                if (dxMouseEventArgs.Button == MouseButtons.Right)
                {
                    popupMenuTariffScaleObj.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void gridControlTariffStaffObj_MouseDown(object sender, MouseEventArgs e)
        {
            if (e is DXMouseEventArgs dxMouseEventArgs && dxMouseEventArgs.Button == MouseButtons.Right)
            {
                if (dxMouseEventArgs.Button == MouseButtons.Right)
                {
                    popupMenuTariffStaffObj.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void barBtnAddTariffStaffObj_ItemClick(object sender, ItemClickEventArgs e)
        {
            CalculatorTaxSystem.TariffStaffObj.Add(new TariffStaffObj(uof)
            {
                Start = 0,
                End = 0,
                Value = 0
            });
        }

        private void barBtnDelTariffStaffObj_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewTariffStaffObj.GetRow(gridViewTariffStaffObj.FocusedRowHandle) is TariffStaffObj tariffStaffObj)
            {
                if (XtraMessageBox.Show($"Вы точно хотите удалить объект: {tariffStaffObj}",
                                        "Удаление объекта",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {
                    tariffStaffObj?.Delete();
                }
            }
        }

        private void barBtnMassChangeValues_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewTariffScale.GetRow(gridViewTariffScale.FocusedRowHandle) is TariffScale tariffScale)
            {
                var form = new MassChangeValuesEdit();
                form.ShowDialog();

                if (form.IsSave)
                {
                    if (XtraMessageBox.Show(
                        $"Вы действительно хотите провести следующую операцию:{Environment.NewLine}" +
                        $"Операция: {form.Operation.GetDescription()}{Environment.NewLine}" +
                        $"Значение: {form.Value}",
                        "Операция над объектами",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        var value = form.Value;
                        if (CalculatorTaxSystem != null)
                        {
                            if (tariffScale != null)
                            {
                                foreach (var tariffScalesObj in tariffScale.TariffScalesObj)
                                {
                                    switch (form.Operation)
                                    {
                                        case Operation.Addition:
                                            tariffScalesObj.Value += value;
                                            break;

                                        case Operation.Subtraction:
                                            tariffScalesObj.Value -= value;
                                            break;

                                        case Operation.Multiplication:
                                            tariffScalesObj.Value *= value;
                                            break;

                                        case Operation.Division:
                                            tariffScalesObj.Value /= value;
                                            break;
                                    }

                                    tariffScalesObj.Save();
                                    gridControlTariffScaleObj.RefreshDataSource();
                                }
                            }
                        }
                    }
                }
            }            
        }
    }
}