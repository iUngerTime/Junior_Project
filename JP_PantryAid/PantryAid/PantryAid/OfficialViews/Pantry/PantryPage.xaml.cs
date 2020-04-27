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
            vm.OnAdd(sender, Item.Text, Convert.ToInt32(Quantity.Text));
        }
        private void RemoveButton_Clicked(object sender, EventArgs e)
        {

        }
        private void QuantityChangeClicked(object sender, EventArgs e)
        {

        }
    }
}