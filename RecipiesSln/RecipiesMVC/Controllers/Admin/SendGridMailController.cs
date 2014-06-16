using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using RecipiesMVC.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RecipiesModelNS;
using Kendo.Mvc;

namespace RecipiesMVC.Controllers
{
    public class SendGridMailController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request, RecipiesEntities context)
        {
            JsonResult result = ReadBase<SendGridMail, SendGridMailViewModel>(request, context.SendGridMails.OrderByDescending(sgm => sgm.PrimaryKey));
            return result;
        }
    }
}