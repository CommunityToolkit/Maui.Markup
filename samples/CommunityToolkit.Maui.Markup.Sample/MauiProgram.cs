using CommunityToolkit.Maui.Markup.Sample.Pages;
using CommunityToolkit.Maui.Markup.Sample.Services;
using CommunityToolkit.Maui.Markup.Sample.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Refit;

namespace CommunityToolkit.Maui.Markup.Sample;

public class MauiProgram
{
	public static MauiApp Create()
	{
		var builder = MauiApp.CreateBuilder();
		builder.UseMauiApp<App>().UseMauiCommunityToolkitMarkup();

		// Services
		builder.Services.AddSingleton<App>();
		builder.Services.AddSingleton(RestService.For<IHackerNewsApi>("https://hacker-news.firebaseio.com/v0"));
		builder.Services.AddSingleton<HackerNewsAPIService>();
		builder.Services.AddSingleton<ISettingsService, SettingsService>();

		// View Models
		builder.Services.AddTransient<NewsViewModel>();
		builder.Services.AddTransient<SettingsViewModel>();

		// Pages
		builder.Services.AddTransient<NewsPage>();
		builder.Services.AddTransient<SettingsPage>();

		return builder.Build();
	}
}