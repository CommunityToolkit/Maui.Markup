using CommunityToolkit.Maui.Alerts;

namespace CommunityToolkit.Maui.Markup.Sample;

class HotReloadHandler : ICommunityToolkitHotReloadHandler
{
	public async void OnHotReload(IReadOnlyList<Type> types)
	{
		await Toast.Make("Hot Reload Executed").Show();
	}
}
