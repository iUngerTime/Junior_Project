using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PantryAid.Core.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PantryAid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchByIngredients : ContentPage
    {
        public SearchByIngredients()
        {
            InitializeComponent();
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
                recipeBy = api.FindRecipeShortByIngredients(tempquery, 1, true, 2, true)[0];
                //recipeFull = SpoonacularAPI.SpoonacularAPI.GetInstance().GetRecipeFull(recipeShort);

                SetLabels(recipeBy);
            }
        }

        private void SetLabels(SpoonacularAPI.SpoonacularAPI.RecipeByIngredient recipeBy)
        {
            L_Instructions.Text = recipeBy.missedIngredientCount.ToString();
            L_RecipeName.Text = recipeBy.title;



            StringBuilder insts = new StringBuilder();
            if (recipeBy.missedIngredients != null)
            {
                foreach (var variable in recipeBy.missedIngredients)
                {
                    insts.Append("* ")
                        .Append(variable.name)
                        .Append("  ")
                        .Append(variable.amount)
                        .Append(" ");
                    if (variable.unit.Length > 0)
                        insts.Append(variable.unit);
                    else if (variable.unitLong.Length > 0)
                        insts.Append(variable.unitLong);
                    else if(variable.unitShort.Length > 0)
                        insts.Append(variable.unitShort);
                    insts.Append("<br />"); //because it's html not ascii

                    //if (VARIABLE.Measurements != null)
                    //    .Append(VARIABLE.Measurements.us.amount)
                    //    .Append(" ").Append(VARIABLE.Measurements.us.unitShort)
                }

                L_Ingredients.Text = insts.ToString();
            }


            //resultLabel1.Text = recipeFull.title;
            //resultLabel2.Text = recipeFull.instructions;
        }


        public void Recipe_Search_OnCompleted(object sender, EventArgs e)
        {
            this.Query();
        }
    }
}