using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Graphics;

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
	/// Remove Dynamic Resource
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="properties"></param>
	/// <returns>Layout without Dynamic Resource</returns>
	public static TBindable RemoveDynamicResources<TBindable>(this TBindable bindable, params BindableProperty[] properties) where TBindable : BindableObject
	{
		foreach (var property in properties)
		{
			bindable.RemoveDynamicResource(property);
		}

		return bindable;
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
		{
			element.Effects.Add(effects[i]);
		}

		return element;
	}

	/// <summary>
	/// Sets FontSize
	/// </summary>
	/// <typeparam name="TFontElement"></typeparam>
	/// <param name="fontElement"></param>
	/// <param name="size"></param>
	/// <returns></returns>
	public static TFontElement FontSize<TFontElement>(this TFontElement fontElement, double size) where TFontElement : BindableObject, IFontElement
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
	public static TFontElement Bold<TFontElement>(this TFontElement fontElement) where TFontElement : BindableObject, IFontElement
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
	public static TFontElement Italic<TFontElement>(this TFontElement fontElement) where TFontElement : BindableObject, IFontElement
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
		bool? italic = null) where TFontElement : BindableObject, IFontElement
	{
		if (family != null)
		{
			fontElement.SetValue(FontElement.FontFamilyProperty, family);
		}

		if (size.HasValue)
		{
			fontElement.SetValue(FontElement.FontSizeProperty, size.Value);
		}

		if (bold.HasValue || italic.HasValue)
		{
			var attributes = FontAttributes.None;

			if (bold is true)
			{
				attributes |= FontAttributes.Bold;
			}

			if (italic is true)
			{
				attributes |= FontAttributes.Italic;
			}

			fontElement.SetValue(FontElement.FontAttributesProperty, attributes);
		}

		return fontElement;
	}

	/// <summary>
	/// Sets <see cref="ITextStyle.TextColor"/> Property
	/// </summary>
	/// <typeparam name="TElement"></typeparam>
	/// <param name="element">Element</param>
	/// <param name="textColor">Text <see cref="Color"/></param>
	/// <returns></returns>
	public static TElement TextColor<TElement>(this TElement element, Color textColor) where TElement : BindableObject, ITextStyle
	{
		element.SetValue(TextElement.TextColorProperty, textColor);
		return element;
	}
}