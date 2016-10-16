using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalTrainerScheduler.Entities;

namespace PersonalTrainerScheduler.Repositories
{
    public interface ICustomerRepository
    {
        void AddNewCustomer(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string adress);
        void ModifyCustomerById(int customerId, string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string adress);
        void DeleteCustomerById(int customerId);
        List<Customer> GetAllCustomers();
        List<Customer> GetCustomersByLastName(string lastName);
    }
}
