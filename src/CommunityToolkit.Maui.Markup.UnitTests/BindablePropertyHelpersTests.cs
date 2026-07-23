using System.Reflection;
using System.Runtime.ExceptionServices;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using NUnit.Framework;
namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class BindablePropertyHelpersTests : BaseMarkupTestFixture
{
	const string getPaddingProperty = "GetPaddingProperty";
	const string getFontFamilyProperty = "GetFontFamilyProperty";
	const string getFontSizeProperty = "GetFontSizeProperty";
	const string getFontAttributesProperty = "GetFontAttributesProperty";
	const string getTextColorProperty = "GetTextColorProperty";
	const string getImageSourceProperty = "GetImageSourceProperty";
	const string getImageAspectProperty = "GetImageAspectProperty";
	const string getImageIsOpaqueProperty = "GetImageIsOpaqueProperty";
	const string getPlaceholderProperty = "GetPlaceholderProperty";
	const string getPlaceholderColorProperty = "GetPlaceholderColorProperty";

	static readonly SupportedBindablePropertyCase[] supportedCases =
	[
		new(getPaddingProperty, "Border", static () => new Border(), Border.PaddingProperty),
		new(getPaddingProperty, "Button", static () => new Button(), Button.PaddingProperty),
		new(getPaddingProperty, "ContentPresenter", static () => new ContentPresenter(), ContentPresenter.PaddingProperty),
		new(getPaddingProperty, "ImageButton", static () => new ImageButton(), ImageButton.PaddingProperty),
		new(getPaddingProperty, "Label", static () => new Label(), Label.PaddingProperty),
		new(getPaddingProperty, "ScrollView", static () => new ScrollView(), ScrollView.PaddingProperty),
		new(getPaddingProperty, "Layout", static () => new Grid(), Layout.PaddingProperty),
		new(getPaddingProperty, "Page", static () => new Page(), Page.PaddingProperty),
		new(getPaddingProperty, "TemplatedView", static () => new ContentView(), TemplatedView.PaddingProperty),

		new(getFontFamilyProperty, "Button", static () => new Button(), Button.FontFamilyProperty),
		new(getFontFamilyProperty, "DatePicker", static () => new DatePicker(), DatePicker.FontFamilyProperty),
		new(getFontFamilyProperty, "FontImageSource", static () => new FontImageSource(), FontImageSource.FontFamilyProperty),
		new(getFontFamilyProperty, "SearchBar", static () => new SearchBar(), SearchBar.FontFamilyProperty),
		new(getFontFamilyProperty, "InputView", static () => new Entry(), InputView.FontFamilyProperty),
		new(getFontFamilyProperty, "Label", static () => new Label(), Label.FontFamilyProperty),
		new(getFontFamilyProperty, "Picker", static () => new Picker(), Picker.FontFamilyProperty),
		new(getFontFamilyProperty, "RadioButton", static () => new RadioButton(), RadioButton.FontFamilyProperty),
		new(getFontFamilyProperty, "SearchHandler", static () => new SearchHandler(), SearchHandler.FontFamilyProperty),
		new(getFontFamilyProperty, "Span", static () => new Span(), Span.FontFamilyProperty),
		new(getFontFamilyProperty, "TimePicker", static () => new TimePicker(), TimePicker.FontFamilyProperty),

		new(getFontSizeProperty, "Button", static () => new Button(), Button.FontSizeProperty),
		new(getFontSizeProperty, "DatePicker", static () => new DatePicker(), DatePicker.FontSizeProperty),
		new(getFontSizeProperty, "SearchBar", static () => new SearchBar(), SearchBar.FontSizeProperty),
		new(getFontSizeProperty, "InputView", static () => new Entry(), InputView.FontSizeProperty),
		new(getFontSizeProperty, "Label", static () => new Label(), Label.FontSizeProperty),
		new(getFontSizeProperty, "Picker", static () => new Picker(), Picker.FontSizeProperty),
		new(getFontSizeProperty, "RadioButton", static () => new RadioButton(), RadioButton.FontSizeProperty),
		new(getFontSizeProperty, "SearchHandler", static () => new SearchHandler(), SearchHandler.FontSizeProperty),
		new(getFontSizeProperty, "Span", static () => new Span(), Span.FontSizeProperty),
		new(getFontSizeProperty, "TimePicker", static () => new TimePicker(), TimePicker.FontSizeProperty),

		new(getFontAttributesProperty, "Button", static () => new Button(), Button.FontAttributesProperty),
		new(getFontAttributesProperty, "DatePicker", static () => new DatePicker(), DatePicker.FontAttributesProperty),
		new(getFontAttributesProperty, "SearchBar", static () => new SearchBar(), SearchBar.FontAttributesProperty),
		new(getFontAttributesProperty, "InputView", static () => new Entry(), InputView.FontAttributesProperty),
		new(getFontAttributesProperty, "Label", static () => new Label(), Label.FontAttributesProperty),
		new(getFontAttributesProperty, "Picker", static () => new Picker(), Picker.FontAttributesProperty),
		new(getFontAttributesProperty, "RadioButton", static () => new RadioButton(), RadioButton.FontAttributesProperty),
		new(getFontAttributesProperty, "SearchHandler", static () => new SearchHandler(), SearchHandler.FontAttributesProperty),
		new(getFontAttributesProperty, "Span", static () => new Span(), Span.FontAttributesProperty),
		new(getFontAttributesProperty, "TimePicker", static () => new TimePicker(), TimePicker.FontAttributesProperty),

		new(getTextColorProperty, "Button", static () => new Button(), Button.TextColorProperty),
		new(getTextColorProperty, "DatePicker", static () => new DatePicker(), DatePicker.TextColorProperty),
		new(getTextColorProperty, "SearchBar", static () => new SearchBar(), SearchBar.TextColorProperty),
		new(getTextColorProperty, "InputView", static () => new Entry(), InputView.TextColorProperty),
		new(getTextColorProperty, "Label", static () => new Label(), Label.TextColorProperty),
		new(getTextColorProperty, "Picker", static () => new Picker(), Picker.TextColorProperty),
		new(getTextColorProperty, "RadioButton", static () => new RadioButton(), RadioButton.TextColorProperty),
		new(getTextColorProperty, "SearchHandler", static () => new SearchHandler(), SearchHandler.TextColorProperty),
		new(getTextColorProperty, "Span", static () => new Span(), Span.TextColorProperty),
		new(getTextColorProperty, "TimePicker", static () => new TimePicker(), TimePicker.TextColorProperty),

		new(getImageSourceProperty, "Image", static () => new Image(), Image.SourceProperty),
		new(getImageSourceProperty, "ImageButton", static () => new ImageButton(), ImageButton.SourceProperty),

		new(getImageAspectProperty, "Image", static () => new Image(), Image.AspectProperty),
		new(getImageAspectProperty, "ImageButton", static () => new ImageButton(), ImageButton.AspectProperty),

		new(getImageIsOpaqueProperty, "Image", static () => new Image(), Image.IsOpaqueProperty),
		new(getImageIsOpaqueProperty, "ImageButton", static () => new ImageButton(), ImageButton.IsOpaqueProperty),

		new(getPlaceholderProperty, "SearchBar", static () => new SearchBar(), SearchBar.PlaceholderProperty),
		new(getPlaceholderProperty, "InputView", static () => new Entry(), InputView.PlaceholderProperty),
		new(getPlaceholderProperty, "SearchHandler", static () => new SearchHandler(), SearchHandler.PlaceholderProperty),

		new(getPlaceholderColorProperty, "SearchBar", static () => new SearchBar(), SearchBar.PlaceholderColorProperty),
		new(getPlaceholderColorProperty, "InputView", static () => new Entry(), InputView.PlaceholderColorProperty),
		new(getPlaceholderColorProperty, "SearchHandler", static () => new SearchHandler(), SearchHandler.PlaceholderColorProperty)
	];

	static readonly UnsupportedBindablePropertyCase[] unsupportedCases =
	[
		new(getPaddingProperty, "Padding"),
		new(getFontFamilyProperty, "FontFamily"),
		new(getFontSizeProperty, "FontSize"),
		new(getFontAttributesProperty, "FontAttributes"),
		new(getTextColorProperty, "TextColor"),
		new(getImageSourceProperty, "Source"),
		new(getImageAspectProperty, "Aspect"),
		new(getImageIsOpaqueProperty, "IsOpaque"),
		new(getPlaceholderProperty, "Placeholder"),
		new(getPlaceholderColorProperty, "PlaceholderColor")
	];

	[TestCaseSource(nameof(supportedCases))]
	public void SupportedBindableReturnsExpectedProperty(SupportedBindablePropertyCase supportedCase)
	{
		var property = InvokeGetProperty(supportedCase.MethodName, supportedCase.CreateBindable());

		Assert.That(property, Is.SameAs(supportedCase.ExpectedProperty));
	}

	[TestCaseSource(nameof(unsupportedCases))]
	public void UnsupportedBindableThrowsNotSupportedException(UnsupportedBindablePropertyCase unsupportedCase)
	{
		var unsupportedBindable = new BoxView();

		var exception = Assert.Throws<NotSupportedException>(() => InvokeGetProperty(unsupportedCase.MethodName, unsupportedBindable));

		Assert.That(exception?.Message, Is.EqualTo($"{unsupportedBindable.GetType()} does not expose a public {unsupportedCase.PropertyName} bindable property."));
	}

	/// <remarks>
	/// <see cref="ElementExtensions"/>, <see cref="ImageExtensions"/> and <see cref="PlaceholderExtensions"/> constrain their
	/// public API to the supported types, so the internal helper is invoked through reflection here to verify every
	/// mapping (including the unsupported fallbacks that the constrained public API cannot reach).
	/// </remarks>
	static BindableProperty InvokeGetProperty(string methodName, BindableObject bindable)
	{
		var bindablePropertyHelpersType = typeof(ElementExtensions).Assembly.GetType("CommunityToolkit.Maui.Markup.BindablePropertyHelpers")
			?? throw new InvalidOperationException("Unable to locate CommunityToolkit.Maui.Markup.BindablePropertyHelpers");
		var method = bindablePropertyHelpersType.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static)
			?? throw new InvalidOperationException($"Unable to locate {methodName}");

		try
		{
			return (BindableProperty)(method.Invoke(null, [bindable]) ?? throw new InvalidOperationException($"{methodName} returned null"));
		}
		catch (TargetInvocationException ex) when (ex.InnerException is not null)
		{
			ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
			throw; // Unreachable; keeps the compiler happy
		}
	}

	public sealed record SupportedBindablePropertyCase(string MethodName, string BindableName, Func<BindableObject> CreateBindable, BindableProperty ExpectedProperty)
	{
		public override string ToString() => $"{MethodName}({BindableName})";
	}

	public sealed record UnsupportedBindablePropertyCase(string MethodName, string PropertyName)
	{
		public override string ToString() => $"{MethodName}(BoxView)";
	}
}