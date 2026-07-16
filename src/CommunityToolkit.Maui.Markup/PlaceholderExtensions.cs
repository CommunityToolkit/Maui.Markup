namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension Methods for <see cref="IPlaceholder"/>
/// </summary>
public static class PlaceholderExtensions
{
	/// <summary>
	/// Sets the <see cref="IPlaceholder.PlaceholderColor"/> Property
	/// </summary>
	/// <typeparam name="TInputView">The <see cref="InputView"/> type.</typeparam>
	/// <param name="bindable">The element on which to set <see cref="IPlaceholder.PlaceholderColor"/></param>
	/// <param name="textColor">The <see cref="Color"/> for <see cref="IPlaceholder.PlaceholderColor"/></param>
	/// <param name="overload">Unused overload discriminator.</param>
	/// <returns></returns>
	public static TInputView PlaceholderColor<TInputView>(this TInputView bindable, Color? textColor, InputView? overload = null) where TInputView : InputView
	{
		return SetPlaceholderColor(bindable, textColor);
	}

	/// <inheritdoc />
	public static TSearchHandler PlaceholderColor<TSearchHandler>(this TSearchHandler bindable, Color? textColor, SearchHandler? overload = null) where TSearchHandler : SearchHandler
	{
		return SetPlaceholderColor(bindable, textColor);
	}

	/// <summary>
	/// Sets the <see cref="IPlaceholder.Placeholder" /> Property/>
	/// </summary>
	/// <typeparam name="TInputView">The <see cref="InputView"/> type.</typeparam>
	/// <param name="bindable">The element on which to set <see cref="IPlaceholder.PlaceholderColor"/></param>
	/// <param name="text">The <see cref="string"/> for the <see cref="IPlaceholder.Placeholder"/> text</param>
	/// <param name="overload">Unused overload discriminator.</param>
	/// <returns></returns>
	public static TInputView Placeholder<TInputView>(this TInputView bindable, string? text, InputView? overload = null) where TInputView : InputView
	{
		return SetPlaceholder(bindable, text);
	}

	/// <inheritdoc />
	public static TSearchHandler Placeholder<TSearchHandler>(this TSearchHandler bindable, string? text, SearchHandler? overload = null) where TSearchHandler : SearchHandler
	{
		return SetPlaceholder(bindable, text);
	}

	/// <summary>
	/// Sets the <see cref="IPlaceholder.Placeholder" /> and <see cref="IPlaceholder.PlaceholderColor" /> Properties/>
	/// </summary>
	/// <typeparam name="TInputView">The <see cref="InputView"/> type.</typeparam>
	/// <param name="bindable">The element on which to set <see cref="IPlaceholder.PlaceholderColor"/></param>
	/// <param name="text">The <see cref="string"/> for the <see cref="IPlaceholder.Placeholder"/> text</param>
	/// <param name="textColor">The <see cref="Color"/> for <see cref="IPlaceholder.PlaceholderColor"/></param>
	/// <param name="overload">Unused overload discriminator.</param>
	/// <returns></returns>
	public static TInputView Placeholder<TInputView>(this TInputView bindable, string? text, Color? textColor, InputView? overload = null) where TInputView : InputView
	{
		SetPlaceholder(bindable, text);
		return SetPlaceholderColor(bindable, textColor);
	}

	/// <inheritdoc />
	public static TSearchHandler Placeholder<TSearchHandler>(this TSearchHandler bindable, string? text, Color? textColor, SearchHandler? overload = null) where TSearchHandler : SearchHandler
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