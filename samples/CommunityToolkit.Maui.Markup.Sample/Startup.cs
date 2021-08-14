using CommunityToolkit.Maui.Markup.Sample.Pages;
using CommunityToolkit.Maui.Markup.Sample.Services;
using CommunityToolkit.Maui.Markup.Sample.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Hosting;
using Refit;

[assembly: XamlCompilationAttribute(XamlCompilationOptions.Compile)]

namespace CommunityToolkit.Maui.Markup.Sample
{
    public class Startup : IStartup
    {
        public void Configure(IAppHostBuilder appBuilder)
        {
            appBuilder
                .UseMauiApp<App>()
                .ConfigureServices(services =>
                {
                    // Services
                    services.AddSingleton(RestService.For<IHackerNewsApi>("https://hacker-news.firebaseio.com/v0"));
                    services.AddSingleton<HackerNewsAPIService>();

                    // View Models
                    services.AddTransient<NewsViewModel>();

                    // Pages
                    services.AddTransient<NewsPage>();
                });
        }
    }
}