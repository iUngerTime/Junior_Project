using Database_Helpers;
using PantryAid.Core.Interfaces;
using PantryAid.Core.Models;
using PantryAid.OfficialViews;
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
        private iUserDataRepo _userDatabaseAccess;

        public INavigation navigation { get; set; }

        public LoginViewModel(INavigation nav, iUserDataRepo databaseAccess)
        {
            //Navigation and command binding
            this.navigation = nav;
            SubmitCommand = new Command(OnSubmit);
            SignupCommand = new Command(OnSignup);

            //Injection of view model
            _userDatabaseAccess = databaseAccess;
        }

        public Action DisplayInvalidLoginPrompt;
        public Action DisplaySuccessfulSignupPrompt;
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
        public ICommand SignupCommand { protected set; get; }

        public void OnSubmit()
        {
            //bool for authenticated or not
            bool authenticated = AuthenticateUser();

            if (authenticated)
            {
                //Open DemoPage.xaml Form as a new stack
                navigation.PushModalAsync(new TabbedMasterPage());
            }
            else
            {
                DisplayInvalidLoginPrompt();
            }
        }

        public void OnSignup()
        {
            string newpass = Hashing.HashPassword(Password);

            _userDatabaseAccess.AddUser(new User(Email, newpass));

            DisplaySuccessfulSignupPrompt();
        }

        /// <summary>
        /// Authenticates a user against a SQL database
        /// </summary>
        /// <returns>True if user was authenticated correctly, False if not</returns>
        private bool AuthenticateUser()
        {
            bool auth = false;

            //Run SQL command to get user's email
            User usr = _userDatabaseAccess.GetUser(email);

            if (usr == null)
                auth = false;
            else if (Database_Helpers.Hashing.VerifyPassword(password, usr.Hash) == false)
                auth = false;
            else
                auth = true;

            if (auth)
            {
                SqlServerDataAccess.CurrentUser = usr;
                SqlServerDataAccess.UserID = usr.Id;
            }

            return auth;
        }
    }
}
