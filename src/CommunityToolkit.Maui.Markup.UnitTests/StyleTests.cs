using CommunityToolkit.Maui.Markup.UnitTests.Base;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class StyleTests : BaseMarkupTestFixture
{
	[Test]
	public void ImplicitCastToStyleT()
	{
		var formsStyle = new Style(typeof(Label));
		var style = (Style<Label>)formsStyle;

		Assert.That(ReferenceEquals(style.MauiStyle, formsStyle));
	}

	[Test]
	public void ImplicitCastToStyleTUsingBaseClass()
	{
		var formsStyle = new Style(typeof(Label))
		{
			Behaviors =
			{
				new LabelBehavior()
			}
		};

		var style = (Style<View>)formsStyle;

		Assert.Multiple(() =>
		{
			Assert.That(formsStyle.Behaviors[0], Is.InstanceOf<LabelBehavior>());
			Assert.That(style.MauiStyle.Behaviors[0], Is.InstanceOf<LabelBehavior>());
		});
	}

	[Test]
	public void ImplicitCastFromStyleT()
	{
		Style<Label> style = new();
		Style formsStyle = style;

		Assert.That(ReferenceEquals(style.MauiStyle, formsStyle));
	}

	[Test]
	public void StyleSingleSetter()
	{
		var style = new Style<Label>(Label.TextColorProperty, Colors.Red);
		Style formsStyle = style;

		Assert.That(formsStyle.Setters.Count, Is.EqualTo(1));

		var setter = formsStyle.Setters[0];

		Assert.Multiple(() =>
		{
			Assert.That(setter.Property, Is.EqualTo(Label.TextColorProperty));
			Assert.That(setter.Value, Is.EqualTo(Colors.Red));
		});
	}

	[Test]
	public void StyleMultipleSetters()
	{
		var style = new Style<Label>(
			(Label.TextColorProperty, Colors.Red),
			(Label.TranslationXProperty, 8.0));
		Style formsStyle = style;

		Assert.That(formsStyle.Setters.Count, Is.EqualTo(2));

		var setter1 = formsStyle.Setters[0];

		Assert.Multiple(() =>
		{
			Assert.That(setter1.Property, Is.EqualTo(Label.TextColorProperty));
			Assert.That(setter1.Value, Is.EqualTo(Colors.Red));
		});

		var setter2 = formsStyle.Setters[1];

		Assert.Multiple(() =>
		{
			Assert.That(setter2.Property, Is.EqualTo(Label.TranslationXProperty));
			Assert.That(setter2.Value, Is.EqualTo(8.0));
		});
	}

	[Test]
	public void ApplyToDerivedTypes()
	{
		var style = new Style<Label>();
		Style formsStyle = style;

		Assert.That(formsStyle.ApplyToDerivedTypes, Is.False);
		style.ApplyToDerivedTypes(true);
		Assert.That(formsStyle.ApplyToDerivedTypes, Is.True);
	}

	[Test]
	public void BasedOn()
	{
		Style baseStyle = new Style<Label>();
		if (baseStyle is null)
		{
			throw new NullReferenceException();
		}

		var style = new Style<Label>().BasedOn(baseStyle);
		Style formsStyle = style, formsBaseStyle = baseStyle;

		Assert.That(ReferenceEquals(formsStyle.BasedOn, formsBaseStyle));
	}

	[Test]
	public void AddSingleSetter()
	{
		var style = new Style<Label>();
		Style formsStyle = style;

		Assume.That(formsStyle.Setters.Count, Is.EqualTo(0));

		style.Add(Label.TextColorProperty, Colors.Red);

		Assert.That(formsStyle.Setters.Count, Is.EqualTo(1));

		var setter = formsStyle.Setters[0];

		Assert.Multiple(() =>
		{
			Assert.That(setter.Property, Is.EqualTo(Label.TextColorProperty));
			Assert.That(setter.Value, Is.EqualTo(Colors.Red));
		});
	}

	[Test]
	public void AddMultipleSetters()
	{
		var style = new Style<Label>();
		Style formsStyle = style;
		Assume.That(formsStyle.Setters.Count, Is.EqualTo(0));

		style.Add(
			(Label.TextColorProperty, Colors.Red),
			(Label.TranslationXProperty, 8.0));

		Assert.That(formsStyle.Setters.Count, Is.EqualTo(2));

		var setter1 = formsStyle.Setters[0];

		Assert.Multiple(() =>
		{
			Assert.That(setter1.Property, Is.EqualTo(Label.TextColorProperty));
			Assert.That(setter1.Value, Is.EqualTo(Colors.Red));
		});

		var setter2 = formsStyle.Setters[1];

		Assert.Multiple(() =>
		{
			Assert.That(setter2.Property, Is.EqualTo(Label.TranslationXProperty));
			Assert.That(setter2.Value, Is.EqualTo(8.0));
		});
	}

	[Test]
	public void AddSingleBehavior()
	{
		var style = new Style<Label>();
		Style formsStyle = style;
		Assume.That(formsStyle.Behaviors.Count, Is.EqualTo(0));
		var behavior = new LabelBehavior();

		style.Add(behavior);

		Assert.Multiple(() =>
		{
			Assert.That(formsStyle.Behaviors.Count, Is.EqualTo(1));
			Assert.That(ReferenceEquals(formsStyle.Behaviors[0], behavior));
		});
	}

	[Test]
	public void AddMultipleBehaviors()
	{
		var style = new Style<Label>();
		Style formsStyle = style;
		Assume.That(formsStyle.Behaviors.Count, Is.EqualTo(0));
		var behavior1 = new LabelBehavior();
		var behavior2 = new LabelBehavior();

		style.Add(behavior1, behavior2);

		Assert.Multiple(() =>
		{
			Assert.That(formsStyle.Behaviors.Count, Is.EqualTo(2));
			Assert.That(ReferenceEquals(formsStyle.Behaviors[0], behavior1));
			Assert.That(ReferenceEquals(formsStyle.Behaviors[1], behavior2));
		});
	}

	[Test]
	public void AddSingleTrigger()
	{
		var style = new Style<Label>();
		Style formsStyle = style;
		Assume.That(formsStyle.Triggers.Count, Is.EqualTo(0));
		var trigger = new Trigger(typeof(Label));

		style.Add(trigger);

		Assert.Multiple(() =>
		{
			Assert.That(formsStyle.Triggers.Count, Is.EqualTo(1));
			Assert.That(ReferenceEquals(formsStyle.Triggers[0], trigger));
		});
	}

	[Test]
	public void AddMultipleTriggers()
	{
		var style = new Style<Label>();
		Style formsStyle = style;
		Assume.That(formsStyle.Triggers.Count, Is.EqualTo(0));
		var trigger1 = new Trigger(typeof(Label));
		var trigger2 = new Trigger(typeof(Label));

		style.Add(trigger1, trigger2);

		Assert.Multiple(() =>
		{
			Assert.That(formsStyle.Triggers.Count, Is.EqualTo(2));
			Assert.That(ReferenceEquals(formsStyle.Triggers[0], trigger1));
			Assert.That(ReferenceEquals(formsStyle.Triggers[1], trigger2));
		});
	}

	[Test]
	public void CanCascade()
	{
		var style = new Style<Label>();
		Style formsStyle = style;

		Assert.That(formsStyle.CanCascade, Is.False);
		style.CanCascade(true);
		Assert.That(formsStyle.CanCascade, Is.True);
	}

	[Test]
	public void Fluent()
	{
		var basedOnStyle = new Style<Label>();

		var style =
			new Style<Label>()
			.ApplyToDerivedTypes(true)
			.BasedOn(basedOnStyle)
			.Add((Label.TextColorProperty, Colors.Red))
			.Add(new LabelBehavior())
			.Add(new Trigger(typeof(Label)))
			.CanCascade(true);

		Assert.Multiple(() =>
		{
			Assert.That(style.MauiStyle.CanCascade, Is.True);
			Assert.That(style.MauiStyle.Setters[0].Value, Is.EqualTo(Colors.Red));
			Assert.That(style.MauiStyle.Behaviors[0], Is.InstanceOf<LabelBehavior>());
			Assert.That(style.MauiStyle.Triggers[0], Is.InstanceOf<Trigger>());
			Assert.That(style.MauiStyle.CanBeAppliedTo(typeof(Label)), Is.True);
		});
	}

	[TestCase(AppTheme.Light)]
	[TestCase(AppTheme.Dark)]
	[TestCase(AppTheme.Unspecified)]
	public void AddAppThemeBindingCorrectlySetsPropertyToChangeBasedOnApplicationsAppTheme(AppTheme appTheme)
	{
		var darkThemeColor = Colors.Orange;
		var otherThemeColor = Colors.Purple;

		var expectedColor = appTheme == AppTheme.Dark ? darkThemeColor : otherThemeColor;

		ApplicationTestHelpers.PerformAppThemeBasedTest(
			appTheme,
			() => new Label()
					.Style(new Style<Label>().AddAppThemeBinding(Label.TextColorProperty, otherThemeColor, darkThemeColor))
					.AppThemeBinding(Label.TextProperty, nameof(AppTheme.Light), nameof(AppTheme.Dark)),
			(label) => Assert.That(label.TextColor, Is.EqualTo(expectedColor)));
	}

	[TestCase(AppTheme.Light)]
	[TestCase(AppTheme.Dark)]
	[TestCase(AppTheme.Unspecified)]
	public void AddAppThemeBindingsCorrectlySetsPropertiesToChangeBasedOnApplicationsAppTheme(AppTheme appTheme)
	{
		var darkThemeColor = Colors.Orange;
		var otherThemeColor = Colors.Purple;

		var expectedColor = appTheme == AppTheme.Dark ? darkThemeColor : otherThemeColor;
		var expectedText = appTheme == AppTheme.Dark ? nameof(AppTheme.Dark) : nameof(AppTheme.Light);

		ApplicationTestHelpers.PerformAppThemeBasedTest(
			appTheme,
			() => new Label()
					.Style(new Style<Label>().AddAppThemeBindings((Label.TextColorProperty, otherThemeColor, darkThemeColor),
													(Label.TextProperty, nameof(AppTheme.Light), nameof(AppTheme.Dark))))
					.AppThemeBinding(Label.TextProperty, nameof(AppTheme.Light), nameof(AppTheme.Dark)),
			(label) =>
			{
				Assert.Multiple(() =>
				{
					Assert.That(label.TextColor, Is.EqualTo(expectedColor));
					Assert.That(label.Text, Is.EqualTo(expectedText));
				});
			});
	}

	[Test]
	public void InvalidMauiStyleInitializationShouldThrowException()
	{
		var buttonStyle = new Style(typeof(Button));
		Assert.Throws<ArgumentException>(() => new Style<Label>(buttonStyle));
	}

	[Test]
	public void InvalidMauiStyleCastShouldThrowException()
	{
		var buttonStyle = new Style(typeof(Button));
		Assert.Throws<ArgumentException>(() =>
		{
			var style = (Style<Label>)buttonStyle;
		});
	}

	[Test]
	public void ValidMauiStyleInitializationDoesNotThrowException()
	{
		var buttonStyle = new Style(typeof(Button));
		Assert.DoesNotThrow(() => new Style<Button>(buttonStyle));
	}

	class LabelBehavior : Behavior<Label>;
}