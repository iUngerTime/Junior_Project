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
        
        public INavigation navigation { get; set; }

        //Properties
        private int _quantity;
        private List<Ingredient> _ingredients;


        public GroceryListViewModel(INavigation nav)
        {
            //Navigation and command binding
            this.navigation = nav;

            AddCommand = new Command(OnAdd);
        }

        // View Model getter and setters
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
            // Not authenticating for Grocery List
            // Add ingredient and Quantity to Grocery List
        }
    }
}
