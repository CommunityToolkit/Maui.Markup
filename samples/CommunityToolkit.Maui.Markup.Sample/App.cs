namespace CommunityToolkit.Maui.Markup.Sample;

class App : Application
{
	public App(AppShell shell)
	{
		SetAppTheme(RequestedTheme);

		RequestedThemeChanged += HandleRequestedThemeChanged;

		MainPage = shell;
	}

	protected override void OnResume()
	{
		base.OnResume();
		SetAppTheme(RequestedTheme);
	}

	void HandleRequestedThemeChanged(object? sender, AppThemeChangedEventArgs e) =>
		SetAppTheme(e.RequestedTheme);

	void SetAppTheme(in AppTheme appTheme) => Resources = appTheme switch
	{
		AppTheme.Dark => new DarkTheme(),
		_ => new LightTheme()
	};
}