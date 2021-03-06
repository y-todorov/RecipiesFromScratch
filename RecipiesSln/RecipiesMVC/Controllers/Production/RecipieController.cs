﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RecipiesMVC.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RecipiesModelNS;
using System.Threading.Tasks;

namespace RecipiesMVC.Controllers
{
    public class RecipeController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request, RecipiesEntities context)
        {
            JsonResult result = ReadBase<Recipe, RecipeViewModel>(request,context.Recipes);
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<RecipeViewModel> recipies)
        {
            var result = CreateBase(request, recipies, typeof(RecipeViewModel), typeof(Recipe));
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<RecipeViewModel> recipies)
        {
            var result = UpdateBase(request, recipies, typeof(RecipeViewModel), typeof(Recipe));
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<RecipeViewModel> recipies)
        {
            var result = DestroyBase(request, recipies, typeof(RecipeViewModel), typeof(Recipe));
            return result;
        }
    }
}