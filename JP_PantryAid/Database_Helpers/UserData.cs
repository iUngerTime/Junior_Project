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
        #region injection info
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
        #endregion

        #region alergies
        /// <summary>
        /// Adds an alergy to the list of alergies associated with a user
        /// </summary>
        /// <param name="currUser">The current user to add a new alergy to</param>
        /// <param name="newAlergy">The ingredient that is the alergy</param>
        /// <returns>0 if successful, 1 if failed</returns>
        public int AddAlergy(User currUser, Alergens newAlergy)
        {
            if (!currUser.Allergies.Contains(newAlergy))
            {
                currUser.Allergies.Add(newAlergy);

                string query = String.Format("INSERT INTO ALERGIES VALUES({0}, {1})", SqlServerDataAccess.UserID, newAlergy);

                return _database.ExecuteQuery_NoReturnType(query);
            }

            return 0;
        }

        /// <summary>
        /// Removes an alergy from a user's list of alergies
        /// </summary>
        /// <param name="currUser">User to operate on</param>
        /// <param name="alergy">Alergy to be removed from list of alergies</param>
        /// <returns>Returns 1 if pass, 0 if failed</returns>
        public int RemoveAlergy(User currUser, Alergens oldAlergy)
        {
            if (currUser.Allergies.Contains(oldAlergy))
            {
                currUser.Allergies.Remove(oldAlergy);

                string query = String.Format("DELETE FROM ALERGIES WHERE UserID={0} AND AlergyID={1};", SqlServerDataAccess.UserID, oldAlergy);

                return _database.ExecuteQuery_NoReturnType(query);
            }

            return 0;
        }
        #endregion

        #region disliked recipes
        /// <summary>
        /// Adds a recipe to a user's list of dislikes
        /// </summary>
        /// <param name="currUser">The user to operate on</param>
        /// <param name="newDisliked">The disliked recipe to add to database</param>
        /// <returns>Returns 1 if pass, 0 if failed</returns>
        public int AddDislikedRecipe(User currUser, int newDisliked)
        {
            if(!currUser.DislikedRecipes.Contains(newDisliked))
            {
                currUser.DislikedRecipes.Add(newDisliked);

                string query = String.Format("INSERT INTO DISLIKED_RECIPE VALUES({0}, {1})", SqlServerDataAccess.UserID, newDisliked);

                return _database.ExecuteQuery_NoReturnType(query);
            }

            return 0;
        }

        /// <summary>
        /// Removes a disliked recipe from a user's list of dislikes
        /// </summary>
        /// <param name="currUser">User to operate on</param>
        /// <param name="nonDisliked">Recipe that is not a disliked recipe</param>
        /// <returns>Returns 1 if pass, 0 if failed</returns>
        public int RemoveDislikedRecipe(User currUser, int nonDisliked)
        {
            if (currUser.DislikedRecipes.Contains(nonDisliked))
            {
                currUser.DislikedRecipes.Remove(nonDisliked);

                string query = String.Format("DELETE FROM DISLIKED_RECIPE WHERE UserID={0} AND RecipeID={1};", SqlServerDataAccess.UserID, nonDisliked);

                return _database.ExecuteQuery_NoReturnType(query);
            }

            return 0;
        }
        #endregion

        #region favorite recipes
        /// <summary>
        /// Adds a favorite recipe to an existing user in the database
        /// </summary>
        /// <param name="currUser">The user to operate on</param>
        /// <param name="newFavorite">New favorite recipe to add into database</param>
        /// <returns>Returns 1 if pass, 0 if failed</returns>
        public int AddFavoriteRecipe(User currUser, int newFavorite)
        {
            if(!currUser.FavoriteRecipes.Contains(newFavorite))
            {
                currUser.FavoriteRecipes.Add(newFavorite);

                string query = String.Format("INSERT INTO FAVORITE_RECIPE VALUES({0}, {1})", SqlServerDataAccess.UserID, newFavorite);

                return _database.ExecuteQuery_NoReturnType(query);
            }

            return 0;
        }

        /// <summary>
        /// Removes a favorited recipe from the list
        /// </summary>
        /// <param name="currUser">User to remove data from</param>
        /// <param name="nonFavorite">Recipe in the user's list of favorites</param>
        /// <returns>Returns 1 if pass, 0 if failed</returns>
        public int RemoveFavoriteRecipe(User currUser, int nonFavorite)
        {
            if (currUser.FavoriteRecipes.Contains(nonFavorite))
            {
                currUser.FavoriteRecipes.Remove(nonFavorite);

                string query = String.Format("DELETE FROM FAVORITE_RECIPE WHERE UserID={0} AND RecipeID={1};", SqlServerDataAccess.UserID, nonFavorite);

                return _database.ExecuteQuery_NoReturnType(query);
            }

            return 0;
        }
        #endregion

        #region diet preferences
        public int AddDietPreference(User currUser, DietPreferences newPreference)
        {
            if (!currUser.DietaryPreferences.Contains(newPreference))
            {
                currUser.DietaryPreferences.Add(newPreference);

                string query = String.Format("INSERT INTO DIETARY_PREFERENCES VALUES({0}, {1})", SqlServerDataAccess.UserID, newPreference);

                return _database.ExecuteQuery_NoReturnType(query);
            }

            return 0;
        }

        public int RemoveDietPreference(User currUser, DietPreferences newPreference)
        {
            if (currUser.DietaryPreferences.Contains(newPreference))
            {
                currUser.DietaryPreferences.Remove(newPreference);

                string query = String.Format("DELETE FROM DIETARY_PREFERENCES WHERE UserID={0} AND DietaryPreferenceID={1};", SqlServerDataAccess.UserID, newPreference);

                return _database.ExecuteQuery_NoReturnType(query);
            }

            return 0;
        }
        #endregion

        /// <summary>
        /// Adds a user to a sql database
        /// </summary>
        /// <param name="newUser">New User to add to database</param>
        /// <returns>Returns 1 if pass, 0 if failed</returns>
        public int AddUser(User newUser)
        {
            string query = String.Format("INSERT INTO PERSON VALUES('{0}', '{1}');", SqlServerDataAccess.Sanitize(newUser.Email), newUser.Hash);

            return _database.ExecuteQuery_NoReturnType(query);
        }

        /// <summary>
        /// Deletes a user from the database
        /// </summary>
        /// <param name="delUser">The info of the user to be deleted</param>
        /// <returns>0 if successful, 1 if failed</returns>
        public int DeleteUser(User delUser)
        {
            //NOTE TO FUTURE SELF: THIS FUNCTION IS BROKE AS OF 5/13/2020
            //THE ORDER OF THE QUERIES IS VERY OUT OF ORDER AND WILL GUARENTEED FAIL

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
        /// Edits a person's entire list of information
        /// </summary>
        /// <param name="currentInfo">The information of the user to be edited</param>
        /// <param name="newInfo">The new information to edit the current to</param>
        /// <returns>0 if successful, 1 if failed</returns>
        public int EditUserInfo(User currentInfo, User newInfo)
        {
            string query = String.Format("UPDATE PERSON SET Email='{0}', PasswordHash = '{1}' WHERE UserID={2};",
            SqlServerDataAccess.Sanitize(newInfo.Email), newInfo.Hash, currentInfo.Id);

            return _database.ExecuteQuery_NoReturnType(query);
        }

        /// <summary>
        /// Get the user
        /// </summary>
        /// <param name="email">Email of a user</param>
        /// <returns></returns>
        public User GetUser(string email)
        {
            string query = "SELECT UserID, Email, PasswordHash FROM PERSON WHERE LOWER(Email) = '" + SqlServerDataAccess.Sanitize(email) + "';";

            User usr = _database.ExecuteQuery_SingleUser(query);

            if(usr != null)
            {
                _database.ExecuteQuery_GetUserAlergies(usr);
                _database.ExecuteQuery_GetUserDietaryPreferences(usr);
                _database.ExecuteQuery_GetUserFavoriteRecipes(usr);
                _database.ExecuteQuery_GetUserDislikedRecipes(usr);
            }

            return usr;
        }

        /// <summary>
        /// Get the user
        /// </summary>
        /// <param name="usrid">UserId as an int</param>
        /// <returns></returns>
        public User GetUser(int usrid)
        {
            string query = "SELECT UserID, Email, PasswordHash FROM PERSON WHERE UserID = '" + usrid + "';";

            User usr = _database.ExecuteQuery_SingleUser(query);

            if (usr != null)
            {
                _database.ExecuteQuery_GetUserAlergies(usr);
                _database.ExecuteQuery_GetUserDietaryPreferences(usr);
                _database.ExecuteQuery_GetUserFavoriteRecipes(usr);
                _database.ExecuteQuery_GetUserDislikedRecipes(usr);
            }

            return usr;
        }

        private int FAIL = 0;
        private int PASS = 1;
    }
}
