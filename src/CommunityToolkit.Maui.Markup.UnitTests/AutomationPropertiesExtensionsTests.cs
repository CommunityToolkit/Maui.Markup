using CommunityToolkit.Maui.Markup.UnitTests.Base;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
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

		Assert.That(AutomationProperties.GetExcludedWithChildren(Bindable) == isExcluded);
	}

	[Test]
	public void SemanticHintShouldCorrectlyAssignValue()
	{
		const bool isInTree = false;
		Bindable.AutomationIsInAccessibleTree(isInTree);

		Assert.That(AutomationProperties.GetIsInAccessibleTree(Bindable) == isInTree);
	}
}
