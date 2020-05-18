using System;
using System.Collections.Generic;
using System.Text;

namespace PantryAid.Core.Models
{
    public class ComplexResult
    {
        public ComplexResult(long Id,
            long UsedIngredients,
            long MissedIngredients,
            long Likes,
            string Title,
            string Image,
            string ImageType,
            Nutrition[] Nutrition)
        {
            id = Id;
            usedIngredientCount = UsedIngredients;
            missedIngredientCount = MissedIngredients;
            likes = Likes;
            title = Title;
            image = Image;
            imageType = ImageType;
            nutrition = Nutrition;
        }
        public ComplexResult()
        {
            id = -1;
            usedIngredientCount = -1;
            missedIngredientCount = -1;
            likes = -1;
            title = "";
            image = "";
            imageType = "";
            nutrition = new Nutrition[1];
        }
        public static implicit operator Recipe_Short(ComplexResult res) => new Recipe_Short((int)res.id, res.title, 0, 0, res.image, new List<string>() { res.image }, "");
        public static implicit operator ComplexResult(Recipe_Short res) => new ComplexResult(res.id, -1, -1, -1, res.title, res.image, "", new Nutrition[1]);
        public long id { get; set; }
        public long usedIngredientCount { get; set; }
        public long missedIngredientCount { get; set; }
        public long likes { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string imageType { get; set; }
        public Nutrition[] nutrition { get; set; }



        public class Nutrition
        {
            public string title { get; set; }

            public double amount { get; set; }

            public string unit { get; set; }
        }
    }
}
