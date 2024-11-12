namespace CommunityToolkit.Maui.Markup.Sample.ViewModels;

sealed partial class NewsViewModel(
	IDispatcher dispatcher,
	SettingsService settingsService,
	HackerNewsAPIService hackerNewsApiService) : BaseViewModel, IDisposable
{
	readonly IDispatcher dispatcher = dispatcher;
	readonly SettingsService settingsService = settingsService;
	readonly HackerNewsAPIService hackerNewsAPIService = hackerNewsApiService;
	readonly WeakEventManager pullToRefreshEventManager = new();
	readonly SemaphoreSlim insertIntoSortedCollectionSemaphore = new(1, 1);

	[ObservableProperty]
	public partial bool IsListRefreshing { get; set; }

	public event EventHandler<string> PullToRefreshFailed
	{
		add => pullToRefreshEventManager.AddEventHandler(value);
		remove => pullToRefreshEventManager.RemoveEventHandler(value);
	}

	public ObservableCollection<StoryModel> TopStoryCollection { get; } = [];

	public void Dispose()
	{
		insertIntoSortedCollectionSemaphore.Dispose();
	}

	[RelayCommand]
	async Task PullToRefresh()
	{
		TopStoryCollection.Clear();

		try
		{
			await foreach (var story in GetTopStories(settingsService.NumberOfTopStoriesToFetch).ConfigureAwait(false))
			{
				await InsertIntoSortedCollection((a, b) => b.Score.CompareTo(a.Score), story).ConfigureAwait(false);
			}
		}
		catch (Exception e)
		{
			OnPullToRefreshFailed(e.ToString());
		}
		finally
		{
			IsListRefreshing = false;
		}
	}

	async IAsyncEnumerable<StoryModel> GetTopStories(int storyCount)
	{
		var topStoryIds = await hackerNewsAPIService.GetTopStoryIDs().ConfigureAwait(false);
		var getTopStoryTaskList = topStoryIds.Select(hackerNewsAPIService.GetStory).ToList();

		while (getTopStoryTaskList.Any() && storyCount-- > 0)
		{
			var completedGetStoryTask = await Task.WhenAny(getTopStoryTaskList).ConfigureAwait(false);
			getTopStoryTaskList.Remove(completedGetStoryTask);

			var story = await completedGetStoryTask.ConfigureAwait(false);
			yield return story;
		}
	}

	async Task InsertIntoSortedCollection(Comparison<StoryModel> comparison, StoryModel modelToInsert)
	{
		await insertIntoSortedCollectionSemaphore.WaitAsync().ConfigureAwait(false);

		try
		{
			if (TopStoryCollection.Count is 0)
			{
				await dispatcher.DispatchAsync(() => TopStoryCollection.Add(modelToInsert)).ConfigureAwait(false);
			}
			else if (!TopStoryCollection.Any(x => x.Title == modelToInsert.Title))
			{
				int index = 0;
				foreach (var model in TopStoryCollection)
				{
					if (comparison(model, modelToInsert) >= 0)
					{
						await dispatcher.DispatchAsync(() => TopStoryCollection.Insert(index, modelToInsert)).ConfigureAwait(false);
						return;
					}

					index++;
				}

				await dispatcher.DispatchAsync(() => TopStoryCollection.Insert(index, modelToInsert)).ConfigureAwait(false);
			}
		}
		finally
		{
			insertIntoSortedCollectionSemaphore.Release();
		}
	}

	void OnPullToRefreshFailed(string message) => pullToRefreshEventManager.HandleEvent(this, message, nameof(PullToRefreshFailed));
}