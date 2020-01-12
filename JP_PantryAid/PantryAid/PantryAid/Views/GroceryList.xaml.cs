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
        //Currently the grocery list does not show a quantity or a longdesc or anything like that. If we decide to show them later then this form will need to be updated
        public GroceryList()
        {
            InitializeComponent();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            FilePath = path + FileName;

            File.Delete(FilePath); //This needs to be removed later, but for test purposes this will reset the file when you reopen the page
            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "67-Butter\n43-Cheese\n98-Noodles\n"); //Creates the file and adds test data
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

                IngredientItem G = new IngredientItem(new Ingredient(Convert.ToInt32(temp[0]), temp[1], ""), 0.0f, Measurements.Serving);
                _list.Add(G);
            }
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            string ConnectionString = SqlHelper.GetConnectionString();
            string query = String.Format("SELECT IngredientID, CommonName FROM INGREDIENT WHERE CommonName='{0}';", IngredientEntry.Text); 

            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand comm = new SqlCommand(query, con);

            try
            {
                con.Open();
            }
            catch (Exception)
            {
                await DisplayAlert("Failed", "Could not connect", "OK");
            }

            try
            {
                SqlDataReader read = comm.ExecuteReader();

                //This section assumes that no Ingredients have duplicate CommonNames
                if (read.Read())
                {
                    IngredientItem G = new IngredientItem(new Ingredient(read.GetInt32(0), read.GetString(1), ""), 0.0f, Measurements.Serving);
                    _list.Add(G);

                    File.AppendAllText(FilePath, String.Format("{0}-{1}\n", G.ID.ToString(), G.Name));
                }
                else
                {
                    await DisplayAlert("Error", "This Ingredient could not be found", "OK");
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Exception thrown while reading from database", "OK");
            }
            con.Close();
        }

        private async void RemoveButton_Clicked(object sender, EventArgs e)
        {
            string[] contents = File.ReadAllLines(FilePath);
            List<string> newcontents = new List<string>();
            string selecteditem = IngredientEntry.Text;

            //Find index of item to remove
            int i = 0;
            foreach (string line in contents)
            {
                string[] temp = line.Split('-');

                if (selecteditem == temp[1])
                {
                    break;
                }
                ++i;
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