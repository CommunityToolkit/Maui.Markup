using System;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using Microsoft.Maui.Controls;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class ObjectExtensionsTests : BaseMarkupTestFixture
{
	Label? nullableLabel;
	CustomLabel? nullableCustomLabel;

	[Test]
	public void AssignLabel()
	{
		var createdLabel = new Label().Assign<Label>(out Label assignedLabel).Assign(out var secondAssignedLabel).Assign<Label>(out nullableLabel);
		Assert.That(ReferenceEquals(createdLabel, assignedLabel));
		Assert.That(ReferenceEquals(secondAssignedLabel, assignedLabel));
		Assert.That(ReferenceEquals(nullableLabel, assignedLabel));
	}

	[Test]
	public void AssignCustomLabel()
	{
		CustomLabel customLabel = new();
		customLabel.Text = "Hello World";

		var createdLabel = new CustomLabel()
								.Assign(out Label assignedLabel)
								.Assign(out CustomLabel assignedCustomLabel)
								.Assign(out customLabel)
								.Assign(out nullableCustomLabel);

		nullableCustomLabel.Text = createdLabel.Text; // Ensure nullableCustomLabel is not null

		Assert.That(ReferenceEquals(createdLabel, assignedLabel));
		Assert.That(ReferenceEquals(customLabel, assignedLabel));
		Assert.That(ReferenceEquals(nullableCustomLabel, assignedLabel));
		Assert.That(ReferenceEquals(assignedCustomLabel, assignedLabel));
	}

	[Test]
	public void AssignString()
	{
		string? testString = null;
		const string text = "Hello World";

		var createdString = text.Invoke(_ => testString = text).Assign<string>(out string assignedString);

		Assert.NotNull(testString);
		Assert.AreEqual(text, testString);
		Assert.AreEqual(text, assignedString);
		Assert.AreEqual(text, createdString);
		Assert.That(ReferenceEquals(createdString, assignedString));
	}

	[Test]
	public void AssignNullThrowsArgumentNullException()
	{
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
		Assert.Throws<ArgumentNullException>(() => ObjectExtensions.Assign<Label>(null, out var label));
		Assert.Throws<ArgumentNullException>(() => ObjectExtensions.Assign<CustomLabel, Label>(null, out var label));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
	}

	[Test]
	public void Invoke()
	{
		const string text = nameof(Invoke);

		var createdLabel = new Label().Invoke(l => l.Text = text);
		Assert.That(createdLabel.Text, Is.EqualTo(text));
	}

	[Test]
	public void InvokeCustomEntry()
	{
		const string text = nameof(Invoke);

		var createdEntry = new CustomEntry().Invoke(l => l.Text = text);
		Assert.That(createdEntry.Text, Is.EqualTo(text));
	}

	[Test]
	public void InvokeNullThrowsArgumentNullException()
	{
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
		Assert.Throws<ArgumentNullException>(() => ObjectExtensions.Invoke<string>(null, text => text = "hello"));
		Assert.Throws<ArgumentNullException>(() => new Label().Invoke(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
	}

	class CustomLabel : Label
	{

	}

	class CustomEntry : Entry
	{

	}
}