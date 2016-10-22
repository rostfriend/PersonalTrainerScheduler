using PersonalTrainerScheduler.Entities;
using PersonalTrainerScheduler.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;




namespace PersonalTrainerScheduler.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        private List<Trainer> displayedTrainers = new List<Trainer>();
        private List<Customer> displayedCustomers = new List<Customer>();
        private List<TrainingSession> sortedTrainingSessionsByCustomerId;
        private List<TrainingSession> sortedTrainingSessionsByDateAndTrainerId;

        private List<Trainer> sortedTrainers;
        private List<Trainer> freeTrainersByDateTime;
        private List<Customer> sortedCustomers;
        private List<Occupation> currentOccupations = new List<Occupation>();

        private TrainerRepository trainerRepository;
        private OccupationRepository occupationRepository;
        private CustomerRepository customerRepository;
        private TrainingSessionRepository trainingSessionRepository;

        private const int firstIndex = 0;


        private string _connectionString;

        #endregion

        #region Constructor
        public MainWindow(Manager manager)
        {
            InitializeComponent();

            _connectionString = ConfigurationManager.ConnectionStrings["PersonalTrainerSchedulerConnectionString"].ConnectionString;

            trainerRepository = new TrainerRepository(_connectionString);
            occupationRepository = new OccupationRepository(_connectionString);
            customerRepository = new CustomerRepository(_connectionString);
            trainingSessionRepository = new TrainingSessionRepository(_connectionString);

            SetAllElements();
            SetCurrentManager(manager);
        }

        #endregion


        #region RegistrationEvents

        private void CustomersSelectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentCustomer = customersSelectionComboBox.SelectionBoxItem as Customer;
            if (currentCustomer == null)
            {
                return;
            }
            phoneNumberSelectedCustomerTB.Text = currentCustomer.PhoneNumber;
        }
        private void RegistrationDataPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            RegistrationPickDateTime();
        }
        private void AvailibleTimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RegistrationPickDateTime();
        }
        private void RegistrationPickDateTime()
        {
            if (registrationDataPicker.SelectedDate != null && availibleTimeComboBox != null)
            {
                freeTrainersByDateTime = new List<Trainer>();
                foreach (var item in trainerRepository.GetFreeTrainersByDateTime(GetCurrentlySelectedTime()))
                {
                    freeTrainersByDateTime.Add(item);
                }

                availibleTrainerComboBox.ItemsSource = freeTrainersByDateTime;
                availibleTrainerComboBox.SelectedIndex = firstIndex;
            }

        }
        private void RegisterTrainingSessionBTN_Click(object sender, RoutedEventArgs e)
        {
            if (availibleTrainerComboBox.SelectedItem == null)
            {
                MessageBox.Show("No trainers available!");
                return;
            }

            if (registrationDataPicker.SelectedDate == null)
            {
                MessageBox.Show("Please, select date!");
                return;
            }

            if (GetCurrentlySelectedTime() < DateTime.Now)
            {
                MessageBox.Show("Please, enter time properly!");
                return;
            }
            int trainerId = (availibleTrainerComboBox.SelectedItem as Trainer).Id;
            int customerId = (customersSelectionComboBox.SelectedItem as Customer).Id;
			// Review Yurii KL: You should handle an exception from DB
			// While adding New Training Session with same trainer, customer and datetime an unhandled exception occurs
            trainingSessionRepository.RegisterTrainingSession(trainerId, customerId, GetCurrentlySelectedTime());
            SetSortedTrainingSessionByCustomerId();
            SetTrainersSchedule();
            MessageBox.Show("Training session registered!");
        }

        #endregion

        #region TrainerEvents

        private void TrainersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var trainer = trainersList.SelectedItem as Trainer;     
            if (trainer == null)
            {
                return;
            }
            trainerFirstName.Text = trainer.FirstName;
            trainerLastName.Text = trainer.LastName;
            trainerDateOfBirth.Text = trainer.DateOfBirth.ToShortDateString();
            trainerPhoneNumber.Text = trainer.PhoneNumber;
            trainerStartOfWork.Text = trainer.StartOfWorkTime.ToString();
            trainerEndOfWork.Text = trainer.EndOfWorkTime.ToString();

            #region setTrainerOccupations

            var st = new StringBuilder();
            var count = 0;
            var comma = ",";
            var indent = " ";
            foreach (var item in trainer.Occupations)
            {
                count++;
                if (trainer.Occupations.Count == count)
                {
                    comma = "";
                    indent = "";
                }
                st.Append(item.OccupationName + comma + indent);
            }
            trainerOccupations.Text = st.ToString();

            #endregion

            SetTrainersSchedule();
        }
        private void Categories_SelectionChanged(object sender, SelectionChangedEventArgs e)////////////////////////////
        {
            if (!(bool)ByCategoryRB.IsChecked)
            {
                return;
            }
            Occupation selectedOcupation = Categories.SelectedItem as Occupation;
            sortedTrainers = new List<Trainer>();

            foreach (var trainer in displayedTrainers)
            {
                if (trainer.Occupations.Where(occ => (occ.Id) == selectedOcupation.Id).Count() != 0)
                {
                    sortedTrainers.Add(trainer);
                }
            }

            trainersList.ItemsSource = sortedTrainers;
            trainersList.Items.Refresh();
            trainersList.SelectedIndex = firstIndex;
        }
        private void TrainersScheduleDataPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SetTrainersSchedule();
        }
        private void ByCategoryRB_Unchecked(object sender, RoutedEventArgs e)
        {
            displayedTrainers.Clear();
            SetTrainersList();
        }
        private void ByCategoryRB_Checked(object sender, RoutedEventArgs e)//////////////////////////
        {
            Categories.SelectedIndex = firstIndex;

        }
        private void DeleteTrainingSessionInTrainersBTN_Click(object sender, RoutedEventArgs e)
        {
            if (scheduleTrainerGrid.SelectedItem == null)
            {
                MessageBox.Show("Nothing to delete!");
                return;
            }

            var sessionId = (scheduleTrainerGrid.SelectedItem as TrainingSession).Id;
            trainingSessionRepository.DeleteTrainingSessionById(sessionId);
            SetTrainersSchedule();
            MessageBox.Show("Training session deleted!");
        }
        private void DeleteTrainerBTN_Click(object sender, RoutedEventArgs e)
        {
            if (trainersList.SelectedItem == null)
            {
                MessageBox.Show("Nothing to delete!");
                return;
            }
            var selectedTrainer = trainersList.SelectedItem as Trainer;
            trainerRepository.DeleteTrainerById(selectedTrainer.Id);
            MessageBox.Show("Trainer Deleted!");

            RefreshTrainersList();
        }
        private void NewTrainerBTN_Click(object sender, RoutedEventArgs e)
        {
            var trainerWindow = new TrainerRegistration();
            trainerWindow.ShowDialog();
            RefreshTrainersList();        
        }
        private void ModifyTrainerBTN_Click(object sender, RoutedEventArgs e)
        {
            if (trainersList.SelectedItem != null)
            {
                var selectedTrainer = trainersList.SelectedItem as Trainer;
                var trainerWindow = new TrainerRegistration(selectedTrainer);
                trainerWindow.ShowDialog();

                RefreshTrainersList();
            }
        }


        #endregion

        #region CustomerEvents

        private void TextChanged_CustomersSearchTB(object sender, TextChangedEventArgs e)
        {
            SetCustomersList();
        }
        private void SetSortedTrainingSessionByCustomerId()
        {
            sortedTrainingSessionsByCustomerId = new List<TrainingSession>();
            var currentCustomer = customersList.SelectedItem as Customer;

            foreach (var item in trainingSessionRepository.GetAllTrainingSessionsByCustomerId(currentCustomer.Id))
            {
                sortedTrainingSessionsByCustomerId.Add(item);
            }
            scheduleCustomerGrid.ItemsSource = sortedTrainingSessionsByCustomerId;
        }
        private void CustomersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentCustomer = customersList.SelectedItem as Customer;
            if (currentCustomer == null)
            {
                return;
            }
            customerFirstName.Text = currentCustomer.FirstName;
            customerLastName.Text = currentCustomer.LastName;
            customerDateOfBirth.Text = currentCustomer.DateOfBirth.ToShortDateString();
            customerPhoneNumber.Text = currentCustomer.PhoneNumber;
            customerAdress.Text = currentCustomer.Adress;

            SetSortedTrainingSessionByCustomerId();
        }
        private void DeleteTrainingSessionInCustomersBTN_Click(object sender, RoutedEventArgs e)
        {
            if (scheduleCustomerGrid.SelectedItem == null)
            {
                MessageBox.Show("Nothing to delete!");
                return;
            }
			// Review Yurii KL: Good practice when you have to confirm deletion of something
            var sessionId = (scheduleCustomerGrid.SelectedItem as TrainingSession).Id;
            trainingSessionRepository.DeleteTrainingSessionById(sessionId);
            SetSortedTrainingSessionByCustomerId();
            MessageBox.Show("Training session deleted!");
        }
        private void DeleteCustomerBTN_Click(object sender, RoutedEventArgs e)
        {
            if (customersList.SelectedItem == null)
            {
                MessageBox.Show("Nothing to delete!");
                return;
            }
			// Review Yurii KL: Good practice when you have to confirm deletion of something
            var selectedCustomer = customersList.SelectedItem as Customer;
            customerRepository.DeleteCustomerById(selectedCustomer.Id);
            MessageBox.Show("Customer Deleted!");

            RefreshCustomersList();
        }
        private void NewCustomerBTN_Click(object sender, RoutedEventArgs e)
        {
                var customerWindow = new CustomerRegistration();
                customerWindow.ShowDialog();

                RefreshCustomersList();
        }
        private void ModifyCustomerBTN_Click(object sender, RoutedEventArgs e)
        {
            if (customersList.SelectedItem != null)
            {
                var selectedCustomer = customersList.SelectedItem as Customer;
                var customerWindow = new CustomerRegistration(selectedCustomer);
                customerWindow.ShowDialog();

                RefreshCustomersList();
            }
        }

        #endregion


        #region Methods

        private void SetAllElements()
        {
            customersSelectionComboBox.SelectedIndex = firstIndex;
            availibleTimeComboBox.SelectedIndex = firstIndex;
            availibleTrainerComboBox.SelectedIndex = firstIndex;

            SetTrainersList();
            SetCategories();
            SetCustomersList();
            SetSortedTrainingSessionByCustomerId();
        }
        private void SetCurrentManager(Manager manager)
        {
            managerFirstNameLB.Text = manager.FirstName;
            managerLastNameLB.Text = manager.LastName;
        }
        private void SetTrainersList()
        {
            foreach (var item in trainerRepository.GetAllTrainers())
            {
                displayedTrainers.Add(item); 
            }
            trainersList.ItemsSource = displayedTrainers; 
            availibleTrainerComboBox.Items.Refresh();
            availibleTrainerComboBox.ItemsSource = displayedTrainers;
            availibleTrainerComboBox.SelectedIndex = firstIndex;
            trainersList.SelectedIndex = firstIndex;

        }
        private void SetCategories()
        {
            Categories.ItemsSource = currentOccupations;
            foreach (var item in occupationRepository.GetAllOccupations())
            {
                currentOccupations.Add(item);
            }
            Categories.Items.Refresh();
        }
        public void SetCustomersList()
        {

            sortedCustomers = new List<Customer>();

            foreach (var item in customerRepository.GetCustomersByLastName(CustomersSearchTB.Text))
            {
                sortedCustomers.Add(item);
            }
            customersList.ItemsSource = sortedCustomers;
            customersList.Items.Refresh();

            foreach (var item in customerRepository.GetAllCustomers())
            {
                displayedCustomers.Add(item);
            }
            customersSelectionComboBox.ItemsSource = displayedCustomers;

            customersList.SelectedIndex = firstIndex;
        }
        private void SetTrainersSchedule()
        {
            sortedTrainingSessionsByDateAndTrainerId = new List<TrainingSession>();
            var trainer = trainersList.SelectedItem as Trainer;

            if (trainersScheduleDataPicker.SelectedDate != null)
            {
                DateTime date = trainersScheduleDataPicker.SelectedDate ?? new DateTime();
                foreach (var item in trainingSessionRepository.GetTrainingSessionsByDateAndTrainerId(trainer.Id, date))
                {
                    sortedTrainingSessionsByDateAndTrainerId.Add(item);
                }
                scheduleTrainerGrid.ItemsSource = sortedTrainingSessionsByDateAndTrainerId;
            }
        }
        private void RefreshTrainersList()
        {
            displayedTrainers.Clear();
            trainersList.Items.Refresh();
            SetTrainersList();
        }
        private void RefreshCustomersList()
        {
            sortedCustomers.Clear();
            customersList.Items.Refresh();
            SetCustomersList();
        }
        private DateTime GetCurrentlySelectedTime()
        {
            var hour = ((availibleTimeComboBox.SelectedValue as ComboBoxItem).Content).ToString().Substring(0, 2);
            DateTime desiredDateTime = registrationDataPicker.SelectedDate ?? new DateTime();
            desiredDateTime = desiredDateTime.AddHours(Double.Parse(hour));

            return desiredDateTime;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you really want to exit?", "Confirm", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        #endregion

    }
}