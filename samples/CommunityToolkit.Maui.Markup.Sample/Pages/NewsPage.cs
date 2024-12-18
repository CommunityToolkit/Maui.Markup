using System.Diagnostics.CodeAnalysis;

namespace CommunityToolkit.Maui.Markup.Sample.Pages;

sealed partial class NewsPage : BaseContentPage<NewsViewModel>
{
	readonly IDispatcher dispatcher;
	readonly RefreshView refreshView;

	[RequiresUnreferencedCode("AppShell.GetRoute Requires Unreferenced Code")]
	public NewsPage(IDispatcher dispatcher,
					NewsViewModel newsViewModel) : base(newsViewModel, "Top Stories")
	{
		this.dispatcher = dispatcher;

		BindingContext.PullToRefreshFailed += HandlePullToRefreshFailed;
		SettingsService.NumberOfTopStoriesToFetchChanged += HandleNumberOfTopStoriesToFetchChanged;

		ToolbarItems.Add(new ToolbarItem { Command = new AsyncRelayCommand(NavigateToSettingsPage) }.Text("Settings"));

		Content = new RefreshView
		{
			Content = new CollectionView
			{
				SelectionMode = SelectionMode.Single,

			}.BackgroundColor(Colors.Transparent)
			 .ItemTemplate(new StoryDataTemplate())
			 .Invoke(collectionView => collectionView.SelectionChanged += HandleSelectionChanged)
			 .Bind(CollectionView.ItemsSourceProperty, static (NewsViewModel vm) => vm.TopStoryCollection)
			 .AutomationId("NewsCollectionView")

		}.Bind(RefreshView.IsRefreshingProperty, static (NewsViewModel vm) => vm.IsListRefreshing, static (vm, isRefreshing) => vm.IsListRefreshing = isRefreshing)
		 .BindCommand(static (NewsViewModel vm) => vm.PullToRefreshCommand, mode: BindingMode.OneTime)
		 .AppThemeColorBinding(RefreshView.RefreshColorProperty, Colors.Black, Colors.LightGray)
		 .Assign(out refreshView);
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		if (refreshView.Content is CollectionView collectionView
			&& IsNullOrEmpty(collectionView.ItemsSource))
		{
			TryRefreshCollectionView();
		}

		static bool IsNullOrEmpty(in IEnumerable? enumerable) => !enumerable?.GetEnumerator().MoveNext() ?? true;
	}

	[RequiresUnreferencedCode("AppShell.GetRoute Requires Unreferenced Code")]
	async void HandleSelectionChanged(object? sender, SelectionChangedEventArgs e)
	{
		ArgumentNullException.ThrowIfNull(sender);

		var collectionView = (CollectionView)sender;
		collectionView.SelectedItem = null;

		if (e.CurrentSelection.FirstOrDefault() is StoryModel storyModel)
		{
			if (!string.IsNullOrEmpty(storyModel.Url))
			{
				await NavigateToNewsDetailPage(storyModel);
			}
			else
			{
				await DisplayAlert("Invalid Article", "ASK HN articles have no url", "OK");
			}
		}
	}

	async void HandlePullToRefreshFailed(object? sender, string message) =>
		await dispatcher.DispatchAsync(() => DisplayAlert("Refresh Failed", message, "OK"));

	bool TryRefreshCollectionView()
	{
		if (!refreshView.IsRefreshing)
		{
			refreshView.IsRefreshing = true;
			return true;
		}

		return false;
	}

	void HandleNumberOfTopStoriesToFetchChanged(object? sender, int e) => TryRefreshCollectionView();

	[RequiresUnreferencedCode("AppShell.GetRoute Requires Unreferenced Code")]
	Task NavigateToSettingsPage() => dispatcher.DispatchAsync(() =>
	{
		var route = AppShell.GetRoute<SettingsPage, SettingsViewModel>();
		return Shell.Current.GoToAsync(route);
	});

	[RequiresUnreferencedCode("AppShell.GetRoute Requires Unreferenced Code")]
	Task NavigateToNewsDetailPage(StoryModel storyModel) => dispatcher.DispatchAsync(() =>
	{
		var route = AppShell.GetRoute<NewsDetailPage, NewsDetailViewModel>();

		// Shell passes these parameters to NewsDetailViewModel.ApplyQueryAttributes
		var parameters = new Dictionary<string, object>
		{
			{ nameof(NewsDetailViewModel.Uri), storyModel.Url },
			{ nameof(NewsDetailViewModel.Title), storyModel.Title },
			{ nameof(NewsDetailViewModel.ScoreDescription), storyModel.Description}
		};

		return Shell.Current.GoToAsync(route, parameters);
	});
}