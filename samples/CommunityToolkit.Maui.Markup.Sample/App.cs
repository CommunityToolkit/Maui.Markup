using CommunityToolkit.Maui.Markup.Sample.Constants;
using CommunityToolkit.Maui.Markup.Sample.Pages;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace CommunityToolkit.Maui.Markup.Sample
{
    class App : Application
    {
        readonly NewsPage _newsPage;

        public App(NewsPage newsPage) => _newsPage = newsPage;

        protected override Window CreateWindow(IActivationState activationState)
        {
            var navigationPage = new NavigationPage(_newsPage)
            {
                BarBackgroundColor = ColorConstants.NavigationBarBackgroundColor,
                BarTextColor = ColorConstants.NavigationBarTextColor
            };

            return new Window(navigationPage);
        }
    }
}
