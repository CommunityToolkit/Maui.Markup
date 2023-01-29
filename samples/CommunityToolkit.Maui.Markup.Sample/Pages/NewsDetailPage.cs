using Microsoft.Maui.Layouts;

namespace CommunityToolkit.Maui.Markup.Sample.Pages;

sealed class NewsDetailPage : BaseContentPage<NewsDetailViewModel>
{
	public NewsDetailPage(NewsDetailViewModel newsDetailViewModel) : base(newsDetailViewModel, newsDetailViewModel.Title)
	{
		this.Bind(TitleProperty, static (NewsDetailViewModel vm) => vm.Title);

		Content = new FlexLayout
		{
			Direction = FlexDirection.Column,
			AlignContent = FlexAlignContent.Center,

			Children =
			{
				new WebView()
					.Grow(1).AlignSelf(FlexAlignSelf.Stretch)
					.Bind(WebView.SourceProperty, static (NewsDetailViewModel vm) => vm.Uri, mode: BindingMode.OneWay),

				new Button()
					.Text("Launch in Browser \uf35d")
					.Font(size: 20, family: "FontAwesome")
					.Basis(50)
					.Style(AppStyles.ButtonStyle)
					.Bind(Button.CommandProperty, static (NewsDetailViewModel vm) => vm.OpenBrowserCommand, mode: BindingMode.OneWay)
					.SemanticHint("Launches the news article in the devices browser."),

				new Label()
					.TextCenter()
					.AlignSelf(FlexAlignSelf.Stretch)
					.Paddings(bottom: 20)
					.Style(AppStyles.LabelStyle)
					.Bind(Label.TextProperty, static (NewsDetailViewModel vm) => vm.ScoreDescription, mode: BindingMode.OneWay)
					.SemanticHint("Displays the score of the news article."),
			}
		};
	}
}