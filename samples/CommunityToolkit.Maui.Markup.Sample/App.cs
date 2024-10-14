namespace CommunityToolkit.Maui.Markup.Sample;

class App : Application
{
	readonly AppShell appShell;

	public App(AppShell shell)
	{
		Resources = new AppStyles();
		appShell = shell;
	}

	protected override Window CreateWindow(IActivationState? activationState) => new(appShell);
}