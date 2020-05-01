using System;
using System.Collections.Generic;
using System.Text;

namespace PantryAid.Core.Models
{
    public class IngredientItem
    {
		public IngredientItem(Ingredient ingr, double amount, string measure)
		{
			Ingredient = ingr;
			Quantity = amount;
			Measurement = measure;
		}

		public Ingredient Ingredient { get; set; }
		public double Quantity { get; set; }
		public string Measurement { get; set; }

		//These getters are needed for the gridview
		public string Name
		{
			get { return Ingredient.Name; }
		}

		public int ID
		{
			get { return Ingredient.IngredientID; }
		}

		//For some reason the binding doesn't like this
		public string FullQuantity
		{
			get
			{
				return Quantity.ToString() + " " + Measurement + "s";
			}
		}
	}
}
