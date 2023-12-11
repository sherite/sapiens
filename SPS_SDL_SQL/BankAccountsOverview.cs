//namespace SPS_SDL_SQL
//{
//    using System;
//    using System.Collections.Generic;
//    using Entities;
//    using static Entities.Enums;
//    using Newtonsoft.Json;
//    using System.Data.OleDb;

//    /// <summary>
//    /// Bank Account Overview data layer
//    /// </summary>
//    public class BankAccountsOverview
//    {
//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public BankAccountsOverview()
//        { }

//        /// <summary>
//        /// filtered bankAccountOverview list
//        /// </summary>
//        /// <param name="paging">paging object</param>
//        /// <param name="ordering">ordering object</param>
//        /// <param name="idBankAccountOverview">id</param>
//        /// <returns>list</returns>
//        public IList<BankAccountOverview> Select(object paging, object ordering, ulong? idBankAccountOverview)
//        {
//            var retVal = new List<User>();

//            try
//            {
//                if (General.Connect())
//                {
//                    var query = from BankAccountOverview
//                                select idBankAccountOverview;

//                    var cmd = new System.Data.OleDb.OleDbCommand()
//                    {
//                        CommandType = System.Data.CommandType.StoredProcedure,
//                        CommandText = "Users_Select",
//                    };

//                    var param = new System.Data.OleDb.OleDbParameter()
//                    {
//                        DbType = System.Data.DbType.Int64,
//                        Value = userID,
//                        ParameterName = "@userID",
//                        IsNullable = true
//                    };

//                    cmd.Parameters.Add(param);

//                    cmd.Connection = General.Connection;

//                    var reader = cmd.ExecuteReader();

//                    if (reader.HasRows)
//                    {
//                        while (reader.Read())
//                        {
//                            var user = new User()
//                            {
//                                ID = int.Parse(reader["ID"].ToString()),
//                                Alias = reader["Alias"].ToString(),
//                                Name = reader["Name"].ToString(),
//                                LastName = reader["LastName"].ToString(),
//                                Status = (UserStatus)int.Parse(reader["Status"].ToString())
//                            };

//                            retVal.Add(user);
//                        }
//                    }

//                    reader.Close();

//                    General.Connection.Close();
//                    General.Connection = null;
//                }
//            }
//            catch (Exception e)
//            {
//                throw e;
//            }

//            return retVal;
//        }


//    }
//}
