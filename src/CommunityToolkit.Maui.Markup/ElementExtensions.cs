namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension Methods for Elements
/// </summary>
public static class ElementExtensions
{
	/// <summary>Set Padding.</summary>
	/// <typeparam name="TVisualElement">The <see cref="VisualElement"/> type implementing <see cref="IPadding"/>.</typeparam>
	/// <param name="paddingElement">Element on which to set padding.</param>
	/// <param name="padding">Padding to set.</param>
	/// <returns>Element with added Padding.</returns>
	public static TVisualElement Padding<TVisualElement>(this TVisualElement paddingElement, Thickness padding) where TVisualElement : VisualElement, IPadding => SetPadding(paddingElement, padding);

	/// <inheritdoc cref="Padding{TVisualElement}(TVisualElement, Thickness)" />
	public static Page Padding(this Page paddingElement, Thickness padding) => SetPadding(paddingElement, padding);

	/// <summary>Set Padding.</summary>
	/// <typeparam name="TVisualElement">The <see cref="VisualElement"/> type implementing <see cref="IPadding"/>.</typeparam>
	/// <param name="paddingElement">Element on which to set padding.</param>
	/// <param name="horizontalSize">Horizontal padding.</param>
	/// <param name="verticalSize">Vertical padding.</param>
	/// <returns>Element with added Padding.</returns>
	public static TVisualElement Padding<TVisualElement>(this TVisualElement paddingElement, double horizontalSize, double verticalSize) where TVisualElement : VisualElement, IPadding => SetPadding(paddingElement, new Thickness(horizontalSize, verticalSize));

	/// <inheritdoc cref="Padding{TVisualElement}(TVisualElement, double, double)" />
	public static Page Padding(this Page paddingElement, double horizontalSize, double verticalSize) => SetPadding(paddingElement, new Thickness(horizontalSize, verticalSize));

	/// <summary>Set Padding.</summary>
	/// <typeparam name="TVisualElement">The <see cref="VisualElement"/> type implementing <see cref="IPadding"/>.</typeparam>
	/// <param name="paddingElement">Element on which to set padding.</param>
	/// <param name="left">Left padding.</param>
	/// <param name="top">Top padding.</param>
	/// <param name="right">Right padding.</param>
	/// <param name="bottom">Bottom padding.</param>
	/// <returns>Element with added Padding.</returns>
	public static TVisualElement Paddings<TVisualElement>(this TVisualElement paddingElement, double left = 0, double top = 0, double right = 0, double bottom = 0) where TVisualElement : VisualElement, IPadding => SetPadding(paddingElement, new Thickness(left, top, right, bottom));

	/// <inheritdoc cref="Paddings{TVisualElement}(TVisualElement, double, double, double, double)" />
	public static Page Paddings(this Page paddingElement, double left = 0, double top = 0, double right = 0, double bottom = 0) => SetPadding(paddingElement, new Thickness(left, top, right, bottom));

	static TElement SetPadding<TElement>(TElement paddingElement, Thickness padding) where TElement : BindableObject
	{
		paddingElement.SetValue(BindablePropertyHelpers.GetPaddingProperty(paddingElement), padding);
		return paddingElement;
	}

	/// <summary>
	/// Remove Dynamic Resource
	/// </summary>
	/// <typeparam name="TElement"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="properties"></param>
	/// <returns>Element without Dynamic Resource</returns>
	public static TElement RemoveDynamicResources<TElement>(this TElement bindable, params ReadOnlySpan<BindableProperty> properties) where TElement : Element
	{
		foreach (var property in properties)
		{
			bindable.RemoveDynamicResource(property);
		}

		return bindable;
	}

	/// <summary>
	/// Add Effects
	/// </summary>
	/// <typeparam name="TElement"></typeparam>
	/// <param name="element"></param>
	/// <param name="effects"></param>
	/// <returns>Element with added Effects</returns>
	public static TElement Effects<TElement>(this TElement element, params ReadOnlySpan<Effect> effects) where TElement : Element
	{
		foreach (var effect in effects)
		{
			element.Effects.Add(effect);
		}

		return element;
	}

	/// <summary>
	/// Sets FontSize
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="fontElement"></param>
	/// <param name="size"></param>
	/// <returns></returns>
	public static TBindable FontSize<TBindable>(this TBindable fontElement, double size) where TBindable : BindableObject, ITextStyle => SetFontSize(fontElement, size);

	/// <inheritdoc cref="FontSize{TBindable}(TBindable, double)" />
	public static Span FontSize(this Span fontElement, double size) => SetFontSize(fontElement, size);

	/// <inheritdoc cref="FontSize{TBindable}(TBindable, double)" />
	public static SearchHandler FontSize(this SearchHandler fontElement, double size) => SetFontSize(fontElement, size);

	/// <summary>
	/// Sets Bold
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="fontElement"></param>
	/// <returns>Font element with added Bold</returns>
	public static TBindable Bold<TBindable>(this TBindable fontElement) where TBindable : BindableObject, ITextStyle => SetFontAttributes(fontElement, FontAttributes.Bold);

	/// <inheritdoc cref="Bold{TBindable}(TBindable)" />
	public static Span Bold(this Span fontElement) => SetFontAttributes(fontElement, FontAttributes.Bold);

