using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipeAPI;
using PantryAid.Core.Models;
using PantryAid.Core.Interfaces;
using SpoonacularAPI;
using Database_Helpers;

namespace PantryAid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PantryRecipePage : ContentPage
    {
        ListViewModel<SpoonacularAPI.SpoonacularAPI.ComplexResult> _list = new ListViewModel<SpoonacularAPI.SpoonacularAPI.ComplexResult>();
        int _offset = 0;
        int _recipesPerPage = 5;
        public PantryRecipePage()
        {
            InitializeComponent();
            this.BindingContext = _list;
        }

        void DoSearch()
        {
            _list.ListView.Clear();
            List<string> ingredients = new List<string>();
            //List<IngredientItem> ingredientItems;

            iIngredientData ingrdata = new IngredientData(new SqlServerDataAccess());
            ingredientItems = ingrdata.GetIngredientsFromPantry(SqlHelper.UserID);

            foreach (IngredientItem ii in ingredientItems)
            {
                ingredients.Add(ii.Name);
            }

            SpoonacularAPI.SpoonacularAPI api = SpoonacularAPI.SpoonacularAPI.GetInstance();
            SpoonacularAPI.SpoonacularAPI.Recipe_Complex complex = api.FindComplexRecipe("", _offset, _recipesPerPage, "", null, "", null, ingredients);

            foreach (SpoonacularAPI.SpoonacularAPI.ComplexResult r in complex.results)
            {
                _list.Add(r);
            }
        }

        private void Find_Pressed(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void Recipe_Tapped(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            Navigation.PushModalAsync(new RecipePage(Convert.ToInt32(l.Text)));
        }

        private void PrevButton_Clicked(object sender, EventArgs e)
        {
            if (_offset >= 5)
                _offset -= _recipesPerPage;
            DoSearch();
        }

        private void NextButton_Clicked(object sender, EventArgs e)
        {
            _offset += _recipesPerPage;
            DoSearch();
        }
    }
}