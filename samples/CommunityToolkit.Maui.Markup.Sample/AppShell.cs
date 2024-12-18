using System.Diagnostics.CodeAnalysis;

namespace CommunityToolkit.Maui.Markup.Sample;

[RequiresUnreferencedCode("SettingsViewModel Calls CommunityToolkit.Maui.Behaviors.NumericValidationBehavior.NumericValidationBehavior()")]
partial class AppShell : Shell
{
    static readonly ReadOnlyDictionary<Type, string> pageRouteMappingDictionary = new Dictionary<Type, string>(
	[
		CreateRoutePageMapping<NewsPage, NewsViewModel>(),
		CreateRoutePageMapping<SettingsPage, SettingsViewModel>(),
		CreateRoutePageMapping<NewsDetailPage, NewsDetailViewModel>()
	]).AsReadOnly();

	public AppShell(NewsPage newsPage)
	{
		Items.Add(newsPage);
	}

	public static string GetRoute<TPage, TViewModel>() where TPage : BaseContentPage<TViewModel>
														where TViewModel : BaseViewModel
	{
		return GetRoute(typeof(TPage));
	}
	
	public static string GetRoute(Type type)
	{
		if (!pageRouteMappingDictionary.TryGetValue(type, out var route))
		{
			throw new KeyNotFoundException($"No map for ${type} was found on navigation mappings. Please register your ViewModel in {nameof(AppShell)}.{nameof(pageRouteMappingDictionary)}");
		}

		return route;
	}

	static KeyValuePair<Type, string> CreateRoutePageMapping<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TPage, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TViewModel>() where TPage : BaseContentPage<TViewModel>
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