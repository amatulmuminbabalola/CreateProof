using CreateProof.ViewModels.Notifications;

namespace CreateProof.Interfaces.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDetailsViewModel>>
            GetNotificationsAsync(string userId);

        Task<int> GetUnreadCountAsync(string userId);

        Task<bool> MarkAsReadAsync(
            int notificationId,
            string userId);

        Task<bool> MarkAllAsReadAsync(string userId);

        Task CreateAsync(
            string userId,
            string title,
            string message,
            string type,
            int? projectId = null);
    }
}