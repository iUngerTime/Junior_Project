using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Database_Helpers;
using PantryAid.Core.Models;

namespace PantryAid
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PantryPage : ContentPage
    {
        ListViewModel<IngredientItem> _list = new ListViewModel<IngredientItem>();
        public PantryPage()
        {
            InitializeComponent();

            this.BindingContext = _list;
            FillGrid();
        }

        async public void FillGrid()
        {
            string ConnectionString = SqlHelper.GetConnectionString();
            //Below querys assumes that UserIDs and PantryIDs are equivalent
            //Commented query below will be used when more columns are added to INGREDIENT
            //string query = String.Format("SELECT ING.IngredientID, ING.CommonName, ING.LongDesc, Quantity FROM PANTRY_INGREDIENTS AS PING JOIN INGREDIENT AS ING ON PING.IngredientID = ING.IngredientID WHERE PantryID = {0};", SqlHelper.UserID);
            string query = String.Format("SELECT IngredientID, CommonName, Quantity FROM PANTRY_INGREDIENTS WHERE PantryID={0};", SqlHelper.UserID);

            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand comm = new SqlCommand(query, con);

            try
            {
                con.Open();
            }
            catch (Exception)
            {
                await DisplayAlert("Failed", "Could not connect", "OK");
            }

            try
            {
                SqlDataReader read = comm.ExecuteReader();

                while (read.Read())
                {
                    IngredientItem p = new IngredientItem(new Ingredient(read.GetInt32(0), read.GetString(1), ""), read.GetInt32(2), Measurements.Serving);
                    _list.Add(p); 
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Exception thrown while reading from database in FillGrid", "OK");
            }

            con.Close();
        }

        //This ugly function uses three queries. I'm sure they could maybe be made into less but I'm not sure at the moment.
        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            string ConnectionString = SqlHelper.GetConnectionString();
            string query = String.Format("SELECT IngredientID, Name FROM INGREDIENT WHERE Name='{0}'", IngredientEntry.Text);

            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand comm = new SqlCommand(query, con);

            string ingrname = "default";
            int ingrid = -1;
            int count = 1;
            bool alreadyexists = false;

            try
            {
                con.Open();
            }
            catch (Exception)
            {
                await DisplayAlert("Failed", "Could not connect", "OK");
                return;
            }

            try
            {
                SqlDataReader read = comm.ExecuteReader();

                if (!read.Read())
                {
                    con.Close();
                    read.Close();
                    await DisplayAlert("Error", "The specified ingredient does not exist", "OK");
                    return; 
                }
                else
                {
                    ingrid = read.GetInt32(0);
                    ingrname = read.GetString(1);
                }
                read.Close();
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Exception thrown while reading from database", "OK");
                return;
            }
            //Below assumes that the PantryID and UserID are equal
            query = String.Format("SELECT Quantity FROM PANTRY_INGREDIENTS WHERE IngredientID={0} AND PantryID={1}", ingrid, SqlHelper.UserID);
            comm = new SqlCommand(query, con);

            try
            {
                SqlDataReader read = comm.ExecuteReader();

                if (read.Read())
                {
                    alreadyexists = true;
                    count = read.GetInt32(0) + 1;
                }
                else
                {
                    alreadyexists = false;
                    count = 1;
                }
                read.Close();
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Exception thrown while reading from database", "OK");
                return;
            }

            //Below assumes that the PantryID and UserID are equal
            if (alreadyexists)
            {
                query = String.Format("UPDATE PANTRY_INGREDIENTS SET Quantity={0} WHERE PantryID={1} AND IngredientID={2};", count, SqlHelper.UserID, ingrid);
                IngredientItem p = _list.ListView.Single(x => x.ID == ingrid); //Finds the ingredient in the ListView that matches the user input
                
                //The quantity didn't update unless I removed it first
                int index = _list.ListView.IndexOf(p);
                _list.ListView.RemoveAt(index);
                p.Quantity += 1;
                _list.ListView.Insert(index, p);
            }
            else
            {
                query = String.Format("INSERT INTO PANTRY_INGREDIENTS VALUES({0}, {1}, '{2}', {3});", SqlHelper.UserID, ingrid, ingrname, 1);
                IngredientItem p = new IngredientItem(new Ingredient(ingrid, ingrname, ""), 1.0f, Measurements.Serving);

                _list.Add(p);
            }

            comm = new SqlCommand(query, con);

            comm.ExecuteNonQuery();
            con.Close();
        }

        private async void RemoveButton_Clicked(object sender, EventArgs e)
        {
            string ConnectionString = SqlHelper.GetConnectionString();
            string query = String.Format("SELECT IngredientID, Name FROM INGREDIENT WHERE Name='{0}'", IngredientEntry.Text);

            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand comm = new SqlCommand(query, con);

            int ingrid = -1;
            int count = 0;

            try
            {
                con.Open();
            }
            catch (Exception)
            {
                await DisplayAlert("Failed", "Could not connect", "OK");
                return;
            }

            try
            {
                SqlDataReader read = comm.ExecuteReader();

                if (!read.Read())
                {
                    con.Close();
                    read.Close();
                    await DisplayAlert("Error", "The specified ingredient does not exist", "OK");
                    return;
                }
                else
                {
                    ingrid = read.GetInt32(0);
                }
                read.Close();
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Exception thrown while reading from database", "OK");
                return;
            }

            query = String.Format("SELECT Quantity FROM PANTRY_INGREDIENTS WHERE IngredientID={0} AND PantryID={1}", ingrid, SqlHelper.UserID);
            comm = new SqlCommand(query, con);

            try
            {
                SqlDataReader read = comm.ExecuteReader();

                if (read.Read())
                {
                    count = read.GetInt32(0);
                }
                else
                {
                    await DisplayAlert("Error", "That ingredient is not in your pantry", "OK");
                }
                read.Close();
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Exception thrown while reading from database", "OK");
                return;
            }

            if (count == 1)
            {
                query = String.Format("DELETE FROM PANTRY_INGREDIENTS WHERE IngredientID={0} AND PantryID={1}", ingrid, SqlHelper.UserID);
                IngredientItem p = _list.ListView.Single(x => x.ID == ingrid); //Finds the ingredient in the ListView that matches the user input
                _list.ListView.Remove(p);
            }
            else
            {
                query = String.Format("UPDATE PANTRY_INGREDIENTS SET Quantity={0} WHERE IngredientID={1} AND PantryID={2};", count-1, ingrid, SqlHelper.UserID);

                IngredientItem p = _list.ListView.Single(x => x.ID == ingrid); //Finds the ingredient in the ListView that matches the user input

                //The quantity didn't update unless I removed it first
                int index = _list.ListView.IndexOf(p);
                _list.ListView.RemoveAt(index);
                p.Quantity -= 1;
                _list.ListView.Insert(index, p);
            }

            comm = new SqlCommand(query, con);
            comm.ExecuteNonQuery();

            con.Close();
        }
    }
}