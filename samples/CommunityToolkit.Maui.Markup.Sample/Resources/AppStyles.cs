namespace CommunityToolkit.Maui.Markup.Sample.Resources;

public class AppStyles : ResourceDictionary
{
	static readonly Color browserNavigationBarTextColorDark = Colors.White,
							browserNavigationBarTextColorLight = Color.FromArgb("3F3F3F"),
							browserNavigationBarBackgroundColorDark = Color.FromArgb("BA7E56"),
							browserNavigationBarBackgroundColorLight = Color.FromArgb("FFE6D5"),
							darkOrange = Color.FromArgb("C3560E"),
							lightOrange = Color.FromArgb("FF6601"),
							pageBackgroundColorLight = Color.FromArgb("#F6F6EF"),
							pageBackgroundColorDark = Color.FromArgb("#1B1B1B"),
							whiteColor = Color.FromArgb("FFFFFF");

	public AppStyles()
	{
		// all the colors and styles are being accessed directly except the below two
		Add(NavigationPageStyle);
		Add(ShellStyle);
	}

	public static Color BlackColor { get; } = Color.FromArgb("#000000");

	public static Color PrimaryTextColorDark { get; } = Color.FromArgb("FFF2EA");

	public static Color SecondaryTextColorDark { get; } = Color.FromArgb("E9DDD5");

	public static Color SecondaryTextColorLight { get; } = Color.FromArgb("828282");


	public static Color PreferredControlColor { get; } = App.Current?.RequestedTheme is AppTheme.Dark
															? browserNavigationBarTextColorDark
															: browserNavigationBarTextColorLight;

	public static Color PreferredToolbarColor { get; } = App.Current?.RequestedTheme is AppTheme.Dark
															? browserNavigationBarBackgroundColorDark
															: browserNavigationBarBackgroundColorLight;

	public static Style ButtonStyle { get; } = new Style<Button>()
														.AddAppThemeBinding(Button.TextColorProperty, BlackColor, PrimaryTextColorDark)
														.AddAppThemeBinding(Button.BackgroundColorProperty, lightOrange, darkOrange);

	public static Style LabelStyle { get; } = new Style<Label>()
														.AddAppThemeBinding(Label.TextColorProperty, BlackColor, PrimaryTextColorDark)
														.AddAppThemeBinding(Label.BackgroundColorProperty, lightOrange, darkOrange);

	public static Style NavigationPageStyle { get; } = new Style<NavigationPage>()
														.AddAppThemeBinding(NavigationPage.BarTextColorProperty, BlackColor, whiteColor)
														.AddAppThemeBinding(NavigationPage.BackgroundColorProperty, pageBackgroundColorLight, pageBackgroundColorDark)
														.AddAppThemeBinding(NavigationPage.BarBackgroundColorProperty, lightOrange, darkOrange)
														.ApplyToDerivedTypes(true);

	public static Style ShellStyle { get; } = new Style<Shell>()
												.AddAppThemeBinding(Shell.NavBarHasShadowProperty, true, true)
												.AddAppThemeBinding(Shell.TitleColorProperty, BlackColor, whiteColor)
												.AddAppThemeBinding(Shell.DisabledColorProperty, BlackColor, whiteColor)
												.AddAppThemeBinding(Shell.UnselectedColorProperty, BlackColor, whiteColor)
												.AddAppThemeBinding(Shell.ForegroundColorProperty, BlackColor, whiteColor)
												.AddAppThemeBinding(Shell.BackgroundColorProperty, lightOrange, darkOrange).ApplyToDerivedTypes(true);

	public static Style ValidEntryNumericValidationBehaviorStyle { get; } = new Style<Entry>()
																				.AddAppThemeBinding(Entry.TextColorProperty, BlackColor, pageBackgroundColorDark);

	public static Style InvalidEntryNumericValidationBehaviorStyle { get; } = new Style<Entry>()
																				.AddAppThemeBinding(Entry.TextColorProperty, Colors.Red, Colors.DarkRed);
}