using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            vm = new RecipeDetailsPageViewModel(Navigation, RecipeId);
            this.BindingContext = vm;
            InitializeComponent();
            
        }

        private void B_SimilarClicked(object sender, EventArgs e)
        {
            //TODO
        }
    }
}