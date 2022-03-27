﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using CommunityToolkit.Maui.Markup.Sample.Models;
using CommunityToolkit.Maui.Markup.Sample.Services;
using CommunityToolkit.Maui.Markup.Sample.ViewModels.Base;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;

namespace CommunityToolkit.Maui.Markup.Sample.ViewModels;

class NewsViewModel : BaseViewModel
{
	readonly WeakEventManager<string> pullToRefreshEventManager = new();
	readonly HackerNewsAPIService hackerNewsAPIService;
	readonly ISettingsService settingsService;
	bool isListRefreshing;

	public NewsViewModel(HackerNewsAPIService hackerNewsAPIService, ISettingsService settingsService)
	{
		this.hackerNewsAPIService = hackerNewsAPIService;
		this.settingsService = settingsService;
		RefreshCommand = new AsyncCommand(ExecuteRefreshCommand);

		//Ensure Observable Collection is thread-safe https://codetraveler.io/2019/09/11/using-observablecollection-in-a-multi-threaded-xamarin-forms-application/
		BindingBase.EnableCollectionSynchronization(TopStoryCollection, null, ObservableCollectionCallback);
	}

	public event EventHandler<string> PullToRefreshFailed
	{
		add => pullToRefreshEventManager.AddEventHandler(value);
		remove => pullToRefreshEventManager.RemoveEventHandler(value);
	}

	public ObservableCollection<StoryModel> TopStoryCollection { get; } = new();

	public ICommand RefreshCommand { get; }

	public bool IsListRefreshing
	{
		get => isListRefreshing;
		set => SetProperty(ref isListRefreshing, value);
	}

	static void InsertIntoSortedCollection<T>(ObservableCollection<T> collection, Comparison<T> comparison, T modelToInsert)
	{
		if (collection.Count is 0)
		{
			collection.Add(modelToInsert);
		}
		else
		{
			int index = 0;
			foreach (var model in collection)
			{
				if (comparison(model, modelToInsert) >= 0)
				{
					collection.Insert(index, modelToInsert);
					return;
				}

				index++;
			}

			collection.Insert(index, modelToInsert);
		}
	}

	async Task ExecuteRefreshCommand()
	{
		TopStoryCollection.Clear();

		try
		{
			await foreach (var story in GetTopStories(settingsService.NumberOfTopStoriesToFetch).ConfigureAwait(false))
			{
				if (story is not null && !TopStoryCollection.Any(x => x.Title.Equals(story.Title)))
				{
					InsertIntoSortedCollection(TopStoryCollection, (a, b) => b.Score.CompareTo(a.Score), story);
				}
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

	async IAsyncEnumerable<StoryModel> GetTopStories(int? storyCount = int.MaxValue)
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

	//Ensure Observable Collection is thread-safe https://codetraveler.io/2019/09/11/using-observablecollection-in-a-multi-threaded-xamarin-forms-application/
	void ObservableCollectionCallback(IEnumerable collection, object context, Action accessMethod, bool writeAccess)
	{
		MainThread.BeginInvokeOnMainThread(accessMethod);
	}

	void OnPullToRefreshFailed(string message) => pullToRefreshEventManager.RaiseEvent(this, message, nameof(PullToRefreshFailed));
}