using CommunityToolkit.Maui.Markup.Sample.Pages;
using CommunityToolkit.Maui.Markup.Sample.Services;
using CommunityToolkit.Maui.Markup.Sample.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Essentials;
using Microsoft.Maui.Essentials.Implementations;
using Microsoft.Maui.Hosting;
using Refit;

namespace CommunityToolkit.Maui.Markup.Sample;

public class MauiProgram
{
	public static MauiApp Create()
	{
		var builder = MauiApp.CreateBuilder()
								.UseMauiApp<App>()
								.UseMauiCommunityToolkit()
								.UseMauiCommunityToolkitMarkup();

		// Maui.Essentials
		builder.Services.AddSingleton<IBrowser, BrowserImplementation>();
		builder.Services.AddSingleton<IPreferences, PreferencesImplementation>();

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