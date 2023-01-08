namespace CommunityToolkit.Maui.Markup.Sample.Resources;

public class AppStyles : ResourceDictionary
{
	public AppStyles()
	{
		// all the colors and styles are being accessed directly except the below two
		Add(NavigationPageStyle);
		Add(ShellStyle);

	}


	#region Colors

	static readonly Color pageBackgroundColorLight = Color.FromArgb("#F6F6EF");

	static readonly Color pageBackgroundColorDark = Color.FromArgb("#1B1B1B");

	public static readonly Color BlackColor = Color.FromArgb("#000000");

	static readonly Color whiteColor = Color.FromArgb("FFFFFF");

	public static readonly Color PrimaryTextColorDark = Color.FromArgb("FFF2EA");

	public static readonly Color SecondaryTextColorDark = Color.FromArgb("E9DDD5");

	public static readonly Color SecondaryTextColorLight = Color.FromArgb("828282");

	static readonly Color browserNavigationBarTextColorDark = Colors.White;

	static readonly Color browserNavigationBarTextColorLight = Color.FromArgb("3F3F3F");

	static readonly Color browserNavigationBarBackgroundColorDark  = Color.FromArgb("BA7E56");

	static readonly Color browserNavigationBarBackgroundColorLight = Color.FromArgb("FFE6D5");

	static readonly Color darkOrange = Color.FromArgb("C3560E");

    static readonly Color lightOrange = Color.FromArgb("FF6601");

	public static readonly Color PreferredControlColor = App.Current?.RequestedTheme == AppTheme.Dark? browserNavigationBarTextColorDark : browserNavigationBarTextColorLight;

	public static readonly Color PreferredToolbarColor = App.Current?.RequestedTheme == AppTheme.Dark ? browserNavigationBarBackgroundColorDark : browserNavigationBarBackgroundColorLight;


	#endregion


	#region styles                                                         

	public static Style<Button> ButtonStyle { get; } = new Style<Button>()
												.AddAppThemeBinding(Button.TextColorProperty, BlackColor, PrimaryTextColorDark)
												.AddAppThemeBinding(Button.BackgroundColorProperty, lightOrange, darkOrange);
	public static Style<Label> LabelStyle { get; } = new Style<Label>()
												.AddAppThemeBinding(Label.TextColorProperty, BlackColor, PrimaryTextColorDark)
												.AddAppThemeBinding(Label.BackgroundColorProperty, lightOrange, darkOrange);
	public static Style NavigationPageStyle { get; } = new Style<NavigationPage>()
														.AddAppThemeBinding(NavigationPage.BarTextColorProperty, BlackColor,whiteColor)
														.AddAppThemeBinding(NavigationPage.BackgroundColorProperty, pageBackgroundColorLight,pageBackgroundColorDark)
														.AddAppThemeBinding(NavigationPage.BarBackgroundColorProperty, lightOrange, darkOrange)
														.ApplyToDerivedTypes(true);

	public static Style ShellStyle { get; } = new Style<Shell>()
												.AddAppThemeBinding(Shell.NavBarHasShadowProperty, true, true)
												.AddAppThemeBinding(Shell.TitleColorProperty, BlackColor,whiteColor)
												.AddAppThemeBinding(Shell.DisabledColorProperty, BlackColor,whiteColor)
												.AddAppThemeBinding(Shell.UnselectedColorProperty, BlackColor,whiteColor)
												.AddAppThemeBinding(Shell.ForegroundColorProperty, BlackColor,whiteColor)
												.AddAppThemeBinding(Shell.BackgroundColorProperty, lightOrange, darkOrange).ApplyToDerivedTypes(true);

	public static Style ValidEntryNumericValidationBehaviorStyle { get; } = new Style<Entry>()
																				.AddAppThemeBinding(Entry.TextColorProperty, BlackColor, pageBackgroundColorDark);

	public static Style InvalidEntryNumericValidationBehaviorStyle { get; } = new Style<Entry>()
																				.AddAppThemeBinding(Entry.TextColorProperty, Colors.Red, Colors.DarkRed);

	#endregion
}