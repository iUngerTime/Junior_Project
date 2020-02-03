using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        /// <returns>Returns 1 if it worked</returns>
        public int AddUser(User newUser)
        {
            //Values needed to add user
            string userName = newUser.FullName;
            string email = newUser.Email;

            //create the user in the database
            string ConnectionString = SqlHelper.GetConnectionString();
            string query = String.Format("INSERT INTO PERSON VALUES('{0}', '{1}');", userName, email);

            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand comm = new SqlCommand(query, con);

            //If any of the following code fails, return 0 as it was not entered into database
            try
            {
                con.Open();
                comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return FAIL;
            }

            return PASS;
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

        private int PASS = 1;
        private int FAIL = 0;
    }
}
