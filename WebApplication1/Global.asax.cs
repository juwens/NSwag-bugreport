using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NSwag.AspNet.Owin;

namespace WebApplication1
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Newtonsoft.Json.JsonConvert.DefaultSettings = () => new Newtonsoft.Json.JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects,
                ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver()
                //ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            RouteTable.Routes.MapOwinPath("swagger", app =>
            {
                app.UseSwaggerUi3(typeof(WebApiApplication).Assembly, settings =>
                {
                    settings.GeneratorSettings.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings()
                    {
                        Formatting = Newtonsoft.Json.Formatting.Indented,
                        TypeNameHandling = Newtonsoft.Json.TypeNameHandling.None, // <- this is ignored, the JsonConvert.DefaultSettings is used
                        ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver()
                    };

                    settings.MiddlewareBasePath = "/swagger";
                    settings.GeneratorSettings.DefaultUrlTemplate = "api/{controller}/{id}";  //this is the default one
                    //settings.GeneratorSettings.DefaultUrlTemplate = "api/{controller}/{action}/{id}";
                });
            });

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
