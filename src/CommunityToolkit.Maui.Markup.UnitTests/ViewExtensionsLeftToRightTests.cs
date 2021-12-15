﻿using NUnit.Framework;
using CommunityToolkit.Maui.Markup.LeftToRight;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using Microsoft.Maui.Controls;

namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class ViewExtensionsLeftToRightTests : BaseMarkupTestFixture<BoxView>
{
    [Test]
    public void Left()
        => TestPropertiesSet(v => v?.Left(), (View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.Start));

    [Test]
    public void Right()
        => TestPropertiesSet(v => v?.Right(), (View.HorizontalOptionsProperty, LayoutOptions.Start, LayoutOptions.End));

    [Test]
    public void LeftExpand()
        => TestPropertiesSet(v => v?.LeftExpand(), (View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.StartAndExpand));

    [Test]
    public void RightExpand()
        => TestPropertiesSet(v => v?.RightExpand(), (View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.EndAndExpand));

    [Test]
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
