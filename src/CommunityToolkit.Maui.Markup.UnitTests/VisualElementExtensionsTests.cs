using System;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using Microsoft.Maui.Controls;
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
		Assert.That(Bindable.WidthRequest, Is.EqualTo(2));
		Assert.That(Bindable.HeightRequest, Is.EqualTo(3));
	}

	[Test]
	public void SizeUniform()
	{
		Bindable.WidthRequest = Bindable.HeightRequest = 1;
		Bindable.Size(2);
		Assert.That(Bindable.WidthRequest, Is.EqualTo(2));
		Assert.That(Bindable.HeightRequest, Is.EqualTo(2));
	}

	[Test]
	public void MinSizeNotUniform()
	{
		Bindable.MinimumWidthRequest = Bindable.MinimumHeightRequest = 1;
		Bindable.MinSize(2, 3);
		Assert.That(Bindable.MinimumWidthRequest, Is.EqualTo(2));
		Assert.That(Bindable.MinimumHeightRequest, Is.EqualTo(3));
	}

	[Test]
	public void MinSizeUniform()
	{
		Bindable.MinimumWidthRequest = Bindable.MinimumHeightRequest = 1;
		Bindable.MinSize(2);
		Assert.That(Bindable.MinimumWidthRequest, Is.EqualTo(2));
		Assert.That(Bindable.MinimumHeightRequest, Is.EqualTo(2));
	}

	[Test]
	public void Style()
	{
		var style = new Style<BoxView>();
		Bindable.Style = null;
		Bindable.Style(style);
		Assert.That(Bindable.Style, Is.EqualTo(style.FormsStyle));
	}

	[Test]
	public void SupportDerivedFromBoxView()
	{
		Assert.IsInstanceOf<DerivedFromBoxView>(
			new DerivedFromBoxView()
			.Height(2)
			.Width(2)
			.MinHeight(2)
			.MinWidth(2)
			.Size(2, 3)
			.Size(2)
			.MinSize(2, 3)
			.MinSize(2)
			.Style(new Style<DerivedFromBoxView>()));
	}

	class DerivedFromBoxView : BoxView
	{
	}
}