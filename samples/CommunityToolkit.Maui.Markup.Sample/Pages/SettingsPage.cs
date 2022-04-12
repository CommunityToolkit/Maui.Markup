using System.Globalization;
using CommunityToolkit.Maui.Markup.Sample.Constants;
using CommunityToolkit.Maui.Markup.Sample.Pages.Base;
using CommunityToolkit.Maui.Markup.Sample.Services;
using CommunityToolkit.Maui.Markup.Sample.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
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
				new Image { Opacity = 0.25 }.Source("dotnet_bot").IsOpaque(false).Aspect(Aspect.AspectFill)
					.LayoutFlags(AbsoluteLayoutFlags.SizeProportional | AbsoluteLayoutFlags.PositionProportional)
					.LayoutBounds(0.5, 0.5, 0.5, 0.5),

				new Label()
					.Text("Top Stories To Fetch", ColorConstants.PrimaryTextColor)
					.LayoutFlags(AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.WidthProportional)
					.LayoutBounds(0, 0, 1, 40)
					.TextCenterHorizontal()
					.TextBottom(),

				new Entry { Keyboard = Keyboard.Numeric, BackgroundColor = Colors.White }
					.Placeholder($"Provide a value between {SettingsService.MinimumStoriesToFetch} and {SettingsService.MaximumStoriesToFetch}", Colors.Grey)
					.LayoutFlags(AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.WidthProportional)
					.LayoutBounds(0.5, 45, 0.8, 40)
					/* Temporarily remove this until CommunityTookit.Maui v1.0.0-pre9 is released
					.Behaviors(new NumericValidationBehavior
					{
						Flags = ValidationFlags.ValidateOnValueChanged,
						MinimumValue = SettingsService.MinimumStoriesToFetch,
						MaximumValue = SettingsService.MaximumStoriesToFetch,
						InvalidStyle = new Style<Entry>(Entry.TextColorProperty, Colors.Red),
						ValidStyle = new Style<Entry>(Entry.TextColorProperty, ColorConstants.PrimaryTextColor),
					}) */
					.Bind(Entry.TextProperty, nameof(SettingsViewModel.NumberOfTopStoriesToFetch))
					.TextCenter(),

				new Label()
					.Bind<Label, int, int, string>(
						Label.TextProperty,
						binding1: new Binding { Source = SettingsService.MinimumStoriesToFetch },
						binding2: new Binding { Source = SettingsService.MaximumStoriesToFetch },
						convert: ((int minimum, int maximum) values) => string.Format(CultureInfo.CurrentUICulture, $"The number must be between {values.minimum} and {values.maximum}."),
						mode: BindingMode.OneTime)
					.LayoutFlags(AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.WidthProportional)
					.LayoutBounds(0, 90, 1, 40)
					.TextCenter().TextColor(ColorConstants.PrimaryTextColor).Font(size: 12, italic: true)
			}
		};
	}
}