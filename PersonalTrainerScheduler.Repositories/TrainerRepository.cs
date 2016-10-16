using PersonalTrainerScheduler.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalTrainerScheduler.Repositories
{
    public class TrainerRepository : ITrainerRepository
    {
        
        #region Fields

        private string _connectionString;

        private const string spGetAllTrainers = "spGetAllTrainers";
        private const string spGetFreeTrainersByDateTime = "spGetFreeTrainersByDateTime";
        private const string spDeleteTrainerById = "spDeleteTrainerById";
        private const string spAddNewTrainer = "spAddNewTrainer";
        private const string spModifyTrainerById = "spModifyTrainerById";

        private OccupationRepository occupationRepository;

        #endregion

        #region Constructor
        public TrainerRepository(string connString)
        {
            this._connectionString = connString;
            occupationRepository = new OccupationRepository(connString);
        }

        #endregion

        #region Methods

        public void ModifyTrainerById(int trainerId,string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, DateTime startOfWorkTime, DateTime endOfWorkTime)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(spModifyTrainerById, sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddRange(new SqlParameter[]
                                                {
                                                    new SqlParameter { ParameterName = "trainerId", Value = trainerId },
                                                    new SqlParameter { ParameterName = "firstName", Value = firstName },
                                                    new SqlParameter { ParameterName = "lastName", Value = lastName },
                                                    new SqlParameter { ParameterName = "dateOfBirth", Value = dateOfBirth },
                                                    new SqlParameter { ParameterName = "phoneNumber", Value = phoneNumber },
                                                    new SqlParameter { ParameterName = "startOfWorkTime", Value = startOfWorkTime },
                                                    new SqlParameter { ParameterName = "endOfWorkTime", Value = endOfWorkTime }
                                                });

                    try
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 50021)
                            throw new ArgumentException("Start of work time must be less than end of work time!");

                    }
                }
            }
        }

        public void AddNewTrainer(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, DateTime startOfWorkTime, DateTime endOfWorkTime, int occupationId)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(spAddNewTrainer, sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddRange(new SqlParameter[]
                                                {
                                                    new SqlParameter { ParameterName = "firstName", Value = firstName },
                                                    new SqlParameter { ParameterName = "lastName", Value = lastName },
                                                    new SqlParameter { ParameterName = "dateOfBirth", Value = dateOfBirth },
                                                    new SqlParameter { ParameterName = "phoneNumber", Value = phoneNumber },
                                                    new SqlParameter { ParameterName = "startOfWorkTime", Value = startOfWorkTime },
                                                    new SqlParameter { ParameterName = "endOfWorkTime", Value = endOfWorkTime },
                                                    new SqlParameter { ParameterName = "occupationId", Value = occupationId }
                                                });

                    try
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 50021)
                            throw new ArgumentException("Start of work time must be less than end of work time!");

                        if (ex.Number == 50011)
                            throw new ArgumentException("'Trainer already exists!");
                    }
                }
            }
        }

        public void DeleteTrainerById(int trainerId)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(spDeleteTrainerById, sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("trainerId", trainerId);

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public List<Trainer> GetAllTrainers()
        {
            var trainers = new List<Trainer>();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(spGetAllTrainers,sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            trainers.Add(new Trainer() {Occupations = occupationRepository.GetOccupationsByTrainerId((int)reader["Id"]), Id = (int)reader["Id"], FirstName = (string)reader["FirstName"], LastName = (string)reader["LastName"], DateOfBirth = (DateTime)reader["DateOfBirth"], PhoneNumber = (string)reader["PhoneNumber"], StartOfWorkTime = (TimeSpan)reader["StartOfWorkTime"], EndOfWorkTime = (TimeSpan)reader["EndOfWorkTime"] });
                        }
                    }
                }
            }
            return trainers;
        }

        public List<Trainer> GetFreeTrainersByDateTime(DateTime desiredDateTime)
        {
            var trainers = new List<Trainer>();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(spGetFreeTrainersByDateTime, sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@desiredDateTime", desiredDateTime);

                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            trainers.Add(new Trainer() { Id = (int)reader["Id"], FirstName = (string)reader["FirstName"], LastName = (string)reader["LastName"], DateOfBirth = (DateTime)reader["DateOfBirth"], PhoneNumber = (string)reader["PhoneNumber"], StartOfWorkTime = (TimeSpan)reader["StartOfWorkTime"], EndOfWorkTime = (TimeSpan)reader["EndOfWorkTime"] });
                        }

                    }
                }
            }
            return trainers;
        }

        #endregion
    }
}
