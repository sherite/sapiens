namespace SPS_SDL_SQL
{
    using System.Collections.Generic;
    using Entities;
    using static Entities.Enums;
    using Entities.Helpers;
    
    /// <summary>
    ///  Capa de datos de Grupos
    /// </summary>
    public class Permisos
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Permisos()
        { }

        /// <summary>
        ///  Lista Grupos filtrados
        /// </summary>
        /// <returns>Lista fltrada</returns>
        public List<Permiso> Select(object paginado, object orden, ulong? idPermiso)
        {
            var miPaginado = (Paging)paginado;

            var retVal = new List<Permiso>();

            try
            {
                if (General.Connect())
                {
                    var cmd = new System.Data.OleDb.OleDbCommand()
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        CommandText = "Permisos_Select",
                    };

                    var param1 = new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.Int64,
                        Value = idPermiso,
                        ParameterName = "@idPermiso",
                        IsNullable = true
                    };

                    cmd.Parameters.Add(param1);

                    cmd.Connection = General.Connection;

                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Permiso Permiso = null;

                        while (reader.Read())
                        {
                            Permiso = new Permiso
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                ID_Estado = (RightStatus)int.Parse(reader["ID_Estado"].ToString())
                            };

                            retVal.Add(Permiso);
                        }
                    }

                    reader.Close();

                    General.Connection.Close();
                    General.Connection = null;
                }
            }
            catch
            {

                throw;
            }

            return retVal;
        }

        /// <summary>
        ///  Busca un Grupo especifico
        /// </summary>
        /// <param name="id">Grupo a buscar</param>
        /// <returns>lista con el Grupo si lo encontro, sino vacia</returns>
        public List<Permiso> Find(int id)
        {
            var retVal = new List<Permiso>();

            if (General.Connect())
            {
                General.Connection.Close();
            }

            return retVal;
        }

        /// <summary>
        /// Actualiza un Grupo
        /// </summary>
        /// <param name="base64">encoding de entrada</param>
        /// <returns>Grupo actualizado</returns>
        public Permiso Update(string base64)
        {
            var param = System.Convert.FromBase64String(base64);
            var json2bytes = System.Text.Encoding.UTF8.GetString(param);
            var Permiso = Newtonsoft.Json.JsonConvert.DeserializeObject<Permiso>(json2bytes);

            var retVal = new Permiso();

            if (General.Connect())
            {
                var cmd = new System.Data.OleDb.OleDbCommand()
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                    CommandText = "Permisos_Update",
                };

                cmd.Parameters.Add(
                    new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.Int32,
                        Value = Permiso.ID,
                        ParameterName = "@ID",
                        IsNullable = false

                    });

                cmd.Parameters.Add(
                    new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.String,
                        Value = Permiso.Nombre,
                        ParameterName = "@Nombre",
                        IsNullable = false

                    });

                cmd.Parameters.Add(
                    new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.String,
                        Value = Permiso.Descripcion,
                        ParameterName = "@Descripcion",
                        IsNullable = false

                    });

                cmd.Parameters.Add(
                    new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.Int32,
                        Value = (int)Permiso.ID_Estado,
                        ParameterName = "@Estado",
                        IsNullable = true

                    });


                cmd.Connection = General.Connection;

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    Permiso miPermiso;

                    while (reader.Read())
                    {
                        miPermiso = new Permiso
                        {
                            ID = int.Parse(reader["ID"].ToString()),
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            ID_Estado = (RightStatus)int.Parse(reader["ID_Estado"].ToString())
                        };

                        retVal = miPermiso;
                    }
                }

                reader.Close();


                General.Connection.Close();
            }

            return retVal;
        }

        /// <summary>
        /// Borra un Grupo especifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns>resultado del borrado</returns>
        public RightStatus Delete(int id)
        {
            var retval = RightStatus.NotFound;

            if (General.Connect())
            {
                var cmd = new System.Data.OleDb.OleDbCommand()
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                    CommandText = "Permisos_Delete",
                };

                cmd.Parameters.Add(
                    new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.Int32,
                        Value = id,
                        ParameterName = "@idPermiso",
                        IsNullable = false

                    });

                cmd.Connection = General.Connection;

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                    }
                }

                reader.Close();

                General.Connection.Close();
                General.Connection = null;
            }

            return retval;

        }

        /// <summary>
        /// Inserta un Grupo
        /// </summary>
        /// <param name="base64">datos a insertar</param>
        /// <returns>Grupo insertado</returns>
        public Permiso Insert(string base64)
        {
            var retVal = new Permiso();

            var param = System.Convert.FromBase64String(base64);
            var json2bytes = System.Text.Encoding.UTF8.GetString(param);
            var Permiso = Newtonsoft.Json.JsonConvert.DeserializeObject<Permiso>(json2bytes);

            if (General.Connect())
            {
                var cmd = new System.Data.OleDb.OleDbCommand()
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                    CommandText = "Permisos_Insert",
                };

                var param1 = new System.Data.OleDb.OleDbParameter()
                {
                    DbType = System.Data.DbType.String,
                    Value = Permiso.Nombre,
                    ParameterName = "@nombre",
                    IsNullable = false

                };

                cmd.Parameters.Add(param1);

                param1 = new System.Data.OleDb.OleDbParameter()
                {
                    DbType = System.Data.DbType.String,
                    Value = Permiso.Descripcion,
                    ParameterName = "@descripcion",
                    IsNullable = false

                };

                cmd.Parameters.Add(param1);

                cmd.Connection = General.Connection;

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    Permiso miPermiso;

                    while (reader.Read())
                    {
                        miPermiso = new Permiso
                        {
                            ID = int.Parse(reader["ID"].ToString()),
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            ID_Estado = (RightStatus)int.Parse(reader["ID_Estado"].ToString())
                        };

                        retVal = miPermiso;
                    }
                }

                reader.Close();


                General.Connection.Close();
                General.Connection = null;
            }

            return retVal;
        }
    }
}