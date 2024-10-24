using DevExpress.Xpo;
using RMS.Setting.Model.ColorSettings;
using System;
using System.Drawing;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class ColorStatusEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private ColorStatus ColorStatus { get; }

        public ColorStatusEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                ColorStatus = new ColorStatus(Session);
            }
        }

        public ColorStatusEdit(int id) : this()
        {
            if (id > 0)
            {
                ColorStatus = Session.GetObjectByKey<ColorStatus>(id);
            }
        }

        public ColorStatusEdit(ColorStatus colorStatus) : this()
        {
            Session = colorStatus.Session;
            ColorStatus = colorStatus;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ColorStatus.Name = txtName.Text;
            ColorStatus.Description = memoDescription.Text;

            ColorStatus.StatusReportNew = ColorTranslator.ToHtml(colorStatusReportNew.Color);
            ColorStatus.StatusReportSurrendered = ColorTranslator.ToHtml(colorStatusReportSurrendered.Color);
            ColorStatus.StatusReportNotSurrendered = ColorTranslator.ToHtml(colorStatusReportNotSurrendered.Color);
            ColorStatus.StatusReportSent = ColorTranslator.ToHtml(colorStatusReportSent.Color);
            ColorStatus.StatusReportPrepared = ColorTranslator.ToHtml(colorStatusReportPrepared.Color);
            ColorStatus.StatusReportNotAccepted = ColorTranslator.ToHtml(colorStatusReportNotAccepted.Color);
            ColorStatus.StatusReportRentedByTheClient = ColorTranslator.ToHtml(colorStatusReportRentedByTheClient.Color);
            ColorStatus.StatusReportDoesnotgiveup = ColorTranslator.ToHtml(colorStatusReportDoesnotgiveup.Color);
            
            ColorStatus.StatusReportAdjustmentRequired = ColorTranslator.ToHtml(colorStatusReportAdjustmentRequired.Color);
            ColorStatus.StatusReportAdjustmentIsReady = ColorTranslator.ToHtml(colorStatusReportAdjustmentIsReady.Color);
            ColorStatus.StatusReportCorrectionSent = ColorTranslator.ToHtml(colorStatusReportCorrectionSent.Color);
            ColorStatus.StatusReportCorrectionSubmitted = ColorTranslator.ToHtml(colorStatusReportCorrectionSubmitted.Color);
            
            ColorStatus.StatusArchiveFolderNew = ColorTranslator.ToHtml(colorStatusArchiveFolderNew.Color);
            ColorStatus.StatusArchiveFolderIsCompleted = ColorTranslator.ToHtml(colorStatusArchiveFolderIsCompleted.Color);
            ColorStatus.StatusArchiveFolderSortout = ColorTranslator.ToHtml(colorStatusArchiveFolderSortout.Color);
            ColorStatus.StatusArchiveFolderIssued = ColorTranslator.ToHtml(colorStatusArchiveFolderIssued.Color);
            ColorStatus.StatusArchiveFolderDone = ColorTranslator.ToHtml(colorStatusArchiveFolderDone.Color);
            ColorStatus.StatusArchiveFolderReturned = ColorTranslator.ToHtml(colorStatusArchiveFolderReturned.Color);

            ColorStatus.StatusArchiveFolderDestroyed = ColorTranslator.ToHtml(colorStatusArchiveFolderDestroyed.Color);

            ColorStatus.StatusArchiveFolderIsSuedEDS = ColorTranslator.ToHtml(colorStatusArchiveFolderIsSuedEDS.Color);
            ColorStatus.StatusArchiveFolderReceivedEDS = ColorTranslator.ToHtml(colorStatusArchiveFolderReceivedEDS.Color);
            ColorStatus.StatusArchiveFolderIsSuedTK = ColorTranslator.ToHtml(colorStatusArchiveFolderIsSuedTK.Color);
            ColorStatus.StatusArchiveFolderReceivedTK = ColorTranslator.ToHtml(colorStatusArchiveFolderReceivedTK.Color);

            ColorStatus.StatusTaskCourierPerformed = ColorTranslator.ToHtml(colorStatusTaskCourierPerformed.Color);
            ColorStatus.StatusTaskCourierCanceled = ColorTranslator.ToHtml(colorStatusTaskCourierCanceled.Color);
            ColorStatus.StatusTaskCourierNew = ColorTranslator.ToHtml(colorStatusTaskCourierNew.Color);

            ColorStatus.StatusDealNew = ColorTranslator.ToHtml(colorStatusDealNew.Color);
            ColorStatus.StatusDealPostponed = ColorTranslator.ToHtml(colorStatusDealPostponed.Color);
            ColorStatus.StatusDealCompleted = ColorTranslator.ToHtml(colorStatusDealCompleted.Color);
            ColorStatus.StatusDealPrimary = ColorTranslator.ToHtml(colorStatusDealPrimary.Color);
            ColorStatus.StatusDealAdministrator = ColorTranslator.ToHtml(colorStatusDealAdministrator.Color);

            if (checkIsDefault.Checked)
            {
                var xpcollection = new XPCollection<ColorStatus>(Session);
                foreach (var item in xpcollection)
                {
                    item.IsDefault = false;
                    item.Save();
                }

                ColorStatus.IsDefault = true;
            }

            Session.Save(ColorStatus);
            id = ColorStatus.Oid;            
            
            flagSave = true;
            Close();
        }

        private void FormLoad(object sender, EventArgs e)
        {
            memoDescription.Text = ColorStatus.Description;
            txtName.Text = ColorStatus.Name;
            checkIsDefault.Checked = ColorStatus.IsDefault;

            colorStatusReportNew.Color = ColorTranslator.FromHtml(ColorStatus.StatusReportNew);
            colorStatusReportSurrendered.Color = ColorTranslator.FromHtml(ColorStatus.StatusReportSurrendered);
            colorStatusReportNotSurrendered.Color = ColorTranslator.FromHtml(ColorStatus.StatusReportNotSurrendered);
            colorStatusReportSent.Color = ColorTranslator.FromHtml(ColorStatus.StatusReportSent);
            colorStatusReportPrepared.Color = ColorTranslator.FromHtml(ColorStatus.StatusReportPrepared);
            colorStatusReportNotAccepted.Color = ColorTranslator.FromHtml(ColorStatus.StatusReportNotAccepted);
            colorStatusReportRentedByTheClient.Color = ColorTranslator.FromHtml(ColorStatus.StatusReportRentedByTheClient);
            colorStatusReportDoesnotgiveup.Color = ColorTranslator.FromHtml(ColorStatus.StatusReportDoesnotgiveup);

            colorStatusReportAdjustmentRequired.Color = ColorTranslator.FromHtml(ColorStatus.StatusReportAdjustmentRequired);
            colorStatusReportAdjustmentIsReady.Color = ColorTranslator.FromHtml(ColorStatus.StatusReportAdjustmentIsReady);
            colorStatusReportCorrectionSent.Color = ColorTranslator.FromHtml(ColorStatus.StatusReportCorrectionSent);
            colorStatusReportCorrectionSubmitted.Color = ColorTranslator.FromHtml(ColorStatus.StatusReportCorrectionSubmitted);

            colorStatusArchiveFolderNew.Color = ColorTranslator.FromHtml(ColorStatus.StatusArchiveFolderNew);
            colorStatusArchiveFolderIsCompleted.Color = ColorTranslator.FromHtml(ColorStatus.StatusArchiveFolderIsCompleted);
            colorStatusArchiveFolderSortout.Color = ColorTranslator.FromHtml(ColorStatus.StatusArchiveFolderSortout);
            colorStatusArchiveFolderIssued.Color = ColorTranslator.FromHtml(ColorStatus.StatusArchiveFolderIssued);
            colorStatusArchiveFolderDone.Color = ColorTranslator.FromHtml(ColorStatus.StatusArchiveFolderDone);
            colorStatusArchiveFolderReturned.Color = ColorTranslator.FromHtml(ColorStatus.StatusArchiveFolderReturned);

            colorStatusArchiveFolderDestroyed.Color = ColorTranslator.FromHtml(ColorStatus.StatusArchiveFolderDestroyed);

            colorStatusArchiveFolderIsSuedEDS.Color = ColorTranslator.FromHtml(ColorStatus.StatusArchiveFolderIsSuedEDS);
            colorStatusArchiveFolderReceivedEDS.Color = ColorTranslator.FromHtml(ColorStatus.StatusArchiveFolderReceivedEDS);
            colorStatusArchiveFolderIsSuedTK.Color = ColorTranslator.FromHtml(ColorStatus.StatusArchiveFolderIsSuedTK);
            colorStatusArchiveFolderReceivedTK.Color = ColorTranslator.FromHtml(ColorStatus.StatusArchiveFolderReceivedTK);

            colorStatusTaskCourierPerformed.Color = ColorTranslator.FromHtml(ColorStatus.StatusTaskCourierPerformed);
            colorStatusTaskCourierCanceled.Color = ColorTranslator.FromHtml(ColorStatus.StatusTaskCourierCanceled);
            colorStatusTaskCourierNew.Color = ColorTranslator.FromHtml(ColorStatus.StatusTaskCourierNew);

            colorStatusDealNew.Color = ColorTranslator.FromHtml(ColorStatus.StatusDealNew);
            colorStatusDealPostponed.Color = ColorTranslator.FromHtml(ColorStatus.StatusDealPostponed);
            colorStatusDealCompleted.Color = ColorTranslator.FromHtml(ColorStatus.StatusDealCompleted);
            colorStatusDealPrimary.Color = ColorTranslator.FromHtml(ColorStatus.StatusDealPrimary);
            colorStatusDealAdministrator.Color = ColorTranslator.FromHtml(ColorStatus.StatusDealAdministrator);
        }
    }
}