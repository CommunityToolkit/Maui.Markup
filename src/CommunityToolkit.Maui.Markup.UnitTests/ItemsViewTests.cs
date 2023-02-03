using CommunityToolkit.Maui.Markup.UnitTests.Base;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests;

class ItemsViewTests : BaseMarkupTestFixture<CollectionView>
{
	[Test]
	public void EmptyViewTest()
	{
		var view = new BoxView();
		TestPropertiesSet(l => l.EmptyView(view), (ItemsView.EmptyViewProperty, view));
	}

	[Test]
	public void EmptyViewTemplateTest()
	{
		var dataTemplate = new DataTemplate(() => new Label().Text("Hello World"));
		TestPropertiesSet(l => l.EmptyViewTemplate(dataTemplate), (ItemsView.EmptyViewTemplateProperty, dataTemplate));
	}

	[Test]
	public void ItemsSourceTest()
	{
		var itemSource = new[] { "Hello", "World" };
		TestPropertiesSet(l => l.ItemsSource(itemSource), (ItemsView.ItemsSourceProperty, itemSource));
	}

	[Test]
	public void HorizontalScrollBarVisibilityTest()
	{
		TestPropertiesSet(l => l.HorizontalScrollBarVisibility(ScrollBarVisibility.Always), (ItemsView.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Always));
	}

	[Test]
	public void VerticalScrollBarVisibilityTest()
	{
		TestPropertiesSet(l => l.VerticalScrollBarVisibility(ScrollBarVisibility.Always), (ItemsView.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Always));
	}

	[Test]
	public void ScrollBarVisibilityTest()
	{
		TestPropertiesSet(l => l.ScrollBarVisibility(ScrollBarVisibility.Always), (ItemsView.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Always), (ItemsView.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Always));
	}

	[Test]
	public void RemainingItemsThresholdTest()
	{
		TestPropertiesSet(l => l.RemainingItemsThreshold(10), (ItemsView.RemainingItemsThresholdProperty, 10));
	}

	[Test]
	public void RemainingItemsThresholdReachedCommandTest()
	{
		var command = new Command<string>(text => text = text[1..]);
		TestPropertiesSet(l => l.RemainingItemsThresholdReachedCommand(command), (ItemsView.RemainingItemsThresholdReachedCommandProperty, command));
	}

	[Test]
	public void RemainingItemsThresholdReachedCommandPlusParameterTest()
	{
		var command = new Command<string>(text => text = text[1..]);
		TestPropertiesSet(l => l.RemainingItemsThresholdReachedCommand(command, "Hello"), (ItemsView.RemainingItemsThresholdReachedCommandProperty, command),
																							(ItemsView.RemainingItemsThresholdReachedCommandParameterProperty, "Hello"));
	}

	[Test]
	public void RemainingItemsThresholdReachedCommandParameterTest()
	{
		TestPropertiesSet(l => l.RemainingItemsThresholdReachedCommandParameter("Hello"), (ItemsView.RemainingItemsThresholdReachedCommandParameterProperty, "Hello"));
	}

	[Test]
	public void ItemTemplateTest()
	{
		var dataTemplate = new DataTemplate(() => new Label().TextColor(Colors.Green));
		TestPropertiesSet(l => l.ItemTemplate(dataTemplate), (ItemsView.ItemTemplateProperty, dataTemplate));
	}

	[Test]
	public void ItemsUpdatingScrollModeTest()
	{
		TestPropertiesSet(l => l.ItemsUpdatingScrollMode(ItemsUpdatingScrollMode.KeepLastItemInView), (ItemsView.ItemsUpdatingScrollModeProperty, ItemsUpdatingScrollMode.KeepLastItemInView));
	}

	[Test]
	public void DerivedFromCarouselViewTest()
	{
		Assert.IsInstanceOf<DerivedFromCarouselView>(
			new DerivedFromCarouselView()
			.EmptyView(new BoxView { BackgroundColor = Colors.Green })
			.EmptyViewTemplate(new DataTemplate(() => new BoxView { BackgroundColor = Colors.Red }))
			.ItemsSource(new[] { "Hello", "World" })
			.ScrollBarVisibility(ScrollBarVisibility.Never)
			.RemainingItemsThreshold(10)
			.RemainingItemsThresholdReachedCommand(new Command<string>(text => text = text[..1]), "Hello World")
			.ItemTemplate(new DataTemplate(() => new Label()))
			.ItemsUpdatingScrollMode(ItemsUpdatingScrollMode.KeepScrollOffset));
	}

	class DerivedFromCarouselView : CarouselView
	{

	}
}