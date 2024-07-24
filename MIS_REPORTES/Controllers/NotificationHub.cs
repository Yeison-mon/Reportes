using Microsoft.AspNetCore.SignalR;
using MIS_REPORTES.Models;

namespace MIS_REPORTES.Controllers
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
