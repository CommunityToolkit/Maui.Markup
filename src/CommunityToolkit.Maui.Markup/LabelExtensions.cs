namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension Methods for Label
/// </summary>
public static class LabelExtensions
{
	/// <summary>
	/// Sets Formatted Text
	/// </summary>
	/// <typeparam name="TLabel"></typeparam>
	/// <param name="label"></param>
	/// <param name="spans"></param>
	/// <returns>Label with added FormattedText</returns>
	public static TLabel FormattedText<TLabel>(this TLabel label, params List<Span> spans) where TLabel : Label
	{
		label.FormattedText = new FormattedString();

		foreach (var span in spans)
		{
			label.FormattedText.Spans.Add(span);
		}

		return label;
	}
}