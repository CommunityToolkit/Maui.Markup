using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Grid Extensions
/// </summary>
public static class ViewInGridExtensions
{
	/// <summary>
	/// Set Row
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <param name="view"></param>
	/// <param name="row"></param>
	/// <returns>View with row set</returns>
	public static TView Row<TView>(this TView view, int row) where TView : View
	{
		view.SetValue(Grid.RowProperty, row);
		return view;
	}

	/// <summary>
	/// Set Row
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <param name="view"></param>
	/// <param name="row"></param>
	/// <param name="span"></param>
	/// <returns>View with row set</returns>
	public static TView Row<TView>(this TView view, int row, int span) where TView : View
	{
		view.SetValue(Grid.RowProperty, row);
		view.SetValue(Grid.RowSpanProperty, span);

		return view;
	}

	/// <summary>
	/// Set Row Span
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <param name="view"></param>
	/// <param name="span"></param>
	/// <returns>View with row span set</returns>
	public static TView RowSpan<TView>(this TView view, int span) where TView : View
	{
		view.SetValue(Grid.RowSpanProperty, span);
		return view;
	}

	/// <summary>
	/// Set Column
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <param name="view"></param>
	/// <param name="column"></param>
	/// <returns>View with Column set</returns>
	public static TView Column<TView>(this TView view, int column) where TView : View
	{
		view.SetValue(Grid.ColumnProperty, column);
		return view;
	}

	/// <summary>
	/// Set Column 
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <param name="view"></param>
	/// <param name="column"></param>
	/// <param name="span"></param>
	/// <returns>View with Column set</returns>
	public static TView Column<TView>(this TView view, int column, int span) where TView : View
	{
		view.SetValue(Grid.ColumnProperty, column);
		view.SetValue(Grid.ColumnSpanProperty, span);

		return view;
	}

	/// <summary>
	/// Set Column Span
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <param name="view"></param>
	/// <param name="span"></param>
	/// <returns>View with ColumnSpan set</returns>
	public static TView ColumnSpan<TView>(this TView view, int span) where TView : View
	{
		view.SetValue(Grid.ColumnSpanProperty, span);
		return view;
	}

	/// <summary>
	/// Set Row
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <typeparam name="TRow"></typeparam>
	/// <param name="view"></param>
	/// <param name="row"></param>
	/// <returns>View with Row set</returns>
	public static TView Row<TView, TRow>(this TView view, TRow row) where TView : View where TRow : Enum
	{
		int rowIndex = row.ToInt();
		view.SetValue(Grid.RowProperty, rowIndex);

		return view;
	}

	/// <summary>
	/// Set Row
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <typeparam name="TRow"></typeparam>
	/// <param name="view"></param>
	/// <param name="first"></param>
	/// <param name="last"></param>
	/// <returns>View with Row set</returns>
	public static TView Row<TView, TRow>(this TView view, TRow first, TRow last) where TView : View where TRow : Enum
	{
		int rowIndex = first.ToInt();
		int span = last.ToInt() - rowIndex + 1;

		view.SetValue(Grid.RowProperty, rowIndex);
		view.SetValue(Grid.RowSpanProperty, span);

		return view;
	}

	/// <summary>
	/// Set Column
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <typeparam name="TColumn"></typeparam>
	/// <param name="view"></param>
	/// <param name="column"></param>
	/// <returns>View with Column set</returns>
	public static TView Column<TView, TColumn>(this TView view, TColumn column) where TView : View where TColumn : Enum
	{
		int columnIndex = column.ToInt();
		view.SetValue(Grid.ColumnProperty, columnIndex);

		return view;
	}

	/// <summary>
	/// Set Column
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <typeparam name="TColumn"></typeparam>
	/// <param name="view"></param>
	/// <param name="first"></param>
	/// <param name="last"></param>
	/// <returns>Vie with Column set</returns>
	public static TView Column<TView, TColumn>(this TView view, TColumn first, TColumn last) where TView : View where TColumn : Enum
	{
		int columnIndex = first.ToInt();
		view.SetValue(Grid.ColumnProperty, columnIndex);

		int span = last.ToInt() + 1 - columnIndex;
		view.SetValue(Grid.ColumnSpanProperty, span);

		return view;
	}

	static int ToInt(this Enum enumValue) => Convert.ToInt32(enumValue, CultureInfo.InvariantCulture);
}