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

        /// <summary>
        /// 
        /// </summary>
        public class MissedIngredient
        {
            public int id { get; set; }
            public double amount { get; set; }
            public string unit { get; set; }
            public string unitLong { get; set; }
            public string unitShort { get; set; }
            public string aisle { get; set; }
            public string name { get; set; }
            public string original { get; set; }
            public string originalString { get; set; }
            public string originalName { get; set; }
            public List<object> metaInformation { get; set; }
            public List<object> meta { get; set; }
            public string image { get; set; }
        }

        /// <summary>
        /// This is the return from the recipe search by ingredient
        /// It is irritating NOT the same as a RecipeShort although it's very close
        /// </summary>
        public class RecipeByIngredient
        {
            public int id { get; set; }
            public string title { get; set; }
            public string image { get; set; }
            public string imageType { get; set; }
            public int usedIngredientCount { get; set; }
            public int missedIngredientCount { get; set; }
            public List<MissedIngredient> missedIngredients { get; set; }
            public List<object> usedIngredients { get; set; }
            public List<object> unusedIngredients { get; set; }
            public int likes { get; set; }
        }

    }
}