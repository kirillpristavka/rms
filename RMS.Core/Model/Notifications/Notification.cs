using RMS.Core.Enumerator;
using System;
using System.ComponentModel;

namespace RMS.Core.Model.Notifications
{
    /// <summary>
    /// Уведомление.
    /// </summary>
    public class Notification
    {
        public Notification(TypeNotification typeNotification, string nameModel, DateTime dateTime, string name, int id, Type type)
        {
            TypeNotification = typeNotification;
            NameModel = nameModel;
            DateTime = dateTime;
            Name = name;
            Id = id;
            Type = type;
        }
        
        /// <summary>
        /// Тип уведомления.
        /// </summary>
        [DisplayName("Тип уведомления")]
        public TypeNotification TypeNotification { get; private set; }

        /// <summary>
        /// Модуль.
        /// </summary>
        [DisplayName("Модуль")]
        public string NameModel { get; set; }

        /// <summary>
        /// Дата объекта.
        /// </summary>
        [DisplayName("Дата")]
        public DateTime DateTime { get; private set; }

        /// <summary>
        /// Наименование объекта.
        /// </summary>
        [DisplayName("Наименование объекта")]
        public string Name { get; private set; }

        /// <summary>
        /// Уникальный идентификатор объекта.
        /// </summary>
        [DisplayName("Уникальный идентификатор объекта")]
        public int Id { get; private set; }

        /// <summary>
        /// Тип объекта.
        /// </summary>
        [DisplayName("Тип")]
        public Type Type { get; private set; }
        
        /// <summary>
        /// Обновление оповещения.
        /// </summary>
        /// <param name="notification"></param>
        public void Update(Notification notification)
        {
            TypeNotification = notification.TypeNotification;
            NameModel = notification.NameModel;
            DateTime = notification.DateTime;
            Name = notification.Name;
            Id = notification.Id;
            Type = notification.Type;
        }
    }
}
