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
		/// <see cref="View.HorizontalOptions"/> = <see cref="LayoutOptions.Start"/>
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <returns>TView with <see cref="LayoutOptions.Start"/></returns>
		public static TView Start<TView>(this TView view) where TView : View
		{
			view.HorizontalOptions = LayoutOptions.Start;
			return view;
		}

		/// <summary>
		/// <see cref="View.HorizontalOptions"/> = <see cref="LayoutOptions.Center"/>
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <returns>TView with <see cref="LayoutOptions.Center"/></returns>
		public static TView CenterHorizontal<TView>(this TView view) where TView : View
		{
			view.HorizontalOptions = LayoutOptions.Center;
			return view;
		}

		/// <summary>
		/// <see cref="View.HorizontalOptions"/> = <see cref="LayoutOptions.Fill"/>
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <returns>TView with <see cref="LayoutOptions.Fill"/></returns>
		public static TView FillHorizontal<TView>(this TView view) where TView : View
		{
			view.HorizontalOptions = LayoutOptions.Fill;
			return view;
		}

		/// <summary>
		/// <see cref="View.HorizontalOptions"/> = <see cref="LayoutOptions.End"/>
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <returns>TView with <see cref="LayoutOptions.End"/></returns>
		public static TView End<TView>(this TView view) where TView : View
		{
			view.HorizontalOptions = LayoutOptions.End;
			return view;
		}

		/// <summary>
		/// <see cref="View.VerticalOptions"/> = <see cref="LayoutOptions.Start"/>
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <returns>TView with <see cref="LayoutOptions.Start"/></returns>
		public static TView Top<TView>(this TView view) where TView : View
		{
			view.VerticalOptions = LayoutOptions.Start;
			return view;
		}

		/// <summary>
		/// <see cref="View.VerticalOptions"/> = <see cref="LayoutOptions.End"/>
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <returns>TView with <see cref="LayoutOptions.End"/></returns>
		public static TView Bottom<TView>(this TView view) where TView : View
		{
			view.VerticalOptions = LayoutOptions.End;
			return view;
		}

		/// <summary>
		/// <see cref="View.VerticalOptions"/> = <see cref="LayoutOptions.Center"/>
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <returns>TView with <see cref="LayoutOptions.Center"/></returns>
		public static TView CenterVertical<TView>(this TView view) where TView : View
		{
			view.VerticalOptions = LayoutOptions.Center;
			return view;
		}

		/// <summary>
		/// <see cref="View.VerticalOptions"/> = <see cref="LayoutOptions.Fill"/>
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <returns>TView with <see cref="LayoutOptions.Fill"/></returns>
		public static TView FillVertical<TView>(this TView view) where TView : View
		{
			view.VerticalOptions = LayoutOptions.Fill;
			return view;
		}

		/// <summary>
		/// <see cref="View.VerticalOptions"/> = <see cref="View.HorizontalOptions"/> = <see cref="LayoutOptions.Center"/>
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <returns>TView with <see cref="LayoutOptions.Center"/></returns>
		public static TView Center<TView>(this TView view) where TView : View
			=> view.CenterHorizontal().CenterVertical();

		/// <summary>
		/// <see cref="View.VerticalOptions"/> = <see cref="View.HorizontalOptions"/> = <see cref="LayoutOptions.Fill"/>
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <returns>TView with <see cref="LayoutOptions.Fill"/></returns>
		public static TView Fill<TView>(this TView view) where TView : View
			=> view.FillHorizontal().FillVertical();

		/// <summary>
		/// Set <see cref="View.Margin"/>
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <param name="margin"></param>
		/// <returns>TView with added <see cref="View.Margin"/></returns>
		public static TView Margin<TView>(this TView view, Thickness margin) where TView : View
		{
			view.Margin = margin;
			return view;
		}

		/// <summary>
		/// Set <see cref="View.Margin"/>
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <param name="horizontal"></param>
		/// <param name="vertical"></param>
		/// <returns>TView with added <see cref="View.Margin"/></returns>
		public static TView Margin<TView>(this TView view, double horizontal, double vertical) where TView : View
		{
			view.Margin = new Thickness(horizontal, vertical);
			return view;
		}

		/// <summary>
		/// Set <see cref="View.Margin"/>
		/// </summary>
		/// <typeparam name="TView"></typeparam>
		/// <param name="view"></param>
		/// <param name="left"></param>
		/// <param name="top"></param>
		/// <param name="right"></param>
		/// <param name="bottom"></param>
		/// <returns>TView with added <see cref="View.Margin"/></returns>
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
			/// <see cref="View.HorizontalOptions"/> = <see cref="LayoutOptions.Start"/>
			/// </summary>
			/// <typeparam name="TView"></typeparam>
			/// <param name="view"></param>
			/// <returns>TView with <see cref="LayoutOptions.Start"/></returns>
			public static TView Left<TView>(this TView view) where TView : View
			{
				view.HorizontalOptions = LayoutOptions.Start;
				return view;
			}

			/// <summary>
			/// <see cref="View.HorizontalOptions"/> = <see cref="LayoutOptions.End"/>
			/// </summary>
			/// <typeparam name="TView"></typeparam>
			/// <param name="view"></param>
			/// <returns>TView with <see cref="LayoutOptions.End"/></returns>
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
			/// <see cref="View.HorizontalOptions"/> = <see cref="LayoutOptions.End"/>
			/// </summary>
			/// <typeparam name="TView"></typeparam>
			/// <param name="view"></param>
			/// <returns>TView with <see cref="LayoutOptions.End"/></returns>
			public static TView Left<TView>(this TView view) where TView : View
			{
				view.HorizontalOptions = LayoutOptions.End;
				return view;
			}

			/// <summary>
			/// <see cref="View.HorizontalOptions"/> = <see cref="LayoutOptions.Start"/>
			/// </summary>
			/// <typeparam name="TView"></typeparam>
			/// <param name="view"></param>
			/// <returns>TView with <see cref="LayoutOptions.Start"/></returns>
			public static TView Right<TView>(this TView view) where TView : View
			{
				view.HorizontalOptions = LayoutOptions.Start;
				return view;
			}
		}
	}
}