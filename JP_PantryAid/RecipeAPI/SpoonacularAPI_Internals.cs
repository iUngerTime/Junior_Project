using System.Collections.Generic;
using PantryAid.Core;
using PantryAid.Core.Models;


namespace SpoonacularAPI
{
    //This file Holds the internal methods and temporary formats from the API. 
    public partial class SpoonacularAPI
    {
        
        /// <summary>
        /// Used to hold the return from the API when searching for a list of Recipe_Short
        /// </summary>
        private class SpoonacularRecipeShortSearchResult
        {
            public List<Recipe_Short> results { get; set; }
            public string baseUri { get; set; }
            public int offset { get; set; }
            public int number { get; set; }
            public int totalResults { get; set; }
            public int processingTimeMs { get; set; }
            public long expires { get; set; }
            public bool isStale { get; set; }
        }
    }
}