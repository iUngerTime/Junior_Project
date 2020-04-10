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

            //Injection of view model
            _ingredientDatabaseAccess = databaseAccess;
        }

        //  View Model Properties 
        private string ingredient;
        public string Ingredient
        {
            get { return ingredient; }
            set
            {
                ingredient = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Ingredient"));
            }
        }

        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Quantity"));
            }
        }

        //
        // end Properties

        public ICommand AddCommand { protected set; get; }
        public void OnAdd()
        {
            bool authenticated = AuthenticateIngredient();
            if(authenticated)
            {
                //ask for quantity or take quantity in quantity box and then add to list
            }
            else
            {
                DisplayInvalidIngredientPrompt();
            }
        }

        /// Authenticate the chosen ingredient against a SQL database
        /// return true is ingredient was authenticated, falsei if not
        
        private bool AuthenticateIngredient()
        {
            bool auth = false;

            Ingredient ing = _ingredientDatabaseAccess.GetIngredient(ingredient);

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
