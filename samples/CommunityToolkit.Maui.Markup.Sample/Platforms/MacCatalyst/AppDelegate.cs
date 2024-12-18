using System.Diagnostics.CodeAnalysis;
using Foundation;
namespace CommunityToolkit.Maui.Markup.Sample;

[Register(nameof(AppDelegate))]
public class AppDelegate : MauiUIApplicationDelegate
{
    [RequiresUnreferencedCode("SettingsViewModel Calls CommunityToolkit.Maui.Behaviors.NumericValidationBehavior.NumericValidationBehavior()")]
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}