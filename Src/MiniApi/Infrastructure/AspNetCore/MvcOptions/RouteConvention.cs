using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applet.API.Infrastructure
{
    public class RouteConvention : IApplicationModelConvention
    {
        readonly AttributeRouteModel _routePrefix;

        readonly bool _justApiController;

        public RouteConvention(IRouteTemplateProvider routeTemplateProvider, bool justApiController = true)
        {
            _routePrefix = new AttributeRouteModel(routeTemplateProvider);
            _justApiController = justApiController;
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers.Where(a => !_justApiController || a.ControllerType.BaseType == typeof(ControllerBase)))
            {
                foreach (var item in controller.Selectors.Where(x => x.AttributeRouteModel != null))
                {
                    if (item.AttributeRouteModel != null)
                    {
                        item.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_routePrefix, item.AttributeRouteModel);
                    }
                    else
                    {
                        item.AttributeRouteModel = _routePrefix;
                    }
                }
            }
        }
    }

    public static class MvcOptionsExtensions
    {
        public static void UseCentralRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute)
        {
            opts.Conventions.Insert(0, new RouteConvention(routeAttribute));
        }
    }
}
