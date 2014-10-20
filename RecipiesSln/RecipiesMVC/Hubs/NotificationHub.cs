using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace RecipiesMVC.Hubs
{
    [HubName("notificationHub")]
    public class NotificationHub : Hub
    {
        //public void Hello()
        //{
        //    Clients.All.notify();
        //}

        public override System.Threading.Tasks.Task OnConnected()
        {
            Clients.All.notify("User Connected!");
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected()
        {
            Clients.All.notify("User Disconnected!");
            return base.OnDisconnected();
        }

        public override System.Threading.Tasks.Task OnReconnected()
        {
            Clients.All.notify("User Reconnected!");
            return base.OnReconnected();
        }
        
    }
}