using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RecipeAPI;
using PantryAid.Core.Models;
using SpoonacularAPI;

namespace PantryAid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShortRecipePage : ContentPage
    {
        ListViewModel<SpoonacularAPI.SpoonacularAPI.ComplexResult> _list = new ListViewModel<SpoonacularAPI.SpoonacularAPI.ComplexResult>();
        int _offset;
        int _recipesPerPage;
        public ShortRecipePage()
        {
            InitializeComponent();

            _offset = 0;
            _recipesPerPage = 5;
            this.BindingContext = _list;
        }

        private void DoSearch()
        {
            _list.ListView.Clear();

            IRecipeAPI api = SpoonacularAPI.SpoonacularAPI.GetInstance();

            List<Recipe_Short> list = api.RecipeSearch(Recipe_Search.Text, _recipesPerPage, _offset);

            foreach (Recipe_Short r in list)
            {
               // _list.Add(r);
            }
        }

        private void DoComplexSearch()
        {
            _list.ListView.Clear();
            SpoonacularAPI.SpoonacularAPI api = SpoonacularAPI.SpoonacularAPI.GetInstance();

            SpoonacularAPI.SpoonacularAPI.Recipe_Complex complex = api.FindComplexRecipe(Recipe_Search.Text, _offset, _recipesPerPage);

            foreach (SpoonacularAPI.SpoonacularAPI.ComplexResult r in complex.results)
            {
                _list.Add(r);
            }
        }

        private void Recipe_Search_OnCompleted(object sender, EventArgs e)
        {
            DoComplexSearch();
        }

        private void PrevButton_Clicked(object sender, EventArgs e)
        {
            if (_offset >= 5)
                _offset -= _recipesPerPage;
            DoComplexSearch();
        }

        private void NextButton_Clicked(object sender, EventArgs e)
        {
            _offset += _recipesPerPage;
            DoComplexSearch();
        }

        private void Recipe_Tapped(object sender, EventArgs e)
        {
            //Note: This only works for clicking on IDs
            Label l = (Label)sender;
            
            Navigation.PushModalAsync(new RecipePage(Convert.ToInt32(l.Text)));
        }
    }
}