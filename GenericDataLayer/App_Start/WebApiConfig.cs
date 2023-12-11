using System.Web.Http;

namespace GenericDataLayer
{
    /// <summary>
    /// Configuracion de Webapi
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registro de WebAPI
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "GenericDataLayerApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new {controller = "users", id = RouteParameter.Optional }
            );
        }
    }
}
