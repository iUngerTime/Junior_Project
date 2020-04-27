using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data.SqlClient;
using Database_Helpers;
using PantryAid.Core.Models;
using PantryAid.Views;
using System.Net;

namespace PantryAid
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DemoPage : ContentPage
    {
        public DemoPage()
        {
            InitializeComponent();
        }

        private void GListButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new GroceryList());
        }

        private void PantryButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PantryPage());
        }

        private void RecipeFinderButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ShortRecipePage());
        }
        private void RecipeIngredientButton_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushModalAsync(new SearchByIngredients());
            Navigation.PushModalAsync(new PantryRecipePage());
        }
        

    }
}