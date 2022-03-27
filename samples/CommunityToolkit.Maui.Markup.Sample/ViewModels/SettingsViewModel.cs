using CommunityToolkit.Maui.Markup.Sample.Services;
using CommunityToolkit.Maui.Markup.Sample.ViewModels.Base;

namespace CommunityToolkit.Maui.Markup.Sample.ViewModels;

class SettingsViewModel : BaseViewModel
{
	readonly SettingsService settingsService;

	public SettingsViewModel(SettingsService settingsService)
	{
		this.settingsService = settingsService;
	}

	public int NumberOfTopStoriesToFetch
	{
		get => settingsService.NumberOfTopStoriesToFetch;
		set => settingsService.NumberOfTopStoriesToFetch = value;
	}
}
