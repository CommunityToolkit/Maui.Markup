using System.Diagnostics;
using System.Reflection.Metadata;
using CommunityToolkit.Maui.Markup;
[assembly: MetadataUpdateHandler(typeof(CommunityToolkitMetadataUpdateHandler))]
namespace CommunityToolkit.Maui.Markup;

static class CommunityToolkitMetadataUpdateHandler
{
	static readonly WeakEventManager reloadApplicationEventHandler = new();

	public static event EventHandler<IReadOnlyList<Type>> ReloadApplication
	{
		add => reloadApplicationEventHandler.AddEventHandler(value);
		remove => reloadApplicationEventHandler.RemoveEventHandler(value);
	}

	[Conditional("DEBUG")]
	static void UpdateApplication(Type[]? types)
	{
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
		reloadApplicationEventHandler.HandleEvent(null, types?.ToList() ?? Enumerable.Empty<Type>(), nameof(ReloadApplication));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
	}
}