using System;
using System.Collections.Generic;
using System.Text;

namespace PantryAid.Core.Models
{
    public class Ingredient
    {
        public Ingredient(int id, string name) //List<FoodLabels> groups = null
        {
            IngredientID = id;
            Name = name;
            //FoodGroups = groups;
        }
        public int IngredientID { get; set; }
        public string Name { get; set; }
        //public List<FoodLabels> FoodGroups { get; set; }
    }
}
