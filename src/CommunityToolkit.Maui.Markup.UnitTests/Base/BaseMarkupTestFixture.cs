using System;
using Microsoft.Maui.Controls;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests.Base;

abstract class BaseMarkupTestFixture<TBindable> : BaseMarkupTestFixture where TBindable : BindableObject, new()
{
	TBindable? _bindable;

	protected TBindable Bindable => _bindable ?? throw new InvalidOperationException($"{nameof(Bindable)} is initialized in the {nameof(Setup)} method, marked by the {nameof(SetUpAttribute)}");

	[SetUp]
	public override void Setup()
	{
		base.Setup();
		_bindable = new TBindable();
	}

	[TearDown]
	public override void TearDown()
	{
		_bindable = null;
		base.TearDown();
	}

	protected void TestPropertiesSet<TPropertyValue>(
		Action<TBindable?> modify,
		params (BindableProperty property, TPropertyValue beforeValue, TPropertyValue expectedValue)[] propertyChanges)
		=> TestPropertiesSet(Bindable, modify, propertyChanges);

	protected void TestPropertiesSet(
		Action<TBindable?> modify,
		params (BindableProperty property, object beforeValue, object expectedValue)[] propertyChanges)
		=> TestPropertiesSet(Bindable, modify, propertyChanges);

	protected void TestPropertiesSet(
		Action<TBindable?> modify,
		params (BindableProperty property, object expectedValue)[] propertyChanges)
		=> TestPropertiesSet(Bindable, modify, propertyChanges);
}

abstract class BaseMarkupTestFixture : BaseTestFixture
{
	protected static void TestPropertiesSet<TBindable, TPropertyValue>(
		TBindable bindable,
		Action<TBindable> modify,
		params (BindableProperty property, TPropertyValue beforeValue, TPropertyValue expectedValue)[] propertyChanges) where TBindable : BindableObject
	{
		foreach (var (property, beforeValue, expectedValue) in propertyChanges)
		{
			bindable.SetValue(property, beforeValue);
			Assume.That(bindable.GetPropertyIfSet(property, expectedValue), Is.Not.EqualTo(expectedValue));
		}

		modify(bindable);

		foreach (var (property, beforeValue, expectedValue) in propertyChanges)
			Assert.That(bindable.GetPropertyIfSet(property, beforeValue), Is.EqualTo(expectedValue));
	}

	protected static void TestPropertiesSet<TBindable, TPropertyValue>(
		TBindable bindable,
		Action<TBindable> modify,
		params (BindableProperty property, TPropertyValue expectedValue)[] propertyChanges) where TBindable : BindableObject
	{
		foreach (var (property, expectedValue) in propertyChanges)
		{
			bindable.SetValue(property, property.DefaultValue);
			Assume.That(bindable.GetPropertyIfSet(property, expectedValue), Is.Not.EqualTo(expectedValue));
		}

		modify(bindable);

		foreach (var (property, expectedValue) in propertyChanges)
			Assert.That(bindable.GetPropertyIfSet(property, property.DefaultValue), Is.EqualTo(expectedValue));
	}
}