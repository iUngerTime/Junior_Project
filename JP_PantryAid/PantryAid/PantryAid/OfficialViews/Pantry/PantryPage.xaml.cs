using CommonServiceLocator;
using PantryAid.Core.Interfaces;
using PantryAid.Core.Models;
using PantryAid.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PantryAid.OfficialViews.Pantry
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PantryPage : BaseNavigationPage
    {
        public PantryViewModel vm;
        public PantryPage()
        {
            InitializeComponent();
            vm = new PantryViewModel(Navigation, ServiceLocator.Current.GetInstance<iIngredientData>());

            this.BindingContext = vm;
            vm.FillGrid();
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            if (ItemEntry.Text != null)
                vm.OnAdd(sender, ItemEntry.Text, QuantityEntry.Text, MeasurementPicker.SelectedItem as string);
        }
        private async void RemoveButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Are You Sure?", "Do you want to delete all checked items?", "Yes", "No");
            if (answer == true)
            {
                vm.OnRemove();
                //Remove the popup manually since the checks have been cleared
                vm.RemovePopup(popup);
            }
        }
        private void QuantityChange_Clicked(object sender, EventArgs e)
        {
            vm.QuantityChanged(sender);
        }
        private void Minus_Clicked(object sender, EventArgs e)
        {
            vm.OnMinus(QuantityEntry);
        }
        private void Plus_Clicked(object sender, EventArgs e)
        {
            vm.OnPlus(QuantityEntry);
        }
        private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var ob = ((CheckBox)sender).BindingContext as IngredientItem;
            vm.OnChecked((CheckBox)sender, ob, popup);
        }
    }
}