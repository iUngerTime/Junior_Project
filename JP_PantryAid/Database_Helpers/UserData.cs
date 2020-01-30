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
    public class UserData : iUserDataRepo
    {
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

        public int AddUser(User newUser)
        {
            throw new NotImplementedException();
        }

        public int DeleteUser(User delUser)
        {
            throw new NotImplementedException();
        }

        public int EditUserInfo(User currentInfo, User newInfo)
        {
            throw new NotImplementedException();
        }

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
