using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Database_Helpers;

namespace PantryAid
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    {
        public LogInPage()
        {
            InitializeComponent();
        }
        private void LogInClick(object sender, EventArgs e)
        {
            //bool for authenticated or not
            bool authenticated = AuthenticateUser();

            if (authenticated)
            {
                //Open DemoPage.xaml Form as a new stack
                Navigation.PushModalAsync(new NavigationPage(new DemoPage()));
            }
            else
            {
                DisplayAlert("ERROR", "That is an invalid email", "Return");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True if user was authenticated correctly, False if not</returns>
        private bool AuthenticateUser()
        {
            bool auth = false;

            //Run SQL command to get user's email
            string query = "SELECT UserID, Email FROM Person WHERE LOWER(Email) = '" + emailBox.Text + "';";
            
            SqlConnection con = new SqlConnection(SqlHelper.GetConnectionString());
            SqlCommand comm = new SqlCommand(query, con);
            

            try
            {
                con.Open();

                SqlDataReader read = comm.ExecuteReader();

                if (read.Read())
                    auth = true;

                con.Close();
            }
            catch (Exception)
            { }

            return auth;
        }
    }
}