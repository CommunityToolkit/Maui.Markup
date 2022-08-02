

namespace CommunityToolkit.Maui.Markup.Sample.Resources.Themes;
public class LightTheme : BaseTheme
{
	public override Color PageBackgroundColor { get; } = Color.FromArgb("F6F6EF");

	public override Color NavigationBarBackgroundColor { get; } = Color.FromArgb("FFE6D5");
	public override Color NavigationBarTextColor { get; } = Colors.Black;

	public override Color PrimaryTextColor { get; } = Colors.Black;
	public override Color SecondaryTextColor { get; } = Color.FromArgb("828282");

	public override Color BrowserNavigationBarBackgroundColor { get; } = Color.FromArgb("FFE6D5");
	public override Color BrowserNavigationBarTextColor { get; } = Color.FromArgb("3F3F3F");
}
