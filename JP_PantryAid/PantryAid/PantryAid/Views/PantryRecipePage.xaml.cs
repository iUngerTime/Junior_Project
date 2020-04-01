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
        }

        private void Find_Pressed(object sender, EventArgs e)
        {//Need IngredientData to finish
            _list.ListView.Clear();
            List<string> ingredients = new List<string>();
            //List<IngredientItem> ingredientItems;

            SpoonacularAPI.SpoonacularAPI api = SpoonacularAPI.SpoonacularAPI.GetInstance();

            SpoonacularAPI.SpoonacularAPI.Recipe_Complex complex = api.FindComplexRecipe("", _offset, _recipesPerPage, "", null, "", null);

            foreach (SpoonacularAPI.SpoonacularAPI.ComplexResult r in complex.results)
            {
                _list.Add(r);
            }
        }
    }
}