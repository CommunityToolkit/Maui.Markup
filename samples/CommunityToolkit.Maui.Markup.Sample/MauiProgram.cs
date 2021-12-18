using CommunityToolkit.Maui.Markup.Sample.Pages;
using CommunityToolkit.Maui.Markup.Sample.Services;
using CommunityToolkit.Maui.Markup.Sample.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Hosting;
using Refit;

namespace CommunityToolkit.Maui.Markup.Sample;

public class MauiProgram
{
	public static MauiApp Create()
	{
		var builder = MauiApp.CreateBuilder();
		builder.UseMauiApp<App>();

		// Services
		builder.Services.AddSingleton<App>();
		builder.Services.AddSingleton(RestService.For<IHackerNewsApi>("https://hacker-news.firebaseio.com/v0"));
		builder.Services.AddSingleton<HackerNewsAPIService>();

		// View Models
		builder.Services.AddTransient<NewsViewModel>();

		// Pages
		builder.Services.AddTransient<NewsPage>();

		return builder.Build();
	}
}