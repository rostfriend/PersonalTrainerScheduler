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
    /// Interaction logic for NewTrainerRegistration.xaml
    /// </summary>
    public partial class TrainerRegistration : Window
    {
        #region Fields

        private TrainerRepository trainerRepository;
        private OccupationRepository occupationRepository;
        private List<Occupation> occupations;

        private Trainer selectedTrainer;
        private bool isNewTrainer;
        private bool closleCheck = true;

        private string _connectionString;

        private const int firstIndex = 0;

        #endregion

        #region Constructors
        public TrainerRegistration()
        {

            InitializeComponent();
            _connectionString = ConfigurationManager.ConnectionStrings["PersonalTrainerSchedulerConnectionString"].ConnectionString;
            trainerRepository = new TrainerRepository(_connectionString);
            occupationRepository = new OccupationRepository(_connectionString);
            isNewTrainer = true;

            SetOccupations();
            startOfWorkCB.SelectedIndex = firstIndex;
            endOfWorkCB.SelectedIndex = firstIndex;
        }
        public TrainerRegistration(Trainer selectedTrainer)
        {
            InitializeComponent();
            _connectionString = ConfigurationManager.ConnectionStrings["PersonalTrainerSchedulerConnectionString"].ConnectionString;
            trainerRepository = new TrainerRepository(_connectionString);
            occupationRepository = new OccupationRepository(_connectionString);
            isNewTrainer = false;
            this.selectedTrainer = selectedTrainer;
            SetOccupations();

            firstNameTB.Text = selectedTrainer.FirstName;
            lastNameTB.Text = selectedTrainer.LastName;
            dateOfBirthDP.SelectedDate = selectedTrainer.DateOfBirth;
            phoneNumberTB.Text = Regex.Replace(selectedTrainer.PhoneNumber, @"\D", "");

            startOfWorkCB.SelectedIndex = firstIndex;
            endOfWorkCB.SelectedIndex = firstIndex;
            occupationCB.IsEnabled = false;
        }

        #endregion


        #region Event Methods
        private void SetOccupations()
        {
            occupations = new List<Occupation>();

            foreach (var item in occupationRepository.GetAllOccupations())
            {
                occupations.Add(item);
            }
            occupationCB.ItemsSource = occupations;
            occupationCB.SelectedIndex = firstIndex;
        }

        private DateTime GetCurrentlySelectedTime(ref ComboBox availibleTimeComboBox)
        {
            var hour = ((availibleTimeComboBox.SelectedValue as ComboBoxItem).Content).ToString().Substring(0, 2);
            DateTime desiredDateTime = dateOfBirthDP.SelectedDate ?? new DateTime();
            desiredDateTime = desiredDateTime.AddHours(Double.Parse(hour));

            return desiredDateTime;
        }

        private void OkBTN_Click(object sender, RoutedEventArgs e)
        {

            if (firstNameTB.Text == null || lastNameTB.Text == null || dateOfBirthDP.SelectedDate == null || phoneNumberTB.Text == null)
            {
                MessageBox.Show("Please, fill all of the fields!");
                return;
            }
            if (dateOfBirthDP.SelectedDate > (DateTime.Today.AddYears(-18)))
            {
                MessageBox.Show("Our trainer must be at least 18 years old!");
                return;
            }
            if (GetCurrentlySelectedTime(ref startOfWorkCB).Hour > GetCurrentlySelectedTime(ref endOfWorkCB).AddHours(-6).Hour)
            {
                MessageBox.Show("Working day must be at least 6 hours!");
                return;
            }
            if (phoneNumberTB.Text.Length < 10 || !Regex.IsMatch(phoneNumberTB.Text, "^[0-9]*$"))
            {
                MessageBox.Show("Please, enter correct phone number!");
                return;
            }

            string firstName = firstNameTB.Text;
            string lastName = lastNameTB.Text;
            DateTime dateOfBirth = dateOfBirthDP.SelectedDate ?? new DateTime(); //!!!
            string phoneNumber = Regex.Replace(phoneNumberTB.Text, @"(\d{3})(\d{7})", "($1)$2");
            DateTime startOfWork = GetCurrentlySelectedTime(ref startOfWorkCB);
            DateTime endOfWork = GetCurrentlySelectedTime(ref endOfWorkCB);

            if (isNewTrainer)
            {               
                int occupationId = (occupationCB.SelectedItem as Occupation).Id;
                trainerRepository.AddNewTrainer(firstName, lastName, dateOfBirth, phoneNumber, startOfWork, endOfWork, occupationId);
                MessageBox.Show("New trainer successfully added!");
            }
            else
            {
                int trainerId = selectedTrainer.Id;  
                trainerRepository.ModifyTrainerById(trainerId, firstName, lastName, dateOfBirth, phoneNumber, startOfWork, endOfWork);
                MessageBox.Show("Trainer is successfully modified!");
            }

            closleCheck = false;
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
        private void CloseBTN_Clode(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
