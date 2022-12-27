
namespace CommunityToolkit.Maui.Markup.Sample.Resources;
public class AppStyles: ResourceDictionary
{
	public AppStyles()
	{
		Add(nameof(ValidEntryNumericValidationBehaviorStyle), ValidEntryNumericValidationBehaviorStyle);
		Add(nameof(InvalidEntryNumericValidationBehaviorStyle), InvalidEntryNumericValidationBehaviorStyle);
		Add(NavigationPageStyle);
		Add(ShellStyle);
	}

	

	// Styles

	public static Style LabelStyle { get; } = new Style<Label>()
		.AddAppThemeBinding(Label.TextColorProperty, Colors.Black, Color.FromArgb("FFF2EA"));
	public static Style NavigationPageStyle { get; } = new Style<NavigationPage>()
				.AddAppThemeBinding(NavigationPage.BarTextColorProperty, Colors.Black, Colors.White)
		        .AddAppThemeBinding(NavigationPage.BarBackgroundColorProperty, Color.FromArgb("FF6601"),Color.FromArgb("C3560E"))
		        .ApplyToDerivedTypes(true);


	public static Style ShellStyle { get; } = new Style<Shell>()
				.AddAppThemeBinding(Shell.NavBarHasShadowProperty, true, true)
				.AddAppThemeBinding(Shell.TitleColorProperty, Colors.Black , Colors.White)
				.AddAppThemeBinding(Shell.DisabledColorProperty, Colors.Black, Colors.White)
				.AddAppThemeBinding(Shell.UnselectedColorProperty, Colors.Black, Colors.White)
				.AddAppThemeBinding(Shell.ForegroundColorProperty, Colors.Black, Colors.White)
				.AddAppThemeBinding(Shell.BackgroundColorProperty, Color.FromArgb("FF6601"), Color.FromArgb("C3560E")).ApplyToDerivedTypes(true);

	public static Style ValidEntryNumericValidationBehaviorStyle { get; } = new Style<Entry>().AddAppThemeBinding(Entry.TextColorProperty, Colors.Black, Color.FromArgb("1B1B1B"));

	public static Style InvalidEntryNumericValidationBehaviorStyle { get; } = new Style<Entry>().AddAppThemeBinding(Entry.TextColorProperty, Colors.Red, Colors.DarkRed);
	
}
