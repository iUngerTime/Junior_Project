using PantryAid.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PantryAid.ViewModels
{
    class UserPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //Properties
        private int _userid;
        private string _email;
        private string _password;
        private List<Ingredient> _alergies;
        private List<Recipe_Short> _favoriteRecipes;
        private List<Recipe_Short> _dislikedRecipes;
        //PLACEHOLDER FOR private list<preferences> DietaryOptions;

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
    }
}
