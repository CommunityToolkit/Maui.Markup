using Polly;

namespace CommunityToolkit.Maui.Markup.Sample.Services;

class HackerNewsAPIService
{
	readonly IHackerNewsApi hackerNewsClient;

	public HackerNewsAPIService(IHackerNewsApi hackerNewslient) => hackerNewsClient = hackerNewslient;

	public Task<StoryModel> GetStory(long storyId) => hackerNewsClient.GetStory(storyId);
	public Task<IReadOnlyList<long>> GetTopStoryIDs() => hackerNewsClient.GetTopStoryIDs();
}