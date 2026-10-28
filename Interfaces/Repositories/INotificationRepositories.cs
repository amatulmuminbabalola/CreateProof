using CreateProof.Models;

namespace CreateProof.Interfaces.Repositories
{
    public interface INotificationRepositories 
    {
        Task<Notification?> GetByIdAsync(int id);

        Task<IEnumerable<Notification>> GetByUserIdAsync(
            string userId);

        Task<int> GetUnreadCountAsync(string userId);

        Task AddAsync(Notification notification);

        Task MarkAsReadAsync(Notification notification);

        Task MarkAllAsReadAsync(string userId);

        Task SaveChangesAsync();
    }
}