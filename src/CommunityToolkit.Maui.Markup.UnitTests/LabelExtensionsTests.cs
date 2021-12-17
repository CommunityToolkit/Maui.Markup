using System;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class LabelExtensionsTests : BaseMarkupTestFixture<Label>
{
	[Test]
	public void TextStart()
		=> TestPropertiesSet(l => l?.TextStart(), (Label.HorizontalTextAlignmentProperty, TextAlignment.End, TextAlignment.Start));

	[Test]
	public void TextCenterHorizontal()
		=> TestPropertiesSet(l => l?.TextCenterHorizontal(), (Label.HorizontalTextAlignmentProperty, TextAlignment.Start, TextAlignment.Center));

	[Test]
	public void TextEnd()
		=> TestPropertiesSet(l => l?.TextEnd(), (Label.HorizontalTextAlignmentProperty, TextAlignment.Start, TextAlignment.End));

	[Test]
	public void TextTop()
		=> TestPropertiesSet(l => l?.TextTop(), (Label.VerticalTextAlignmentProperty, TextAlignment.End, TextAlignment.Start));

	[Test]
	public void TextCenterVertical()
		=> TestPropertiesSet(l => l?.TextCenterVertical(), (Label.VerticalTextAlignmentProperty, TextAlignment.Start, TextAlignment.Center));

	[Test]
	public void TextBottom()
		=> TestPropertiesSet(l => l?.TextBottom(), (Label.VerticalTextAlignmentProperty, TextAlignment.Start, TextAlignment.End));

	[Test]
	public void TextCenter()
		=> TestPropertiesSet(
				l => l?.TextCenter(),
				(Label.HorizontalTextAlignmentProperty, TextAlignment.Start, TextAlignment.Center),
				(Label.VerticalTextAlignmentProperty, TextAlignment.Start, TextAlignment.Center));

	[Test]
	public void FontSize()
		=> TestPropertiesSet(l => l?.FontSize(8.0), (Label.FontSizeProperty, 6.0, 8.0));

	[Test]
	public void Bold()
		=> TestPropertiesSet(l => l?.Bold(), (Label.FontAttributesProperty, FontAttributes.None, FontAttributes.Bold));

	[Test]
	public void Italic()
		=> TestPropertiesSet(l => l?.Italic(), (Label.FontAttributesProperty, FontAttributes.None, FontAttributes.Italic));

	[Test]
	public void FormattedTextSingleSpan()
	{
		Bindable.FormattedText = null;
		Bindable.FormattedText(
			new Span { BackgroundColor = Colors.Blue }
		);

		var spans = Bindable.FormattedText?.Spans;
		Assert.That(spans?.Count == 1 && spans[0].BackgroundColor == Colors.Blue);
	}

	[Test]
	public void FormattedTextMultipleSpans()
	{
		Bindable.FormattedText = null;
		Bindable.FormattedText(
			new Span { BackgroundColor = Colors.Blue },
			new Span { BackgroundColor = Colors.Green }
		);

		var spans = Bindable.FormattedText?.Spans;
		Assert.That(spans?.Count == 2 && spans[0].BackgroundColor == Colors.Blue && spans[1].BackgroundColor == Colors.Green);
	}

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
			.Italic()
			.FormattedText());
	}

	class DerivedFromLabel : Label { }
}