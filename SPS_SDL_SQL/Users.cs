namespace SPS_SDL_SQL
{
    using System;
    using System.Collections.Generic;
    using Entities;
    using static Entities.Enums;
    using Newtonsoft.Json;
    using System.Data.OleDb;
    using System.Threading.Tasks;

    /// <summary>
    ///  User's data layer.
    /// </summary>
    public class Users
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Users()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Login(string user, string password)
        {
            var retVal = false;

            if (General.Connect())
            {
                var cmd = new System.Data.OleDb.OleDbCommand
                {
                    CommandType = System.Data.CommandType.Text,
                    CommandText = "SELECT COUNT(*) FROM USERS WHERE ALIAS='" + user + "' AND PASSWORD = '" + password + "';",
                    Connection = General.Connection
                };

                var result = int.Parse(cmd.ExecuteScalar().ToString());

                retVal = result > 0;
                
                General.Connection.Close();
                General.Connection = null;
            }

            return retVal;

        }

        /// <summary>
        ///  Filtered User's list.
        /// </summary>
        /// <returns>filtered list</returns>
        public async Task<IList<User>> Select(object paging, object ordering, ulong? userID)
        {
            var retVal = new List<User>();

            try
            {
                if (General.Connect())
                {
                    var cmd = new System.Data.OleDb.OleDbCommand()
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        CommandText = "Users_Select",
                    };

                    var param = new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.Int64,
                        Value = userID,
                        ParameterName = "@userID",
                        IsNullable = true
                    };

                    cmd.Parameters.Add(param);

                    cmd.Connection = General.Connection;

                    var reader = await cmd.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var user = new User()
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                Alias = reader["Alias"].ToString(),
                                Name = reader["Name"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Status = (UserStatus)int.Parse(reader["Status"].ToString())
                            };

                            retVal.Add(user);
                        }
                    }

                    reader.Close();

                    General.Connection.Close();
                    General.Connection = null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return retVal;
        }

        /// <summary>
        ///  Find a specifc user.
        /// </summary>
        /// <param name="id">User to find</param>
        /// <returns>a List</returns>
        public async Task<IList<User>> Find(int? id)
        {
            Contract.Requires<Exception>(id != null, "User ID can not to be empty.");

            return await Select(null, null, (ulong?)id);
        }

        /// <summary>
        /// Update an user.
        /// </summary>
        /// <param name="base64">input encoding</param>
        /// <returns>user updated</returns>
        public User Update(string base64)
        {
            Contract.Requires<Exception>(base64 != null, "Encoding can not to be empty.");

            var param = System.Convert.FromBase64String(base64);
            var json2bytes = System.Text.Encoding.UTF8.GetString(param);
            var User = JsonConvert.DeserializeObject<User>(json2bytes);

            var retVal = new User();

            try
            {
                if (General.Connect())
                {
                    var cmd = new System.Data.OleDb.OleDbCommand()
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        CommandText = "Users_Update",
                    };

                    var cmdParam = new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.Int32,
                        Value = User.ID,
                        ParameterName = "@ID",
                        IsNullable = false

                    };
                    cmd.Parameters.Add(cmdParam);

                    cmdParam = new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.String,
                        Value = User.Alias,
                        ParameterName = "@Alias",
                        IsNullable = false

                    };
                    cmd.Parameters.Add(cmdParam);

                    cmdParam = new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.String,
                        Value = User.Name,
                        ParameterName = "@Name",
                        IsNullable = false

                    };
                    cmd.Parameters.Add(cmdParam);

                    cmdParam = new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.String,
                        Value = User.LastName,
                        ParameterName = "@LastName",
                        IsNullable = false

                    };
                    cmd.Parameters.Add(cmdParam);

                    cmdParam = new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.Int32,
                        Value = (int)User.Status,
                        ParameterName = "@Status",
                        IsNullable = true

                    };
                    cmd.Parameters.Add(cmdParam);

                    cmd.Connection = General.Connection;

                    try
                    {
                        var reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var user = new User
                                {
                                    ID = int.Parse(reader["ID"].ToString()),
                                    Alias = reader["Alias"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Status = (UserStatus)int.Parse(reader["Status"].ToString())
                                };

                                retVal = user;
                            }
                        }

                        reader.Close();
                    }
                    catch
                    {
                        throw;
                    }


                    General.Connection.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return retVal;
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>result</returns>
        public UserStatus Delete(int? id)
        {
            Contract.Requires<Exception>(id != null, "User ID can not to be empty.");

            var retval = UserStatus.NotFound;

            try
            {
                if (General.Connect())
                {
                    var cmd = new System.Data.OleDb.OleDbCommand()
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        CommandText = "Users_Delete",
                    };

                    cmd.Parameters.Add(
                        new System.Data.OleDb.OleDbParameter()
                        {
                            DbType = System.Data.DbType.Int32,
                            Value = id,
                            ParameterName = "@UserID",
                            IsNullable = false

                        });

                    cmd.Connection = General.Connection;

                    var reader = cmd.ExecuteReader();

                    reader.Close();

                    General.Connection.Close();
                    General.Connection = null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return retval;
        }

        /// <summary>
        /// Insert a user
        /// </summary>
        /// <param name="base64">data to insert</param>
        /// <returns>user inserted</returns>
        public User Insert(string base64)
        {
            var param = System.Convert.FromBase64String(base64);
            var json2bytes = System.Text.Encoding.UTF8.GetString(param);
            var User = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(json2bytes);

            Contract.Requires<Exception>(!string.IsNullOrEmpty( base64));

            var retVal = new User();

            try
            {
                if (General.Connect())
                {
                    var cmd = new System.Data.OleDb.OleDbCommand()
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        CommandText = "Users_Insert",
                    };

                    var cmdParam = new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.String,
                        Value = User.Alias,
                        ParameterName = "@alias",
                        IsNullable = false

                    };
                    cmd.Parameters.Add(cmdParam);

                    cmdParam = new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.String,
                        Value = User.Name,
                        ParameterName = "@name",
                        IsNullable = false

                    };
                    cmd.Parameters.Add(cmdParam);

                    cmdParam = new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.String,
                        Value = User.LastName,
                        ParameterName = "@lastname",
                        IsNullable = false

                    };
                    cmd.Parameters.Add(cmdParam);

                    cmd.Connection = General.Connection;

                    var transaction = cmd.Connection.BeginTransaction();

                    cmd.Transaction = transaction;

                    try
                    {
                        var reader = cmd.ExecuteReader();

                        var user = new User();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                user = new User
                                {
                                    ID = int.Parse(reader["ID"].ToString()),
                                    Alias = reader["Alias"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Status = (UserStatus)int.Parse(reader["Status"].ToString())
                                };

                                retVal = user;
                            }
                        }

                        reader.Close();

                        foreach (var grupo in User.Groups)
                        {
                            new SPS_SDL_SQL.UserGroups().InsertFromNewUser(retVal.ID, grupo.ID.Value, cmd);
                        }

                        transaction.Commit();

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();

                        throw e;
                    }

                    General.Connection.Close();
                    General.Connection = null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return retVal;
        }
    }
}