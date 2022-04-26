using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.UI.Xaml;
using Windows.ApplicationModel;

namespace CommunityToolkit.Maui.Markup.Sample.Windows;

public partial class App : MauiWinUIApplication
{
	public App() => this.InitializeComponent();

	protected override MauiApp CreateMauiApp() => MauiProgram.Create();
}