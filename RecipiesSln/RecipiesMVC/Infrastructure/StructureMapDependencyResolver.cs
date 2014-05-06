using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using RecipiesMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecipiesModelNS;
using RecipiesMVC.Controllers.WebApis;
using StructureMap;


namespace RecipiesMVC.Infrastructure
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        private readonly Func<IContainer> _containerFactory;

        public StructureMapDependencyResolver(Func<IContainer> containerFactory)
        {
            _containerFactory = containerFactory;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
            {
                return null;
            }

            IContainer container = _containerFactory();

            object result = null;
            if (serviceType.IsAbstract || serviceType.IsInterface)
            {
                result = container.TryGetInstance(serviceType);
            }
            else
            {
                result = container.GetInstance(serviceType);
            }
            return result;

        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            IContainer container = _containerFactory();
            IEnumerable<object> result = container.GetAllInstances(serviceType)
                .Cast<object>();
            return result;
        }
    }
}