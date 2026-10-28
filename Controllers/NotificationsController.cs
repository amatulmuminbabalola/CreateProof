// // using System.Security.Claims;
// // using CreateProof.Interfaces.Services;
// // using CreateProof.ViewModels.Notifications;
// // // using Microsoft.AspNetCore.Authorization;
// // using Microsoft.AspNetCore.Mvc;

// // namespace CreateProof.Controllers;

// // // [Authorize]
// // public class NotificationsController : Controller
// // {
// // 	private readonly INotificationService _notificationService;

// // 	public NotificationsController(INotificationService notificationService)
// // 	{
// // 		_notificationService = notificationService;
// // 	}

// // 	[HttpGet]
// // 	public async Task<IActionResult> Index()
// // 	{
// // 		var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
// // 		if (string.IsNullOrEmpty(userId))
// // 		{
// // 			return Forbid();
// // 		}

// // 		var notifications = await _notificationService.GetNotificationsAsync(userId);
// // 		var unreadCount = await _notificationService.GetUnreadCountAsync(userId);

// // 		var viewModel = new NotificationListViewModel
// // 		{
// // 			Notifications = notifications,
// // 			UnreadCount = unreadCount,
// // 			TotalCount = notifications.Count()
// // 		};

// // 		return View(viewModel);
// // 	}

// // 	[HttpGet]
// // 	public async Task<IActionResult> UnreadCount()
// // 	{
// // 		var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
// // 		if (string.IsNullOrEmpty(userId))
// // 		{
// // 			return Forbid();
// // 		}

// // 		var count = await _notificationService.GetUnreadCountAsync(userId);
// // 		return Json(new { unreadCount = count });
// // 	}

// // 	[HttpPost]
// // 	[ValidateAntiForgeryToken]
// // 	public async Task<IActionResult> MarkAsRead(int id)
// // 	{
// // 		var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
// // 		if (string.IsNullOrEmpty(userId))
// // 		{
// // 			return Forbid();
// // 		}

// // 		var success = await _notificationService.MarkAsReadAsync(id, userId);
// // 		if (!success)
// // 		{
// // 			return NotFound();
// // 		}

// // 		return RedirectToAction(nameof(Index));
// // 	}

// // 	[HttpPost]
// // 	[ValidateAntiForgeryToken]
// // 	public async Task<IActionResult> MarkAllAsRead()
// // 	{
// // 		var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
// // 		if (string.IsNullOrEmpty(userId))
// // 		{
// // 			return Forbid();
// // 		}

// // 		await _notificationService.MarkAllAsReadAsync(userId);
// // 		return RedirectToAction(nameof(Index));
// // 	}
// // }


// using System.Security.Claims;
// using CreateProof.Interfaces.Services;
// // using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;

// namespace CreateProof.Controllers
// {
//     // [Authorize]
//     public class NotificationsController : Controller
//     {
//         private readonly INotificationService _notificationService;

//         public NotificationsController(
//             INotificationService notificationService)
//         {
//             _notificationService = notificationService;
//         }

//         // GET: /Notifications
//         [HttpGet]
//         public async Task<IActionResult> Index()
//         {
//             var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

//             if (string.IsNullOrEmpty(userId))
//             {
//                 return Forbid();
//             }

//             var notifications =
//                 await _notificationService.GetNotificationsAsync(userId);

//             var unreadCount =
//                 await _notificationService.GetUnreadCountAsync(userId);

//             var viewModel = new CreateProof.ViewModels.Notifications
//                 .NotificationListViewModel
//             {
//                 Notifications = notifications,
//                 UnreadCount = unreadCount,
//                 TotalCount = notifications.Count()
//             };

//             return View(viewModel);
//         }

//         // GET: /Notifications/UnreadCount
//         [HttpGet]
//         public async Task<IActionResult> UnreadCount()
//         {
//             var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

//             if (string.IsNullOrEmpty(userId))
//             {
//                 return Forbid();
//             }

//             var count =
//                 await _notificationService.GetUnreadCountAsync(userId);

//             return Json(new { unreadCount = count });
//         }

//         // POST: /Notifications/MarkAsRead/5
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> MarkAsRead(int id)
//         {
//             var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

//             if (string.IsNullOrEmpty(userId))
//             {
//                 return Forbid();
//             }

//             var success =
//                 await _notificationService.MarkAsReadAsync(id, userId);

//             if (!success)
//             {
//                 return NotFound();
//             }

//             return RedirectToAction(nameof(Index));
//         }

//         // POST: /Notifications/MarkAllAsRead
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> MarkAllAsRead()
//         {
//             var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

//             if (string.IsNullOrEmpty(userId))
//             {
//                 return Forbid();
//             }

//             await _notificationService.MarkAllAsReadAsync(userId);

//             return RedirectToAction(nameof(Index));
//         }
//     }
// }
using CreateProof.Interfaces.Services;
using CreateProof.ViewModels.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace CreateProof.Controllers;

public class NotificationsController : Controller
{
private readonly INotificationService _notificationService;

// DEVELOPMENT ONLY: Replace with proper authenticated user identity before production.
private const string DevelopmentUserId = "development-user";

public NotificationsController(INotificationService notificationService)
{
    _notificationService = notificationService;
}

[HttpGet]
public async Task<IActionResult> Index()
{
    var notifications = (await _notificationService
        .GetNotificationsAsync(DevelopmentUserId)).ToList();

    var unreadCount = await _notificationService
        .GetUnreadCountAsync(DevelopmentUserId);

    var viewModel = new NotificationListViewModel
    {
        Notifications = notifications,
        UnreadCount = unreadCount,
        TotalCount = notifications.Count
    };

    return View(viewModel);
}

[HttpGet]
public async Task<IActionResult> UnreadCount()
{
    var count = await _notificationService
        .GetUnreadCountAsync(DevelopmentUserId);

    return Json(new { unreadCount = count });
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> MarkAsRead(int id)
{
    var success = await _notificationService
        .MarkAsReadAsync(id, DevelopmentUserId);

    if (!success)
    {
        return NotFound();
    }

    return RedirectToAction(nameof(Index));
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> MarkAllAsRead()
{
    await _notificationService.MarkAllAsReadAsync(DevelopmentUserId);

    return RedirectToAction(nameof(Index));
}

}