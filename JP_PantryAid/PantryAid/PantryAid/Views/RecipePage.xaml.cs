using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PantryAid.Core.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PantryAid
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipePage : ContentPage
    {
        private string _imageUrl;

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = "https://spoonacular.com/recipeImages/" + value; }
        }

        public RecipePage()
        {
            InitializeComponent();
        }

        //Called from ShortRecipePage
        public RecipePage(int RecipeID)
        {
            InitializeComponent();
            SpoonacularAPI.SpoonacularAPI api = SpoonacularAPI.SpoonacularAPI.GetInstance();
            Recipe_Search.Text = RecipeID.ToString();

            Recipe_Full RF = api.GetRecipeFull(RecipeID);
            SetLabels(RF);
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            Query();
        }

        //Query and update the labels
        private void Query()
        {
            SpoonacularAPI.SpoonacularAPI api = SpoonacularAPI.SpoonacularAPI.GetInstance();
            string query = Recipe_Search.Text;
            Recipe_Short recipeShort;
            Recipe_Full recipeFull = new Recipe_Full();
            if (query.Length > 0)
            {
                recipeShort = api.RecipeSearch(query, 1)[0];
                recipeFull = SpoonacularAPI.SpoonacularAPI.GetInstance().GetRecipeFull(recipeShort);

                SetLabels(recipeFull);
            }
        }

        private void SetLabels(Recipe_Full recipeFull)
        {
            Image1.Source = recipeFull.image;
            L_Instructions.Text = recipeFull.instructions;
            L_RecipeName.Text = recipeFull.title;

            StringBuilder insts = new StringBuilder();
            if (recipeFull.extendedIngredients != null)
            {
                foreach (var VARIABLE in recipeFull.extendedIngredients)
                {
                    insts.Append("* ")
                        .Append(VARIABLE.name)
                        .Append("  ")
                        .Append(VARIABLE.amount)
                        .Append(" ")
                        .Append(VARIABLE.unit)
                        .Append("<br />"); //because it's html not ascii
                }

                L_Ingredients.Text = insts.ToString();
            }
        }

        //When user hits enter
        private void Recipe_Search_OnCompleted(object sender, EventArgs e)
        {
            Query();
        }
    }
}