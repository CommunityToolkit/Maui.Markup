using CommunityToolkit.Maui.Alerts;

namespace CommunityToolkit.Maui.Markup.Sample;

class HotReloadHandler : ICommunityToolkitHotReloadHandler
{
	public async void OnHotReload(Type[]? types)
	{
		await Toast.Make("Hot Reload Executed").Show();
	}
}
