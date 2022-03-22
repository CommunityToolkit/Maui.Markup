using CommunityToolkit.Maui.Markup.UnitTests.Base;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class TextAlignmentExtensionsTests : BaseMarkupTestFixture<Picker>
{
	[Test]
	public void TextStart()
		=> TestPropertiesSet(l => l.TextStart(), (Label.HorizontalTextAlignmentProperty, TextAlignment.End, TextAlignment.Start));

	[Test]
	public void TextCenterHorizontal()
		=> TestPropertiesSet(l => l.TextCenterHorizontal(), (Label.HorizontalTextAlignmentProperty, TextAlignment.Start, TextAlignment.Center));

	[Test]
	public void TextEnd()
		=> TestPropertiesSet(l => l.TextEnd(), (Label.HorizontalTextAlignmentProperty, TextAlignment.Start, TextAlignment.End));

	[Test]
	public void TextTop()
		=> TestPropertiesSet(l => l.TextTop(), (Label.VerticalTextAlignmentProperty, TextAlignment.End, TextAlignment.Start));

	[Test]
	public void TextCenterVertical()
		=> TestPropertiesSet(l => l.TextCenterVertical(), (Label.VerticalTextAlignmentProperty, TextAlignment.Start, TextAlignment.Center));

	[Test]
	public void TextBottom()
		=> TestPropertiesSet(l => l.TextBottom(), (Label.VerticalTextAlignmentProperty, TextAlignment.Start, TextAlignment.End));

	[Test]
	public void TextCenter()
		=> TestPropertiesSet(
				l => l.TextCenter(),
				(Label.HorizontalTextAlignmentProperty, TextAlignment.Start, TextAlignment.Center),
				(Label.VerticalTextAlignmentProperty, TextAlignment.Start, TextAlignment.Center));

	[Test]
	public void FontSize()
		=> TestPropertiesSet(l => l.FontSize(8.0), (Label.FontSizeProperty, 6.0, 8.0));

	[Test]
	public void Bold()
		=> TestPropertiesSet(l => l.Bold(), (Label.FontAttributesProperty, FontAttributes.None, FontAttributes.Bold));

	[Test]
	public void Italic()
		=> TestPropertiesSet(l => l.Italic(), (Label.FontAttributesProperty, FontAttributes.None, FontAttributes.Italic));

	[Test]
	public void SupportDerivedFromBindable()
	{
		Assert.IsInstanceOf<DerivedFromLabel>(
			new DerivedFromLabel()
			.TextStart()
			.TextCenterHorizontal()
			.TextEnd()
			.TextTop()
			.TextCenterVertical()
			.TextBottom()
			.TextCenter()
			.FontSize(8.0)
			.Bold()
			.Italic());
	}

	class DerivedFromLabel : Picker { }
}