using PantryAid.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PantryAid.Core.Utilities
{
    public interface iSqlServerDataAccess
    {
        int ExecuteQuery_NoReturnType(string sql);
        List<IngredientItem> ExecuteQuery_GetPantry(string sql);
        Ingredient ExecuteQuery_SingleIngredientItem(string sql);
        User ExecuteQuery_SingleUser(string sql);
        void ExecuteQuery_GetUserAlergies(User user);
        void ExecuteQuery_GetUserDietaryPreferences(User user);
        void ExecuteQuery_GetUserDislikedRecipes(User user);
        void ExecuteQuery_GetUserFavoriteRecipes(User user);
    }
}
