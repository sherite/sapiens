using Entities;

using NLog;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace Entities.Controllers
{
    [EnableCors("*")]
    public class DynamicMenuSideBarController : ControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        HorusDL.DynamicMenuSideBar dynamicMenuSideBarManager = new HorusDL.DynamicMenuSideBar();

        [HttpPost]
        [Route("api/v1/dynamicMenu")]
        public IActionResult Post()
        {
            var lstMenus = new List<HorusDL.Models.vwDynamicMenuSideBar>();
            try
            {
                lstMenus = this.dynamicMenuSideBarManager.Select();
            }
            catch (Exception e)
            {
                Logger.Error("BankAccountOverview.Get", e.StackTrace);

                return StatusCode(500, e);
            }

            return Ok(lstMenus);
        }
    }
}