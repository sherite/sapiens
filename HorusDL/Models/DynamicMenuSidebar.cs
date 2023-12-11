namespace HorusDL
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    /// <summary>
    /// DynamicMenuSideBar data layer
    /// </summary>
    public class DynamicMenuSideBar
    {
        public IConfigurationRoot Configuration { get; set; }
        private readonly BaseDatos db;

        public DynamicMenuSideBar()
        {
           var connstring = "Server=tcp: 19.168.0.6,1433;Database=HORUS_PARAM;Integrated Security=true;";
           var options = new DbContextOptionsBuilder<BaseDatos>().UseSqlServer(connstring).Options;

           db = new BaseDatos(options);
        }

        /// <summary>
        /// select method
        /// </summary>CON
        public List<vwDynamicMenuSideBar> Select()
        {
            var result = new List<vwDynamicMenuSideBar>();

            try
            {
                result = db.vwDynamicMenuSideBar.ToList();
            }
            catch (System.Exception)
            {
                throw;
            }

            return result;
        }
    }
}