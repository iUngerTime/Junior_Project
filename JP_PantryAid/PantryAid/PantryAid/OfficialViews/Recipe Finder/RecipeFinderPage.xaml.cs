using PantryAid.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace PantryAid.OfficialViews.Recipe_Finder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeFinderPage : BaseNavigationPage
    {
        public RecipeFinderViewModel vm;
        //public ListViewModel<Recipe_Short> _list = new ListViewModel<Recipe_Short>();

        public RecipeFinderPage()
        {
            vm = new RecipeFinderViewModel(Navigation);
            this.BindingContext = vm;
            //this.BindingContext = vm._list;
            InitializeComponent();
            //this.Resources.Add(StyleSheet.FromResource("Assets/StyleSheets.css",this.GetType().Assembly));
            if (Preferences.Get("Images", false) == false)
            {
                BGImage.Opacity = 0;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.OnAppear();
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e) 
        {
            //Navigation.PushModalAsync(new RecipePage(Convert.ToInt32(vm._list.ListView[].id)));
            vm.ItemTapped(e.ItemIndex);
        }

        private void Recipe_Search_OnCompleted(object sender, EventArgs e)
        {
            vm.SearchByName(Recipe_Search.Text);
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