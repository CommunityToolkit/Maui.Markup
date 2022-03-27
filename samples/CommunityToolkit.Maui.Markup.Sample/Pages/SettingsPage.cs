using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Markup.Sample.Pages.Base;
using CommunityToolkit.Maui.Markup.Sample.ViewModels;
using Microsoft.Maui.Controls;
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
				new Label { Text = "Number of top stories to fetch"}
					.LayoutFlags(AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.WidthProportional)
					.LayoutBounds(0.25,0,0.5,40),

				new Entry()
					.LayoutFlags(AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.WidthProportional)
					.LayoutBounds(0.75,0,0.5,40)
					.Behaviors(new NumericValidationBehavior
					{
						MinimumValue = 1,
						MaximumValue = 100
					})
					.Bind(
						Entry.TextProperty,
						nameof(SettingsViewModel.NumberOfTopStoriesToFetch))
			}
		};
	}
}
