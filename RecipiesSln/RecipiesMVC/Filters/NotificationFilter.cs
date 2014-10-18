using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using System.Text;



namespace RecipiesMVC.Filters
{
    public class NotificationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<RecipiesMVC.Hubs.NotificationHub>();
            string message = string.Format("{0} {1} {2} {3}",
                DateTime.Now.ToLongTimeString(),
                "OnActionExecuted", filterContext.ActionDescriptor.ActionName,
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName);
            context.Clients.All.notify(message);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            WriteText("OnActionExecuting");
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            WriteText("OnResultExecuted");
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            WriteText("OnResultExecuting");
        }

        private void WriteText(params string[] args)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<RecipiesMVC.Hubs.NotificationHub>();

            StringBuilder sb = new StringBuilder();
            foreach (var s in args)
            {
                sb.Append(s);
                sb.Append(" ");
            }
            context.Clients.All.notify(DateTime.Now.ToUniversalTime() + " " + sb);
        }
    }
}