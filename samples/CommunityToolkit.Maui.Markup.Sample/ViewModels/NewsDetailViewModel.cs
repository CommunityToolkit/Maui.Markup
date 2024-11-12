﻿namespace CommunityToolkit.Maui.Markup.Sample.ViewModels;

sealed partial class NewsDetailViewModel(IBrowser browser) : BaseViewModel, IQueryAttributable
{
	readonly IBrowser browser = browser;

	[ObservableProperty]
	Uri? uri;

	[ObservableProperty]
	string title = string.Empty;

	[ObservableProperty]
	string scoreDescription = string.Empty;

	[RelayCommand]
	Task OpenBrowser()
	{
		ArgumentNullException.ThrowIfNull(Uri);
		var browserOptions = new BrowserLaunchOptions
		{
			PreferredControlColor = AppStyles.PreferredControlColor,
			PreferredToolbarColor = AppStyles.PreferredToolbarColor,
		};

		return browser.OpenAsync(Uri, browserOptions);
	}

	void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
	{
		var url = (string)query[nameof(Uri)];
		var title = (string)query[nameof(Title)];
		var scoreDescription = (string)query[nameof(ScoreDescription)];

		Uri = new Uri(url);
		Title = title;
		ScoreDescription = scoreDescription;
	}
}