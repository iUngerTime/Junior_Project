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
        int AddAlergy(User currUser, Ingredient newAlergy);
        int RemoveAlergy(User currUser, Ingredient alergy);
        int AddFavoriteRecipe(User currUser, Recipe_Short newFavorite);
        int RemoveFavoriteRecipe(User currUser, Recipe_Short nonFavorite);
        int AddDislikedRecipe(User currUser, Recipe_Short newDisliked);
        int RemoveDislikedRecipe(User currUser, Recipe_Short nonDisliked);
    }
}
