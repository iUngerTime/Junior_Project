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

/*
The file that holds ingredients for the grocery list is in the format
<Name>-<Quantity>-<Measurement>
<Name>-<Quantity>-<Measurement>
<Name>-<Quantity>-<Measurement>
...

ex.
Milk-1-Quarts
Eggs-5-Servings
Cookies-7-Servings
...
*/

namespace PantryAid.OfficialViews.Grocery_List
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroceryListPage : BaseNavigationPage
    {
        public GroceryListViewModel vm;
        public GroceryListPage()
        {
            InitializeComponent();
            vm = new GroceryListViewModel(Navigation, ServiceLocator.Current.GetInstance<iIngredientData>());
            //vm.DisplayCommand += (string title, string msg, string cancel) => DisplayAlert(title, msg, cancel);
            this.BindingContext = vm;
            vm.FillGrid();
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            if (ItemEntry.Text != null)
                vm.OnAdd(ItemEntry.Text, Math.Abs(Convert.ToDouble(QuantityEntry.Text)), MeasurementPicker.SelectedItem as string);
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

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Are You Sure?", "Do you want to delete all checked items?", "Yes", "No");
            if (answer == true)
            {
                vm.OnDelete();
                //Remove the popup manually since the checks have been cleared
                vm.RemovePopup(popup);
            }
        }

        private void Dump_Clicked(object sender, EventArgs e)
        {
            if (DisplayAlert("Move To Pantry", "This action may take a long time to complete\nAre you sure?", "YES", "NO").IsCanceled)
                return;
            
            vm.OnDump();
            vm.RemovePopup(popup);
        }
    }
}