#if DEBUG
[assembly: System.Reflection.Metadata.MetadataUpdateHandler(typeof(CommunityToolkit.Maui.Markup.CommunityToolkitMetadataUpdateHandler))]
namespace CommunityToolkit.Maui.Markup;

static class CommunityToolkitMetadataUpdateHandler
{
	static readonly WeakEventManager reloadApplicationEventHandler = new();

	public static event EventHandler ReloadApplication
	{
		add => reloadApplicationEventHandler.AddEventHandler(value);
		remove => reloadApplicationEventHandler.RemoveEventHandler(value);
	}

	static void UpdateApplication(Type[]? types)
	{
		reloadApplicationEventHandler.HandleEvent(new object(), EventArgs.Empty, nameof(ReloadApplication));
	}
}
#endif

/// <summary>
/// Interface for handling C# Hot Reload requests
/// </summary>
public interface ICommunityToolkitHotReloadHander
{
	/// <summary>
	/// Executes when C# Hot Reload is invoked
	/// </summary>
	void OnHotReload();
}