using System;
using System.Collections.Generic;
using System.Text;

namespace PantryAid.Core.Models
{

    /// <summary>
    /// A full recipe with instructions, ingredients, image, ect.
    /// </summary>
    public class Recipe_Full : Recipe_Short
    {
        /// <summary>
        /// The file extension for the image e.g. jpg or png
        /// </summary>
        public string imageType { get; set; }
        /// <summary>
        /// The legal license of the recipe
        /// </summary>
        public string license { get; set; }
        /// <summary>
        /// Where the recipe came from.  
        /// </summary>
        public string sourceName { get; set; }
        /// <summary>
        /// A url for the source of the recipe
        /// </summary>
        public string sourceUrl { get; set; }
        /// <summary>
        /// A link to the recipe on Spoonacular
        /// </summary>
        public string spoonacularSourceUrl { get; set; }
        /// <summary>
        /// Some Spoonacular like metric
        /// </summary>
        public int aggregateLikes { get; set; }
        /// <summary>
        /// Some Spoonacular health metric
        /// </summary>
        public double healthScore { get; set; }
        /// <summary>
        /// Some Spoonacular scoring metric
        /// </summary>
        public double spoonacularScore { get; set; }
        /// <summary>
        /// Price per serving.  Useful for budget meals
        /// </summary>
        public double pricePerServing { get; set; }
        /// <summary>
        /// List of instructions.  I believe this is the original instructions that Spoonacular scraped from a website.   believe it is a list of strings but have not verified that.
        /// Use the instructions method instead.
        /// </summary>
        public List<object> analyzedInstructions { get; set; }
        /// <summary>
        /// A cheap flag.  No clue what the requirements are to be qualified as "cheap"
        /// </summary>
        public bool cheap { get; set; }
        /// <summary>
        /// Credit for the recipe.  May be the same as the sourceName
        /// </summary>
        public string creditsText { get; set; }
        /// <summary>
        /// List of cuisines as strings.  No clue what possible cuisines there are
        /// </summary>
        public List<object> cuisines { get; set; }
        /// <summary>
        /// Flag for dairy free
        /// </summary>
        public bool dairyFree { get; set; }
        /// <summary>
        /// List of diets associated with this recipe.
        /// </summary>
        public List<object> diets { get; set; }
        /// <summary>
        /// Literally no idea what this means
        /// </summary>
        public string gaps { get; set; }
        /// <summary>
        /// Flag for gluten free
        /// </summary>
        public bool glutenFree { get; set; }
        /// <summary>
        /// The instructions for the recipe.  NOTE: these have HTML formatting flags in them which is actually good if we can figure out how to use them when we display the recipes.
        /// </summary>
        public string instructions { get; set; }
        /// <summary>
        /// Flag for ketogenic.  Not sure why this isn't in the diet list.
        /// </summary>
        public bool ketogenic { get; set; }
        /// <summary>
        /// Flag for some kind of diet
        /// </summary>
        public bool lowFodmap { get; set; }
        /// <summary>
        /// List of occasions which the recipe is suitable for
        /// </summary>
        public List<object> occasions { get; set; }
        /// <summary>
        /// Flag for some sustainable metric
        /// </summary>
        public bool sustainable { get; set; }
        /// <summary>
        /// Flag for vegan
        /// </summary>
        public bool vegan { get; set; }
        /// <summary>
        /// Flag for vegetarian
        /// </summary>
        public bool vegetarian { get; set; }
        /// <summary>
        /// Flag for some metric of healthiness
        /// </summary>
        public bool veryHealthy { get; set; }
        /// <summary>
        /// Flag for some popularity metric
        /// </summary>
        public bool veryPopular { get; set; }
        /// <summary>
        /// Flag for some diet?
        /// </summary>
        public bool whole30 { get; set; }
        /// <summary>
        /// Weight watchers points
        /// </summary>
        public int weightWatcherSmartPoints { get; set; }
        /// <summary>
        /// List of dish types. e.g. lunch, main course, dinner, breakfast
        /// </summary>
        public List<string> dishTypes { get; set; }
        /// <summary>
        /// List of ingredients
        /// </summary>
        public List<RecipeFullIngredient> extendedIngredients { get; set; }
        /// <summary>
        /// This is a thing apparently
        /// </summary>
        public RecipeFullWinePairing winePairing { get; set; }






        public class RecipeFullWinePairing
        {
            public List<object> pairedWines { get; set; }
            public string pairingText { get; set; }
            public List<object> productMatches { get; set; }
        }


        public class RecipeFullIngredient
        {
            public string aisle { get; set; }
            public double amount { get; set; }
            public string consitency { get; set; }
            public int id { get; set; }
            public string image { get; set; }
            public Core.Measurements Measurements { get; set; }
            public List<object> meta { get; set; }
            public List<object> metaInformation { get; set; }
            public string name { get; set; }
            public string original { get; set; }
            public string originalName { get; set; }
            public string originalString { get; set; }
            public string unit { get; set; }
        }



        //Prevent null objects
        public Recipe_Full()
        {
            imageUrls = new List<string>();
            analyzedInstructions = new List<object>();
            cuisines = new List<object>();
            diets = new List<object>();
            occasions = new List<object>();
            dishTypes = new List<string>();
            extendedIngredients = new List<RecipeFullIngredient>();
        }

        public Recipe_Full(Recipe_Full R)
        {
            id = R.id;
            title = R.title;
            readyInMinutes = R.readyInMinutes;
            servings = R.servings;
            image = R.image;
            imageUrls = R.imageUrls;
            author = R.author;
            imageType = R.imageType;
            license = R.license;
            sourceName = R.sourceName;
            sourceUrl = R.sourceUrl;
            spoonacularSourceUrl = R.spoonacularSourceUrl;
            aggregateLikes = R.aggregateLikes;
            healthScore = R.healthScore;
            spoonacularScore = R.spoonacularScore;
            pricePerServing = R.pricePerServing;
            analyzedInstructions = R.analyzedInstructions;
            cheap = R.cheap;
            creditsText = R.creditsText;
            cuisines = R.cuisines;
            dairyFree = R.dairyFree;
            diets = R.diets;
            gaps = R.gaps;
            glutenFree = R.glutenFree;
            instructions = R.instructions;
            ketogenic = R.ketogenic;
            lowFodmap = R.lowFodmap;
            occasions = R.occasions;
            sustainable = R.sustainable;
            vegan = R.vegan;
            vegetarian = R.vegetarian;
            veryHealthy = R.veryHealthy;
            veryPopular = R.veryPopular;
            whole30 = R.whole30;
            weightWatcherSmartPoints = R.weightWatcherSmartPoints;
            dishTypes = R.dishTypes;
            extendedIngredients = R.extendedIngredients;
            winePairing = R.winePairing;
        }
    }
}
