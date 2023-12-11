namespace GenericDataLayer.Configuration
{
    using System.Configuration;

    /// <summary>
    /// Manager de claves del web.config
    /// </summary>
    public class ConfigurationHandler
    {
        /// <summary>
        /// retorna la entidad
        /// </summary>
        /// <returns>entidad sobre la que se esta operando</returns>
        public static string idEntity()
        {
            return ConfigurationManager.AppSettings["ID_ENTIDAD"].ToUpper();
        }

        /// <summary>
        /// retorna el motor de base de datos utilizado
        /// </summary>
        /// <returns>nombre del motor</returns>
        public static string EnginePath()
        {
            return ConfigurationManager.AppSettings["ENGINE_PATH"].ToUpper();
        }

        /// <summary>
        /// retorna el motor de base de datos utilizado
        /// </summary>
        /// <returns>nombre del motor</returns>
        public static string EngineMSSQL()
        {
            return ConfigurationManager.AppSettings["ENGINE_MSSQLDATALAYER"].ToUpper();
        }
    }
}