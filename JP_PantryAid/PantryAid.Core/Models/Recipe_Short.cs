using System;
using System.Collections.Generic;
using System.Text;

namespace PantryAid.Core.Models
{
    //This is a short recipe model and it only contains the name and a few other bits of data.  This what recipe searches will return.
    public class Recipe_Short
    {
        /// <summary>
        /// A unique id which can be used to get the Recipe_Full
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// The title or name of the recipe
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// How many minutes to prepare the recipe
        /// </summary>
        public int readyInMinutes { get; set; }
        /// <summary>
        /// Servings count
        /// </summary>
        public int servings { get; set; }
        /// <summary>
        /// Name of the image file
        /// </summary>
        public string image { get; set; }
        /// <summary>
        /// List of urls where the image can be found
        /// </summary>
        public List<string> imageUrls { get; set; }
        /// <summary>
        /// Credit for the recipe
        /// </summary>
        public string author { get; set; }
    }
}
