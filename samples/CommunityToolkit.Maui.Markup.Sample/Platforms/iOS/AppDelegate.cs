using System.Diagnostics.CodeAnalysis;
using Foundation;
namespace CommunityToolkit.Maui.Markup.Sample;

[RequiresUnreferencedCode("SettingsViewModel Calls CommunityToolkit.Maui.Behaviors.NumericValidationBehavior.NumericValidationBehavior()")]
[Register(nameof(AppDelegate))]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}