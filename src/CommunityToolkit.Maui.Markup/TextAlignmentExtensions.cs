using Microsoft.Maui;
using Microsoft.Maui.Controls;
using TextAlignmentElement = Microsoft.Maui.Controls.Label; // TODO: Get rid of this after we have default interface implementation in Forms for ITextAlignmentElement


namespace CommunityToolkit.Maui.Markup
{
	/// <summary>
	/// Extension Methods for ITextAlignment
	/// </summary>
	public static class TextAlignmentExtensions
	{
		/// <summary>
		/// HorizontalTextAlignment = TextAlignment.Start
		/// </summary>
		/// <typeparam name="TBindable"></typeparam>
		/// <param name="bindable"></param>
		/// <returns>Label with added TextAlignment.Start</returns>
		public static TBindable TextStart<TBindable>(this TBindable bindable) where TBindable : BindableObject, ITextAlignment
		{
			bindable.SetValue(TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.Start);
			return bindable;
		}

		/// <summary>
		/// HorizontalTextAlignment = TextAlignment.Center
		/// </summary>
		/// <typeparam name="TBindable"></typeparam>
		/// <param name="bindable"></param>
		/// <returns>Label with added TextAlignment.Center</returns>
		public static TBindable TextCenterHorizontal<TBindable>(this TBindable bindable) where TBindable : BindableObject, ITextAlignment
		{
			bindable.SetValue(TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.Center);
			return bindable;
		}

		/// <summary>
		/// HorizontalTextAlignment = TextAlignment.End
		/// </summary>
		/// <typeparam name="TBindable"></typeparam>
		/// <param name="bindable"></param>
		/// <returns>Label with added TextAlignment.End</returns>
		public static TBindable TextEnd<TBindable>(this TBindable bindable) where TBindable : BindableObject, ITextAlignment
		{
			bindable.SetValue(TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.End);
			return bindable;
		}

		/// <summary>
		/// VerticalTextAlignment = TextAlignment.Start
		/// </summary>
		/// <typeparam name="TBindable"></typeparam>
		/// <param name="bindable"></param>
		/// <returns>Label with added TextAlignment.Start</returns>
		public static TBindable TextTop<TBindable>(this TBindable bindable) where TBindable : BindableObject, ITextAlignment
		{
			bindable.SetValue(TextAlignmentElement.VerticalTextAlignmentProperty, TextAlignment.Start);
			return bindable;
		}

		/// <summary>
		/// VerticalTextAlignment = TextAlignment.Center
		/// </summary>
		/// <typeparam name="TBindable"></typeparam>
		/// <param name="bindable"></param>
		/// <returns>Label with added TextAlignment.Center</returns>
		public static TBindable TextCenterVertical<TBindable>(this TBindable bindable) where TBindable : BindableObject, ITextAlignment
		{
			bindable.SetValue(TextAlignmentElement.VerticalTextAlignmentProperty, TextAlignment.Center);
			return bindable;
		}

		/// <summary>
		/// VerticalTextAlignment = TextAlignment.End
		/// </summary>
		/// <typeparam name="TBindable"></typeparam>
		/// <param name="bindable"></param>
		/// <returns>Label with added TextAlignment.End</returns>
		public static TBindable TextBottom<TBindable>(this TBindable bindable) where TBindable : BindableObject, ITextAlignment
		{
			bindable.SetValue(TextAlignmentElement.VerticalTextAlignmentProperty, TextAlignment.End);
			return bindable;
		}

		/// <summary>
		/// VerticalTextAlignment = HorizontalTextAlignment = TextAlignment.Center
		/// </summary>
		/// <typeparam name="TBindable"></typeparam>
		/// <param name="bindable"></param>
		/// <returns></returns>
		public static TBindable TextCenter<TBindable>(this TBindable bindable) where TBindable : BindableObject, ITextAlignment
			=> bindable.TextCenterHorizontal().TextCenterVertical();
	}


	// The extensions in these sub-namespaces are designed to be used together with the extensions in the parent namespace.
	// Keep them in a single file for better maintainability
	namespace LeftToRight
	{
		/// <summary>
		/// Extension methods for Labels
		/// </summary>
		public static class LabelExtensions
		{
			/// <summary>
			/// HorizontalTextAlignment = TextAlignment.Start
			/// </summary>
			/// <typeparam name="TBindable"></typeparam>
			/// <param name="bindable"></param>
			/// <returns>Label with TextAlignment.Start</returns>
			public static TBindable TextLeft<TBindable>(this TBindable bindable) where TBindable : BindableObject, ITextAlignment
			{
				bindable.SetValue(TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.Start);
				return bindable;
			}

			/// <summary>
			/// HorizontalTextAlignment = TextAlignment.End
			/// </summary>
			/// <typeparam name="TBindable"></typeparam>
			/// <param name="bindable"></param>
			/// <returns>Label with TextAligment.End</returns>
			public static TBindable TextRight<TBindable>(this TBindable bindable) where TBindable : BindableObject, ITextAlignment
			{
				bindable.SetValue(TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.End);
				return bindable;
			}
		}
	}

	namespace RightToLeft
	{
		/// <summary>
		/// Extension methods for Label
		/// </summary>
		public static class LabelExtensions
		{
			/// <summary>
			/// HorizontalTextAlignment = TextAlignment.End
			/// </summary>
			/// <typeparam name="TBindable"></typeparam>
			/// <param name="bindable"></param>
			/// <returns>Label with TextAlignment.End</returns>
			public static TBindable TextLeft<TBindable>(this TBindable bindable) where TBindable : BindableObject, ITextAlignment
			{
				bindable.SetValue(TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.End);
				return bindable;
			}

			/// <summary>
			/// HorizontalTextAlignment = TextAlignment.Start
			/// </summary>
			/// <typeparam name="TBindable"></typeparam>
			/// <param name="bindable"></param>
			/// <returns>Label with TextAlignment.Start</returns>
			public static TBindable TextRight<TBindable>(this TBindable bindable) where TBindable : BindableObject, ITextAlignment
			{
				bindable.SetValue(TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.Start);
				return bindable;
			}
		}
	}
}