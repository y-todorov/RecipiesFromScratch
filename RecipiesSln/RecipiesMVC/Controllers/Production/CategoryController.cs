using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using RecipiesModelNS;
using RecipiesMVC.Models;
using Microsoft.Web.Mvc;

namespace RecipiesMVC.Controllers
{
    public class CategoryController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request, RecipiesEntities context)
        {
            JsonResult result = ReadBase<ProductCategory, CategoryViewModel>(request, context.ProductCategories);
            return result;
        }

        public ActionResult ReadProducts(int? categoryId, [DataSourceRequest] DataSourceRequest request)
        {
            ActionResult result = ReadBase(request, typeof (ProductViewModel), typeof (Product),
                ContextFactory.Current.Products.Where(p => p.CategoryId == categoryId).ToList());
            return result;
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<CategoryViewModel> categories)
        {
            ActionResult result = CreateBase(request, categories, typeof (CategoryViewModel), typeof (ProductCategory));
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<CategoryViewModel> categories)
        {
            ActionResult result = UpdateBase(request, categories, typeof (CategoryViewModel), typeof (ProductCategory));
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<CategoryViewModel> categories)
        {
            ActionResult result = DestroyBase(request, categories, typeof (CategoryViewModel), typeof (ProductCategory));
            return result;
        }
    }
}