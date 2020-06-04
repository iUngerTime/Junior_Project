using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using PantryAid.Core.Interfaces;
using PantryAid.Core.Models;
using PantryAid.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PantryAid.OfficialViews.Recipe_Finder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDetailsPage : ContentPage
    {
        public RecipeDetailsPageViewModel vm;
        public RecipeDetailsPage(int RecipeId)
        {
            vm = new RecipeDetailsPageViewModel(Navigation, ServiceLocator.Current.GetInstance<iUserDataRepo>(), RecipeId);
            this.BindingContext = vm;
            InitializeComponent();
            foreach (var Amount in vm.RecipeFull.extendedIngredients)
            {
                Amount.amount = Math.Round(Amount.amount, 2);
            }
            
        }

        public RecipeDetailsPage(Recipe_Full recipe)
        {
            vm = new RecipeDetailsPageViewModel(Navigation, ServiceLocator.Current.GetInstance<iUserDataRepo>(), recipe);
            this.BindingContext = vm;
            InitializeComponent();
            foreach (var Amount in vm.RecipeFull.extendedIngredients)
            {
                Amount.amount = Math.Round(Amount.amount, 2);
            }

        }

        private void B_SimilarClicked(object sender, EventArgs e)
        {
            //TODO
        }

        private void CheckBox_DislikedCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            //if the user disliked the recipe
            if(CheckBox_Disliked.IsChecked == true)
            {
                if(CheckBox_Liked.IsChecked == true)
                {
                    //remove the recipe from like recipes if it is liked
                    vm.RemoveRecipeFromPreferedList(vm.RecipeFull.id);
                    CheckBox_Liked.IsChecked = false;
                }
                vm.AddRecipeToDislikedList(vm.RecipeFull.id);

            }
            else
            {
                vm.RemoveRecipeFromDislikedList(vm.RecipeFull.id);
            }
        }
        private void CheckBox_LikedCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            //if the user disliked the recipe
            if (CheckBox_Liked.IsChecked == true)
            {
                if (CheckBox_Disliked.IsChecked == true)
                {
                    //remove the recipe from like recipes if it is liked
                    vm.RemoveRecipeFromDislikedList(vm.RecipeFull.id);
                    CheckBox_Disliked.IsChecked = false;
                }
                vm.AddRecipeToPreferedList(vm.RecipeFull.id);

            }
            else
            {
                vm.RemoveRecipeFromPreferedList(vm.RecipeFull.id);
            }
        }
    }
}