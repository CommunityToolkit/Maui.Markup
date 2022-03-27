using Microsoft.Maui.Essentials;

namespace CommunityToolkit.Maui.Markup.Sample.Services;

public class SettingsService
{
	readonly IPreferences preferences;

	public SettingsService(IPreferences preferences) => this.preferences = preferences;

	public int NumberOfTopStoriesToFetch
	{
		get => preferences.Get(nameof(NumberOfTopStoriesToFetch), 25, nameof(CommunityToolkit.Maui.Markup.Sample));
		set => preferences.Set(nameof(NumberOfTopStoriesToFetch), value, nameof(CommunityToolkit.Maui.Markup.Sample));
	}
}
