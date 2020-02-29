using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using PantryAid.Core.Models;
using Database_Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using PantryAid.Core.Interfaces;


/*
The file that holds ingredients for the grocery list is in the format
<ID>-<Name>
<ID>-<Name>
<ID>-<Name>
...

ex.
1-Milk
2-Eggs
3-Cookies
...
*/
namespace PantryAid
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroceryList : ContentPage
    {
        string FilePath;
        string FileName = "GroceryList";

        ListViewModel<IngredientItem> _list = new ListViewModel<IngredientItem>();
        public GroceryList()
        {
            InitializeComponent();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            FilePath = path + FileName;

            File.Delete(FilePath); //This needs to be removed later, but for test purposes this will reset the file when you reopen the page
            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "");
            } 

            this.BindingContext = _list;
            FillGrid();
        }

        public void FillGrid()
        {
            _list.ListView.Clear();

            string[] contents = File.ReadAllLines(FilePath);

            foreach (string line in contents)
            {
                string[] temp = line.Split('-'); //temp should always have only two strings contained after this point, the ID and the Name

                IngredientItem G = new IngredientItem(new Ingredient(Convert.ToInt32(temp[0]), temp[1]), 0.0f, Measurements.Serving);
                _list.Add(G);
            }
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            IngredientData ingrdata = new IngredientData(new SqlServerDataAccess());
            Ingredient foundingr = ingrdata.GetIngredient(IngredientEntry.Text.ToLower());

            if (foundingr == null)
            {
                await DisplayAlert("Error", "That ingredient could not be found", "OK");
                return;
            }
            else
            {
                IngredientItem item = new IngredientItem(foundingr, 1.0f, Measurements.Serving);
                _list.Add(item);

                File.AppendAllText(FilePath, String.Format("{0}-{1}\n", item.ID.ToString(), item.Name));
            }
        }

        private async void RemoveButton_Clicked(object sender, EventArgs e)
        {
            string[] contents = File.ReadAllLines(FilePath);
            List<string> newcontents = new List<string>();
            string selecteditem = IngredientEntry.Text.ToLower();
            bool found = false;

            //Find index of item to remove
            int i = 0;
            foreach (string line in contents)
            {
                string[] temp = line.Split('-');

                if (selecteditem == temp[1].ToLower())
                {
                    found = true;
                    break;
                }
                ++i;
            }

            if (found == false)
            {
                await DisplayAlert("Error", "Could not find that ingredient", "OK");
                return;
            }

            //Create a new array of all contents except for the one at index i
            foreach (string line in contents)
            {
                if (line != contents[i])
                {
                    newcontents.Add(line);
                }
            }

            File.WriteAllLines(FilePath, newcontents.ToArray());

            FillGrid();
        }
    }
}