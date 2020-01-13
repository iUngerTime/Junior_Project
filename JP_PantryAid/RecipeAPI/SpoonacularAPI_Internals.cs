using System.Collections.Generic;
using PantryAid.Core;



namespace SpoonacularAPI

{
    /// <summary>
    /// 
    /// </summary>
    public partial class SpoonacularAPI
    { 

        private class SpoonacularRecipeSearchResult
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            public int id { get; set; }
            public string title { get; set; }
            public int readyInMinutes { get; set; }
            public int servings { get; set; }
            public string image { get; set; }
            public List<string> imageUrls { get; set; }
            public string author { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        //Used to hold the return from the API
        private class SpoonacularRecipeSearchRootObject
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member       
            public List<SpoonacularRecipeSearchResult> results { get; set; }
            public string baseUri { get; set; }
            public int offset { get; set; }
            public int number { get; set; }
            public int totalResults { get; set; }
            public int processingTimeMs { get; set; }
            public long expires { get; set; }
            public bool isStale { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        /////////////////////////////////////////////  Below are Recipe Information classes




        /// <summary>
        /// What the RecipeInformation API returns about the ingredients
        /// </summary>
        public class RecipeInformationExtendedIngredient
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            public string aisle { get; set; }

            public double amount { get; set; }
            public string consitency { get; set; }
            public int id { get; set; }
            public string image { get; set; }
            public Measures measures { get; set; }
            public List<object> meta { get; set; }
            public List<object> metaInformation { get; set; }
            public string name { get; set; }
            public string original { get; set; }
            public string originalName { get; set; }
            public string originalString { get; set; }
            public string unit { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        /// <summary>
        /// Yes this is a thing.  Somehow...
        /// </summary>
        public class RecipeInformationWinePairing
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            public List<object> pairedWines { get; set; }
            public string pairingText { get; set; }
            public List<object> productMatches { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        /// <summary>
        /// What the RecipeInformation API returns about a recipe
        /// </summary>
        public class RecipeInformationRootObject
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            public int id { get; set; }
            public string title { get; set; }
            public string image { get; set; }
            public string imageType { get; set; }
            public int servings { get; set; }
            public int readyInMinutes { get; set; }
            public string license { get; set; }
            public string sourceName { get; set; }
            public string sourceUrl { get; set; }
            public string spoonacularSourceUrl { get; set; }
            public int aggregateLikes { get; set; }
            public double healthScore { get; set; }
            public double spoonacularScore { get; set; }
            public double pricePerServing { get; set; }
            public List<object> analyzedInstructions { get; set; }
            public bool cheap { get; set; }
            public string creditsText { get; set; }
            public List<object> cuisines { get; set; }
            public bool dairyFree { get; set; }
            public List<object> diets { get; set; }
            public string gaps { get; set; }
            public bool glutenFree { get; set; }
            public string instructions { get; set; }
            public bool ketogenic { get; set; }
            public bool lowFodmap { get; set; }
            public List<object> occasions { get; set; }
            public bool sustainable { get; set; }
            public bool vegan { get; set; }
            public bool vegetarian { get; set; }
            public bool veryHealthy { get; set; }
            public bool veryPopular { get; set; }
            public bool whole30 { get; set; }
            public int weightWatcherSmartPoints { get; set; }
            public List<string> dishTypes { get; set; }
            public List<RecipeInformationExtendedIngredient> extendedIngredients { get; set; }
            public RecipeInformationWinePairing winePairing { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }
    }
}