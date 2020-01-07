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
    public class GroceryItem
    {
        public Ingredient _ingredient;
        public int _userid;

        public GroceryItem(Ingredient ingr, int uid)
        {
            _ingredient = ingr;
            _userid = uid;
        }

        //The below setters and getters are required for the datagrid to see the ingredient variables
        public string Name
        {
            get { return _ingredient.CommonName; }
            set { _ingredient.CommonName = value; }
        }

        public int ID
        {
            get { return _ingredient.IngredientID; }
            set { _ingredient.IngredientID = value; }
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroceryList : ContentPage
    {
        string FilePath;
        string FileName = "GroceryList";

        ListViewModel<GroceryItem> _list = new ListViewModel<GroceryItem>();
        public GroceryList()
        {
            InitializeComponent();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            FilePath = path + FileName;

            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "67-Butter\n43-Cheese\n98-Noodles\n"); //Creates the file and adds test data
            } 

            this.BindingContext = _list;
            FillGrid();
        }

        public void FillGrid()
        {
            string[] contents = File.ReadAllLines(FilePath);

            foreach (string line in contents)
            {
                string[] temp = line.Split('-'); //temp should always have only two strings contained after this point, the ID and the Name

                GroceryItem G = new GroceryItem(new Ingredient(Convert.ToInt32(temp[0]), temp[1]), SqlHelper.UserID);
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
                    GroceryItem G = new GroceryItem(new Ingredient(read.GetInt32(0), read.GetString(1)), SqlHelper.UserID);
                    _list.Add(G);

                    File.AppendAllText(FilePath, String.Format("{0}-{1}\n", G._ingredient.IngredientID.ToString(), G._ingredient.CommonName));
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
            await DisplayAlert("Warning", "This button currently does absolutely nothing", "OK");
        }
    }
}