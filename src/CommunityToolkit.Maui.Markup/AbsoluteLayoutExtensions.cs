using Microsoft.Maui.Layouts;

namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Absolute Layout Extension Methods
/// </summary>
public static class AbsoluteLayoutExtensions
{
	/// <summary>
	/// Removes all <see cref="AbsoluteLayoutFlags"/>, reverting to the default configuraion of <see cref="AbsoluteLayoutFlags.None"/>.
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="bindable"></param>
	/// <returns></returns>
	public static TBindable ClearLayoutFlags<TBindable>(this TBindable bindable) where TBindable : BindableObject
	{
		AbsoluteLayout.SetLayoutFlags(bindable, AbsoluteLayoutFlags.None);
		return bindable;
	}

	/// <summary>
	/// Set an <see cref="AbsoluteLayoutFlags"/> that indicates whether the layout bounds position and size values for a child are proportional to the size of the <see cref="AbsoluteLayout"/>.
	/// Appends <paramref name="flag"/> to existing <see cref="AbsoluteLayoutFlags"/>
	/// </summary>
	/// <remarks>
	/// To clear existing <see cref="AbsoluteLayoutFlags"/>, use <see cref="ClearLayoutFlags{TBindable}(TBindable)"/>
	/// </remarks>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="flag"></param>
	/// <returns></returns>
	public static TBindable LayoutFlags<TBindable>(this TBindable bindable, AbsoluteLayoutFlags flag) where TBindable : BindableObject
	{
		AbsoluteLayout.SetLayoutFlags(bindable, flag);
		return bindable;
	}

	/// <summary>
	/// Set an <see cref="AbsoluteLayoutFlags"/> that indicates whether the layout bounds position and size values for a child are proportional to the size of the <see cref="AbsoluteLayout"/>.
	/// </summary>
	/// <remarks>
	/// To clear existing <see cref="AbsoluteLayoutFlags"/>, use <see cref="ClearLayoutFlags{TBindable}(TBindable)"/>
	/// </remarks>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="flags"></param>
	/// <returns></returns>
	public static TBindable LayoutFlags<TBindable>(this TBindable bindable, params AbsoluteLayoutFlags[] flags) where TBindable : BindableObject
	{
		var newFlags = AbsoluteLayoutFlags.None;

		foreach(var flag in flags)
		{
			newFlags |= flag;
		}

		AbsoluteLayout.SetLayoutFlags(bindable, newFlags);
		return bindable;
	}

	/// <summary> 
	/// Set the position and size of a <see cref="View"/> in an <see cref="AbsoluteLayout"/>
	/// </summary> 
	/// <typeparam name="TBindable"></typeparam> 
	/// <param name="bindable"></param> 
	/// <param name="bounds"></param> 
	/// <returns></returns> 
	public static TBindable LayoutBounds<TBindable>(this TBindable bindable, Rect bounds) where TBindable : BindableObject
	{
		AbsoluteLayout.SetLayoutBounds(bindable, bounds);
		return bindable;
	}

	/// <summary>
	/// Set the position and of a <see cref="View"/> in an <see cref="AbsoluteLayout"/>, the <see cref="View"/> will size itself
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <returns></returns>
	public static TBindable LayoutBounds<TBindable>(this TBindable bindable, double x, double y) where TBindable : BindableObject
	{
		return bindable.LayoutBounds(new Rect(x, y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
	}


	/// <summary>
	/// Set the position and of a <see cref="View"/> in an <see cref="AbsoluteLayout"/>, the <see cref="View"/> will size itself
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="point"></param>
	/// <returns></returns>
	public static TBindable LayoutBounds<TBindable>(this TBindable bindable, Point point) where TBindable : BindableObject
	{
		return bindable.LayoutBounds(new Rect(point.X, point.Y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
	}


	/// <summary> 
	/// Set the position and size of a <see cref="View"/> in an <see cref="AbsoluteLayout"/>
	/// </summary> 
	/// <typeparam name="TBindable"></typeparam> 
	/// <param name="bindable"></param> 
	/// <param name="point"></param> 
	/// <param name="size"></param> 
	/// <returns></returns> 
	public static TBindable LayoutBounds<TBindable>(this TBindable bindable, Point point, Size size) where TBindable : BindableObject
	{
		return bindable.LayoutBounds(new Rect(point, size));
	}

	/// <summary>
	/// Set the position and size of a <see cref="View"/> in an <see cref="AbsoluteLayout"/>
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="view"></param>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="size"></param>
	/// <returns></returns>
	public static TBindable LayoutBounds<TBindable>(this TBindable view, double x, double y, Size size) where TBindable : BindableObject
	{
		return view.LayoutBounds(new Rect(x, y, size.Width, size.Height));
	}

	/// <summary>
	/// Set the position and size of a <see cref="View"/> in an <see cref="AbsoluteLayout"/>
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="point"></param>
	/// <param name="width"></param>
	/// <param name="height"></param>
	/// <returns></returns>
	public static TBindable LayoutBounds<TBindable>(this TBindable bindable, Point point, double width, double height) where TBindable : BindableObject
	{
		return bindable.LayoutBounds(new Rect(point.X, point.Y, width, height));
	}

	/// <summary>
	/// Set the position and size of a <see cref="View"/> in an <see cref="AbsoluteLayout"/>
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="width"></param>
	/// <param name="height"></param>
	/// <returns></returns>
	public static TBindable LayoutBounds<TBindable>(this TBindable bindable, double x, double y, double width, double height) where TBindable : BindableObject
	{
		return bindable.LayoutBounds(new Rect(x, y, width, height));
	}
}