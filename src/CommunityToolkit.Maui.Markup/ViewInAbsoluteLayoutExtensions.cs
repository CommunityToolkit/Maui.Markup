using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;

namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Absolute Layout Extension Methods
/// </summary>
public static class ViewInAbsoluteLayoutExtensions
{
	/// <summary>
	/// Set LayoutFlags
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <param name="view"></param>
	/// <param name="flag"></param>
	/// <returns></returns>
	public static TView LayoutFlags<TView>(this TView view, AbsoluteLayoutFlags flag) where TView : View
	{
		AbsoluteLayout.SetLayoutFlags(view, flag);
		return view;
	}

	/// <summary>
	/// Set LayoutBounds
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <param name="view"></param>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <returns></returns>
	public static TView LayoutBounds<TView>(this TView view, double x, double y) where TView : View
	{
		Rect rect = new(x, y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize);
		AbsoluteLayout.SetLayoutBounds(view, rect);
		return view;
	}


	/// <summary>
	/// Set LayoutBounds
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <param name="view"></param>
	/// <param name="point"></param>
	/// <returns></returns>
	public static TView LayoutBounds<TView>(this TView view, Point point) where TView : View
	{
		Rect rect = new(point.X, point.Y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize);
		AbsoluteLayout.SetLayoutBounds(view, rect);
		return view;
	}

	/// <summary> 
	/// Set LayoutBounds 
	/// </summary> 
	/// <typeparam name="TView"></typeparam> 
	/// <param name="view"></param> 
	/// <param name="bounds"></param> 
	/// <returns></returns> 
	public static TView LayoutBounds<TView>(this TView view, Rect bounds) where TView : View
	{
		AbsoluteLayout.SetLayoutBounds(view, bounds);
		return view;
	}

	/// <summary> 
	/// Set LayoutBounds
	/// </summary> 
	/// <typeparam name="TView"></typeparam> 
	/// <param name="view"></param> 
	/// <param name="point"></param> 
	/// <param name="size"></param> 
	/// <returns></returns> 
	public static TView LayoutBounds<TView>(this TView view, Point point, Size size) where TView : View
	{
		Rect rect = new(point, size);
		AbsoluteLayout.SetLayoutBounds(view, rect);
		return view;
	}

	/// <summary>
	/// Set LayoutBounds
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <param name="view"></param>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="size"></param>
	/// <returns></returns>
	public static TView LayoutBounds<TView>(this TView view, double x, double y, Size size) where TView : View
	{
		Rect rect = new(x, y, size.Width, size.Height);
		AbsoluteLayout.SetLayoutBounds(view, rect);
		return view;
	}

	/// <summary>
	/// Set LayoutBounds
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <param name="view"></param>
	/// <param name="point"></param>
	/// <param name="width"></param>
	/// <param name="height"></param>
	/// <returns></returns>
	public static TView LayoutBounds<TView>(this TView view, Point point, double width, double height) where TView : View
	{
		Rect rect = new(point.X, point.Y, width, height);
		AbsoluteLayout.SetLayoutBounds(view, rect);
		return view;
	}

	/// <summary>
	/// Set LayoutBounds
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <param name="view"></param>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="width"></param>
	/// <param name="height"></param>
	/// <returns></returns>
	public static TView LayoutBounds<TView>(this TView view, double x, double y, double width, double height) where TView : View
	{
		Rect rect = new(x, y, width, height);
		AbsoluteLayout.SetLayoutBounds(view, rect);
		return view;
	}

}
