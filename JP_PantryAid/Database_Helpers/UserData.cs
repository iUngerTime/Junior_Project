using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PantryAid.Core.Interfaces;
using PantryAid.Core.Models;

namespace Database_Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class UserData : iUserDataRepo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currUser"></param>
        /// <param name="newAlergy"></param>
        /// <returns></returns>
        public int AddAlergy(User currUser, Ingredient newAlergy)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currUser"></param>
        /// <param name="newDisliked"></param>
        /// <returns></returns>
        public int AddDislikedRecipe(User currUser, Recipe_Short newDisliked)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currUser"></param>
        /// <param name="newFavorite"></param>
        /// <returns></returns>
        public int AddFavoriteRecipe(User currUser, Recipe_Short newFavorite)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public int AddUser(User newUser)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delUser"></param>
        /// <returns></returns>
        public int DeleteUser(User delUser)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentInfo"></param>
        /// <param name="newInfo"></param>
        /// <returns></returns>
        public int EditUserInfo(User currentInfo, User newInfo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currUser"></param>
        /// <param name="alergy"></param>
        /// <returns></returns>
        public int RemoveAlergy(User currUser, Ingredient alergy)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currUser"></param>
        /// <param name="nonDisliked"></param>
        /// <returns></returns>
        public int RemoveDislikedRecipe(User currUser, Recipe_Short nonDisliked)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currUser"></param>
        /// <param name="nonFavorite"></param>
        /// <returns></returns>
        public int RemoveFavoriteRecipe(User currUser, Recipe_Short nonFavorite)
        {
            throw new NotImplementedException();
        }
    }
}
