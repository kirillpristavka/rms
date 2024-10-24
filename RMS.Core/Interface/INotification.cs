using RMS.Core.Enumerator;
using RMS.Core.Model.Notifications;

namespace RMS.Core.Interface
{
    public interface INotification
    {
        Notification GetNotification(TypeNotification typeNotification);
        string StatusString { get; }
    }
}
