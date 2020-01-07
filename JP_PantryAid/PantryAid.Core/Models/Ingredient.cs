using System;
using System.Collections.Generic;
using System.Text;

namespace PantryAid.Core.Models
{
    public class Ingredient
    {
        public Ingredient(int id, string name)
        {
            IngredientID = id;
            CommonName = name;
        }
        public int IngredientID { get; set; }
        public string CommonName { get; set; }
    }
}
