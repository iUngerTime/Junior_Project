using System;
using System.Collections.Generic;
using System.Text;

namespace PantryAid.Core.Models
{
    //This is a short recipe model and it only contains the name and a few other bits of data.  Thies what recipe searches will retrun.
    public class Recipe_Short
    {
        //Data
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public int readyInMinutes { get; set; }
        public int servings { get; set; }
        public string Name { get; set; }

        //Ctors
        public Recipe_Short() { Id = -1; ImageURL = ""; readyInMinutes = -1; servings = -1; Name = ""; }
        public Recipe_Short(Recipe_Short Rshort)
        {
            Id = Rshort.Id;
            ImageURL = Rshort.ImageURL;
            readyInMinutes = Rshort.readyInMinutes;
            servings = Rshort.readyInMinutes;
            Name = Rshort.Name;
        }
    }
}
