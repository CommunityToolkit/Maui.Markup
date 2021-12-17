using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Internals;
using FontElement = Microsoft.Maui.Controls.Label; // TODO: Get rid of this after we have default interface implementation in Forms for IFontElement
using PaddingElement = Microsoft.Maui.Controls.Label; // TODO: Get rid of this after we have default interface implementation in Forms for IPaddingElement

namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension Methods for Elements
/// </summary>
public static class ElementExtensions
{
	/// <summary>
	/// Set Padding
	/// </summary>
	/// <typeparam name="TLayout"></typeparam>
	/// <param name="paddingElement"></param>
	/// <param name="padding"></param>
	/// <returns>Layout with added Padding</returns>
	public static TLayout Padding<TLayout>(this TLayout paddingElement, Thickness padding) where TLayout : Element, IPaddingElement
	{
		paddingElement.SetValue(PaddingElement.PaddingProperty, padding);

		return paddingElement;
	}

	/// <summary>
	/// Set Padding
	/// </summary>
	/// <typeparam name="TLayout"></typeparam>
	/// <param name="paddingElement"></param>
	/// <param name="horizontalSize"></param>
	/// <param name="verticalSize"></param>
	/// <returns>Layout with added Padding</returns>
	public static TLayout Padding<TLayout>(this TLayout paddingElement, double horizontalSize, double verticalSize) where TLayout : Element, IPaddingElement
	{
		paddingElement.SetValue(PaddingElement.PaddingProperty, new Thickness(horizontalSize, verticalSize));

		return paddingElement;
	}

	/// <summary>
	/// Set Padding
	/// </summary>
	/// <typeparam name="TLayout"></typeparam>
	/// <param name="paddingElement"></param>
	/// <param name="left"></param>
	/// <param name="top"></param>
	/// <param name="right"></param>
	/// <param name="bottom"></param>
	/// <returns>Layout with added Padding</returns>
	public static TLayout Paddings<TLayout>(this TLayout paddingElement, double left = 0, double top = 0, double right = 0, double bottom = 0) where TLayout : Element, IPaddingElement
	{
		paddingElement.SetValue(PaddingElement.PaddingProperty, new Thickness(left, top, right, bottom));

		return paddingElement;
	}

	/// <summary>
	/// Set Dynamic Resource
	/// </summary>
	/// <typeparam name="TElement"></typeparam>
	/// <param name="element"></param>
	/// <param name="property"></param>
	/// <param name="key"></param>
	/// <returns>Layout with added Dynamic Resource</returns>
	public static TElement DynamicResource<TElement>(this TElement element, BindableProperty property, string key) where TElement : Element
	{
		element.SetDynamicResource(property, key);

		return element;
	}

	/// <summary>
	/// Set Dynamic Resource
	/// </summary>
	/// <typeparam name="TElement"></typeparam>
	/// <param name="element"></param>
	/// <param name="resources"></param>
	/// <returns>Layout with added Dynamic Resource</returns>
	public static TElement DynamicResources<TElement>(this TElement element, params (BindableProperty property, string key)[] resources) where TElement : Element
	{
		foreach (var resource in resources)
			element.SetDynamicResource(resource.property, resource.key);

		return element;
	}

	/// <summary>
	/// Remove Dynamic Resource
	/// </summary>
	/// <typeparam name="TElement"></typeparam>
	/// <param name="element"></param>
	/// <param name="properties"></param>
	/// <returns>Layout without Dynamic Resource</returns>
	public static TElement RemoveDynamicResources<TElement>(this TElement element, params BindableProperty[] properties) where TElement : Element
	{
		foreach (var property in properties)
			element.RemoveDynamicResource(property);

		return element;
	}

	/// <summary>
	/// Add Effects
	/// </summary>
	/// <typeparam name="TElement"></typeparam>
	/// <param name="element"></param>
	/// <param name="effects"></param>
	/// <returns>Element with added Effects</returns>
	public static TElement Effects<TElement>(this TElement element, params Effect[] effects) where TElement : Element
	{
		for (var i = 0; i < effects.Length; i++)
			element.Effects.Add(effects[i]);

		return element;
	}

	/// <summary>
	/// Sets FontSize
	/// </summary>
	/// <typeparam name="TFontElement"></typeparam>
	/// <param name="fontElement"></param>
	/// <param name="size"></param>
	/// <returns></returns>
	public static TFontElement FontSize<TFontElement>(this TFontElement fontElement, double size) where TFontElement : Element, IFontElement
	{
		fontElement.SetValue(FontElement.FontSizeProperty, size);
		return fontElement;
	}

	/// <summary>
	/// Sets Bold
	/// </summary>
	/// <typeparam name="TFontElement"></typeparam>
	/// <param name="fontElement"></param>
	/// <returns>Font element with added Bold</returns>
	public static TFontElement Bold<TFontElement>(this TFontElement fontElement) where TFontElement : Element, IFontElement
	{
		fontElement.SetValue(FontElement.FontAttributesProperty, FontAttributes.Bold);
		return fontElement;
	}

	/// <summary>
	/// Sets Italic
	/// </summary>
	/// <typeparam name="TFontElement"></typeparam>
	/// <param name="fontElement"></param>
	/// <returns>Font element with added Italic</returns>
	public static TFontElement Italic<TFontElement>(this TFontElement fontElement) where TFontElement : Element, IFontElement
	{
		fontElement.SetValue(FontElement.FontAttributesProperty, FontAttributes.Italic);
		return fontElement;
	}

	/// <summary>
	/// Sets Font Properties
	/// </summary>
	/// <typeparam name="TFontElement"></typeparam>
	/// <param name="fontElement"></param>
	/// <param name="family"></param>
	/// <param name="size"></param>
	/// <param name="bold"></param>
	/// <param name="italic"></param>
	/// <returns>Font element with added Font properties</returns>
	public static TFontElement Font<TFontElement>(
		this TFontElement fontElement,
		string? family = null,
		double? size = null,
		bool? bold = null,
		bool? italic = null) where TFontElement : Element, IFontElement
	{
		if (family != null)
			fontElement.SetValue(FontElement.FontFamilyProperty, family);

		if (size.HasValue)
			fontElement.SetValue(FontElement.FontSizeProperty, size.Value);

		if (bold.HasValue || italic.HasValue)
		{
			var attributes = FontAttributes.None;

			if (bold == true)
				attributes |= FontAttributes.Bold;
			if (italic == true)
				attributes |= FontAttributes.Italic;

			fontElement.SetValue(FontElement.FontAttributesProperty, attributes);
		}

		return fontElement;
	}
}