using CommunityToolkit.Maui.Markup.Sample.Resources;

namespace CommunityToolkit.Maui.Markup.Sample;

class App : Application
{
	public App(AppShell shell)
	{
		Resources = new AppStyles();

		MainPage = shell;
	}


}