

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

		Current.RequestedThemeChanged += Current_RequestedThemeChanged;

		MainPage = shell;
	}

	private void Current_RequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
	{
		if (Current?.RequestedTheme == AppTheme.Dark)
		{
			Current.Resources = new DarkTheme();
		}
		else if (Current?.RequestedTheme == AppTheme.Light)
		{
			Current.Resources = new LightTheme();
		}
	}
}