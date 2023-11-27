using CommunityToolkit.Maui.Markup.RightToLeft;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class LabelExtensionsRightToLeftTests : BaseMarkupTestFixture<Label>
{
	[Test]
	public void TextLeft()
		=> TestPropertiesSet(l => l.TextLeft(), (Label.HorizontalTextAlignmentProperty, TextAlignment.Start, TextAlignment.End));

	[Test]
	public void TextRight()
		=> TestPropertiesSet(l => l.TextRight(), (Label.HorizontalTextAlignmentProperty, TextAlignment.End, TextAlignment.Start));

	[Test]
	public void SupportDerivedFromLabel() => Assert.That(new DerivedFromLabel().TextLeft().TextRight(), Is.InstanceOf<DerivedFromLabel>());

	class DerivedFromLabel : Label { }
}