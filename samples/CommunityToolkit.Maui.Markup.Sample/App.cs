using CommunityToolkit.Maui.Markup.Sample.Constants;
using CommunityToolkit.Maui.Markup.Sample.Pages;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace CommunityToolkit.Maui.Markup.Sample;

class App : Application
{
    public App(NewsPage newsPage) => MainPage = new NavigationPage(newsPage)
    {
        BarBackgroundColor = ColorConstants.NavigationBarBackgroundColor,
        BarTextColor = ColorConstants.NavigationBarTextColor
    };
}
