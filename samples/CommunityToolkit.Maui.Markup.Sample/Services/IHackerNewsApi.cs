using Refit;

namespace CommunityToolkit.Maui.Markup.Sample.Services;

interface IHackerNewsApi
{
	[Get("/topstories.json?print=pretty")]
	Task<IReadOnlyList<long>> GetTopStoryIDs();

	[Get("/item/{storyId}.json?print=pretty")]
	Task<StoryModel> GetStory(long storyId);
}