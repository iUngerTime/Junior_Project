using CommonServiceLocator;
using Database_Helpers;
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
    public partial class DislikedRecipesPage : ContentPage
    {
        public RecipeListManagerViewModel vm;

        public DislikedRecipesPage()
        {
            vm = new RecipeListManagerViewModel(Navigation, ServiceLocator.Current.GetInstance<iUserDataRepo>(), SqlServerDataAccess.CurrentUser.DislikedRecipes);
            this.BindingContext = vm;

            InitializeComponent();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            vm.ItemTapped(e.ItemIndex);
        }

        private void PrevButton_Clicked(object sender, EventArgs e)
        {
            vm.PrevPage();
        }

        private void NextButton_Clicked(object sender, EventArgs e)
        {
            vm.NextPage();
        }
    }
}