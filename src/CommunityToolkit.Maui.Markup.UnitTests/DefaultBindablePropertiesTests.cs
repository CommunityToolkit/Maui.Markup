using CommunityToolkit.Maui.Markup.UnitTests.Base;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests
{
	using CommunityToolkit.Maui.Markup.UnitTests.DefaultBindablePropertiesViews;
	// These usings are placed here to avoid ambiguities
	using Microsoft.Maui.Controls;

	[TestFixture]
	class DefaultBindablePropertiesTests : BaseMarkupTestFixture
	{
		[Test]
		public void GetDefaultBindablePropertyForBuiltInType()
			=> Assert.That(DefaultBindableProperties.GetDefaultProperty<Label>(), Is.Not.Null);

		[Test]
		public void GetDefaultBindablePropertyForDerivedType()
			=> Assert.That(DefaultBindableProperties.GetDefaultProperty<DerivedFromBoxView>(), Is.Not.Null);

		[Test]
		public void GetDefaultBindablePropertyForUnsupportedType()
			=> Assert.Throws<ArgumentException>(
				() => DefaultBindableProperties.GetDefaultProperty<CustomView>(),
				"No default bindable property is registered for BindableObject type XamarinFormsMarkupUnitTestsDefaultBindablePropertiesViews.CustomView" +
				"\r\nEither specify a property when calling Bind() or register a default bindable property for this BindableObject type");

		[Test]
		public void RegisterDefaultBindableProperty()
		{
			Assert.Throws<ArgumentException>(() => DefaultBindableProperties.GetDefaultProperty<CustomViewWithText>());

			DefaultBindableProperties.Register(CustomViewWithText.TextProperty);
		}

		[Test]
		public void GetDefaultBindablePropertiesForBuiltInType()
			=> Assert.That(DefaultBindableProperties.GetDefaultProperty<Button>(), Is.Not.Null);

		[Test]
		public void GetDefaultBindablePropertiesForDerivedType()
			=> Assert.That(DefaultBindableProperties.GetDefaultProperty<DerivedFromButton>(), Is.Not.Null);

		[Test]
		public void GetDefaultBindablePropertiesForMauiDerivedType()
			=> Assert.That(DefaultBindableProperties.GetDefaultProperty<MenuFlyoutItem>(), Is.Not.Null);

		[Test]
		public void GetDefaultBindableCommandPropertiesForBuiltInType()
			=> Assert.That(DefaultBindableProperties.GetCommandAndCommandParameterProperty<Button>(), Is.Not.Null);

		[Test]
		public void GetDefaultBindableCommandPropertiesForDerivedType()
			=> Assert.That(DefaultBindableProperties.GetCommandAndCommandParameterProperty<DerivedFromButton>(), Is.Not.Null);

		[Test]
		public void GetDefaultBindableCommandPropertiesForMauiDerivedType()
			=> Assert.That(DefaultBindableProperties.GetCommandAndCommandParameterProperty<MenuFlyoutItem>(), Is.Not.Null);

		[Test]
		public void GetDefaultBindableCommandPropertiesForUnsupportedType()
			=> Assert.Throws<ArgumentException>(
				() => DefaultBindableProperties.GetDefaultProperty<CustomView>(),
				"No command + command parameter properties are registered for BindableObject type XamarinFormsMarkupUnitTestsDefaultBindablePropertiesViews.CustomView" +
				"\r\nRegister command + command parameter properties for this BindableObject type");

		[Test]
		public void RegisterDefaultBindableCommandProperties()
		{
			Assert.Throws<ArgumentException>(() => DefaultBindableProperties.GetCommandAndCommandParameterProperty<CustomViewWithCommand>());

			DefaultBindableProperties.RegisterForCommand((CustomViewWithCommand.CommandProperty, CustomViewWithCommand.CommandParameterProperty));
		}

		[TearDown]
		public override void TearDown()
		{
			if (DefaultBindableProperties.TryGetDefaultProperty<CustomViewWithText>(out _))
			{
				DefaultBindableProperties.Unregister(CustomViewWithText.TextProperty);
			}

			if (DefaultBindableProperties.TryGetCommandAndCommandParameterProperty<CustomViewWithCommand>(out _, out _))
			{
				DefaultBindableProperties.UnregisterForCommand(CustomViewWithCommand.CommandProperty);
			}

			base.TearDown();
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
		public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomViewWithText), default(string));

		public string Text
		{
			get => (string)GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}
	}

	class CustomViewWithCommand : View
	{
		public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(CustomViewWithCommand), default(ICommand));
		public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CustomViewWithCommand), default(object));

		public ICommand Command
		{
			get => (ICommand)GetValue(CommandProperty);
			set => SetValue(CommandProperty, value);
		}

		public object CommandParameter
		{
			get => GetValue(CommandParameterProperty);
			set => SetValue(CommandParameterProperty, value);
		}
	}
}