using Microsoft.Maui.Controls;

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
	public static implicit operator Style?(Style<T>? style) => style?.FormsStyle;

	/// <summary>
	/// Style(typeof(T))
	/// </summary>
	public Style FormsStyle { get; }

	/// <summary>
	/// Initialize Style
	/// </summary>
	/// <param name="setters"></param>
	public Style(params (BindableProperty Property, object Value)[] setters)
	{
		FormsStyle = new Style(typeof(T));
		Add(setters);
	}

	/// <summary>
	/// Apply to derived types
	/// </summary>
	/// <param name="value"></param>
	/// <returns>Style</returns>
	public Style<T> ApplyToDerivedTypes(bool value)
	{
		FormsStyle.ApplyToDerivedTypes = value;
		return this;
	}

	/// <summary>
	/// Set BasedOn
	/// </summary>
	/// <param name="value"></param>
	/// <returns>Style with added BasedOn</returns>
	public Style<T> BasedOn(Style value)
	{
		FormsStyle.BasedOn = value;
		return this;
	}

	/// <summary>
	/// Add Setters
	/// </summary>
	/// <param name="setters"></param>
	/// <returns>Style with added setters</returns>
	public Style<T> Add(params (BindableProperty Property, object Value)[] setters)
	{
		foreach (var (Property, Value) in setters)
			FormsStyle.Setters.Add(Property, Value);

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
			FormsStyle.Behaviors.Add(behavior);

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
			FormsStyle.Triggers.Add(trigger);

		return this;
	}

	/// <summary>
	/// Set CanCascade
	/// </summary>
	/// <param name="value"></param>
	/// <returns>Style with added CanCascade</returns>
	public Style<T> CanCascade(bool value)
	{
		FormsStyle.CanCascade = value;
		return this;
	}
}