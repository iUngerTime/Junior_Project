using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using Database_Helpers;
using PantryAid.Configuration;
using PantryAid.OfficialViews;
using PantryAid.Views;
using Xamarin.Forms;

namespace PantryAid
{
    public partial class App : Application
    {
        //FOR DEBUG PURPOSES
        bool _debugMode = true;

        public App()
        {
            //Initialize
            InitializeComponent();

            //Configure Injection
            var container = ContainerConfig.Configure();

            AutofacServiceLocator asl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => asl);


            //Sets start up page based on debug mode set or not
            if (_debugMode)
            {
                //Set user to user id of Brenton
                SqlServerDataAccess.UserID = 1;
                MainPage = new TabbedMasterPage();
            }
            else
            {
                //Set the home page as navigation page
                MainPage = new NavigationPage(new SignInPage());
            }
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
