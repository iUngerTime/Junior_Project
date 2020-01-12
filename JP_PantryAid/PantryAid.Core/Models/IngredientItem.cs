using System;
using System.Collections.Generic;
using System.Text;

namespace PantryAid.Core.Models
{
    public class IngredientItem
    {
		public Ingredient Ingredient { get; set; }
		public float Quantity { get; set; }
		public Measurements MeasurementType { get; set; }

		public IngredientItem(Ingredient ingr, float amount, Measurements measure)
		{
			Ingredient = ingr;
			Quantity = amount;
			MeasurementType = measure;
		}

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
	public enum Measurements
	{
		Teaspoon,
		Tablespoon,
		FluidOunce,
		Gill,
		Cup,
		Pint,
		Quart,
		Gallon,
		Mililiter,
		Liter,
		Deciliter,
		Pound,
		Ounce,
		Milligram,
		Gram,
		Kilogram,
		Millimeter,
		Centimeter,
		Meter,
		Inch,
		Serving //I'm going to classify all things in the pantry and grocery list by this until we get a better idea of how we'll handle it
	}
}
