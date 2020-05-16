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

        public async void OnAdd(object sender, string ingrName, string quant, string measure)
        {
            string ingrname = ingrName;

            if (ingrname == null) //User clicked cancel
                return;

            double quantity = 0.0f;
            bool validquantity = false;
            while (!validquantity)
            {
                string strquant = quant;

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
                // Wrong way to display an alert, Breaks MVVM model.   
                //await Application.Current.MainPage.DisplayAlert("Error", "The specified ingredient was not found", "OK");
                return;
            }

            List<IngredientItem> pantryingredients = ingrdata.GetIngredientsFromPantry(SqlServerDataAccess.UserID);

            if (pantryingredients.Exists(x => x.ID == foundingr.IngredientID))
            {
                //await DisplayAlert("Error", "The specified ingredient is already in your pantry", "OK");
                return;
            }

            _ingredientList.Add(new IngredientItem(foundingr, quantity, measure));
            ingrdata.AddIngredientToPantry(SqlServerDataAccess.UserID, foundingr.IngredientID, measure, quantity);
        }

        public void OnRemove()
        {
            IngredientData ingrdata = new IngredientData(new SqlServerDataAccess());
            List<IngredientItem> pantryingredients = ingrdata.GetIngredientsFromPantry(SqlServerDataAccess.UserID);
            foreach (IngredientItem ingr in Checks)
            {
                _ingredientList.ListView.Remove(ingr);
                ingrdata.RemoveIngredientFromPantry(SqlServerDataAccess.UserID, ingr.ID);
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
        public void QuantityChanged(object sender, Entry QuantEntry)
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
                    QuantEntry.Text = (Convert.ToDouble(QuantEntry.Text) + 1).ToString();
                else if (b.Text == "-")
                     if (Convert.ToDouble(QuantEntry.Text) > 1)
                        QuantEntry.Text = (Convert.ToDouble(QuantEntry.Text) - 1).ToString();

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
