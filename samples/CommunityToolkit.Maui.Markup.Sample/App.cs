using CommunityToolkit.Maui.Markup.Sample.Constants;
using CommunityToolkit.Maui.Markup.Sample.Pages;
using CommunityToolkit.Maui.Markup.Sample.Services;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace CommunityToolkit.Maui.Markup.Sample
{
    class App : Application
    {
        protected override Window CreateWindow(IActivationState activationState)
        {
            var newsPage = ServiceProvider.GetService<NewsPage>();

            var navigationPage = new NavigationPage(newsPage)
            {
                BarBackgroundColor = ColorConstants.NavigationBarBackgroundColor,
                BarTextColor = ColorConstants.NavigationBarTextColor
            };

            return new Window(navigationPage);
        }
    }
}
