using System.Collections;
using System.Windows.Input;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Fluent extension methods for <see cref="ItemsView"/>
/// </summary>
public static class ItemsViewExtensions
{
	/// <summary>
	/// Assigns the <see cref="ItemsView.EmptyView"/> property
	/// </summary>
	/// <typeparam name="TItemsView"></typeparam>
	/// <param name="itemsView"></param>
	/// <param name="view"></param>
	/// <returns>ItemsView with Empty View</returns>
	public static TItemsView EmptyView<TItemsView>(this TItemsView itemsView, object view) where TItemsView : ItemsView
	{
		itemsView.EmptyView = view;
		return itemsView;
	}

	/// <summary>
	/// Assigns the <see cref="ItemsView.EmptyViewTemplate"/> property
	/// </summary>
	/// <typeparam name="TItemsView"></typeparam>
	/// <param name="itemsView"></param>
	/// <param name="view"></param>
	/// <returns>ItemsView with Empty View Template</returns>
	public static TItemsView EmptyViewTemplate<TItemsView>(this TItemsView itemsView, DataTemplate view) where TItemsView : ItemsView
	{
		itemsView.EmptyViewTemplate = view;
		return itemsView;
	}

	/// <summary>
	/// Assigns the <see cref="ItemsView.ItemsSource"/> property
	/// </summary>
	/// <typeparam name="TItemsView"></typeparam>
	/// <param name="itemsView"></param>
	/// <param name="itemsSource"></param>
	/// <returns>ItemsView with ItemSource</returns>
	public static TItemsView ItemsSource<TItemsView>(this TItemsView itemsView, IEnumerable itemsSource) where TItemsView : ItemsView
	{
		itemsView.ItemsSource = itemsSource;
		return itemsView;
	}

	/// <summary>
	/// Assigns the <see cref="ItemsView.HorizontalScrollBarVisibility"/> property
	/// </summary>
	/// <typeparam name="TItemsView"></typeparam>
	/// <param name="itemsView"></param>
	/// <param name="visibility"></param>
	/// <returns>ItemsView with updated Horiztonal Scroll Bar Visibility</returns>
	public static TItemsView HorizontalScrollBarVisibility<TItemsView>(this TItemsView itemsView, ScrollBarVisibility visibility) where TItemsView : ItemsView
	{
		itemsView.HorizontalScrollBarVisibility = visibility;
		return itemsView;
	}

	/// <summary>
	/// Assigns the <see cref="ItemsView.VerticalScrollBarVisibility"/> property
	/// </summary>
	/// <typeparam name="TItemsView"></typeparam>
	/// <param name="itemsView"></param>
	/// <param name="visibility"></param>
	/// <returns>ItemsView with updated Vertical Scroll Bar Visibility</returns>
	public static TItemsView VerticalScrollBarVisibility<TItemsView>(this TItemsView itemsView, ScrollBarVisibility visibility) where TItemsView : ItemsView
	{
		itemsView.VerticalScrollBarVisibility = visibility;
		return itemsView;
	}

	/// <summary>
	/// Assigns the <see cref="ItemsView.VerticalScrollBarVisibility"/> and <see cref="ItemsView.HorizontalScrollBarVisibility"/> properties
	/// </summary>
	/// <typeparam name="TItemsView"></typeparam>
	/// <param name="itemsView"></param>
	/// <param name="visibility"></param>
	/// <returns>ItemsView with updated Horiztonal + Vertical Scroll Bar Visibility</returns>
	public static TItemsView ScrollBarVisibility<TItemsView>(this TItemsView itemsView, ScrollBarVisibility visibility) where TItemsView : ItemsView
	{
		return itemsView.HorizontalScrollBarVisibility(visibility).VerticalScrollBarVisibility(visibility);
	}

	/// <summary>
	/// Assigns the <see cref="ItemsView.RemainingItemsThreshold"/> property
	/// </summary>
	/// <typeparam name="TItemsView"></typeparam>
	/// <param name="itemsView"></param>
	/// <param name="threshold"></param>
	/// <returns>ItemsView with updated Remaining Items Threshold</returns>
	public static TItemsView RemainingItemsThreshold<TItemsView>(this TItemsView itemsView, int threshold) where TItemsView : ItemsView
	{
		itemsView.RemainingItemsThreshold = threshold;
		return itemsView;
	}

	/// <summary>
	/// Assigns the <see cref="ItemsView.RemainingItemsThresholdReachedCommand"/>  ans <see cref="ItemsView.RemainingItemsThresholdReachedCommandParameter"/>properties
	/// </summary>
	/// <typeparam name="TItemsView"></typeparam>
	/// <param name="itemsView"></param>
	/// <param name="command"></param>
	/// <param name="parameter"></param>
	/// <returns>ItemsView with updated Remaining Items Threshold Reached Command + CommandParameter</returns>
	public static TItemsView RemainingItemsThresholdReachedCommand<TItemsView>(this TItemsView itemsView, ICommand command, object? parameter) where TItemsView : ItemsView
	{
		return itemsView.RemainingItemsThresholdReachedCommand(command).RemainingItemsThresholdReachedCommandParameter(parameter);
	}

	/// <summary>
	/// Assigns the <see cref="ItemsView.RemainingItemsThresholdReachedCommand"/> property
	/// </summary>
	/// <typeparam name="TItemsView"></typeparam>
	/// <param name="itemsView"></param>
	/// <param name="command"></param>
	/// <returns>ItemsView with updated Remaining Items Threshold Reached Command</returns>
	public static TItemsView RemainingItemsThresholdReachedCommand<TItemsView>(this TItemsView itemsView, ICommand command) where TItemsView : ItemsView
	{
		itemsView.RemainingItemsThresholdReachedCommand = command;
		return itemsView;
	}

	/// <summary>
	/// Assigns the <see cref="ItemsView.RemainingItemsThresholdReachedCommandParameter"/> property
	/// </summary>
	/// <typeparam name="TItemsView"></typeparam>
	/// <param name="itemsView"></param>
	/// <param name="parameter"></param>
	/// <returns>ItemsView with updated Remaining Items Threshold Reached Command Parameter</returns>
	public static TItemsView RemainingItemsThresholdReachedCommandParameter<TItemsView>(this TItemsView itemsView, object? parameter) where TItemsView : ItemsView
	{
		itemsView.RemainingItemsThresholdReachedCommandParameter = parameter;
		return itemsView;
	}

	/// <summary>
	/// Assigns the <see cref="ItemsView.ItemTemplate"/> property
	/// </summary>
	/// <typeparam name="TItemsView"></typeparam>
	/// <param name="itemsView"></param>
	/// <param name="template"></param>
	/// <returns>ItemsView with updated Item Template</returns>
	public static TItemsView ItemTemplate<TItemsView>(this TItemsView itemsView, DataTemplate template) where TItemsView : ItemsView
	{
		itemsView.ItemTemplate = template;
		return itemsView;
	}

	/// <summary>
	/// Assigns the <see cref="ItemsView.ItemsUpdatingScrollMode"/> property
	/// </summary>
	/// <typeparam name="TItemsView"></typeparam>
	/// <param name="itemsView"></param>
	/// <param name="mode"></param>
	/// <returns>ItemsView with updated ItemsUpdatingScrollMode</returns>
	public static TItemsView ItemsUpdatingScrollMode<TItemsView>(this TItemsView itemsView, ItemsUpdatingScrollMode mode) where TItemsView : ItemsView
	{
		itemsView.ItemsUpdatingScrollMode = mode;
		return itemsView;
	}
}