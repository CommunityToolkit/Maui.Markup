using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Xunit;

namespace CommunityToolkit.Maui.Markup.UnitTests
{
    
	public class ViewExtensionsTests : MarkupBaseTest<BoxView>
	{
		[Fact]
		public void Start()
			=> TestPropertiesSet(v => v?.Start(), (View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.Start));

		[Fact]
		public void CenterHorizontal()
			=> TestPropertiesSet(v => v?.CenterHorizontal(), (View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.Center));

		[Fact]
		public void FillHorizontal()
			=> TestPropertiesSet(v => v?.FillHorizontal(), (View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.Fill));

		[Fact]
		public void End()
			=> TestPropertiesSet(v => v?.End(), (View.HorizontalOptionsProperty, LayoutOptions.Start, LayoutOptions.End));

		[Fact]
		public void StartExpand()
			=> TestPropertiesSet(v => v?.StartExpand(), (View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.StartAndExpand));

		[Fact]
		public void CenterExpandHorizontal()
			=> TestPropertiesSet(v => v?.CenterExpandHorizontal(), (View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.CenterAndExpand));

		[Fact]
		public void FillExpandHorizontal()
			=> TestPropertiesSet(v => v?.FillExpandHorizontal(), (View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.FillAndExpand));

		[Fact]
		public void EndExpand()
			=> TestPropertiesSet(v => v?.EndExpand(), (View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.EndAndExpand));

		[Fact]
		public void Top()
			=> TestPropertiesSet(v => v?.Top(), (View.VerticalOptionsProperty, LayoutOptions.End, LayoutOptions.Start));

		[Fact]
		public void Bottom()
			=> TestPropertiesSet(v => v?.Bottom(), (View.VerticalOptionsProperty, LayoutOptions.Start, LayoutOptions.End));

		[Fact]
		public void CenterVertical()
			=> TestPropertiesSet(v => v?.CenterVertical(), (View.VerticalOptionsProperty, LayoutOptions.End, LayoutOptions.Center));

		[Fact]
		public void FillVertical()
			=> TestPropertiesSet(v => v?.FillVertical(), (View.VerticalOptionsProperty, LayoutOptions.End, LayoutOptions.Fill));

		[Fact]
		public void TopExpand()
			=> TestPropertiesSet(v => v?.TopExpand(), (View.VerticalOptionsProperty, LayoutOptions.End, LayoutOptions.StartAndExpand));

		[Fact]
		public void BottomExpand()
			=> TestPropertiesSet(v => v?.BottomExpand(), (View.VerticalOptionsProperty, LayoutOptions.End, LayoutOptions.EndAndExpand));

		[Fact]
		public void CenterExpandVertical()
			=> TestPropertiesSet(v => v?.CenterExpandVertical(), (View.VerticalOptionsProperty, LayoutOptions.End, LayoutOptions.CenterAndExpand));

		[Fact]
		public void FillExpandVertical()
			=> TestPropertiesSet(v => v?.FillExpandVertical(), (View.VerticalOptionsProperty, LayoutOptions.End, LayoutOptions.FillAndExpand));

		[Fact]
		public void Center()
			=> TestPropertiesSet(
					v => v?.Center(),
					(View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.Center),
					(View.VerticalOptionsProperty, LayoutOptions.End, LayoutOptions.Center));

		[Fact]
		public void Fill()
			=> TestPropertiesSet(
					v => v?.Fill(),
					(View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.Fill),
					(View.VerticalOptionsProperty, LayoutOptions.End, LayoutOptions.Fill));

		[Fact]
		public void CenterExpand()
			=> TestPropertiesSet(
					v => v?.CenterExpand(),
					(View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.CenterAndExpand),
					(View.VerticalOptionsProperty, LayoutOptions.End, LayoutOptions.CenterAndExpand));

		[Fact]
		public void FillExpand()
			=> TestPropertiesSet(
					v => v?.FillExpand(),
					(View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.FillAndExpand),
					(View.VerticalOptionsProperty, LayoutOptions.End, LayoutOptions.FillAndExpand));

		[Fact]
		public void MarginThickness()
			=> TestPropertiesSet(v => v?.Margin(new Thickness(1)), (View.MarginProperty, new Thickness(0), new Thickness(1)));

		[Fact]
		public void MarginUniform()
			=> TestPropertiesSet(v => v?.Margin(1), (View.MarginProperty, new Thickness(0), new Thickness(1)));

		[Fact]
		public void MarginHorizontalVertical()
			=> TestPropertiesSet(v => v?.Margin(1, 2), (View.MarginProperty, new Thickness(0), new Thickness(1, 2)));

		[Fact]
		public void Margins()
			=> TestPropertiesSet(v => v?.Margins(left: 1, top: 2, right: 3, bottom: 4), (View.MarginProperty, new Thickness(0), new Thickness(1, 2, 3, 4)));

		[Fact]
		public void SupportDerivedFromView()
		{
			Assert.IsInstanceOf<DerivedFromView>(
				new DerivedFromView()
				.Start()
				.CenterHorizontal()
				.FillHorizontal()
				.End()
				.StartExpand()
				.CenterExpandHorizontal()
				.FillExpandHorizontal()
				.EndExpand()
				.Top()
				.Bottom()
				.CenterVertical()
				.FillVertical()
				.TopExpand()
				.BottomExpand()
				.CenterExpandVertical()
				.FillExpandVertical()
				.Center()
				.Fill()
				.CenterExpand()
				.FillExpand()
				.Margin(new Thickness(1))
				.Margin(1, 2)
				.Margins(left: 1, top: 2, right: 3, bottom: 4));
		}

		class DerivedFromView : BoxView { }
	}
}