using CommunityToolkit.Maui.Markup.UnitTests.Mocks;
namespace CommunityToolkit.Maui.Markup.UnitTests;

public static class ApplicationTestHelpers
{
	/// <summary>
	/// Performs a test relating to the <see cref="AppTheme"/> of a running application.
	/// </summary>
	/// <param name="appTheme">The <see cref="AppTheme"/> value to set the application to.</param>
	/// <param name="setAppThemeValue">The action to perform in setting the value for the themes. Basically the thing that will be tested. This should return the <see cref="BindableObject"/> that is being tested.</param>
	/// <param name="assertResult">The action to perform when asserting the result of the <see cref="AppTheme"/> change. Basically the proof of the test.</param>
	public static void PerformAppThemeBasedTest<TBindable>(
		AppTheme appTheme,
		Func<TBindable> setAppThemeValue,
		Action<TBindable> assertResult) where TBindable : View
	{
		try
		{
			var appBuilder = MauiApp.CreateBuilder()
				.UseMauiCommunityToolkit()
				.UseMauiApp<MockApplication>();

			var mauiApp = appBuilder.Build();

			var application = mauiApp.Services.GetRequiredService<IApplication>();

			var bindable = setAppThemeValue();

			ArgumentNullException.ThrowIfNull(Application.Current);

			Application.Current.MainPage = new ContentPage
			{
				Content = bindable
			};

			Application.Current.UserAppTheme = appTheme;

			assertResult.Invoke(bindable);
		}
		finally
		{
			Application.ClearCurrent();
		}
	}
}