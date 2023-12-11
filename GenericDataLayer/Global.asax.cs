using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;

namespace GenericDataLayer
{
    /// <summary>
    /// Global asax
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Inicio de la aplicacion
        /// </summary>
        protected void Application_Start()
        {
            var builder = Register.RegisterTypes();
            builder.RegisterControllers(typeof(GenericDataLayer.WebApiApplication).Assembly).InstancePerRequest();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}