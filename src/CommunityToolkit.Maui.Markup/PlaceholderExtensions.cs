namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension Methods for <see cref="IPlaceholder"/>
/// </summary>
public static class PlaceholderExtensions
{
	/// <summary>
	/// Sets the <see cref="IPlaceholder.PlaceholderColor"/> Property
	/// </summary>
	/// <typeparam name="TBindable">Bindable Object</typeparam>
	/// <param name="bindable">The element on which to set <see cref="IPlaceholder.PlaceholderColor"/></param>
	/// <param name="textColor">The <see cref="Color"/> for <see cref="IPlaceholder.PlaceholderColor"/></param>
	/// <returns></returns>
	public static TBindable PlaceholderColor<TBindable>(this TBindable bindable, Color? textColor) where TBindable : BindableObject, IPlaceholder
	{
		bindable.SetValue(PlaceholderElement.PlaceholderColorProperty, textColor);
		return bindable;
	}

	/// <summary>
	/// Sets the <see cref="IPlaceholder.Placeholder" /> Property/>
	/// </summary>
	/// <typeparam name="TBindable">Bindable Object</typeparam>
	/// <param name="bindable">The element on which to set <see cref="IPlaceholder.PlaceholderColor"/></param>
	/// <param name="text">The <see cref="string"/> for the <see cref="IPlaceholder.Placeholder"/> text</param>
	/// <returns></returns>
	public static TBindable Placeholder<TBindable>(this TBindable bindable, string? text) where TBindable : BindableObject, IPlaceholder
	{
		bindable.SetValue(PlaceholderElement.PlaceholderProperty, text);
		return bindable;
	}

	/// <summary>
	/// Sets the <see cref="IPlaceholder.Placeholder" /> and <see cref="IPlaceholder.PlaceholderColor" /> Properties/>
	/// </summary>
	/// <typeparam name="TBindable">Bindable Object</typeparam>
	/// <param name="bindable">The element on which to set <see cref="IPlaceholder.PlaceholderColor"/></param>
	/// <param name="text">The <see cref="string"/> for the <see cref="IPlaceholder.Placeholder"/> text</param>
	/// <param name="textColor">The <see cref="Color"/> for <see cref="IPlaceholder.PlaceholderColor"/></param>
	/// <returns></returns>
	public static TBindable Placeholder<TBindable>(this TBindable bindable, string? text, Color? textColor) where TBindable : BindableObject, IPlaceholder
	{
		return bindable.Placeholder(text).PlaceholderColor(textColor);
	}
}