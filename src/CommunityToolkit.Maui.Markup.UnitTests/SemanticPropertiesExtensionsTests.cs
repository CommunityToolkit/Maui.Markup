using CommunityToolkit.Maui.Markup.UnitTests.Base;
using NUnit.Framework;
namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class SemanticPropertiesExtensionsTests : BaseMarkupTestFixture<Label>
{
	[Test]
	public void SemanticDescriptionShouldCorrectlyAssignValue()
	{
		const string description = "This label does XYZ";
		Bindable.SemanticDescription(description);

		Assert.That(SemanticProperties.GetDescription(Bindable) == description);
	}

	[Test]
	public void SemanticHeadingLevelShouldCorrectlyAssignValue()
	{
		const SemanticHeadingLevel headingLevel = SemanticHeadingLevel.Level5;
		Bindable.SemanticHeadingLevel(headingLevel);

		Assert.That(SemanticProperties.GetHeadingLevel(Bindable) == headingLevel);
	}

	[Test]
	public void SemanticHintShouldCorrectlyAssignValue()
	{
		const string hint = "This label does XYZ";
		Bindable.SemanticHint(hint);

		Assert.That(SemanticProperties.GetHint(Bindable) == hint);
	}
}