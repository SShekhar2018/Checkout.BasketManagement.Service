using Checkout.BasketManagement.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

//[assembly: PreApplicationStartMethod(typeof(WebApiApplication), "Application_Start")]
namespace Checkout.BasketManagement.Service
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public void Application_Start()
        {
            RouteTable.Routes.RouteExistingFiles = true;

            SwaggerConfig.Register();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            //RouteParameter..RegisterRoutes(RouteTable.Routes);
            
            //GlobalConfiguration.Configure(SwaggerConfig.Register);
        }
    }
}
