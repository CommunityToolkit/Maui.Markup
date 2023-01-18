using Microsoft.UI.Xaml;

namespace CommunityToolkit.Maui.Markup.Sample.Windows;

public partial class App : MauiWinUIApplication
{
	public App() => InitializeComponent();

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}