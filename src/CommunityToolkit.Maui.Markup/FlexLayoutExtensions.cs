using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;

namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Flex Layout Extension Methods
/// </summary>
public static class FlexLayoutExtensions
{
	/// <summary>
	/// Set AlignSelf
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="value"></param>
	/// <returns>View with AlignSelf</returns>
	public static TBindable AlignSelf<TBindable>(this TBindable bindable, FlexAlignSelf value) where TBindable : BindableObject
	{
		FlexLayout.SetAlignSelf(bindable, value);
		return bindable;
	}

	/// <summary>
	/// Set Basis
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="value"></param>
	/// <returns>View with SetBasis</returns>
	public static TBindable Basis<TBindable>(this TBindable bindable, FlexBasis value) where TBindable : BindableObject
	{
		FlexLayout.SetBasis(bindable, value);
		return bindable;
	}

	/// <summary>
	/// Set Grow
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="value"></param>
	/// <returns>View with SetGrow</returns>
	public static TBindable Grow<TBindable>(this TBindable bindable, float value) where TBindable : BindableObject
	{
		FlexLayout.SetGrow(bindable, value);
		return bindable;
	}

	/// <summary>
	/// Set Order
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="value"></param>
	/// <returns>View with SetOrder</returns>
	public static TBindable Order<TBindable>(this TBindable bindable, int value) where TBindable : BindableObject
	{
		FlexLayout.SetOrder(bindable, value);
		return bindable;
	}

	/// <summary>
	/// Set Shrink 
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="value"></param>
	/// <returns>View with SetShrink</returns>
	public static TBindable Shrink<TBindable>(this TBindable bindable, float value) where TBindable : BindableObject
	{
		FlexLayout.SetShrink(bindable, value);
		return bindable;
	}
}