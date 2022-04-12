using Microsoft.Maui.Layouts;
using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using CommunityToolkit.Maui.Markup.Sample.Pages.Base;
using CommunityToolkit.Maui.Markup.Sample.ViewModels;

namespace CommunityToolkit.Maui.Markup.Sample.Pages;

class NewsDetailPage : BaseContentPage<NewsDetailViewModel>
{
	public NewsDetailPage(NewsDetailViewModel newsDetailViewModel) : base(newsDetailViewModel, "Details")
	{
		Content = new FlexLayout
		{
			Direction = FlexDirection.Column,
			AlignContent = FlexAlignContent.Center,

			Children =
			{
				new Label { BackgroundColor = Colors.Aqua }
					.Text("HEADER").Font(size: 25).TextCenterHorizontal()
					.AlignSelf(FlexAlignSelf.Stretch),

				new FlexLayout()
				{
					Children =
					{
						new Label { BackgroundColor = Colors.Grey }
							.Text("Content").Font(size: 20)
							.Grow(1)
							.TextCenterHorizontal()
							.AlignSelf(FlexAlignSelf.Stretch),

						new BoxView { Color = Colors.Blue }
							.Basis(50)
							.Order(-1),

						new BoxView { Color = Colors.Green }
							.Basis(50)
					}

				}.Grow(1)
				.AlignSelf(FlexAlignSelf.Stretch),

				new Label { BackgroundColor = Colors.Pink }
					.Text("FOOTER").Font(size: 20)
					.TextCenterHorizontal()
					.AlignSelf(FlexAlignSelf.Stretch)
			}
		};
	}
}

