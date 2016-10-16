using PersonalTrainerScheduler.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalTrainerScheduler.Repositories
{
   public class CustomerRepository : ICustomerRepository
    {

        #region Fields

        private string _connectionString;

        private const string spGetAllCustomers = "spGetAllCustomers";
        private const string spGetCustomersByLastName = "spGetCustomersByLastName";
        private const string spDeleteCustomerById = "spDeleteCustomerById";
        private const string spAddNewCustomer = "spAddNewCustomer";
        private const string spModifyCustomerById = "spModifyCustomerById";

        #endregion

        #region Constructor
        public CustomerRepository(string connString)
        {
            this._connectionString = connString;
        }
        #endregion

        #region Methods

        public void AddNewCustomer(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string adress)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(spAddNewCustomer, sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddRange(new SqlParameter[]
                                                {
                                                    new SqlParameter { ParameterName = "firstName", Value = firstName },
                                                    new SqlParameter { ParameterName = "lastName", Value = lastName },
                                                    new SqlParameter { ParameterName = "dateOfBirth", Value = dateOfBirth },
                                                    new SqlParameter { ParameterName = "phoneNumber", Value = phoneNumber },
                                                    new SqlParameter { ParameterName = "adress", Value = adress }
                                                });

                    try
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 50011)
                            throw new ArgumentException("Customer already exists!");
                    }
                }
            }
        }

        public void ModifyCustomerById(int customerId, string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string adress)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(spModifyCustomerById, sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddRange(new SqlParameter[]
                                                {
                                                    new SqlParameter { ParameterName = "customerId", Value = customerId },
                                                    new SqlParameter { ParameterName = "firstName", Value = firstName },
                                                    new SqlParameter { ParameterName = "lastName", Value = lastName },
                                                    new SqlParameter { ParameterName = "dateOfBirth", Value = dateOfBirth },
                                                    new SqlParameter { ParameterName = "phoneNumber", Value = phoneNumber },
                                                    new SqlParameter { ParameterName = "adress", Value = adress }
                                                });

                        sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCustomerById(int customerId)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(spDeleteCustomerById, sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("customerId", customerId);

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public List<Customer> GetAllCustomers()
        {
            var customers = new List<Customer>();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(spGetAllCustomers, sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(new Customer() { Id = (int)reader["Id"], FirstName = (string)reader["FirstName"], LastName = (string)reader["LastName"], DateOfBirth = (DateTime)reader["DateOfBirth"], PhoneNumber = (string)reader["PhoneNumber"], Adress = (string)reader["Adress"] });
                        }
                    }
                }
            }
            return customers;
        }

        public List<Customer> GetCustomersByLastName(string lastName)
        {
            var customers = new List<Customer>();


            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(spGetCustomersByLastName, sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@lastName", lastName);

                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add( new Customer() { Id = (int)reader["Id"], FirstName = (string)reader["FirstName"], LastName = (string)reader["LastName"], DateOfBirth = (DateTime)reader["DateOfBirth"], PhoneNumber = (string)reader["PhoneNumber"], Adress = (string)reader["Adress"] });
                        }
                    }
                }
            }

            return customers;
        }

        #endregion 
    }
}
