using DevExpress.Xpo;
using System;

namespace RMS.Setting.Model.ColorSettings
{
    public class ColorStatus : XPObject
    {
        public ColorStatus() { }
        public ColorStatus(Session session) : base(session) { }

        public bool IsDefault { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }        

        public string StatusArchiveFolderNew { get; set; }
        public string StatusArchiveFolderIsCompleted { get; set; }
        public string StatusArchiveFolderSortout { get; set; }
        public string StatusArchiveFolderIssued { get; set; }
        public string StatusArchiveFolderDone { get; set; }
        public string StatusArchiveFolderReturned { get; set; }
        
        public string StatusArchiveFolderDestroyed { get; set; }

        public string StatusArchiveFolderIsSuedEDS { get; set; }
        public string StatusArchiveFolderReceivedEDS { get; set; }
        public string StatusArchiveFolderIsSuedTK { get; set; }
        public string StatusArchiveFolderReceivedTK { get; set; }

        public string StatusTaskCourierPerformed { get; set; }
        public string StatusTaskCourierCanceled { get; set; }
        public string StatusTaskCourierNew { get; set; }

        public string StatusReportNew { get; set; }
        public string StatusReportSurrendered { get; set; }
        public string StatusReportNotSurrendered { get; set; }
        public string StatusReportSent { get; set; }
        public string StatusReportPrepared { get; set; }
        public string StatusReportNotAccepted { get; set; }
        public string StatusReportDoesnotgiveup { get; set; }
        public string StatusReportRentedByTheClient { get; set; }

        [Obsolete]
        public string StatusReportNeedsadjustmentourfault { get; set; }
        [Obsolete]
        public string StatusReportNeedsadjustmentcustomerfault { get; set; }
        
        public string StatusReportAdjustmentRequired { get; set; }
        public string StatusReportAdjustmentIsReady { get; set; }
        public string StatusReportCorrectionSent { get; set; }
        public string StatusReportCorrectionSubmitted { get; set; }

        public string StatusDealNew { get; set; }
        public string StatusDealPostponed { get; set; }
        public string StatusDealCompleted { get; set; }
        public string StatusDealPrimary { get; set; }
        public string StatusDealAdministrator { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}