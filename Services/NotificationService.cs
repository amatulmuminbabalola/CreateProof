using CreateProof.Interfaces.Services;
using CreateProof.Interfaces.Repositories;
using CreateProof.Models;
using CreateProof.ViewModels.Notifications;

namespace CreateProof.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepositories _notificationRepositories;

        public NotificationService(
            INotificationRepositories notificationRepositories)
        {
            _notificationRepositories = notificationRepositories;
        }

        public async Task<IEnumerable<NotificationDetailsViewModel>>
            GetNotificationsAsync(string userId)
        {
            var notifications =
                await _notificationRepositories.GetByUserIdAsync(userId);

            return notifications.Select(n => new NotificationDetailsViewModel
            {
                Id = n.Id,
                Title = n.Title,
                Message = n.Message,
                Type = n.Type,
                ProjectId = n.ProjectId,
                ProjectTitle = n.Project?.Title,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt,
                ReadAt = n.ReadAt
            });
        }

        public Task<int> GetUnreadCountAsync(string userId)
        {
            return _notificationRepositories.GetUnreadCountAsync(userId);
        }

        public async Task<bool> MarkAsReadAsync(
            int notificationId,
            string userId)
        {
            var notification =
                await _notificationRepositories.GetByIdAsync(notificationId);

            if (notification == null || notification.UserId != userId)
            {
                return false;
            }

            if (!notification.IsRead)
            {
                await _notificationRepositories.MarkAsReadAsync(notification);
                await _notificationRepositories.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> MarkAllAsReadAsync(string userId)
        {
            await _notificationRepositories.MarkAllAsReadAsync(userId);
            await _notificationRepositories.SaveChangesAsync();

            return true;
        }

        public async Task CreateAsync(
            string userId,
            string title,
            string message,
            string type,
            int? projectId = null)
        {
            var notification = new Notification
            {
                UserId = userId,
                Title = title,
                Message = message,
                Type = type,
                ProjectId = projectId,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            await _notificationRepositories.AddAsync(notification);
            await _notificationRepositories.SaveChangesAsync();
        }
    }
}