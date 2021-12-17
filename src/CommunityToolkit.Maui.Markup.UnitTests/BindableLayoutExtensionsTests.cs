using System;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using Microsoft.Maui.Controls;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class BindableLayoutExtensionsTests : BaseMarkupTestFixture<StackLayout>
{
	[Test]
	public void EmptyView()
	{
		var view = new BoxView();
		TestPropertiesSet(l => l?.EmptyView(view), (BindableLayout.EmptyViewProperty, view));
	}

	[Test]
	public void EmptyViewTemplate()
	{
		var template = new DataTemplate(() => new BoxView());
		TestPropertiesSet(l => l?.EmptyViewTemplate(template), (BindableLayout.EmptyViewTemplateProperty, template));
	}

	[Test]
	public void EmptyViewTemplateFunction()
	{
		Func<object> loadTemplate = () => new BoxView();
		Bindable.EmptyViewTemplate(loadTemplate);

		Assert.That(BindableLayout.GetEmptyViewTemplate(Bindable), Is.Not.Null);
	}

	[Test]
	public void ItemsSource()
	{
		var source = Array.Empty<string>();
		TestPropertiesSet(l => l?.ItemsSource(source), (BindableLayout.ItemsSourceProperty, source));
	}

	[Test]
	public void ItemTemplate()
	{
		var template = new DataTemplate(() => new BoxView());
		TestPropertiesSet(l => l?.ItemTemplate(template), (BindableLayout.ItemTemplateProperty, template));
	}

	[Test]
	public void ItemTemplateFunction()
	{
		Func<object> loadTemplate = () => new BoxView();
		Bindable.ItemTemplate(loadTemplate);

		Assert.That(BindableLayout.GetItemTemplate(Bindable), Is.Not.Null);
	}

	[Test]
	public void ItemTemplateSelector()
	{
		var selector = new Selector();
		TestPropertiesSet(l => l?.ItemTemplateSelector(selector), (BindableLayout.ItemTemplateSelectorProperty, selector));
	}

	class Selector : DataTemplateSelector
	{
		protected override DataTemplate OnSelectTemplate(object item, BindableObject container) => new(() => new BoxView());
	}

	[Test]
	public void SupportDerivedFromView()
	{
		_ = new DerivedFromView()
			.EmptyView(new BoxView())
			.EmptyViewTemplate(new DataTemplate(() => new BoxView()))
			.ItemsSource(Array.Empty<string>())
			.ItemTemplate(new DataTemplate(() => new BoxView()))
			.ItemTemplateSelector(new Selector());
	}

	class DerivedFromView : StackLayout
	{
	}
}