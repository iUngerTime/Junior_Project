using Database_Helpers;
using PantryAid.Core.Interfaces;
using PantryAid.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PantryAid.ViewModels
{
    public class RecipeListManagerViewModel : BaseViewModel
    {
        public INavigation navigation { get; set; }

        private iUserDataRepo _userDatabaseAccess;
        private List<int> _listToManage;

        #region private variable
        int _offset = 0;
        int _recipesPerPage = 10;
        bool _endOfList;
        bool _startOfList;
        bool _notEndOfList;
        bool _notStartOfList;
        private ListViewModel<Recipe_Full> _list = new ListViewModel<Recipe_Full>();
        #endregion

        public RecipeListManagerViewModel(INavigation nav, iUserDataRepo userDataAccess, List<int> ListToManage)
        {
            //Navigation and command binding
            navigation = nav;

            //injection of database access and user's list of recipe id's
            _listToManage = ListToManage;
            _userDatabaseAccess = userDataAccess;

            //Load the list of IDs into the view
            LoadDisplayList();
        }

        private void LoadDisplayList()
        {
            //start at the beginning unless already displayed that
            int startIndex = _offset * _recipesPerPage;
            int numLeft = _listToManage.Count - startIndex;

            if (_offset == 0)
            {
                _startOfList = true;
                _notStartOfList = !_startOfList;
            }
            else
            {
                _startOfList = false;
                _notStartOfList = !_startOfList;
            }

            if(numLeft != 0)
            {
                int endIndex;
                //Set the ending values
                if (numLeft > 10)
                {
                    endIndex = startIndex + 10;
                    _endOfList = false;
                    _notEndOfList = !_endOfList;
                }
                else
                {
                    endIndex = startIndex + numLeft;
                    _endOfList = true;
                    _notEndOfList = !_endOfList;
                }

                //Set the display list
                _list.ListView.Clear();
                for (int currInd = startIndex; currInd < endIndex; currInd++)
                {
                    SpoonacularAPI.SpoonacularAPI api = SpoonacularAPI.SpoonacularAPI.GetInstance();
                    _list.Add(api.GetRecipeFull(_listToManage[currInd]));
                }
            }
        }

        #region public properties
        public ListViewModel<Recipe_Full> DisplayList
        {
            get { return _list; }
            set
            {
                _list = value;
                NotifyPropertyChanged("DisplayList");
            }
        }
        public bool EndOfList
        {
            get { return _endOfList; }
            set
            {
                _endOfList = value;
                NotifyPropertyChanged("EndOfList");
            }
        }

        public bool StartOfList
        {
            get { return _startOfList; }
            set
            {
                _startOfList = value;
                NotifyPropertyChanged("StartOfList");
            }
        }

        public bool NotEndOfList
        {
            get { return _notEndOfList; }
            set
            {
                _notEndOfList = value;
                NotifyPropertyChanged("NotEndOfList");
            }
        }

        public bool NotStartOfList
        {
            get { return _notStartOfList; }
            set
            {
                _notStartOfList = value;
                NotifyPropertyChanged("NotStartOfList");
            }
        }
        #endregion

        #region public functions
        public void NextPage()
        {
            if(!_endOfList)
            {
                _offset++;

                LoadDisplayList();
            }
        }
        public void PrevPage()
        {
            if (!_startOfList)
            {
                _offset--;

                LoadDisplayList();
            }
        }

        public void ItemTapped(int index)
        {
            navigation.PushModalAsync(new RecipePage(Convert.ToInt32(_list.ListView[index].id)));
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
        #endregion
    }
}
