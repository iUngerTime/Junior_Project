using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Database_Helpers;
using PantryAid.Core.Models;
using PantryAid.Core.Interfaces;

namespace PantryAid
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PantryPage : ContentPage
    {
        ListViewModel<IngredientItem> _list = new ListViewModel<IngredientItem>();
        public PantryPage()
        {
            InitializeComponent();

            this.BindingContext = _list;
            FillGrid();
        }

        async public void FillGrid()
        {
            iIngredientData ingrdata = new IngredientData(new SqlServerDataAccess());
            
            List<IngredientItem> results = ingrdata.GetIngredientsFromPantry(SqlHelper.UserID);

            if (results == null)
            {
                await DisplayAlert("Error", "The data reader isn't doing its job", "end me");
            }

            foreach (IngredientItem item in results)
            {
                _list.Add(item);
            }
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            IngredientData ingrdata = new IngredientData(new SqlServerDataAccess());

            Ingredient foundingr = ingrdata.GetIngredient(IngredientEntry.Text.ToLower());
            if (foundingr == null)
            {
                await DisplayAlert("Error", "The specified ingredient was not found", "OK");
                return;
            }

            List<IngredientItem> pantryingredients = ingrdata.GetIngredientsFromPantry(SqlHelper.UserID);

            if (pantryingredients.Exists(x => x.ID == foundingr.IngredientID))
            {
                await DisplayAlert("Error", "The specified ingredient is already in your pantry", "OK");
                return;
            }

            _list.Add(new IngredientItem(foundingr, 1.0f, Measurements.Serving));
            ingrdata.AddIngredientToPantry(SqlHelper.UserID, foundingr);
        }

        private async void RemoveButton_Clicked(object sender, EventArgs e)
        {
            IngredientData ingrdata = new IngredientData(new SqlServerDataAccess());
            Ingredient foundingr = ingrdata.GetIngredient(IngredientEntry.Text.ToLower());

            if (foundingr == null)
            {
                await DisplayAlert("Error", "That ingredient could not be found", "OK");
                return;
            }

            if (ingrdata.RemoveIngredientFromPantry(SqlHelper.UserID, foundingr) == 0)
            {
                await DisplayAlert("Error", "That ingredient is not currently in your pantry", "OK");
                return;
            }

            //Should never throw exception because of the above check
            IngredientItem item = _list.ListView.Single(x => x.ID == foundingr.IngredientID);
            _list.ListView.Remove(item);
        }

        private void QuantityChangeClicked(object sender, EventArgs e)
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
                
                ingrdata.UpdatePantryIngredientQuantity(SqlHelper.UserID, ob.ID, newquant);
                //Replace the item in the list with a duplicate that has the changed quantity
                int index = _list.ListView.IndexOf(ob);
                _list.ListView.Remove(ob);
                if (newquant > 0)
                    _list.ListView.Insert(index, new IngredientItem(ob.Ingredient, newquant, Measurements.Serving));
            }
            else
                DisplayAlert("Error", "CommandParameter was null", "OK");
        }
    }
}