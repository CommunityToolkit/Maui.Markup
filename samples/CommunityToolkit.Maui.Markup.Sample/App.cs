

namespace CommunityToolkit.Maui.Markup.Sample;

class App : Application
{
	public App(AppShell shell)
	{

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