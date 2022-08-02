
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

				new Button {  }
				.DynamicResource(Button.BackgroundColorProperty,nameof(BaseTheme.NavigationBarBackgroundColor))
					.Text("Launch in Browser \uf35d")
					.DynamicResource(Button.TextColorProperty,nameof(BaseTheme.PrimaryTextColor))
					.Font(size: 20, family: "FontAwesome")
					.Basis(50)
					.Bind(Button.CommandProperty, nameof(NewsDetailViewModel.OpenBrowserCommand), BindingMode.OneWay),

				new Label { }
				.DynamicResource(Label.BackgroundColorProperty,nameof(BaseTheme.NavigationBarBackgroundColor))
				.DynamicResource(Label.TextColorProperty,nameof(BaseTheme.PrimaryTextColor)).TextCenter()
					.TextCenter()
					.AlignSelf(FlexAlignSelf.Stretch)
					.Paddings(bottom: 20)
					.Bind(Label.TextProperty, nameof(NewsDetailViewModel.ScoreDescription), BindingMode.OneWay),
			}
		};
	}
}