using Database_Helpers;
using PantryAid.OfficialViews.Profile;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PantryAid.OfficialViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseNavigationPage : ContentPage
    {
        public BaseNavigationPage()
        {
            InitializeComponent();
        }

        private async void NavigateToAccountPage(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ProfilePage()));
        }

        private async void NavigateToAboutPage(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AboutPage());
        }

        private async void LogoutOfApplication(object sender, EventArgs e)
        {
            //Set the selected user ID to null
            SqlServerDataAccess.UserID = -1;
            
            //Remove the Tabbed master page
            await Navigation.PopModalAsync();
        }

        private void ExitApplication(object sender, EventArgs e)
        {
            //Kill the application
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}