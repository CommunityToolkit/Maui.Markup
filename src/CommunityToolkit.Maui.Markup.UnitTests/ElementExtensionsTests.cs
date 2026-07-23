using CommunityToolkit.Maui.Markup.UnitTests.Base;
using NUnit.Framework;
namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class ElementExtensionsTests : BaseMarkupTestFixture<Label>
{
	static readonly TextStyleCase[] textStyleCases =
	[
		new("DatePicker", static () => new DatePicker(), DatePicker.FontFamilyProperty, DatePicker.FontSizeProperty, DatePicker.FontAttributesProperty, DatePicker.TextColorProperty, static bindable => ((DatePicker)bindable).Font("AFontName", 8, bold: true, italic: true).TextColor(Colors.Purple)),
		new("Picker", static () => new Picker(), Picker.FontFamilyProperty, Picker.FontSizeProperty, Picker.FontAttributesProperty, Picker.TextColorProperty, static bindable => ((Picker)bindable).Font("AFontName", 8, bold: true, italic: true).TextColor(Colors.Purple)),
		new("RadioButton", static () => new RadioButton(), RadioButton.FontFamilyProperty, RadioButton.FontSizeProperty, RadioButton.FontAttributesProperty, RadioButton.TextColorProperty, static bindable => ((RadioButton)bindable).Font("AFontName", 8, bold: true, italic: true).TextColor(Colors.Purple)),
		new("TimePicker", static () => new TimePicker(), TimePicker.FontFamilyProperty, TimePicker.FontSizeProperty, TimePicker.FontAttributesProperty, TimePicker.TextColorProperty, static bindable => ((TimePicker)bindable).Font("AFontName", 8, bold: true, italic: true).TextColor(Colors.Purple))
	];

	[Test]
	public void RemoveDynamicResources()
	{
		ArgumentNullException.ThrowIfNull(Application.Current);

		var label = AssertDynamicResources();

		Application.Current.Windows[0].Page = new ContentPage
		{
			Content = label
		};

		label.RemoveDynamicResources(Label.TextProperty, Label.TextColorProperty);
		label.Resources["TextKey"] = "ChangedTextValue";
		label.Resources["ColorKey"] = Colors.Green;

		Assert.Multiple(() =>
		{
			Assert.That(label.Text, Is.EqualTo("TextValue"));
			Assert.That(label.TextColor, Is.EqualTo(Colors.Green));
		});
	}

	[Test]
	public void EffectSingle()
	{
		Bindable.Effects.Clear();
		Assume.That(Bindable.Effects.Count, Is.EqualTo(0));

		var effect1 = new NullEffect();
		Bindable.Effects(effect1);

		Assert.That(Bindable.Effects, Has.Count.EqualTo(1));
		Assert.That(Bindable.Effects.Contains(effect1));
	}

	[Test]
	public void EffectsMultiple()
	{
		Bindable.Effects.Clear();
		Assume.That(Bindable.Effects.Count, Is.EqualTo(0));

		NullEffect effect1 = new(), effect2 = new();
		Bindable.Effects(effect1, effect2);

		Assert.Multiple(() =>
		{
			Assert.That(Bindable.Effects, Has.Count.EqualTo(2));
			Assert.That(Bindable.Effects.Contains(effect1));
			Assert.That(Bindable.Effects.Contains(effect2));
		});
	}

	[Test]
	public void FontSize()
		=> TestPropertiesSet(l => l.FontSize(8), (Label.FontSizeProperty, 6.0, 8.0));

	[Test]
	public void Bold()
		=> TestPropertiesSet(l => l.Bold(), (Label.FontAttributesProperty, FontAttributes.None, FontAttributes.Bold));

	[Test]
	public void Italic()
		=> TestPropertiesSet(l => l.Italic(), (Label.FontAttributesProperty, FontAttributes.None, FontAttributes.Italic));

	[Test]
	public void FontWithPositionalParameters()
		=> TestPropertiesSet(
			l => l.Font("AFontName", 8, true, true),
			(Label.FontSizeProperty, 6.0, 8.0),
			(Label.FontAttributesProperty, FontAttributes.None, FontAttributes.Bold | FontAttributes.Italic),
			(Label.FontFamilyProperty, string.Empty, "AFontName"));

	[Test]
	public void FontWithSizeNamedParameter()
		=> TestPropertiesSet(l => l.Font(size: 8), (Label.FontSizeProperty, 6.0, 8.0));

	[Test]
	public void FontWithBoldNamedParameter()
		=> TestPropertiesSet(l => l.Font(bold: true), (Label.FontAttributesProperty, FontAttributes.None, FontAttributes.Bold));

	[Test]
	public void FontWithItalicNamedParameter()
		=> TestPropertiesSet(l => l.Font(italic: true), (Label.FontAttributesProperty, FontAttributes.None, FontAttributes.Italic));

	[Test]
	public void FontWithFamilyNamedParameter()
		=> TestPropertiesSet(l => l.Font(family: "AFontName"), (Label.FontFamilyProperty, string.Empty, "AFontName"));

	[Test]
	public void FontSupportedOnSpan()
	{
		var span = new Span().Font("AFontName", 8, bold: true, italic: true);

		Assert.Multiple(() =>
		{
			Assert.That(span.GetValue(Span.FontFamilyProperty), Is.EqualTo("AFontName"));
			Assert.That(span.GetValue(Span.FontSizeProperty), Is.EqualTo(8.0));
			Assert.That(span.GetValue(Span.FontAttributesProperty), Is.EqualTo(FontAttributes.Bold | FontAttributes.Italic));
		});
	}

	[Test]
	public void FontSizeSupportedOnSpan()
		=> Assert.That(new Span().FontSize(8).GetValue(Span.FontSizeProperty), Is.EqualTo(8.0));

	[Test]
	public void BoldSupportedOnSpan()
		=> Assert.That(new Span().Bold().GetValue(Span.FontAttributesProperty), Is.EqualTo(FontAttributes.Bold));

	[Test]
	public void ItalicSupportedOnSpan()
		=> Assert.That(new Span().Italic().GetValue(Span.FontAttributesProperty), Is.EqualTo(FontAttributes.Italic));

	[Test]
	public void SupportDerivedFromSpan()
		=> Assert.That(new DerivedFromSpan().Font("AFontName").FontSize(8).Bold().Italic(), Is.InstanceOf<DerivedFromSpan>());

	[Test]
	public void FontSupportedOnSearchHandler()
	{
		var searchHandler = new SearchHandler().Font("AFontName", 8, bold: true, italic: true);

		Assert.Multiple(() =>
		{
			Assert.That(searchHandler.GetValue(SearchHandler.FontFamilyProperty), Is.EqualTo("AFontName"));
			Assert.That(searchHandler.GetValue(SearchHandler.FontSizeProperty), Is.EqualTo(8.0));
			Assert.That(searchHandler.GetValue(SearchHandler.FontAttributesProperty), Is.EqualTo(FontAttributes.Bold | FontAttributes.Italic));
		});
	}

	[Test]
	public void FontSizeSupportedOnSearchHandler()
		=> Assert.That(new SearchHandler().FontSize(8).GetValue(SearchHandler.FontSizeProperty), Is.EqualTo(8.0));

	[Test]
	public void BoldSupportedOnSearchHandler()
		=> Assert.That(new SearchHandler().Bold().GetValue(SearchHandler.FontAttributesProperty), Is.EqualTo(FontAttributes.Bold));

	[Test]
	public void ItalicSupportedOnSearchHandler()
		=> Assert.That(new SearchHandler().Italic().GetValue(SearchHandler.FontAttributesProperty), Is.EqualTo(FontAttributes.Italic));

	[Test]
	public void SupportDerivedFromSearchHandler()
		=> Assert.That(new DerivedFromSearchHandler().Font("AFontName").FontSize(8).Bold().Italic(), Is.InstanceOf<DerivedFromSearchHandler>());

	[TestCaseSource(nameof(textStyleCases))]
	public void FontAndTextColorSupportedOnTextStyleElement(TextStyleCase textStyleCase)
	{
		var bindable = textStyleCase.Create();

		textStyleCase.ApplyFontAndTextColor(bindable);

		Assert.Multiple(() =>
		{
			Assert.That(bindable.GetValue(textStyleCase.FontFamilyProperty), Is.EqualTo("AFontName"));
			Assert.That(bindable.GetValue(textStyleCase.FontSizeProperty), Is.EqualTo(8.0));
			Assert.That(bindable.GetValue(textStyleCase.FontAttributesProperty), Is.EqualTo(FontAttributes.Bold | FontAttributes.Italic));
			Assert.That(bindable.GetValue(textStyleCase.TextColorProperty), Is.EqualTo(Colors.Purple));
		});
	}

	[Test]
	public void FontSizeOnUnsupportedTextStyleElementThrowsNotSupportedException()
		=> Assert.Throws<NotSupportedException>(() => new UnsupportedTextStyleView().FontSize(8));

	[Test]
	public void BoldOnUnsupportedTextStyleElementThrowsNotSupportedException()
		=> Assert.Throws<NotSupportedException>(() => new UnsupportedTextStyleView().Bold());

	[Test]
	public void ItalicOnUnsupportedTextStyleElementThrowsNotSupportedException()
		=> Assert.Throws<NotSupportedException>(() => new UnsupportedTextStyleView().Italic());

	[Test]
	public void FontFamilyOnUnsupportedTextStyleElementThrowsNotSupportedException()
		=> Assert.Throws<NotSupportedException>(() => new UnsupportedTextStyleView().Font(family: "AFontName"));

	[Test]
	public void TextColorOnUnsupportedTextStyleElementThrowsNotSupportedException()
		=> Assert.Throws<NotSupportedException>(() => new UnsupportedTextStyleView().TextColor(Colors.Red));

	[Test]
	public void TextOnUnsupportedTextElementThrowsNotSupportedException()
		=> Assert.Throws<NotSupportedException>(() => new UnsupportedTextView().Text("Hello World"));

	static Label AssertDynamicResources()
	{
		var label = new Label
		{
			Resources = new ResourceDictionary
			{
				{
					"TextKey", "TextValue"
				},
				{
					"ColorKey", Colors.Green
				}
			}
		};

		Assert.Multiple(() =>
		{
			Assert.That(label.Text, Is.EqualTo(Label.TextProperty.DefaultValue));
			Assert.That(label.TextColor, Is.EqualTo(Label.TextColorProperty.DefaultValue));
		});

		label.DynamicResources((Label.TextProperty, "TextKey"),
			(Label.TextColorProperty, "ColorKey"));

		Assert.Multiple(() =>
		{
			Assert.That(label.Text, Is.EqualTo("TextValue"));
			Assert.That(label.TextColor, Is.EqualTo(Colors.Green));
		});

		return label;
	}

	public sealed record TextStyleCase(
		string Name,
		Func<BindableObject> Create,
		BindableProperty FontFamilyProperty,
		BindableProperty FontSizeProperty,
		BindableProperty FontAttributesProperty,
		BindableProperty TextColorProperty,
		Action<BindableObject> ApplyFontAndTextColor)
	{
		public override string ToString() => Name;
	}
}
class UnsupportedTextStyleView : View, ITextStyle
{
	public Color TextColor { get; set; } = Colors.Black;

	public Microsoft.Maui.Font Font => Microsoft.Maui.Font.Default;

	public double CharacterSpacing => 0;
}

sealed class UnsupportedTextView : UnsupportedTextStyleView, IText
{
	public string Text => string.Empty;
}

sealed class DerivedFromSpan : Span
{
}

sealed class DerivedFromSearchHandler : SearchHandler
{
}