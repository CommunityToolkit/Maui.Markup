namespace CommunityToolkit.Maui.Markup;

static class BindablePropertyHelpers
{
	public static BindableProperty GetPaddingProperty(BindableObject bindable) => bindable switch
	{
		Border => Border.PaddingProperty,
		Button => Button.PaddingProperty,
		ContentPresenter => ContentPresenter.PaddingProperty,
		ImageButton => ImageButton.PaddingProperty,
		Label => Label.PaddingProperty,
		ScrollView => ScrollView.PaddingProperty,
		Layout => Layout.PaddingProperty,
		Page => Page.PaddingProperty,
		TemplatedView => TemplatedView.PaddingProperty,
		_ => throw CreateUnsupportedException(bindable, "Padding")
	};

	public static BindableProperty GetFontFamilyProperty(BindableObject bindable) => bindable switch
	{
		Button => Button.FontFamilyProperty,
		DatePicker => DatePicker.FontFamilyProperty,
		FontImageSource => FontImageSource.FontFamilyProperty,
		SearchBar => SearchBar.FontFamilyProperty,
		InputView => InputView.FontFamilyProperty,
		Label => Label.FontFamilyProperty,
		Picker => Picker.FontFamilyProperty,
		RadioButton => RadioButton.FontFamilyProperty,
		SearchHandler => SearchHandler.FontFamilyProperty,
		Span => Span.FontFamilyProperty,
		TimePicker => TimePicker.FontFamilyProperty,
		_ => throw CreateUnsupportedException(bindable, "FontFamily")
	};

	public static BindableProperty GetFontSizeProperty(BindableObject bindable) => bindable switch
	{
		Button => Button.FontSizeProperty,
		DatePicker => DatePicker.FontSizeProperty,
		SearchBar => SearchBar.FontSizeProperty,
		InputView => InputView.FontSizeProperty,
		Label => Label.FontSizeProperty,
		Picker => Picker.FontSizeProperty,
		RadioButton => RadioButton.FontSizeProperty,
		SearchHandler => SearchHandler.FontSizeProperty,
		Span => Span.FontSizeProperty,
		TimePicker => TimePicker.FontSizeProperty,
		_ => throw CreateUnsupportedException(bindable, "FontSize")
	};

	public static BindableProperty GetFontAttributesProperty(BindableObject bindable) => bindable switch
	{
		Button => Button.FontAttributesProperty,
		DatePicker => DatePicker.FontAttributesProperty,
		SearchBar => SearchBar.FontAttributesProperty,
		InputView => InputView.FontAttributesProperty,
		Label => Label.FontAttributesProperty,
		Picker => Picker.FontAttributesProperty,
		RadioButton => RadioButton.FontAttributesProperty,
		SearchHandler => SearchHandler.FontAttributesProperty,
		Span => Span.FontAttributesProperty,
		TimePicker => TimePicker.FontAttributesProperty,
		_ => throw CreateUnsupportedException(bindable, "FontAttributes")
	};

	public static BindableProperty GetTextColorProperty(BindableObject bindable) => bindable switch
	{
		Button => Button.TextColorProperty,
		DatePicker => DatePicker.TextColorProperty,
		SearchBar => SearchBar.TextColorProperty,
		InputView => InputView.TextColorProperty,
		Label => Label.TextColorProperty,
		Picker => Picker.TextColorProperty,
		RadioButton => RadioButton.TextColorProperty,
		SearchHandler => SearchHandler.TextColorProperty,
		Span => Span.TextColorProperty,
		TimePicker => TimePicker.TextColorProperty,
		_ => throw CreateUnsupportedException(bindable, "TextColor")
	};

	public static BindableProperty GetImageSourceProperty(BindableObject bindable) => bindable switch
	{
		Image => Image.SourceProperty,
		ImageButton => ImageButton.SourceProperty,
		_ => throw CreateUnsupportedException(bindable, "Source")
	};

	public static BindableProperty GetImageAspectProperty(BindableObject bindable) => bindable switch
	{
		Image => Image.AspectProperty,
		ImageButton => ImageButton.AspectProperty,
		_ => throw CreateUnsupportedException(bindable, "Aspect")
	};

	public static BindableProperty GetImageIsOpaqueProperty(BindableObject bindable) => bindable switch
	{
		Image => Image.IsOpaqueProperty,
		ImageButton => ImageButton.IsOpaqueProperty,
		_ => throw CreateUnsupportedException(bindable, "IsOpaque")
	};

	public static BindableProperty GetPlaceholderProperty(BindableObject bindable) => bindable switch
	{
		SearchBar => SearchBar.PlaceholderProperty,
		InputView => InputView.PlaceholderProperty,
		SearchHandler => SearchHandler.PlaceholderProperty,
		_ => throw CreateUnsupportedException(bindable, "Placeholder")
	};

	public static BindableProperty GetPlaceholderColorProperty(BindableObject bindable) => bindable switch
	{
		SearchBar => SearchBar.PlaceholderColorProperty,
		InputView => InputView.PlaceholderColorProperty,
		SearchHandler => SearchHandler.PlaceholderColorProperty,
		_ => throw CreateUnsupportedException(bindable, "PlaceholderColor")
	};

	static NotSupportedException CreateUnsupportedException(BindableObject bindable, string propertyName)
		=> new($"{bindable.GetType()} does not expose a public {propertyName} bindable property.");
}
