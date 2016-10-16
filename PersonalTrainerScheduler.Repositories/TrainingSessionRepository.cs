using PersonalTrainerScheduler.Entities;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalTrainerScheduler.Repositories
{
    public class TrainingSessionRepository : ITrainingSessionRepository
    {
        
        #region Fields

        private string _connectionString;

        private const string spGetAllTrainingSessionsByCustomerId = "spGetAllTrainingSessionsByCustomerId";
        private const string spGetTrainersScheduleByTheDay = "spGetTrainersScheduleByTheDay";
        private const string spRegisterTrainingSession = "spRegisterTrainingSession";
        private const string spDeleteTrainingSessionById = "spDeleteTrainingSessionById";

        #endregion

        #region Constructor
        public TrainingSessionRepository(string connString)
        {
            this._connectionString = connString;
        }

        #endregion

        #region Methods
        public void DeleteTrainingSessionById(int sessionId)
        {

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(spDeleteTrainingSessionById, sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("sessionId", sessionId);

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public void RegisterTrainingSession(int trainerId, int customerId,DateTime trainingSessionDateTimeStart)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(spRegisterTrainingSession, sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddRange(new SqlParameter[]
                                                {
                                                    new SqlParameter { ParameterName = "trainerId", Value = trainerId },
                                                    new SqlParameter { ParameterName = "customerId", Value = customerId },
                                                    new SqlParameter { ParameterName = "trainingSessionDateTimeStart", Value = trainingSessionDateTimeStart },
                                                });

                    try
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number==50001)
                        throw new ArgumentException("Customer is already registered for training session on that time!");
                    }
                }
            }
        }

        public List<TrainingSession> GetTrainingSessionsByDateAndTrainerId(int trainerId, DateTime date)
        {
            var trainingSessions = new List<TrainingSession>();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(spGetTrainersScheduleByTheDay, sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddRange(new SqlParameter[]
                                                {
                                                    new SqlParameter { ParameterName = "trainerId", Value = trainerId },
                                                    new SqlParameter { ParameterName = "date", Value = date },
                                                });

                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            trainingSessions.Add(new TrainingSession
                            {
                                CustomerEntity = new Customer
                                {
                                    FirstName = (string)reader["FirstName"],
                                    LastName = (string)reader["LastName"]
                                },
                                Id = (int)reader["Id"],
                                TrainingDateTimeStart = (DateTime)reader["TrainingSessionDateTimeStart"],
                            });
                        }
                    }
                }
            }
            return trainingSessions;
        }

        public List<TrainingSession> GetAllTrainingSessionsByCustomerId(int customerId)
        {
            var trainingSessions = new List<TrainingSession>();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(spGetAllTrainingSessionsByCustomerId, sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("customerId", customerId);

                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            trainingSessions.Add(new TrainingSession
                            {
                                TrainerEntity = new Trainer
                                {
                                    FirstName = (string)reader["FirstName"],
                                    LastName = (string)reader["LastName"]
                                },
                                Id = (int)reader["Id"],
                                TrainingDateTimeStart = (DateTime)reader["TrainingSessionDateTimeStart"],
                            });
                        }
                    }
                }
            }
            return trainingSessions;
        }

        #endregion
    }
}
