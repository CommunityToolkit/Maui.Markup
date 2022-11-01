namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Style Extensions
/// </summary>
/// <typeparam name="T"></typeparam>
public class Style<T> where T : BindableObject
{
	/// <summary>
	/// FormsStyle
	/// </summary>
	/// <param name="style"></param>
	public static implicit operator Style(Style<T> style) => style.MauiStyle;

	/// <summary>
	/// Initialize Style
	/// </summary>
	/// <param name="property">"The <see cref="BindableProperty"/> to style </param>
	/// <param name="value">"The value for the <see cref="BindableProperty"/> </param>
	public Style(BindableProperty property, object value) : this((property, value))
	{

	}

	/// <summary>
	/// Initialize Style
	/// </summary>
	/// <param name="setters"></param>
	public Style(params (BindableProperty Property, object Value)[] setters)
	{
		MauiStyle = new Style(typeof(T));
		Add(setters);
	}

	/// <summary>
	/// Style(typeof(T))
	/// </summary>
	public Style MauiStyle { get; }

	/// <summary>
	/// Apply to derived types
	/// </summary>
	/// <param name="value"></param>
	/// <returns>Style</returns>
	public Style<T> ApplyToDerivedTypes(bool value)
	{
		MauiStyle.ApplyToDerivedTypes = value;
		return this;
	}

	/// <summary>
	/// Set BasedOn
	/// </summary>
	/// <param name="value"></param>
	/// <returns>Style with added BasedOn</returns>
	public Style<T> BasedOn(Style value)
	{
		MauiStyle.BasedOn = value;
		return this;
	}

	/// <summary>
	/// Add Setters
	/// </summary>
	/// <param name="property">"The <see cref="BindableProperty"/> to style </param>
	/// <param name="value">"The value for the <see cref="BindableProperty"/> </param>
	/// <returns>Style with added setters</returns>
	public Style<T> Add(BindableProperty property, object value)
	{
		MauiStyle.Setters.Add(property, value);
		return this;
	}

	/// <summary>
	/// Add Setters
	/// </summary>
	/// <param name="setters"></param>
	/// <returns>Style with added setters</returns>
	public Style<T> Add(params (BindableProperty Property, object Value)[] setters)
	{
		foreach (var (property, value) in setters)
		{
			Add(property, value);
		}

		return this;
	}

	/// <summary>
	/// Adds the supplied <paramref name="light"/> and <paramref name="dark"/> values in an <see cref="AppThemeBinding"/>.
	/// </summary>
	/// <param name="property">"The <see cref="BindableProperty"/> to style </param>
	/// <param name="light">"The light value for the <see cref="BindableProperty"/> </param>
	/// <param name="dark">"The dark value for the <see cref="BindableProperty"/> </param>
	/// <returns>Style with added setters</returns>
	public Style<T> AddAppThemeBinding(BindableProperty property, object light, object dark)
	{
		MauiStyle.Setters.Add(property, new AppThemeBinding { Light = light, Dark = dark });
		return this;
	}

	/// <summary>
	/// Adds the supplied <paramref name="setters"/> in an <see cref="AppThemeBinding"/>.
	/// </summary>
	/// <param name="setters">A set of <see cref="BindableProperty"/>, and value for light and dark theme.</param>
	/// <returns>Style with added setters</returns>
	public Style<T> AddAppThemeBindings(params (BindableProperty Property, object Light, object Dark)[] setters)
	{
		foreach (var (property, light, dark) in setters)
		{
			Add(property, light, dark);
		}

		return this;
	}

	/// <summary>
	/// Add Behaviors
	/// </summary>
	/// <param name="behaviors"></param>
	/// <returns>Style with added behaviors</returns>
	public Style<T> Add(params Behavior[] behaviors)
	{
		foreach (var behavior in behaviors)
		{
			MauiStyle.Behaviors.Add(behavior);
		}

		return this;
	}

	/// <summary>
	/// Add Triggers
	/// </summary>
	/// <param name="triggers"></param>
	/// <returns>Style with added Triggers</returns>
	public Style<T> Add(params TriggerBase[] triggers)
	{
		foreach (var trigger in triggers)
		{
			MauiStyle.Triggers.Add(trigger);
		}

		return this;
	}

	/// <summary>
	/// Set CanCascade
	/// </summary>
	/// <param name="value"></param>
	/// <returns>Style with added CanCascade</returns>
	public Style<T> CanCascade(bool value)
	{
		MauiStyle.CanCascade = value;
		return this;
	}
}