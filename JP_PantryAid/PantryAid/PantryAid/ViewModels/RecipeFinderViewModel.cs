using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using PantryAid.Core.Models;
using RecipeAPI;
using Xamarin.Forms;

namespace PantryAid.ViewModels
{
    //TODO: pass in some way to control whether it searches online or locally for testing
    public class RecipeFinderViewModel : INotifyPropertyChanged
    {
        

        public ListViewModel<Recipe_Short> _list = new ListViewModel<Recipe_Short>();

        
        public ListViewModel<Recipe_Short> List
        {
            get { return _list; }
            set
            {
                _list = value;
                PropertyChanged(this, new PropertyChangedEventArgs("List"));
            }

        }
        
        int _offset;
        int _recipesPerPage = 10;
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
        public INavigation navigation { get; set; }

        public RecipeFinderViewModel(INavigation nav)
        {
            navigation = nav;
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
                list[i].imageUrls[0] = "https://spoonacular.com/recipeImages/" + list[i].imageUrls[0];
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



        public event PropertyChangedEventHandler PropertyChanged;
    }
}
