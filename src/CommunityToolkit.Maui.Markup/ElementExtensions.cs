namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension Methods for Elements
/// </summary>
public static class ElementExtensions
{
	/// <summary>Set Padding.</summary>
	/// <param name="paddingElement">Element on which to set padding.</param>
	/// <param name="padding">Padding to set.</param>
	/// <param name="overload">Unused overload discriminator.</param>
	/// <returns>Element with added Padding.</returns>
	public static TBorder Padding<TBorder>(this TBorder paddingElement, Thickness padding, Border? overload = null) where TBorder : Border => SetPadding(paddingElement, padding);

	/// <inheritdoc />
	public static TButton Padding<TButton>(this TButton paddingElement, Thickness padding, Button? overload = null) where TButton : Button => SetPadding(paddingElement, padding);

	/// <inheritdoc />
	public static TContentPresenter Padding<TContentPresenter>(this TContentPresenter paddingElement, Thickness padding, ContentPresenter? overload = null) where TContentPresenter : ContentPresenter => SetPadding(paddingElement, padding);

	/// <inheritdoc />
	public static TImageButton Padding<TImageButton>(this TImageButton paddingElement, Thickness padding, ImageButton? overload = null) where TImageButton : ImageButton => SetPadding(paddingElement, padding);

	/// <inheritdoc />
	public static TLabel Padding<TLabel>(this TLabel paddingElement, Thickness padding, Label? overload = null) where TLabel : Label => SetPadding(paddingElement, padding);

	/// <inheritdoc />
	public static TLayout Padding<TLayout>(this TLayout paddingElement, Thickness padding, Layout? overload = null) where TLayout : Layout => SetPadding(paddingElement, padding);

	/// <inheritdoc />
	public static TPage Padding<TPage>(this TPage paddingElement, Thickness padding, Page? overload = null) where TPage : Page => SetPadding(paddingElement, padding);

	/// <inheritdoc />
	public static TScrollView Padding<TScrollView>(this TScrollView paddingElement, Thickness padding, ScrollView? overload = null) where TScrollView : ScrollView => SetPadding(paddingElement, padding);

	/// <inheritdoc />
	public static TTemplatedView Padding<TTemplatedView>(this TTemplatedView paddingElement, Thickness padding, TemplatedView? overload = null) where TTemplatedView : TemplatedView => SetPadding(paddingElement, padding);

	/// <summary>Set Padding.</summary>
	/// <param name="paddingElement">Element on which to set padding.</param>
	/// <param name="horizontalSize">Horizontal padding.</param>
	/// <param name="verticalSize">Vertical padding.</param>
	/// <param name="overload">Unused overload discriminator.</param>
	/// <returns>Element with added Padding.</returns>
	public static TBorder Padding<TBorder>(this TBorder paddingElement, double horizontalSize, double verticalSize, Border? overload = null) where TBorder : Border => SetPadding(paddingElement, new Thickness(horizontalSize, verticalSize));

	/// <inheritdoc />
	public static TButton Padding<TButton>(this TButton paddingElement, double horizontalSize, double verticalSize, Button? overload = null) where TButton : Button => SetPadding(paddingElement, new Thickness(horizontalSize, verticalSize));

	/// <inheritdoc />
	public static TContentPresenter Padding<TContentPresenter>(this TContentPresenter paddingElement, double horizontalSize, double verticalSize, ContentPresenter? overload = null) where TContentPresenter : ContentPresenter => SetPadding(paddingElement, new Thickness(horizontalSize, verticalSize));

	/// <inheritdoc />
	public static TImageButton Padding<TImageButton>(this TImageButton paddingElement, double horizontalSize, double verticalSize, ImageButton? overload = null) where TImageButton : ImageButton => SetPadding(paddingElement, new Thickness(horizontalSize, verticalSize));

	/// <inheritdoc />
	public static TLabel Padding<TLabel>(this TLabel paddingElement, double horizontalSize, double verticalSize, Label? overload = null) where TLabel : Label => SetPadding(paddingElement, new Thickness(horizontalSize, verticalSize));

	/// <inheritdoc />
	public static TLayout Padding<TLayout>(this TLayout paddingElement, double horizontalSize, double verticalSize, Layout? overload = null) where TLayout : Layout => SetPadding(paddingElement, new Thickness(horizontalSize, verticalSize));

	/// <inheritdoc />
	public static TPage Padding<TPage>(this TPage paddingElement, double horizontalSize, double verticalSize, Page? overload = null) where TPage : Page => SetPadding(paddingElement, new Thickness(horizontalSize, verticalSize));

	/// <inheritdoc />
	public static TScrollView Padding<TScrollView>(this TScrollView paddingElement, double horizontalSize, double verticalSize, ScrollView? overload = null) where TScrollView : ScrollView => SetPadding(paddingElement, new Thickness(horizontalSize, verticalSize));

	/// <inheritdoc />
	public static TTemplatedView Padding<TTemplatedView>(this TTemplatedView paddingElement, double horizontalSize, double verticalSize, TemplatedView? overload = null) where TTemplatedView : TemplatedView => SetPadding(paddingElement, new Thickness(horizontalSize, verticalSize));

