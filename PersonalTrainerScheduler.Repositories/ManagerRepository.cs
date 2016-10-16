using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalTrainerScheduler.Entities;
using System.Data.SqlClient;

namespace PersonalTrainerScheduler.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
       
        #region Fields

        private string _connectionString;

        private string spGetManagerByLogin = "spGetManagerByLogin";

        #endregion

        #region Constructor
        public ManagerRepository(string connString)
        {
            this._connectionString = connString;
        }

        #endregion

        #region Methods
        public Manager GetManagerByLogin(string login, string password)
        {
            Manager manager = null;

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(spGetManagerByLogin, sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddRange(new SqlParameter[]
                                                {
                                                    new SqlParameter { ParameterName = "login", Value = login },
                                                    new SqlParameter { ParameterName = "password", Value = password },
                                                });

                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            manager = new Manager();

                            manager.Id = (int)reader["Id"];
                            manager.FirstName = (string)reader["FirstName"];
                            manager.LastName = (string)reader["LastName"];
                            manager.Login = (string)reader["Login"];
                        }
                    }
                }
            }

            return manager;
        }

        #endregion
    }
}
