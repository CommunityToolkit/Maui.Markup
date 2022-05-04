namespace CommunityToolkit.Maui.Markup.Sample.Models;

record StoryModel(long Id, string By, long Score, long Time, string Title, string Url)
{
	public string Description => ToString();

	public DateTimeOffset CreatedAt => UnixTimeStampToDateTimeOffset(Time);

	public override string ToString() => $"{Score} Points by {By}, {GetAgeOfStory(CreatedAt)} ago";

	static string GetAgeOfStory(in DateTimeOffset storyCreatedAt)
	{
		var timespanSinceStoryCreated = DateTimeOffset.UtcNow - storyCreatedAt;

		return timespanSinceStoryCreated switch
		{
			TimeSpan storyAge when storyAge < TimeSpan.FromHours(1) => $"{Math.Ceiling(timespanSinceStoryCreated.TotalMinutes)} minutes",

			TimeSpan storyAge when storyAge >= TimeSpan.FromHours(1) && storyAge < TimeSpan.FromHours(2) => $"{Math.Floor(timespanSinceStoryCreated.TotalHours)} hour",

			TimeSpan storyAge when storyAge >= TimeSpan.FromHours(2) && storyAge < TimeSpan.FromHours(24) => $"{Math.Floor(timespanSinceStoryCreated.TotalHours)} hours",

			TimeSpan storyAge when storyAge >= TimeSpan.FromHours(24) && storyAge < TimeSpan.FromHours(48) => $"{Math.Floor(timespanSinceStoryCreated.TotalDays)} day",

			TimeSpan storyAge when storyAge >= TimeSpan.FromHours(48) => $"{Math.Floor(timespanSinceStoryCreated.TotalDays)} days",

			_ => string.Empty,
		};
	}

	static DateTimeOffset UnixTimeStampToDateTimeOffset(in long unixTimeStamp)
	{
		var dateTimeOffset = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, default);
		return dateTimeOffset.AddSeconds(unixTimeStamp);
	}
}