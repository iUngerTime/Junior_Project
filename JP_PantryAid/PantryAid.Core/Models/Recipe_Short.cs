using System;
using System.Collections.Generic;
using System.Net;
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


        public Recipe_Short(int id_, string tite_, int readyInMinutes_, int servings_, string image_,
            List<String> imageUrls_, string author_ = "")
        {
            id = id_;
            title = tite_;
            readyInMinutes = readyInMinutes_;
            servings = servings_;
            image = image_;
            imageUrls = imageUrls_;
            author = author_;
        }

        public Recipe_Short()
        {
            id = -1;
            title = "";
            readyInMinutes = -1;
            servings = -1;
            image = "";
            imageUrls = new List<string>();
            author = "";
        }

        public Recipe_Short(ComplexResult res)
        {
            id = (int)res.id;
            imageUrls = new List<string>(){res.image};
            title = res.title;
            readyInMinutes = -1;
            servings = -1;
            image = res.image;
            author = "";

        }
        //public static implicit operator Recipe_Short(ComplexResult res) => new Recipe_Short((int)res.id, res.title, 0, 0, res.image, new List<string>() { res.image }, "");
        //public static implicit operator ComplexResult(Recipe_Short res) => new ComplexResult(res.id, -1, -1, -1, res.title, res.image, "", new ComplexResult.Nutrition[1]);


    }
}
