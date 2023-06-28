#if DEBUG
[assembly: System.Reflection.Metadata.MetadataUpdateHandler(typeof(CommunityToolkit.Maui.Markup.CommunityToolkitMetadataUpdateHandler))]
namespace CommunityToolkit.Maui.Markup;

static class CommunityToolkitMetadataUpdateHandler
{
	static readonly WeakEventManager reloadApplicationEventHandler = new();

	public static event EventHandler<IReadOnlyList<Type>> ReloadApplication
	{
		add => reloadApplicationEventHandler.AddEventHandler(value);
		remove => reloadApplicationEventHandler.RemoveEventHandler(value);
	}

	static void UpdateApplication(Type[]? types)
	{
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
		reloadApplicationEventHandler.HandleEvent(null, types?.ToList() ?? Enumerable.Empty<Type>(), nameof(ReloadApplication));
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
	/// <param name="types">A <see cref="IReadOnlyList{Type}"/> contianing the types that have been changed in code.</param>
	void OnHotReload(IReadOnlyList<Type> types);
}