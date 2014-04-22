using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LootTrack.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; 
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("CharacterLootsApi", routeTemplate: "api/characters/{characterId}/{controller}/{id}", defaults: new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "api/{controller}/{id}", defaults: new { id = RouteParameter.Optional });
        }
    }
}
