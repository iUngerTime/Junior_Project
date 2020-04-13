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
        private ListViewModel<IngredientItem> _ingredientList;
        public ListViewModel<IngredientItem> ingredientList
        {
            get { return _ingredientList; }
            set
            {
                _ingredientList = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Ingredient"));
            }
        }


        // Commands
        public ICommand AddCommand { protected set; get; }
        public void OnAdd()
        {
        }

        public ICommand RemoveCommand { protected set; get; }
        public async void OnRemove()
        {
            string ingrname = "";// = await DisplayPromptAsync("Remove", "Enter an ingredient name", "OK", "Cancel", "eg. Apple", 500);

            if (ingrname == null) //User clicked cancel
                return;

            IngredientData ingrdata = new IngredientData(new SqlServerDataAccess());
            Ingredient foundingr = ingrdata.GetIngredient(ingrname.ToLower());

            if (foundingr == null)
            {
                //await DisplayAlert("Error", "That ingredient could not be found", "OK");
                return;
            }

            if (ingrdata.RemoveIngredientFromPantry(SqlServerDataAccess.UserID, foundingr) == 0)
            {
                //await DisplayAlert("Error", "That ingredient is not currently in your pantry", "OK");
                return;
            }

            //Should never throw exception because of the above check
            IngredientItem item = _ingredientList.ListView.Single(x => x.ID == foundingr.IngredientID);
            _ingredientList.ListView.Remove(item);
        }

        /// Authenticate an ingedient against a SQL database
        /// returns true is ingredient was authenticated, false if not
        
        private bool AuthenticateIngredient()
        {
            bool auth = false;

            //Ingredient ing = _ingredientDatabaseAccess.GetIngredient(_ingredientItem.ToLower());

            //auth = (ing == null ? false : true);

            if (auth)
            {

            }

            return auth;
        }
    }
}
