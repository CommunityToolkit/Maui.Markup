using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace CommunityToolkit.Maui.Markup
{
	/// <summary>
	/// Extensions for View
	/// </summary>
	public static class ViewExtensions
	{
		/// <summary>
		/// HorizontalOptions = LayoutOptions.Start
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <returns>View with LayoutOptions.Start</returns>
		public static TView Start<TView>(this TView view) where TView : View
		{
			view.HorizontalOptions = LayoutOptions.Start;
			return view;
		}

		/// <summary>
		/// HorizontalOptions = LayoutOptions.Center
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <returns>View with LayoutOptions.Center</returns>
		public static TView CenterHorizontal<TView>(this TView view) where TView : View
		{
			view.HorizontalOptions = LayoutOptions.Center;
			return view;
		}

		/// <summary>
		/// HorizontalOptions = LayoutOptions.Fill
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <returns>View with LayoutOptions.Fill</returns>
		public static TView FillHorizontal<TView>(this TView view) where TView : View
		{
			view.HorizontalOptions = LayoutOptions.Fill;
			return view;
		}

		/// <summary>
		/// HorizontalOptions = LayoutOptions.End
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <returns>View with LayoutOptions.End</returns>
		public static TView End<TView>(this TView view) where TView : View
		{
			view.HorizontalOptions = LayoutOptions.End;
			return view;
		}

		/// <summary>
		/// VerticalOptions = LayoutOptions.Start
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <returns>View with LayoutOptions.Start</returns>
		public static TView Top<TView>(this TView view) where TView : View
		{
			view.VerticalOptions = LayoutOptions.Start;
			return view;
		}

		/// <summary>
		/// VerticalOptions = LayoutOptions.End
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <returns>View with LayoutOptions.End</returns>
		public static TView Bottom<TView>(this TView view) where TView : View
		{
			view.VerticalOptions = LayoutOptions.End;
			return view;
		}

		/// <summary>
		/// VerticalOptions = LayoutOptions.Center
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <returns>View with LayoutOptions.Center</returns>
		public static TView CenterVertical<TView>(this TView view) where TView : View
		{
			view.VerticalOptions = LayoutOptions.Center;
			return view;
		}

		/// <summary>
		/// VerticalOptions = LayoutOptions.Fill
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <returns>View with LayoutOptions.Fill</returns>
		public static TView FillVertical<TView>(this TView view) where TView : View
		{
			view.VerticalOptions = LayoutOptions.Fill;
			return view;
		}

		/// <summary>
		/// VerticalOptions = HorizontalOptions = LayoutOptions.Center
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <returns>View with LayoutOptions.Center</returns>
		public static TView Center<TView>(this TView view) where TView : View
			=> view.CenterHorizontal().CenterVertical();

		/// <summary>
		/// VerticalOptions = HorizontalOptions = LayoutOptions.Fill
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <returns>View with LayoutOptions.FillAndExpand</returns>
		public static TView Fill<TView>(this TView view) where TView : View
			=> view.FillHorizontal().FillVertical();

		/// <summary>
		/// Set Margin
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <param name="margin"></param>
		/// <returns>View with added margin</returns>
		public static TView Margin<TView>(this TView view, Thickness margin) where TView : View
		{
			view.Margin = margin;
			return view;
		}

		/// <summary>
		/// Set Margin
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <param name="horizontal"></param>
		/// <param name="vertical"></param>
		/// <returns>View with added margin</returns>
		public static TView Margin<TView>(this TView view, double horizontal, double vertical) where TView : View
		{
			view.Margin = new Thickness(horizontal, vertical);
			return view;
		}

		/// <summary>
		/// Set Margin
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <param name="left"></param>
		/// <param name="top"></param>
		/// <param name="right"></param>
		/// <param name="bottom"></param>
		/// <returns>View with added margin</returns>
		public static TView Margins<TView>(this TView view, double left = 0, double top = 0, double right = 0, double bottom = 0) where TView : View
		{
			view.Margin = new Thickness(left, top, right, bottom);
			return view;
		}
	}

	// The extensions in these sub-namespaces are designed to be used together with the extensions in the parent namespace.
	// Keep them in a single file for better maintainability
	namespace LeftToRight
	{
		/// <summary>
		/// View Extensions
		/// </summary>
		public static class ViewExtensions
		{
			/// <summary>
			/// HorizontalOptions = LayoutOptions.Start
			/// </summary>
			/// <typeparam name="TView"></typeparam>
			/// <param name="view"></param>
			/// <returns>View with LayoutOptions.Start</returns>
			public static TView Left<TView>(this TView view) where TView : View
			{
				view.HorizontalOptions = LayoutOptions.Start;
				return view;
			}

			/// <summary>
			/// HorizontalOptions = LayoutOptions.End
			/// </summary>
			/// <typeparam name="TView"></typeparam>
			/// <param name="view"></param>
			/// <returns>View with LayoutOptions.End</returns>
			public static TView Right<TView>(this TView view) where TView : View
			{
				view.HorizontalOptions = LayoutOptions.End;
				return view;
			}
		}
	}

	namespace RightToLeft
	{
		/// <summary>
		/// View Extensions
		/// </summary>
		public static class ViewExtensions
		{
			/// <summary>
			/// HorizontalOptions = LayoutOptions.End
			/// </summary>
			/// <typeparam name="TView"></typeparam>
			/// <param name="view"></param>
			/// <returns>View with LayoutOptions.End</returns>
			public static TView Left<TView>(this TView view) where TView : View
			{
				view.HorizontalOptions = LayoutOptions.End;
				return view;
			}

			/// <summary>
			/// HorizontalOptions = LayoutOptions.Start
			/// </summary>
			/// <typeparam name="TView"></typeparam>
			/// <param name="view"></param>
			/// <returns>View with LayoutOptions.Start</returns>
			public static TView Right<TView>(this TView view) where TView : View
			{
				view.HorizontalOptions = LayoutOptions.Start;
				return view;
			}
		}
	}
}