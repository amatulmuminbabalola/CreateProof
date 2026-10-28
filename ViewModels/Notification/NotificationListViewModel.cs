using System.Collections.Generic;

namespace CreateProof.ViewModels.Notifications
{
    public class NotificationListViewModel
    {
        public IEnumerable<NotificationDetailsViewModel> Notifications
        {
            get;
            set;
        } = new List<NotificationDetailsViewModel>();

        public int UnreadCount { get; set; }

        public int TotalCount { get; set; }

        public bool HasNotifications => TotalCount > 0;
    }
}