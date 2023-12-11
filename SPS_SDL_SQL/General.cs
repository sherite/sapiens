// Sherite Professional Software - Specific Data Layer for Microsoft SQL Server Database
namespace SPS_SDL_SQL
{
    using System.Configuration;
    using System.Data.OleDb;

    /// <summary>
    /// clase de metodos generales
    /// </summary>
    public static class General
    {
        /// <summary>
        /// cadena de conexion
        /// </summary>
        public static string ConnStr { get { return ConfigurationManager.AppSettings.Get("connStr"); } }

        /// <summary>
        /// Conexion al SQL Server
        /// </summary>
        public static OleDbConnection Connection { get; set; }

        /// <summary>
        /// Conecta a una base de datos
        /// </summary>
        /// <returns>Estado de la conexion</returns>
        public static bool Connect()
        {
            var retVal = false;

            var connString = ConnStr;

            DesConecta();

            if (!string.IsNullOrEmpty(connString))
            {
                Connection = new OleDbConnection()
                {
                    ConnectionString = connString
                };
                try
                {
                    Connection.Open();

                    retVal = Connection.State == System.Data.ConnectionState.Open;

                }
                catch
                {
                    throw;
                }
            }

            return retVal;
        }

        /// <summary>
        /// Desconecta el SQL Server
        /// </summary>
        public static void DesConecta()
        {
            if (Connection != null && Connection.State != System.Data.ConnectionState.Closed)
            {
                Connection.Close();
                Connection = null;
            }
        }


        /// <summary>
        /// cadena de conexion
        /// </summary>
        public static string ConnStrGTF { get { return ConfigurationManager.AppSettings.Get("connStrGTF"); } }

        /// <summary>
        /// Conexion al SQL Server
        /// </summary>
        public static OleDbConnection ConnectionGTF { get; set; }
        /// <summary>
        /// Conecta a una base de datos
        /// </summary>
        /// <returns>Estado de la conexion</returns>
        public static bool ConnectGTF()
        {
            var retVal = false;

            var connString = ConnStrGTF;

            DesConectaGTF();

            if (!string.IsNullOrEmpty(connString))
            {
                ConnectionGTF = new OleDbConnection()
                {
                    ConnectionString = connString
                };
                try
                {
                    ConnectionGTF.Open();

                    retVal = ConnectionGTF.State == System.Data.ConnectionState.Open;

                }
                catch
                {
                    throw;
                }
            }

            return retVal;
        }

        /// <summary>
        /// Desconecta el SQL Server
        /// </summary>
        public static void DesConectaGTF()
        {
            if (ConnectionGTF != null && ConnectionGTF.State != System.Data.ConnectionState.Closed)
            {
                ConnectionGTF.Close();
                ConnectionGTF = null;
            }
        }
    }
}