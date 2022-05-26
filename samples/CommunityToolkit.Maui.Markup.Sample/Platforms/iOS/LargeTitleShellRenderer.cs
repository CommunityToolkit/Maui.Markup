using System;
using System.Runtime.Versioning;
using CoreGraphics;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;
using UIKit;

namespace CommunityToolkit.Maui.Markup.Sample
{
	public class LargeTitleShellRenderer : ShellRenderer
	{
		protected override IShellNavBarAppearanceTracker CreateNavBarAppearanceTracker()
		{
			return new Test();
		}

		class Test : ShellNavBarAppearanceTracker
		{
			public new void SetAppearance(UINavigationController controller, ShellAppearance appearance)
			{
				var navBar = controller.NavigationBar;
				navBar.PrefersLargeTitles = true;
				base.SetAppearance(controller, appearance);
			}
		}

		class LargeTitleShellNavBarAppearanceTracker : IShellNavBarAppearanceTracker
		{
			float shadowOpacity = float.MinValue;

			UIColor? defaultBarTint;
			UIColor? defaultTint;
			UIStringAttributes? defaultTitleAttributes;
			CGColor? shadowColor;

			public void UpdateLayout(UINavigationController controller)
			{
			}

			public void ResetAppearance(UINavigationController controller)
			{
				if (defaultTint != null)
				{
					var navBar = controller.NavigationBar;
					navBar.PrefersLargeTitles = true;

					navBar.BarTintColor = defaultBarTint;
					navBar.TintColor = defaultTint;

					if (defaultTitleAttributes is not null)
					{
						navBar.TitleTextAttributes = defaultTitleAttributes;
					}
				}
			}

			public void SetAppearance(UINavigationController controller, ShellAppearance appearance)
			{
				var navBar = controller.NavigationBar;

				if (defaultTint == null)
				{
					defaultBarTint = navBar?.BarTintColor ?? Colors.Transparent.ToPlatform();
					defaultTint = navBar?.TintColor ?? Colors.Transparent.ToPlatform();
					defaultTitleAttributes = navBar?.TitleTextAttributes ?? new();
				}

				if (OperatingSystem.IsIOSVersionAtLeast(15) || OperatingSystem.IsTvOSVersionAtLeast(15))
				{
					UpdateiOS15NavigationBarAppearance(controller, appearance);
				}
				else
				{
					UpdateNavigationBarAppearance(controller, appearance);
				}
			}

			public void Dispose()
			{
				Dispose(true);
			}

			public virtual void SetHasShadow(UINavigationController controller, bool hasShadow)
			{
				var navigationBar = controller.NavigationBar;
				if (shadowOpacity == float.MinValue)
				{
					// Don't do anything if user hasn't changed the shadow to true
					if (!hasShadow)
					{
						return;
					}

					shadowOpacity = navigationBar.Layer.ShadowOpacity;
					shadowColor = navigationBar.Layer.ShadowColor;
				}

				if (hasShadow)
				{
					navigationBar.Layer.ShadowColor = UIColor.Black.CGColor;
					navigationBar.Layer.ShadowOpacity = 1.0f;
				}
				else
				{
					navigationBar.Layer.ShadowColor = shadowColor;
					navigationBar.Layer.ShadowOpacity = shadowOpacity;
				}
			}

			protected virtual void Dispose(bool disposing)
			{
			}

			[SupportedOSPlatform("ios15.0")]
			[SupportedOSPlatform("tvos15.0")]
			void UpdateiOS15NavigationBarAppearance(UINavigationController controller, ShellAppearance appearance)
			{
				var navBar = controller.NavigationBar;
				navBar.PrefersLargeTitles = true;

				var navigationBarAppearance = new UINavigationBarAppearance();
				navigationBarAppearance.ConfigureWithOpaqueBackground();

				navBar.Translucent = false;

				// Set ForegroundColor
				var foreground = appearance.ForegroundColor;

				if (foreground != null)
				{
					navBar.TintColor = foreground.ToPlatform();
				}

				// Set BackgroundColor
				var background = appearance.BackgroundColor;

				if (background != null)
				{
					navigationBarAppearance.BackgroundColor = background.ToPlatform();
				}
				// Set TitleColor
				var titleColor = appearance.TitleColor;

				if (titleColor != null)
				{
					navigationBarAppearance.TitleTextAttributes = new UIStringAttributes() { ForegroundColor = titleColor.ToPlatform() };
				}

				navBar.StandardAppearance = navBar.ScrollEdgeAppearance = navigationBarAppearance;
			}

			void UpdateNavigationBarAppearance(UINavigationController controller, ShellAppearance appearance)
			{
				var background = appearance.BackgroundColor;
				var foreground = appearance.ForegroundColor;
				var titleColor = appearance.TitleColor;

				var navBar = controller.NavigationBar;

				if (background != null)
				{
					navBar.BarTintColor = background.ToPlatform();
				}
				if (foreground != null)
				{
					navBar.TintColor = foreground.ToPlatform();
				}
				if (titleColor != null)
				{
					navBar.TitleTextAttributes = new UIStringAttributes
					{
						ForegroundColor = titleColor.ToPlatform()
					};
				}
			}
		}
	}
}

