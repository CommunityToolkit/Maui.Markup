namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension methods that apply to all implementations inheriting from a <see cref="VisualElement"/>.
/// These methods provide helpful ways of fluently setting properties.
/// </summary>
public static class VisualElementExtensions
{
	/// <summary>
	/// Sets the <see cref="VisualElement"/>s AutomationId property for the supplied <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of visual element being updated.</typeparam>
	/// <param name="element">This element to apply the <paramref name="automationId"/> to.</param>
	/// <param name="automationId">The value that the automation framework can use to find and interact with.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the AutomationId property set
	/// to the supplied <paramref name="automationId"/>.
	/// </returns>
	public static TElement AutomationId<TElement>(
		this TElement element,
		string automationId)
		where TElement : VisualElement
	{
		element.AutomationId = automationId;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.HeightRequest"/> property to the supplied <paramref name="heightRequest"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="heightRequest"/> to.</param>
	/// <param name="heightRequest">The height to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.HeightRequest"/> property set to the supplied
	/// <paramref name="heightRequest"/>.
	/// </returns>
	public static TElement Height<TElement>(this TElement element, double heightRequest) where TElement : VisualElement
	{
		element.HeightRequest = heightRequest;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.WidthRequest"/> property to the supplied <paramref name="widthRequest"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="widthRequest"/> to.</param>
	/// <param name="widthRequest">The width to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.WidthRequest"/> property set to the supplied
	/// <paramref name="widthRequest"/>.
	/// </returns>
	public static TElement Width<TElement>(this TElement element, double widthRequest) where TElement : VisualElement
	{
		element.WidthRequest = widthRequest;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.MinimumHeightRequest"/> property to the supplied <paramref name="minimumHeightRequest"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="minimumHeightRequest"/> to.</param>
	/// <param name="minimumHeightRequest">The minimum height to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.MinimumHeightRequest"/> property set to the supplied
	/// <paramref name="minimumHeightRequest"/>.
	/// </returns>
	public static TElement MinHeight<TElement>(this TElement element, double minimumHeightRequest) where TElement : VisualElement
	{
		element.MinimumHeightRequest = minimumHeightRequest;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.MinimumWidthRequest"/> property to the supplied <paramref name="minimumWidthRequest"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="minimumWidthRequest"/> to.</param>
	/// <param name="minimumWidthRequest">The minimum width to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.MinimumWidthRequest"/> property set to the supplied
	/// <paramref name="minimumWidthRequest"/>.
	/// </returns>
	public static TElement MinWidth<TElement>(this TElement element, double minimumWidthRequest) where TElement : VisualElement
	{
		element.MinimumWidthRequest = minimumWidthRequest;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.WidthRequest"/> property to the supplied <paramref name="widthRequest"/>
	/// and the <see cref="VisualElement.HeightRequest"/> property to the supplied <paramref name="heightRequest"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="widthRequest"/> and <paramref name="heightRequest"/> to.</param>
	/// <param name="widthRequest">The width to apply to this <paramref name="element"/>.</param>
	/// <param name="heightRequest">The height to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.WidthRequest"/> property set to the supplied
	/// <paramref name="widthRequest"/> and the <see cref="VisualElement.HeightRequest"/> property set to the supplied
	/// <paramref name="heightRequest"/>.
	/// </returns>
	public static TElement Size<TElement>(this TElement element, double widthRequest, double heightRequest) where TElement : VisualElement
		=> element.Width(widthRequest).Height(heightRequest);

	/// <summary>
	/// Sets both the <see cref="VisualElement.WidthRequest"/> and <see cref="VisualElement.HeightRequest"/> to
	/// the same supplied <paramref name="sizeRequest"/> on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="sizeRequest"/> to.</param>
	/// <param name="sizeRequest">The size to apply to both the <see cref="VisualElement.WidthRequest"/> and
	/// <see cref="VisualElement.HeightRequest"/> properties.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with both the <see cref="VisualElement.WidthRequest"/> and
	/// <see cref="VisualElement.HeightRequest"/> set to the same supplied <paramref name="sizeRequest"/>.
	/// </returns>
	public static TElement Size<TElement>(this TElement element, double sizeRequest) where TElement : VisualElement
		=> element.Width(sizeRequest).Height(sizeRequest);

	/// <summary>
	/// Sets the <see cref="VisualElement.MinimumWidthRequest"/> property to the supplied <paramref name="minimumWidthRequest"/>
	/// and the <see cref="VisualElement.MinimumHeightRequest"/> property to the supplied <paramref name="minimumHeightRequest"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="minimumWidthRequest"/> and <paramref name="minimumHeightRequest"/> to.</param>
	/// <param name="minimumWidthRequest">The minimum width to apply to this <paramref name="element"/>.</param>
	/// <param name="minimumHeightRequest">The minimum height to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.MinimumWidthRequest"/> property set to the supplied
	/// <paramref name="minimumWidthRequest"/> and the <see cref="VisualElement.MinimumHeightRequest"/> property set to the supplied
	/// <paramref name="minimumHeightRequest"/>.
	/// </returns>
	public static TElement MinSize<TElement>(this TElement element, double minimumWidthRequest, double minimumHeightRequest) where TElement : VisualElement
		=> element.MinWidth(minimumWidthRequest).MinHeight(minimumHeightRequest);

	/// <summary>
	/// Sets both the <see cref="VisualElement.MinimumWidthRequest"/> and <see cref="VisualElement.MinimumHeightRequest"/> to
	/// the same supplied <paramref name="minimumSizeRequest"/> on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="minimumSizeRequest"/> to.</param>
	/// <param name="minimumSizeRequest">The minimum size to apply to both the <see cref="VisualElement.MinimumWidthRequest"/> and
	/// <see cref="VisualElement.MinimumHeightRequest"/> properties.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with both the <see cref="VisualElement.MinimumWidthRequest"/> and
	/// <see cref="VisualElement.MinimumHeightRequest"/> set to the same supplied <paramref name="minimumSizeRequest"/>.
	/// </returns>
	public static TElement MinSize<TElement>(this TElement element, double minimumSizeRequest) where TElement : VisualElement
		=> element.MinWidth(minimumSizeRequest).MinHeight(minimumSizeRequest);

	/// <summary>
	/// Sets the supplied <paramref name="style"/> on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="style"/> to.</param>
	/// <param name="style">The <see cref="Style"/> to apply.</param>
	/// <returns>The supplied <paramref name="element"/> with the supplied <paramref name="style"/> applied.</returns>
	public static TElement Style<TElement>(this TElement element, Style<TElement> style) where TElement : VisualElement
	{
		element.Style = style;
		return element;
	}

	/// <summary>
	/// Adds the supplied <paramref name="behaviors"/> to the Behaviors collection on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to add the <paramref name="behaviors"/> to.</param>
	/// <param name="behaviors">The <see cref="Behavior"/>s to add.</param>
	/// <returns>The supplied <paramref name="element"/> with the supplied <paramref name="behaviors"/> added.</returns>
	public static TElement Behaviors<TElement>(this TElement element, params Behavior[] behaviors) where TElement : VisualElement
	{
		foreach (var behavior in behaviors)
		{
			element.Behaviors.Add(behavior);
		}
		return element;
	}
}