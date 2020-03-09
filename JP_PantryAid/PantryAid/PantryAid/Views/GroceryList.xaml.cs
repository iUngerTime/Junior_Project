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
<Name>-<Quantity>
<Name>-<Quantity>
<Name>-<Quantity>
...

ex.
Milk-1
Eggs-5
Cookies-7
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

            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, ""); //Creates file
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
                string[] temp = line.Split('-'); //temp[0] holds the name and temp[1] holds the quantity

                IngredientItem G = new IngredientItem(new Ingredient(-1, temp[0]), Convert.ToDouble(temp[1]), Measurements.Serving);
                _list.Add(G);
            }
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("ADD", "Enter an ingredient to add to your list", "Add", "Cancel", "eg. Apple", 500, Keyboard.Text);

            if (result == null)
                return;

            bool valid = false;
            double fquant = 0;

            while (!valid)
            {
                string quant = await DisplayPromptAsync("How Much?", "Enter an quantity for the ingredient you just entered", "Add", "Cancel", "eg. 3", 10, Keyboard.Numeric);

                if (quant == null)
                    return;

                fquant = Convert.ToDouble(quant);
                if (fquant <= 0)
                {
                    valid = false;
                    await DisplayAlert("Error", "Invalid quantity\nPlease enter a positive number", "OK");
                }
                else
                    valid = true;
            }
            IngredientItem item = new IngredientItem(new Ingredient(-1, result), fquant, Measurements.Serving);
            _list.Add(item);

            File.AppendAllText(FilePath, String.Format("{0}-{1}\n", item.Name, item.Quantity));
            
        }

        private async void RemoveButton_Clicked(object sender, EventArgs e)
        {
            string selecteditem = await DisplayPromptAsync("REMOVE", "Enter an ingredient to remove from your list", "Remove", "Cancel", "eg. Apple", 500, Keyboard.Text);

            if (selecteditem == null)
                return;

            selecteditem = selecteditem.ToLower();

            string[] contents = File.ReadAllLines(FilePath);
            List<string> newcontents = new List<string>();

            bool found = false;

            //Find index of item to remove
            int i = 0;
            foreach (string line in contents)
            {
                string[] temp = line.Split('-');

                if (selecteditem == temp[0].ToLower())
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

        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            File.WriteAllText(FilePath, "");
            _list.ListView.Clear();
        }

        private async void DumpButton_Clicked(object sender, EventArgs e)
        {
            List<IngredientItem> LostIngredients = new List<IngredientItem>(); //Keeps track of the entries that are not dumped because they don't exist in the database
            iIngredientData ingrdata = new IngredientData(new SqlServerDataAccess());

            foreach (IngredientItem item in _list.ListView)
            {
                Ingredient ingr = ingrdata.GetIngredient(item.Name.ToLower());

                if (ingr == null)
                    LostIngredients.Add(item);
                else
                    ingrdata.AddIngredientToPantry(SqlHelper.UserID, ingr, item.Quantity);
            }

            string alert = "The following ingredients could not be added to your pantry:\n";

            foreach (IngredientItem item in LostIngredients)
            {
                alert += item.Name + "\n";
            }
            if (LostIngredients.Count == 0)
                alert = "Successfully dumped grocery list!";

            await DisplayAlert("Grocery Dump", alert, "OK");
        }
    }
}