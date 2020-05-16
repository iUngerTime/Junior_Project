using CommonServiceLocator;
using PantryAid.Core.Interfaces;
using PantryAid.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PantryAid.OfficialViews.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            //Connect View model
            var vm = new UserPageViewModel(Navigation, ServiceLocator.Current.GetInstance<iUserDataRepo>());
            this.BindingContext = vm;

            InitializeComponent();
        }
    }
}