	/// <inheritdoc cref="Bold{TBindable}(TBindable)" />
	public static SearchHandler Bold(this SearchHandler fontElement) => SetFontAttributes(fontElement, FontAttributes.Bold);

	/// <summary>
	/// Sets Italic
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="fontElement"></param>
	/// <returns>Font element with added Italic</returns>
	public static TBindable Italic<TBindable>(this TBindable fontElement) where TBindable : BindableObject, ITextStyle => SetFontAttributes(fontElement, FontAttributes.Italic);

	/// <inheritdoc cref="Italic{TBindable}(TBindable)" />
	public static Span Italic(this Span fontElement) => SetFontAttributes(fontElement, FontAttributes.Italic);

	/// <inheritdoc cref="Italic{TBindable}(TBindable)" />
	public static SearchHandler Italic(this SearchHandler fontElement) => SetFontAttributes(fontElement, FontAttributes.Italic);

	/// <summary>
	/// Sets Font Properties
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="fontElement"></param>
	/// <param name="family"></param>
	/// <param name="size"></param>
	/// <param name="bold"></param>
	/// <param name="italic"></param>
	/// <returns>Font element with added Font properties</returns>
	public static TBindable Font<TBindable>(
		this TBindable fontElement,
		string? family = null,
		double? size = null,
		bool? bold = null,
		bool? italic = null) where TBindable : BindableObject, ITextStyle => SetFont(fontElement, family, size, bold, italic);

	/// <inheritdoc cref="Font{TBindable}(TBindable, string?, double?, bool?, bool?)" />
	public static Span Font(
		this Span fontElement,
		string? family = null,
		double? size = null,
		bool? bold = null,
		bool? italic = null) => SetFont(fontElement, family, size, bold, italic);

	/// <inheritdoc cref="Font{TBindable}(TBindable, string?, double?, bool?, bool?)" />
	public static SearchHandler Font(
		this SearchHandler fontElement,
		string? family = null,
		double? size = null,
		bool? bold = null,
		bool? italic = null) => SetFont(fontElement, family, size, bold, italic);
	
	/// <summary>
	/// Sets <see cref="ITextStyle.TextColor"/> Property
	/// </summary>
	/// <typeparam name="TBindable"><see cref="BindableObject"/></typeparam>
	/// <param name="bindable">Element</param>
	/// <param name="textColor">Text <see cref="Color"/></param>
	/// <returns></returns>
	public static TBindable TextColor<TBindable>(this TBindable bindable, Color? textColor) where TBindable : BindableObject, ITextStyle
	{
		if (bindable is MenuItem)
		{
			throw new NotSupportedException($"{typeof(MenuItem)} is not supported");
		}

		bindable.SetValue(BindablePropertyHelpers.GetTextColorProperty(bindable), textColor);

		return bindable;
	}

	/// <summary>
	/// Sets <see cref="IText.Text"/> Property
	/// </summary>
	/// <typeparam name="TBindable"><see cref="BindableObject"/></typeparam>
	/// <param name="bindable">Element</param>
	/// <param name="text"></param>
	/// <returns></returns>
	public static TBindable Text<TBindable>(this TBindable bindable, string? text) where TBindable : BindableObject, IText
	{
		bindable.SetValue(Label.TextProperty, text);
		return bindable;
	}

	/// <summary>
	/// Sets <see cref="IText.Text"/> Property
	/// </summary>
	/// <typeparam name="TBindable"><see cref="BindableObject"/></typeparam>
	/// <param name="bindable">Element</param>
	/// <param name="text"></param>
	/// <param name="textColor">Text <see cref="Color"/></param>
	public static TBindable Text<TBindable>(this TBindable bindable, string? text, Color? textColor) where TBindable : BindableObject, IText
	{
		return bindable.Text(text).TextColor(textColor);
	}

	static TBindable SetFontSize<TBindable>(TBindable fontElement, double size) where TBindable : BindableObject
	{
		fontElement.SetValue(BindablePropertyHelpers.GetFontSizeProperty(fontElement), size);
		return fontElement;
	}

	static TBindable SetFontAttributes<TBindable>(TBindable fontElement, FontAttributes attributes) where TBindable : BindableObject
	{
		fontElement.SetValue(BindablePropertyHelpers.GetFontAttributesProperty(fontElement), attributes);
		return fontElement;
	}

	static TBindable SetFont<TBindable>(
		TBindable fontElement,
		string? family,
		double? size,
		bool? bold,
		bool? italic) where TBindable : BindableObject
	{
		if (family != null)
		{
			fontElement.SetValue(BindablePropertyHelpers.GetFontFamilyProperty(fontElement), family);
		}

		if (size.HasValue)
		{
			fontElement.SetValue(BindablePropertyHelpers.GetFontSizeProperty(fontElement), size.Value);
		}

		if (bold.HasValue || italic.HasValue)
		{
			var attributes = FontAttributes.None;

			if (bold is true)
			{
				attributes |= FontAttributes.Bold;
			}

			if (italic is true)
			{
				attributes |= FontAttributes.Italic;
			}

			fontElement.SetValue(BindablePropertyHelpers.GetFontAttributesProperty(fontElement), attributes);
		}

		return fontElement;
	}
}