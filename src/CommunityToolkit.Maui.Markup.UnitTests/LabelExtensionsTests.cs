using CommunityToolkit.Maui.Markup.UnitTests.Base;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests;

class LabelExtensionsTests : BaseMarkupTestFixture<Label>
{
	[Test]
	public void FormattedTextSingleSpan()
	{
		Bindable.FormattedText = null;
		Bindable.FormattedText(
			new Span { BackgroundColor = Colors.Blue }
		);

		var spans = Bindable.FormattedText?.Spans;
		Assert.That(spans?.Count == 1 && spans[0].BackgroundColor == Colors.Blue);
	}

	[Test]
	public void FormattedTextMultipleSpans()
	{
		Bindable.FormattedText = null;
		Bindable.FormattedText(
			new Span { BackgroundColor = Colors.Blue },
			new Span { BackgroundColor = Colors.Green }
		);

		var spans = Bindable.FormattedText?.Spans;
		Assert.That(spans?.Count == 2 && spans[0].BackgroundColor == Colors.Blue && spans[1].BackgroundColor == Colors.Green);
	}

	[Test]
	public void SupportDerivedFromBindable()
	{
		Assert.That(new DerivedFromLabel()
					.TextStart()
					.TextCenterHorizontal()
					.TextEnd()
					.TextTop()
					.TextCenterVertical()
					.TextBottom()
					.TextCenter()
					.FontSize(8.0)
					.Bold()
					.Italic()
					.FormattedText(),
					Is.InstanceOf<DerivedFromLabel>());
	}

	class DerivedFromLabel : Label { }
}