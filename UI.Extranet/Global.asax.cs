using System.Web.Mvc;
using System.Web.Routing;

namespace UI.Extranet
{
    /// <summary>
    /// Global asax
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Inicio de la aplicacion
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
