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
    class GroceryListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private iIngredientData _ingredientDatabaseAccess;

        
        public INavigation navigation { get; set; }

        //Properties
        private int _quantity;
        private List<Ingredient> _ingredients;


        public GroceryListViewModel(INavigation nav, iIngredientData databaseAccess)
        {
            // Navigation and command binding
            this.navigation = nav;
            AddCommand = new Command(OnAdd);

            // Dependency injection
            _ingredientDatabaseAccess = databaseAccess;
        }

        // View Model getter and setters and properties
        private ListViewModel<IngredientItem> _ingredient;
        public ListViewModel<IngredientItem> Ingredient
        {
            get { return _ingredient; }
            set
            {
                _ingredient = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Ingredient"));
            }
        }

        //
        // end Properties

        public ICommand AddCommand { protected set; get; }
        public void OnAdd()
        {
            // Not authenticating for Grocery List
            // Add ingredient and Quantity to Grocery List
        }
    }
}
