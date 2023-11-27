using CommunityToolkit.Maui.Markup.UnitTests.Base;
using Microsoft.Maui.Controls.Shapes;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class VisualElementExtensionsTests : BaseMarkupTestFixture<BoxView>
{
	[Test]
	public void Height()
	{
		Bindable.HeightRequest = 1;
		Bindable.Height(2);
		Assert.That(Bindable.HeightRequest, Is.EqualTo(2));
	}

	[Test]
	public void Width()
	{
		Bindable.WidthRequest = 1;
		Bindable.Width(2);
		Assert.That(Bindable.WidthRequest, Is.EqualTo(2));
	}

	[Test]
	public void MinHeight()
	{
		Bindable.MinimumHeightRequest = 1;
		Bindable.MinHeight(2);
		Assert.That(Bindable.MinimumHeightRequest, Is.EqualTo(2));
	}

	[Test]
	public void MinWidth()
	{
		Bindable.MinimumWidthRequest = 1;
		Bindable.MinWidth(2);
		Assert.That(Bindable.MinimumWidthRequest, Is.EqualTo(2));
	}

	[Test]
	public void SizeNotUniform()
	{
		Bindable.WidthRequest = Bindable.HeightRequest = 1;
		Bindable.Size(2, 3);

		Assert.Multiple(() =>
		{
			Assert.That(Bindable.WidthRequest, Is.EqualTo(2));
			Assert.That(Bindable.HeightRequest, Is.EqualTo(3));
		});
	}

	[Test]
	public void SizeUniform()
	{
		Bindable.WidthRequest = Bindable.HeightRequest = 1;
		Bindable.Size(2);

		Assert.Multiple(() =>
		{
			Assert.That(Bindable.WidthRequest, Is.EqualTo(2));
			Assert.That(Bindable.HeightRequest, Is.EqualTo(2));
		});
	}

	[Test]
	public void MinSizeNotUniform()
	{
		Bindable.MinimumWidthRequest = Bindable.MinimumHeightRequest = 1;
		Bindable.MinSize(2, 3);

		Assert.Multiple(() =>
		{
			Assert.That(Bindable.MinimumWidthRequest, Is.EqualTo(2));
			Assert.That(Bindable.MinimumHeightRequest, Is.EqualTo(3));
		});
	}

	[Test]
	public void MinSizeUniform()
	{
		Bindable.MinimumWidthRequest = Bindable.MinimumHeightRequest = 1;
		Bindable.MinSize(2);

		Assert.Multiple(() =>
		{
			Assert.That(Bindable.MinimumWidthRequest, Is.EqualTo(2));
			Assert.That(Bindable.MinimumHeightRequest, Is.EqualTo(2));
		});
	}

	[Test]
	public void AnchorX()
	{
		Bindable.AnchorX(200);
		Assert.That(Bindable.AnchorX, Is.EqualTo(200));
	}

	[Test]
	public void AnchorY()
	{
		Bindable.AnchorY(100);
		Assert.That(Bindable.AnchorY, Is.EqualTo(100));
	}

	[Test]
	public void Anchor()
	{
		Bindable.Anchor(50, 75);

		Assert.Multiple(() =>
		{
			Assert.That(Bindable.AnchorX, Is.EqualTo(50));
			Assert.That(Bindable.AnchorY, Is.EqualTo(75));
		});
	}

	[Test]
	public void Background()
	{
		var bgBrush = new SolidColorBrush(Colors.HotPink);

		Bindable.Background(bgBrush);
		Assert.That(Bindable.Background, Is.EqualTo(bgBrush));
	}

	[Test]
	public void BackgroundColor()
	{
		Bindable.BackgroundColor(Colors.Goldenrod);
		Assert.That(Bindable.BackgroundColor, Is.EqualTo(Colors.Goldenrod));
	}

	[Test]
	public void Clip()
	{
		var ellipse = new EllipseGeometry { Center = new Point(50, 50), RadiusX = 50, RadiusY = 50 };

		Bindable.Clip(ellipse);
		Assert.That(Bindable.Clip, Is.EqualTo(ellipse));
	}

	[Test]
	public void FlowDirection_LeftToRight()
	{
		Bindable.FlowDirection(Microsoft.Maui.FlowDirection.LeftToRight);
		Assert.That(Bindable.FlowDirection, Is.EqualTo(Microsoft.Maui.FlowDirection.LeftToRight));
	}

	[Test]
	public void FlowDirection_RightToLeft()
	{
		Bindable.FlowDirection(Microsoft.Maui.FlowDirection.RightToLeft);
		Assert.That(Bindable.FlowDirection, Is.EqualTo(Microsoft.Maui.FlowDirection.RightToLeft));
	}

	[Test]
	public void FlowDirection_MatchParent()
	{
		Bindable.FlowDirection(Microsoft.Maui.FlowDirection.MatchParent);
		Assert.That(Bindable.FlowDirection, Is.EqualTo(Microsoft.Maui.FlowDirection.MatchParent));
	}

	[Test]
	public void Opacity()
	{
		Bindable.Opacity(0.5);
		Assert.That(Bindable.AnchorX, Is.EqualTo(0.5));
	}

	[Test]
	public void InputTransparent_False()
	{
		Bindable.InputTransparent(false);
		Assert.That(Bindable.InputTransparent, Is.False);
	}

	[Test]
	public void InputTransparent_True()
	{
		Bindable.InputTransparent(true);
		Assert.That(Bindable.InputTransparent, Is.True);
	}

	[Test]
	public void IsEnabled_False()
	{
		Bindable.IsEnabled(false);
		Assert.That(Bindable.IsEnabled, Is.False);
	}

	[Test]
	public void IsEnabled_True()
	{
		Bindable.IsEnabled(true);
		Assert.That(Bindable.IsEnabled, Is.True);
	}

	[Test]
	public void IsVisible_False()
	{
		Bindable.IsVisible(false);
		Assert.That(Bindable.IsVisible, Is.False);
	}

	[Test]
	public void IsVisible_True()
	{
		Bindable.IsVisible(true);
		Assert.That(Bindable.IsVisible, Is.True);
	}

	[Test]
	public void RotationX()
	{
		Bindable.RotationX(2);
		Assert.That(Bindable.RotationX, Is.EqualTo(2));
	}

	[Test]
	public void RotationY()
	{
		Bindable.RotationY(1.5);
		Assert.That(Bindable.RotationY, Is.EqualTo(1.5));
	}

	[Test]
	public void Rotation()
	{
		Bindable.Rotation(2.5);
		Assert.That(Bindable.Rotation, Is.EqualTo(2.5));
	}

	[Test]
	public void ScaleX()
	{
		Bindable.ScaleX(2);
		Assert.That(Bindable.ScaleX, Is.EqualTo(2));
	}

	[Test]
	public void ScaleY()
	{
		Bindable.ScaleY(1.5);
		Assert.That(Bindable.ScaleY, Is.EqualTo(1.5));
	}

	[Test]
	public void Scale_Different()
	{
		Bindable.Scale(2, 3);

		Assert.Multiple(() =>
		{
			Assert.That(Bindable.ScaleX, Is.EqualTo(2));
			Assert.That(Bindable.ScaleY, Is.EqualTo(3));
		});
	}

	[Test]
	public void Scale_Same()
	{
		Bindable.Scale(1.25);

		Assert.Multiple(() =>
		{
			Assert.That(Bindable.ScaleX, Is.EqualTo(1.25));
			Assert.That(Bindable.ScaleY, Is.EqualTo(1.25));
		});
	}

	[Test]
	public void TranslationX()
	{
		Bindable.TranslationX(2);
		Assert.That(Bindable.TranslationX, Is.EqualTo(2));
	}

	[Test]
	public void TranslationY()
	{
		Bindable.TranslationY(1.5);
		Assert.That(Bindable.TranslationY, Is.EqualTo(1.5));
	}

	[Test]
	public void ZIndex()
	{
		Bindable.ZIndex(99);
		Assert.That(Bindable.ZIndex, Is.EqualTo(99));
	}

	[Test]
	public void Style()
	{
		var style = new Style<BoxView>();
		Bindable.Style = null;
		Bindable.Style(style);
		Assert.That(Bindable.Style, Is.EqualTo(style.MauiStyle));
	}

	[Test]
	public void SupportDerivedFromBoxView()
	{
		Assert.That(new DerivedFromBoxView()
								.Height(2)
								.Width(2)
								.MinHeight(2)
								.MinWidth(2)
								.Size(2, 3)
								.Size(2)
								.MinSize(2, 3)
								.MinSize(2)
								.Style(new Style<DerivedFromBoxView>()),
					Is.InstanceOf<DerivedFromBoxView>());
	}

	[Test]
	public void Behaviors()
	{
		var behavior = new BoxViewBehavior();
		Bindable.Behaviors(behavior);
		Assert.That(Bindable.Behaviors, Is.EquivalentTo(new[] { behavior }));
	}

	class DerivedFromBoxView : BoxView
	{
	}

	class BoxViewBehavior : Behavior<BoxView>
	{

	}
}