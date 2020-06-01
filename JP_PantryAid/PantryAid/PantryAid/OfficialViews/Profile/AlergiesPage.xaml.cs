using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PantryAid.OfficialViews.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlergiesPage : ContentPage
    {
        public ObservableCollection<Alergy> Alergens { get; set; }

        public AlergiesPage()
        {
            Alergens = new ObservableCollection<Alergy>();

            Alergens.Add(new Alergy { Name = "Dairy" });
            Alergens.Add(new Alergy { Name = "Egg" });
            Alergens.Add(new Alergy { Name = "Gluten" });
            Alergens.Add(new Alergy { Name = "Grain" });
            Alergens.Add(new Alergy { Name = "Peanut" });
            Alergens.Add(new Alergy { Name = "Seafood" });
            Alergens.Add(new Alergy { Name = "Sesame" });
            Alergens.Add(new Alergy { Name = "Shellfish" });
            Alergens.Add(new Alergy { Name = "Soy" });
            Alergens.Add(new Alergy { Name = "Sulfite" });
            Alergens.Add(new Alergy { Name = "Tree Nut" });
            Alergens.Add(new Alergy { Name = "Wheat" });

            InitializeComponent();

            AlergyList.ItemsSource = Alergens;
        }

        private void UpdateUserAlergyPreferences(object sender, EventArgs e)
        {
            
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            
        }
    }

    public class Alergy
    {
        public string Name { get; set; }
    }
}