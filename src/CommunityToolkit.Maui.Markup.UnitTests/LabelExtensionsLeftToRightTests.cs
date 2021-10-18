﻿using NUnit.Framework;
using CommunityToolkit.Maui.Markup.LeftToRight;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
public class LabelExtensionsLeftToRightTests : MarkupBaseTestFixture<Label>
{
    [Test]
    public void TextLeft()
        => TestPropertiesSet(l => l?.TextLeft(), (Label.HorizontalTextAlignmentProperty, TextAlignment.End, TextAlignment.Start));

    [Test]
    public void TextRight()
        => TestPropertiesSet(l => l?.TextRight(), (Label.HorizontalTextAlignmentProperty, TextAlignment.Start, TextAlignment.End));

    [Test]
    public void SupportDerivedFromLabel() => Assert.IsInstanceOf<DerivedFromLabel>(new DerivedFromLabel().TextLeft().TextRight());

    class DerivedFromLabel : Label
    {
    }
}
