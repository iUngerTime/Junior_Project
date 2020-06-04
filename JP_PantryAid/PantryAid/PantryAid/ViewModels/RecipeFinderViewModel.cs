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
using PantryAid.OfficialViews.Recipe_Finder;

namespace PantryAid.ViewModels
{
    //TODO: pass in some way to control whether it searches online or locally for testing
    public class RecipeFinderViewModel : INotifyPropertyChanged
    {

        public ListViewModel<Recipe_Short> _list = new ListViewModel<Recipe_Short>();
        public event PropertyChangedEventHandler PropertyChanged;
        int _offset;
        int _recipesPerPage = 10;
        public INavigation navigation { get; set; }
        private iUserDataRepo _userDatabaseAccess;

        #region INotify Getters/Setters

        private Search_State _state = Search_State.None;
        public Search_State State
        {
            get { return _state; }
        }
        public enum Search_State
        {
            None = 0, //no search has been done
            Normal, //search by name
            Complex //complex search
        }
        //this state keeps track of whether the current search's state'

        private bool _showReadyInMin;
        public bool ShowReadyInMin
        {
            get { return _showReadyInMin; }
            set
            {
                _showReadyInMin = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ShowReadyInMin"));
            }
        }

        public RecipeFinderViewModel(INavigation nav, iUserDataRepo databaseAccess)
        {
            //Navigation and command binding
            navigation = nav;


            //injection of database access
            _userDatabaseAccess = databaseAccess;
        }

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
                PropertyChanged(this, new PropertyChangedEventArgs("BG_Opacity"));
            }
        }
        #endregion

        #region Ctor, OnAppear()
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

        public void OnAppear()
        {
            if (Preferences.Get("Images", false) == false)
                BG_Opacity = 0;
            else
                BG_Opacity = 100;
        }
        #endregion

        #region Search Functions

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
                if (list[i].imageUrls == null)
                    list[i].imageUrls = new List<string>() { "" };
                list[i].servings = 255; //using servings for opacity
                _list.Add(list[i]);
            }

            _state = Search_State.Normal;
            return _list;
        }

        public ListViewModel<Recipe_Short> ComplexSearch(string recipeSearch)
        {
            _list.ListView.Clear();
            _currentSearch = recipeSearch;

            SpoonacularAPI.SpoonacularAPI api = SpoonacularAPI.SpoonacularAPI.GetInstance();
            SpoonacularAPI.SpoonacularAPI.ComplexParameters param = new SpoonacularAPI.SpoonacularAPI.ComplexParameters();
            //set up the default values for the complex search
            param = api.SetUpComplexParameters();

            param.query = _currentSearch;
            param.number = _recipesPerPage;
            param.offset = _offset;
            //TODO: Add filters here!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            List<string> allergens = new List<string>();
            foreach (var allegeryItem in SqlServerDataAccess.CurrentUser.Allergies)
            {
                allergens.Add(allegeryItem.ToString());
            }
            param.intolerances = allergens;


            //get the results of the search
            SpoonacularAPI.SpoonacularAPI.Recipe_Complex_Results result =
                api.FindComplexRecipe(param);
            //IEnumerable<ComplexResult> list = result.results;

            if (result != null && result.results != null)
            {
                List<Recipe_Short> list = new List<Recipe_Short>();
                //Converting the complex results to short results via constructors
                //I tries inheritance but it didn't work
                //these two do however have implicit conversions
                foreach (ComplexResult res in result.results)
                {
                    
                    list.Add(new Recipe_Short(res));
                }

                for (int i = 0; i < list.Count; i++)
                {
                    //cover nulls
                    if (list[i].image == null)
                        list[i].image = "";
                    //list[i].image = "https://spoonacular.com/recipeImages/" + list[i].image;
                    if (list[i].author == null)
                        list[i].author = "";
                    if (list[i].imageUrls == null)
                        list[i].imageUrls = new List<string>() { "" };
                    list[i].servings = 0; //using servings for opacity
                    _list.Add(list[i]);
                }
                _state = Search_State.Complex;

            }
            else
            {
                //clear the last search
                _list.ListView.Clear();
                //_state = Search_State.None; //set this so that is shows no results
            }
            return _list;
        }
        #endregion



        #region Page Navigation Functions
        public void NextPage()
        {
            //if (_list.ListView.Count > 0 && CurrentSearch.Length > 0)
            if (_state == Search_State.Normal
                && _list.ListView.Count > 0 && CurrentSearch.Length > 0)
            {
                _offset += _recipesPerPage;
                SearchByName(CurrentSearch);
            }
            else if (_state == Search_State.Complex
                     && _list.ListView.Count > 0 && CurrentSearch.Length > 0)
            {
                _offset += _recipesPerPage;
                ComplexSearch(CurrentSearch);
            }


        }
        public void PrevPage()
        {
            //if (_list.ListView.Count > 0 && CurrentSearch.Length > 0)
            if (_state == Search_State.Normal
               && _list.ListView.Count > 0 && CurrentSearch.Length > 0)
            {
                if (_offset >= 5)
                {
                    _offset -= _recipesPerPage;
                    SearchByName(CurrentSearch);
                }

            }
            else if (_state == Search_State.Complex
                    && _list.ListView.Count > 0 && CurrentSearch.Length > 0)
            {
                if (_offset >= 5)
                {
                    _offset -= _recipesPerPage;
                    ComplexSearch(CurrentSearch);
                }
            }
        }

        public void ItemTapped(int index)
        {
            navigation.PushModalAsync(new RecipeDetailsPage(Convert.ToInt32(_list.ListView[index].id)));
            //AddRecipeToPreferedList(_list.ListView[index].id);
        }
        #endregion


        

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
    }
}
