﻿using System;
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
    }
}