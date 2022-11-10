using Microsoft.Maui.Controls.Shapes;

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

	/// <summary>
	/// Sets the <see cref="VisualElement.AnchorX"/> property to the supplied <paramref name="anchorX"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="anchorX"/> to.</param>
	/// <param name="anchorX">The X component of the center point for any transform, relative to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.AnchorX"/> property set to the supplied
	/// <paramref name="anchorX"/>.
	/// </returns>
	public static TElement AnchorX<TElement>(this TElement element, double anchorX) where TElement : VisualElement
	{
		element.AnchorX = anchorX;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.AnchorY"/> property to the supplied <paramref name="anchorY"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="anchorY"/> to.</param>
	/// <param name="anchorY">The Y component of the center point for any transform, relative to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.AnchorY"/> property set to the supplied
	/// <paramref name="anchorY"/>.
	/// </returns>
	public static TElement AnchorY<TElement>(this TElement element, double anchorY) where TElement : VisualElement
	{
		element.AnchorY = anchorY;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.AnchorX"/> property to the supplied <paramref name="anchorX"/>
	/// and the <see cref="VisualElement.AnchorY"/> property to the supplied <paramref name="anchorY"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="anchorX"/> and <paramref name="anchorY"/> to.</param>
	/// <param name="anchorX">The X component of the center point for any transform, to apply to this <paramref name="element"/>.</param>
	/// <param name="anchorY">The Y component of the center point for any transform, to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.AnchorX"/> property set to the supplied
	/// <paramref name="anchorX"/> and the <see cref="VisualElement.AnchorY"/> property set to the supplied
	/// <paramref name="anchorY"/>.
	/// </returns>
	public static TElement Anchor<TElement>(this TElement element, double anchorX, double anchorY) where TElement : VisualElement
		=> element.AnchorX(anchorX).AnchorY(anchorY);

	/// <summary>
	/// Sets the <see cref="VisualElement.Background"/> property to the supplied <paramref name="backgroundBrush"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="backgroundBrush"/> to.</param>
	/// <param name="backgroundBrush">The Brush to apply the the Background of this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.Background"/> property set to the supplied
	/// <paramref name="backgroundBrush"/>.
	/// </returns>
	public static TElement Background<TElement>(this TElement element, Brush backgroundBrush) where TElement : VisualElement
	{
		element.Background = backgroundBrush;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.BackgroundColor"/> property to the supplied <paramref name="color"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="color"/> to.</param>
	/// <param name="color">The BackgroundColor to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.BackgroundColor"/> property set to the supplied
	/// <paramref name="color"/>.
	/// </returns>
	public static TElement BackgroundColor<TElement>(this TElement element, Color color) where TElement : VisualElement
	{
		element.BackgroundColor = color;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.Clip"/> property to the supplied <paramref name="clip"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="clip"/> to.</param>
	/// <param name="clip">The Clip to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.Clip"/> property set to the supplied
	/// <paramref name="clip"/>.
	/// </returns>
	public static TElement Clip<TElement>(this TElement element, Geometry clip) where TElement : VisualElement
	{
		element.Clip = clip;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.FlowDirection"/> property to the supplied <paramref name="flowDirection"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="flowDirection"/> to.</param>
	/// <param name="flowDirection">The FlowDirection to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.FlowDirection"/> property set to the supplied
	/// <paramref name="flowDirection"/>.
	/// </returns>
	public static TElement FlowDirection<TElement>(this TElement element, FlowDirection flowDirection) where TElement : VisualElement
	{
		element.FlowDirection = flowDirection;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.InputTransparent"/> property to the supplied <paramref name="inputTransparent"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="inputTransparent"/> to.</param>
	/// <param name="inputTransparent">Whether the <paramref name="element"/> should be involved in the user interaction cycle.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.InputTransparent"/> property set to the supplied
	/// <paramref name="inputTransparent"/>.
	/// </returns>
	public static TElement InputTransparent<TElement>(this TElement element, bool inputTransparent) where TElement : VisualElement
	{
		element.InputTransparent = inputTransparent;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.IsEnabled"/> property to the supplied <paramref name="isEnabled"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="isEnabled"/> to.</param>
	/// <param name="isEnabled">Whether the <paramref name="element"/> should be enabled.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.IsEnabled"/> property set to the supplied
	/// <paramref name="isEnabled"/>.
	/// </returns>
	public static TElement IsEnabled<TElement>(this TElement element, bool isEnabled) where TElement : VisualElement
	{
		element.IsEnabled = isEnabled;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.IsVisible"/> property to the supplied <paramref name="isVisible"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="isVisible"/> to.</param>
	/// <param name="isVisible">Whether the <paramref name="element"/> should be visible.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.IsVisible"/> property set to the supplied
	/// <paramref name="isVisible"/>.
	/// </returns>
	public static TElement IsVisible<TElement>(this TElement element, bool isVisible) where TElement : VisualElement
	{
		element.IsVisible = isVisible;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.Opacity"/> property to the supplied <paramref name="opacity"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="opacity"/> to.</param>
	/// <param name="opacity">The Opacity to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.Opacity"/> property set to the supplied
	/// <paramref name="opacity"/>.
	/// </returns>
	public static TElement Opacity<TElement>(this TElement element, double opacity) where TElement : VisualElement
	{
		element.Opacity = opacity;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.Rotation"/> property to the supplied <paramref name="rotation"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="rotation"/> to.</param>
	/// <param name="rotation">The Rotation to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.Rotation"/> property set to the supplied
	/// <paramref name="rotation"/>.
	/// </returns>
	public static TElement Rotation<TElement>(this TElement element, double rotation) where TElement : VisualElement
	{
		element.Rotation = rotation;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.RotationX"/> property to the supplied <paramref name="rotationX"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="rotationX"/> to.</param>
	/// <param name="rotationX">The RotationX to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.RotationX"/> property set to the supplied
	/// <paramref name="rotationX"/>.
	/// </returns>
	public static TElement RotationX<TElement>(this TElement element, double rotationX) where TElement : VisualElement
	{
		element.RotationX = rotationX;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.RotationY"/> property to the supplied <paramref name="rotationY"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="rotationY"/> to.</param>
	/// <param name="rotationY">The RotationY to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.RotationY"/> property set to the supplied
	/// <paramref name="rotationY"/>.
	/// </returns>
	public static TElement RotationY<TElement>(this TElement element, double rotationY) where TElement : VisualElement
	{
		element.RotationY = rotationY;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.ScaleX"/> property to the supplied <paramref name="scaleX"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="scaleX"/> to.</param>
	/// <param name="scaleX">The scale value to apply in the X direction to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.ScaleX"/> property set to the supplied
	/// <paramref name="scaleX"/>.
	/// </returns>
	public static TElement ScaleX<TElement>(this TElement element, double scaleX) where TElement : VisualElement
	{
		element.ScaleX = scaleX;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.ScaleY"/> property to the supplied <paramref name="scaleY"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="scaleY"/> to.</param>
	/// <param name="scaleY">The scale value to apply in the Y direction to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.ScaleY"/> property set to the supplied
	/// <paramref name="scaleY"/>.
	/// </returns>
	public static TElement ScaleY<TElement>(this TElement element, double scaleY) where TElement : VisualElement
	{
		element.ScaleY = scaleY;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.ScaleX"/> property to the supplied <paramref name="scaleX"/>
	/// and the <see cref="VisualElement.ScaleY"/> property to the supplied <paramref name="scaleY"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="scaleX"/> and <paramref name="scaleY"/> to.</param>
	/// <param name="scaleX">The scale value to apply in the X direction to this <paramref name="element"/>.</param>
	/// <param name="scaleY">The scale value to apply in the Y direction to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.ScaleX"/> property set to the supplied
	/// <paramref name="scaleX"/> and the <see cref="VisualElement.ScaleY"/> property set to the supplied
	/// <paramref name="scaleY"/>.
	/// </returns>
	public static TElement Scale<TElement>(this TElement element, double scaleX, double scaleY) where TElement : VisualElement
		=> element.ScaleX(scaleX).ScaleY(scaleY);

	/// <summary>
	/// Sets the <see cref="VisualElement.ScaleX"/> and <see cref="VisualElement.ScaleY"/> properties
	/// to the supplied <paramref name="scale"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="scale"/> to.</param>
	/// <param name="scale">The scale value to apply in the X and Y directions to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.ScaleX"/> property set to the supplied
	/// <paramref name="scale"/> and the <see cref="VisualElement.ScaleY"/> property set to the supplied
	/// <paramref name="scale"/>.
	/// </returns>
	public static TElement Scale<TElement>(this TElement element, double scale) where TElement : VisualElement
		=> element.ScaleX(scale).ScaleY(scale);

	/// <summary>
	/// Sets the <see cref="VisualElement.TranslationX"/> property to the supplied <paramref name="translationX"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="translationX"/> to.</param>
	/// <param name="translationX">The X translation delta to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.TranslationX"/> property set to the supplied
	/// <paramref name="translationX"/>.
	/// </returns>
	public static TElement TranslationX<TElement>(this TElement element, double translationX) where TElement : VisualElement
	{
		element.TranslationX = translationX;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.TranslationY"/> property to the supplied <paramref name="translationY"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="translationY"/> to.</param>
	/// <param name="translationY">The Y translation delta to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.TranslationY"/> property set to the supplied
	/// <paramref name="translationY"/>.
	/// </returns>
	public static TElement TranslationY<TElement>(this TElement element, double translationY) where TElement : VisualElement
	{
		element.TranslationY = translationY;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.ZIndex"/> property to the supplied <paramref name="zIndex"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="zIndex"/> to.</param>
	/// <param name="zIndex">The ZIndex to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.ZIndex"/> property set to the supplied
	/// <paramref name="zIndex"/>.
	/// </returns>
	public static TElement ZIndex<TElement>(this TElement element, int zIndex) where TElement : VisualElement
	{
		element.ZIndex = zIndex;
		return element;
	}
}
