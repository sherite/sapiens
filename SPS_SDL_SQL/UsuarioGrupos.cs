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
    public class UserGroups
    {
        /// <summary>
        /// 
        /// </summary>
        public UserGroups() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Group>> Select(int userId)
        {
            var retVal = new List<Group>();

            try
            {
                if (General.Connect())
                {
                    var cmd = new System.Data.OleDb.OleDbCommand()
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        CommandText = "Users_Groups_Select",
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
                            var group = new Group
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString(),
                                Status = (GroupStatus)int.Parse(reader["Status"].ToString())
                            };

                            retVal.Add(group);
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
        /// <param name="groupId"></param>
        /// <returns></returns>
        public IList<UsuarioGetRequestDTO> SelectUsers(int groupId)
        {
            var retVal = new List<UsuarioGetRequestDTO>();

            try
            {
                if (General.Connect())
                {
                    var cmd = new System.Data.OleDb.OleDbCommand()
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        CommandText = "Groups_Users_Select",
                    };

                    var cmdParam = new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.Int64,
                        Value = groupId,
                        ParameterName = "@GroupId",
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
        public async Task<List<Group>> Find(int id)
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
        /// <param name="idGrupo"></param>
        public void DeleteUsers(int? idGrupo)
        {
            if (General.Connect())
            {
                var cmd = new System.Data.OleDb.OleDbCommand()
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                    CommandText = "Groups_Users_Delete",
                };

                cmd.Parameters.Add(
                    new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.Int32,
                        Value = idGrupo,
                        ParameterName = "@groupId",
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
                    CommandText = "Users_Groups_Delete",
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
        /// <param name="groupId"></param>
        /// <param name="cmd"></param>
        public void InsertFromNewUser(int userId, int groupId, System.Data.OleDb.OleDbCommand cmd)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Users_Groups_Insert";

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
                Value = groupId,
                ParameterName = "@groupId",
                IsNullable = false
            });

            var  reader = cmd.ExecuteReader();

            reader.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        public void Insert(int userId, int groupId)
        {
            if (General.Connect())
            {
                var cmd = new System.Data.OleDb.OleDbCommand()
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                    CommandText = "Users_Groups_Insert",
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
                    Value = groupId,
                    ParameterName = "@groupId",
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