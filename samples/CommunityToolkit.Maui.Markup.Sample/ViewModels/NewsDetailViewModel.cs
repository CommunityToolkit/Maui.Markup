using CommunityToolkit.Maui.Markup.Sample.Constants;
using CommunityToolkit.Maui.Markup.Sample.Models;
using CommunityToolkit.Maui.Markup.Sample.ViewModels.Base;
using CommunityToolkit.Mvvm.Input;

namespace CommunityToolkit.Maui.Markup.Sample.ViewModels;

partial class NewsDetailViewModel : BaseViewModel
{
	readonly IBrowser browser;

	public NewsDetailViewModel(StoryModel storyModel, IBrowser browser)
	{
		this.browser = browser;

		Uri = new Uri(storyModel.Url);
		Title = storyModel.Title;
		ScoreDescription = storyModel.ToString();
	}

	public Uri Uri { get; }
	public string Title { get; }
	public string ScoreDescription { get; }

	[ICommand]
	Task OpenBrowser()
	{
		var browserOptions = new BrowserLaunchOptions
		{
			PreferredControlColor = ColorConstants.BrowserNavigationBarTextColor,
			PreferredToolbarColor = ColorConstants.BrowserNavigationBarBackgroundColor
		};

		return browser.OpenAsync(Uri, browserOptions);
	}
}