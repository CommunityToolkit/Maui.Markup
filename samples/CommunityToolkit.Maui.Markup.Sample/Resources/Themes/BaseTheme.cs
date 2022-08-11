namespace CommunityToolkit.Maui.Markup.Sample.Resources.Themes;

public abstract class BaseTheme : ResourceDictionary
{
	protected BaseTheme()
	{
		Add(nameof(PageBackgroundColor), PageBackgroundColor);
		Add(nameof(NavigationBarBackgroundColor), NavigationBarBackgroundColor);
		Add(nameof(NavigationBarTextColor), NavigationBarTextColor);
		Add(nameof(PrimaryTextColor), PrimaryTextColor);
		Add(nameof(SecondaryTextColor), SecondaryTextColor);
		Add(nameof(BrowserNavigationBarBackgroundColor), BrowserNavigationBarBackgroundColor);
		Add(nameof(BrowserNavigationBarTextColor), BrowserNavigationBarTextColor);
		Add(NavigationPageStyle);
		Add(ShellStyle);
	}

	public abstract Color PageBackgroundColor { get; }

	public abstract Color NavigationBarBackgroundColor { get; }
	public abstract Color NavigationBarTextColor { get; }

	public abstract Color PrimaryTextColor { get; }
	public abstract Color SecondaryTextColor { get; }

	public abstract Color BrowserNavigationBarBackgroundColor { get; }
	public abstract Color BrowserNavigationBarTextColor { get; }

	public abstract Style NavigationPageStyle { get; }

	public abstract Style ShellStyle { get; }
}