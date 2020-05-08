using CommonServiceLocator;
using PantryAid.Core.Interfaces;
using PantryAid.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PantryAid.OfficialViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            var vm = new LoginViewModel(Navigation, ServiceLocator.Current.GetInstance<iUserDataRepo>());
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Login, try again", "OK");
            vm.DisplaySuccessfulSignupPrompt += () => DisplayAlert("Success", "Your account has been successfully created!", "OK");
            InitializeComponent();

            Email.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };

            LoginButton.Clicked += (object sender, EventArgs e) =>
            {
                vm.SubmitCommand.Execute(null);
            };

            SignupButton.Clicked += (object sender, EventArgs e) =>
            {
                vm.SignupCommand.Execute(null);
            };
        }
    }
}