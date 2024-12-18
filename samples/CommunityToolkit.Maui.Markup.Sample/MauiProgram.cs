using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Http.Resilience;
using Polly;
using Refit;
namespace CommunityToolkit.Maui.Markup.Sample;

[RequiresUnreferencedCode("SettingsViewModel Calls CommunityToolkit.Maui.Behaviors.NumericValidationBehavior.NumericValidationBehavior()")]
public class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder()
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.UseMauiCommunityToolkitMarkup()
			.ConfigureFonts(fonts => fonts.AddFont("FontAwesome.otf", "FontAwesome"))
			.ConfigureMauiHandlers(handlers =>
			{
#if IOS || MACCATALYST
				handlers.AddHandler<CollectionView, Microsoft.Maui.Controls.Handlers.Items2.CollectionViewHandler2>();
				handlers.AddHandler<CarouselView, Microsoft.Maui.Controls.Handlers.Items2.CarouselViewHandler2>();
#endif
			});

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
			.AddStandardResilienceHandler(options => options.Retry = new MobileHttpRetryStrategyOptions());

		// Pages + View Models
		builder.Services.AddTransient<NewsPage, NewsViewModel>();
		builder.Services.AddTransient<SettingsPage, SettingsViewModel>();
		builder.Services.AddTransient<NewsDetailPage, NewsDetailViewModel>();

		// C# Hot Reload Handler
		builder.Services.AddSingleton<ICommunityToolkitHotReloadHandler, HotReloadHandler>();

		return builder.Build();
	}

	sealed class MobileHttpRetryStrategyOptions : HttpRetryStrategyOptions
	{
		public MobileHttpRetryStrategyOptions()
		{
			BackoffType = DelayBackoffType.Exponential;
			MaxRetryAttempts = 3;
			UseJitter = true;
			Delay = TimeSpan.FromSeconds(2);
		}
	}
}