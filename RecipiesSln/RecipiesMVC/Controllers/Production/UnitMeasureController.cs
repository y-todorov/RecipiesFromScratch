using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RecipiesMVC.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RecipiesModelNS;
using System.Threading.Tasks;

namespace RecipiesMVC.Controllers
{
    public class UnitMeasureController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request, RecipiesEntities context)
        {
            var result = ReadBase<UnitMeasure, UnitMeasureViewModel>(request, context.UnitMeasures);
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<UnitMeasureViewModel> unitMeasures)
        {
            var result = CreateBase(request, unitMeasures, typeof(UnitMeasureViewModel), typeof(UnitMeasure));
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<UnitMeasureViewModel> unitMeasures)
        {
            var result = UpdateBase(request, unitMeasures, typeof(UnitMeasureViewModel), typeof(UnitMeasure));
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<UnitMeasureViewModel> unitMeasures)
        {
            var result = DestroyBase(request, unitMeasures, typeof(UnitMeasureViewModel), typeof(UnitMeasure));
            return result;
        }
    }
}