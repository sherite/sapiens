using System.Data.Entity;

namespace MSSQLDataLayer.Models
{
    /// <summary>
    /// contexto
    /// </summary>
    public class BaseDatos : DbContext // debe heredar de la clase DbContext
    {
        /// <summary>
        /// constructor
        /// </summary>
        public BaseDatos() : base(Properties.Settings.Default.connStr) //<- Pasar la connectionString
        {
        }

        /// <summary>
        /// bancos
        /// </summary>
        public virtual DbSet<Entities.DTOs.BankAccountOverview> BankAccountsOverview { get; set; }

        /// <summary>
        /// usuarios
        /// </summary>
        public virtual DbSet<Entities.DTOs.UserInfoOverview> UserInfoOverview { get; set; }

        /// <summary>
        /// Dynamic menu sidebar
        /// </summary>
        public virtual DbSet<Entities.DTOs.DynamicMenuSideBar> DynamicMenuSideBar { get; set; }

        /// <summary>
        /// menu side bar
        /// </summary>
        public virtual DbSet<Entities.DTOs.MENUS> Menus { get; set; }

        /// <summary>
        /// textos
        /// </summary>
        public virtual DbSet<Entities.DTOs.Menus_Texto> MenusTexto { get; set; }
    }

    /// <summary>
    /// aaa
    /// </summary>
    public class VWDynamicMenuSideBar
    {
        /// <summary>
        /// 
        /// </summary>
        public int? ID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int? Modulo { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int? Padre { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int? Orden { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int? Tipo { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Accion { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Texto { get; set; }
    }


}