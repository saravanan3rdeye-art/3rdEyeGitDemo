using Microsoft.AspNetCore.SignalR;
using _3rdEyeGitDemo.Models;

namespace _3rdEyeGitDemo.Hubs
{
    public class TradeHub : Hub
    {
        public async Task SendTradeUpdate(TradeData tradeData)
        {
            await Clients.All.SendAsync("ReceiveTradeUpdate", tradeData);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}