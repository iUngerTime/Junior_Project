using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PantryAid.Core.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SpoonacularAPI;

namespace PantryAid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchByIngredients : ContentPage
    {
        ListViewModel<SpoonacularAPI.SpoonacularAPI.RecipeByIngredient> _list
            = new ListViewModel<SpoonacularAPI.SpoonacularAPI.RecipeByIngredient>();

        public SearchByIngredients()
        {
            this.BindingContext = _list;
            InitializeComponent();
        }

        public SearchByIngredients(List<string> ingreList)
        {
            this.BindingContext = _list;
            SearchForRecipes(ingreList);
            InitializeComponent();
        }

        private void SearchForRecipes(List<string> ingrList)
        {
            SpoonacularAPI.SpoonacularAPI api = SpoonacularAPI.SpoonacularAPI.GetInstance();
            List<SpoonacularAPI.SpoonacularAPI.RecipeByIngredient> recipeBy;
            //Recipe_Full recipeFull = new Recipe_Full();
            if (ingrList.Count > 0)
            {
                recipeBy = api.FindRecipeByIngredients(ingrList, 5, true, 2, true);

                //SetLabels(recipeBy);
                _list.ListView.Clear();
                foreach (SpoonacularAPI.SpoonacularAPI.RecipeByIngredient r in recipeBy)
                {
                    _list.Add(r);
                }
            }
        }

        private void Query()
        {
            SpoonacularAPI.SpoonacularAPI api = SpoonacularAPI.SpoonacularAPI.GetInstance();
            string query = Recipe_Search.Text;
            List<string> tempquery = new List<string>();
            tempquery.Add(query);
            SpoonacularAPI.SpoonacularAPI.RecipeByIngredient recipeBy;
            Recipe_Full recipeFull = new Recipe_Full();
            if (query.Length > 0)
            {
                recipeBy = api.FindRecipeByIngredients(tempquery, 1, true, 2, true)[0];
            }
        }

        public void Recipe_Search_OnCompleted(object sender, EventArgs e)
        {
            //this.Query();
            //TODO: add validation checker for the user input
            if(Recipe_Search.Text.Length > 0)
                SearchForRecipes(new List<string>() { Recipe_Search.Text });
        }

        private void Recipe_Tapped(object sender, EventArgs e)
        {
            //Note: This only works for clicking on IDs
            Label l = (Label)sender;

            Navigation.PushModalAsync(new RecipePage(Convert.ToInt32(l.Text)));
        }
    }
}