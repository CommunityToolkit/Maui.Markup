using CommunityToolkit.Maui.Markup.Sample.Pages;
using CommunityToolkit.Maui.Markup.Sample.Pages.Base;
using CommunityToolkit.Maui.Markup.Sample.ViewModels;
using CommunityToolkit.Maui.Markup.Sample.ViewModels.Base;

namespace CommunityToolkit.Maui.Markup.Sample;

class AppShell : Shell
{
	readonly static IReadOnlyDictionary<Type, string> pageRouteMappingDictionary = new Dictionary<Type, string>(new[]
	{
		CreateRoutePageMapping<NewsPage, NewsViewModel>(),
		CreateRoutePageMapping<SettingsPage, SettingsViewModel>(),
		CreateRoutePageMapping<NewsDetailPage, NewsDetailViewModel>(),
	});

	public AppShell(NewsPage newsPage)
	{
		Items.Add(newsPage);
	}

	public static string GetRoute<TPage, TViewModel>() where TPage : BaseContentPage<TViewModel>
														where TViewModel : BaseViewModel
	{
		if (!pageRouteMappingDictionary.TryGetValue(typeof(TPage), out var route))
		{
			throw new KeyNotFoundException($"No map for ${typeof(TPage)} was found on navigation mappings. Please register your ViewModel in {nameof(AppShell)}.{nameof(pageRouteMappingDictionary)}");
		}

		return route;
	}

	static KeyValuePair<Type, string> CreateRoutePageMapping<TPage, TViewModel>() where TPage : BaseContentPage<TViewModel>
																					where TViewModel : BaseViewModel
	{
		var route = CreateRoute();
		Routing.RegisterRoute(route, typeof(TPage));

		return new KeyValuePair<Type, string>(typeof(TPage), route);

		static string CreateRoute()
		{
			if (typeof(TPage) == typeof(NewsPage))
			{
				return $"//{nameof(NewsPage)}";
			}

			if (typeof(TPage) == typeof(NewsDetailPage))
			{
				return $"//{nameof(NewsPage)}/{nameof(NewsDetailPage)}";
			}

			if (typeof(TPage) == typeof(SettingsPage))
			{
				return $"//{nameof(NewsPage)}/{nameof(SettingsPage)}";
			}

			throw new NotSupportedException($"{typeof(TPage)} Not Implemented in {nameof(pageRouteMappingDictionary)}");
		}
	}
}