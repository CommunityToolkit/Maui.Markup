using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;

namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Flex Layout Extension Methods
/// </summary>
public static class ViewInFlexLayoutExtensions
{
	/// <summary>
	/// Set AlignSelf
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <param name="view"></param>
	/// <param name="value"></param>
	/// <returns>View with AlignSelf</returns>
	public static TView AlignSelf<TView>(this TView view, FlexAlignSelf value) where TView : View
	{
		FlexLayout.SetAlignSelf(view, value);
		return view;
	}

	/// <summary>
	/// Set Basis
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <param name="view"></param>
	/// <param name="value"></param>
	/// <returns>View with SetBasis</returns>
	public static TView Basis<TView>(this TView view, FlexBasis value) where TView : View
	{
		FlexLayout.SetBasis(view, value);
		return view;
	}

	/// <summary>
	/// Set Grow
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <param name="view"></param>
	/// <param name="value"></param>
	/// <returns>View with SetGrow</returns>
	public static TView Grow<TView>(this TView view, float value) where TView : View
	{
		FlexLayout.SetGrow(view, value);
		return view;
	}

	/// <summary>
	/// Set Order
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <param name="view"></param>
	/// <param name="value"></param>
	/// <returns>View with SetOrder</returns>
	public static TView Order<TView>(this TView view, int value) where TView : View
	{
		FlexLayout.SetOrder(view, value);
		return view;
	}

	/// <summary>
	/// Set Shrink 
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <param name="view"></param>
	/// <param name="value"></param>
	/// <returns>View with SetShrink</returns>
	public static TView Shrink<TView>(this TView view, float value) where TView : View
	{
		FlexLayout.SetShrink(view, value);
		return view;
	}
}