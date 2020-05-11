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
                PropertyChanged(this, new PropertyChangedEventArgs("IngredientList"));
            }
        }

        private List<IngredientItem> checks = new List<IngredientItem>();
        public List<IngredientItem> Checks
        {
            get { return checks; }
            set
            {
                checks = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Checks"));
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
        public void OnAdd(object sender, string ingrName, string quant, string measure)
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

            if (foundingr == null)
            {
                //DisplayAlert("Error", "The specified ingredient was not found", "OK");
                return;
            }

            List<IngredientItem> pantryingredients = ingrdata.GetIngredientsFromPantry(SqlServerDataAccess.UserID);

            if (pantryingredients != null && pantryingredients.Exists(x => x.ID == foundingr.IngredientID))
            {
                //await DisplayAlert("Error", "The specified ingredient is already in your pantry", "OK");
                return;
            }

            _ingredientList.Add(new IngredientItem(foundingr, quantity, measure));
            ingrdata.AddIngredientToPantry(SqlServerDataAccess.UserID, foundingr.IngredientID, measure, quantity);
        }


        public ICommand RemoveCommand { protected set; get; }
        public void OnRemove()
        {
            //string ingrname = ingrName;// = await DisplayPromptAsync("Remove", "Enter an ingredient name", "OK", "Cancel", "eg. Apple", 500);

            //if (ingrname == null) //User clicked cancel
            //    return;

            IngredientData ingrdata = new IngredientData(new SqlServerDataAccess());
            //Ingredient foundingr = ingrdata.GetIngredient(ingrname.ToLower());

            //IngredientItem item = _ingredientList.ListView.Single(x => x.ID == foundingr.IngredientID);
            foreach (IngredientItem ingr in Checks)
            {
                _ingredientList.ListView.Remove(ingr);
            }
  
        }
        public void OnPlus(Entry QuantEntry)
        {
            QuantEntry.Text = (Convert.ToDouble(QuantEntry.Text) + 1).ToString();
        }

        public void OnMinus(Entry QuantEntry)
        {
            if (Convert.ToDouble(QuantEntry.Text) > 1)
                QuantEntry.Text = (Convert.ToDouble(QuantEntry.Text) - 1).ToString();
        }
        public void QuantityChanged(object sender)
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
        public async void OnChecked(CheckBox sender, IngredientItem ingr, Frame popup)
        {
            if (sender.IsChecked)
                Checks.Add(ingr);
            else
                Checks.Remove(ingr);

            //If the popup isn't there and there's at least one item checked
            if (!popup.IsVisible && Checks.Count > 0)
            {
                popup.IsVisible = true;
                popup.AnchorX = 1;
                popup.AnchorY = 1;

                Animation scaleAnimation = new Animation(f => popup.Scale = f, 0.5, 1, Easing.SinInOut);
                Animation fadeAnimation = new Animation(f => popup.Opacity = f, 0.2, 1, Easing.SinInOut);

                scaleAnimation.Commit(popup, "popupScaleAnimation", 25, 25);
                fadeAnimation.Commit(popup, "popupFadeAnimation", 25, 50);
            }
            else if (Checks.Count < 1)
            {
                RemovePopup(popup);
            }
        }
        public async void RemovePopup(Frame popup)
        {
            popup.IsVisible = false;
            await Task.WhenAny<bool>
                (
                popup.FadeTo(0, 25, Easing.SinInOut)
                );
        }
    }
}
