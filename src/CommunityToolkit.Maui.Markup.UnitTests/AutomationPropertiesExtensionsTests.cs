using CommunityToolkit.Maui.Markup.UnitTests.Base;
using NUnit.Framework;
namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class AutomationPropertiesExtensionsTests : BaseMarkupTestFixture<Label>
{
	[Test]
	public void AutomationExcludedWithChildrenShouldCorrectlyAssignValue()
	{
		const bool isExcluded = true;
		Bindable.AutomationExcludedWithChildren(isExcluded);

		Assert.That(AutomationProperties.GetExcludedWithChildren(Bindable), Is.EqualTo(isExcluded));
	}

	[Test]
	public void SemanticHintShouldCorrectlyAssignValue()
	{
		const bool isInTree = false;
		Bindable.AutomationIsInAccessibleTree(isInTree);

		Assert.That(AutomationProperties.GetIsInAccessibleTree(Bindable), Is.EqualTo(isInTree));
	}
}