	/// <summary>Set Padding.</summary>
	/// <param name="paddingElement">Element on which to set padding.</param>
	/// <param name="left">Left padding.</param>
	/// <param name="top">Top padding.</param>
	/// <param name="right">Right padding.</param>
	/// <param name="bottom">Bottom padding.</param>
	/// <param name="overload">Unused overload discriminator.</param>
	/// <returns>Element with added Padding.</returns>
	public static TBorder Paddings<TBorder>(this TBorder paddingElement, double left = 0, double top = 0, double right = 0, double bottom = 0, Border? overload = null) where TBorder : Border => SetPadding(paddingElement, new Thickness(left, top, right, bottom));

	/// <inheritdoc />
	public static TButton Paddings<TButton>(this TButton paddingElement, double left = 0, double top = 0, double right = 0, double bottom = 0, Button? overload = null) where TButton : Button => SetPadding(paddingElement, new Thickness(left, top, right, bottom));

	/// <inheritdoc />
	public static TContentPresenter Paddings<TContentPresenter>(this TContentPresenter paddingElement, double left = 0, double top = 0, double right = 0, double bottom = 0, ContentPresenter? overload = null) where TContentPresenter : ContentPresenter => SetPadding(paddingElement, new Thickness(left, top, right, bottom));

	/// <inheritdoc />
	public static TImageButton Paddings<TImageButton>(this TImageButton paddingElement, double left = 0, double top = 0, double right = 0, double bottom = 0, ImageButton? overload = null) where TImageButton : ImageButton => SetPadding(paddingElement, new Thickness(left, top, right, bottom));

	/// <inheritdoc />
	public static TLabel Paddings<TLabel>(this TLabel paddingElement, double left = 0, double top = 0, double right = 0, double bottom = 0, Label? overload = null) where TLabel : Label => SetPadding(paddingElement, new Thickness(left, top, right, bottom));

	/// <inheritdoc />
	public static TLayout Paddings<TLayout>(this TLayout paddingElement, double left = 0, double top = 0, double right = 0, double bottom = 0, Layout? overload = null) where TLayout : Layout => SetPadding(paddingElement, new Thickness(left, top, right, bottom));

	/// <inheritdoc />
	public static TPage Paddings<TPage>(this TPage paddingElement, double left = 0, double top = 0, double right = 0, double bottom = 0, Page? overload = null) where TPage : Page => SetPadding(paddingElement, new Thickness(left, top, right, bottom));

	/// <inheritdoc />
	public static TScrollView Paddings<TScrollView>(this TScrollView paddingElement, double left = 0, double top = 0, double right = 0, double bottom = 0, ScrollView? overload = null) where TScrollView : ScrollView => SetPadding(paddingElement, new Thickness(left, top, right, bottom));

	/// <inheritdoc />
	public static TTemplatedView Paddings<TTemplatedView>(this TTemplatedView paddingElement, double left = 0, double top = 0, double right = 0, double bottom = 0, TemplatedView? overload = null) where TTemplatedView : TemplatedView => SetPadding(paddingElement, new Thickness(left, top, right, bottom));

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
	/// <returns>Layout without Dynamic Resource</returns>
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
	public static TBindable FontSize<TBindable>(this TBindable fontElement, double size) where TBindable : BindableObject, ITextStyle
	{
		fontElement.SetValue(BindablePropertyHelpers.GetFontSizeProperty(fontElement), size);
		return fontElement;
	}

	/// <summary>
	/// Sets Bold
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="fontElement"></param>
	/// <returns>Font element with added Bold</returns>
	public static TBindable Bold<TBindable>(this TBindable fontElement) where TBindable : BindableObject, ITextStyle
	{
		fontElement.SetValue(BindablePropertyHelpers.GetFontAttributesProperty(fontElement), FontAttributes.Bold);
		return fontElement;
	}

	/// <summary>
	/// Sets Italic
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="fontElement"></param>
	/// <returns>Font element with added Italic</returns>
	public static TBindable Italic<TBindable>(this TBindable fontElement) where TBindable : BindableObject, ITextStyle
	{
		fontElement.SetValue(BindablePropertyHelpers.GetFontAttributesProperty(fontElement), FontAttributes.Italic);
		return fontElement;
	}

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
		bool? italic = null) where TBindable : BindableObject, ITextStyle
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
		switch (bindable)
		{
			case ILabel:
				bindable.SetValue(Label.TextProperty, text);
				break;

			case IButton:
				bindable.SetValue(Button.TextProperty, text);
				break;

			case MenuItem:
				bindable.SetValue(MenuItem.TextProperty, text);
				break;

			case IEditor:
				bindable.SetValue(Editor.TextProperty, text);
				break;

			case IEntry:
				bindable.SetValue(Entry.TextProperty, text);
				break;

			case ISearchBar:
				bindable.SetValue(SearchBar.TextProperty, text);
				break;

			default:
				throw new NotSupportedException($"{typeof(TBindable)} is not supported");
		}

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
}