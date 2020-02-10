using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PantryAid.Core.Interfaces;
using PantryAid.Core.Models;

namespace Database_Helpers
{
    public class IngredientData : iIngredientData
    {
        public int AddIngredient(Ingredient newIng)
        {
            throw new NotImplementedException();
        }

        public int AddIngredientsFromRecipeFull(Recipe_Full recipe)
        {
            throw new NotImplementedException();
        }

        public int AddIngredientsFromRecipeShort(Recipe_Short recipe)
        {
            throw new NotImplementedException();
        }

        public int GetIngredient(Ingredient ing)
        {
            throw new NotImplementedException();
        }

        public int RemoveIngredient(Ingredient oldIng)
        {
            throw new NotImplementedException();
        }
    }
}
