using CommunityToolkit.Maui.Markup.UnitTests.Base;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests
{

	[TestFixture]
	class ViewExtensionsTests : BaseMarkupTestFixture<BoxView>
	{
		[Test]
		public void Start()
			=> TestPropertiesSet(v => v.Start(), (View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.Start));

		[Test]
		public void CenterHorizontal()
			=> TestPropertiesSet(v => v.CenterHorizontal(), (View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.Center));

		[Test]
		public void FillHorizontal()
			=> TestPropertiesSet(v => v.FillHorizontal(), (View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.Fill));

		[Test]
		public void End()
			=> TestPropertiesSet(v => v.End(), (View.HorizontalOptionsProperty, LayoutOptions.Start, LayoutOptions.End));

		[Test]
		public void Top()
			=> TestPropertiesSet(v => v.Top(), (View.VerticalOptionsProperty, LayoutOptions.End, LayoutOptions.Start));

		[Test]
		public void Bottom()
			=> TestPropertiesSet(v => v.Bottom(), (View.VerticalOptionsProperty, LayoutOptions.Start, LayoutOptions.End));

		[Test]
		public void CenterVertical()
			=> TestPropertiesSet(v => v.CenterVertical(), (View.VerticalOptionsProperty, LayoutOptions.End, LayoutOptions.Center));

		[Test]
		public void FillVertical()
			=> TestPropertiesSet(v => v.FillVertical(), (View.VerticalOptionsProperty, LayoutOptions.End, LayoutOptions.Fill));

		[Test]
		public void Center()
			=> TestPropertiesSet(
					v => v.Center(),
					(View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.Center),
					(View.VerticalOptionsProperty, LayoutOptions.End, LayoutOptions.Center));

		[Test]
		public void Fill()
			=> TestPropertiesSet(
					v => v.Fill(),
					(View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.Fill),
					(View.VerticalOptionsProperty, LayoutOptions.End, LayoutOptions.Fill));

		[Test]
		public void MarginThickness()
			=> TestPropertiesSet(v => v.Margin(new Thickness(1)), (View.MarginProperty, new Thickness(0), new Thickness(1)));

		[Test]
		public void MarginUniform()
			=> TestPropertiesSet(v => v.Margin(1), (View.MarginProperty, new Thickness(0), new Thickness(1)));

		[Test]
		public void MarginHorizontalVertical()
			=> TestPropertiesSet(v => v.Margin(1, 2), (View.MarginProperty, new Thickness(0), new Thickness(1, 2)));

		[Test]
		public void Margins()
			=> TestPropertiesSet(v => v.Margins(left: 1, top: 2, right: 3, bottom: 4), (View.MarginProperty, new Thickness(0), new Thickness(1, 2, 3, 4)));

		[Test]
		public void SupportDerivedFromView()
		{
			Assert.That(new DerivedFromView()
						.Start()
						.CenterHorizontal()
						.FillHorizontal()
						.End()
						.Top()
						.Bottom()
						.CenterVertical()
						.FillVertical()
						.Center()
						.Fill()
						.Margin(new Thickness(1))
						.Margin(1, 2)
						.Margins(left: 1, top: 2, right: 3, bottom: 4),
						Is.InstanceOf<DerivedFromView>());
		}

		class DerivedFromView : BoxView { }
	}
}

namespace CommunityToolkit.Maui.Markup.UnitTests
{

	[TestFixture]
	class LeftToRightViewExtensionsTests : BaseMarkupTestFixture<BoxView>
	{
		[Test]
		public void Left()
			=> TestPropertiesSet(v => LeftToRight.ViewExtensions.Left(v), (View.HorizontalOptionsProperty, LayoutOptions.Start));

		[Test]
		public void Right()
			=> TestPropertiesSet(v => LeftToRight.ViewExtensions.Right(v), (View.HorizontalOptionsProperty, LayoutOptions.End));
	}
}

namespace CommunityToolkit.Maui.Markup.UnitTests
{

	[TestFixture]
	class RightToLeftViewExtensionsTests : BaseMarkupTestFixture<BoxView>
	{
		[Test]
		public void Left()
			=> TestPropertiesSet(v => RightToLeft.ViewExtensions.Left(v), (View.HorizontalOptionsProperty, LayoutOptions.End));

		[Test]
		public void Right()
			=> TestPropertiesSet(v => RightToLeft.ViewExtensions.Right(v), (View.HorizontalOptionsProperty, LayoutOptions.Start));
	}
}