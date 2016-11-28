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
using PersonalTrainerScheduler.Repositories;
using System.Configuration;
using PersonalTrainerScheduler.UI.Code;
using PersonalTrainerScheduler.Entities;

namespace PersonalTrainerScheduler.UI
{
    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        #region Fields
        // Review TK: It is a good practice to use a similar approach in order to initialize private fields.
        // I would prefer to use readonly modifier.
        private string _connectionString = ConfigurationManager.ConnectionStrings["PersonalTrainerSchedulerConnectionString"].ConnectionString;
        private ManagerRepository managerRepository;

        #endregion

        #region Constructor
        public LoginDialog()
        {
            InitializeComponent();
            managerRepository = new ManagerRepository(_connectionString);
        }

        #endregion

        #region EventMethods
        private void Login_BTN(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text;
            string password =  Encryptor.GetHash(passwordTextBox.Password);

            Manager manager = managerRepository.GetManagerByLogin(login, password);

            // Review TK: You could avoid using of if else statements.
            // You could just write if(){ MessageBox.Show(...); return; }
            if (manager == null)
            {
                MessageBox.Show("Invalid login or password!");
            }
            else
            {
                MainWindow window = new MainWindow(manager);
                window.Show();
                this.Close();
            }
        }
        private void Close_BTN(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        #endregion

    }
}
