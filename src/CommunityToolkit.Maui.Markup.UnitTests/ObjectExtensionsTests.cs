using System;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using Microsoft.Maui.Controls;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class ObjectExtensionsTests : BaseMarkupTestFixture
{
	[Test]
	public void Assign()
	{
		var createdLabel = new Label().Assign(out Label assignLabel);
		Assert.That(ReferenceEquals(createdLabel, assignLabel));
	}

	[Test]
	public void Invoke()
	{
		var createdLabel = new Label().Invoke(l => l.Text = nameof(Invoke));
		Assert.That(createdLabel.Text, Is.EqualTo(nameof(Invoke)));
	}

	[Test]
	public void InvokeNullThrowsArgumentNullException()
	{
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
		Assert.Throws<ArgumentNullException>(() => new Label().Invoke(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
	}
}

