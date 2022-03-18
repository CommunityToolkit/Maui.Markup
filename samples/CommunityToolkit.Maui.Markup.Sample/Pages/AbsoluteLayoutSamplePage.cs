using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;

namespace CommunityToolkit.Maui.Markup.Sample.Pages;

public class AbsoluteLayoutSamplePage : ContentPage
{
	public AbsoluteLayoutSamplePage()
	{
		Content = new AbsoluteLayout
		{
			Children =
				{
					new BoxView
					{
						Color = Colors.Blue,
					}.LayoutFlags(AbsoluteLayoutFlags.PositionProportional)
					.LayoutBounds(0.5,0,100,25),

					new BoxView
					{
						Color = Colors.Green,
						WidthRequest = 25,
						HeightRequest = 100,
					}.LayoutFlags(AbsoluteLayoutFlags.PositionProportional)
					.LayoutBounds(0,0.5),

					new BoxView
					{
						Color = Colors.Red,
						WidthRequest = 25,
						HeightRequest = 100,
					}.LayoutFlags(AbsoluteLayoutFlags.PositionProportional)
					.LayoutBounds(new Point(1,0.5)),

					new BoxView
					{
						Color = Colors.Grey,
					}.LayoutFlags(AbsoluteLayoutFlags.PositionProportional)
					.LayoutBounds(new Point(0.5,1), new Size(100,25)),

					new BoxView
					{
						Color = Colors.Tan,
					}.LayoutFlags(AbsoluteLayoutFlags.All)
					.LayoutBounds(new Rect(0.5,0.5,1d/3d, 1d/3d))

				}
		};
	}
}
