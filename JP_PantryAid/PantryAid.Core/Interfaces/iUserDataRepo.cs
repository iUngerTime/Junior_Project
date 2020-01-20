using System;
using System.Collections.Generic;
using System.Text;
using PantryAid.Core.Models;

namespace PantryAid.Core.Interfaces
{
    /// <summary>
    /// Defines any functions related to editing/retrieving user info
    /// </summary>
    interface iUserDataRepo
    {
        /// <summary>
        /// Adds a new user to the database
        /// </summary>
        /// <param name="newUser">The information of the new user</param>
        /// <returns>0 if successful, 1 if failed</returns>
        int AddUser(User newUser);

        /// <summary>
        /// Edits a person's entire list of information
        /// </summary>
        /// <param name="currentInfo">The information of the user to be edited</param>
        /// <param name="newInfo">The new information to edit the current to</param>
        /// <returns>0 if successful, 1 if failed</returns>
        int EditUserInfo(User currentInfo, User newInfo);

        /// <summary>
        /// Deletes a user from the database
        /// </summary>
        /// <param name="delUser">The info of the user to be deleted</param>
        /// <returns>0 if successful, 1 if failed</returns>
        int DeleteUser(User delUser);

        /// <summary>
        /// Adds an alergy to the list of alergies associated with a user
        /// </summary>
        /// <param name="currUser">The current user to add a new alergy to</param>
        /// <param name="newAlergy">The ingredient that is the alergy</param>
        /// <returns>0 if successful, 1 if failed</returns>
        int AddAlergy(User currUser, Ingredient newAlergy);

        /// <summary>
        /// Remove an alergy from the list of alergies associated with a user
        /// </summary>
        /// <param name="currUser">The current user to remove an alergy from</param>
        /// <param name="alergy">The ingredient that is not an alergy</param>
        /// <returns>0 if successful, 1 if failed</returns>
        int RemoveAlergy(User currUser, Ingredient alergy);

        /// <summary>
        /// Adds a favorite recipe to a user
        /// </summary>
        /// <param name="currUser">The current user to add a new recipe to</param>
        /// <param name="newFavorite">The recipe that is to be a favorite</param>
        /// <returns>0 if successful, 1 if failed</returns>
        int AddFavoriteRecipe(User currUser, Recipe newFavorite);

        /// <summary>
        /// Remove a favorited recipe from a user
        /// </summary>
        /// <param name="currUser">The current user to remove an old favorite recipe from</param>
        /// <param name="nonFavorite">The recipe to remove from the list of favorites</param>
        /// <returns>0 if successful, 1 if failed</returns>
        int RemoveFavoriteRecipe(User currUser, Recipe nonFavorite);

        /// <summary>
        /// Add a disliked recipe to a user
        /// </summary>
        /// <param name="currUser">The user to add a new disliked recipe to</param>
        /// <param name="newDisliked">The recipe to add to the list of dislikes</param>
        /// <returns>0 if successful, 1 if failed</returns>
        int AddDislikedRecipe(User currUser, Recipe newDisliked);

        /// <summary>
        /// Remove a disliked recipe from a user
        /// </summary>
        /// <param name="currUser">The user to remove an old disliked recipe from</param>
        /// <param name="nonDisliked">The recipe to remove from the list of dislikes</param>
        /// <returns>0 if successful, 1 if failed</returns>
        int RemoveDislikedRecipe(User currUser, Recipe nonDisliked);
    }
}
