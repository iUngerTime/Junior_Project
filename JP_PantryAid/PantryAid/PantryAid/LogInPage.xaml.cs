using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            //Open DemoPage.xaml Form as a new stack
            Navigation.PushModalAsync(new NavigationPage(new DemoPage()));
        }
    }
}