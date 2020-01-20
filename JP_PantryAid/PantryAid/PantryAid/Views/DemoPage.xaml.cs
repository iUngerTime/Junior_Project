using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data.SqlClient;
using Database_Helpers;
using PantryAid.Core.Models;
using PantryAid.Views;
using System.Net;

namespace PantryAid
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DemoPage : ContentPage
    {
        public DemoPage()
        {
            InitializeComponent();
        }

        //private void IncrButton_Clicked(object sender, EventArgs e)
        //{
        //    int i = Convert.ToInt32(IncrLabel.Text);
        //    ++i;
        //    IncrLabel.Text = i.ToString();
        //}

        //private void DecrButton_Clicked(object sender, EventArgs e)
        //{
        //    int i = Convert.ToInt32(IncrLabel.Text);
        //    --i;
        //    IncrLabel.Text = i.ToString();
        //}

        private async void ConButton_Clicked(object sender, EventArgs e)
        {
            string query = "SELECT Name FROM INGREDIENT;";

            SqlConnection con = new SqlConnection(SqlHelper.GetConnectionString());
            SqlCommand comm = new SqlCommand(query, con);
            try
            {
                con.Open();

                SpoonacularAPI.SpoonacularAPI api = SpoonacularAPI.SpoonacularAPI.GetInstance();

                List<Recipe_Short> recipes = api.RecipeSearch("Banana", 1);

                SqlDataReader read = comm.ExecuteReader();
                read.Read();
                resultLabel1.Text = read.GetValue(0).ToString();
                read.Read();
                resultLabel2.Text = read.GetValue(0).ToString();
               
                resultLabel3.Text = recipes[0].title;
                resultLabel4.Text = recipes[0].id.ToString();
                Recipe_Full rFull = SpoonacularAPI.SpoonacularAPI.GetInstance().GetRecipeFull(recipes[0]);
                resultLabel5.Text = rFull.instructions;


                //read.Read();
                //resultLabel3.Text = read.GetValue(0).ToString();
                //read.Read();
                //resultLabel4.Text = read.GetValue(0).ToString();

                await DisplayAlert("Connected", "Successfully connected!", "YES");

                con.Close();
            }
            catch (Exception x)
            {

                await DisplayAlert("Failed", $"Successfully failed with exception {x}!", "NO");
            }
        }

        private void GListButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new GroceryList());
        }

        private void PantryButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PantryPage());
        }

        private void RecipeFinderButton_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushModalAsync(new RecipePage());
            Navigation.PushModalAsync(new ShortRecipePage());
        }
    }
}