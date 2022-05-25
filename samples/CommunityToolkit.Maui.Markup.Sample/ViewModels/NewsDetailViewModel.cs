using System.Text.Json;
using CommunityToolkit.Maui.Markup.Sample.Constants;
using CommunityToolkit.Maui.Markup.Sample.Models;
using CommunityToolkit.Maui.Markup.Sample.ViewModels.Base;
using CommunityToolkit.Mvvm.Input;

namespace CommunityToolkit.Maui.Markup.Sample.ViewModels;

partial class NewsDetailViewModel : BaseViewModel, IQueryAttributable
{
	readonly IBrowser browser;

	public NewsDetailViewModel(IBrowser browser)
	{
		this.browser = browser;
	}

	public Uri? Uri { get; private set; }
	public string Title { get; private set; } = string.Empty;
	public string ScoreDescription { get; private set; } = string.Empty;


	public void ApplyQueryAttributes(IDictionary<string, object> query)
	{
		var url = (string)query[nameof(Uri)];
		var title = (string)query[nameof(Title)];
		var scoreDescription = (string)query[nameof(ScoreDescription)];

		Uri = new Uri(url);
		Title = title;
		ScoreDescription = scoreDescription;
	}

	[ICommand]
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
}