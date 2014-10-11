using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Net;
using System.Web.Mvc;
using Kendo.Mvc.UI;

namespace RecipiesMVC.ActionFilters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AjaxExceptionFilter : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest()) return;

            filterContext.Result = AjaxError(filterContext.Exception, filterContext);

            //Let the system know that the exception has been handled
            filterContext.ExceptionHandled = true;
        }

        protected JsonResult AjaxError(Exception exception, ExceptionContext filterContext)
        {
            // it is important to be not 500

            //Set the response status code to 500
            filterContext.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            //Needed for IIS7.0
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

            var listOfExMessages = new List<string>();

            if (exception is DbEntityValidationException)
            {
                var dbEx = exception as DbEntityValidationException;

                foreach (DbEntityValidationResult entityValidationResult in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError dbValidationError in entityValidationResult.ValidationErrors)
                    {
                        string fullErrorMessage = string.Format("Field: '{0}', Error: '{1}'.",
                            dbValidationError.PropertyName,
                            dbValidationError.ErrorMessage);
                        listOfExMessages.Add(fullErrorMessage);
                    }
                }
            }
            else
            {
                listOfExMessages.Add(exception.Message);
            }


            var jr = new JsonResult();
            jr.Data = new DataSourceResult
            {
                Errors = listOfExMessages
            };

            return jr;
        }
    }
}