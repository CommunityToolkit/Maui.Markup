using Microsoft.Maui.Layouts;

namespace CommunityToolkit.Maui.Markup.Sample.Pages;

sealed class SettingsPage : BaseContentPage<SettingsViewModel>
{
	public SettingsPage(SettingsViewModel settingsViewModel) : base(settingsViewModel, "Settings")
	{
		Content = new AbsoluteLayout
		{
			Children =
			{
				new Image().Source("dotnet_bot.png").Opacity(0.25).IsOpaque(false).Aspect(Aspect.AspectFit)
					.LayoutFlags(AbsoluteLayoutFlags.SizeProportional | AbsoluteLayoutFlags.PositionProportional)
					.LayoutBounds(0.5, 0.5, 0.5, 0.5)
					.AutomationIsInAccessibleTree(false),

				new Label()
					.Text("Top Stories To Fetch")
					.AppThemeBinding(Label.TextColorProperty,AppStyles.BlackColor, AppStyles.PrimaryTextColorDark)
					.LayoutFlags(AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.WidthProportional)
					.LayoutBounds(0, 0, 1, 40)
					.TextCenterHorizontal()
					.TextBottom()
					.Assign(out Label topNewsStoriesToFetchLabel),

				new Entry { Keyboard = Keyboard.Numeric }
					.BackgroundColor(Colors.White)
					.Placeholder($"Provide a value between {SettingsService.MinimumStoriesToFetch} and {SettingsService.MaximumStoriesToFetch}", Colors.Grey)
					.LayoutFlags(AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.WidthProportional)
					.LayoutBounds(0.5, 45, 0.8, 40)
					.Behaviors(new NumericValidationBehavior
					{
						Flags = ValidationFlags.ValidateOnValueChanged,
						MinimumValue = SettingsService.MinimumStoriesToFetch,
						MaximumValue = SettingsService.MaximumStoriesToFetch,
						ValidStyle = AppStyles.ValidEntryNumericValidationBehaviorStyle,
						InvalidStyle = AppStyles.InvalidEntryNumericValidationBehaviorStyle,
					})
					.TextCenter()
					.SemanticDescription(topNewsStoriesToFetchLabel.Text)
					.Bind(Entry.TextProperty, static (SettingsViewModel vm) => vm.NumberOfTopStoriesToFetch, static (SettingsViewModel vm, int text) => vm.NumberOfTopStoriesToFetch = text),

				new Label()
					.Bind(
						Label.TextProperty,
						binding1: new Binding { Source = SettingsService.MinimumStoriesToFetch },
						binding2: new Binding { Source = SettingsService.MaximumStoriesToFetch },
						convert: ((int minimum, int maximum) values) => string.Format(CultureInfo.CurrentUICulture, $"The number must be between {values.minimum} and {values.maximum}."),
						mode: BindingMode.OneTime)
					.LayoutFlags(AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.WidthProportional)
					.LayoutBounds(0, 90, 1, 40)
					.TextCenter()
					.AppThemeColorBinding(Label.TextColorProperty,AppStyles.BlackColor, AppStyles.PrimaryTextColorDark)
					.Font(size: 12, italic: true)
					.SemanticHint($"The minimum and maximum possible values for the {topNewsStoriesToFetchLabel.Text} field above.")
			}
		};
	}
}