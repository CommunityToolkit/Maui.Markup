#if DEBUG
[assembly: System.Reflection.Metadata.MetadataUpdateHandler(typeof(CommunityToolkit.Maui.Markup.CommunityToolkitMetadataUpdateHandler))]
namespace CommunityToolkit.Maui.Markup;

static class CommunityToolkitMetadataUpdateHandler
{
	static readonly WeakEventManager reloadApplicationEventHandler = new();

	public static event EventHandler<Type[]?> ReloadApplication
	{
		add => reloadApplicationEventHandler.AddEventHandler(value);
		remove => reloadApplicationEventHandler.RemoveEventHandler(value);
	}

	static void UpdateApplication(Type[]? types)
	{
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8604 // Possible null reference argument.
		reloadApplicationEventHandler.HandleEvent(null, types, nameof(ReloadApplication));
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
	}
}
#endif

/// <summary>
/// Interface for handling C# Hot Reload requests
/// </summary>
public interface ICommunityToolkitHotReloadHandler
{
	/// <summary>
	/// Executes when C# Hot Reload is invoked
	/// </summary>
	void OnHotReload(Type[]? types);
}