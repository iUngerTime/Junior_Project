using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    /// <summary>
    /// View Model for modeling an ingredient from the database of ingredients
    /// </summary>
    public class IngredientViewModel
    {
        public int IngredientID { get; set; }
        public string CommonName { get; set; }
    }
}
