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

namespace PantryAid.OfficialViews.Grocery_List
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroceryListPage : ContentPage
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
            vm.OnAdd(ItemEntry.Text, Convert.ToInt32(QuantityEntry.Text), MeasurementPicker.SelectedItem as string);
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
            vm.OnChecked((CheckBox)sender, ob.Name, popup);
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            vm.OnDelete();
        }

        private void Dump_Clicked(object sender, EventArgs e)
        {
            vm.OnDump();
        }
    }
}