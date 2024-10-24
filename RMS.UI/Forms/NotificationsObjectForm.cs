using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using RMS.Core.Interface;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Notifications;
using RMS.Core.Model.Reports;
using RMS.Core.Model.Taxes;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RMS.UI.Forms
{
    public partial class NotificationsObjectForm : XtraForm
    {
        private List<Notification> notifications;
        
        public NotificationsObjectForm(List<Notification> notifications, string caption)
        {
            InitializeComponent();
            this.notifications = notifications;
            Text = caption;
        }

        private void NotificationsObjectForm_Load(object sender, EventArgs e)
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridView);

            if (notifications is null || notifications.Count == 0)
            {
                gridControl.DataSource = null;
                return;
            }
            
            gridControl.DataSource = notifications.OrderByDescending(o => o.DateTime);

            if (gridView.Columns[nameof(Notification.TypeNotification)] != null)
            {
                gridView.Columns[nameof(Notification.TypeNotification)].Visible = false;
            }            

            if (gridView.Columns[nameof(Notification.Name)] != null)
            {
                RepositoryItemMemoEdit memoEdit = gridControl.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                gridView.Columns[nameof(Notification.Name)].ColumnEdit = memoEdit;
            }

            if (gridView.Columns[nameof(Notification.Id)] != null)
            {
                gridView.Columns[nameof(Notification.Id)].Visible = false;
            }
            
            if (gridView.Columns[nameof(Notification.Type)] != null)
            {
                gridView.Columns[nameof(Notification.Type)].Visible = false;
            }

            if (gridView.Columns[nameof(Notification.DateTime)] != null)
            {
                gridView.Columns[nameof(Notification.DateTime)].Width = 100;
                gridView.Columns[nameof(Notification.DateTime)].OptionsColumn.FixedWidth = true;
                gridView.Columns[nameof(Notification.DateTime)].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }

            if (gridView.Columns[nameof(Notification.NameModel)] != null)
            {
                gridView.Columns[nameof(Notification.NameModel)].Width = 100;
                gridView.Columns[nameof(Notification.NameModel)].OptionsColumn.FixedWidth = true;
                gridView.Columns[nameof(Notification.NameModel)].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }
        }

        private async void gridView_DoubleClick(object sender, EventArgs e)
        {
            var obj = gridView.GetRow(gridView.FocusedRowHandle) as Notification;
            if (obj != null)
            {
                var model = await DatabaseConnection.WorkSession.GetObjectByKeyAsync(obj.Type, obj.Id);
                if (model != null)
                {
                    var formType = GetTypeForm(obj.Type);

                    if (formType != null)
                    {
                        var formMethod = formType.GetMethod("ShowDialog", new Type[] { });

                        if (formMethod != null)
                        {
                            var form = Activator.CreateInstance(formType, model);
                            formMethod.Invoke(form, null);

                            if (model is INotification notification)
                            {
                                obj.Update(notification.GetNotification(obj.TypeNotification));
                            }
                        }                        
                    }                                  
                }                
            }
        }
        
        private Type GetTypeForm(Type classType)
        {
            if (classType.Name.Equals(nameof(Task)))
            {
                return typeof(TaskEdit);
            }
            else if (classType.Name.Equals(nameof(ReportChange)))
            {
                return typeof(ReportChangeEdit);
            }
            else if (classType.Name.Equals(nameof(PreTax)))
            {
                return typeof(PreTaxEdit);
            }
            else if (classType.Name.Equals(nameof(IndividualEntrepreneursTax)))
            {
                return typeof(IndividualEntrepreneursTaxEdit);                
            }
            else if (classType.Name.Equals(nameof(Deal)))
            {
                return typeof(DealEdit);
            }
            else if (classType.Name.Equals(nameof(Customer)))
            {
                return typeof(CustomerEdit);
            }

            return default;
        }

        private void gridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            var view = sender as GridView;
            
            if (view.Columns[nameof(Notification.NameModel)] != null)
            {
                var nameModel = view.GetRowCellValue
                            (e.RowHandle, view.Columns[nameof(Notification.NameModel)])?.ToString();

                if (!string.IsNullOrWhiteSpace(nameModel))
                {
                    if (nameModel.Equals("Задачи"))
                    {
                        e.Appearance.BackColor = Color.LightGreen;
                    }
                    else if (nameModel.Equals("Предварительные налоги"))
                    {
                        e.Appearance.BackColor = Color.LightBlue;
                    }
                    else if (nameModel.Equals("ИП/Страховые"))
                    {
                        e.Appearance.BackColor = Color.LightYellow;
                    }
                    else if (nameModel.Equals("ИП/Патенты"))
                    {
                        e.Appearance.BackColor = Color.LightSalmon;
                    }
                }
            }
        }
    }
}