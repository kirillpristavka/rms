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
using RMS.Core.Model.CourierService;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RMS.UI.Forms.CourierService.v2
{
    public partial class RouteSheetv2Edit : formEdit_BaseSpr
    {
        private Session Session { get; }
        public RouteSheetv2 RouteSheetv2 { get; }

        public RouteSheetv2Edit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                RouteSheetv2 = new RouteSheetv2(Session);
            }

            XPObject.AutoSaveOnEndEdit = false;
        }

        public RouteSheetv2Edit(int id) : this()
        {
            if (id > 0)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                RouteSheetv2 = Session.GetObjectByKey<RouteSheetv2>(id);
            }
        }

        public RouteSheetv2Edit(RouteSheetv2 routeSheet) : this()
        {
            RouteSheetv2 = routeSheet;
            Session = routeSheet.Session;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
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
            
            var isClosed = checkIsClosed.Checked;
            var comment = memoComment.Text;

            var groupRouteSheetOperator = new GroupOperator(GroupOperatorType.And);
            var criteriaRouteSheetDate = new BinaryOperator(nameof(TaskCourier.Date), date);
            groupRouteSheetOperator.Operands.Add(criteriaRouteSheetDate);

            var routeSheet = Session.FindObject<RouteSheetv2>(groupRouteSheetOperator);

            if (routeSheet != null && routeSheet != RouteSheetv2)
            {
                XtraMessageBox.Show($"Маршрутный лист с параметрами:{Environment.NewLine}" +
                    $"Дата: {date.ToShortDateString()}{Environment.NewLine}" +
                    $"уже существует.",
                    "Найден маршрутный лист",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            
            RouteSheetv2.Date = date;
            RouteSheetv2.IsClosed = isClosed;
            RouteSheetv2.Comment = comment;
            RouteSheetv2.Value = calcCashBox.Value;

            if (RouteSheetv2.UserCreate is null)
            {
                RouteSheetv2.UserCreate = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid);
                RouteSheetv2.DateCreate = DateTime.Now;
            }
            else
            {
                RouteSheetv2.UserUpdate = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid);
                RouteSheetv2.DateUpdate = DateTime.Now;
            }

            RouteSheetv2.Save();
            Session.Save(RouteSheetv2.Payments);
            
            id = RouteSheetv2.Oid;
            flagSave = true;

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RouteSheetEdit_Load(object sender, EventArgs e)
        {
            dateDate.EditValue = RouteSheetv2.Date == DateTime.MinValue ? DateTime.Now : RouteSheetv2.Date;
            checkIsClosed.Checked = RouteSheetv2.IsClosed;
            memoComment.EditValue = RouteSheetv2.Comment;
            calcCashBox.EditValue = RouteSheetv2.Value;
            
            gridControlPayments.DataSource = RouteSheetv2.Payments;
                         
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
                repositoryItemButtonEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
                repositoryItemButtonEdit.ButtonPressed += Button_ButtonPressed;

                gridViewPayments.Columns[nameof(RouteSheetPayment.Description)].ColumnEdit = repositoryItemButtonEdit;
                gridViewPayments.Columns[nameof(RouteSheetPayment.Description)].OptionsColumn.ReadOnly = true;
            }

            if (gridViewPayments.Columns[nameof(Payment.TypePayment)] is GridColumn gridTypePayment)
            {
                gridTypePayment.Width = 175;
                gridTypePayment.OptionsColumn.FixedWidth = true;
                gridTypePayment.VisibleIndex = 2;
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
            var routeSheetPayment = gridViewPayments.GetRow(gridViewPayments.FocusedRowHandle) as RouteSheetPaymentv2;
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

        private void barBtnDelPayment_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewPayments.IsEmpty)
            {
                return;
            }

            var payment = gridViewPayments.GetRow(gridViewPayments.FocusedRowHandle) as RouteSheetPaymentv2;
            if (payment != null)
            {
                if (XtraMessageBox.Show($"Вы действительно хотите удалить следующий платеж:{Environment.NewLine}{payment}?",
                                        "Удаление объекта",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {
                    RouteSheetv2.Payments.Remove(payment);
                }
            }
        }

        private void barBtnAddPayment_ItemClick(object sender, ItemClickEventArgs e)
        {
            RouteSheetv2.Payments.Add(
                new RouteSheetPaymentv2(Session)
                {
                    TypePayment = Core.Enumerator.TypePayment.CashPayment 
                });
        }

        private void RouteSheetEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            RouteSheetv2.Reload();
            RouteSheetv2.Payments.Reload();
            foreach (var payment in RouteSheetv2.Payments)
            {
                payment.Reload();
            }
            XPObject.AutoSaveOnEndEdit = true;
        }

        private void gridViewPayments_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (gridView.GetRow(gridView.FocusedRowHandle) is RouteSheetPaymentv2)
                    {
                        barBtnDelPayment.Enabled = true;
                    }
                    else
                    {
                        barBtnDelPayment.Enabled = false;
                    }

                    popupMenuPayments.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }
    }
}