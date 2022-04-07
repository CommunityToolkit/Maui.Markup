using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;

namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Flex Layout Extension Methods
/// </summary>
public static class FlexLayoutExtensions
{
	/// <summary>
	/// Set the <see cref="FlexLayout.AlignSelfProperty"/> that indicates how the child is aligned on the cross axis. Setting this property overrides the <see cref="FlexLayout.AlignItems"/> property set on the <see cref="FlexLayout"/> itself
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
	/// Set the <see cref="FlexLayout.BasisProperty"/> that indicates the amount of space that is allocated to a child of the <see cref="FlexLayout"/> on the main axis. The size can be specified in device-independent units, as a percentage of the size of the <see cref="FlexLayout"/> or based on the child's requested width or height. 
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
	/// Set the <see cref="FlexLayout.BasisProperty"/> that indicates the amount of space that is allocated to a child of the <see cref="FlexLayout"/> on the main axis. The size can be specified in device-independent units when <paramref name="isRelative"/> is <see langword="false"/>, or as a percentage of the size of the <see cref="FlexLayout"/> when <paramref name="isRelative"/> is <see langword="true"/>. 
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="length"></param>
	/// <param name="isRelative"></param>
	/// <returns></returns>
	public static TBindable Basis<TBindable>(this TBindable bindable, float length, bool isRelative) where TBindable : BindableObject
	{
		return bindable.Basis(new FlexBasis(length, isRelative));
	}

	/// <summary>
	/// Set the <see cref="FlexLayout.GrowProperty"/> that indicates the amount of available space a child should use on the main axis of the <see cref="FlexLayout"/>
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
	/// Set the <see cref="FlexLayout.OrderProperty"/> that indicates the order a child laid out in a <see cref="FlexLayout"/>. Setting this property overrides the order that it appears in the <see cref="Layout.Children"/> collection
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
	/// Set the <see cref="FlexLayout.ShrinkProperty"/> that indicates the priority a child is given in being displayed at its full size, when the total sizes of <see cref="Layout.Children"/> is greater than the size of <see cref="FlexLayout"/> on its main axis.
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