using System;
using System.Collections.Generic;
using System.Text;

namespace PantryAid.Core.Models
{
    public class User
    {
        public string Handle { get; set; }
        public string email { get; set; }
        public List<Ingredient> Alergies { get; set; }
        public List<Ingredient> FavoriteIngredients { get; set; }
        public List<Recipe> FavoriteRecipes { get; set; }
        public List<Recipe> DislikedRecipes { get; set; }

    }
}
