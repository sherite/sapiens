namespace Sapiens.FrontOffice
{
    using System.Web.Mvc;

    /// <summary>
    /// Global Asax
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MvcApplication"/> class.
        /// </summary>
        public MvcApplication()
        {
        }

        /// <summary>
        ///  Inicio de la app
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
        }
    }
}
