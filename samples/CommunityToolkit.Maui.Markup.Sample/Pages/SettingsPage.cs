using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Markup.Sample.Constants;
using CommunityToolkit.Maui.Markup.Sample.Pages.Base;
using CommunityToolkit.Maui.Markup.Sample.ViewModels;
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
				new Label { Text = "Top Stories To Fetch", TextColor = ColorConstants.PrimaryTextColor }
					.LayoutFlags(AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.WidthProportional)
					.LayoutBounds(0.25,0,0.5,40),

				new Entry { Keyboard = Microsoft.Maui.Keyboard.Numeric, BackgroundColor = Colors.White }
					.LayoutFlags(AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.WidthProportional)
					.LayoutBounds(0.75,0,0.5,40)
					.Behaviors(new NumericValidationBehavior
					{
						MinimumValue = 1,
						MaximumValue = 50,
						Flags = ValidationFlags.ValidateOnValueChanged,
						InvalidStyle = new Style<Entry>((Entry.TextColorProperty, Colors.Red)),
						ValidStyle = new Style<Entry>((Entry.TextColorProperty, ColorConstants.PrimaryTextColor)),
					})
					.Bind(Entry.TextProperty, nameof(SettingsViewModel.NumberOfTopStoriesToFetch))
			}
		};
	}
}
