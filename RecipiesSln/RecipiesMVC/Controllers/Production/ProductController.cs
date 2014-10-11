//using RecipiesWebFormApp.Caching;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RecipiesModelNS;
using RecipiesMVC.Models;

namespace RecipiesMVC.Controllers
{
    public class ProductController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request, RecipiesEntities context)
        {
            JsonResult result = ReadBase<Product, ProductViewModel>(request, context.Products);
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductViewModel> products)
        {
            ActionResult result = CreateBase(request, products, typeof (ProductViewModel), typeof (Product));
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductViewModel> products)
        {
            //HttpRuntime.Close(); .. THIS IS God Damn fucking slow. NEVER EVER use it!

            ActionResult result = UpdateBase(request, products, typeof (ProductViewModel), typeof (Product));
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductViewModel> products)
        {
            ActionResult result = DestroyBase(request, products, typeof (ProductViewModel), typeof (Product));
            return result;
        }

        public ActionResult ReadProductRecipies(int? productId, [DataSourceRequest] DataSourceRequest request)
        {
            ActionResult result = ReadBase(request, typeof (RecipeViewModel), typeof (Recipe),
                ContextFactory.Current.Recipes.Where(r => r.ProductIngredients.Any(pi => pi.ProductId == productId))
                    .ToList());
            return result;
        }

        public ActionResult ReadProductInventories(int? productId, [DataSourceRequest] DataSourceRequest request)
        {
            List<ProductInventory> productInventories =
                ContextFactory.Current.Inventories.OfType<ProductInventory>()
                    .Where(pi => pi.ProductId == productId)
                    .OrderByDescending(de => de.ProductInventoryHeader.ForDate)
                    .ToList();

            ActionResult result = ReadBase(request, typeof (ProductInventoryViewModel), typeof (ProductInventory),
                productInventories);
            return result;
        }

        public ActionResult ReadProductPurchaseOrders(int? productId, [DataSourceRequest] DataSourceRequest request)
        {
            List<PurchaseOrderDetail> productPurchaseOrders =
                ContextFactory.Current.PurchaseOrderDetails.Where
                    (pi => pi.ProductId == productId)
                    .OrderByDescending(de => de.PurchaseOrderHeader.OrderDate)
                    .ToList();
            List<PurchaseOrderDetailViewModel> productPurchaseOrdersModels = productPurchaseOrders.Select
                (r =>
                    PurchaseOrderDetailViewModel.ConvertFromPurchaseOrderDetailEntity(r,
                        new PurchaseOrderDetailViewModel()))
                .ToList();

            return Json(productPurchaseOrdersModels.ToDataSourceResult(request));
        }

        public ActionResult ReadProductWastes(int? productId, [DataSourceRequest] DataSourceRequest request)
        {
            List<ProductWaste> productWastes =
                ContextFactory.Current.Wastes.OfType<ProductWaste>().Where
                    (pi => pi.ProductId == productId).ToList().Where(w => w.Quantity.GetValueOrDefault() != 0)
                    .OrderByDescending(de => de.ProductWasteHeader.ForDate)
                    .ToList();
            ActionResult result = ReadBase(request, typeof (ProductWasteViewModel), typeof (ProductWaste),
                productWastes);


            return result;
        }

        public ActionResult ReadProductUnitsInStockPurchaseOrders(int? productId,
            [DataSourceRequest] DataSourceRequest request)
        {
            Product product = ContextFactory.Current.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                ProductInventory inv = product.GetLastInventoryForDate(DateTime.Now.Date);

                if (inv != null)
                {
                    List<PurchaseOrderDetail> res =
                        product.GetPurchaseOrderDetailsInPeriod(inv.ProductInventoryHeader.ForDate.GetValueOrDefault(),
                            DateTime.Now);
                    ActionResult result = ReadBase(request, typeof (PurchaseOrderDetailViewModel),
                        typeof (PurchaseOrderDetail),
                        res);
                    return result;
                }
            }
            return null;
        }

        public ActionResult ReadProductUnitsInStockSalesOrderDetails(int? productId,
            [DataSourceRequest] DataSourceRequest request)
        {
            Product product = ContextFactory.Current.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                ProductInventory inv = product.GetLastInventoryForDate(DateTime.Now.Date);

                if (inv != null)
                {
                    List<SalesOrderDetail> res =
                        product.GetSalesOrderDetailsForPeriod(inv.ProductInventoryHeader.ForDate.GetValueOrDefault(),
                            DateTime.Now);
                    ActionResult result = ReadBase(request, typeof (SalesOrderDetailViewModel),
                        typeof (SalesOrderDetail),
                        res);
                    return result;
                }
            }
            return null;
        }

        public ActionResult ReadProductUnitsInStockProductWastes(int? productId,
            [DataSourceRequest] DataSourceRequest request)
        {
            Product product = ContextFactory.Current.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                ProductInventory inv = product.GetLastInventoryForDate(DateTime.Now.Date);

                if (inv != null)
                {
                    List<ProductWaste> res =
                        product.GetProductWastes(inv.ProductInventoryHeader.ForDate.GetValueOrDefault(), DateTime.Now);
                    ActionResult result = ReadBase(request, typeof (ProductWasteViewModel), typeof (ProductWaste),
                        res);
                    return result;
                }
            }
            return null;
        }

        public ActionResult ReadProductInventory(int? productId,
            [DataSourceRequest] DataSourceRequest request)
        {
            Product product = ContextFactory.Current.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                //ProductInventory inv = product.GetLastInventoryForDate(DateTime.Now.Date);

                //if (inv == null)
                //{

                //}

                List<ProductInventory> invs = product.ProductInventories.ToList();
                // new List<ProductInventory>() { inv };

                ActionResult result = ReadBase(request, typeof (ProductInventoryViewModel), typeof (ProductInventory),
                    invs);
                return result;
            }
            return null;
        }


        public string Test(int? productId)
        {
            return DateTime.Now.ToString();
        }

        public string CalculateProductsUnitPrice()
        {
            //trywe
            {
                Product.UpdateUnitPriceOfAllProducts();
                return "CalculateProductsUnitPrice succeeded!";
            }
            //catch (Exception ex)
            //{
            //    return ex.Message;
            //}
        }
    }
}

// test 2