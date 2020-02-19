using Database_Helpers;
using PantryAid.Views;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Input;
using Xamarin.Forms;

namespace PantryAid.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public INavigation navigation { get; set; }

        public LoginViewModel(INavigation nav)
        {
            this.navigation = nav;
            SubmitCommand = new Command(OnSubmit);
        }

        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        public ICommand SubmitCommand { protected set; get; }

        public void OnSubmit()
        {
            //bool for authenticated or not
            bool authenticated = AuthenticateUser();

            if (authenticated)
            {
                //Open DemoPage.xaml Form as a new stack
                navigation.PushModalAsync(new NavigationPage(new DemoPage()));
            }
            else
            {
                DisplayInvalidLoginPrompt();
            }
        }

        /// <summary>
        /// Authenticates a user against a SQL database
        /// </summary>
        /// <returns>True if user was authenticated correctly, False if not</returns>
        private bool AuthenticateUser()
        {
            bool auth = false;

            //Run SQL command to get user's email
            string query = "SELECT UserID, Email FROM Person WHERE LOWER(Email) = '" + email + "';";

            SqlConnection con = new SqlConnection(SqlHelper.GetConnectionString());
            SqlCommand comm = new SqlCommand(query, con);

            try
            {
                con.Open();

                SqlDataReader read = comm.ExecuteReader();

                if (read.Read())
                {
                    auth = true;
                    SqlHelper.UserID = read.GetInt32(0);
                }

                con.Close();
            }
            catch (Exception)
            { }

            return auth;
        }
    }
}
