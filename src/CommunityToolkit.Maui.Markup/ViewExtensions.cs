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
        /// HorizontalOptions = LayoutOptions.StartAndExpand
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <param name="view"></param>
        /// <returns>View with LayoutOptions.StartAndExpand</returns>
        public static TView StartExpand<TView>(this TView view) where TView : View
        {
            view.HorizontalOptions = LayoutOptions.StartAndExpand;
            return view;
        }

        /// <summary>
        /// HorizontalOptions = LayoutOptions.CenterAndExpand
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <param name="view"></param>
        /// <returns>View with LayoutOptions.CenterAndExpand</returns>
        public static TView CenterExpandHorizontal<TView>(this TView view) where TView : View
        {
            view.HorizontalOptions = LayoutOptions.CenterAndExpand;
            return view;
        }

        /// <summary>
        /// HorizontalOptions = LayoutOptions.FillAndExpand
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <param name="view"></param>
        /// <returns>View with LayoutOptions.FillAndExpand</returns>
        public static TView FillExpandHorizontal<TView>(this TView view) where TView : View
        {
            view.HorizontalOptions = LayoutOptions.FillAndExpand;
            return view;
        }

        /// <summary>
        /// HorizontalOptions = LayoutOptions.EndAndExpand
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <param name="view"></param>
        /// <returns>View with LayoutOptions.EndAndExpand</returns>
        public static TView EndExpand<TView>(this TView view) where TView : View
        {
            view.HorizontalOptions = LayoutOptions.EndAndExpand;
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
        /// VerticalOptions = LayoutOptions.StartAndExpand
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <param name="view"></param>
        /// <returns>View with LayoutOptions.StartAndExpand</returns>
        public static TView TopExpand<TView>(this TView view) where TView : View
        {
            view.VerticalOptions = LayoutOptions.StartAndExpand;
            return view;
        }

        /// <summary>
        /// VerticalOptions = LayoutOptions.EndAndExpand
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <param name="view"></param>
        /// <returns>View with LayoutOptions.EndAndExpand</returns>
        public static TView BottomExpand<TView>(this TView view) where TView : View
        {
            view.VerticalOptions = LayoutOptions.EndAndExpand;
            return view;
        }

        /// <summary>
        /// VerticalOptions = LayoutOptions.CenterAndExpand
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <param name="view"></param>
        /// <returns>View with LayoutOptions.CenterAndExpand</returns>
        public static TView CenterExpandVertical<TView>(this TView view) where TView : View
        {
            view.VerticalOptions = LayoutOptions.CenterAndExpand;
            return view;
        }

        /// <summary>
        /// VerticalOptions = LayoutOptions.FillAndExpand
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <param name="view"></param>
        /// <returns>View with LayoutOptions.FillAndExpand</returns>
        public static TView FillExpandVertical<TView>(this TView view) where TView : View
        {
            view.VerticalOptions = LayoutOptions.FillAndExpand;
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
        /// VerticalOptions = HorizontalOptions = LayoutOptions.CenterAndExpand
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <param name="view"></param>
        /// <returns>View with LayoutOptions.CenterAndExpand</returns>
        public static TView CenterExpand<TView>(this TView view) where TView : View
            => view.CenterExpandHorizontal().CenterExpandVertical();

        /// <summary>
        /// VerticalOptions = HorizontalOptions = LayoutOptions.FillAndExpand
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <param name="view"></param>
        /// <returns>View with LayoutOptions.FillAndExpand</returns>
        public static TView FillExpand<TView>(this TView view) where TView : View
            => view.FillExpandHorizontal().FillExpandVertical();

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

            /// <summary>
            /// HorizontalOptions = LayoutOptions.StartAndExpand
            /// </summary>
            /// <typeparam name="TView"></typeparam>
            /// <param name="view"></param>
            /// <returns>View with LayoutOptions.StartAndExpand</returns>
            public static TView LeftExpand<TView>(this TView view) where TView : View
            {
                view.HorizontalOptions = LayoutOptions.StartAndExpand;
                return view;
            }

            /// <summary>
            /// HorizontalOptions = LayoutOptions.EndAndExpand
            /// </summary>
            /// <typeparam name="TView"></typeparam>
            /// <param name="view"></param>
            /// <returns>View with LayoutOptions.EndAndExpand</returns>
            public static TView RightExpand<TView>(this TView view) where TView : View
            {
                view.HorizontalOptions = LayoutOptions.EndAndExpand;
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

            /// <summary>
            /// HorizontalOptions = LayoutOptions.EndAndExpand
            /// </summary>
            /// <typeparam name="TView"></typeparam>
            /// <param name="view"></param>
            /// <returns>View with LayoutOptions.EndAndExpand</returns>
            public static TView LeftExpand<TView>(this TView view) where TView : View
            {
                view.HorizontalOptions = LayoutOptions.EndAndExpand;
                return view;
            }

            /// <summary>
            /// HorizontalOptions = LayoutOptions.StartAndExpand
            /// </summary>
            /// <typeparam name="TView"></typeparam>
            /// <param name="view"></param>
            /// <returns>View with LayoutOptions.StartAndExpand</returns>
            public static TView RightExpand<TView>(this TView view) where TView : View
            {
                view.HorizontalOptions = LayoutOptions.StartAndExpand;
                return view;
            }
        }
    }
}