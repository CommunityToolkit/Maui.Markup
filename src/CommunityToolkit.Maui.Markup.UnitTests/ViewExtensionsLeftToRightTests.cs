using CommunityToolkit.Maui.Markup.LeftToRight;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using NUnit.Framework;
namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class ViewExtensionsLeftToRightTests : BaseMarkupTestFixture<BoxView>
{
	[Test]
	public void Left() => TestPropertiesSet(v => v.Left(), (View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.Start));

	[Test]
	public void Right() => TestPropertiesSet(v => v.Right(), (View.HorizontalOptionsProperty, LayoutOptions.Start, LayoutOptions.End));

	[Test]
	public void SupportDerivedFromView()
	{
		Assert.That(new DerivedFromView().Left().Right(), Is.InstanceOf<DerivedFromView>());
	}

	class DerivedFromView : BoxView
	{
	}
}