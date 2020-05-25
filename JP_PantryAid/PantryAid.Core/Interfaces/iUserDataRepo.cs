using System;
using System.Collections.Generic;
using System.Text;
using PantryAid.Core.Models;

namespace PantryAid.Core.Interfaces
{
    /// <summary>
    /// Defines any functions related to editing/retrieving user info
    /// </summary>
    public interface iUserDataRepo
    {
        User GetUser(string usr);
        User GetUser(int usrid);
        int AddUser(User newUser);
        int EditUserInfo(User currentInfo, User newInfo);
        int DeleteUser(User delUser);
        int AddAlergy(User currUser, Alergens newAlergy);
        int RemoveAlergy(User currUser, Alergens oldAlergy);
        int AddDietPreference(User currUser, DietPreferences newPreference);
        int RemoveDietPreference(User currUser, DietPreferences newPreference);
        int AddFavoriteRecipe(User currUser, int newFavorite);
        int RemoveFavoriteRecipe(User currUser, int nonFavorite);
        int AddDislikedRecipe(User currUser, int newDisliked);
        int RemoveDislikedRecipe(User currUser, int nonDisliked);
    }
}
