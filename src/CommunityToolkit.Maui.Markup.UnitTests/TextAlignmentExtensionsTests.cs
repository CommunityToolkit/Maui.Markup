using CommunityToolkit.Maui.Markup.UnitTests.Base;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests
{
	[TestFixture]
	class TextAlignmentExtensionsTests : BaseMarkupTestFixture<Picker>
	{
		[Test]
		public void TextStart()
			=> TestPropertiesSet(l => l.TextStart(), (TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.End, TextAlignment.Start));

		[Test]
		public void TextCenterHorizontal()
			=> TestPropertiesSet(l => l.TextCenterHorizontal(), (TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.Start, TextAlignment.Center));

		[Test]
		public void TextEnd()
			=> TestPropertiesSet(l => l.TextEnd(), (TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.Start, TextAlignment.End));

		[Test]
		public void TextTop()
			=> TestPropertiesSet(l => l.TextTop(), (TextAlignmentElement.VerticalTextAlignmentProperty, TextAlignment.End, TextAlignment.Start));

		[Test]
		public void TextCenterVertical()
			=> TestPropertiesSet(l => l.TextCenterVertical(), (TextAlignmentElement.VerticalTextAlignmentProperty, TextAlignment.Start, TextAlignment.Center));

		[Test]
		public void TextBottom()
			=> TestPropertiesSet(l => l.TextBottom(), (TextAlignmentElement.VerticalTextAlignmentProperty, TextAlignment.Start, TextAlignment.End));

		[Test]
		public void TextCenter()
			=> TestPropertiesSet(
					l => l.TextCenter(),
					(TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.Start, TextAlignment.Center),
					(TextAlignmentElement.VerticalTextAlignmentProperty, TextAlignment.Start, TextAlignment.Center));

		[Test]
		public void SupportDerivedFromBindable()
		{
			Assert.IsInstanceOf<DerivedFromSearchBar>(
				new DerivedFromSearchBar()
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

		class DerivedFromSearchBar : SearchBar { }
	}
}

namespace CommunityToolkit.Maui.Markup.UnitTests
{
	using CommunityToolkit.Maui.Markup.LeftToRight;

	[TestFixture]
	class LeftToRightTextAlignmentExtensionsTests : BaseMarkupTestFixture<Picker>
	{

		[Test]
		public void TextLeft()
			=> TestPropertiesSet(l => l.TextLeft(), (TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.Start));

		[Test]
		public void TextRight()
			=> TestPropertiesSet(l => l.TextRight(), (TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.End));
	}
}

namespace CommunityToolkit.Maui.Markup.UnitTests
{
	using CommunityToolkit.Maui.Markup.RightToLeft;

	[TestFixture]
	class RightToLeftTextAlignmentExtensionsTests : BaseMarkupTestFixture<Picker>
	{

		[Test]
		public void TextLeft()
			=> TestPropertiesSet(l => l.TextLeft(), (TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.End));

		[Test]
		public void TextRight()
			=> TestPropertiesSet(l => l.TextRight(), (TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.Start));
	}
}