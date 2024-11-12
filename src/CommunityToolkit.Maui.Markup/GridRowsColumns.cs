using System.Globalization;
namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Grid Rows and Column Extensions
/// </summary>
public static class GridRowsColumns
{
	/// <summary>
	/// GridLength.Auto
	/// </summary>
	public static GridLength Auto => GridLength.Auto;

	/// <summary>
	/// GridLength.Star
	/// </summary>
	public static GridLength Star => GridLength.Star;

	/// <summary>
	/// new GridLength(value, GridUnitType.Star)
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public static GridLength Stars(double value) => new(value, GridUnitType.Star);

	/// <summary>
	/// Grid Columns
	/// </summary>
	public static class Columns
	{
		/// <summary>
		/// Define Columns
		/// </summary>
		/// <param name="widths"></param>
		/// <returns></returns>
		public static ColumnDefinitionCollection Define(params ReadOnlySpan<GridLength> widths)
		{
			var columnDefinitions = new ColumnDefinitionCollection();

			foreach (var width in widths)
			{
				columnDefinitions.Add(new ColumnDefinition { Width = width });
			}

			return columnDefinitions;
		}

		/// <summary>
		/// Define Columns
		/// </summary>
		/// <typeparam name="TEnum"></typeparam>
		/// <param name="columns"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		public static ColumnDefinitionCollection Define<TEnum>(params ReadOnlySpan<(TEnum Name, GridLength Width)> columns) where TEnum : Enum
		{
			var columnDefinitions = new ColumnDefinitionCollection();

			for (var i = 0; i < columns.Length; i++)
			{
				if (i != columns[i].Name.ToInt())
				{
					throw new ArgumentException(
						$"Value of column name {columns[i].Name} is not {i}. " +
						"Columns must be defined with enum names whose values form the sequence 0,1,2,...");
				}

				columnDefinitions.Add(new ColumnDefinition { Width = columns[i].Width });
			}

			return columnDefinitions;
		}
	}

	/// <summary>
	/// Grid Rows
	/// </summary>
	public static class Rows
	{
		/// <summary>
		/// Define Grid Rows
		/// </summary>
		/// <param name="heights"></param>
		/// <returns></returns>
		public static RowDefinitionCollection Define(params ReadOnlySpan<GridLength> heights)
		{
			var rowDefinitions = new RowDefinitionCollection();

			foreach (var height in heights)
			{
				rowDefinitions.Add(new RowDefinition { Height = height });
			}

			return rowDefinitions;
		}

		/// <summary>
		/// Define Grid Row
		/// </summary>
		/// <typeparam name="TEnum"></typeparam>
		/// <param name="rows"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		public static RowDefinitionCollection Define<TEnum>(params ReadOnlySpan<(TEnum Name, GridLength Height)> rows) where TEnum : Enum
		{
			var rowDefinitions = new RowDefinitionCollection();
			for (var i = 0; i < rows.Length; i++)
			{
				if (i != rows[i].Name.ToInt())
				{
					throw new ArgumentException(
						$"Value of row name {rows[i].Name} is not {i}. " +
						"Rows must be defined with enum names whose values form the sequence 0,1,2,...");
				}

				rowDefinitions.Add(new RowDefinition { Height = rows[i].Height });
			}
			return rowDefinitions;
		}
	}

	/// <summary>
	/// Get Enum Value Count
	/// </summary>
	/// <typeparam name="TEnum"></typeparam>
	/// <returns>Count of enum values</returns>
	/// <exception cref="ArgumentNullException"></exception>
	/// <exception cref="InvalidOperationException"></exception>
	public static int All<TEnum>() where TEnum : struct, Enum
	{
		var values = Enum.GetValues<TEnum>();
		int span = 1 + (int)(values.GetValue(values.Length - 1) ?? throw new InvalidOperationException("Value Not Found"));

		return span;
	}

	/// <summary>
	/// Get Last Enum value
	/// </summary>
	/// <typeparam name="TEnum"></typeparam>
	/// <returns>Last value in Enum</returns>
	/// <exception cref="InvalidOperationException"></exception>
	public static int Last<TEnum>() where TEnum : struct, Enum
	{
		var values = Enum.GetValues<TEnum>();
		int last = (int)(values.GetValue(values.Length - 1) ?? throw new InvalidOperationException("Value Not Found"));

		return last;
	}

	static int ToInt(this Enum enumValue) => Convert.ToInt32(enumValue, CultureInfo.InvariantCulture);
}