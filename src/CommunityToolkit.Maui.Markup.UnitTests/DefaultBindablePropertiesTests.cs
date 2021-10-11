﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace CommunityToolkit.Maui.Markup.UnitTests
{
    // These usings are placed here to avoid ambiguities
    using Microsoft.Maui.Controls;
    using CommunityToolkit.Maui.Markup.UnitTests.DefaultBindablePropertiesViews;
    using Microsoft.Maui.Controls.Shapes;

    public class DefaultBindablePropertiesTests : MarkupBaseTest
    {
        [Fact]
        public void AllBindableElementsInCoreHaveDefaultBindablePropertyOrAreExcluded()
        {
            const string na = "not applicable", tbd = "to be determined";
            IReadOnlyDictionary<Type, string> excludedTypeReasons = new Dictionary<Type, string>
            { // Key: type, Value: reason why it does not have a default bindable property
				{ typeof(Application), na },
                { typeof(AdaptiveTrigger), na },
                { typeof(BaseMenuItem), na },
                { typeof(BaseShellItem), na },
                { typeof(Behavior), na },
                { typeof(BindableObject), na },
                { typeof(CarouselView), na },
                { typeof(Cell), na },
                { typeof(ColumnDefinition), na },
                { typeof(CompareStateTrigger), na },
                { typeof(DataTrigger), na },
                { typeof(DeviceStateTrigger), na },
                { typeof(DragGestureRecognizer), na },
                { typeof(DropGestureRecognizer), na },
                { typeof(Element), na },
                { typeof(EventTrigger), na },
                { typeof(FontImageSource), na },
                { typeof(FormattedString), na },
                { typeof(GestureElement), na },
                { typeof(GestureRecognizer), na },
                { typeof(GradientStop), na },
                { typeof(GridItemsLayout), na },
                { typeof(GroupableItemsView), na },
                { typeof(HorizontalStackLayout), na },
                { typeof(ImageSource), na },
                { typeof(InputView), na },
                { typeof(ItemsLayout), na },
                { typeof(LinearItemsLayout), na },
                { typeof(LinearGradientBrush), na },
                { typeof(Menu), na },
                { typeof(MultiTrigger), na },
                { typeof(NavigableElement), na },
                { typeof(OpenGLView), na },
                { typeof(OrientationStateTrigger), na },
                { typeof(PanGestureRecognizer), na },
                { typeof(PinchGestureRecognizer), na },
                { typeof(RadialGradientBrush), na },
                { typeof(RoundRectangleGeometry), na },
                { typeof(RowDefinition), na },
                { typeof(SelectableItemsView), na },
                { typeof(StateTrigger), na },
                { typeof(StateTriggerBase), na },
                { typeof(StructuredItemsView), na },
                { typeof(SwipeItems), na },
                { typeof(TableRoot), na },
                { typeof(TableSection), na },
                { typeof(TableView), na },
                { typeof(Trigger), na },
                { typeof(TriggerBase), na },
                { typeof(VerticalStackLayout), na },
                { typeof(View), na },
                { typeof(ViewCell), na },
                { typeof(VisualElement), na },
                { typeof(WebViewSource), na },
                { typeof(AppLinkEntry), tbd },
                { typeof(FlyoutItem), tbd },
                { typeof(Shell), tbd },
                { typeof(ShellContent), tbd },
                { typeof(ShellGroupItem), tbd },
                { typeof(ShellItem), tbd },
                { typeof(ShellSection), tbd },
                { typeof(Tab), tbd },
                { typeof(TabBar), tbd },
                { typeof(ArcSegment), tbd },
                { typeof(BezierSegment), tbd },
                { typeof(CompositeTransform), tbd },
                { typeof(EllipseGeometry), tbd },
                { typeof(Geometry), tbd },
                { typeof(GeometryGroup), tbd },
                { typeof(LineGeometry), tbd },
                { typeof(LineSegment), tbd },
                { typeof(MatrixTransform), tbd },
                { typeof(Path), tbd },
                { typeof(PathFigure), tbd },
                { typeof(PathGeometry), tbd },
                { typeof(PathSegment), tbd },
                { typeof(PolyBezierSegment), tbd },
                { typeof(PolyLineSegment), tbd },
                { typeof(PolyQuadraticBezierSegment), tbd },
                { typeof(QuadraticBezierSegment), tbd },
                { typeof(RectangleGeometry), tbd },
                { typeof(RotateTransform), tbd },
                { typeof(ScaleTransform), tbd },
                { typeof(SkewTransform), tbd },
                { typeof(Shape), tbd },
                { typeof(Transform), tbd },
                { typeof(TransformGroup), tbd },
                { typeof(TranslateTransform), tbd },
                { typeof(Ellipse), tbd },
                { typeof(Line), tbd },
                { typeof(Polygon), tbd },
                { typeof(Polyline), tbd },
                { typeof(Rectangle), tbd },
            };

            var failMessage = new StringBuilder();
            var bindableObjectTypes = typeof(BindableObject).Assembly.GetExportedTypes()
                .Where(t => typeof(BindableObject).IsAssignableFrom(t) && !t.IsAbstract && !typeof(Layout).IsAssignableFrom(t) && !t.ContainsGenericParameters);

            // The logical default property for a Layout is for its child view(s), which is not a bindable property.
            // So we exclude Layouts from this test. Note that it is still perfectly OK to define a default
            // bindable property for a Layout where that makes sense.
            // We also do not support specifying default properties for unconstructed generic types.

            foreach (var type in bindableObjectTypes)
            {
                if (excludedTypeReasons.TryGetValue(type, out var exclusionReason))
                {
                    Console.WriteLine($"Info: no default BindableProperty defined for BindableObject type {type.FullName} because {exclusionReason}");
                    continue;
                }

                if (DefaultBindableProperties.GetFor(type) == null)
                {
                    failMessage.AppendLine(type.FullName);
                    var propertyNames = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                                        .Where(f => f.FieldType == typeof(BindableProperty)).Select(f => f?.DeclaringType?.Name + "." + f?.Name).ToList();

                    if (propertyNames.Count > 0)
                    {
                        failMessage.AppendLine("\tCandidate properties:");
                        foreach (var propertyName in propertyNames)
                            failMessage.Append('\t').AppendLine(propertyName);
                    }
                }
            }

            if (failMessage.Length > 0)
            {
                throw new Exception(
                    $"Missing default BindableProperty / exclusion for BindableObject types:\n{failMessage}\n" +
                    $"Either register these types in {typeof(DefaultBindableProperties).FullName} or exclude them in this test");
            }
        }

        [Fact]
        public void GetDefaultBindablePropertyForBuiltInType() => Assert.NotNull(DefaultBindableProperties.GetFor(new Label()));

        [Fact]
        public void GetDefaultBindablePropertyForDerivedType() => Assert.NotNull(DefaultBindableProperties.GetFor(new DerivedFromBoxView()));

        [Fact]
        //  "No default bindable property is registered for BindableObject type XamarinFormsMarkupUnitTestsDefaultBindablePropertiesViews.CustomView"
        //  Either specify a property when calling Bind() or register a default bindable property for this BindableObject type");
        public void GetDefaultBindablePropertyForUnsupportedType() => Assert.Throws<ArgumentException>(() => DefaultBindableProperties.GetFor(new CustomView()));

        [Fact]
        public void RegisterDefaultBindableProperty()
        {
            var v = new CustomViewWithText();
            Assert.Throws<ArgumentException>(() => DefaultBindableProperties.GetFor(v));

            DefaultBindableProperties.Register(CustomViewWithText.TextProperty);
        }

        [Fact]
        public void GetDefaultBindableCommandPropertiesForBuiltInType()
        {
            var (commandProperty, commandParameterProperty) = DefaultBindableProperties.GetForCommand(new Button());

            Assert.NotNull(commandProperty);
            Assert.NotNull(commandParameterProperty);
        }

        [Fact]
        public void GetDefaultBindableCommandPropertiesForDerivedType() => Assert.NotNull(DefaultBindableProperties.GetFor(new DerivedFromButton()));

        [Fact]
        //  "No command + command parameter properties are registered for BindableObject type XamarinFormsMarkupUnitTestsDefaultBindablePropertiesViews.CustomView"
        //  Register command + command parameter properties for this BindableObject type");
        public void GetDefaultBindableCommandPropertiesForUnsupportedType() => Assert.Throws<ArgumentException>(() => DefaultBindableProperties.GetFor(new CustomView()));

        [Fact]
        public void RegisterDefaultBindableCommandProperties()
        {
            var v = new CustomViewWithCommand();
            Assert.Throws<ArgumentException>(() => DefaultBindableProperties.GetForCommand(v));

            DefaultBindableProperties.RegisterForCommand((CustomViewWithCommand.CommandProperty, CustomViewWithCommand.CommandParameterProperty));
        }

        protected override void Dispose(bool isDisposing)
        {
            if (DefaultBindableProperties.GetFor(typeof(CustomViewWithText)) != null)
                DefaultBindableProperties.Unregister(CustomViewWithText.TextProperty);

            if (DefaultBindableProperties.GetForCommand(typeof(CustomViewWithCommand)) != (null, null))
                DefaultBindableProperties.UnregisterForCommand(CustomViewWithCommand.CommandProperty);

            base.Dispose(isDisposing);
        }
    }
}

namespace CommunityToolkit.Maui.Markup.UnitTests.DefaultBindablePropertiesViews // This namespace simulates derived controls defined in a separate app, for use in the tests in this file only
{
    // These usings are placed here to avoid ambiguities
    using System.Windows.Input;
    using Microsoft.Maui.Controls;

    class DerivedFromBoxView : BoxView
    {
    }

    class DerivedFromButton : Button
    {
    }

    class CustomView : View
    {
    }

    class CustomViewWithText : View
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomViewWithText), string.Empty);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
    }

    class CustomViewWithCommand : View
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(CustomViewWithCommand));
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CustomViewWithCommand));

        public ICommand? Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object? CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
    }
}