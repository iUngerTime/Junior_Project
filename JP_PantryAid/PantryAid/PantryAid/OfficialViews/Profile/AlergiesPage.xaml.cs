using CommonServiceLocator;
using Database_Helpers;
using PantryAid.Core.Interfaces;
using PantryAid.Core.Models;
using PantryAid.Core.Utilities;
using PantryAid.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
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
        iUserDataRepo _database;

        public AlergiesPage()
        {
            _database = new UserData(ServiceLocator.Current.GetInstance<iSqlServerDataAccess>());
            AlergiesCol = new ObservableCollection<Alergy>();

            AlergiesCol.Add(new Alergy { Name = "Dairy", Value = Alergens.Dairy});
            AlergiesCol.Add(new Alergy { Name = "Egg", Value = Alergens.Egg});
            AlergiesCol.Add(new Alergy { Name = "Gluten", Value = Alergens.Gluten});
            AlergiesCol.Add(new Alergy { Name = "Grain", Value = Alergens.Grain});
            AlergiesCol.Add(new Alergy { Name = "Peanut", Value = Alergens.Peanut});
            AlergiesCol.Add(new Alergy { Name = "Seafood", Value = Alergens.Seafood});
            AlergiesCol.Add(new Alergy { Name = "Sesame", Value = Alergens.Sesame});
            AlergiesCol.Add(new Alergy { Name = "Shellfish", Value = Alergens.Shellfish});
            AlergiesCol.Add(new Alergy { Name = "Soy", Value = Alergens.Soy});
            AlergiesCol.Add(new Alergy { Name = "Sulfite", Value = Alergens.Sulfite});
            AlergiesCol.Add(new Alergy { Name = "Tree Nut", Value = Alergens.TreeNut});
            AlergiesCol.Add(new Alergy { Name = "Wheat", Value = Alergens.Wheat});

            UpdateAlergiesToMatchUser();

            InitializeComponent();

            AlergyList.ItemsSource = AlergiesCol;
        }

        private async void UpdateUserAlergyPreferences(object sender, EventArgs e)
        {
            List<Alergy> alergies = GetAlergiesInForm();
            List<Alergy> nonAlergies = GetNonAlergiesInForm();

            //Add alergies
            foreach (Alergy alergy in alergies)
            {
                _database.AddAlergy(SqlServerDataAccess.CurrentUser, alergy.Value);
            }

            //Remove alergies
            foreach (Alergy alergy in nonAlergies)
            {
                _database.RemoveAlergy(SqlServerDataAccess.CurrentUser, alergy.Value);
            }

            await DisplayAlert("Finished", "Your alergy preferences have been saved.", "OK");
        }

        private void AlergyList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //Toggle Selection State
            Alergy selAlergy = e.Item as Alergy;
            selAlergy.IsSelected = !selAlergy.IsSelected;

            Debug.WriteLine("Checked: " + selAlergy.IsSelected.ToString());
            Debug.WriteLine("Item name: " + selAlergy.Name);
            Debug.WriteLine("Item Value: " + (int)selAlergy.Value);
        }

        private void UpdateAlergiesToMatchUser()
        {
            foreach(Alergens agent in SqlServerDataAccess.CurrentUser.Allergies)
            {
                //Corrosponding alergy gets updated
                Alergy alergy = AlergiesCol.Single(x => x.Value == agent);
                alergy.IsSelected = true;
            }
        }

        private List<Alergy> GetAlergiesInForm()
        {
            return AlergiesCol.Where(x => x.IsSelected == true).ToList<Alergy>();
        }

        private List<Alergy> GetNonAlergiesInForm()
        {
            return AlergiesCol.Where(x => x.IsSelected == false).ToList<Alergy>();
        }
    }

    public class Alergy : BaseViewModel
    {
        private string _name;
        private Alergens _value;
        private bool _isSelected;

        public string Name 
        { 
            get { return _name; } 
            set 
            { 
                _name = value; 
                NotifyPropertyChanged("Name");
            } 
        }
        public Alergens Value
        {
            get { return _value; }
            set
            {
                _value = value;
                NotifyPropertyChanged("Value");
            }
        }
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }
    }
}