using System;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

namespace CommunityToolkit.Maui.Markup.UnitTests;

public static class ApplicationTestHelpers
{
	/// <summary>
	/// Performs a test relating to the <see cref="AppTheme"/> of a running application.
	/// </summary>
	/// <param name="appTheme">The <see cref="AppTheme"/> value to set the application to.</param>
	/// <param name="setAppThemeValue">The action to perform in setting the value for the themes. Basically the thing that will be tested.</param>
	/// <param name="assertResult">The action to perform when asserting the result of the <see cref="AppTheme"/> change. Basically the proof of the test.</param>
	public static void PerformAppThemeBasedTest<TBindable>(
		AppTheme appTheme,
		Func<TBindable> setAppThemeValue,
		Action<TBindable> assertResult)
	{
		try
		{
			new Application();

			var bindable = setAppThemeValue.Invoke();

			var current = Application.Current;

			ArgumentNullException.ThrowIfNull(current);

			current.UserAppTheme = appTheme;

			assertResult.Invoke(bindable);
		}
		finally
		{
			Application.SetCurrentApplication(null!);
		}
	}
}