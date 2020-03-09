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
        //ListViewModel<Recipe_Short> _list = new ListViewModel<Recipe_Short>();
        ListViewModel<SpoonacularAPI.SpoonacularAPI.ComplexResult> _list = new ListViewModel<SpoonacularAPI.SpoonacularAPI.ComplexResult>();
        int _offset;
        int _recipesPerPage;

        //this is a struct with all the possible filters
        private SpoonacularAPI.SpoonacularAPI.ComplexParameters m_filters = new SpoonacularAPI.SpoonacularAPI.ComplexParameters();

        public ShortRecipePage()
        {
            InitializeComponent();

            _offset = 0;
            _recipesPerPage = 5;
            this.BindingContext = _list;
            m_filters = SpoonacularAPI.SpoonacularAPI.SetUpComplexParameters();
        }

        //displays a list of similar recipes
        public ShortRecipePage(string similarID)
        {

            InitializeComponent();

            _offset = 0;
            _recipesPerPage = 5;
            this.BindingContext = _list;
            FindSimilarRecipes(similarID);
            m_filters = SpoonacularAPI.SpoonacularAPI.SetUpComplexParameters();
        }

        /*
        private void DoSearch()
        {
            _list.ListView.Clear();

            IRecipeAPI api = SpoonacularAPI.SpoonacularAPI.GetInstance();

            List<Recipe_Short> list = api.RecipeSearch(Recipe_Search.Text, _recipesPerPage, _offset);

            for (int i = 0; i < list.Count; i++)
            {
                list[i].imageUrls[0] = "https://spoonacular.com/recipeImages/" + list[i].imageUrls[0];
                _list.Add(list[i]);
                
            }
           
        }
        */

        private void DoSearchComplex()
        {
            _list.ListView.Clear();

            SpoonacularAPI.SpoonacularAPI api = SpoonacularAPI.SpoonacularAPI.GetInstance();

            List<SpoonacularAPI.SpoonacularAPI.ComplexResult> list = new List<SpoonacularAPI.SpoonacularAPI.ComplexResult>();
            m_filters.query = Recipe_Search.Text;
            m_filters.offset = _offset;
            m_filters.number = _recipesPerPage;
            list = api.FindComplexRecipe(m_filters).results;

            for (int i = 0; i < list.Count; i++)
            {
                //list[i].image = "https://spoonacular.com/recipeImages/" + list[i].image;
                _list.Add(list[i]);

            }
        }


        private void Recipe_Search_OnCompleted(object sender, EventArgs e)
        {
            //DoSearch();
            DoSearchComplex();
        }

        private void PrevButton_Clicked(object sender, EventArgs e)
        {
            if (_offset >= _recipesPerPage)
                _offset -= _recipesPerPage;
            //DoSearch();
            DoSearchComplex();
        }

        private void NextButton_Clicked(object sender, EventArgs e)
        {
            _offset += _recipesPerPage;
            //DoSearch();
            DoSearchComplex();
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushModalAsync(new RecipePage(Convert.ToInt32(_list.ListView[e.ItemIndex].id)));
        }
        /*
        private void SimilarRecipe_Search_Completed(object sender, EventArgs e)
        {
           FindSimilarRecipes(SimilarRecipe_Search.Text);
        }
        */

        private void FindSimilarRecipes(string id)
        {
            _list.ListView.Clear();

            SpoonacularAPI.SpoonacularAPI api = SpoonacularAPI.SpoonacularAPI.GetInstance();

            List<SpoonacularAPI.SpoonacularAPI.Recipe_Shorter> list = api.FindSimilarRecipes(id, _recipesPerPage);

            for (int i = 0; i < list.Count; i++)
            {
                //if (list[i].imageUrls[0] != null)
                //list[i].imageUrls[0] = "https://spoonacular.com/recipeImages/" + list[i].imageUrls[0];
                list[i].image = "https://spoonacular.com/recipeImages/" + list[i].image;
                /*
                Recipe_Short rs = new Recipe_Short();
                rs.title = list[i].title;
                rs.id = list[i].id;
                rs.readyInMinutes = list[i].readyInMinutes;
                rs.servings = list[i].servings;
                rs.image = list[i].image;
                rs.imageUrls = list[i].imageUrls;
                rs.author = "";
            
                _list.Add(rs);
                */
                SpoonacularAPI.SpoonacularAPI.ComplexResult result = new SpoonacularAPI.SpoonacularAPI.ComplexResult();
                result.title = list[i].title;
                result.id = list[i].id;
                result.image = list[i].image;
                _list.Add(result);
            }
        }
    }
}