using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Database_Helpers;
using PantryAid.Core.Interfaces;
using PantryAid.Core.Models;
using PantryAid.Views;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace PantryAid.ViewModels
{
    public class GroceryListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private iIngredientData _ingredientDatabaseAccess;


        public INavigation navigation { get; set; }

        //Properties
        private string FilePath;
        private string FileName = "GroceryList";


        public GroceryListViewModel(INavigation nav, iIngredientData databaseAccess)
        {
            FilePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + FileName;

            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, ""); //Creates file
            }

            // Navigation and command binding
            this.navigation = nav;
            //AddCommand = new Command(OnAdd);

            // Dependency injection
            _ingredientDatabaseAccess = databaseAccess;
        }

        // View Model getter and setters and properties
        private ListViewModel<IngredientItem> _glist = new ListViewModel<IngredientItem>();
        public ListViewModel<IngredientItem> GList
        {
            get { return _glist; }
            set
            {
                _glist = value;
                PropertyChanged(this, new PropertyChangedEventArgs("GList"));
            }
        }

        //Keeps track of which ingredients have their boxes checked
        private List<IngredientItem> checks = new List<IngredientItem>();
        public List<IngredientItem> Checks
        {
            get { return checks; }
            set
            {
                checks = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Checks"));
            }
        }

        //
        // end Properties

        //public Action DisplayCommand { set; get; }

        public void FillGrid()
        {
            GList.ListView.Clear();

            string[] contents = File.ReadAllLines(FilePath);

            foreach (string line in contents)
            {
                string[] temp = line.Split('-'); //temp[0] holds the name, temp[1] holds the quantity, and temp[2] holds the measurement

                IngredientItem I = new IngredientItem(new Ingredient(-1, temp[0]), Convert.ToDouble(temp[1]), temp[2]);
                GList.Add(I);
            }
        }

        public async void OnAdd(string ingrname, double quant, string measure)
        {
            ingrname = ingrname.ToLower();
            ingrname = SqlServerDataAccess.Sanitize(ingrname);
            
            IngredientItem item = new IngredientItem(new Ingredient(-1, ingrname), quant, measure);
            GList.Add(item);

            File.AppendAllText(FilePath, String.Format("{0}-{1}-{2}\n", item.Name, item.Quantity, item.Measurement));
        }

        public async void OnDelete()
        {
            //Remove the items from the GList that were checked
            foreach (IngredientItem ingr in Checks)
            {
                GList.ListView.Remove(ingr);
            }

            //Wipe the file
            File.WriteAllText(FilePath, "");

            //Rewrite the file from the contents of the list view model
            foreach (IngredientItem item in GList.ListView)
            {
                File.AppendAllText(FilePath, String.Format("{0}-{1}-{2}\n", item.Name, item.Quantity, item.Measurement));
            }
            Checks.Clear();
        }

        public async void OnDump()
        {
            List<IngredientItem> LostIngredients = new List<IngredientItem>(); //Keeps track of the entries that are not dumped because they don't exist in the database
            iIngredientData ingrdata = new IngredientData(new SqlServerDataAccess());

            foreach (IngredientItem item in Checks)
            {
                Ingredient ingr = ingrdata.GetIngredient(item.Name.ToLower());

                if (ingr == null)
                    LostIngredients.Add(item);
                else
                    ingrdata.AddIngredientToPantry(SqlServerDataAccess.UserID, ingr, item.Measurement, item.Quantity);
            }

            //Commented out until I can ask brent how to do alert displays in the vm

            /*string alert = "The following ingredients could not be added to your pantry:\n";

            foreach (IngredientItem item in LostIngredients)
            {
                alert += item.Name + "\n";
            }
            if (LostIngredients.Count == 0)
                alert = "Successfully dumped grocery list!";

            //await DisplayAlert("Grocery Dump", alert, "OK");*/
            Checks.Clear();
        }

        public void OnPlus(Entry QuantEntry)
        {
            QuantEntry.Text = (Convert.ToDouble(QuantEntry.Text) + 1).ToString();
        }

        public void OnMinus(Entry QuantEntry)
        {
            if (Convert.ToDouble(QuantEntry.Text) > 1)
                QuantEntry.Text = (Convert.ToDouble(QuantEntry.Text) - 1).ToString();
        }

        public async void OnChecked(CheckBox sender, IngredientItem ingr, Frame popup)
        {
            if (sender.IsChecked)
                Checks.Add(ingr);
            else
                Checks.Remove(ingr);

            //If the popup isn't there and there's at least one item checked
            if (!popup.IsVisible && Checks.Count > 0)
            {
                popup.IsVisible = true;
                popup.AnchorX = 1;
                popup.AnchorY = 1;

                Animation scaleAnimation = new Animation(f => popup.Scale = f, 0.5, 1, Easing.SinInOut);
                Animation fadeAnimation = new Animation(f => popup.Opacity = f, 0.2, 1, Easing.SinInOut);

                scaleAnimation.Commit(popup, "popupScaleAnimation", 25, 25);
                fadeAnimation.Commit(popup, "popupFadeAnimation", 25, 50);
            }
            else if (Checks.Count < 1)
            {
                RemovePopup(popup);
            }
        }

        public async void RemovePopup(Frame popup)
        {
            popup.IsVisible = false;
            await Task.WhenAny<bool>
                (
                popup.FadeTo(0, 25, Easing.SinInOut)
                );
        }
    }
}
