
namespace CommunityToolkit.Maui.Markup.Sample.ViewModels;

partial class NewsDetailViewModel : BaseViewModel, IQueryAttributable
{
	readonly IBrowser browser;

	[ObservableProperty]
	Uri? uri;

	[ObservableProperty]
	string title = string.Empty;

	[ObservableProperty]
	string scoreDescription = string.Empty;

	public NewsDetailViewModel(IBrowser browser)
	{
		this.browser = browser;
	}

	[RelayCommand]
	Task OpenBrowser()
	{
		ArgumentNullException.ThrowIfNull(Uri);

		var browserOptions = new BrowserLaunchOptions
		{
			PreferredControlColor = ColorConstants.BrowserNavigationBarTextColor,
			PreferredToolbarColor = ColorConstants.BrowserNavigationBarBackgroundColor
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