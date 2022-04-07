using Microsoft.Maui.Layouts;
using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace CommunityToolkit.Maui.Markup.Sample.Pages;

public class FlexLayoutSamplePage : ContentPage
{
	public FlexLayoutSamplePage()
	{
		Content = new FlexLayout
		{
			Direction = FlexDirection.Column,
			AlignContent = FlexAlignContent.Center,
			Children =
				{
					new Label
					{
						Text = "HEADER",
						FontSize = 25,
						BackgroundColor = Colors.Aqua,
					}.TextCenterHorizontal()
					.AlignSelf(FlexAlignSelf.Stretch),

					new FlexLayout()
					{
						Children =
						{
							new Label
							{
								Text = "Content",
								FontSize = 20,
								BackgroundColor = Colors.Grey
							}.Grow(1)
							.TextCenterHorizontal()
							.AlignSelf(FlexAlignSelf.Stretch),

							new BoxView
							{
								Color = Colors.Blue,
							}.Basis(50)
							.Order(-1),

							new BoxView
							{
								Color= Colors.Green,
							}.Basis(50)
						}

					}.Grow(1)
					.AlignSelf(FlexAlignSelf.Stretch),

					new Label
					{
						Text = "FOOTER",
						FontSize = 20,
						BackgroundColor = Colors.Pink,
					}.TextCenterHorizontal()
					.AlignSelf(FlexAlignSelf.Stretch)
				}
		};
	}
}

