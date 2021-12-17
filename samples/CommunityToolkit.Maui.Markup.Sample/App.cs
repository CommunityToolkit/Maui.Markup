using CommunityToolkit.Maui.Markup.Sample.Constants;
using CommunityToolkit.Maui.Markup.Sample.Pages;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace CommunityToolkit.Maui.Markup.Sample;

class App : Microsoft.Maui.Controls.Application
{
	public App(NewsPage newsPage)
	{
		var navigationPage = new Microsoft.Maui.Controls.NavigationPage(newsPage)
		{
			BarBackgroundColor = ColorConstants.NavigationBarBackgroundColor,
			BarTextColor = ColorConstants.NavigationBarTextColor
		};
		navigationPage.On<iOS>().SetPrefersLargeTitles(true);

		MainPage = navigationPage;
	}
}