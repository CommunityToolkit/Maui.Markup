using System.Diagnostics.CodeAnalysis;

namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension Methods for Unconstrained Objects
/// </summary>
public static class ObjectExtensions
{
	/// <summary>
	/// Assign <typeparamref name="TAssignable"/> to a variable
	/// </summary>
	/// <typeparam name="TAssignable"></typeparam>
	/// <typeparam name="TVariable"></typeparam>
	/// <param name="assignable"></param>
	/// <param name="variable"></param>
	/// <returns>TBindable</returns>
	[return: NotNull]
	public static T Assign<T>(this T assignable, [NotNull] out T? variable)
	{
		ArgumentNullException.ThrowIfNull(assignable);

		variable = assignable;
		return assignable;
	}

	/// <summary>
	/// Assign <typeparamref name="TAssignable"/> to a variable
	/// </summary>
	/// <typeparam name="TAssignable"></typeparam>
	/// <typeparam name="TVariable"></typeparam>
	/// <param name="assignable"></param>
	/// <param name="variable"></param>
	/// <returns>TBindable</returns>
	[return: NotNull]
	public static TAssignable Assign<TVariable, TAssignable>(this TAssignable assignable, [NotNull] out TVariable? variable)
		where TVariable : TAssignable
		where TAssignable : notnull
	{
		ArgumentNullException.ThrowIfNull(assignable);

		variable = (TVariable)assignable;
		return assignable;
	}

	/// <summary>
	/// Perform an action on <typeparamref name="TAssignable"/>
	/// </summary>
	/// <typeparam name="TAssignable"></typeparam>
	/// <param name="assignable"></param>
	/// <param name="action"></param>
	/// <returns>TBindable</returns>
	public static TAssignable Invoke<TAssignable>(this TAssignable assignable, Action<TAssignable> action)
	{
		ArgumentNullException.ThrowIfNull(assignable);
		ArgumentNullException.ThrowIfNull(action);

		action(assignable);
		return assignable;
	}
}