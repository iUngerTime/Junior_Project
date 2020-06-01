using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Database_Helpers;
using PantryAid.Core.Interfaces;
using PantryAid.Core.Models;
using RecipeAPI;
using Xamarin.Forms;

namespace PantryAid.ViewModels
{
    public class RecipeDetailsPageViewModel : INotifyPropertyChanged
    {
        private Recipe_Full _recipeFull;
        private iUserDataRepo _userDatabaseAccess;

        bool _liked = false;
        public bool Liked
        {
            get { return _liked; }
            set
            {
                _liked = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Liked"));
            }
        }

        bool _disliked = false;
        public bool DisLiked
        {
            get { return _disliked; }
            set
            {
                _disliked = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Disliked"));
            }
        }

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

        public RecipeDetailsPageViewModel(INavigation nav, iUserDataRepo databaseAccess, int recipeId)
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
            //injection of database access
            _userDatabaseAccess = databaseAccess;
        }


        #region Like Disliked FUnctions
        public void AddRecipeToDislikedList(int index)
        {
            _userDatabaseAccess.AddDislikedRecipe(SqlServerDataAccess.CurrentUser, index);
        }
        public void RemoveRecipeFromDislikedList(int index)
        {
            _userDatabaseAccess.RemoveDislikedRecipe(SqlServerDataAccess.CurrentUser, index);
        }
        public void AddRecipeToPreferedList(int index)
        {
            _userDatabaseAccess.AddFavoriteRecipe(SqlServerDataAccess.CurrentUser, index);
        }
        public void RemoveRecipeFromPreferedList(int index)
        {
            _userDatabaseAccess.RemoveFavoriteRecipe(SqlServerDataAccess.CurrentUser, index);
        }
        #endregion
    

    public event PropertyChangedEventHandler PropertyChanged;
    }
}

