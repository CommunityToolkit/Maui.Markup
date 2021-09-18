using Foundation;
using Microsoft.Maui;

namespace CommunityToolkit.Maui.Markup.Sample
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => Startup.Create();
    }
}