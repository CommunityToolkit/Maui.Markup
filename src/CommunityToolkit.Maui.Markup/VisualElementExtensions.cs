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
	/// <typeparam name="TVisualElement">The type of visual element being updated.</typeparam>
	/// <param name="element">This element to apply the <paramref name="automationId"/> to.</param>
	/// <param name="automationId">The value that the automation framework can use to find and interact with.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the AutomationId property set
	/// to the supplied <paramref name="automationId"/>.
	/// </returns>
	public static TVisualElement AutomationId<TVisualElement>(
		this TVisualElement element,
		string automationId)
		where TVisualElement : VisualElement
	{
		element.AutomationId = automationId;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.HeightRequest"/> property to the supplied <paramref name="heightRequest"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="heightRequest"/> to.</param>
	/// <param name="heightRequest">The height to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.HeightRequest"/> property set to the supplied
	/// <paramref name="heightRequest"/>.
	/// </returns>
	public static TVisualElement Height<TVisualElement>(this TVisualElement element, double heightRequest) where TVisualElement : VisualElement
	{
		element.HeightRequest = heightRequest;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.WidthRequest"/> property to the supplied <paramref name="widthRequest"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="widthRequest"/> to.</param>
	/// <param name="widthRequest">The width to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.WidthRequest"/> property set to the supplied
	/// <paramref name="widthRequest"/>.
	/// </returns>
	public static TVisualElement Width<TVisualElement>(this TVisualElement element, double widthRequest) where TVisualElement : VisualElement
	{
		element.WidthRequest = widthRequest;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.MinimumHeightRequest"/> property to the supplied <paramref name="minimumHeightRequest"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="minimumHeightRequest"/> to.</param>
	/// <param name="minimumHeightRequest">The minimum height to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.MinimumHeightRequest"/> property set to the supplied
	/// <paramref name="minimumHeightRequest"/>.
	/// </returns>
	public static TVisualElement MinHeight<TVisualElement>(this TVisualElement element, double minimumHeightRequest) where TVisualElement : VisualElement
	{
		element.MinimumHeightRequest = minimumHeightRequest;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.MinimumWidthRequest"/> property to the supplied <paramref name="minimumWidthRequest"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="minimumWidthRequest"/> to.</param>
	/// <param name="minimumWidthRequest">The minimum width to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.MinimumWidthRequest"/> property set to the supplied
	/// <paramref name="minimumWidthRequest"/>.
	/// </returns>
	public static TVisualElement MinWidth<TVisualElement>(this TVisualElement element, double minimumWidthRequest) where TVisualElement : VisualElement
	{
		element.MinimumWidthRequest = minimumWidthRequest;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.WidthRequest"/> property to the supplied <paramref name="widthRequest"/>
	/// and the <see cref="VisualElement.HeightRequest"/> property to the supplied <paramref name="heightRequest"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="widthRequest"/> and <paramref name="heightRequest"/> to.</param>
	/// <param name="widthRequest">The width to apply to this <paramref name="element"/>.</param>
	/// <param name="heightRequest">The height to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.WidthRequest"/> property set to the supplied
	/// <paramref name="widthRequest"/> and the <see cref="VisualElement.HeightRequest"/> property set to the supplied
	/// <paramref name="heightRequest"/>.
	/// </returns>
	public static TVisualElement Size<TVisualElement>(this TVisualElement element, double widthRequest, double heightRequest) where TVisualElement : VisualElement
		=> element.Width(widthRequest).Height(heightRequest);

	/// <summary>
	/// Sets both the <see cref="VisualElement.WidthRequest"/> and <see cref="VisualElement.HeightRequest"/> to
	/// the same supplied <paramref name="sizeRequest"/> on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="sizeRequest"/> to.</param>
	/// <param name="sizeRequest">The size to apply to both the <see cref="VisualElement.WidthRequest"/> and
	/// <see cref="VisualElement.HeightRequest"/> properties.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with both the <see cref="VisualElement.WidthRequest"/> and
	/// <see cref="VisualElement.HeightRequest"/> set to the same supplied <paramref name="sizeRequest"/>.
	/// </returns>
	public static TVisualElement Size<TVisualElement>(this TVisualElement element, double sizeRequest) where TVisualElement : VisualElement
		=> element.Width(sizeRequest).Height(sizeRequest);

	/// <summary>
	/// Sets the <see cref="VisualElement.MinimumWidthRequest"/> property to the supplied <paramref name="minimumWidthRequest"/>
	/// and the <see cref="VisualElement.MinimumHeightRequest"/> property to the supplied <paramref name="minimumHeightRequest"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="minimumWidthRequest"/> and <paramref name="minimumHeightRequest"/> to.</param>
	/// <param name="minimumWidthRequest">The minimum width to apply to this <paramref name="element"/>.</param>
	/// <param name="minimumHeightRequest">The minimum height to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.MinimumWidthRequest"/> property set to the supplied
	/// <paramref name="minimumWidthRequest"/> and the <see cref="VisualElement.MinimumHeightRequest"/> property set to the supplied
	/// <paramref name="minimumHeightRequest"/>.
	/// </returns>
	public static TVisualElement MinSize<TVisualElement>(this TVisualElement element, double minimumWidthRequest, double minimumHeightRequest) where TVisualElement : VisualElement
		=> element.MinWidth(minimumWidthRequest).MinHeight(minimumHeightRequest);

	/// <summary>
	/// Sets both the <see cref="VisualElement.MinimumWidthRequest"/> and <see cref="VisualElement.MinimumHeightRequest"/> to
	/// the same supplied <paramref name="minimumSizeRequest"/> on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="minimumSizeRequest"/> to.</param>
	/// <param name="minimumSizeRequest">The minimum size to apply to both the <see cref="VisualElement.MinimumWidthRequest"/> and
	/// <see cref="VisualElement.MinimumHeightRequest"/> properties.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with both the <see cref="VisualElement.MinimumWidthRequest"/> and
	/// <see cref="VisualElement.MinimumHeightRequest"/> set to the same supplied <paramref name="minimumSizeRequest"/>.
	/// </returns>
	public static TVisualElement MinSize<TVisualElement>(this TVisualElement element, double minimumSizeRequest) where TVisualElement : VisualElement
		=> element.MinWidth(minimumSizeRequest).MinHeight(minimumSizeRequest);

	/// <summary>
	/// Sets the supplied <paramref name="style"/> on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="style"/> to.</param>
	/// <param name="style">The <see cref="Markup.Style{T}"/> to apply.</param>
	/// <returns>The supplied <paramref name="element"/> with the supplied <paramref name="style"/> applied.</returns>
	public static TVisualElement Style<TVisualElement>(this TVisualElement element, Style<TVisualElement> style) where TVisualElement : VisualElement
		=> element.Style((Style)style);

	/// <summary>
	/// Sets the supplied <paramref name="style"/> on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="style"/> to.</param>
	/// <param name="style">The <see cref="Microsoft.Maui.Controls.Style"/> to apply.</param>
	/// <returns>The supplied <paramref name="element"/> with the supplied <paramref name="style"/> applied.</returns>
	public static TVisualElement Style<TVisualElement>(this TVisualElement element, Style style) where TVisualElement : VisualElement
	{
		element.Style = style;
		return element;
	}

	/// <summary>
	/// Adds the supplied <paramref name="behaviors"/> to the Behaviors collection on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to add the <paramref name="behaviors"/> to.</param>
	/// <param name="behaviors">The <see cref="Behavior"/>s to add.</param>
	/// <returns>The supplied <paramref name="element"/> with the supplied <paramref name="behaviors"/> added.</returns>
	public static TVisualElement Behaviors<TVisualElement>(this TVisualElement element, params Behavior[] behaviors) where TVisualElement : VisualElement
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
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="anchorX"/> to.</param>
	/// <param name="anchorX">The X component of the center point for any transform, relative to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.AnchorX"/> property set to the supplied
	/// <paramref name="anchorX"/>.
	/// </returns>
	public static TVisualElement AnchorX<TVisualElement>(this TVisualElement element, double anchorX) where TVisualElement : VisualElement
	{
		element.AnchorX = anchorX;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.AnchorY"/> property to the supplied <paramref name="anchorY"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="anchorY"/> to.</param>
	/// <param name="anchorY">The Y component of the center point for any transform, relative to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.AnchorY"/> property set to the supplied
	/// <paramref name="anchorY"/>.
	/// </returns>
	public static TVisualElement AnchorY<TVisualElement>(this TVisualElement element, double anchorY) where TVisualElement : VisualElement
	{
		element.AnchorY = anchorY;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.AnchorX"/> property to the supplied <paramref name="anchorX"/>
	/// and the <see cref="VisualElement.AnchorY"/> property to the supplied <paramref name="anchorY"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="anchorX"/> and <paramref name="anchorY"/> to.</param>
	/// <param name="anchorX">The X component of the center point for any transform, to apply to this <paramref name="element"/>.</param>
	/// <param name="anchorY">The Y component of the center point for any transform, to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.AnchorX"/> property set to the supplied
	/// <paramref name="anchorX"/> and the <see cref="VisualElement.AnchorY"/> property set to the supplied
	/// <paramref name="anchorY"/>.
	/// </returns>
	public static TVisualElement Anchor<TVisualElement>(this TVisualElement element, double anchorX, double anchorY) where TVisualElement : VisualElement
		=> element.AnchorX(anchorX).AnchorY(anchorY);

	/// <summary>
	/// Sets the <see cref="VisualElement.Background"/> property to the supplied <paramref name="backgroundBrush"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="backgroundBrush"/> to.</param>
	/// <param name="backgroundBrush">The Brush to apply the the Background of this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.Background"/> property set to the supplied
	/// <paramref name="backgroundBrush"/>.
	/// </returns>
	public static TVisualElement Background<TVisualElement>(this TVisualElement element, Brush backgroundBrush) where TVisualElement : VisualElement
	{
		element.Background = backgroundBrush;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.BackgroundColor"/> property to the supplied <paramref name="color"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="color"/> to.</param>
	/// <param name="color">The BackgroundColor to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.BackgroundColor"/> property set to the supplied
	/// <paramref name="color"/>.
	/// </returns>
	public static TVisualElement BackgroundColor<TVisualElement>(this TVisualElement element, Color color) where TVisualElement : VisualElement
	{
		element.BackgroundColor = color;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.Clip"/> property to the supplied <paramref name="clip"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="clip"/> to.</param>
	/// <param name="clip">The Clip to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.Clip"/> property set to the supplied
	/// <paramref name="clip"/>.
	/// </returns>
	public static TVisualElement Clip<TVisualElement>(this TVisualElement element, Geometry clip) where TVisualElement : VisualElement
	{
		element.Clip = clip;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.FlowDirection"/> property to the supplied <paramref name="flowDirection"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="flowDirection"/> to.</param>
	/// <param name="flowDirection">The FlowDirection to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.FlowDirection"/> property set to the supplied
	/// <paramref name="flowDirection"/>.
	/// </returns>
	public static TVisualElement FlowDirection<TVisualElement>(this TVisualElement element, FlowDirection flowDirection) where TVisualElement : VisualElement
	{
		element.FlowDirection = flowDirection;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.InputTransparent"/> property to the supplied <paramref name="inputTransparent"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="inputTransparent"/> to.</param>
	/// <param name="inputTransparent">Whether the <paramref name="element"/> should be involved in the user interaction cycle.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.InputTransparent"/> property set to the supplied
	/// <paramref name="inputTransparent"/>.
	/// </returns>
	public static TVisualElement InputTransparent<TVisualElement>(this TVisualElement element, bool inputTransparent) where TVisualElement : VisualElement
	{
		element.InputTransparent = inputTransparent;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.IsEnabled"/> property to the supplied <paramref name="isEnabled"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="isEnabled"/> to.</param>
	/// <param name="isEnabled">Whether the <paramref name="element"/> should be enabled.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.IsEnabled"/> property set to the supplied
	/// <paramref name="isEnabled"/>.
	/// </returns>
	public static TVisualElement IsEnabled<TVisualElement>(this TVisualElement element, bool isEnabled) where TVisualElement : VisualElement
	{
		element.IsEnabled = isEnabled;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.IsVisible"/> property to the supplied <paramref name="isVisible"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="isVisible"/> to.</param>
	/// <param name="isVisible">Whether the <paramref name="element"/> should be visible.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.IsVisible"/> property set to the supplied
	/// <paramref name="isVisible"/>.
	/// </returns>
	public static TVisualElement IsVisible<TVisualElement>(this TVisualElement element, bool isVisible) where TVisualElement : VisualElement
	{
		element.IsVisible = isVisible;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.Opacity"/> property to the supplied <paramref name="opacity"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="opacity"/> to.</param>
	/// <param name="opacity">The Opacity to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.Opacity"/> property set to the supplied
	/// <paramref name="opacity"/>.
	/// </returns>
	public static TVisualElement Opacity<TVisualElement>(this TVisualElement element, double opacity) where TVisualElement : VisualElement
	{
		element.Opacity = opacity;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.Rotation"/> property to the supplied <paramref name="rotation"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="rotation"/> to.</param>
	/// <param name="rotation">The Rotation to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.Rotation"/> property set to the supplied
	/// <paramref name="rotation"/>.
	/// </returns>
	public static TVisualElement Rotation<TVisualElement>(this TVisualElement element, double rotation) where TVisualElement : VisualElement
	{
		element.Rotation = rotation;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.RotationX"/> property to the supplied <paramref name="rotationX"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="rotationX"/> to.</param>
	/// <param name="rotationX">The RotationX to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.RotationX"/> property set to the supplied
	/// <paramref name="rotationX"/>.
	/// </returns>
	public static TVisualElement RotationX<TVisualElement>(this TVisualElement element, double rotationX) where TVisualElement : VisualElement
	{
		element.RotationX = rotationX;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.RotationY"/> property to the supplied <paramref name="rotationY"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="rotationY"/> to.</param>
	/// <param name="rotationY">The RotationY to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.RotationY"/> property set to the supplied
	/// <paramref name="rotationY"/>.
	/// </returns>
	public static TVisualElement RotationY<TVisualElement>(this TVisualElement element, double rotationY) where TVisualElement : VisualElement
	{
		element.RotationY = rotationY;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.ScaleX"/> property to the supplied <paramref name="scaleX"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="scaleX"/> to.</param>
	/// <param name="scaleX">The scale value to apply in the X direction to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.ScaleX"/> property set to the supplied
	/// <paramref name="scaleX"/>.
	/// </returns>
	public static TVisualElement ScaleX<TVisualElement>(this TVisualElement element, double scaleX) where TVisualElement : VisualElement
	{
		element.ScaleX = scaleX;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.ScaleY"/> property to the supplied <paramref name="scaleY"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="scaleY"/> to.</param>
	/// <param name="scaleY">The scale value to apply in the Y direction to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.ScaleY"/> property set to the supplied
	/// <paramref name="scaleY"/>.
	/// </returns>
	public static TVisualElement ScaleY<TVisualElement>(this TVisualElement element, double scaleY) where TVisualElement : VisualElement
	{
		element.ScaleY = scaleY;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.ScaleX"/> property to the supplied <paramref name="scaleX"/>
	/// and the <see cref="VisualElement.ScaleY"/> property to the supplied <paramref name="scaleY"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="scaleX"/> and <paramref name="scaleY"/> to.</param>
	/// <param name="scaleX">The scale value to apply in the X direction to this <paramref name="element"/>.</param>
	/// <param name="scaleY">The scale value to apply in the Y direction to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.ScaleX"/> property set to the supplied
	/// <paramref name="scaleX"/> and the <see cref="VisualElement.ScaleY"/> property set to the supplied
	/// <paramref name="scaleY"/>.
	/// </returns>
	public static TVisualElement Scale<TVisualElement>(this TVisualElement element, double scaleX, double scaleY) where TVisualElement : VisualElement
		=> element.ScaleX(scaleX).ScaleY(scaleY);

	/// <summary>
	/// Sets the <see cref="VisualElement.ScaleX"/> and <see cref="VisualElement.ScaleY"/> properties
	/// to the supplied <paramref name="scale"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="scale"/> to.</param>
	/// <param name="scale">The scale value to apply in the X and Y directions to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.ScaleX"/> property set to the supplied
	/// <paramref name="scale"/> and the <see cref="VisualElement.ScaleY"/> property set to the supplied
	/// <paramref name="scale"/>.
	/// </returns>
	public static TVisualElement Scale<TVisualElement>(this TVisualElement element, double scale) where TVisualElement : VisualElement
		=> element.ScaleX(scale).ScaleY(scale);

	/// <summary>
	/// Sets the <see cref="VisualElement.TranslationX"/> property to the supplied <paramref name="translationX"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="translationX"/> to.</param>
	/// <param name="translationX">The X translation delta to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.TranslationX"/> property set to the supplied
	/// <paramref name="translationX"/>.
	/// </returns>
	public static TVisualElement TranslationX<TVisualElement>(this TVisualElement element, double translationX) where TVisualElement : VisualElement
	{
		element.TranslationX = translationX;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.TranslationY"/> property to the supplied <paramref name="translationY"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="translationY"/> to.</param>
	/// <param name="translationY">The Y translation delta to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.TranslationY"/> property set to the supplied
	/// <paramref name="translationY"/>.
	/// </returns>
	public static TVisualElement TranslationY<TVisualElement>(this TVisualElement element, double translationY) where TVisualElement : VisualElement
	{
		element.TranslationY = translationY;
		return element;
	}

	/// <summary>
	/// Sets the <see cref="VisualElement.ZIndex"/> property to the supplied <paramref name="zIndex"/>
	/// on this <paramref name="element"/>.
	/// </summary>
	/// <typeparam name="TVisualElement">The type of element.</typeparam>
	/// <param name="element">This element to apply the <paramref name="zIndex"/> to.</param>
	/// <param name="zIndex">The ZIndex to apply to this <paramref name="element"/>.</param>
	/// <returns>
	/// The supplied <paramref name="element"/> with the <see cref="VisualElement.ZIndex"/> property set to the supplied
	/// <paramref name="zIndex"/>.
	/// </returns>
	public static TVisualElement ZIndex<TVisualElement>(this TVisualElement element, int zIndex) where TVisualElement : VisualElement
	{
		element.ZIndex = zIndex;
		return element;
	}
}