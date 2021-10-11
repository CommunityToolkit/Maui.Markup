using CommunityToolkit.Maui.Markup.RightToLeft;
using Microsoft.Maui.Controls;
using Xunit;

namespace CommunityToolkit.Maui.Markup.UnitTests
{
    
	public class ViewExtensionsRightToLeftTests : MarkupBaseTest<BoxView>
	{
		[Fact]
		public void Left()
			=> TestPropertiesSet(v => v?.Left(), (View.HorizontalOptionsProperty, LayoutOptions.Start, LayoutOptions.End));

		[Fact]
		public void Right()
			=> TestPropertiesSet(v => v?.Right(), (View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.Start));

		[Fact]
		public void LeftExpand()
			=> TestPropertiesSet(v => v?.LeftExpand(), (View.HorizontalOptionsProperty, LayoutOptions.Start, LayoutOptions.EndAndExpand));

		[Fact]
		public void RightExpand()
			=> TestPropertiesSet(v => v?.RightExpand(), (View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.StartAndExpand));

		[Fact]
		public void SupportDerivedFromView()
		{
			Assert.IsInstanceOf<DerivedFromView>(
				new DerivedFromView()
				.Left()
				.Right()
				.LeftExpand()
				.RightExpand());
		}

		class DerivedFromView : BoxView { }
	}
}