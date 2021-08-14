using System.Collections;
using CommunityToolkit.Maui.Markup.Sample.Constants;
using CommunityToolkit.Maui.Markup.Sample.Models;
using CommunityToolkit.Maui.Markup.Sample.Pages.Base;
using CommunityToolkit.Maui.Markup.Sample.ViewModels;
using CommunityToolkit.Maui.Markup.Sample.Views.News;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;
using Microsoft.Maui.Graphics;

namespace CommunityToolkit.Maui.Markup.Sample.Pages
{
    class NewsPage : BaseContentPage<NewsViewModel>
    {
        public NewsPage(NewsViewModel newsViewModel) : base(newsViewModel, "Top Stories")
        {
            ViewModel.PullToRefreshFailed += HandlePullToRefreshFailed;

            Content = new RefreshView
            {
                RefreshColor = Colors.Black,

                Content = new CollectionView
                {
                    BackgroundColor = Color.FromHex("F6F6EF"),
                    SelectionMode = SelectionMode.Single,
                    ItemTemplate = new StoryDataTemplate(),

                }.Assign(out CollectionView collectionView)
                 .Bind(CollectionView.ItemsSourceProperty, nameof(NewsViewModel.TopStoryCollection))

            }.Bind(RefreshView.IsRefreshingProperty, nameof(NewsViewModel.IsListRefreshing))
             .Bind(RefreshView.CommandProperty, nameof(NewsViewModel.RefreshCommand));

            collectionView.SelectionChanged += HandleSelectionChanged;
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
            var collectionView = (CollectionView)(sender ?? throw new NullReferenceException());
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

                    await Browser.OpenAsync(storyModel.Url, browserOptions);
                }
                else
                {
                    await DisplayAlert("Invalid Article", "ASK HN articles have no url", "OK");
                }
            }
        }

        void HandlePullToRefreshFailed(object? sender, string message) =>
            MainThread.BeginInvokeOnMainThread(async () => await DisplayAlert("Refresh Failed", message, "OK"));
    }
}
