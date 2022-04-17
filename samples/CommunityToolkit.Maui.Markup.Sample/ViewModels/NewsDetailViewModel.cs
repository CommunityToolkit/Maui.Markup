using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Maui.Markup.Sample.Constants;
using CommunityToolkit.Maui.Markup.Sample.Models;
using CommunityToolkit.Maui.Markup.Sample.ViewModels.Base;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

namespace CommunityToolkit.Maui.Markup.Sample.ViewModels;

class NewsDetailViewModel : BaseViewModel
{
	readonly IBrowser browser;

	public NewsDetailViewModel(StoryModel storyModel, IBrowser browser)
	{
		this.browser = browser;

		Uri = new Uri(storyModel.Url);
		Title = storyModel.Title;
		ScoreDescription = storyModel.ToString();

		OpenBrowserCommand = new AsyncRelayCommand(ExecuteOpenBrowserCommand);
	}

	public Uri Uri { get; }
	public string Title { get; }
	public string ScoreDescription { get; }

	public ICommand OpenBrowserCommand { get; }

	Task ExecuteOpenBrowserCommand()
	{
		var browserOptions = new BrowserLaunchOptions
		{
			PreferredControlColor = ColorConstants.BrowserNavigationBarTextColor,
			PreferredToolbarColor = ColorConstants.BrowserNavigationBarBackgroundColor
		};

		return browser.OpenAsync(Uri, browserOptions);
	}
}
