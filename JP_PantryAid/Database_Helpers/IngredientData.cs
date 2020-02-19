using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PantryAid.Core.Interfaces;
using PantryAid.Core.Models;
using System.Data.SqlClient;
using PantryAid.Core.Utilities;

namespace Database_Helpers
{
    public class IngredientData : iIngredientData
    {
        private iSqlServerDataAccess _database;

        public IngredientData(iSqlServerDataAccess database)
        {
            _database = database;
        }

        public int AddIngredient(Ingredient newIng)
        {
            throw new NotImplementedException();
        }

        public int AddIngredientsFromRecipeFull(Recipe_Full recipe)
        {
            throw new NotImplementedException();
        }

        public int AddIngredientsFromRecipeShort(Recipe_Short recipe)
        {
            throw new NotImplementedException();
        }

        public Ingredient GetIngredient(Ingredient ing)
        {
            return GetIngredient(ing.IngredientID);
        }

        public Ingredient GetIngredient(int ID)
        {
            SqlConnection con = new SqlConnection(SqlHelper.GetConnectionString());
            string query = String.Format("SELECT * FROM NEW_INGREDIENTS WHERE IngredientID={0};", ID);
            SqlCommand comm = new SqlCommand(query, con);
            string ingrname = "";

            try
            {
                con.Open();
            }
            catch(Exception)
            {
                return null;
            }

            try
            {
                SqlDataReader read = comm.ExecuteReader();

                if (read.Read())
                {
                    ingrname = read.GetString(1);
                }
                else
                {
                    return null;
                }

                read.Close();
            }
            catch(Exception)
            {
                return null;
            }

            con.Close();

            return new Ingredient(ID, ingrname);
        }

        public Ingredient GetIngredient(string name)
        {
            SqlConnection con = new SqlConnection(SqlHelper.GetConnectionString());
            string query = String.Format("SELECT * FROM NEW_INGREDIENTS WHERE IngredientName='{0}';", name);
            SqlCommand comm = new SqlCommand(query, con);
            int ingrid = -1;

            try
            {
                con.Open();
            }
            catch (Exception)
            {
                return null;
            }

            try
            {
                SqlDataReader read = comm.ExecuteReader();

                if (read.Read())
                {
                    ingrid = read.GetInt32(0);
                }
                else
                {
                    return null;
                }

                read.Close();
            }
            catch (Exception)
            {
                return null;
            }

            con.Close();

            return new Ingredient(ingrid, name);
        }

        public List<IngredientItem> GetIngredientsFromPantry(int PantryID)
        {
            SqlConnection con = new SqlConnection(SqlHelper.GetConnectionString());
            string query = String.Format("SELECT IngredientID, IngredientName, Quantity FROM PANTRY_INGREDIENTS WHERE PantryID={0};", PantryID);
            SqlCommand comm = new SqlCommand(query, con);

            List<IngredientItem> pantryingredients = new List<IngredientItem>();

            try
            {
                con.Open();
            }
            catch (Exception)
            {
                return null;
            }

            try
            {
                //The following garbage is utterly broken if we try to get the quantity
                using (SqlDataReader read = comm.ExecuteReader())
                {

                    int id = -1;
                    string name = "";
                    double quant = 1.0f;

                    while (read.Read())
                    {
                        id = read.GetInt32(0);
                        name = read.GetString(1);
                        quant = read.GetDouble(2);
                        Ingredient ing = new Ingredient(id, name);
                        IngredientItem ingi = new IngredientItem(ing, quant, Measurements.Serving);

                        pantryingredients.Add(ingi);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            con.Close();


            return pantryingredients;
        }

        public int RemoveIngredient(Ingredient oldIng)
        {
            throw new NotImplementedException();
        }

        public int AddIngredientToPantry(int PantryID, int IngredientID, double Quantity = 1.0f)
        {
            Ingredient ingr = this.GetIngredient(IngredientID);

            string query = String.Format("INSERT INTO PANTRY_INGREDIENTS VALUES ({0}, {1}, '{2}', {3});", PantryID, IngredientID, ingr.Name, Quantity);

            return _database.ExecuteQuery_NoReturnType(query);
        }

        public int RemoveIngredientFromPantry(int PantryID, int IngredientID)
        {
            string query = String.Format("DELETE FROM PANTRY_INGREDIENTS WHERE PantryID={0} AND IngredientID={1};", PantryID, IngredientID);

            return _database.ExecuteQuery_NoReturnType(query);
        }

        public int UpdatePantryIngredientQuantity(int PantryID, int IngredientID, double NewQuantity)
        {
            string query;
            if (NewQuantity <= 0) //If new quantity is 0 then remove it from pantry
                query = String.Format("DELETE FROM PANTRY_INGREDIENTS WHERE PantryID={0} AND IngredientID={1};", PantryID, IngredientID);
            else
                query = String.Format("UPDATE PANTRY_INGREDIENTS SET Quantity={2} WHERE PantryID={0} AND IngredientID={1};", PantryID, IngredientID, NewQuantity);

            return _database.ExecuteQuery_NoReturnType(query);
        }

        public int AddIngredientToPantry(int PantryID, Ingredient ingredient, double Quantity = 1.0f)
        {
            return this.AddIngredientToPantry(PantryID, ingredient.IngredientID, Quantity);
        }

        public int RemoveIngredientFromPantry(int PantryID, Ingredient ingredient)
        {
            return this.RemoveIngredientFromPantry(PantryID, ingredient.IngredientID);
        }

        private int FAIL = 0;
        private int PASS = 1;
    }

}
