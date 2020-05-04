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
using System.Collections;

using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;
using PantryAid.Core;

namespace PantryAid.ViewModels
{
    public class PantryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private iIngredientData _ingredientDatabaseAccess;

        public INavigation navigation { get; set; }

        public PantryViewModel(INavigation nav, iIngredientData databaseAccess)
        {
            //Navigation and command binding
            this.navigation = nav;

            //Injection of view model
            _ingredientDatabaseAccess = databaseAccess;
        }

        //  View Model Getter and Setters and properties
        private ListViewModel<IngredientItem> _ingredientList = new ListViewModel<IngredientItem>();
        public ListViewModel<IngredientItem> ingredientList
        {
            get { return _ingredientList; }
            set
            {
                _ingredientList = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Ingredient"));
            }
        }

        public async void FillGrid()
        {
            iIngredientData ingrdata = new IngredientData(new SqlServerDataAccess());

            List<IngredientItem> results = ingrdata.GetIngredientsFromPantry(SqlServerDataAccess.UserID);

            if (results == null)
            {
                return;// await DisplayAlert("Error", "An error occurred when getting ingredients from pantry", "OK");
            }

            foreach (IngredientItem item in results)
            {
                _ingredientList.Add(item);
            }

        }

        // Commands
        public ICommand AddCommand { protected set; get; }
        public void OnAdd(object sender, string ingrName, string quant)
        {
            string ingrname = ingrName;//await DisplayPromptAsync("Add", "Enter an ingredient name", "Add", "Cancel", "eg. Apple", 500);

            if (ingrname == null) //User clicked cancel
                return;

            double quantity = 0.0f;
            bool validquantity = false;
            while (!validquantity)
            {
                string strquant = quant;//await DisplayPromptAsync("How much?", "Enter a quantity", "Add", "Cancel", "eg. 3", 10, Keyboard.Numeric);

                if (strquant == null) //User clicked cancel
                    return;

                quantity = Convert.ToDouble(strquant);

                if (quantity <= 0)
                    return; //await DisplayAlert("Error", "Quantity must be positive", "OK");
                else
                    validquantity = true;
            }


            IngredientData ingrdata = new IngredientData(new SqlServerDataAccess());

            Ingredient foundingr = ingrdata.GetIngredient(ingrname.ToLower());


            List<IngredientItem> pantryingredients = ingrdata.GetIngredientsFromPantry(SqlServerDataAccess.UserID);

            if (pantryingredients.Exists(x => x.ID == foundingr.IngredientID))
            {
                //await DisplayAlert("Error", "The specified ingredient is already in your pantry", "OK");
                return;
            }

            //_ingredientList.Add(new IngredientItem(foundingr, quantity, Measurements));
            //ingrdata.AddIngredientToPantry(SqlServerDataAccess.UserID, foundingr, quantity);
        }


        public ICommand RemoveCommand { protected set; get; }
        public async void OnRemove(string ingrName)
        {
            string ingrname = ingrName;// = await DisplayPromptAsync("Remove", "Enter an ingredient name", "OK", "Cancel", "eg. Apple", 500);

            if (ingrname == null) //User clicked cancel
                return;

            IngredientData ingrdata = new IngredientData(new SqlServerDataAccess());
            Ingredient foundingr = ingrdata.GetIngredient(ingrname.ToLower());

           IngredientItem item = _ingredientList.ListView.Single(x => x.ID == foundingr.IngredientID);
            _ingredientList.ListView.Remove(item);
        }

        /// Authenticate an ingedient against a SQL database
        /// returns true is ingredient was authenticated, false if not

        //private bool AuthenticateIngredient()
        //{
        //    bool auth = false;

        //    //Ingredient ing = _ingredientDatabaseAccess.GetIngredient(_ingredientItem.ToLower());

        //    //auth = (ing == null ? false : true);

        //    if (auth)
        //    {

        //    }

        //    return auth;
        //}

        private void QuantityChange(object sender)
        {
            Button b = (Button)sender;
            //Get Command param
            IngredientItem ob = b.CommandParameter as IngredientItem;

            if (ob != null)
            {
                iIngredientData ingrdata = new IngredientData(new SqlServerDataAccess());
                //Increment or decrement based on which button was clicked
                double newquant = ob.Quantity;
                if (b.Text == "+")
                    newquant += 1;
                else if (b.Text == "-")
                    newquant -= 1;

                ingrdata.UpdatePantryIngredientQuantity(SqlServerDataAccess.UserID, ob.ID, newquant);
                //Replace the item in the list with a duplicate that has the changed quantity
                int index = _ingredientList.ListView.IndexOf(ob);
                _ingredientList.ListView.Remove(ob);
                if (newquant > 0)
                    _ingredientList.ListView.Insert(index, new IngredientItem(ob.Ingredient, newquant, ob.Measurement));
            }
        }
    }
}
