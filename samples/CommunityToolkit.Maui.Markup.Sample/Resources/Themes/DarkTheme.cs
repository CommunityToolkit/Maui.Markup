

namespace CommunityToolkit.Maui.Markup.Sample.Resources.Themes;
public class DarkTheme : BaseTheme
{
	public override Color PageBackgroundColor => Color.FromArgb("000000");

	public override Color NavigationBarBackgroundColor => Color.FromArgb("ff6600");

	public override Color NavigationBarTextColor => Colors.White;

	public override Color PrimaryTextColor => Colors.White;

	public override Color SecondaryTextColor => Color.FromArgb("f2f2f2");

	public override Color BrowserNavigationBarBackgroundColor => Color.FromArgb("ff6600");

	public override Color BrowserNavigationBarTextColor => Colors.White;

	public override Style NavigationPageStyle => new Style<NavigationPage>(
				(NavigationPage.BarTextColorProperty, Colors.White),
				(NavigationPage.BarBackgroundColorProperty, NavigationBarBackgroundColor)).ApplyToDerivedTypes(true);

	public override Style ShellStyle => new Style<Shell>(
				(Shell.NavBarHasShadowProperty, true),
				(Shell.TitleColorProperty, Colors.White),
				(Shell.DisabledColorProperty, Colors.White),
				(Shell.UnselectedColorProperty, Colors.White),
				(Shell.ForegroundColorProperty, Colors.White),
				(Shell.BackgroundColorProperty, Color.FromArgb("ff6600"))).ApplyToDerivedTypes(true);
}
