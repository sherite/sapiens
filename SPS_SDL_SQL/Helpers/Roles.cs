namespace SPS_SDL_SQL
{
    using System.Collections.Generic;
    using Entities;
    using static Entities.Enums;
    using Entities.Helpers;

    /// <summary>
    ///  Capa de datos de Grupos
    /// </summary>
    public class Roles
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Roles()
        { }

        /// <summary>
        ///  Lista Grupos filtrados
        /// </summary>
        /// <returns>Lista fltrada</returns>
        public List<Rol> Select(object paginado, object orden, ulong? idGrupo)
        {
            var miPaginado = (Paging)paginado;

            var retVal = new List<Rol>();

            try
            {
                if (General.Connect())
                {
                    var cmd = new System.Data.OleDb.OleDbCommand()
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        CommandText = "Roles_Select",
                    };

                    var param1 = new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.Int64,
                        Value = idGrupo,
                        ParameterName = "@idRol",
                        IsNullable = true
                    };

                    cmd.Parameters.Add(param1);

                    cmd.Connection = General.Connection;

                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Rol Rol = null;

                        while (reader.Read())
                        {
                            Rol = new Rol
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                ID_Estado = (RolStatus)int.Parse(reader["ID_Estado"].ToString())
                            };

                            retVal.Add(Rol);
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
        public List<Rol> Find(int id)
        {
            var retVal = new List<Rol>();

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
        public Rol Update(string base64)
        {
            var param = System.Convert.FromBase64String(base64);
            var json2bytes = System.Text.Encoding.UTF8.GetString(param);
            var Rol = Newtonsoft.Json.JsonConvert.DeserializeObject<Rol>(json2bytes);

            var retVal = new Rol();

            if (General.Connect())
            {
                var cmd = new System.Data.OleDb.OleDbCommand()
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                    CommandText = "Roles_Update",
                };

                cmd.Parameters.Add(
                    new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.Int32,
                        Value = Rol.ID,
                        ParameterName = "@ID",
                        IsNullable = false

                    });

                cmd.Parameters.Add(
                    new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.String,
                        Value = Rol.Nombre,
                        ParameterName = "@Nombre",
                        IsNullable = false

                    });

                cmd.Parameters.Add(
                    new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.String,
                        Value = Rol.Descripcion,
                        ParameterName = "@Descripcion",
                        IsNullable = false

                    });

                cmd.Parameters.Add(
                    new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.Int32,
                        Value = (int)Rol.ID_Estado,
                        ParameterName = "@Estado",
                        IsNullable = true

                    });


                cmd.Connection = General.Connection;

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var rol = new Rol
                        {
                            ID = int.Parse(reader["ID"].ToString()),
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            ID_Estado = (RolStatus)int.Parse(reader["ID_Estado"].ToString())
                        };

                        retVal = rol;
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
        public RolStatus Delete(int id)
        {
            var retval = RolStatus.NotFound;

            if (General.Connect())
            {
                var cmd = new System.Data.OleDb.OleDbCommand()
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                    CommandText = "Roles_Delete",
                };

                cmd.Parameters.Add(
                    new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.Int32,
                        Value = id,
                        ParameterName = "@idRol",
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
        /// Inserta un Rol
        /// </summary>
        /// <param name="base64">datos a insertar</param>
        /// <returns>Grupo insertado</returns>
        public Rol Insert(string base64)
        {
            var retVal = new Rol();

            var param = System.Convert.FromBase64String(base64);
            var json2bytes = System.Text.Encoding.UTF8.GetString(param);
            var Rol = Newtonsoft.Json.JsonConvert.DeserializeObject<Rol>(json2bytes);

            if (General.Connect())
            {
                var cmd = new System.Data.OleDb.OleDbCommand()
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                    CommandText = "Roles_Insert",
                };

                var param1 = new System.Data.OleDb.OleDbParameter()
                {
                    DbType = System.Data.DbType.String,
                    Value = Rol.Nombre,
                    ParameterName = "@nombre",
                    IsNullable = false

                };

                cmd.Parameters.Add(param1);

                param1 = new System.Data.OleDb.OleDbParameter()
                {
                    DbType = System.Data.DbType.String,
                    Value = Rol.Descripcion,
                    ParameterName = "@descripcion",
                    IsNullable = false

                };

                cmd.Parameters.Add(param1);

                cmd.Connection = General.Connection;

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    Rol miRol = null;

                    while (reader.Read())
                    {
                        miRol = new Rol
                        {
                            ID = int.Parse(reader["ID"].ToString()),
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            ID_Estado = (RolStatus)int.Parse(reader["ID_Estado"].ToString())
                        };

                        retVal = miRol;
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