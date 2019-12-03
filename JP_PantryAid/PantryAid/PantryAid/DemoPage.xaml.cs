using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data.SqlClient;

namespace PantryAid
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DemoPage : ContentPage
    {
        public DemoPage()
        {
            InitializeComponent();
        }

        private void IncrButton_Clicked(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(IncrLabel.Text);
            ++i;
            IncrLabel.Text = i.ToString();
        }

        private void DecrButton_Clicked(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(IncrLabel.Text);
            --i;
            IncrLabel.Text = i.ToString();
        }

        private async void ConButton_Clicked(object sender, EventArgs e)
        {
            string ConnectionString = "server=aura.cset.oit.edu, 5433; database=iUngerTime; UID=iUngerTime; password=iUngerTime";
            string query = "SELECT CommonName FROM INGREDIENT;";

            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand comm = new SqlCommand(query, con);
            try
            {
                con.Open();
                
                SqlDataReader read = comm.ExecuteReader();
                if (read.Read())
                    resultLabel.Text = read.GetValue(0).ToString();
                await DisplayAlert("Connected", "Successfully connected!", "YES");

                con.Close();
            }
            catch (Exception)
            {
                await DisplayAlert("NOT YEET", "Successfully failed!", "NO");
            }
        }
    }
}