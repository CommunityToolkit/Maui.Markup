using CommunityToolkit.Maui.Markup.Sample.Models;
using Polly;

namespace CommunityToolkit.Maui.Markup.Sample.Services;

class HackerNewsAPIService
{
	readonly IHackerNewsApi hackerNewsClient;

	public HackerNewsAPIService(IHackerNewsApi hackerNewslient) => hackerNewsClient = hackerNewslient;

	public Task<StoryModel> GetStory(long storyId) => AttemptAndRetry(() => hackerNewsClient.GetStory(storyId));
	public Task<IReadOnlyList<long>> GetTopStoryIDs() => AttemptAndRetry(() => hackerNewsClient.GetTopStoryIDs());

	static Task<T> AttemptAndRetry<T>(Func<Task<T>> action, int numRetries = 3)
	{
		return Policy.Handle<Exception>().WaitAndRetryAsync(numRetries, pollyRetryAttempt).ExecuteAsync(action);

		static TimeSpan pollyRetryAttempt(int attemptNumber) => TimeSpan.FromSeconds(Math.Pow(2, attemptNumber));
	}
}