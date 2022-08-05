namespace CommunityToolkit.Maui.Markup.Sample;

class App : Application
{
	public App(AppShell shell)
	{
		Resources = RequestedTheme switch
		{
			AppTheme.Dark => new DarkTheme(),
			_ => new LightTheme()
		};

		RequestedThemeChanged += HandleRequestedThemeChanged;

		MainPage = shell;
	}

	void HandleRequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
	{
		Resources = RequestedTheme switch
		{
			AppTheme.Dark => new DarkTheme(),
			_ => new LightTheme()
		};
	}
}