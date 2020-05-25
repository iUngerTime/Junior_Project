using PantryAid.Core.Models;
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
        public ObservableCollection<Alergy> AlergiesCol { get; set; }

        public AlergiesPage()
        {
            AlergiesCol = new ObservableCollection<Alergy>();

            AlergiesCol.Add(new Alergy { Name = "Dairy", Value = Alergens.Dairy});
            AlergiesCol.Add(new Alergy { Name = "Egg", Value = Alergens.Egg });
            AlergiesCol.Add(new Alergy { Name = "Gluten", Value = Alergens.Gluten });
            AlergiesCol.Add(new Alergy { Name = "Grain", Value = Alergens.Grain });
            AlergiesCol.Add(new Alergy { Name = "Peanut", Value = Alergens.Peanut });
            AlergiesCol.Add(new Alergy { Name = "Seafood", Value = Alergens.Seafood });
            AlergiesCol.Add(new Alergy { Name = "Sesame", Value = Alergens.Sesame });
            AlergiesCol.Add(new Alergy { Name = "Shellfish", Value = Alergens.Shellfish });
            AlergiesCol.Add(new Alergy { Name = "Soy", Value = Alergens.Soy });
            AlergiesCol.Add(new Alergy { Name = "Sulfite", Value = Alergens.Sulfite });
            AlergiesCol.Add(new Alergy { Name = "Tree Nut", Value = Alergens.TreeNut });
            AlergiesCol.Add(new Alergy { Name = "Wheat", Value = Alergens.Wheat });

            InitializeComponent();

            AlergyList.ItemsSource = AlergiesCol;
        }

        private void UpdateUserAlergyPreferences(object sender, EventArgs e)
        {
        }

        private void AlergyList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Alergy selAlergy = e.Item as Alergy;

            Debug.WriteLine("Item name: " + selAlergy.Name);
            Debug.WriteLine("Item Value: " + selAlergy.Value);
        }
    }

    public class Alergy
    {
        public string Name { get; set; }
        public Alergens Value { get; set; }
    }
}