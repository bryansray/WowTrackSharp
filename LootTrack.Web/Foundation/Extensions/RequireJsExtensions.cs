using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LootTrack.Web.Foundation.Extensions
{
    public static class HtmlHelperExtensions
    {
        private static readonly JsonSerializerSettings Settings;

        static HtmlHelperExtensions()
        {
            Settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
                ,ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                ,PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
        }

        public static MvcHtmlString ToJson(this HtmlHelper html, object value)
        {
            return MvcHtmlString.Create(JsonConvert.SerializeObject(value, Formatting.None, Settings));
        }
    }

    public static class RequireJsExtensions
    {
        public static MvcHtmlString RenderViewSpecificRequireJs(this HtmlHelper htmlHelper, string baseUrl)
        {
            var require = new StringBuilder();

            var root = HttpContext.Current.Server.MapPath("~/Scripts/App");
            var controller = htmlHelper.ViewContext.RouteData.Values["Controller"].ToString();
            var action = htmlHelper.ViewContext.RouteData.Values["Action"].ToString();

            var conventionalPath = string.Format("{0}/{1}/index", controller, action);
            var fullPath = Path.Combine(root, controller, action, "index.js");

            require.AppendLine(@"<script>");
            require.AppendLine(@"require(['/Scripts/main.js'], function (main) {");
            require.AppendLine(@"require(['application'], function(application) {");
            require.AppendLine(@"application.initialize();");

            if (File.Exists(fullPath))
                require.AppendLine(String.Format("require([\"{0}/{1}/index\"]);", controller, action));

            require.AppendLine(@"});");
            
            require.AppendLine(@"});");
            require.AppendLine(@"</script>");

            return new MvcHtmlString(require.ToString());
        }
    }
}