using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MysteryCaseAPI.Hubs
{
    [Authorize]
    public class CaseHub : Hub
    {
        public async Task JoinCaseGroup(string caseId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, caseId);
            await Clients.Group(caseId).SendAsync("StatusUpdate", $"{Context.User?.Identity?.Name} has joined the investigation.");
        }

        public async Task LeaveCaseGroup(string caseId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, caseId);
        }
    }
}
