namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension Methods for <see cref="IPlaceholder"/>
/// </summary>
public static class PlaceholderExtensions
{
	/// <summary>
	/// Sets the <see cref="IPlaceholder.PlaceholderColor"/> property.
	/// </summary>
	/// <typeparam name="TBindable">The <see cref="BindableObject"/> type implementing <see cref="IPlaceholder"/>.</typeparam>
	/// <param name="bindable">The element on which to set <see cref="IPlaceholder.PlaceholderColor"/>.</param>
	/// <param name="textColor">The <see cref="Color"/> for <see cref="IPlaceholder.PlaceholderColor"/>.</param>
	/// <returns></returns>
	public static TBindable PlaceholderColor<TBindable>(this TBindable bindable, Color? textColor) where TBindable : BindableObject, IPlaceholder
	{
		return SetPlaceholderColor(bindable, textColor);
	}

	/// <inheritdoc cref="PlaceholderColor{TBindable}(TBindable, Color?)" />
	public static SearchHandler PlaceholderColor(this SearchHandler bindable, Color? textColor)
	{
		return SetPlaceholderColor(bindable, textColor);
	}

	/// <summary>
	/// Sets the <see cref="IPlaceholder.Placeholder" /> property.
	/// </summary>
	/// <typeparam name="TBindable">The <see cref="BindableObject"/> type implementing <see cref="IPlaceholder"/>.</typeparam>
	/// <param name="bindable">The element on which to set <see cref="IPlaceholder.Placeholder"/>.</param>
	/// <param name="text">The <see cref="string"/> for the <see cref="IPlaceholder.Placeholder"/> text.</param>
	/// <returns></returns>
	public static TBindable Placeholder<TBindable>(this TBindable bindable, string? text) where TBindable : BindableObject, IPlaceholder
	{
		return SetPlaceholder(bindable, text);
	}

	/// <inheritdoc cref="Placeholder{TBindable}(TBindable, string?)" />
	public static SearchHandler Placeholder(this SearchHandler bindable, string? text)
	{
		return SetPlaceholder(bindable, text);
	}

	/// <summary>
	/// Sets the <see cref="IPlaceholder.Placeholder" /> and <see cref="IPlaceholder.PlaceholderColor" /> properties.
	/// </summary>
	/// <typeparam name="TBindable">The <see cref="BindableObject"/> type implementing <see cref="IPlaceholder"/>.</typeparam>
	/// <param name="bindable">The element on which to set <see cref="IPlaceholder.Placeholder"/> and <see cref="IPlaceholder.PlaceholderColor"/>.</param>
	/// <param name="text">The <see cref="string"/> for the <see cref="IPlaceholder.Placeholder"/> text.</param>
	/// <param name="textColor">The <see cref="Color"/> for <see cref="IPlaceholder.PlaceholderColor"/>.</param>
	/// <returns></returns>
	public static TBindable Placeholder<TBindable>(this TBindable bindable, string? text, Color? textColor) where TBindable : BindableObject, IPlaceholder
	{
		SetPlaceholder(bindable, text);
		return SetPlaceholderColor(bindable, textColor);
	}

	/// <inheritdoc cref="Placeholder{TBindable}(TBindable, string?, Color?)" />
	public static SearchHandler Placeholder(this SearchHandler bindable, string? text, Color? textColor)
	{
		SetPlaceholder(bindable, text);
		return SetPlaceholderColor(bindable, textColor);
	}

	static TBindable SetPlaceholderColor<TBindable>(TBindable bindable, Color? textColor) where TBindable : BindableObject
	{
		bindable.SetValue(BindablePropertyHelpers.GetPlaceholderColorProperty(bindable), textColor);
		return bindable;
	}

	static TBindable SetPlaceholder<TBindable>(TBindable bindable, string? text) where TBindable : BindableObject
	{
		bindable.SetValue(BindablePropertyHelpers.GetPlaceholderProperty(bindable), text);
		return bindable;
	}
}