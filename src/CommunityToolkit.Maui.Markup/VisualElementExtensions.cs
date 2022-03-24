using Microsoft.Maui.Controls;

namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Visual Element Extensions
/// </summary>
public static class VisualElementExtensions
{
	/// <summary>
	/// Set Height Request
	/// </summary>
	/// <typeparam name="TElement"></typeparam>
	/// <param name="element"></param>
	/// <param name="request"></param>
	/// <returns>Element with HeightRequest set</returns>
	public static TElement Height<TElement>(this TElement element, double request) where TElement : VisualElement
	{
		element.HeightRequest = request;
		return element;
	}

	/// <summary>
	/// Set Width Request
	/// </summary>
	/// <typeparam name="TElement"></typeparam>
	/// <param name="element"></param>
	/// <param name="request"></param>
	/// <returns>Vie with WidthRequest set</returns>
	public static TElement Width<TElement>(this TElement element, double request) where TElement : VisualElement
	{
		element.WidthRequest = request;
		return element;
	}

	/// <summary>
	/// Set Minimum Height Request
	/// </summary>
	/// <typeparam name="TElement"></typeparam>
	/// <param name="element"></param>
	/// <param name="request"></param>
	/// <returns>Element with MinimumHeightRequest set</returns>
	public static TElement MinHeight<TElement>(this TElement element, double request) where TElement : VisualElement
	{
		element.MinimumHeightRequest = request;
		return element;
	}

	/// <summary>
	/// Set Minimum Width Request
	/// </summary>
	/// <typeparam name="TElement"></typeparam>
	/// <param name="element"></param>
	/// <param name="request"></param>
	/// <returns>Element with MinimumWidthRequest set</returns>
	public static TElement MinWidth<TElement>(this TElement element, double request) where TElement : VisualElement
	{
		element.MinimumWidthRequest = request;
		return element;
	}

	/// <summary>
	/// Set Width Request + Height Request
	/// </summary>
	/// <typeparam name="TElement"></typeparam>
	/// <param name="element"></param>
	/// <param name="widthRequest"></param>
	/// <param name="heightRequest"></param>
	/// <returns>Element with WidthRequest and HeightRequest set</returns>
	public static TElement Size<TElement>(this TElement element, double widthRequest, double heightRequest) where TElement : VisualElement
		=> element.Width(widthRequest).Height(heightRequest);

	/// <summary>
	/// Set Equal Width Request + Height Request
	/// </summary>
	/// <typeparam name="TElement"></typeparam>
	/// <param name="element"></param>
	/// <param name="sizeRequest"></param>
	/// <returns>Element with equal WidthRequest and HeightRequest set</returns>
	public static TElement Size<TElement>(this TElement element, double sizeRequest) where TElement : VisualElement
		=> element.Width(sizeRequest).Height(sizeRequest);

	/// <summary>
	/// Set Minimum Width Request + Minimum Height Request
	/// </summary>
	/// <typeparam name="TElement"></typeparam>
	/// <param name="element"></param>
	/// <param name="widthRequest"></param>
	/// <param name="heightRequest"></param>
	/// <returns>Element with MinimumWidthRequest and MinimumHeightRequest set</returns>
	public static TElement MinSize<TElement>(this TElement element, double widthRequest, double heightRequest) where TElement : VisualElement
		=> element.MinWidth(widthRequest).MinHeight(heightRequest);

	/// <summary>
	/// Set equal Minimum Width Request + Minimum Height Request
	/// </summary>
	/// <typeparam name="TElement"></typeparam>
	/// <param name="element"></param>
	/// <param name="sizeRequest"></param>
	/// <returns>Element with equal MinimumWidthRequest and MinimumHeightRequest set</returns>
	public static TElement MinSize<TElement>(this TElement element, double sizeRequest) where TElement : VisualElement
		=> element.MinWidth(sizeRequest).MinHeight(sizeRequest);

	/// <summary>
	/// Set Style
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="view"></param>
	/// <param name="style"></param>
	/// <returns>Element with Style set</returns>
	public static T Style<T>(this T view, Style<T> style) where T : VisualElement
	{
		view.Style = style;
		return view;
	}

	/// <summary>
	/// Adds the supplied <paramref name="behaviors"/> to the Behaviors collection for this <see cref="VisualElement"/>.
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