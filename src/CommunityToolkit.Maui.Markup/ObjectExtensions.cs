namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension Methods for Unconstrained Objects
/// </summary>
public static class ObjectExtensions
{
	/// <summary>
	/// Assign <typeparam name="TAssignable"/> to a variable
	/// </summary>
	/// <typeparam name="TAssignable"></typeparam>
	/// <typeparam name="TVariable"></typeparam>
	/// <param name="assignable"></param>
	/// <param name="variable"></param>
	/// <returns>TBindable</returns>
	public static TAssignable Assign<TAssignable, TVariable>(this TAssignable assignable, out TVariable variable)
		where TAssignable : TVariable
	{
		variable = assignable;
		return assignable;
	}

	/// <summary>
	/// Perform an action on <typeparam name="TAssignable"/>
	/// </summary>
	/// <typeparam name="TAssignable"></typeparam>
	/// <param name="assignable"></param>
	/// <param name="action"></param>
	/// <returns>TBindable</returns>
	public static TAssignable Invoke<TAssignable>(this TAssignable assignable, Action<TAssignable> action)
	{
		ArgumentNullException.ThrowIfNull(action);

		action(assignable);
		return assignable;
	}
}

