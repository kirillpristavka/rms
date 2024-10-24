using DevExpress.XtraEditors;
using PulsLibrary.Extensions.DevForm;
using RMS.Core.Model;
using RMS.UI.Forms.Vacations;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace RMS.UI.Control.Desktop
{
    public partial class VacationObjectControl : UserControl
    {
        private Vacation _vacation;
        public bool IsCongratulate { get; private set; }


        public delegate void SaveEventHandler(object sender);
        public event SaveEventHandler SaveEvent;

        public VacationObjectControl(Vacation vacation)
        {
            InitializeComponent();
            _vacation = vacation;
        }

        private void TaskControl_Load(object sender, EventArgs e)
        {
            ReloadControl();
        }

        public void ReloadControl()
        {
            lblCustomer.Text = _vacation.StaffName;
            lblDescription.Text = $"{_vacation.DateSince.ToShortDateString()} - {_vacation.DateTo.ToShortDateString()}";

            btnCongratulate.Visible = true;

            if (_vacation.IsConfirm)
            {
                IsCongratulate = false;
            }
            else
            {
                IsCongratulate = true;
            }
        }

        private void lblTask_MouseEnter(object sender, EventArgs e)
        {
            var label = sender as LabelControl;
            if (label != null)
            {
                foreach (var item in Controls)
                {
                    if (item is LabelControl labelControl)
                    {
                        labelControl.Font = new Font(labelControl.Font.Name, labelControl.Font.SizeInPoints, FontStyle.Underline);
                    }
                }
            }
        }

        private void lblTask_MouseLeave(object sender, EventArgs e)
        {
            var label = sender as LabelControl;
            if (label != null)
            {
                foreach (var item in Controls)
                {
                    if (item is LabelControl labelControl)
                    {
                        labelControl.Font = new Font(labelControl.Font.Name, labelControl.Font.SizeInPoints, FontStyle.Regular);
                    }
                }
            }
        }

        private void btnCongratulate_Click(object sender, EventArgs e)
        {
            var form = new VacationEdit(_vacation);
            form.ShowDialog();
        }

        private void lblCustomer_Click(object sender, EventArgs e)
        {
            var form = new VacationEdit(_vacation);
            form.ShowDialog();
        }

        private async void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (DevXtraMessageBox.ShowQuestionXtraMessageBox("Утвердить отпуск?"))
                {
                    _vacation.IsConfirm = true;
                    _vacation.Save();

                    SaveEvent?.Invoke(this);

                    await VacationForm.SendMessageTelegram(_vacation).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }
    }
}
