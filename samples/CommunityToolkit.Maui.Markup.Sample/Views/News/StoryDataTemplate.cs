using CommunityToolkit.Maui.Markup.Sample.Constants;
using CommunityToolkit.Maui.Markup.Sample.Models;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
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
			new Label().Row(Row.Title)
				.Font(size: 16).TextColor(ColorConstants.PrimaryTextColor)
				.Top().Padding(10, 0)
				.Bind(Label.TextProperty, nameof(StoryModel.Title)),

			new Label().Row(Row.Description)
				.Font(size: 13).TextColor(ColorConstants.SecondaryTextColor)
				.Paddings(10, 0, 10, 5)
				.Bind(Label.TextProperty, nameof(StoryModel.Description))
		}
	};

	enum Row { Title, Description, BottomPadding }
}