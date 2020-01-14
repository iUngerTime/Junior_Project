using System;
using System.Collections.Generic;
using System.Text;

namespace PantryAid.Core.Models
{
    public class Recipe_Full : Recipe_Short
    {
        //Data
        public List<IngredientItem> Ingredients { get; set; }
        public string Instructions { get; set; }
        public List<FoodLabels> FoodType { get; set; }

        //Ctors
        public Recipe_Full() { Ingredients = new List<IngredientItem>(); }

        public Recipe_Full(Recipe_Full Rfull)
        {
            Id = Rfull.Id;
            ImageURL = Rfull.ImageURL;
            readyInMinutes = Rfull.readyInMinutes;
            servings = Rfull.readyInMinutes;
            Name = Rfull.Name;
            Ingredients = Rfull.Ingredients;
            Instructions = Rfull.Instructions;
            FoodType = Rfull.FoodType;
        }

    }

    public enum FoodLabels
    {
        
    }
}
