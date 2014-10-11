using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using RecipiesModelNS;
using RecipiesMVC.Models;

namespace RecipiesMVC.Controllers
{
    public class EmployeeController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request, RecipiesEntities context)
        {
            JsonResult result = ReadBase<Employee, EmployeeViewModel>(request, context.Employees);
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<EmployeeViewModel> employees)
        {
            ActionResult result = CreateBase(request, employees, typeof (EmployeeViewModel), typeof (Employee));
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<EmployeeViewModel> employees)
        {
            ActionResult result = UpdateBase(request, employees, typeof (EmployeeViewModel), typeof (Employee));
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<EmployeeViewModel> employees)
        {
            ActionResult result = DestroyBase(request, employees, typeof (EmployeeViewModel), typeof (Employee));
            return result;
        }
    }
}