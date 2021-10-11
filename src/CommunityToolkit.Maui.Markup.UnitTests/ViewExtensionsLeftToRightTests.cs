using Xunit;
using CommunityToolkit.Maui.Markup.LeftToRight;
using Microsoft.Maui.Controls;

namespace CommunityToolkit.Maui.Markup.UnitTests
{
    
    public class ViewExtensionsLeftToRightTests : MarkupBaseTest<BoxView>
    {
        [Fact]
        public void Left()
            => TestPropertiesSet(v => v?.Left(), (View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.Start));

        [Fact]
        public void Right()
            => TestPropertiesSet(v => v?.Right(), (View.HorizontalOptionsProperty, LayoutOptions.Start, LayoutOptions.End));

        [Fact]
        public void LeftExpand()
            => TestPropertiesSet(v => v?.LeftExpand(), (View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.StartAndExpand));

        [Fact]
        public void RightExpand()
            => TestPropertiesSet(v => v?.RightExpand(), (View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.EndAndExpand));

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

        class DerivedFromView : BoxView
        {
        }
    }
}