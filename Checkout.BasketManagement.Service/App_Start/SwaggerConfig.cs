using System.Web.Http;
using WebActivatorEx;
using Checkout.BasketManagement.Service;
using Swashbuckle.Application;
using System;

//[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Checkout.BasketManagement.Service
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "API to manage basket items");
                    c.IncludeXmlComments(GetXmlCommentsPath());
                })
                .EnableSwaggerUi(c =>
                {
                });
        }

        private static string GetXmlCommentsPath()
        {
            var srt = String.Format(@"{0}\bin\Checkout.BasketManagement.Service.XML", System.AppDomain.CurrentDomain.BaseDirectory);
            return srt;
        }
    }
}
