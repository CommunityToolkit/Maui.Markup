namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension Methods for Bindable Objects
/// </summary>
public static partial class BindableObjectExtensions
{
		/// <summary>
	/// Sets the property values for <paramref name="light"/> and <paramref name="dark"/> themes respectively.
	/// </summary>
	/// <typeparam name="TBindable">The type of the <paramref name="bindable"/> object.</typeparam>
	/// <typeparam name="TValue">The value type to be set for <paramref name="light"/> and <paramref name="dark"/> theme.</typeparam>
	/// <param name="bindable">The bindable object to <c>SetAppTheme</c> on.</param>
	/// <param name="bindableProperty">The property to apply to the <paramref name="light"/> and <paramref name="dark"/> values to.</param>
	/// <param name="light">The value to use when the device is configured to use a light theme.</param>
	/// <param name="dark">The value to use when the device is configured to use a dark theme.</param>
	/// <returns>The <paramref name="bindable"/> instance to allow for fluently building the user interface.</returns>
	public static TBindable AppThemeBinding<TBindable, TValue>(this TBindable bindable, BindableProperty bindableProperty, TValue light, TValue dark) where TBindable : BindableObject
	{
		bindable.SetAppTheme(bindableProperty, light, dark);

		return bindable;
	}

	/// <summary>
	/// Set the app theme color for <paramref name="light"/> and <paramref name="dark"/> themes respectively.
	/// </summary>
	/// <typeparam name="TBindable">The type of the <paramref name="bindable"/> object.</typeparam>
	/// <param name="bindable">The bindable object to <c>SetAppThemeColor</c> on.</param>
	/// <param name="bindableProperty">The property to apply to the <paramref name="light"/> and <paramref name="dark"/> values to.</param>
	/// <param name="light">The <see cref="Color"/> to use when the device is configured to use a light theme.</param>
	/// <param name="dark">The <see cref="Color"/> to use when the device is configured to use a dark theme.</param>
	/// <returns>The <paramref name="bindable"/> instance to allow for fluently building the user interface.</returns>
	public static TBindable AppThemeColorBinding<TBindable>(this TBindable bindable, BindableProperty bindableProperty, Color light, Color dark) where TBindable : BindableObject
	{
		bindable.SetAppThemeColor(bindableProperty, light, dark);

		return bindable;
	}
}