using PantryAid.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace PantryAid.Core.Interfaces
{
    /// <summary>
    /// Interface for interacting with User data
    /// </summary>
    public interface iIngredientData
    {
        int AddIngredient(Ingredient newIng);
        int RemoveIngredient(Ingredient oldIng);
        Ingredient GetIngredient(Ingredient ing);
        Ingredient GetIngredient(int ID);
        Ingredient GetIngredient(string name);
        List<IngredientItem> GetIngredientsFromPantry(int PantryID);
        int AddIngredientToPantry(int PantryID, int IngredientID, float Quantity = 1.0f);
        int AddIngredientToPantry(int PantryID, Ingredient ingredient, float Quantity = 1.0f);
        int RemoveIngredientFromPantry(int PantryID, int IngredientID);
        int RemoveIngredientFromPantry(int PantryID, Ingredient ingredient);
        int UpdatePantryIngredientQuantity(int PantryID, int IngredientID, float NewQuantity);
        int AddIngredientsFromRecipeShort(Recipe_Short recipe);
        int AddIngredientsFromRecipeFull(Recipe_Full recipe);
    }
}
