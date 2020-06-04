using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using PantryAid.Core.Models;
using RecipeAPI;
using Xamarin.Forms;

namespace PantryAid.ViewModels
{
    public class RecipeDetailsPageViewModel : INotifyPropertyChanged
    {
        private Recipe_Full _recipeFull;

        public Recipe_Full RecipeFull
        {
            get { return _recipeFull; }
            set
            {
                _recipeFull = value;
                PropertyChanged(this, new PropertyChangedEventArgs("RecipeFull"));
            }
        }

        public INavigation navigation { get; set; }

        public RecipeDetailsPageViewModel(INavigation nav, int recipeId)
        {
            navigation = nav;
            //_recipeFull
            SpoonacularAPI.SpoonacularAPI api = SpoonacularAPI.SpoonacularAPI.GetInstance();
            try
            {
                _recipeFull = api.GetRecipeFull(recipeId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
                //throw;
            }
            //_recipeFull.image = "https://spoonacular.com/recipeImages/" + _recipeFull.image;
        }

        public RecipeDetailsPageViewModel(INavigation nav, Recipe_Full recipe)
        {
            _recipeFull = recipe;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

