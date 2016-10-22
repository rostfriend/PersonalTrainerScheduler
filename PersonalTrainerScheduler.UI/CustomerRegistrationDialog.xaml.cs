using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PersonalTrainerScheduler.Entities;
using PersonalTrainerScheduler.Repositories;
using System.Configuration;
using System.Text.RegularExpressions;

namespace PersonalTrainerScheduler.UI
{
    /// <summary>
    /// Interaction logic for NewCustomerRegistration.xaml
    /// </summary>
    public partial class CustomerRegistration : Window
    {

        #region Fields
        // Review Yurii KL: Recommended to name private fields with _underscore
        private CustomerRepository customerRepository;
        private Customer selectedCustomer;

        private bool isNewCustomer;
        private bool closleCheck = true;
        private string _connectionString;

        #endregion

        #region Constructors
        public CustomerRegistration()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["PersonalTrainerSchedulerConnectionString"].ConnectionString;
            isNewCustomer = true;
            InitializeComponent();
            customerRepository = new CustomerRepository(_connectionString);
        }

        public CustomerRegistration(Customer selectedCustomer)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["PersonalTrainerSchedulerConnectionString"].ConnectionString;
            customerRepository = new CustomerRepository(_connectionString);
            isNewCustomer = false;
            InitializeComponent();
            this.selectedCustomer = selectedCustomer;

            firstNameTB.Text = this.selectedCustomer.FirstName;
            lastNameTB.Text = this.selectedCustomer.LastName;
            dateOfBirthDP.SelectedDate = this.selectedCustomer.DateOfBirth;
            phoneNumberTB.Text = Regex.Replace(this.selectedCustomer.PhoneNumber, @"\D", "");
            adressTB.Text = this.selectedCustomer.Adress;          
        }

        #endregion


        #region EventMethods
        private void OkBTN_Click(object sender, RoutedEventArgs e)
        {
            if (firstNameTB.Text == null || lastNameTB.Text == null || dateOfBirthDP.SelectedDate == null || phoneNumberTB.Text == null || adressTB.Text == null)
            {
                MessageBox.Show("Please, fill all of the fields!");
                return;
            }
            if (dateOfBirthDP.SelectedDate > (DateTime.Today.AddYears(-3)))
            {
                MessageBox.Show("Our customer must be at least 3 years old!");
                return;
            }
            if(phoneNumberTB.Text.Length < 10 || !Regex.IsMatch(phoneNumberTB.Text, "^[0-9]*$"))
            {
                MessageBox.Show("Please, enter correct phone number!");
                return;
            }

            string firstName = firstNameTB.Text;
            string lastName = lastNameTB.Text;
            DateTime dateOfBirth = dateOfBirthDP.SelectedDate ?? new DateTime();
            string phoneNumber = Regex.Replace(phoneNumberTB.Text, @"(\d{3})(\d{7})", "($1)$2");
            string adress = adressTB.Text;

            if (isNewCustomer)
            {
                customerRepository.AddNewCustomer(firstName, lastName, dateOfBirth, phoneNumber, adress);
                MessageBox.Show("New customer successfully added!");
            }
            else
            {
                int id = selectedCustomer.Id; 
                customerRepository.ModifyCustomerById(id, firstName, lastName, dateOfBirth, phoneNumber, adress);
                MessageBox.Show("Customer is successfully modified!");
            }

            closleCheck = false;
            this.Close();
        }

        private void CancelBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {   
            if (closleCheck == false)
            {
                return;
            }
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        #endregion
    }
}
