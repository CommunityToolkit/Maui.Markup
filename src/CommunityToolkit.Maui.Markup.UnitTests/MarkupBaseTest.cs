using System;
using Microsoft.Maui.Controls;
using Xunit;

namespace CommunityToolkit.Maui.Markup.UnitTests
{
    public abstract class MarkupBaseTest<TBindable> : MarkupBaseTest where TBindable : BindableObject, new()
    {
        protected TBindable? Bindable { get; private set; } = new TBindable();

        protected override void Dispose(bool isDisposing)
        {
            Bindable = null;
            base.Dispose(isDisposing);
        }

        protected void TestPropertiesSet<TPropertyValue>(Action<TBindable?> modify, params (BindableProperty property, TPropertyValue beforeValue, TPropertyValue expectedValue)[] propertyChanges) =>
            TestPropertiesSet(Bindable, modify, propertyChanges);

        protected void TestPropertiesSet(Action<TBindable?> modify, params (BindableProperty property, object beforeValue, object expectedValue)[] propertyChanges) =>
            TestPropertiesSet(Bindable, modify, propertyChanges);

        protected void TestPropertiesSet(Action<TBindable?> modify, params (BindableProperty property, object expectedValue)[] propertyChanges) =>
            TestPropertiesSet(Bindable, modify, propertyChanges);
    }

    public abstract class MarkupBaseTest : BaseTest
    {
        protected static void TestPropertiesSet<TBindable, TPropertyValue>(
            TBindable? bindable,
            Action<TBindable?> modify,
            params (BindableProperty property, TPropertyValue beforeValue, TPropertyValue expectedValue)[] propertyChanges) where TBindable : BindableObject
        {
            foreach (var (property, beforeValue, expectedValue) in propertyChanges)
            {
                bindable?.SetValue(property, beforeValue);
                Assert.NotEqual(bindable.GetPropertyIfSet(property, expectedValue), expectedValue);
            }

            modify(bindable);

            foreach (var (property, beforeValue, expectedValue) in propertyChanges)
                Assert.Equal(bindable.GetPropertyIfSet(property, beforeValue), expectedValue);
        }

        protected static void TestPropertiesSet<TBindable, TPropertyValue>(
            TBindable? bindable,
            Action<TBindable?> modify,
            params (BindableProperty property, TPropertyValue expectedValue)[] propertyChanges) where TBindable : BindableObject
        {
            foreach (var (property, expectedValue) in propertyChanges)
            {
                bindable?.SetValue(property, property.DefaultValue);
                Assert.NotEqual(bindable.GetPropertyIfSet(property, expectedValue), expectedValue);
            }

            modify(bindable);

            foreach (var (property, expectedValue) in propertyChanges)
                Assert.Equal(bindable.GetPropertyIfSet(property, property.DefaultValue), expectedValue);
        }
    }
}
