using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RecipeAPI;
using PantryAid.Core.Models;

namespace PantryAid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShortRecipePage : ContentPage
    {
        ListViewModel<Recipe_Short> _list = new ListViewModel<Recipe_Short>();
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




            for (int i = 0; i < list.Count; i++)
            {
                list[i].imageUrls[0] = "https://spoonacular.com/recipeImages/" + list[i].imageUrls[0];
                _list.Add(list[i]);
                
            }
           
        }


        private void Recipe_Search_OnCompleted(object sender, EventArgs e)
        {
            DoSearch();
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

        private void Recipe_Tapped(object sender, EventArgs e)
        {
            //Note: This only works for clicking on IDs
            //Label l = (Label)sender;
            //Image i = (Image) sender;
            ImageButton IB;
            
            
            //Navigation.PushModalAsync(new RecipePage(Convert.ToInt32(i)));
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            //ListView view = (ListView) sender;
            //int index = e.ItemIndex;
            //Recipe_Short shrt =;

            Navigation.PushModalAsync(new RecipePage(Convert.ToInt32(_list.ListView[e.ItemIndex].id)));
        }
    }
}