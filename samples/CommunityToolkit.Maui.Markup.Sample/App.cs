

namespace CommunityToolkit.Maui.Markup.Sample;

class App : Application
{
	public App(AppShell shell)
	{


		Resources = new ResourceDictionary()
		{
			new Style<Shell>(
				(Shell.NavBarHasShadowProperty, true),
				(Shell.TitleColorProperty, ColorConstants.NavigationBarTextColor),
				(Shell.DisabledColorProperty, ColorConstants.NavigationBarTextColor),
				(Shell.UnselectedColorProperty, ColorConstants.NavigationBarTextColor),
				(Shell.ForegroundColorProperty, ColorConstants.NavigationBarTextColor),
				(Shell.BackgroundColorProperty, ColorConstants.NavigationBarBackgroundColor)).ApplyToDerivedTypes(true),

			new Style<NavigationPage>(
				(NavigationPage.BarTextColorProperty, ColorConstants.NavigationBarTextColor),
				(NavigationPage.BarBackgroundColorProperty, ColorConstants.NavigationBarBackgroundColor)).ApplyToDerivedTypes(true)
		};

		if (Current?.RequestedTheme == AppTheme.Dark)
		{
			Current.Resources = new DarkTheme();
		}
		else if (Current?.RequestedTheme == AppTheme.Light)
		{
			Current.Resources = new LightTheme();
		}

		MainPage = shell;
	}
}