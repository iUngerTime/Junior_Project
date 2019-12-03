using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PantryAid
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //Set the home page as navigation page
            MainPage = new NavigationPage(new LogInPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
