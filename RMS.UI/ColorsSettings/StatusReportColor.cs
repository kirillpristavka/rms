using DevExpress.Xpo;
using RMS.Setting.Model.ColorSettings;
using System.Drawing;

namespace RMS.UI.ColorsSettings
{
    /// <summary>
    /// Цветовая палитра для отчетов.
    /// </summary>
    public static class StatusReportColor
    {
        public static Color ColorStatusReportNew { get; private set; }
        public static Color ColorStatusReportSent { get; private set; }
        public static Color ColorStatusReportSurrendered { get; private set; }
        public static Color ColorStatusReportNotSurrendered { get; private set; }
        public static Color ColorStatusReportPrepared { get; private set; }
        public static Color ColorStatusReportNotAccepted { get; private set; }
        public static Color ColorStatusReportDoesnotgiveup { get; private set; }
        public static Color ColorStatusReportRentedByTheClient { get; private set; }

        public static Color ColorStatusReportAdjustmentRequired { get; private set; }
        public static Color ColorStatusReportAdjustmentIsReady { get; private set; }
        public static Color ColorStatusReportCorrectionSent { get; private set; }
        public static Color ColorStatusReportCorrectionSubmitted { get; private set; }

        public static async System.Threading.Tasks.Task GetStatusReportColor()
        {
            using (var uof = new UnitOfWork())
            {
                var colorStatus = await new XPQuery<ColorStatus>(uof)?.FirstOrDefaultAsync(f => f.IsDefault);
                if (colorStatus != null)
                {
                    ColorStatusReportNew = ColorTranslator.FromHtml(colorStatus.StatusReportNew);
                    ColorStatusReportSent = ColorTranslator.FromHtml(colorStatus.StatusReportSent);
                    ColorStatusReportSurrendered = ColorTranslator.FromHtml(colorStatus.StatusReportSurrendered);
                    ColorStatusReportNotSurrendered = ColorTranslator.FromHtml(colorStatus.StatusReportNotSurrendered);
                    ColorStatusReportPrepared = ColorTranslator.FromHtml(colorStatus.StatusReportPrepared);
                    ColorStatusReportNotAccepted = ColorTranslator.FromHtml(colorStatus.StatusReportNotAccepted);
                    ColorStatusReportDoesnotgiveup = ColorTranslator.FromHtml(colorStatus.StatusReportDoesnotgiveup);
                    ColorStatusReportRentedByTheClient = ColorTranslator.FromHtml(colorStatus.StatusReportRentedByTheClient);

                    ColorStatusReportAdjustmentRequired = ColorTranslator.FromHtml(colorStatus.StatusReportAdjustmentRequired);
                    ColorStatusReportAdjustmentIsReady = ColorTranslator.FromHtml(colorStatus.StatusReportAdjustmentIsReady);
                    ColorStatusReportCorrectionSent = ColorTranslator.FromHtml(colorStatus.StatusReportCorrectionSent);
                    ColorStatusReportCorrectionSubmitted = ColorTranslator.FromHtml(colorStatus.StatusReportCorrectionSubmitted);
                }
                else
                {
                    ColorStatusReportNew = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportNew", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(152, 251, 152))));
                    ColorStatusReportSent = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportSent", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(255, 250, 205))));
                    ColorStatusReportSurrendered = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportSurrendered", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(230, 230, 250))));
                    ColorStatusReportNotSurrendered = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportNotSurrendered", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(250, 128, 114))));
                    ColorStatusReportPrepared = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportPrepared", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(135, 206, 235))));
                    ColorStatusReportNotAccepted = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportNotAccepted", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(255, 0, 0))));
                    ColorStatusReportDoesnotgiveup = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportDoesnotgiveup", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(205, 197, 191))));
                    ColorStatusReportRentedByTheClient = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportRentedByTheClient", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(155, 100, 228))));

                    ColorStatusReportAdjustmentRequired = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportAdjustmentRequired", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(155, 255, 205))));
                    ColorStatusReportAdjustmentIsReady = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportAdjustmentIsReady", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(105, 178, 196))));
                    ColorStatusReportCorrectionSent = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportCorrectionSent", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(55, 125, 186))));
                    ColorStatusReportCorrectionSubmitted = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportCorrectionSubmitted", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(5, 3, 65))));
                }
            }            
        }
    }
}