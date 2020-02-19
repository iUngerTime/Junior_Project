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
    }
}
