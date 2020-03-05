using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using PantryAid.Core.Models;
using RecipeAPI;
using RestSharp;
using System.Web;

namespace SpoonacularAPI
{
    /// <summary>
    /// The interface for Spoonacular API
    /// </summary>
    public partial class SpoonacularAPI : IRecipeAPI
    {
        private SpoonacularAPI() { }

        /// <summary>
        /// Singleton GetInstance.  Gets a pointer to an instance
        /// </summary>
        /// <returns></returns>
        public static SpoonacularAPI GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = new SpoonacularAPI();
            }

            return m_instance;
        }

        /// <summary>
        /// This is a list of parameters for a recipe search
        /// </summary>
        /// <remarks>
        /// I am not certain of what cuisines exist.  Probably safe to ignore for now.  maybe stretch goal.
        /// I am also not certain what diet options there are.
        /// I believe intolerance is in terms of ingredients.  Not sure what form or how it identifies them.
        /// offset is the number of recipes to skip.  for example if we display 10 recipes per page then page 2 would have an offset of 10 or the page's index times the number of items per page
        /// number is the number of results to return.
        /// </remarks>
        public struct RecipeSearchParams
        {
            /// <summary>
            /// The search query to be sent.  e.g "Chicken"
            /// </summary>
            public string query;
            /// <summary>
            /// How many items to return
            /// </summary>
            public int number;
            /// <summary>
            /// How many items to offset by.  Used to display multiple pages without querying all the pages at once
            /// </summary>
            public int offset;
            /// <summary>
            /// A filter for cuisine.  e.g "Italian"
            /// </summary>
            public string cuisine;
            /// <summary>
            /// A diet filter
            /// </summary>
            public string diet;
            /// <summary>
            /// Pretty obvious what it does.  No clue how.  Common name?  Ingredient ID?
            /// </summary>
            public string excludeIngredients;
            /// <summary>
            /// Same as excluded ingredients but may work with stuff like Shellfish?
            /// </summary>
            public string intolerances;
            /// <summary>
            /// Some legal thing about displaying recipes.  Should always be true;
            /// </summary>
            public bool limitLicense;
            /// <summary>
            /// Whether the recipes should have instructions.  Should always be true for obvious reasons.
            /// </summary>
            public bool instructionsRequired;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="maxResults">default 5, max 20</param>
        /// <param name="offset"> default 0</param>
        /// <returns>
        /// Returns a List of Recipe models
        /// </returns>
        public List<Recipe_Short> RecipeSearch(string query, int maxResults, int offset)
        {
            RecipeSearchParams param = SetUpParams(query, maxResults, offset);
            SpoonacularRecipeShortSearchResult result = QueryAPI(param);
            return result.results;
            //List<RecipeInformationRootObject> recipeInfoList = ToRecipeInformationList(result);
            //return ToRecipeShortList(recipeInfoList);
        }




        /// <summary>
        /// A Spoonacular specific search.  Is MUCH better than the alternatives
        /// </summary>
        /// <param name="query">The query. The only needed value</param>
        /// <param name="number">The number of results to return. Capped by the MAX_RESULTS value</param>
        /// <param name="offset">Number of items to skip. Leave default unless needed</param>
        /// <param name="cuisine">Leave default unless needed</param>
        /// <param name="diet">Leave default unless needed</param>
        /// <param name="excludeIngredients">Leave default unless needed</param>
        /// <param name="intolerances">Leave default unless needed</param>
        /// <param name="limitLicense">Leave default unless needed</param>
        /// <param name="instructionsRequired">Leave default unless needed</param>
        /// <returns></returns>
        public List<Recipe_Short> RecipeSearch(string query,
            int number = 5,
            int offset = 0,
            string cuisine = "",
            string diet = "",
            string excludeIngredients = "",
            string intolerances = "",
            bool limitLicense = true,
            bool instructionsRequired = true)
        {
            RecipeSearchParams param = SetUpParams(query, number, offset, cuisine, diet, excludeIngredients, intolerances, limitLicense,
                instructionsRequired);
            SpoonacularRecipeShortSearchResult result = QueryAPI(param);
            return result.results;
        }

        public List<Recipe_Shorter> FindSimilarRecipes(string id, int number = 5, bool limitLicense = true)
        {//Broken
            RestClient client = new RestClient(SpoonacularAPI.m_URL);
            RestRequest request = new RestRequest(SpoonacularAPI.m_RecipeInformationURL + id + "/similar", Method.GET);

            request.AddParameter("number", number);
            request.AddParameter("limitLicense", limitLicense);

            request.AddParameter("apiKey", m_APYKey);

            List<Recipe_Shorter> result;
            try
            {
                RestResponse response = client.Execute(request);
                //try to get the data out of the response

                result = JsonConvert.DeserializeObject<List<Recipe_Shorter>>(response.Content);
                return result;
            }
            catch (Exception)
            {
                //add call to exception logger
                //throw;
                return null;
            }
        }

        /// <summary>
        /// Returns a list of RecipeByIngredient. The ingredients appear to be quite fuzzy
        /// </summary>
        /// <param name="ingredients"> a list of ingredients to search with</param>
        /// <param name="number">The maximum number of recipes to return (between 1 and 100). Defaults to 10.</param>
        /// <param name="limitLicense">Whether the recipes should have an open license that allows display with proper attribution. Default to true</param>
        /// <param name="ranking">Whether to maximize used ingredients (1) or minimize missing ingredients (2) first. 2 is best most of the time</param>
        /// <param name="ignorePantry">Whether to ignore typical pantry items, such as water, salt, flo</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<RecipeByIngredient> FindRecipeByIngredients(List<string> ingredients, int number, bool limitLicense = true, int ranking = 2, bool ignorePantry = true)
        {
            RestClient client = new RestClient(SpoonacularAPI.m_URL);
            RestRequest request = new RestRequest(SpoonacularAPI.m_RecipeSearchByIngredientsURL, Method.GET);

            //Set up the query parameters
            if(ingredients.Count < 1)
                throw new Exception("Error no ingredients given");
            string ingreds = ingredients[0];
            for(int i = 1; i < ingredients.Count; ++i) //set up the list of ingredients as a string of comma separated entries
            {
                ingreds += "," + ingredients[i];
            }

            request.AddParameter("ingredients", ingreds);
            if (number > m_maxResults)
                number = 20;
            if (number < 0)
                number = 1;
            request.AddParameter("number", number);
            request.AddParameter("limitLicense", limitLicense);
            request.AddParameter("ranking", ranking);
            request.AddParameter("ignorePantry", ignorePantry);
            request.AddParameter("apiKey", m_APYKey);

            //Execute the query
            List<RecipeByIngredient> result;
            try
            {
                RestResponse response = client.Execute(request);
                //try to get the data out of the response

                result = JsonConvert.DeserializeObject<List<RecipeByIngredient>>(response.Content);
                return result;
            }
            catch (Exception)
            {
                //add call to exception logger
                //throw;
                return null;
            }
        }

        public Recipe_Complex FindComplexRecipe(string query,
            int offset = 0,
            int number = 5,
            string cuisine = "", 
            List<string> excludeCuisine = null, 
            string diet = "", 
            List<string> intolerances = null, 
            List<string> includeIngredients = null, 
            List<string> excludeIngredients = null, 
            string type = "",
            bool instructionsRequired = true, 
            bool fillIngredients = false, 
            bool addRecipeInformation = false, 
            string titleMatch = "", 
            int maxReadyTime = -1,
            bool ignorePantry = true, 
            string sort = "", 
            string sortDirection = "", 
            bool limitLicense = true
            )
        {
            RestClient client = new RestClient(SpoonacularAPI.m_URL);
            RestRequest request = new RestRequest(SpoonacularAPI.m_ComplexRecipeURL, Method.GET);

            if (query != "")
                request.AddParameter("query", query);
            if (cuisine != "")
                request.AddParameter("cuisine", cuisine);
            string excuisine = ConvertListToSingleString(excludeCuisine);
            if (excuisine != "")
                request.AddParameter("excludeCuisine", excuisine);
            if (diet != "")
                request.AddParameter("diet", diet);
            string intolr = ConvertListToSingleString(intolerances);
            if (intolr != "")
                request.AddParameter("intolerances", intolr);
            string inclIngr = ConvertListToSingleString(includeIngredients);
            if (inclIngr != "")
                request.AddParameter("includeIngredients", inclIngr);
            string exclIngr = ConvertListToSingleString(excludeIngredients);
            if (exclIngr != "")
                request.AddParameter("excludeIngredients", exclIngr);
            if (type != "")
                request.AddParameter("type", type);
            request.AddParameter("instructionsRequired", instructionsRequired);
            request.AddParameter("fillIngredients", fillIngredients);
            request.AddParameter("addRecipeInformation", addRecipeInformation);
            if (titleMatch != "")
                request.AddParameter("titleMatch", titleMatch);
            if (maxReadyTime > 0)
                request.AddParameter("maxReadyTime", maxReadyTime);
            request.AddParameter("ignorePantry", ignorePantry);
            if (sort != "")
                request.AddParameter("sort", sort);
            if (sortDirection != "")
                request.AddParameter("sortDirection", sortDirection);
            request.AddParameter("offset", offset);
            request.AddParameter("number", number);
            request.AddParameter("limitLicense", limitLicense);

            request.AddParameter("apiKey", m_APYKey);

            //Execute the query
            Recipe_Complex result;
            try
            {
                RestResponse response = client.Execute(request);
                //try to get the data out of the response

                result = JsonConvert.DeserializeObject<Recipe_Complex>(response.Content);
                return result;
            }
            catch (Exception) //I don't know what this exception handling does, I copied it from Nick's stuff
            {
                //add call to exception logger
                throw;
            }
        }

        /// <summary>
        /// Queries the API for short recipes based off the passed params
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private SpoonacularRecipeShortSearchResult QueryAPI(RecipeSearchParams param)
        {
            RestClient client = new RestClient(SpoonacularAPI.m_URL);
            RestRequest request = new RestRequest(SpoonacularAPI.m_RecipeSearchURL, Method.GET);

            //Set up the query parameters
            request.AddParameter("query", param.query);
            if (param.number > m_maxResults)
                param.number = 20;
            if (param.number < 0)
                param.number = 1;
            request.AddParameter("number", param.number);
            if (param.offset < 0)
                param.offset = 0;
            request.AddParameter("offset", param.offset);
            if (param.cuisine != "")
                request.AddParameter("cuisine", param.cuisine);
            if (param.diet != "")
                request.AddParameter("diet", param.diet);
            if (param.excludeIngredients != "")
                request.AddParameter("excludeIngredients", param.excludeIngredients);
            if (param.intolerances != "")
                request.AddParameter("intolerances", param.intolerances);
            request.AddParameter("limitLicense", param.limitLicense);
            request.AddParameter("instructionsRequired", param.instructionsRequired);
            request.AddParameter("apiKey", m_APYKey);

            //Execute the query
            SpoonacularRecipeShortSearchResult result;
            try
            {
                RestResponse response = client.Execute(request);
                //try to get the data out of the response

                result = JsonConvert.DeserializeObject<SpoonacularRecipeShortSearchResult>(response.Content); 
                return result;
            }
            catch (Exception)
            {
                //add call to exception logger
                throw;
            }
        }


        /// <summary>
        /// Takes a Recipe ID and returns the Recipe_Full.  
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        public Recipe_Full GetRecipeFull(int recipeId)
        {
            RestClient client = new RestClient(SpoonacularAPI.m_URL);
            Recipe_Full recipe;
            try
            {
                RestRequest request = new RestRequest(SpoonacularAPI.m_RecipeInformationURL + recipeId + "/information", Method.GET);
                request.AddParameter("apiKey", m_APYKey);
                var response = client.Execute(request);
                //try to get the data out of the response
                recipe = JsonConvert.DeserializeObject<Recipe_Full>(response.Content);
            }
            catch (Exception e)
            {
                throw e;
            }
            return recipe;
        }

        /// <summary>
        /// Returns a Recipe Full given a RecipeByIngredient
        /// </summary>
        /// <param name="recipeBy"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Recipe_Full GetRecipeFull(RecipeByIngredient recipeBy)
        {
            int recipeId = recipeBy.id;
            RestClient client = new RestClient(SpoonacularAPI.m_URL);
            Recipe_Full recipe;
            try
            {
                RestRequest request = new RestRequest(SpoonacularAPI.m_RecipeInformationURL + recipeId + "/information", Method.GET);
                request.AddParameter("apiKey", m_APYKey);
                var response = client.Execute(request);
                //try to get the data out of the response
                recipe = JsonConvert.DeserializeObject<Recipe_Full>(response.Content);
            }
            catch (Exception e)
            {
                throw e;
            }
            return recipe;
        }


        /// <summary>
        /// Takes a Recipe_Short and returns the corresponding Recipe_Full. 
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        public Recipe_Full GetRecipeFull(Recipe_Short recipe)
        {
            return GetRecipeFull(recipe.id);
        }


        /// <summary>
        /// Converts a list of short recipes to a list of full recipes.  NOTE: this is expensive in terms of API calls so I'm leaving it private for now until we see a specific need for it.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private List<Recipe_Full> ToRecipeFullList(List<Recipe_Short> list)
        {
            List<Recipe_Full> fullList = new List<Recipe_Full>();
            foreach (var variable in list)
            {
                fullList.Add(this.GetRecipeFull(variable.id));
            }

            return fullList;
        }

        //SAVE - Will be used in the Image getting functions
        //recipe.ImageURL = m_URL + "/recipeImages/" + recipe.Id + "-" + m_RecipeImagex + "x" + m_RecipeImagey;

        /// <summary>
        /// Used to set up the struct with the recommended configuration. 
        /// </summary>
        /// <param name="query">The query. The only needed value</param>
        /// <param name="number">The number of results to return</param>
        /// <param name="offset">Number of items to skip. Leave default unless needed</param>
        /// <param name="cuisine">Leave default unless needed</param>
        /// <param name="diet">Leave default unless needed</param>
        /// <param name="excludeIngredients">Leave default unless needed</param>
        /// <param name="intolerances">Leave default unless needed</param>
        /// <param name="limitLicense">Leave default unless needed</param>
        /// <param name="instructionsRequired">Leave default unless needed</param>
        /// <returns></returns>
        public RecipeSearchParams SetUpParams(string query,
            int number = 5,
            int offset = 0,
            string cuisine = "",
            string diet = "",
            string excludeIngredients = "",
            string intolerances = "",
            bool limitLicense = true,
            bool instructionsRequired = true)
        {
            RecipeSearchParams param = new RecipeSearchParams
            {
                query = query,
                number = number,
                offset = offset,
                cuisine = cuisine,
                diet = diet,
                excludeIngredients = excludeIngredients,
                intolerances = intolerances,
                limitLicense = limitLicense,
                instructionsRequired = instructionsRequired
            };
            return param;
        }

        
        string ConvertListToSingleString(List<string> list)
        {
            if (list == null) return "";

            string ret = "";
            foreach (string item in list)
            {
                ret += item + ",";
            }
            //Cut off the last comma
            return ret.Substring(0, ret.Length - 1);
        }


        //The key for our API
        private static string m_APYKey = "6da40b0861384c3dbf879eb47b5bb539";
        //These two urls are broken apart as the API has different sub addresses for different types of searches
        private static string m_URL = "https://api.spoonacular.com";
        private static string m_RecipeSearchURL = "recipes/search";
        private static string m_RecipeInformationURL = "recipes/";
        private static string m_RecipeSearchByIngredientsURL = "recipes/findByIngredients";
        private static string m_ComplexRecipeURL = "/recipes/complexSearch";


        //Limits the max number of results to return.
        private static int m_maxResults = 20;

        /*  Valid values:
         *  90x90
         *  240x150
         *  312x150
         *  312x231
         *  480x360
         *  556x370
         *  636x393
         */

        /// <summary>
        /// Sets the x dimension of the returned images in pixels
        /// </summary>
        /// <remarks>
        ///  Valid values:
        /// *  90x90
        /// *  240x150
        /// *  312x150
        /// *  312x231
        /// *  480x360
        /// *  556x370
        /// *  636x393
        /// </remarks>
        public static int m_RecipeImagex = 556;

        /// <summary>
        /// Sets the y dimension of the returned images in pixels
        /// </summary>
        /// <remarks>
        ///  Valid values:
        /// *  90x90
        /// *  240x150
        /// *  312x150
        /// *  312x231
        /// *  480x360
        /// *  556x370
        /// *  636x393
        /// </remarks>
        public static int m_RecipeImagey = 370;


        //Singleton instance

        private static SpoonacularAPI m_instance;
        

 
    }
   
}