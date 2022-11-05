namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension Methods for Unconstrained Objects
/// </summary>
public static class ObjectExtensions
{
	/// <summary>
	/// Assign TBindable to a variable
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <typeparam name="TVariable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="variable"></param>
	/// <returns>TBindable</returns>
	public static TBindable Assign<TBindable, TVariable>(this TBindable bindable, out TVariable variable)
		where TBindable : BindableObject, TVariable
	{
		variable = bindable;
		return bindable;
	}
	/// <summary>
	/// Perform an action on a Bindable Object
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="action"></param>
	/// <returns>TBindable</returns>
	public static TBindable Invoke<TBindable>(this TBindable bindable, Action<TBindable> action)
	{
		ArgumentNullException.ThrowIfNull(action);

		action.Invoke(bindable);
		return bindable;
	}
}

