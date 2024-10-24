using DevExpress.Xpo;
using Newtonsoft.Json;
using RMS.Core.Model;
using RMS.Core.Model.Mail;
using System;
using System.Diagnostics;

namespace RMS.Setting.Model.GeneralSettings
{
    /// <summary>
    /// Общие настройки приложения.
    /// </summary>
    public class Settings : XPObject
    {
        public Settings() { }
        public Settings(Session session) : base(session) { }

        /// <summary>
        /// Использование года, который указан в дате до.
        /// </summary>
        public bool IsUseYearReport { get; set; }

        /// <summary>
        /// Использование года, за который сдается отчет.
        /// </summary>
        public bool IsUseDeliveryYearReport { get; set; }

        /// <summary>
        /// Пользовательская группа для доступа к фильтру ВСЕ.
        /// </summary>
        public UserGroup UserGroupEverything { get; set; }

        /// <summary>
        /// Токен доступа для телеграмм бота.
        /// </summary>
        public string TelegramBotToken { get; set; }

        /// <summary>
        /// Дата фильтрации писем.
        /// </summary>
        public DateTime? EmailFilteringDate { get; set; }

        /// <summary>
        /// Список провайдеров для клиентов без ЭЦП.
        /// </summary>
        public byte[] ElectronicReportingList { get; set; }

        /// <summary>
        /// Список прайса для автозаполнения.
        /// </summary>
        public byte[] PriceList { get; set; }
        
        public static T GetDeserializeObject<T>(byte[] obj)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(Letter.ByteToString(obj));
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }

            return default;
        }
    }
}
