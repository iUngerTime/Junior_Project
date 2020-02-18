using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PantryAid.Core.Interfaces;
using PantryAid.Core.Models;
using PantryAid.Core.Utilities;

namespace Database_Helpers
{
    /// <summary>
    /// This class holds helper functions that interact with a sql database
    /// The database connected to is definend in "SQL_Helper_Class.cs" in the same namespace
    /// </summary>
    public class UserData : iUserDataRepo
    {
        //Database we perform functions on
        private iSqlServerDataAccess _database;

        /// <summary>
        /// Ctor for this class
        /// </summary>
        /// <param name="database">database implenation to execute on</param>
        public UserData(iSqlServerDataAccess database)
        {
            _database = database;
        }

        /// <summary>
        /// Adds an alergy to a user's list of alergies
        /// </summary>
        /// <param name="currUser">The user to operate on</param>
        /// <param name="newAlergy">Alergy to be added to database</param>
        /// <returns>Returns 1 if pass, 0 if failed</returns>
        public int AddAlergy(User currUser, Ingredient newAlergy)
        {
            currUser.Allergies.Add(newAlergy);

            string query = String.Format("INSERT INTO ALERGIES VALUES({0}, {1})", SqlHelper.UserID, newAlergy.IngredientID);

            return _database.ExecuteQuery_NoReturnType(query);
        }

        /// <summary>
        /// Adds a recipe to a user's list of dislikes
        /// </summary>
        /// <param name="currUser">The user to operate on</param>
        /// <param name="newDisliked">The disliked recipe to add to database</param>
        /// <returns>Returns 1 if pass, 0 if failed</returns>
        public int AddDislikedRecipe(User currUser, Recipe_Short newDisliked)
        {
            currUser.DislikedRecipes.Add(newDisliked);

            string query = String.Format("INSERT INTO DISLIKED_RECIPE VALUES({0}, {1})", SqlHelper.UserID, newDisliked.id);

            return _database.ExecuteQuery_NoReturnType(query);
        }

        /// <summary>
        /// Adds a favorite recipe to an existing user in the database
        /// </summary>
        /// <param name="currUser">The user to operate on</param>
        /// <param name="newFavorite">New favorite recipe to add into database</param>
        /// <returns>Returns 1 if pass, 0 if failed</returns>
        public int AddFavoriteRecipe(User currUser, Recipe_Short newFavorite)
        {
            currUser.FavoriteRecipes.Add(newFavorite);

            string query = String.Format("INSERT INTO FAVORITE_RECIPE VALUES({0}, {1})", SqlHelper.UserID, newFavorite.id);

            return _database.ExecuteQuery_NoReturnType(query);
        }

        /// <summary>
        /// Adds a user to a sql database
        /// </summary>
        /// <param name="newUser">New User to add to database</param>
        /// <returns>Returns 1 if pass, 0 if failed</returns>
        public int AddUser(User newUser)
        {
            string query = String.Format("INSERT INTO PERSON VALUES('{0}', '{1}');", newUser.FullName, newUser.Email);

            return _database.ExecuteQuery_NoReturnType(query);
        }

        /// <summary>
        /// Deletes a user from the sql database
        /// </summary>
        /// <param name="delUser">The user to be deleted</param>
        /// <returns>Returns 1 if pass, 0 if failed</returns>
        public int DeleteUser(User delUser)
        {
            string query = String.Format("DELETE FROM PERSON WHERE UserID={0};", delUser.Id);
            string query2 = String.Format("DELETE FROM PANTRY_INGREDIENTS WHERE PantryID={0};", delUser.Id);
            string query3 = String.Format("DELETE FROM FROM ALERGIES WHERE UserID={0}", delUser.Id);
            string query4 = String.Format("DELETE FROM FAVORITE_RECIPE WHERE UserID={0}", delUser.Id);
            string query5 = String.Format("DELETE FROM DISLIKED_RECIPE WHERE UserID={0}", delUser.Id);

            int failed = _database.ExecuteQuery_NoReturnType(query);
            if (failed != 0) { _database.ExecuteQuery_NoReturnType(query2); }
            if (failed != 0) { _database.ExecuteQuery_NoReturnType(query3); }
            if (failed != 0) { _database.ExecuteQuery_NoReturnType(query4); }
            if (failed != 0) { _database.ExecuteQuery_NoReturnType(query5); }

            return (failed == 1 ? PASS : FAIL);
        }

        /// <summary>
        /// Edits a current user's info to the new info (WILL NOT CHANGE USER_ID)
        /// </summary>
        /// <param name="currentInfo">The current user's info</param>
        /// <param name="newInfo">The new info to update to</param>
        /// <returns></returns>
        public int EditUserInfo(User currentInfo, User newInfo)
        {
            string query = String.Format("UPDATE PERSON SET FullName='{0}', Email='{1}' WHERE UserID={2};", newInfo.FullName, newInfo.Email, currentInfo.Id);

            return _database.ExecuteQuery_NoReturnType(query);
        }

        /// <summary>
        /// Removes an alergy from a user's list of alergies
        /// </summary>
        /// <param name="currUser">User to operate on</param>
        /// <param name="alergy">Alergy to be removed from list of alergies</param>
        /// <returns>Returns 1 if pass, 0 if failed</returns>
        public int RemoveAlergy(User currUser, Ingredient alergy)
        {
            currUser.Allergies.Remove(alergy);

            string query = String.Format("DELETE FROM ALERGIES WHERE UserID={0} AND RecipeID={1};", SqlHelper.UserID, alergy.IngredientID);

            return _database.ExecuteQuery_NoReturnType(query);
        }

        /// <summary>
        /// Removes a disliked recipe from a user's list of dislikes
        /// </summary>
        /// <param name="currUser">User to operate on</param>
        /// <param name="nonDisliked">Recipe that is not a disliked recipe</param>
        /// <returns>Returns 1 if pass, 0 if failed</returns>
        public int RemoveDislikedRecipe(User currUser, Recipe_Short nonDisliked)
        {
            currUser.DislikedRecipes.Remove(nonDisliked);

            string query = String.Format("DELETE FROM DISLIKED_RECIPE WHERE UserID={0} AND RecipeID={1};", SqlHelper.UserID, nonDisliked.id);

            return _database.ExecuteQuery_NoReturnType(query);
        }

        /// <summary>
        /// Removes a favorited recipe from the list
        /// </summary>
        /// <param name="currUser">User to remove data from</param>
        /// <param name="nonFavorite">Recipe in the user's list of favorites</param>
        /// <returns>Returns 1 if pass, 0 if failed</returns>
        public int RemoveFavoriteRecipe(User currUser, Recipe_Short nonFavorite)
        {
            currUser.DislikedRecipes.Remove(nonFavorite);

            string query = String.Format("DELETE FROM FAVORITE_RECIPE WHERE UserID={0} AND RecipeID={1};", SqlHelper.UserID, nonFavorite.id);

            return _database.ExecuteQuery_NoReturnType(query);
        }

        private int FAIL = 0;
        private int PASS = 1;
    }
}
