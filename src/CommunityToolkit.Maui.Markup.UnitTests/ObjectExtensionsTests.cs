﻿using System;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using Microsoft.Maui.Controls;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class ObjectExtensionsTests : BaseMarkupTestFixture
{
	[Test]
	public void AssignLabel()
	{
		var createdLabel = new Label().Assign(out Label assignedLabel);
		Assert.That(ReferenceEquals(createdLabel, assignedLabel));
	}

	[Test]
	public void AssignString()
	{
		string? testString = null;
		const string text = "Hello World";

		var createdString = text.Invoke(_ => testString = text).Assign(out string assignedString);

		Assert.NotNull(testString);
		Assert.AreEqual(text, testString);
		Assert.AreEqual(text, assignedString);
		Assert.AreEqual(text, createdString);
		Assert.That(ReferenceEquals(createdString, assignedString));
	}

	[Test]
	public void Invoke()
	{
		const string text = nameof(Invoke);

		var createdLabel = new Label().Invoke(l => l.Text = text);
		Assert.That(createdLabel.Text, Is.EqualTo(text));
	}

	[Test]
	public void InvokeNullThrowsArgumentNullException()
	{
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
		Assert.Throws<ArgumentNullException>(() => new Label().Invoke(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
	}
}

