using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace CommunityToolkit.Maui.Markup
{
	/// <summary>
	/// Extension Methods for <see cref="ITextAlignment"/>
	/// </summary>
	public static class TextAlignmentExtensions
	{
		/// <summary>
		/// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.Start"/>
		/// </summary>
		/// <typeparam name="TBindable"></typeparam>
		/// <param name="bindable"></param>
		/// <returns><typeparam name="TBindable"/> with added <see cref="TextAlignment.Start"/></returns>
		public static TBindable TextStart<TBindable>(this TBindable bindable) where TBindable : BindableObject, ITextAlignment
		{
			bindable.SetValue(TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.Start);
			return bindable;
		}

		/// <summary>
		/// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.Center"/>
		/// </summary>
		/// <typeparam name="TBindable"></typeparam>
		/// <param name="bindable"></param>
		/// <returns><typeparam name="TBindable"/> with added <see cref="TextAlignment.Center"/></returns>
		public static TBindable TextCenterHorizontal<TBindable>(this TBindable bindable) where TBindable : BindableObject, ITextAlignment
		{
			bindable.SetValue(TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.Center);
			return bindable;
		}

		/// <summary>
		/// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.End"/>
		/// </summary>
		/// <typeparam name="TBindable"></typeparam>
		/// <param name="bindable"></param>
		/// <returns><typeparam name="TBindable"/> with added <see cref="TextAlignment.End"/></returns>
		public static TBindable TextEnd<TBindable>(this TBindable bindable) where TBindable : BindableObject, ITextAlignment
		{
			bindable.SetValue(TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.End);
			return bindable;
		}

		/// <summary>
		/// <see cref="ITextAlignment.VerticalTextAlignment"/> = <see cref="TextAlignment.Start"/>
		/// </summary>
		/// <typeparam name="TBindable"></typeparam>
		/// <param name="bindable"></param>
		/// <returns><typeparam name="TBindable"/> with added <see cref="TextAlignment.Start"/></returns>
		public static TBindable TextTop<TBindable>(this TBindable bindable) where TBindable : BindableObject, ITextAlignment
		{
			bindable.SetValue(TextAlignmentElement.VerticalTextAlignmentProperty, TextAlignment.Start);
			return bindable;
		}

		/// <summary>
		/// <see cref="ITextAlignment.VerticalTextAlignment"/> = <see cref="TextAlignment.Center"/>
		/// </summary>
		/// <typeparam name="TBindable"></typeparam>
		/// <param name="bindable"></param>
		/// <returns><typeparam name="TBindable"/> with added <see cref="TextAlignment.Center"/></returns>
		public static TBindable TextCenterVertical<TBindable>(this TBindable bindable) where TBindable : BindableObject, ITextAlignment
		{
			bindable.SetValue(TextAlignmentElement.VerticalTextAlignmentProperty, TextAlignment.Center);
			return bindable;
		}

		/// <summary>
		/// <see cref="ITextAlignment.VerticalTextAlignment"/> = <see cref="TextAlignment.End"/>
		/// </summary>
		/// <typeparam name="TBindable"></typeparam>
		/// <param name="bindable"></param>
		/// <returns><typeparam name="TBindable"/> with added <see cref="TextAlignment.End"/></returns>
		public static TBindable TextBottom<TBindable>(this TBindable bindable) where TBindable : BindableObject, ITextAlignment
		{
			bindable.SetValue(TextAlignmentElement.VerticalTextAlignmentProperty, TextAlignment.End);
			return bindable;
		}

		/// <summary>
		/// <see cref="ITextAlignment.VerticalTextAlignment"/> = <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.Center"/>
		/// </summary>
		/// <typeparam name="TBindable"></typeparam>
		/// <param name="bindable"></param>
		/// <returns><typeparam name="TBindable"/> with added <see cref="TextAlignment.Center"/></returns>
		public static TBindable TextCenter<TBindable>(this TBindable bindable) where TBindable : BindableObject, ITextAlignment
			=> bindable.TextCenterHorizontal().TextCenterVertical();
	}


	// The extensions in these sub-namespaces are designed to be used together with the extensions in the parent namespace.
	// Keep them in a single file for better maintainability

	/// <summary>
	/// Extension Methods for Left-to-Right Text
	/// </summary>
	namespace LeftToRight
	{
		/// <summary>
		/// Extension Methods for <see cref="ITextAlignment"/>
		/// </summary>
		public static class TextAlignmentExtensions
		{
			/// <summary>
			/// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.Start"/>
			/// </summary>
			/// <typeparam name="TBindable"></typeparam>
			/// <param name="bindable"></param>
			/// <returns><typeparam name="TBindable"/> with <see cref="TextAlignment.Start"/></returns>
			public static TBindable TextLeft<TBindable>(this TBindable bindable) where TBindable : BindableObject, ITextAlignment
			{
				bindable.SetValue(TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.Start);
				return bindable;
			}

			/// <summary>
			/// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.End"/>
			/// </summary>
			/// <typeparam name="TBindable"></typeparam>
			/// <param name="bindable"></param>
			/// <returns><typeparam name="TBindable"/> with <see cref="TextAlignment.End"/></returns>
			public static TBindable TextRight<TBindable>(this TBindable bindable) where TBindable : BindableObject, ITextAlignment
			{
				bindable.SetValue(TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.End);
				return bindable;
			}
		}
	}

	// The extensions in these sub-namespaces are designed to be used together with the extensions in the parent namespace.
	// Keep them in a single file for better maintainability

	/// <summary>
	/// Extension Methods for Right-to-Left Text
	/// </summary>
	namespace RightToLeft
	{
		/// <summary>
		/// Extension methods for <see cref="ITextAlignment"/>
		/// </summary>
		public static class TextAlignmentExtensions
		{
			/// <summary>
			/// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.End"/>
			/// </summary>
			/// <typeparam name="TBindable"></typeparam>
			/// <param name="bindable"></param>
			/// <returns><typeparam name="TBindable"/> with <see cref="TextAlignment.End"/></returns>
			public static TBindable TextLeft<TBindable>(this TBindable bindable) where TBindable : BindableObject, ITextAlignment
			{
				bindable.SetValue(TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.End);
				return bindable;
			}

			/// <summary>
			/// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.Start"/>
			/// </summary>
			/// <typeparam name="TBindable"></typeparam>
			/// <param name="bindable"></param>
			/// <returns><typeparam name="TBindable"/> with <see cref="TextAlignment.Start"/></returns>
			public static TBindable TextRight<TBindable>(this TBindable bindable) where TBindable : BindableObject, ITextAlignment
			{
				bindable.SetValue(TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.Start);
				return bindable;
			}
		}
	}
}