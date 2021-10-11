using Xunit;
using CommunityToolkit.Maui.Markup.LeftToRight;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CommunityToolkit.Maui.Markup.UnitTests
{
    
    public class LabelExtensionsLeftToRightTests : MarkupBaseTest<Label>
    {
        [Fact]
        public void TextLeft()
            => TestPropertiesSet(l => l?.TextLeft(), (Label.HorizontalTextAlignmentProperty, TextAlignment.End, TextAlignment.Start));

        [Fact]
        public void TextRight()
            => TestPropertiesSet(l => l?.TextRight(), (Label.HorizontalTextAlignmentProperty, TextAlignment.Start, TextAlignment.End));

        [Fact]
        public void SupportDerivedFromLabel() => Assert.IsInstanceOf<DerivedFromLabel>(new DerivedFromLabel().TextLeft().TextRight());

        class DerivedFromLabel : Label
        {
        }
    }
}