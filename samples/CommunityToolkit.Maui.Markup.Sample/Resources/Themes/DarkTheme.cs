namespace CommunityToolkit.Maui.Markup.Sample.Resources.Themes;

public class DarkTheme : BaseTheme
{
	public override Color PageBackgroundColor { get; } = Color.FromArgb("1B1B1B");

	public override Color NavigationBarBackgroundColor { get; } = Color.FromArgb("C3560E");

	public override Color NavigationBarTextColor { get; } = Color.FromArgb("FFF2EA");

	public override Color PrimaryTextColor { get; } = Color.FromArgb("FFF2EA");

	public override Color SecondaryTextColor { get; } = Color.FromArgb("E9DDD5");

	public override Color BrowserNavigationBarBackgroundColor { get; } = Color.FromArgb("BA7E56");

	public override Color BrowserNavigationBarTextColor { get; } = Colors.White;

	public override Style NavigationPageStyle { get; } = new Style<NavigationPage>(
				(NavigationPage.BarTextColorProperty, Colors.White),
				(NavigationPage.BarBackgroundColorProperty, Color.FromArgb("C3560E"))).ApplyToDerivedTypes(true);

	public override Style ShellStyle { get; } = new Style<Shell>(
				(Shell.NavBarHasShadowProperty, true),
				(Shell.TitleColorProperty, Colors.White),
				(Shell.DisabledColorProperty, Colors.White),
				(Shell.UnselectedColorProperty, Colors.White),
				(Shell.ForegroundColorProperty, Colors.White),
				(Shell.BackgroundColorProperty, Color.FromArgb("C3560E"))).ApplyToDerivedTypes(true);
}