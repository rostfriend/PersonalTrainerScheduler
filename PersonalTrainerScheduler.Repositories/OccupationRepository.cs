using PersonalTrainerScheduler.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalTrainerScheduler.Repositories
{
    public class OccupationRepository : IOccupationRepository
    {
        
        #region Fields

        private string _connectionString;

        private const string spGetAllOccupations = "spGetAllOccupations";
        private const string spGetOccupationsByTrainerId = "spGetOccupationsByTrainerId";

        #endregion

        #region Constructor
        public OccupationRepository(string connString)
        {
            this._connectionString = connString;
        }

        #endregion

        #region Methods
        public List<Occupation> GetAllOccupations()
        {
            var occupations = new List<Occupation>();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(spGetAllOccupations, sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            occupations.Add(new Occupation() { Id = (int)reader["Id"], OccupationName = (string)reader["Occupation"]});
                        }

                    }
                }
            }
            return occupations;
        }

        public List<Occupation> GetOccupationsByTrainerId(int trainerId)
        {
            var occupations = new List<Occupation>();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(spGetOccupationsByTrainerId, sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("trainerId",trainerId);

                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            occupations.Add(new Occupation() { Id = (int)reader["OccupationId"], OccupationName = (string)reader["Occupation"] });
                        }

                    }
                }
            }
            return occupations;
        }

        #endregion
    }
}
