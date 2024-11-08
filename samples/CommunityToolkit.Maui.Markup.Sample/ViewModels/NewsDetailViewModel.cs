namespace CommunityToolkit.Maui.Markup.Sample.ViewModels;

sealed partial class NewsDetailViewModel(IBrowser browser) : BaseViewModel, IQueryAttributable
{
	readonly IBrowser browser = browser;

    [ObservableProperty]
    public partial Uri? Uri { get; set; }

    [ObservableProperty]
    public partial string Title { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string ScoreDescription { get; set; } = string.Empty;

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