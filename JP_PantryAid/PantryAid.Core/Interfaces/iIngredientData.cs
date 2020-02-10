using PantryAid.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PantryAid.Core.Interfaces
{
    /// <summary>
    /// Interface for interacting with User data
    /// </summary>
    public interface iIngredientData
    {
        int AddIngredient(Ingredient newIng);
        int RemoveIngredient(Ingredient oldIng);
        int GetIngredient(Ingredient ing);
        int AddIngredientsFromRecipeShort(Recipe_Short recipe);
        int AddIngredientsFromRecipeFull(Recipe_Full recipe);
    }
}
