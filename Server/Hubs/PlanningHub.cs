using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TeamPlanner.Shared;

namespace TeamPlanner.Server.Hubs
{
    //TODO: [Authorize]
    public class PlanningHub : Hub
    {
        public async Task SendTask(Activity activity)
        {
            await Clients.All.SendAsync("ReceiveTask", activity);
        }
    }
}