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
        public User() { }
        public User(string email, string hash, int id = -1)
        {
            Id = id;
            Email = email;
            Hash = hash;
        }
        public int Id { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public List<Alergens> Allergies { get; set; }
        public List<DietPreferences> DietaryPreferences { get; set; }
        public List<int> FavoriteRecipes { get; set; }
        public List<int> DislikedRecipes { get; set; }
    }

    public enum Alergens
    {
        Dairy = 1,
        Egg,
        Gluten,
        Grain,
        Peanut,
        Seafood,
        Sesame,
        Shellfish = 14,
        Soy,
        Sulfite,
        TreeNut,
        Wheat
    }

    public enum DietPreferences
    {
        GlutenFree = 1,
        Ketogenic,
        Vegetarian,
        LactoVegetarian,
        OvoVegetarian,
        Vegan,
        Pescetarian,
        Paleo,
        Primal,
        Whole30
    }
}
