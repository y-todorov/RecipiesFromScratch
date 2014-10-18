using AutoMapper.QueryableExtensions;
using RecipiesMVC.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RecipiesMVC.Controllers
{
    public class StoreController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request, RecipiesEntities context)
        {
            var newRes = ReadBase<Store, StoreViewModel>(request, context.Stores);
            return newRes;
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<StoreViewModel> stores)
        {
            var result = CreateBase(request, stores, typeof(StoreViewModel), typeof(Store));
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<StoreViewModel> stores)
        {
            var result = UpdateBase(request, stores, typeof(StoreViewModel), typeof(Store));
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<StoreViewModel> stores)
        {
            var result = DestroyBase(request, stores, typeof(StoreViewModel), typeof(Store));
            return result;
        }
    }
}