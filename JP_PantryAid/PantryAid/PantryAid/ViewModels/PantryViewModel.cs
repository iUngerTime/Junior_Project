using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Database_Helpers;
using PantryAid.Core.Interfaces;
using PantryAid.Core.Models;
using PantryAid.Views;

using System.Data.SqlClient;
using System.Windows.Input;
using Xamarin.Forms;

namespace PantryAid.ViewModels
{
    class PantryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private iIngredientData _ingredientDatabaseAccess;
        public Action DisplayInvalidIngredientPrompt;


        public INavigation navigation { get; set; }

        public PantryViewModel(INavigation nav, iIngredientData databaseAccess)
        {
            //Navigation and command binding
            this.navigation = nav;
            AddCommand = new Command(OnAdd);
            RemoveCommand = new Command(OnRemove);

            //Injection of view model
            _ingredientDatabaseAccess = databaseAccess;
        }

        //  View Model Getter and Setters and properties
        private string _ingredient;
        public string ingredient
        {
            get { return _ingredient; }
            set
            {
                _ingredient = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Ingredient"));
            }
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Quantity"));
            }
        }

        private Measurements _measurement;
        public Measurements Measurement
        {
            get { return _measurement; }
            set
            {
                _measurement = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Measurement"));
            }
        }

        //
        // end Properties

        // Commands
        public ICommand AddCommand { protected set; get; }
        public void OnAdd()
        {
            bool authenticated = AuthenticateIngredient();
            if(authenticated)
            {
                //add ingredient with quantity to database
            }
            else
            {
                DisplayInvalidIngredientPrompt();
            }
        }

        public ICommand RemoveCommand { protected set; get; }
        public void OnRemove()
        {

        }

        /// Authenticate an ingedient against a SQL database
        /// returns true is ingredient was authenticated, false if not
        
        private bool AuthenticateIngredient()
        {
            bool auth = false;

            Ingredient ing = _ingredientDatabaseAccess.GetIngredient(ingredient.ToLower());

            auth = (ing == null ? false : true);

            if (auth)
            {
                // Set ing id to current id or ing string to current string
                //SqlHelper. = ing.IngredientID;
                //SqlServerDataAccess.IngredientID = ing.IngredientID;
            }

            return auth;
        }
    }
}
