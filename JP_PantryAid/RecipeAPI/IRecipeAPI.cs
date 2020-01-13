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
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="maxResults">Maximum number of recipes to return.  Used to save API queries</param>
        /// <param name="offset">Used to control how many recipes to skip.  Useful for multiple pages of recipes</param>
        /// <returns></returns>
        List<Recipe> RecipeSearch(string query, int maxResults = 5, int offset = 0);




    }
}
