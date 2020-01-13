using System;
using System.Collections.Generic;
using System.Text;

namespace PantryAid.Core.Models
{
    public class Recipe
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<IngredientItem> Ingredients { get; set; }
        public List<String> Instructions { get; set; }
        public List<FoodLabels> FoodType { get; set; }
        public string ImageURL { get; set; }
    }

    public enum FoodLabels
    {
        
    }
}
