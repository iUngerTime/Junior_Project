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
		//public Measurements MeasurementType { get; set; }
		public string Measurement { get; set; }

		//These two getters are needed for the gridview
		public string Name
		{
			get { return Ingredient.Name; }
		}

		public int ID
		{
			get { return Ingredient.IngredientID; }
		}
	}

	/// <summary>
	/// An Enumeration that encompases every possible form of measurement
	/// </summary>
	/*public enum Measurements //To be replaces with strings
	{
		Teaspoons,
		Tablespoons,
		FluidOunces,
		Cups,
		Pints,
		Quarts,
		Gallons,
		Milliliters,
		Liters,
		Pounds,
		Ounces,
		Milligrams,
		Grams,
		Kilograms,
		Servings //I'm going to classify all things in the pantry and grocery list by this until we get a better idea of how we'll handle it
	}*/
}
