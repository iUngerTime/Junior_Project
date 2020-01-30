using System;
using System.Collections.Generic;
using System.Text;

namespace PantryAid.Core.Models
{
    /// <summary>
    /// Model to contain information pertinent to a user
    /// </summary>
    public class User
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<Ingredient> Allergies { get; set; }
        public List<Recipe_Short> FavoriteRecipes { get; set; }
        public List<Recipe_Short> DislikedRecipes { get; set; }
    }
}
