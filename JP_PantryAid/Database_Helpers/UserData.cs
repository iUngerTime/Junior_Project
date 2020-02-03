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
            currUser.Allergies.Add(newAlergy);

            string query = String.Format("INSERT INTO ALERGIES VALUES({0}, {1})", SqlHelper.UserID, newAlergy.IngredientID);
            SqlConnection con = new SqlConnection(SqlHelper.GetConnectionString());
            SqlCommand comm = new SqlCommand(query, con);

            try
            {
                con.Open();
            }
            catch (Exception)
            {
                return -1;
            }

            try
            {
                comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return -1;
            }


            con.Close();

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currUser"></param>
        /// <param name="newDisliked"></param>
        /// <returns></returns>
        public int AddDislikedRecipe(User currUser, Recipe_Short newDisliked)
        {
            currUser.DislikedRecipes.Add(newDisliked);

            string query = String.Format("INSERT INTO DISLIKED_RECIPE VALUES({0}, {1})", SqlHelper.UserID, newDisliked.id);
            SqlConnection con = new SqlConnection(SqlHelper.GetConnectionString());
            SqlCommand comm = new SqlCommand(query, con);

            try
            {
                con.Open();
            }
            catch (Exception)
            {
                return -1;
            }

            try
            {
                comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return -1;
            }

            con.Close();

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currUser"></param>
        /// <param name="newFavorite"></param>
        /// <returns></returns>
        public int AddFavoriteRecipe(User currUser, Recipe_Short newFavorite)
        {
            currUser.FavoriteRecipes.Add(newFavorite);

            string query = String.Format("INSERT INTO FAVORITE_RECIPE VALUES({0}, {1})", SqlHelper.UserID, newFavorite.id);
            SqlConnection con = new SqlConnection(SqlHelper.GetConnectionString());
            SqlCommand comm = new SqlCommand(query, con);

            try
            {
                con.Open();
            }
            catch (Exception)
            {
                return -1;
            }

            try
            {
                comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return -1;
            }

            con.Close();

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public int AddUser(User newUser)
        {
            string query = String.Format("INSERT INTO PERSON VALUES('{0}', '{1}');", newUser.FullName, newUser.Email);
            SqlConnection con = new SqlConnection(SqlHelper.GetConnectionString());
            SqlCommand comm = new SqlCommand(query, con);

            try
            {
                con.Open();
            }
            catch (Exception)
            {
                return -1;
            }

            try
            {
                comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return -1;
            }

            con.Close();

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delUser"></param>
        /// <returns></returns>
        public int DeleteUser(User delUser)
        {
            string query = String.Format("DELETE FROM PERSON WHERE UserID={0};", delUser.Id);
            string query2 = String.Format("DELETE FROM PANTRY_INGREDIENTS WHERE PantryID={0};", delUser.Id);
            SqlConnection con = new SqlConnection(SqlHelper.GetConnectionString());
            SqlCommand comm = new SqlCommand(query, con);

            try
            {
                con.Open();
            }
            catch (Exception)
            {
                return -1;
            }

            try
            {
                comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return -1;
            }

            comm = new SqlCommand(query2, con);

            try
            {
                comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return -1;
            }

            con.Close();

            return 0;
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
            currUser.Allergies.Remove(alergy);

            string query = String.Format("DELETE FROM ALERGIES WHERE UserID={0} AND RecipeID={1};", SqlHelper.UserID, alergy.IngredientID);
            SqlConnection con = new SqlConnection(SqlHelper.GetConnectionString());
            SqlCommand comm = new SqlCommand(query, con);

            try
            {
                con.Open();
            }
            catch (Exception)
            {
                return -1;
            }

            try
            {
                comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return -1;
            }

            con.Close();

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currUser"></param>
        /// <param name="nonDisliked"></param>
        /// <returns></returns>
        public int RemoveDislikedRecipe(User currUser, Recipe_Short nonDisliked)
        {
            currUser.DislikedRecipes.Remove(nonDisliked);

            string query = String.Format("DELETE FROM DISLIKED_RECIPE WHERE UserID={0} AND RecipeID={1};", SqlHelper.UserID, nonDisliked.id);
            SqlConnection con = new SqlConnection(SqlHelper.GetConnectionString());
            SqlCommand comm = new SqlCommand(query, con);

            try
            {
                con.Open();
            }
            catch (Exception)
            {
                return -1;
            }

            try
            {
                comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return -1;
            }

            con.Close();

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currUser"></param>
        /// <param name="nonFavorite"></param>
        /// <returns></returns>
        public int RemoveFavoriteRecipe(User currUser, Recipe_Short nonFavorite)
        {
            currUser.DislikedRecipes.Remove(nonFavorite);

            string query = String.Format("DELETE FROM FAVORITE_RECIPE WHERE UserID={0} AND RecipeID={1};", SqlHelper.UserID, nonFavorite.id);
            SqlConnection con = new SqlConnection(SqlHelper.GetConnectionString());
            SqlCommand comm = new SqlCommand(query, con);

            try
            {
                con.Open();
            }
            catch (Exception)
            {
                return -1;
            }

            try
            {
                comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return -1;
            }

            con.Close();

            return 0;
        }
    }
}
