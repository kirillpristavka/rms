using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.SalaryStaff;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms
{
    public partial class SalaryStaffForm : XtraForm
    {    
        private int currentYear = DateTime.Now.Year;
        private int currentMonth = DateTime.Now.Month;

        private XPCollection<Staff> staffs;
        private XPCollection<Basis> basis;
        private XPCollection<Сalculation> calculation;

        private Session Session { get; }

        
        private void UpdateStaffs()
        {
            var groupOperator = new GroupOperator(GroupOperatorType.And);

            var dateStart = new DateTime(currentYear, currentMonth, 1);
            var dateEnd = new DateTime(currentYear, currentMonth, DateTime.DaysInMonth(currentYear, currentMonth));
                        
            groupOperator.Operands.Add(CriteriaOperator.Parse($"[{nameof(Staff.DateReceipt)}] is NULL OR [{nameof(Staff.DateReceipt)}] <= ?", dateEnd));
            groupOperator.Operands.Add(CriteriaOperator.Parse($"[{nameof(Staff.DateDismissal)}] is NULL OR [{nameof(Staff.DateDismissal)}] >= ?", dateStart));

            staffs = new XPCollection<Staff>(Session, groupOperator);
            
            gridControl.DataSource = staffs;

            if (gridView.Columns[nameof(Staff.Oid)] != null)
            {
                gridView.Columns[nameof(Staff.Oid)].Visible = false;
                gridView.Columns[nameof(Staff.Oid)].Width = 18;
                gridView.Columns[nameof(Staff.Oid)].OptionsColumn.FixedWidth = true;
            }
        }
        private void UpdateBasis(Staff staff)
        {
            var groupOperator = new GroupOperator(GroupOperatorType.And);

            var dateStart = new DateTime(currentYear, currentMonth, 1);
            var dateEnd = new DateTime(currentYear, currentMonth, DateTime.DaysInMonth(currentYear, currentMonth));

            groupOperator.Operands.Add(new BinaryOperator(nameof(Basis.Staff), staff));
            groupOperator.Operands.Add(CriteriaOperator.Parse($"[{nameof(Basis.DateSince)}] is NULL OR [{nameof(Basis.DateSince)}] <= ?", dateEnd));
            groupOperator.Operands.Add(CriteriaOperator.Parse($"[{nameof(Basis.DateTo)}] is NULL OR [{nameof(Basis.DateTo)}] >= ?", dateStart));

            basis = new XPCollection<Basis>(Session, groupOperator);

            gridControlBasis.DataSource = basis;

            if (gridViewBasis.Columns[nameof(Staff.Oid)] != null)
            {
                gridViewBasis.Columns[nameof(Staff.Oid)].Visible = false;
                gridViewBasis.Columns[nameof(Staff.Oid)].Width = 18;
                gridViewBasis.Columns[nameof(Staff.Oid)].OptionsColumn.FixedWidth = true;
            }
        }

        private void UpdateCalculation(Staff staff)
        {
            var groupOperator = new GroupOperator(GroupOperatorType.And);            

            groupOperator.Operands.Add(new BinaryOperator(nameof(Сalculation.Staff), staff));
            groupOperator.Operands.Add(new BinaryOperator(nameof(Сalculation.Month), currentMonth));
            groupOperator.Operands.Add(new BinaryOperator(nameof(Сalculation.Year), currentYear));

            calculation = new XPCollection<Сalculation>(Session, groupOperator);

            gridControlСalculation.DataSource = calculation;

            if (gridViewCalculation.Columns[nameof(Сalculation.Oid)] != null)
            {
                gridViewCalculation.Columns[nameof(Сalculation.Oid)].Visible = false;
                gridViewCalculation.Columns[nameof(Сalculation.Oid)].Width = 18;
                gridViewCalculation.Columns[nameof(Сalculation.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridViewCalculation.Columns[nameof(Сalculation.Value)] is GridColumn gridColumn)
            {
                gridColumn.Summary.Clear();
                gridColumn.Summary.Add(SummaryItemType.Sum, nameof(Сalculation.Value), "{0:n2}");
            }
        }

        private void EditPeriod_SelectedValueChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cmbYear.Text, out int result))
            {
                currentYear = result;
            }
            else
            {
                return;
            }

            foreach (Month month in Enum.GetValues(typeof(Month)))
            {
                if (month.GetEnumDescription().Equals(cmbMonth.Text))
                {
                    currentMonth = (int)month;
                    break;
                }
            }

            UpdateStaffs();
            gridView_FocusedRowChanged(gridView, null);
        }

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridView);
            //BVVGlobal.oFuncXpo.PressEnterGrid<Task, TaskEdit>(gridViewTasks);
        }

        public SalaryStaffForm(Session session)
        {
            InitializeComponent();
            FunctionalGridSetup();

            Session = session ?? BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
        }

        private void TaskForm_Load(object sender, EventArgs e)
        {
            foreach (Month item in Enum.GetValues(typeof(Month)))
            {
                cmbMonth.Properties.Items.Add(item.GetEnumDescription());
            }            
            
            cmbMonth.SelectedIndex = currentMonth - 1;
            cmbYear.EditValue = currentYear;

            UpdateStaffs();            
        }

        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (gridView.GetRow(gridView.FocusedRowHandle) is Staff staff)
                {
                    UpdateBasis(staff);
                    UpdateCalculation(staff);
                }
            }
        }

        private void barBtnAddBasis_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is Staff staff)
            {
                var form = new BasisEdit(staff);
                form.ShowDialog();

                basis?.Reload();
            }                    
        }

        private void barBtnEditBasis_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewBasis.GetRow(gridViewBasis.FocusedRowHandle) is Basis basis)
            {
                var form = new BasisEdit(basis);
                form.ShowDialog();
                
                basis?.Reload();
            }
        }

        private void barBtnDelBasis_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewBasis.GetRow(gridViewBasis.FocusedRowHandle) is Basis basis)
            {
                if (XtraMessageBox.Show($"Хотите удалить следующее основание: {basis}", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    basis.Delete();

                    basis?.Reload();
                }
            }
        }

        private void barBtnCalculation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewBasis.GetRow(gridViewBasis.FocusedRowHandle) is Basis basis)
            {
                var calculation = this.calculation.FirstOrDefault(f => 
                    f.Basis == basis 
                    && f.Staff == basis.Staff 
                    && f.Year == currentYear 
                    && f.Month == currentMonth);
                
                if (calculation is null)
                {
                    calculation = new Сalculation(basis.Session)
                    {
                        Staff = basis.Staff,
                        Year = currentYear,
                        Month = currentMonth,
                        Value = basis.Rate,
                        Basis = basis,
                        PayoutDictionary = basis.PayoutDictionary
                    };
                }
                else
                {
                    calculation.Value = basis.Rate;
                }
                
                calculation.Save();

                this.calculation?.Reload();
            }
        }

        private void barBtnEditCalculation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewCalculation.GetRow(gridViewCalculation.FocusedRowHandle) is Сalculation calculation)
            {
                var form = new СalculationEdit(calculation);
                form.ShowDialog();

                this.calculation?.Reload();
            }
        }

        private void barBtnDelCalculation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewCalculation.GetRow(gridViewCalculation.FocusedRowHandle) is Сalculation calculation)
            {
                if (XtraMessageBox.Show($"Хотите удалить следующий расчет: {calculation}?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    calculation.Delete();

                    this.calculation?.Reload();
                }
            }
        }

        private void gridViewBasis_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (e.MenuType != GridMenuType.Row)
                    {
                        barBtnEditBasis.Enabled = false;
                        barBtnDelBasis.Enabled = false;
                    }
                    else
                    {
                        barBtnEditBasis.Enabled = true;
                        barBtnDelBasis.Enabled = true;
                    }
                    
                    popupMenuBasis.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void gridViewCalculation_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (e.MenuType != GridMenuType.Row)
                    {
                        barBtnEditCalculation.Enabled = false;
                        barBtnDelCalculation.Enabled = false;
                    }
                    else
                    {
                        barBtnEditCalculation.Enabled = true;
                        barBtnDelCalculation.Enabled = true;
                    }

                    popupMenuCalculation.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }
    }
}