using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using PantryAid.Configuration;
using PantryAid.Views;
using Xamarin.Forms;

namespace PantryAid
{
    public partial class App : Application
    {
        public App()
        {
            //Initialize
            InitializeComponent();

            //Configure Injection
            var container = ContainerConfig.Configure();

            AutofacServiceLocator asl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => asl);

            //Set the home page as navigation page
            MainPage = new NavigationPage(new SignInPage());
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
