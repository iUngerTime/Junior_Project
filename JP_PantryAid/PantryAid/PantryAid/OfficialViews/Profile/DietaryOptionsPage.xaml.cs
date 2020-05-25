using CommonServiceLocator;
using Database_Helpers;
using PantryAid.Core.Interfaces;
using PantryAid.Core.Models;
using PantryAid.Core.Utilities;
using PantryAid.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PantryAid.OfficialViews.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DietaryOptionsPage : ContentPage
    {        
        public ObservableCollection<DietPreference> DietPrefCol { get; set; }
        iUserDataRepo _database;

        public DietaryOptionsPage()
        {
            _database = new UserData(ServiceLocator.Current.GetInstance<iSqlServerDataAccess>());
            DietPrefCol = new ObservableCollection<DietPreference>();

            DietPrefCol.Add(new DietPreference { Name = "Gluten Free", Value = DietPreferences.GlutenFree});
            DietPrefCol.Add(new DietPreference { Name = "Ketogenic", Value = DietPreferences.Ketogenic });
            DietPrefCol.Add(new DietPreference { Name = "Vegetarian", Value = DietPreferences.Vegetarian });
            DietPrefCol.Add(new DietPreference { Name = "Lacto Vegetarian", Value = DietPreferences.LactoVegetarian });
            DietPrefCol.Add(new DietPreference { Name = "Ovo Vegetarian", Value = DietPreferences.OvoVegetarian });
            DietPrefCol.Add(new DietPreference { Name = "Vegan", Value = DietPreferences.Vegan });
            DietPrefCol.Add(new DietPreference { Name = "Pescetarian", Value = DietPreferences.Pescetarian });
            DietPrefCol.Add(new DietPreference { Name = "Paleo", Value = DietPreferences.Paleo });
            DietPrefCol.Add(new DietPreference { Name = "Primal", Value = DietPreferences.Primal });
            DietPrefCol.Add(new DietPreference { Name = "Whole 30", Value = DietPreferences.Whole30 });

            UpdateDietaryPreferencesToMatchUser();

            InitializeComponent();

            DietaryList.ItemsSource = DietPrefCol;
        }

        private async void UpdateUserDietaryPreferences(object sender, EventArgs e)
        {
            List<DietPreference> dietPref = GetDietPreferencesInForm();
            List<DietPreference> nonDietPref = GetNonDietPreferencesInForm();

            //Add alergies
            foreach (DietPreference agent in dietPref)
            {
                _database.AddDietPreference(SqlServerDataAccess.CurrentUser, agent.Value);
            }

            //Remove alergies
            foreach (DietPreference agent in nonDietPref)
            {
                _database.RemoveDietPreference(SqlServerDataAccess.CurrentUser, agent.Value);
            }

            await DisplayAlert("Finished", "Your dietary preferences have been saved.", "OK");
        }

        private void DietaryList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //Toggle Selection State
            DietPreference selPref = e.Item as DietPreference;
            selPref.IsSelected = !selPref.IsSelected;
        }

        private void UpdateDietaryPreferencesToMatchUser()
        {
            foreach (DietPreferences agent in SqlServerDataAccess.CurrentUser.DietaryPreferences)
            {
                //Corrosponding alergy gets updated
                DietPreference dietaryPref = DietPrefCol.Single(x => x.Value == agent);
                dietaryPref.IsSelected = true;
            }
        }

        private List<DietPreference> GetDietPreferencesInForm()
        {
            return DietPrefCol.Where(x => x.IsSelected == true).ToList<DietPreference>();
        }

        private List<DietPreference> GetNonDietPreferencesInForm()
        {
            return DietPrefCol.Where(x => x.IsSelected == false).ToList<DietPreference>();
        }
    }

    public class DietPreference : BaseViewModel
    {
        private string _name;
        private DietPreferences _value;
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
        public DietPreferences Value
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