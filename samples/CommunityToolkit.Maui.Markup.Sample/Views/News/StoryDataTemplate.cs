using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace CommunityToolkit.Maui.Markup.Sample.Views.News;

class StoryDataTemplate : DataTemplate
{
	public StoryDataTemplate() : base(CreateGrid)
	{

	}

	static Grid CreateGrid() => new()
	{
		RowSpacing = 1,

		RowDefinitions = Rows.Define(
			(Row.Title, 20),
			(Row.Description, 20),
			(Row.BottomPadding, 1)),

		Children =
		{
			new Label { LineBreakMode = LineBreakMode.TailTruncation, MaxLines = 1 }
				.Row(Row.Title)
				.Font(size: 16).AppThemeBinding(Label.TextColorProperty, AppStyles.BlackColor, AppStyles.PrimaryTextColorDark)
				.Top().Padding(10, 0)
				.Bind(Label.TextProperty, static (StoryModel m) => m.Title, mode: BindingMode.OneTime)
				.SemanticHint("The title of the news article."),

			new Label().Row(Row.Description)
				.Font(size: 13).AppThemeBinding(Label.TextColorProperty, AppStyles.SecondaryTextColorLight, AppStyles.SecondaryTextColorDark)
				.Paddings(10, 0, 10, 5)
				.Bind(Label.TextProperty, static (StoryModel m) => m.Description, mode: BindingMode.OneTime)
				.SemanticHint("The description of the news article.")
		}
	};

	enum Row { Title, Description, BottomPadding }
}