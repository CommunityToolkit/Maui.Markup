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
	/// Set an <see cref="AbsoluteLayoutFlags"/> that indicates whether the layout bounds position and size values for a child are proportional to the size of the <see cref="AbsoluteLayout"/>.
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
	/// Set the position and size of a <see cref="View"/> in an <see cref="AbsoluteLayout"/>
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
	/// Set the position and of a <see cref="View"/> in an <see cref="AbsoluteLayout"/>, the <see cref="View"/> will size itself
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <param name="view"></param>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <returns></returns>
	public static TView LayoutBounds<TView>(this TView view, double x, double y) where TView : View
	{
		return view.LayoutBounds(new Rect(x, y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
	}


	/// <summary>
	/// Set the position and of a <see cref="View"/> in an <see cref="AbsoluteLayout"/>, the <see cref="View"/> will size itself
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <param name="view"></param>
	/// <param name="point"></param>
	/// <returns></returns>
	public static TView LayoutBounds<TView>(this TView view, Point point) where TView : View
	{
		return view.LayoutBounds(new Rect(point.X, point.Y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
	}


	/// <summary> 
	/// Set the position and size of a <see cref="View"/> in an <see cref="AbsoluteLayout"/>
	/// </summary> 
	/// <typeparam name="TView"></typeparam> 
	/// <param name="view"></param> 
	/// <param name="point"></param> 
	/// <param name="size"></param> 
	/// <returns></returns> 
	public static TView LayoutBounds<TView>(this TView view, Point point, Size size) where TView : View
	{
		return view.LayoutBounds(new Rect(point, size));
	}

	/// <summary>
	/// Set the position and size of a <see cref="View"/> in an <see cref="AbsoluteLayout"/>
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <param name="view"></param>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="size"></param>
	/// <returns></returns>
	public static TView LayoutBounds<TView>(this TView view, double x, double y, Size size) where TView : View
	{
		return view.LayoutBounds(new Rect(x, y, size.Width, size.Height));
	}

	/// <summary>
	/// Set the position and size of a <see cref="View"/> in an <see cref="AbsoluteLayout"/>
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <param name="view"></param>
	/// <param name="point"></param>
	/// <param name="width"></param>
	/// <param name="height"></param>
	/// <returns></returns>
	public static TView LayoutBounds<TView>(this TView view, Point point, double width, double height) where TView : View
	{
		return view.LayoutBounds(new Rect(point.X, point.Y, width, height));
	}

	/// <summary>
	/// Set the position and size of a <see cref="View"/> in an <see cref="AbsoluteLayout"/>
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
		return view.LayoutBounds(new Rect(x, y, width, height));
	}

}