using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Database_Helpers;
using PantryAid.Core.Interfaces;
using PantryAid.Core.Models;
using RecipeAPI;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace PantryAid.ViewModels
{
    //TODO: pass in some way to control whether it searches online or locally for testing
    public class RecipeFinderViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        int _offset;
        int _recipesPerPage = 10;
        public INavigation navigation { get; set; }
        private iUserDataRepo _userDatabaseAccess;

        public RecipeFinderViewModel(INavigation nav, iUserDataRepo databaseAccess)
        {
            //Navigation and command binding
            navigation = nav;


            //injection of database access
            _userDatabaseAccess = databaseAccess;
        }

        private ListViewModel<Recipe_Short> _list = new ListViewModel<Recipe_Short>();

        public ListViewModel<Recipe_Short> List
        {
            get { return _list; }
            set
            {
                _list = value;
                PropertyChanged(this, new PropertyChangedEventArgs("List"));
            }
        }
        
        //save the current search for paging
        private string _currentSearch;
        public string CurrentSearch
        {
            get { return _currentSearch; }
            set
            {
                _currentSearch = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CurrentSearch"));
            }

        }
        
        private int bgOpacity;
        public int BG_Opacity
        {
            get { return bgOpacity; }
            set
            {
                bgOpacity = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("BG_Opacity"));
            }
        }
        public INavigation navigation { get; set; }

        public RecipeFinderViewModel(INavigation nav)
        {
            navigation = nav;

            if (Preferences.Get("Images", false) == false)
            {
                BG_Opacity = 0;
            }
            else
            {
                BG_Opacity = 100;
            }
        }

        //searches for short recipes from names
        public ListViewModel<Recipe_Short> SearchByName(string recipeSearch)
        {
            _list.ListView.Clear();
            _currentSearch = recipeSearch;

            IRecipeAPI api = SpoonacularAPI.SpoonacularAPI.GetInstance();

            List<Recipe_Short> list = api.RecipeSearch(recipeSearch, _recipesPerPage, _offset);

            for (int i = 0; i < list.Count; i++)
            {
                //cover nulls
                if (list[i].image == null)
                    list[i].image = "";
                list[i].image = "https://spoonacular.com/recipeImages/" + list[i].image;
                if (list[i].author == null)
                    list[i].author = "";
                if(list[i].imageUrls == null)
                    list[i].imageUrls = new List<string>(){""};
                _list.Add(list[i]);
            }

            return _list;
        }


        public void NextPage()
        {
            _offset += _recipesPerPage;
            SearchByName(CurrentSearch);
        }
        public void PrevPage()
        {
            if (_offset >= 5)
                _offset -= _recipesPerPage;
            SearchByName(CurrentSearch);
        }

        public void ItemTapped(int index)
        {
            navigation.PushModalAsync(new RecipePage(Convert.ToInt32(_list.ListView[index].id)));
            //AddRecipeToPreferedList(_list.ListView[index].id);
        }
        
        public void OnAppear()
        {
            if (Preferences.Get("Images", false) == false)
                BG_Opacity = 0;
            else
                BG_Opacity = 100;
        }

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
    }
}
