using System.Globalization;

namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Grid Extensions
/// </summary>
public static class GridExtensions
{
	/// <summary>
	/// Set Row
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="row"></param>
	/// <returns>View with row set</returns>
	public static TBindable Row<TBindable>(this TBindable bindable, int row) where TBindable : BindableObject
	{
		bindable.SetValue(Grid.RowProperty, row);
		return bindable;
	}

	/// <summary>
	/// Set Row
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="row"></param>
	/// <param name="span"></param>
	/// <returns>View with row set</returns>
	public static TBindable Row<TBindable>(this TBindable bindable, int row, int span) where TBindable : BindableObject
	{
		bindable.SetValue(Grid.RowProperty, row);
		bindable.SetValue(Grid.RowSpanProperty, span);

		return bindable;
	}

	/// <summary>
	/// Set Row Span
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="span"></param>
	/// <returns>View with row span set</returns>
	public static TBindable RowSpan<TBindable>(this TBindable bindable, int span) where TBindable : BindableObject
	{
		bindable.SetValue(Grid.RowSpanProperty, span);
		return bindable;
	}

	/// <summary>
	/// Set Column
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="column"></param>
	/// <returns>View with Column set</returns>
	public static TBindable Column<TBindable>(this TBindable bindable, int column) where TBindable : BindableObject
	{
		bindable.SetValue(Grid.ColumnProperty, column);
		return bindable;
	}

	/// <summary>
	/// Set Column 
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="column"></param>
	/// <param name="span"></param>
	/// <returns>View with Column set</returns>
	public static TBindable Column<TBindable>(this TBindable bindable, int column, int span) where TBindable : BindableObject
	{
		bindable.SetValue(Grid.ColumnProperty, column);
		bindable.SetValue(Grid.ColumnSpanProperty, span);

		return bindable;
	}

	/// <summary>
	/// Set Column Span
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="span"></param>
	/// <returns>View with ColumnSpan set</returns>
	public static TBindable ColumnSpan<TBindable>(this TBindable bindable, int span) where TBindable : BindableObject
	{
		bindable.SetValue(Grid.ColumnSpanProperty, span);
		return bindable;
	}

	/// <summary>
	/// Set Row
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <typeparam name="TRow"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="row"></param>
	/// <returns>View with Row set</returns>
	public static TBindable Row<TBindable, TRow>(this TBindable bindable, TRow row) where TBindable : BindableObject where TRow : Enum
	{
		int rowIndex = row.ToInt();
		bindable.SetValue(Grid.RowProperty, rowIndex);

		return bindable;
	}

	/// <summary>
	/// Set Row
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <typeparam name="TRow"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="first"></param>
	/// <param name="last"></param>
	/// <returns>View with Row set</returns>
	public static TBindable Row<TBindable, TRow>(this TBindable bindable, TRow first, TRow last) where TBindable : BindableObject where TRow : Enum
	{
		int rowIndex = first.ToInt();
		int span = last.ToInt() - rowIndex + 1;

		bindable.SetValue(Grid.RowProperty, rowIndex);
		bindable.SetValue(Grid.RowSpanProperty, span);

		return bindable;
	}

	/// <summary>
	/// Set Column
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <typeparam name="TColumn"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="column"></param>
	/// <returns>View with Column set</returns>
	public static TBindable Column<TBindable, TColumn>(this TBindable bindable, TColumn column) where TBindable : BindableObject where TColumn : Enum
	{
		int columnIndex = column.ToInt();
		bindable.SetValue(Grid.ColumnProperty, columnIndex);

		return bindable;
	}

	/// <summary>
	/// Set Column
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <typeparam name="TColumn"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="first"></param>
	/// <param name="last"></param>
	/// <returns>Vie with Column set</returns>
	public static TBindable Column<TBindable, TColumn>(this TBindable bindable, TColumn first, TColumn last) where TBindable : BindableObject where TColumn : Enum
	{
		int columnIndex = first.ToInt();
		bindable.SetValue(Grid.ColumnProperty, columnIndex);

		int span = last.ToInt() + 1 - columnIndex;
		bindable.SetValue(Grid.ColumnSpanProperty, span);

		return bindable;
	}

	static int ToInt(this Enum enumValue) => Convert.ToInt32(enumValue, CultureInfo.InvariantCulture);
}