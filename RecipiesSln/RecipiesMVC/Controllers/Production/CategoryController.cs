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
    public class CategoryController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            //JsonResult cachedResult = HttpContext.Cache["categories"] as JsonResult;
            //if (cachedResult != null)
            //{
            //    return cachedResult;
            //}
            //else
            //{
                IQueryable<CategoryViewModel> categories =
                    ContextFactory.Current.ProductCategories.Project().To<CategoryViewModel>();
                DataSourceResult dataSourceResult = categories.ToDataSourceResult(request);
                JsonResult jresult = Json(dataSourceResult);
                //HttpContext.Cache.Add("categories", jresult,
                //null, DateTime.Now.AddMinutes(10),
                //TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
                return jresult;
            //}
        }

        public ActionResult ReadProducts(int categoryId, [DataSourceRequest] DataSourceRequest request)
        {
            var result = ReadBase(request, typeof(ProductViewModel), typeof(Product), ContextFactory.Current.Products.Where(p => p.CategoryId == categoryId).ToList());
            return result;
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<CategoryViewModel> categories)
        {
            var result = CreateBase(request, categories, typeof(CategoryViewModel), typeof(ProductCategory));
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<CategoryViewModel> categories)
        {
            var result = UpdateBase(request, categories, typeof(CategoryViewModel), typeof(ProductCategory));
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<CategoryViewModel> categories)
        {
            var result = DestroyBase(request, categories, typeof(CategoryViewModel), typeof(ProductCategory));
            return result;
        }
    }
}