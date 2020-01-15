using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PantryAid.Core.Models;

namespace RecipeAPI
{
    /// <summary>
    /// Interface for our recipe API.  Whichever one we end up using.
    /// </summary>
    public interface IRecipeAPI
    {
        /// <summary>
        /// Searches the API and returns a list of Recipe_Short
        /// </summary>
        /// <param name="query"></param>
        /// <param name="maxResults">Maximum number of recipes to return.  Used to save API queries</param>
        /// <param name="offset">Used to control how many recipes to skip.  Useful for multiple pages of recipes</param>
        /// <returns></returns>
        List<Recipe_Short> RecipeSearch(string query, int maxResults = 5, int offset = 0);

        /// <summary>
        /// Searches the API to get a Recipe_Full from a Recipe_Short.Id
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        Recipe_Full GetRecipeFull(int recipeId);

        /// <summary>
        /// Takes a Recipe ID and returns a full recipe.  
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        Recipe_Full GetRecipeFull(Recipe_Short recipe);

    }
}
