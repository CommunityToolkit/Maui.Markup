using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace CommunityToolkit.Maui.Markup
{
	/// <summary>
	/// Extension Methods for Label
	/// </summary>
	public static class LabelExtensions
	{
		/// <summary>
		/// VerticalTextAlignment = TextAlignment.Start
		/// </summary>
		/// <typeparam name="TLabel"></typeparam>
		/// <param name="label"></param>
		/// <returns>Label with added TextAlignment.Start</returns>
		public static TLabel TextStart<TLabel>(this TLabel label) where TLabel : Label
		{
			label.HorizontalTextAlignment = TextAlignment.Start;
			return label;
		}

		/// <summary>
		/// VerticalTextAlignment = TextAlignment.Center
		/// </summary>
		/// <typeparam name="TLabel"></typeparam>
		/// <param name="label"></param>
		/// <returns>Label with added TextAlignment.Center</returns>
		public static TLabel TextCenterHorizontal<TLabel>(this TLabel label) where TLabel : Label
		{
			label.HorizontalTextAlignment = TextAlignment.Center;
			return label;
		}

		/// <summary>
		/// VerticalTextAlignment = TextAlignment.End
		/// </summary>
		/// <typeparam name="TLabel"></typeparam>
		/// <param name="label"></param>
		/// <returns>Label with added TextAlignment.End</returns>
		public static TLabel TextEnd<TLabel>(this TLabel label) where TLabel : Label
		{
			label.HorizontalTextAlignment = TextAlignment.End;
			return label;
		}

		/// <summary>
		/// VerticalTextAlignment = TextAlignment.Start
		/// </summary>
		/// <typeparam name="TLabel"></typeparam>
		/// <param name="label"></param>
		/// <returns>Label with added TextAlignment.Start</returns>
		public static TLabel TextTop<TLabel>(this TLabel label) where TLabel : Label
		{
			label.VerticalTextAlignment = TextAlignment.Start;
			return label;
		}

		/// <summary>
		/// VerticalTextAlignment = TextAlignment.Center
		/// </summary>
		/// <typeparam name="TLabel"></typeparam>
		/// <param name="label"></param>
		/// <returns>Label with added TextAlignment.Center</returns>
		public static TLabel TextCenterVertical<TLabel>(this TLabel label) where TLabel : Label
		{
			label.VerticalTextAlignment = TextAlignment.Center;
			return label;
		}

		/// <summary>
		/// VerticalTextAlignment = TextAlignment.End
		/// </summary>
		/// <typeparam name="TLabel"></typeparam>
		/// <param name="label"></param>
		/// <returns>Label with added TextAlignment.End</returns>
		public static TLabel TextBottom<TLabel>(this TLabel label) where TLabel : Label
		{
			label.VerticalTextAlignment = TextAlignment.End;
			return label;
		}

		/// <summary>
		/// VerticalTextAlignment = TextAlignment.Center + HorizontalTextAlignment = TextAlignment.Center
		/// </summary>
		/// <typeparam name="TLabel"></typeparam>
		/// <param name="label"></param>
		/// <returns></returns>
		public static TLabel TextCenter<TLabel>(this TLabel label) where TLabel : Label
			=> label.TextCenterHorizontal().TextCenterVertical();

		/// <summary>
		/// Sets Formatted Text
		/// </summary>
		/// <typeparam name="TLabel"></typeparam>
		/// <param name="label"></param>
		/// <param name="spans"></param>
		/// <returns>Label with added FormattedText</returns>
		public static TLabel FormattedText<TLabel>(this TLabel label, params Span[] spans) where TLabel : Label
		{
			label.FormattedText = new FormattedString();

			foreach (var span in spans)
				label.FormattedText.Spans.Add(span);

			return label;
		}
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
			/// <typeparam name="TLabel"></typeparam>
			/// <param name="label"></param>
			/// <returns>Label with TextAlignment.Start</returns>
			public static TLabel TextLeft<TLabel>(this TLabel label) where TLabel : Label
			{
				label.HorizontalTextAlignment = TextAlignment.Start;
				return label;
			}

			/// <summary>
			/// HorizontalTextAlignment = TextAlignment.End
			/// </summary>
			/// <typeparam name="TLabel"></typeparam>
			/// <param name="label"></param>
			/// <returns>Label with TextAligment.End</returns>
			public static TLabel TextRight<TLabel>(this TLabel label) where TLabel : Label
			{
				label.HorizontalTextAlignment = TextAlignment.End;
				return label;
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
			/// <typeparam name="TLabel"></typeparam>
			/// <param name="label"></param>
			/// <returns>Label with TextAlignment.End</returns>
			public static TLabel TextLeft<TLabel>(this TLabel label) where TLabel : Label
			{
				label.HorizontalTextAlignment = TextAlignment.End;
				return label;
			}

			/// <summary>
			/// HorizontalTextAlignment = TextAlignment.Start
			/// </summary>
			/// <typeparam name="TLabel"></typeparam>
			/// <param name="label"></param>
			/// <returns>Label with TextAlignment.Start</returns>
			public static TLabel TextRight<TLabel>(this TLabel label) where TLabel : Label
			{
				label.HorizontalTextAlignment = TextAlignment.Start;
				return label;
			}
		}
	}
}