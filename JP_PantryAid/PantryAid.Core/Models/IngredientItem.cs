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
	}

	/// <summary>
	/// An Enumeration that encompases every possible form of measurement
	/// </summary>
	public enum Measurements
	{
		Teaspoon,
		Tablespoon,
		FuildOunce,
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
		Inch
	}
}
