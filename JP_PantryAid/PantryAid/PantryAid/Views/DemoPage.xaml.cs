using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data.SqlClient;
using Database_Helpers;

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
            string query = "SELECT CommonName FROM INGREDIENT;";

            SqlConnection con = new SqlConnection(SqlHelper.GetConnectionString());
            SqlCommand comm = new SqlCommand(query, con);
            try
            {
                con.Open();
                
                SqlDataReader read = comm.ExecuteReader();
                read.Read();
                resultLabel1.Text = read.GetValue(0).ToString();
                read.Read();
                resultLabel2.Text = read.GetValue(0).ToString();
                read.Read();
                resultLabel3.Text = read.GetValue(0).ToString();
                read.Read();
                resultLabel4.Text = read.GetValue(0).ToString();

                await DisplayAlert("Connected", "Successfully connected!", "YES");

                con.Close();
            }
            catch (Exception)
            {
                await DisplayAlert("Failed", "Successfully failed!", "NO");
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
            Navigation.PushModalAsync(new RecipePage());
        }
    }
}