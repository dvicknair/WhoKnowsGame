using Microsoft.AspNetCore.SignalR;
using WhoKnowsGame.Shared.Models;

namespace WhoKnowsGame.SignalR
{
    public class GameHub : Hub
    {
        public async Task ReportNewPlayer(Player player)
        {
            await Clients.All.SendAsync("ReceiveNewPlayer", player);
        }
    }
}
