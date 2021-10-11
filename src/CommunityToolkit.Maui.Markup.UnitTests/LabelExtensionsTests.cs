using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Xunit;

namespace CommunityToolkit.Maui.Markup.UnitTests
{
    
    public class LabelExtensionsTests : MarkupBaseTest<Label>
    {
        Label Label => Bindable ?? throw new NullReferenceException();

        [Fact]
        public void TextStart()
            => TestPropertiesSet(l => l?.TextStart(), (Label.HorizontalTextAlignmentProperty, TextAlignment.End, TextAlignment.Start));

        [Fact]
        public void TextCenterHorizontal()
            => TestPropertiesSet(l => l?.TextCenterHorizontal(), (Label.HorizontalTextAlignmentProperty, TextAlignment.Start, TextAlignment.Center));

        [Fact]
        public void TextEnd()
            => TestPropertiesSet(l => l?.TextEnd(), (Label.HorizontalTextAlignmentProperty, TextAlignment.Start, TextAlignment.End));

        [Fact]
        public void TextTop()
            => TestPropertiesSet(l => l?.TextTop(), (Label.VerticalTextAlignmentProperty, TextAlignment.End, TextAlignment.Start));

        [Fact]
        public void TextCenterVertical()
            => TestPropertiesSet(l => l?.TextCenterVertical(), (Label.VerticalTextAlignmentProperty, TextAlignment.Start, TextAlignment.Center));

        [Fact]
        public void TextBottom()
            => TestPropertiesSet(l => l?.TextBottom(), (Label.VerticalTextAlignmentProperty, TextAlignment.Start, TextAlignment.End));

        [Fact]
        public void TextCenter()
            => TestPropertiesSet(
                    l => l?.TextCenter(),
                    (Label.HorizontalTextAlignmentProperty, TextAlignment.Start, TextAlignment.Center),
                    (Label.VerticalTextAlignmentProperty, TextAlignment.Start, TextAlignment.Center));

        [Fact]
        public void FontSize()
            => TestPropertiesSet(l => l?.FontSize(8.0), (Label.FontSizeProperty, 6.0, 8.0));

        [Fact]
        public void Bold()
            => TestPropertiesSet(l => l?.Bold(), (Label.FontAttributesProperty, FontAttributes.None, FontAttributes.Bold));

        [Fact]
        public void Italic()
            => TestPropertiesSet(l => l?.Italic(), (Label.FontAttributesProperty, FontAttributes.None, FontAttributes.Italic));

        [Fact]
        public void FormattedTextSingleSpan()
        {
            Label.FormattedText = null;
            Label.FormattedText(
                new Span { BackgroundColor = Colors.Blue }
            );

            var spans = Label.FormattedText?.Spans;
            Assert.That(spans?.Count == 1 && spans[0].BackgroundColor == Colors.Blue);
        }

        [Fact]
        public void FormattedTextMultipleSpans()
        {
            Label.FormattedText = null;
            Label.FormattedText(
                new Span { BackgroundColor = Colors.Blue },
                new Span { BackgroundColor = Colors.Green }
            );

            var spans = Label.FormattedText?.Spans;
            Assert.That(spans?.Count == 2 && spans[0].BackgroundColor == Colors.Blue && spans[1].BackgroundColor == Colors.Green);
        }

        [Fact]
        public void SupportDerivedFromLabel()
        {
            Assert.IsInstanceOf<DerivedFromLabel>(
                new DerivedFromLabel()
                .TextStart()
                .TextCenterHorizontal()
                .TextEnd()
                .TextTop()
                .TextCenterVertical()
                .TextBottom()
                .TextCenter()
                .FontSize(8.0)
                .Bold()
                .Italic()
                .FormattedText());
        }

        class DerivedFromLabel : Label { }
    }
}