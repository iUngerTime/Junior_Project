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
            /// Whether the recipies should have instructions.  Should always be true for obvious reasons.
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
        public List<Recipe> RecipeSearch(string query, int maxResults , int offset )
        {
            RecipeSearchParams param = SetUpParams(query, maxResults, offset);
            SpoonacularRecipeSearchRootObject rootObject = QueryAPI(param);
            List<RecipeInformationRootObject> recipeInfoList = ToRecipeInformationList(rootObject);
            return ToRecipeList(recipeInfoList);
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
        public List<Recipe> RecipeSearch(string query,
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
            SpoonacularRecipeSearchRootObject rootObject = QueryAPI(param);
            List<RecipeInformationRootObject> recipeInfoList = ToRecipeInformationList(rootObject);
            return ToRecipeList(recipeInfoList);
        }

        private  SpoonacularRecipeSearchRootObject QueryAPI(RecipeSearchParams param)
        {
            //string url =  + ;
            RestClient client = new RestClient(SpoonacularAPI.m_URL);
            RestRequest request = new RestRequest(SpoonacularAPI.m_RecipeSearchURL, Method.GET);

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

            SpoonacularRecipeSearchRootObject rootObject = new SpoonacularRecipeSearchRootObject();
            try
            {
                RestResponse response = client.Execute(request);

                //try to get the data out of the response
                rootObject = JsonConvert.DeserializeObject<SpoonacularRecipeSearchRootObject>(response.Content); return rootObject;


                

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //public RecipeInformationRootObject ToRecipeInformation(Spoona)


        //Converts a rootObject to a List of recipe information objects
        private List<RecipeInformationRootObject> ToRecipeInformationList(SpoonacularRecipeSearchRootObject rootObject)
        {
            RestClient client = new RestClient(SpoonacularAPI.m_URL);
            List<RecipeInformationRootObject> rootList = new List<RecipeInformationRootObject>();  //for storing the recipe information
            try
            {
                for (int i = 0; i < rootObject.results.Count; ++i)
                {
                    RestRequest request = new RestRequest(SpoonacularAPI.m_RecipeInformationURL + rootObject.results[i].id + "/information", Method.GET);
                    request.AddParameter("apiKey", m_APYKey);
                    var response = client.Execute(request);

                    //try to get the data out of the response
                    RecipeInformationRootObject recipeRoot = JsonConvert.DeserializeObject<RecipeInformationRootObject>(response.Content);
                    rootList.Add(recipeRoot);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return rootList;
        }


        private List<Recipe> ToRecipeList(List<RecipeInformationRootObject> recipeInfo)
        {
            List<Recipe> recipes = new List<Recipe>();
            //iterate through the list
            for (int i = 0; i < recipeInfo.Count; ++i)
            {
                Recipe recipe = new Recipe();
                recipe.Instructions = new List<string>();
                recipe.Id = recipeInfo[i].id;
                if (recipeInfo[i].title != null)
                    recipe.Name = recipeInfo[i].title;
                if (recipeInfo[i].instructions != null)
                    recipe.Instructions.Add( recipeInfo[i].instructions);  //well need to break this out later
                if (recipeInfo[i].image != null)
                    recipe.ImageURL = recipeInfo[i].image;
                ///TODO:  Parse out all the bools into labels!!!!!!
                ///TODO:  Parse out the Ingredients!!!!!!!
                
                recipes.Add(recipe);
            }

            return recipes;
        }




        /// <summary>
        /// Used to set up the struct with recommended configuration. 
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

        //THe key for our API
        private static string m_APYKey = "6da40b0861384c3dbf879eb47b5bb539";
        //These two urls are broken apart as the API has different sub addresses for different types of searches
        private static string m_URL = "https://api.spoonacular.com";
        private static string m_RecipeSearchURL = "recipes/search";
        private static string m_RecipeInformationURL = "recipes/";
        //Limits the max number of results to return.
        private static int m_maxResults = 20;

        private static SpoonacularAPI m_instance;
        //private string 











       
    }
   
}