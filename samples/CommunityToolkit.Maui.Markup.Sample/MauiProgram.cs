using CommunityToolkit.Maui.Markup.Sample.Pages;
using CommunityToolkit.Maui.Markup.Sample.Services;
using CommunityToolkit.Maui.Markup.Sample.ViewModels;
using Refit;

namespace CommunityToolkit.Maui.Markup.Sample;

public class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder()
								.UseMauiApp<App>()
								.UseMauiCommunityToolkit()
								.UseMauiCommunityToolkitMarkup();

		// Fonts
		builder.ConfigureFonts(fonts => fonts.AddFont("FontAwesome.otf", "FontAwesome"));

		// Maui.Essentials
		builder.Services.AddSingleton(Browser.Default);
		builder.Services.AddSingleton(Preferences.Default);

		// Services
		builder.Services.AddSingleton<App>();
		builder.Services.AddSingleton<SettingsService>();
		builder.Services.AddSingleton<HackerNewsAPIService>();
		builder.Services.AddSingleton(RestService.For<IHackerNewsApi>("https://hacker-news.firebaseio.com/v0"));

		// View Models
		builder.Services.AddTransient<NewsViewModel>();
		builder.Services.AddTransient<SettingsViewModel>();
		builder.Services.AddTransient<NewsDetailViewModel>();

		// Pages
		builder.Services.AddTransient<NewsPage>();
		builder.Services.AddTransient<SettingsPage>();
		builder.Services.AddTransient<NewsDetailPage>();

		return builder.Build();
	}
}