using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PyramidPlaningSystem
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            //nyaste
            config.Routes.MapHttpRoute("CustomApi", "Api/{controller}/{action}/{id}", 
             new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute("DefaultApiWithId", "Api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional }, new { id = @"\d+" });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
