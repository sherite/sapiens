namespace SPS_SDL_SQL
{
    using System.Collections.Generic;
    using Entities;
    using Entities.Helpers;

    using static Entities.Enums;

    /// <summary>
    /// 
    /// </summary>
    public class Groups
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="ordering"></param>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Group> Select(Entities.Helpers.Paging paging, Ordering ordering, int? groupId, int? userId)
        {
            var retVal = new List<Group>();

            try
            {
                if (General.Connect())
                {
                    var cmd = new System.Data.OleDb.OleDbCommand()
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        CommandText = "Groups_Select",
                    };

                    if (groupId.HasValue)
                    {
                        cmd.Parameters.Add(new System.Data.OleDb.OleDbParameter()
                        {
                            DbType = System.Data.DbType.Int64,
                            Value = groupId,
                            ParameterName = "@groupId",
                            IsNullable = true
                        });
                    }

                    if (userId.HasValue)
                    {
                        cmd.Parameters.Add( new System.Data.OleDb.OleDbParameter()
                        {
                            DbType = System.Data.DbType.Int64,
                            Value = groupId,
                            ParameterName = "@userId",
                            IsNullable = true
                        });
                    }

                    cmd.Connection = General.Connection;

                    var reader = cmd.ExecuteReader();

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
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Group> Find(int id)
        {
            return Select(null, null, id, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public Group Update(string base64)
       {
            var param = System.Convert.FromBase64String(base64);
            var json2bytes = System.Text.Encoding.UTF8.GetString(param);
            var Group = Newtonsoft.Json.JsonConvert.DeserializeObject<Group>(json2bytes);

            var retVal = new Group();

            if (General.Connect())
            {
                var cmd = new System.Data.OleDb.OleDbCommand()
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                    CommandText = "Grupos_Update",
                };

                cmd.Parameters.Add(
                    new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.Int32,
                        Value = Group.ID,
                        ParameterName = "@ID",
                        IsNullable = false

                    });

                cmd.Parameters.Add(
                    new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.String,
                        Value = Group.Name,
                        ParameterName = "@Name",
                        IsNullable = false

                    });

                cmd.Parameters.Add(
                    new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.String,
                        Value = Group.Description,
                        ParameterName = "@Description",
                        IsNullable = false

                    });

                cmd.Parameters.Add(
                    new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.Int32,
                        Value = (int)Group.Status,
                        ParameterName = "@Status",
                        IsNullable = true

                    });

                cmd.Connection = General.Connection;

                var reader = cmd.ExecuteReader();

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

                        retVal = group;
                    }
                }

                reader.Close();

                General.Connection.Close();
            }

            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GroupStatus Delete(int id)
        {
            var retval = GroupStatus.NotFound;

            if (General.Connect())
            {
                var cmd = new System.Data.OleDb.OleDbCommand()
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                    CommandText = "Grupos_Delete",
                };

                cmd.Parameters.Add(
                    new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.Int32,
                        Value = id,
                        ParameterName = "@groupId",
                        IsNullable = false

                    });

                cmd.Connection = General.Connection;

                var reader = cmd.ExecuteReader();

                reader.Close();

                General.Connection.Close();
                General.Connection = null;
            }

            return retval;
        }

        /// <summary>
        /// Insert a new Group
        /// </summary>
        /// <param name="base64">group data in base64</param>
        /// <returns></returns>
        public Group Insert(string base64)
        {
            var retVal = new Group();
 
            var param = System.Convert.FromBase64String(base64);
            var json2bytes = System.Text.Encoding.UTF8.GetString(param);
            var Group = Newtonsoft.Json.JsonConvert.DeserializeObject<Group>(json2bytes);

            if (General.Connect())
            {
                var cmd = new System.Data.OleDb.OleDbCommand()
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                    CommandText = "Grupos_Insert",
                };

                cmd.Parameters.Add(
                    new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.String,
                        Value = Group.Name,
                        ParameterName = "@Name",
                        IsNullable = false
                    });

                cmd.Parameters.Add(
                    new System.Data.OleDb.OleDbParameter()
                    {
                        DbType = System.Data.DbType.String,
                        Value = Group.Description,
                        ParameterName = "@Description",
                        IsNullable = false

                    });

                cmd.Connection = General.Connection;

                var transaction = cmd.Connection.BeginTransaction();

                cmd.Transaction = transaction;

                try
                {
                    var reader = cmd.ExecuteReader();

                    var group = new Group();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            group = new Group
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString(),
                                Status = (GroupStatus)int.Parse(reader["Status"].ToString())
                            };

                            retVal = group;
                        }
                    }

                    reader.Close();

                    foreach (var user in Group.Users)
                    {
                        new SPS_SDL_SQL.UserGroups().InsertFromNewUser(user.ID, group.ID.Value, cmd);
                    }

                    transaction.Commit();

                }
                catch (System.Exception)
                {
                    transaction.Rollback();

                    throw;
                }

                General.Connection.Close();
                General.Connection = null;
            }

            return retVal;
        }
    }
}