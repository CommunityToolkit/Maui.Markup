using CommunityToolkit.Maui.Markup.UnitTests.Base;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class ElementExtensionsTests : BaseMarkupTestFixture<Label>
{
	[Test]
	public void RemoveDynamicResources()
	{
		var label = AssertDynamicResources();

		label.RemoveDynamicResources(Label.TextProperty, Label.TextColorProperty);
		label.Resources["TextKey"] = "ChangedTextValue";
		label.Resources["ColorKey"] = Colors.Green;

		Assert.That(label.Text, Is.EqualTo("TextValue"));
		Assert.That(label.TextColor, Is.EqualTo(Colors.Green));
	}

	[Test]
	public void EffectSingle()
	{
		Bindable.Effects.Clear();
		Assume.That(Bindable.Effects.Count, Is.EqualTo(0));

		var effect1 = new NullEffect();
		Bindable.Effects(effect1);

		Assert.That(Bindable.Effects.Count, Is.EqualTo(1));
		Assert.That(Bindable.Effects.Contains(effect1));
	}

	[Test]
	public void EffectsMultiple()
	{
		Bindable.Effects.Clear();
		Assume.That(Bindable.Effects.Count, Is.EqualTo(0));

		NullEffect effect1 = new NullEffect(), effect2 = new NullEffect();
		Bindable.Effects(effect1, effect2);

		Assert.That(Bindable.Effects.Count, Is.EqualTo(2));
		Assert.That(Bindable.Effects.Contains(effect1));
		Assert.That(Bindable.Effects.Contains(effect2));
	}

	[Test]
	public void FontSize()
		=> TestPropertiesSet(l => l.FontSize(8), (FontElement.FontSizeProperty, 6.0, 8.0));

	[Test]
	public void Bold()
		=> TestPropertiesSet(l => l.Bold(), (FontElement.FontAttributesProperty, FontAttributes.None, FontAttributes.Bold));

	[Test]
	public void Italic()
		=> TestPropertiesSet(l => l.Italic(), (FontElement.FontAttributesProperty, FontAttributes.None, FontAttributes.Italic));

	[Test]
	public void FontWithPositionalParameters()
		=> TestPropertiesSet(
				l => l.Font("AFontName", 8, true, true),
				(FontElement.FontSizeProperty, 6.0, 8.0),
				(FontElement.FontAttributesProperty, FontAttributes.None, FontAttributes.Bold | FontAttributes.Italic),
				(FontElement.FontFamilyProperty, string.Empty, "AFontName"));

	[Test]
	public void FontWithSizeNamedParameter()
		=> TestPropertiesSet(l => l.Font(size: 8), (FontElement.FontSizeProperty, 6.0, 8.0));

	[Test]
	public void FontWithBoldNamedParameter()
		=> TestPropertiesSet(l => l.Font(bold: true), (FontElement.FontAttributesProperty, FontAttributes.None, FontAttributes.Bold));

	[Test]
	public void FontWithItalicNamedParameter()
		=> TestPropertiesSet(l => l.Font(italic: true), (FontElement.FontAttributesProperty, FontAttributes.None, FontAttributes.Italic));

	[Test]
	public void FontWithFamilyNamedParameter()
		=> TestPropertiesSet(l => l.Font(family: "AFontName"), (FontElement.FontFamilyProperty, string.Empty, "AFontName"));

	[Test]
	public void TextColor_ProvidedColor()
		=> TestPropertiesSet(l => l.TextColor(Colors.Red), (TextElement.TextColorProperty, Colors.Red));

	[Test]
	public void TextColor_CustomColor()
		=> TestPropertiesSet(l => l.TextColor(new Color(0.124f, 0.654f, 0.9234f, 0.100f)), (TextElement.TextColorProperty, new Color(0.124f, 0.654f, 0.9234f, 0.100f)));

	[Test]
	public void Text_NoColor()
		=> TestPropertiesSet(l => l.Text("Hello World"), (Label.TextProperty, "Hello World"));

	[Test]
	public void Text_ProvidedColor()
		=> TestPropertiesSet(l => l.Text("Hello World", Colors.Green), (Label.TextProperty, "Hello World"), (TextElement.TextColorProperty, Colors.Green));

	[Test]
	public void Text_CustomColor()
		=> TestPropertiesSet(l => l.Text("Hello World", new Color(250, 5, 128, 1)), (Label.TextProperty, "Hello World"), (TextElement.TextColorProperty, new Color(250, 5, 128, 1)));

	[Test]
	public void SupportDerivedFromLabel()
	{
		Assert.IsInstanceOf<DerivedFromLabel>(
			new DerivedFromLabel()
			.Effects(new NullEffect())
			.FontSize(8)
			.Bold()
			.Italic()
			.Text("Hello World", new Color(255, 255, 128, 1))
			.Font("AFontName", 8, true, true));
	}

	static Label AssertDynamicResources()
	{
		var label = new Label { Resources = new ResourceDictionary { { "TextKey", "TextValue" }, { "ColorKey", Colors.Green } } };

		Assert.That(label.Text, Is.EqualTo(Label.TextProperty.DefaultValue));
		Assert.That(label.TextColor, Is.EqualTo(Label.TextColorProperty.DefaultValue));

		label.DynamicResources((Label.TextProperty, "TextKey"),
							   (Label.TextColorProperty, "ColorKey"));

		Assert.That(label.Text, Is.EqualTo("TextValue"));
		Assert.That(label.TextColor, Is.EqualTo(Colors.Green));

		return label;
	}

	class DerivedFromLabel : Label
	{
	}
}