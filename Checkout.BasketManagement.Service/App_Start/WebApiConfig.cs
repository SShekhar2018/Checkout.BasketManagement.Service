using Checkout.BasketManagement.Core;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Checkout.BasketManagement.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "Swagger UI",
            routeTemplate: "",
            defaults: null,
            constraints: null,
            handler: new RedirectHandler(SwaggerDocsConfig.DefaultRootUrlResolver, "swagger/ui/index")
            );
        }
    }
}
