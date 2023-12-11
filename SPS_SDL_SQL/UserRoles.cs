namespace SPS_SDL_SQL
{
    using Entities;
    using Entities.DTOs;

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using static Entities.Enums;

    /// <summary>
    /// 
    /// </summary>
    public class UserRoles
    {
        /// <summary>
        /// 
        /// </summary>
        public UserRoles() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Rol>> Select(int userId)
        {
            var retVal = new List<Rol>();

            try
            {
                if(General.Connect())
                {
                    var cmd = new System.Data.OleDb.OleDbCommand()
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        CommandText = "Users_Roles_Select",
                    };

                    var cmdParam = new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.Int64,
                        Value = userId,
                        ParameterName = "@UserId",
                        IsNullable = true
                    };

                    cmd.Parameters.Add(cmdParam);

                    cmd.Connection = General.Connection;

                    var reader = await cmd.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var rol = new Rol
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                ID_Estado = (RolStatus)int.Parse(reader["ID_ESTADO"].ToString())
                            };

                            retVal.Add(rol);
                        }
                    }

                    reader.Close();

                    General.Connection.Close();
                    General.Connection = null;
                }

            }
            catch (Exception)
            {

                throw;
            }

            return retVal;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<UsuarioGetRequestDTO> SelectUsers(int roleId)
        {
            var retVal = new List<UsuarioGetRequestDTO>();

            try
            {
                if (General.Connect())
                {
                    var cmd = new System.Data.OleDb.OleDbCommand()
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        CommandText = "Roles_Users_Select",
                    };

                    var cmdParam = new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.Int64,
                        Value = roleId,
                        ParameterName = "@RoleId",
                        IsNullable = true
                    };

                    cmd.Parameters.Add(cmdParam);

                    cmd.Connection = General.Connection;

                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var user = new UsuarioGetRequestDTO()
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                Name = reader["Name"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Alias = reader["Alias"].ToString(),
                                Status = (UserStatus)(Convert.ToInt32((reader["Status"].ToString())))
                            };

                            retVal.Add(user);
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Rol>> Find(int id)
        {
            return await Select(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public int Update(string base64)
        {
            var retVal = 0;

            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idRol"></param>
        public void DeleteUsers(int? idRol)
        {
            if (General.Connect())
            {
                var cmd = new System.Data.OleDb.OleDbCommand()
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                    CommandText = "Roles_Users_Delete",
                };

                cmd.Parameters.Add(
                    new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.Int32,
                        Value = idRol,
                        ParameterName = "@rolId",
                        IsNullable = true

                    });

                cmd.Connection = General.Connection;

                cmd.ExecuteNonQuery();

                General.Connection.Close();
                General.Connection = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUsuario"></param>
        public void Delete(int? idUsuario)
        {
            if (General.Connect())
            {
                var cmd = new System.Data.OleDb.OleDbCommand()
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                    CommandText = "Users_Roles_Delete",
                };

                cmd.Parameters.Add(
                    new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.Int32,
                        Value = idUsuario.Value,
                        ParameterName = "@userId",
                        IsNullable = true

                    });

                cmd.Connection = General.Connection;

                cmd.ExecuteNonQuery();

                General.Connection.Close();
                General.Connection = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <param name="cmd"></param>
        public void InsertFromNewUser(int userId, int roleId, System.Data.OleDb.OleDbCommand cmd)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Users_Roles_Insert";

            cmd.Parameters.Clear();

            cmd.Parameters.Add(new System.Data.OleDb.OleDbParameter()
            {
                DbType = System.Data.DbType.Int32,
                Value = userId,
                ParameterName = "@userId",
                IsNullable = false
            });

            cmd.Parameters.Add(new System.Data.OleDb.OleDbParameter()
            {
                DbType = System.Data.DbType.Int32,
                Value = roleId,
                ParameterName = "@roleId",
                IsNullable = false
            });

            var reader = cmd.ExecuteReader();

            reader.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        public void Insert(int userId, int roleId)
        {
            if (General.Connect())
            {
                var cmd = new System.Data.OleDb.OleDbCommand()
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                    CommandText = "Users_Roles_Insert",
                };

                var param = new System.Data.OleDb.OleDbParameter()
                {
                    DbType = System.Data.DbType.Int32,
                    Value = userId,
                    ParameterName = "@userId",
                    IsNullable = false
                };

                cmd.Parameters.Add(param);

                param = new System.Data.OleDb.OleDbParameter()
                {
                    DbType = System.Data.DbType.Int32,
                    Value = roleId,
                    ParameterName = "@roleId",
                    IsNullable = false
                };

                cmd.Parameters.Add(param);

                cmd.Connection = General.Connection;

                cmd.ExecuteNonQuery();

                General.Connection.Close();
                General.Connection = null;
            }
        }
    }
}