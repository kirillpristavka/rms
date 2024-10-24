using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using RMS.Core.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RMS.UI.Forms.CourierService.v1
{
    public partial class RouteSheetEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        public RouteSheet RouteSheet { get; }

        public RouteSheetEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                RouteSheet = new RouteSheet(Session);
            }

            XPObject.AutoSaveOnEndEdit = false;
        }

        public RouteSheetEdit(int id) : this()
        {
            if (id > 0)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                RouteSheet = Session.GetObjectByKey<RouteSheet>(id);
            }
        }

        public RouteSheetEdit(RouteSheet routeSheet) : this()
        {
            RouteSheet = routeSheet;
            Session = routeSheet.Session;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var courier = btnCourier.EditValue as Individual;
            if (courier == null)
            {
                XtraMessageBox.Show("Для сохранение курьерской задачи укажите курьера.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCourier.Focus();
                return;
            }

            var date = default(DateTime);
            if (dateDate.EditValue != null)
            {
                date = Convert.ToDateTime(dateDate.EditValue).Date;
            }
            else
            {
                XtraMessageBox.Show("Для сохранение курьерской задачи укажите дату.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dateDate.Focus();
                return;
            }

            var value = Convert.ToDecimal(calcValue.EditValue);
            var isClosed = checkIsClosed.Checked;
            var comment = memoComment.Text;

            var groupRouteSheetOperator = new GroupOperator(GroupOperatorType.And);
            var criteriaRouteSheetDate = new BinaryOperator(nameof(TaskCourier.Date), date);
            groupRouteSheetOperator.Operands.Add(criteriaRouteSheetDate);
            var criteriaRouteSheetCourier = new BinaryOperator(nameof(TaskCourier.Courier), courier);
            groupRouteSheetOperator.Operands.Add(criteriaRouteSheetCourier);

            var routeSheet = Session.FindObject<RouteSheet>(groupRouteSheetOperator);

            if (routeSheet != null && routeSheet != RouteSheet)
            {
                XtraMessageBox.Show($"Маршрутный лист с параметрами:{Environment.NewLine}" +
                    $"Дата: {date.ToShortDateString()}{Environment.NewLine}" +
                    $"Курьер: {courier}{Environment.NewLine}" +
                    $"уже существует.",
                    "Найден маршрутный лист",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            RouteSheet.Courier = courier;
            RouteSheet.Date = date;
            RouteSheet.Value = value;
            RouteSheet.IsClosed = isClosed;
            RouteSheet.Comment = comment;

            if (RouteSheet.UserCreate is null)
            {
                RouteSheet.UserCreate = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid);
                RouteSheet.DateCreate = DateTime.Now;
            }
            else
            {
                RouteSheet.UserUpdate = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid);
                RouteSheet.DateUpdate = DateTime.Now;
            }

            RouteSheet.Save();
            Session.Save(RouteSheet.Payments);
            
            id = RouteSheet.Oid;
            flagSave = true;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RouteSheetEdit_Load(object sender, EventArgs e)
        {
            btnCourier.EditValue = RouteSheet.Courier;
            dateDate.EditValue = RouteSheet.Date == DateTime.MinValue ? DateTime.Now : RouteSheet.Date;
            calcValue.EditValue = RouteSheet.Value;
            checkIsClosed.Checked = RouteSheet.IsClosed;
            memoComment.EditValue = RouteSheet.Comment;

            gridControlPayments.DataSource = RouteSheet.Payments;

            if (gridViewPayments.Columns[nameof(Payment.TypePayment)] != null)
            {
                gridViewPayments.Columns[nameof(Payment.TypePayment)].Visible = false;
            }
            if (gridViewPayments.Columns[nameof(Payment.Date)] != null)
            {
                gridViewPayments.Columns[nameof(Payment.Date)].Visible = false;
            }            
            if (gridViewPayments.Columns[nameof(Payment.Oid)] != null)
            {
                gridViewPayments.Columns[nameof(Payment.Oid)].Visible = false;
                gridViewPayments.Columns[nameof(Payment.Oid)].Width = 18;
                gridViewPayments.Columns[nameof(Payment.Oid)].OptionsColumn.FixedWidth = true;
                gridViewPayments.BestFitColumns();
            }
            
            if (gridViewPayments.Columns[nameof(RouteSheetPayment.Description)] != null)
            {
                var repositoryItemButtonEdit = new RepositoryItemButtonEdit();
                repositoryItemButtonEdit.Buttons.Add(new EditorButton()
                {
                    Kind = ButtonPredefines.Delete
                });                

                repositoryItemButtonEdit.ButtonPressed += Button_ButtonPressed;

                gridViewPayments.Columns[nameof(RouteSheetPayment.Description)].ColumnEdit = repositoryItemButtonEdit;
                gridViewPayments.Columns[nameof(RouteSheetPayment.Description)].OptionsColumn.ReadOnly = true;
            }

            if (gridViewPayments.Columns[nameof(Payment.Value)] is GridColumn gridValue)
            {
                gridValue.DisplayFormat.FormatType = FormatType.Numeric;
                gridValue.DisplayFormat.FormatString = "n2";
                gridValue.RealColumnEdit.EditFormat.FormatType = FormatType.Numeric;
                gridValue.RealColumnEdit.EditFormat.FormatString = "n2";
                gridValue.Width = 150;
                gridValue.OptionsColumn.FixedWidth = true;
                gridValue.VisibleIndex = 3;
            }
        }

        private void Button_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var routeSheetPayment = gridViewPayments.GetRow(gridViewPayments.FocusedRowHandle) as RouteSheetPayment;
            if (routeSheetPayment != null)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    routeSheetPayment.CostItem = null;
                    gridViewPayments.UpdateCurrentRow();
                    return;
                }

                if (e.Button.Kind == ButtonPredefines.Ellipsis)
                {
                    var id = cls_BaseSpr.RunBaseSpr((int)(int)cls_App.ReferenceBooks.CostItem);
                    if (id > 0)
                    {
                        var costItem = Session.GetObjectByKey<CostItem>(id);
                        if (costItem != null)
                        {
                            routeSheetPayment.CostItem = costItem;
                        }
                    }
                    gridViewPayments.UpdateCurrentRow();
                    return;
                }
            }
        }

        private void btnCourier_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Individual>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Individual, 1, null, null, false, null, string.Empty, false, true);
        }

        private void gridViewPayments_MouseDown(object sender, MouseEventArgs e)
        {
            DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            GridView gridview = sender as GridView;
            _ = gridview.CalcHitInfo(dxMouseEventArgs.Location);
            //if ((gridHitInfo.InRow || gridHitInfo.InRowCell) && dxMouseEventArgs.Button == MouseButtons.Right)
            if (dxMouseEventArgs.Button == MouseButtons.Right)
            {
                popupMenuPayments.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void barBtnDelPayment_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewPayments.IsEmpty)
            {
                return;
            }

            var payment = gridViewPayments.GetRow(gridViewPayments.FocusedRowHandle) as RouteSheetPayment;
            if (payment != null)
            {
                RouteSheet.Payments.Remove(payment);
            }
        }

        private void barBtnAddPayment_ItemClick(object sender, ItemClickEventArgs e)
        {
            RouteSheet.Payments.Add(new RouteSheetPayment(Session));
        }

        private void RouteSheetEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            RouteSheet.Reload();
            RouteSheet.Payments.Reload();
            foreach (var payment in RouteSheet.Payments)
            {
                payment.Reload();
            }
            XPObject.AutoSaveOnEndEdit = true;
        }
    }
}