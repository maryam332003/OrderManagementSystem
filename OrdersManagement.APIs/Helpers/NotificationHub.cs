using Microsoft.AspNetCore.SignalR;

namespace OrdersManagement.APIs.Helpers
{
    public class NotificationHub:Hub
    {
        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
