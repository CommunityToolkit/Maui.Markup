using CommunityToolkit.Maui.Markup.Sample.Constants;

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

		MainPage = shell;
	}
}