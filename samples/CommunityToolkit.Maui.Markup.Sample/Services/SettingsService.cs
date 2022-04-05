using System;
using Microsoft.Maui.Essentials;

namespace CommunityToolkit.Maui.Markup.Sample.Services;

public class SettingsService
{
	public const int MinimumStoriesToFetch = 1;
	public const int MaximumStoriesToFetch = 50;

	readonly IPreferences preferences;

	public SettingsService(IPreferences preferences) => this.preferences = preferences;

	public int NumberOfTopStoriesToFetch
	{
		get => preferences.Get(nameof(NumberOfTopStoriesToFetch), 25, nameof(CommunityToolkit.Maui.Markup.Sample));
		set => preferences.Set(nameof(NumberOfTopStoriesToFetch), Math.Clamp(value, MinimumStoriesToFetch, MaximumStoriesToFetch), nameof(CommunityToolkit.Maui.Markup.Sample));
	}
}