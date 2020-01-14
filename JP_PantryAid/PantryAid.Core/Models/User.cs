using System;
using System.Collections.Generic;
using System.Text;

namespace PantryAid.Core.Models
{
    /// <summary>
    /// Model to contain information pertainent to a user
    /// </summary>
    public class User
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<Ingredient> Alergies { get; set; }
        public List<Recipe_Full> FavoriteRecipes { get; set; }
        public List<Recipe_Full> DislikedRecipes { get; set; }
    }
}
