using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Markup.Sample.Constants;
using CommunityToolkit.Maui.Markup.Sample.Models;
using CommunityToolkit.Maui.Markup.Sample.Pages.Base;
using CommunityToolkit.Maui.Markup.Sample.ViewModels;
using CommunityToolkit.Maui.Markup.Sample.Views.News;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;
using Microsoft.Maui.Graphics;

namespace CommunityToolkit.Maui.Markup.Sample.Pages;

class NewsPage : BaseContentPage<NewsViewModel>
{
	readonly IBrowser browser;
	readonly IDispatcher dispatcher;
	readonly SettingsPage settingsPage;

	public NewsPage(IBrowser browser,
					IDispatcher dispatcher,
					SettingsPage settingsPage,
					NewsViewModel newsViewModel) : base(newsViewModel, "Top Stories")
	{
		this.browser = browser;
		this.dispatcher = dispatcher;
		this.settingsPage = settingsPage;

		BindingContext.PullToRefreshFailed += HandlePullToRefreshFailed;

		ToolbarItems.Add(new ToolbarItem { Command = new AsyncRelayCommand(ShowSettings, true) }.Text("Settings"));

		Content = new RefreshView
		{
			RefreshColor = Colors.Black,

			Content = new CollectionView
			{
				BackgroundColor = Colors.Transparent,
				SelectionMode = SelectionMode.Single,

			}.ItemTemplate(new StoryDataTemplate())
			 .Invoke(collectionView => collectionView.SelectionChanged += HandleSelectionChanged)
			 .Bind(CollectionView.ItemsSourceProperty, nameof(NewsViewModel.TopStoryCollection))

		}.Bind(RefreshView.IsRefreshingProperty, nameof(NewsViewModel.IsListRefreshing))
		 .Bind(RefreshView.CommandProperty, nameof(NewsViewModel.RefreshCommand));
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		if (Content is RefreshView refreshView
			&& refreshView.Content is CollectionView collectionView
			&& IsNullOrEmpty(collectionView.ItemsSource))
		{
			refreshView.IsRefreshing = true;
		}


		static bool IsNullOrEmpty(in IEnumerable? enumerable) => !enumerable?.GetEnumerator().MoveNext() ?? true;
	}

	async void HandleSelectionChanged(object? sender, SelectionChangedEventArgs e)
	{
		ArgumentNullException.ThrowIfNull(sender);

		var collectionView = (CollectionView)sender;
		collectionView.SelectedItem = null;

		if (e.CurrentSelection.FirstOrDefault() is StoryModel storyModel)
		{
			if (!string.IsNullOrEmpty(storyModel.Url))
			{
				var browserOptions = new BrowserLaunchOptions
				{
					PreferredControlColor = ColorConstants.BrowserNavigationBarTextColor,
					PreferredToolbarColor = ColorConstants.BrowserNavigationBarBackgroundColor
				};

				await browser.OpenAsync(storyModel.Url, browserOptions);
			}
			else
			{
				await DisplayAlert("Invalid Article", "ASK HN articles have no url", "OK");
			}
		}
	}

	async void HandlePullToRefreshFailed(object? sender, string message) =>
		await dispatcher.DispatchAsync(() => DisplayAlert("Refresh Failed", message, "OK"));

	Task ShowSettings() => dispatcher.DispatchAsync(() => Navigation.PushAsync(settingsPage));
}