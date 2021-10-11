using System;
using Microsoft.Maui.Controls;
using Xunit;

namespace CommunityToolkit.Maui.Markup.UnitTests
{
    
    public class BindableLayoutExtensionsTests : MarkupBaseTest<StackLayout>
    {
        [Fact]
        public void EmptyView()
        {
            var view = new BoxView();
            TestPropertiesSet(l => l?.EmptyView(view), (BindableLayout.EmptyViewProperty, view));
        }

        [Fact]
        public void EmptyViewTemplate()
        {
            var template = new DataTemplate(() => new BoxView());
            TestPropertiesSet(l => l?.EmptyViewTemplate(template), (BindableLayout.EmptyViewTemplateProperty, template));
        }

        [Fact]
        public void EmptyViewTemplateFunction()
        {
            Func<object> loadTemplate = () => new BoxView();
            Bindable?.EmptyViewTemplate(loadTemplate);

            Assert.That(BindableLayout.GetEmptyViewTemplate(Bindable), Is.Not.Null);
        }

        [Fact]
        public void ItemsSource()
        {
            var source = new string[] { };
            TestPropertiesSet(l => l?.ItemsSource(source), (BindableLayout.ItemsSourceProperty, source));
        }

        [Fact]
        public void ItemTemplate()
        {
            var template = new DataTemplate(() => new BoxView());
            TestPropertiesSet(l => l?.ItemTemplate(template), (BindableLayout.ItemTemplateProperty, template));
        }

        [Fact]
        public void ItemTemplateFunction()
        {
            Func<object> loadTemplate = () => new BoxView();
            Bindable?.ItemTemplate(loadTemplate);

            Assert.That(BindableLayout.GetItemTemplate(Bindable), Is.Not.Null);
        }

        [Fact]
        public void ItemTemplateSelector()
        {
            var selector = new Selector();
            TestPropertiesSet(l => l?.ItemTemplateSelector(selector), (BindableLayout.ItemTemplateSelectorProperty, selector));
        }

        class Selector : DataTemplateSelector
        {
            protected override DataTemplate OnSelectTemplate(object item, BindableObject container) => new(() => new BoxView());
        }

        [Fact]
        public void SupportDerivedFromView()
        {
            _ = new DerivedFromView()
                .EmptyView(new BoxView())
                .EmptyViewTemplate(new DataTemplate(() => new BoxView()))
                .ItemsSource(Array.Empty<string>())
                .ItemTemplate(new DataTemplate(() => new BoxView()))
                .ItemTemplateSelector(new Selector());
        }

        class DerivedFromView : StackLayout
        {
        }
    }
}