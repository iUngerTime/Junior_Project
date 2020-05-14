using Database_Helpers;
using PantryAid.Core.Interfaces;
using PantryAid.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PantryAid.ViewModels
{
    class UserPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private iUserDataRepo _userDatabaseAccess;
        public INavigation navigation { get; set; }

        #region private properties
        //Properties
        private int _userid;
        private string _email;
        private string _password;
        private List<Ingredient> _alergies;
        private List<Recipe_Short> _favoriteRecipes;
        private List<Recipe_Short> _dislikedRecipes;
        //PLACEHOLDER FOR private list<preferences> DietaryOptions;
        #endregion

        public UserPageViewModel(INavigation nav, iUserDataRepo databaseAccess)
        {
            //Navigation and command binding
            this.navigation = nav;

            //Injection of view model
            _userDatabaseAccess = databaseAccess;

            //Get Info of user
            _userid = SqlServerDataAccess.UserID;
            User currUser = GetUserInfoFromDB();
            _email = currUser.Email;
            _password = currUser.Hash;

            //Command binding
            ChangeEmail = new Command(OnChangeEmailPress);
            ChangePassword = new Command(OnChangePasswordPress);
        }

        #region public properties
        //Getters and setters
        public int UserId
        {
            get { return _userid; }
            set
            {
                _userid = value;
                PropertyChanged(this, new PropertyChangedEventArgs("UserId"));
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        public List<Ingredient> Alergies
        {
            get { return _alergies; }
            set
            {
                _alergies = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Alergies"));
            }
        }

        public List<Recipe_Short> FavoriteRecipes
        {
            get { return _favoriteRecipes; }
            set
            {
                _favoriteRecipes = value;
                PropertyChanged(this, new PropertyChangedEventArgs("FavoriteRecipes"));
            }
        }

        public List<Recipe_Short> DislikedRecipes
        {
            get { return _dislikedRecipes; }
            set
            {
                _dislikedRecipes = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DislikedRecipes"));
            }
        }
        #endregion

        #region public commands
        public ICommand ChangeEmail { protected set; get; }
        public ICommand ChangePassword { protected set; get; }
        #endregion

        #region command implementation
        async public void OnChangeEmailPress()
        {
             string firstAttempt = await Application.Current.MainPage.DisplayPromptAsync("Email Change", "Enter new email below");

             if(firstAttempt != null)
             {
                string secondAttempt = await Application.Current.MainPage.DisplayPromptAsync("Confirm change", "Re-enter email for verification");

                if(secondAttempt != null)
                {
                    if(firstAttempt != secondAttempt)
                        await Application.Current.MainPage.DisplayAlert("Error", "Emails you entered differ, try again", "OK");
                    else
                    {
                        //update user with new email
                        User newInfo = new User { Hash = _password, Email = firstAttempt, Id = _userid };
                        int pass = _userDatabaseAccess.EditUserInfo(newInfo, newInfo);

                        if(pass == 1)
                        {
                            await Application.Current.MainPage.DisplayAlert("Success", "Your email has been changed!", "OK");

                            //Update UI
                            Email = newInfo.Email;
                        }
                    }
                }
            }
        }

        async public void OnChangePasswordPress()
        {
            string firstAttempt = await Application.Current.MainPage.DisplayPromptAsync("Password Change", "Enter new password below");

            if (firstAttempt != null)
            {
                string secondAttempt = await Application.Current.MainPage.DisplayPromptAsync("Confirm change", "Re-enter password for verification");

                if (secondAttempt != null)
                {
                    if (firstAttempt != secondAttempt)
                        await Application.Current.MainPage.DisplayAlert("Error", "Passwords you entered differ, try again", "OK");
                    else
                    {
                        //hash the plain text password
                        firstAttempt = Database_Helpers.Hashing.HashPassword(secondAttempt);

                        //Change password
                        //update user
                        User newInfo = new User { Hash = firstAttempt, Email = _email, Id = _userid };
                        int pass = _userDatabaseAccess.EditUserInfo(newInfo, newInfo);

                        if(pass == 1)
                        {
                            await Application.Current.MainPage.DisplayAlert("Success", "Your password has been changed!", "OK");

                            //Update UI *THIS IS FOR TESTING LEAVE COMMENTED OUT
                            //Password = secondAttempt;
                        }
                    }
                }
            }
        }
        #endregion

        #region private functions
        private User GetUserInfoFromDB()
        {
            User usr =_userDatabaseAccess.GetUser(_userid);

            return usr;
        }
        #endregion


    }
}
