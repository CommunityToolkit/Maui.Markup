namespace CommunityToolkit.Maui.Markup.Sample.ViewModels;

sealed partial class SettingsViewModel(SettingsService settingsService) : BaseViewModel
{
	readonly SettingsService settingsService = settingsService;

	[ObservableProperty]
	public partial int NumberOfTopStoriesToFetch { get; set; } = settingsService.NumberOfTopStoriesToFetch;

	partial void OnNumberOfTopStoriesToFetchChanged(int value)
	{
		settingsService.NumberOfTopStoriesToFetch = value;
	}
}