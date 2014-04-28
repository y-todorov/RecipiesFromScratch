using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Kendo.Mvc;
using Ninject;
using RecipiesWebFormApp;
using RecipiesMVC.App_Start;
using RecipiesMVC.Infrastructure;
using RecipiesWebFormApp.Shared;
using System.Web.Http;
using System.Web.Routing;

namespace RecipiesMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            if (!SiteMapManager.SiteMaps.ContainsKey("sitemap"))
            {
                SiteMapManager.SiteMaps.Register<XmlSiteMap>("sitemap", sitemap =>
                    sitemap.LoadFrom("~/sitemap.sitemap"));
            }
            GlobalConfiguration.Configure(WebApiConfig.Register); // web api registration for api contrllers

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperConfig.Execute();

            DependencyResolver.SetResolver(new NinjectDependencyResolver(new StandardKernel()));


           

            LogentriesHelper.WriteMessage("public class MvcApplication : System.Web.HttpApplication START", LogentriesMessageType.Info);
        }
    }
}
