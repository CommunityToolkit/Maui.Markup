using Polly;
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

		// App Shell
		builder.Services.AddTransient<AppShell>();

		// Services
		builder.Services.AddSingleton<App>();
		builder.Services.AddSingleton(Browser.Default);
		builder.Services.AddSingleton<SettingsService>();
		builder.Services.AddSingleton(Preferences.Default);
		builder.Services.AddSingleton<HackerNewsAPIService>();
		builder.Services.AddRefitClient<IHackerNewsApi>()
							.ConfigureHttpClient(client => client.BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0"))
							.AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(3, sleepDurationProvider));

		// Pages + View Models
		builder.Services.AddTransient<NewsPage, NewsViewModel>();
		builder.Services.AddTransient<SettingsPage, SettingsViewModel>();
		builder.Services.AddTransient<NewsDetailPage, NewsDetailViewModel>();

		return builder.Build();

		static TimeSpan sleepDurationProvider(int attemptNumber) => TimeSpan.FromSeconds(Math.Pow(2, attemptNumber));
	}
}