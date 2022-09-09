namespace CommunityToolkit.Maui.Markup.Sample.Services;

public class SettingsService
{
	public const int MinimumStoriesToFetch = 1;
	public const int MaximumStoriesToFetch = 50;

	static readonly WeakEventManager numberOfTopStoriesToFetchChangedEventManager = new();

	readonly IPreferences preferences;

	public SettingsService(IPreferences preferences) => this.preferences = preferences;

	public static event EventHandler<int> NumberOfTopStoriesToFetchChanged
	{
		add => numberOfTopStoriesToFetchChangedEventManager.AddEventHandler(value);
		remove => numberOfTopStoriesToFetchChangedEventManager.RemoveEventHandler(value);
	}

	public int NumberOfTopStoriesToFetch
	{
		get => preferences.Get(nameof(NumberOfTopStoriesToFetch), 25, nameof(CommunityToolkit.Maui.Markup.Sample));
		set
		{
			var clampedValue = Math.Clamp(value, MinimumStoriesToFetch, MaximumStoriesToFetch);

			if (NumberOfTopStoriesToFetch != clampedValue)
			{
				preferences.Set(nameof(NumberOfTopStoriesToFetch), clampedValue, nameof(CommunityToolkit.Maui.Markup.Sample));
				numberOfTopStoriesToFetchChangedEventManager.HandleEvent(this, value, nameof(NumberOfTopStoriesToFetchChanged));
			}
		}
	}
}