namespace CommunityToolkit.Maui.Markup;

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