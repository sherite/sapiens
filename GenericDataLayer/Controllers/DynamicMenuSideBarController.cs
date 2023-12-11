using GenericDataLayer.Managers;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GenericDataLayer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DynamicMenuSideBarController : ApiController
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        readonly DynamicMenuSideBarManager dynamicMenuSideBarManager = new DynamicMenuSideBarManager(new MSSQLDataLayer.DynamicMenuSideBar());

        [HttpGet]
        [Route("api/v1/dynamicMenu")]
        public IHttpActionResult Post()
        {
            var lstMenus = new List<string>();
            try
            {
                lstMenus = this.dynamicMenuSideBarManager.Select();
            }
            catch (Exception e)
            {
                Logger.Error("BankAccountOverview.Get", e.StackTrace);

                return this.InternalServerError(e);
            }

            return this.Ok(lstMenus);
        }


    }
}