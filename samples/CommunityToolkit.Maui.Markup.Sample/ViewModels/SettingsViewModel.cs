using CommunityToolkit.Maui.Markup.Sample.Services;
using CommunityToolkit.Maui.Markup.Sample.ViewModels.Base;

namespace CommunityToolkit.Maui.Markup.Sample.ViewModels;

class SettingsViewModel : BaseViewModel
{
	readonly SettingsService settingsService;

	int numberOfTopStoriesToFetch;

	public SettingsViewModel(SettingsService settingsService)
	{
		this.settingsService = settingsService;
		NumberOfTopStoriesToFetch = settingsService.NumberOfTopStoriesToFetch;
	}

	public int NumberOfTopStoriesToFetch
	{
		get => numberOfTopStoriesToFetch;
		set
		{
			if (numberOfTopStoriesToFetch != value)
			{
				settingsService.NumberOfTopStoriesToFetch = numberOfTopStoriesToFetch = value;
				OnPropertyChanged(nameof(NumberOfTopStoriesToFetch));
			}
		}
	}
}