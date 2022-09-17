namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension methods for setting <see cref="SemanticProperties"/> on <see cref="BindableObject"/>s.
/// These methods provide helpful ways of fluently setting properties.
/// </summary>
public static class SemanticPropertiesExtensions
{
	/// <summary>
	/// Sets a short, descriptive string that the platforms screen reader uses to announce the <paramref name="bindable"/>.
	/// </summary>
	/// <typeparam name="TBindable">The type of bindable object being updated.</typeparam>
	/// <param name="bindable">The <see cref="BindableObject"/> to provide the <paramref name="description"/> for.</param>
	/// <param name="description">The short, descriptive string that the platforms screen reader uses to announce the <paramref name="bindable"/>.</param>
	/// <returns>
	/// The supplied <paramref name="bindable"/> with the <see cref="SemanticProperties.DescriptionProperty"/> set
	/// to the supplied <paramref name="description"/>.
	/// </returns>
	public static TBindable SemanticDescription<TBindable>(
		this TBindable bindable,
		string description)
		where TBindable : BindableObject
	{
		SemanticProperties.SetDescription(bindable, description);
		return bindable;
	}

	/// <summary>
	/// Sets a heading level to enable the <paramref name="bindable"/> to be marked as a heading to organize the UI and make it easier to navigate.
	/// </summary>
	/// <typeparam name="TBindable">The type of bindable object being updated.</typeparam>
	/// <param name="bindable">The <see cref="BindableObject"/> to provide the <paramref name="headingLevel"/> for.</param>
	/// <param name="headingLevel">The heading level to enable the <paramref name="bindable"/> to be marked as a heading to organize the UI and make it easier to navigate.</param>
	/// <returns>
	/// The supplied <paramref name="bindable"/> with the <see cref="SemanticProperties.HeadingLevelProperty"/> set
	/// to the supplied <paramref name="headingLevel"/>.
	/// </returns>
	public static TBindable SemanticHeadingLevel<TBindable>(
		this TBindable bindable,
		SemanticHeadingLevel headingLevel)
		where TBindable : BindableObject
	{
		SemanticProperties.SetHeadingLevel(bindable, headingLevel);
		return bindable;
	}

	/// <summary>
	/// Sets an additional context to that set in <see cref="SemanticDescription{TBindable}(TBindable, string)"/>, such as the purpose of the <paramref name="bindable"/>.
	/// </summary>
	/// <typeparam name="TBindable">The type of bindable object being updated.</typeparam>
	/// <param name="bindable">The <see cref="BindableObject"/> to provide the <paramref name="hint"/> for.</param>
	/// <param name="hint">The additional context to that set in <see cref="SemanticDescription{TBindable}(TBindable, string)"/>, such as the purpose of the <paramref name="bindable"/>.</param>
	/// <returns>
	/// The supplied <paramref name="bindable"/> with the <see cref="SemanticProperties.HintProperty"/> set
	/// to the supplied <paramref name="hint"/>.
	/// </returns>
	public static TBindable SemanticHint<TBindable>(
		this TBindable bindable,
		string hint)
		where TBindable : BindableObject
	{
		SemanticProperties.SetHint(bindable, hint);
		return bindable;
	}
}