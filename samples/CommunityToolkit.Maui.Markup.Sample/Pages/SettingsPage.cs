using Microsoft.Maui.Layouts;

namespace CommunityToolkit.Maui.Markup.Sample.Pages;

class SettingsPage : BaseContentPage<SettingsViewModel>
{
	public SettingsPage(SettingsViewModel settingsViewModel) : base(settingsViewModel, "Settings")
	{
		Content = new AbsoluteLayout
		{
			Children =
			{
				new Image { Opacity = 0.25 }.Source("dotnet_bot").IsOpaque(false).Aspect(Aspect.AspectFit)
					.LayoutFlags(AbsoluteLayoutFlags.SizeProportional | AbsoluteLayoutFlags.PositionProportional)
					.LayoutBounds(0.5, 0.5, 0.5, 0.5)
					.AutomationIsInAccessibleTree(false),

				new Label()
					.Text("Top Stories To Fetch")
					.DynamicResource(Label.TextColorProperty, nameof(BaseTheme.PrimaryTextColor))
					.LayoutFlags(AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.WidthProportional)
					.LayoutBounds(0, 0, 1, 40)
					.TextCenterHorizontal()
					.TextBottom()
					.Assign(out Label topNewsStoriesToFetchLabel),

				new Entry { Keyboard = Keyboard.Numeric, BackgroundColor = Colors.White }
					.Placeholder($"Provide a value between {SettingsService.MinimumStoriesToFetch} and {SettingsService.MaximumStoriesToFetch}", Colors.Grey)
					.LayoutFlags(AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.WidthProportional)
					.LayoutBounds(0.5, 45, 0.8, 40)
					.Behaviors(new NumericValidationBehavior
					{
						Flags = ValidationFlags.ValidateOnValueChanged,
						MinimumValue = SettingsService.MinimumStoriesToFetch,
						MaximumValue = SettingsService.MaximumStoriesToFetch,
						ValidStyle = (Style?)Application.Current?.Resources[nameof(BaseTheme.ValidEntryNumericValidationBehaviorStyle)],
						InvalidStyle = (Style?)Application.Current?.Resources[nameof(BaseTheme.InvalidEntryNumericValidationBehaviorStyle)],
					})
					.Bind(Entry.TextProperty, nameof(SettingsViewModel.NumberOfTopStoriesToFetch))
					.TextCenter()
					.SemanticDescription(topNewsStoriesToFetchLabel.Text),

				new Label()
					.Bind<Label, int, int, string>(
						Label.TextProperty,
						binding1: new Binding { Source = SettingsService.MinimumStoriesToFetch },
						binding2: new Binding { Source = SettingsService.MaximumStoriesToFetch },
						convert: ((int minimum, int maximum) values) => string.Format(CultureInfo.CurrentUICulture, $"The number must be between {values.minimum} and {values.maximum}."),
						mode: BindingMode.OneTime)
					.LayoutFlags(AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.WidthProportional)
					.LayoutBounds(0, 90, 1, 40)
					.TextCenter().DynamicResource(Label.TextColorProperty, nameof(BaseTheme.PrimaryTextColor)).Font(size: 12, italic: true)
					.SemanticHint($"The minimum and maximum possible values for the {topNewsStoriesToFetchLabel.Text} field above.")
			}
		};
	}
}