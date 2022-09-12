namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension methods for setting <see cref="AutomationProperties"/> on <see cref="BindableObject"/>s.
/// These methods provide helpful ways of fluently setting properties.
/// </summary>
public static class AutomationPropertiesExtensions
{
	/// <summary>
	/// Sets a value determining if the <paramref name="bindable"/> and its children should be excluded from the accessibility tree.
	/// </summary>
	/// <typeparam name="TBindable">The type of bindable object being updated.</typeparam>
	/// <param name="bindable">The <see cref="BindableObject"/> to determine whether it should be excluded from the accessibility tree including its children.</param>
	/// <param name="isExcludedWithChildren">The value determining if the <paramref name="bindable"/> and its children should be excluded from the accessibility tree.</param>
	/// <returns>
	/// The supplied <paramref name="bindable"/> with the <see cref="AutomationProperties.ExcludedWithChildrenProperty"/> set
	/// to the supplied <paramref name="isExcludedWithChildren"/>.
	/// </returns>
	public static TBindable AutomationExcludedWithChildren<TBindable>(
		this TBindable bindable,
		bool? isExcludedWithChildren)
		where TBindable : BindableObject
	{
		AutomationProperties.SetExcludedWithChildren(bindable, isExcludedWithChildren);
		return bindable;
	}

	/// <summary>
	/// Sets a value determining if the <paramref name="bindable"/> is visible to screen readers.
	/// </summary>
	/// <typeparam name="TBindable">The type of bindable object being updated.</typeparam>
	/// <param name="bindable">The <see cref="BindableObject"/> to determine whether it should be visible to screen readers.</param>
	/// <param name="isInAccessibleTree">The value determining if the <paramref name="bindable"/> is visible to screen readers..</param>
	/// <returns>
	/// The supplied <paramref name="bindable"/> with the <see cref="AutomationProperties.IsInAccessibleTreeProperty"/> set
	/// to the supplied <paramref name="isInAccessibleTree"/>.
	/// </returns>
	public static TBindable AutomationIsInAccessibleTree<TBindable>(
		this TBindable bindable,
		bool? isInAccessibleTree)
		where TBindable : BindableObject
	{
		AutomationProperties.SetIsInAccessibleTree(bindable, isInAccessibleTree);
		return bindable;
	}
}