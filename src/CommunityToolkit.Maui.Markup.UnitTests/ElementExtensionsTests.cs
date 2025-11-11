using CommunityToolkit.Maui.Markup.UnitTests.Base;
using NUnit.Framework;
namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class ElementExtensionsTests : BaseMarkupTestFixture<Label>
{
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
}