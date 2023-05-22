using CommunityToolkit.Maui.Markup.UnitTests.Base;
using Microsoft.Maui.Layouts;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class AbsoluteLayoutExtensionsTest : BaseMarkupTestFixture<BoxView>
{
	[Test]
	public void LayoutFlags() => TestPropertiesSet(
		b => b.LayoutFlags(AbsoluteLayoutFlags.PositionProportional),
		(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.PositionProportional));

	[Test]
	public void MultipleLayoutFlagsUsingBooleanLogic() => TestPropertiesSet(
		b => b.LayoutFlags(AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.XProportional),
		(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.XProportional));

	[Test]
	public void MultipleLayoutFlagsUsingParams() => TestPropertiesSet(
		b => b.LayoutFlags(AbsoluteLayoutFlags.PositionProportional, AbsoluteLayoutFlags.XProportional),
		(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.XProportional));

	[Test]
	public void LayoutBoundsPositionOnlyDouble() => TestPropertiesSet(
		b =>
		{
			b.LayoutBounds(100, 100);
		},
		(AbsoluteLayout.LayoutBoundsProperty, new Rect(100, 100, -1, -1)));

	[Test]
	public void LayoutBoundsPositionOnlyPoint() => TestPropertiesSet(
		b =>
		{
			b.LayoutBounds(new Point(100, 100));
		},
		(AbsoluteLayout.LayoutBoundsProperty, new Rect(100, 100, -1, -1)));

	[Test]
	public void LayoutBoundsRect() => TestPropertiesSet(
		b =>
		{
			b.LayoutBounds(new Rect(100, 100, 100, 100));
		},
		(AbsoluteLayout.LayoutBoundsProperty, new Rect(100, 100, 100, 100)));

	[Test]
	public void LayoutBoundsPointAndSize() => TestPropertiesSet(
		b =>
		{
			b.LayoutBounds(new Point(100, 100), new Size(100, 100));
		},
		(AbsoluteLayout.LayoutBoundsProperty, new Rect(100, 100, 100, 100)));

	[Test]
	public void LayoutBoundsDoubleAndSize() => TestPropertiesSet(
		b =>
		{
			b.LayoutBounds(100, 100, new Size(100, 100));
		},
		(AbsoluteLayout.LayoutBoundsProperty, new Rect(100, 100, 100, 100)));

	[Test]
	public void LayoutBoundsPointAndDouble() => TestPropertiesSet(
		b =>
		{
			b.LayoutBounds(new Point(100, 100), 100, 100);
		},
		(AbsoluteLayout.LayoutBoundsProperty, new Rect(100, 100, 100, 100)));

	[Test]
	public void LayoutBoundsAllDouble() => TestPropertiesSet(
		b =>
		{
			b.LayoutBounds(100, 100, 100, 100);
		},
		(AbsoluteLayout.LayoutBoundsProperty, new Rect(100, 100, 100, 100)));

	// Cannot use TestPropertiesSet() because ClearLayoutFlags sets AbsoluteLayout.LayoutFlagsProperty to its default 
	[Test]
	public void ClearLayoutFlags()
	{
		var label = new Label().ClearLayoutFlags();
		Assert.AreEqual(AbsoluteLayoutFlags.None, label.GetPropertyIfSet(AbsoluteLayout.LayoutFlagsProperty, (AbsoluteLayoutFlags)(-1)));
	}

	// Cannot use TestPropertiesSet() because ClearLayoutFlags sets AbsoluteLayout.LayoutFlagsProperty to its default 
	[Test]
	public void ClearLayoutFlagsAfterSettingLayoutFlags()
	{
		var label = new Label().LayoutFlags(AbsoluteLayoutFlags.XProportional).ClearLayoutFlags();
		Assert.AreEqual(AbsoluteLayoutFlags.None, label.GetPropertyIfSet(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.XProportional));
	}
}