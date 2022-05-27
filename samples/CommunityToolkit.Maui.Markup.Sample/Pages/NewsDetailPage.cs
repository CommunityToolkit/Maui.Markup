using CommunityToolkit.Maui.Markup.Sample.Constants;
using CommunityToolkit.Maui.Markup.Sample.Pages.Base;
using CommunityToolkit.Maui.Markup.Sample.ViewModels;
using Microsoft.Maui.Layouts;

namespace CommunityToolkit.Maui.Markup.Sample.Pages;

class NewsDetailPage : BaseContentPage<NewsDetailViewModel>
{
	public NewsDetailPage(NewsDetailViewModel newsDetailViewModel) : base(newsDetailViewModel, newsDetailViewModel.Title)
	{
		this.Bind(TitleProperty, nameof(NewsDetailViewModel.Title));

		Content = new FlexLayout
		{
			Direction = FlexDirection.Column,
			AlignContent = FlexAlignContent.Center,

			Children =
			{
				new WebView()
					.Grow(1).AlignSelf(FlexAlignSelf.Stretch)
					.Bind(WebView.SourceProperty, nameof(NewsDetailViewModel.Uri), BindingMode.OneWay),

				new Button { BackgroundColor = ColorConstants.NavigationBarBackgroundColor }
					.Text("Launch in Browser \uf35d", ColorConstants.PrimaryTextColor)
					.Font(size: 20, family: "FontAwesome")
					.Basis(50)
					.Bind(Button.CommandProperty, nameof(NewsDetailViewModel.OpenBrowserCommand), BindingMode.OneWay),

				new Label { BackgroundColor = ColorConstants.NavigationBarBackgroundColor }
					.TextColor(ColorConstants.PrimaryTextColor).TextCenter()
					.AlignSelf(FlexAlignSelf.Stretch)
					.Paddings(bottom: 20)
					.Bind(Label.TextProperty, nameof(NewsDetailViewModel.ScoreDescription), BindingMode.OneWay),
			}
		};
	}
}