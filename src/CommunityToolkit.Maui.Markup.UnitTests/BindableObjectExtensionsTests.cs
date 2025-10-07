using System.Windows.Input;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using NUnit.Framework;
namespace CommunityToolkit.Maui.Markup.UnitTests
{
	[TestFixture]
	class BindableObjectExtensionsTests : BaseMarkupTestFixture
	{
		ViewModel? viewModel;

		[SetUp]
		public override void Setup()
		{
			base.Setup();
			viewModel = new ViewModel();
		}

		[TearDown]
		public override void TearDown()
		{
			viewModel = null;
			base.TearDown();
		}

		[Test]
		public void BindSpecifiedPropertyWithDefaults()
		{
			var label = new Label();
			label.Bind(Label.TextColorProperty, nameof(viewModel.TextColor));
			BindingHelpers.AssertBindingExists(label, Label.TextColorProperty, nameof(viewModel.TextColor));
		}

		// Note that we test positional parameters to catch API parameter order changes (which would be breaking).
		// Testing named parameters is not useful because a parameter rename operation in the API would also rename it in the test
		[Test]
		public void BindSpecifiedPropertyWithPositionalParameters()
		{
			var button = new Button();
			object converterParameter = 1;
			var stringFormat = nameof(BindSpecifiedPropertyWithPositionalParameters) + " {0}";
			IValueConverter converter = new ToStringConverter();
			object source = new ViewModel();
			object targetNullValue = nameof(BindSpecifiedPropertyWithPositionalParameters) + " null";
			object fallbackValue = nameof(BindSpecifiedPropertyWithPositionalParameters) + " fallback";

			button.Bind(
				Button.TextProperty,
				nameof(viewModel.Text),
				BindingMode.OneWay,
				converter,
				converterParameter,
				stringFormat,
				source,
				targetNullValue,
				fallbackValue
			);

			BindingHelpers.AssertBindingExists(
				button,
				targetProperty: Button.TextProperty,
				path: nameof(viewModel.Text),
				mode: BindingMode.OneWay,
				converter: converter,
				converterParameter: converterParameter,
				stringFormat: stringFormat,
				source: source,
				targetNullValue: targetNullValue,
				fallbackValue: fallbackValue
			);
		}

		[Test]
		public void BindSpecifiedPropertyWithInlineOneWayConvertAndDefaults()
		{
			var label = new Label();
			label.Bind(
				Label.TextColorProperty,
				nameof(viewModel.IsRed),
				convert: (bool? isRed) => isRed.HasValue && isRed.Value ? Colors.Red : Colors.Transparent
			);

			BindingHelpers.AssertBindingExists<Color>(
				label,
				Label.TextColorProperty,
				nameof(viewModel.IsRed),
				assertConverterInstanceIsAnyNotNull: true,
				assertConvert: c => c.AssertConvert<bool?, Color>(true, Colors.Red).AssertConvert<bool?, Color>(false, Colors.Transparent)
			);
		}

		[Test]
		public void BindSpecifiedPropertyWithInlineOneWayConvertUsingValueTypeAndDefaults()
		{
			const double additionalHeight = 5;

			var label = new Label();
			label.Bind(
				Label.HeightRequestProperty,
				nameof(viewModel.HeightRequest),
				convert: (double heightRequest) => heightRequest + additionalHeight
			);

			BindingHelpers.AssertBindingExists(
				label,
				Label.HeightRequestProperty,
				nameof(viewModel.HeightRequest),
				targetNullValue: 0.0,
				assertConverterInstanceIsAnyNotNull: true,
				assertConvert: c => c.AssertConvert<double, double>(0, additionalHeight)
			);
		}

		[Test]
		public void BindSpecifiedPropertyWithInlineOneWayParameterizedConvertAndDefaults()
		{
			var label = new Label();
			label.Bind(Label.TextColorProperty,
				nameof(viewModel.IsRed),
				convert: (bool? isRed, float? alpha) =>
				{
					ArgumentNullException.ThrowIfNull(alpha);
					return (isRed.HasValue && isRed.Value ? Colors.Red : Colors.Green).MultiplyAlpha(alpha.Value);
				},
				converterParameter: 0.5f
			);

			BindingHelpers.AssertBindingExists<Color, double>(
				label,
				Label.TextColorProperty,
				nameof(viewModel.IsRed),
				assertConverterInstanceIsAnyNotNull: true,
				converterParameter: 0.5f,
				assertConvert: c => c.AssertConvert<bool?, Color>(true, 0.5f, Colors.Red.MultiplyAlpha(0.5f))
					.AssertConvert<bool?, Color>(false, 0.2f, Colors.Green.MultiplyAlpha(0.2f))
			);
		}

		[Test]
		public void BindSpecifiedPropertyWithInlineTwoWayConvertAndDefaults()
		{
			var label = new Label();
			label.Bind(
				Label.TextColorProperty,
				nameof(viewModel.IsRed),
				BindingMode.TwoWay,
				(bool? isRed) => isRed.HasValue && isRed.Value ? Colors.Red : Colors.Transparent,
				color => color == Colors.Red
			);

			BindingHelpers.AssertBindingExists<Color>(
				label,
				Label.TextColorProperty,
				nameof(viewModel.IsRed),
				BindingMode.TwoWay,
				assertConverterInstanceIsAnyNotNull: true,
				assertConvert: c => c.AssertConvert(true, Colors.Red, twoWay: true)
					.AssertConvert(false, Colors.Transparent, twoWay: true)
			);
		}

		[Test]
		public void BindSpecifiedPropertyWithInlineTwoWayParameterizedConvertAndDefaults()
		{
			var label = new Label();
			label.Bind(
				Label.TextColorProperty,
				nameof(viewModel.IsRed),
				BindingMode.TwoWay,
				convert(),
				convertBack(),
				0.5f
			);

			BindingHelpers.AssertBindingExists<Color, float>(
				label,
				Label.TextColorProperty,
				nameof(viewModel.IsRed),
				BindingMode.TwoWay,
				assertConverterInstanceIsAnyNotNull: true,
				converterParameter: 0.5f,
				assertConvert: c => c.AssertConvert(true, 0.5f, Colors.Red.MultiplyAlpha(0.5f), twoWay: true)
					.AssertConvert(false, 0.2f, Colors.Green.MultiplyAlpha(0.2f), twoWay: true)
			);

			static Func<bool, float, Color> convert()
			{
				return (isRed, alpha) => (isRed ? Colors.Red : Colors.Green).MultiplyAlpha(alpha);
			}

			static Func<Color?, float, bool> convertBack()
			{
				return (color, alpha) => color?.Alpha == alpha && color.Red == Colors.Red.Red && color.Green == Colors.Red.Green && color.Blue == Colors.Red.Blue;
			}
		}

		[Test]
		public void BindSpecifiedPropertyWithInlineOneWayConvertAndPositionalParameters()
		{
			var button = new Button();
			var stringFormat = nameof(BindSpecifiedPropertyWithInlineOneWayConvertAndPositionalParameters) + " {0}";
			object source = new ViewModel();
			var targetNullValue = nameof(BindSpecifiedPropertyWithInlineOneWayConvertAndPositionalParameters) + " null";
			var fallbackValue = nameof(BindSpecifiedPropertyWithInlineOneWayConvertAndPositionalParameters) + " fallback";

			button.Bind(
				Button.TextProperty,
				nameof(viewModel.Text),
				BindingMode.OneWay,
				(string? text) => $"'{text?.Trim('\'')}'",
				null,
				stringFormat,
				source,
				targetNullValue,
				fallbackValue
			);

			BindingHelpers.AssertBindingExists(
				button,
				targetProperty: Button.TextProperty,
				path: nameof(viewModel.Text),
				mode: BindingMode.OneWay,
				assertConverterInstanceIsAnyNotNull: true,
				stringFormat: stringFormat,
				source: source,
				targetNullValue: targetNullValue,
				fallbackValue: fallbackValue,
				assertConvert: c => c.AssertConvert("test", "'test'")
			);
		}

		[Test]
		public void BindSpecifiedPropertyWithInlineOneWayParameterizedConvertAndPositionalParameters()
		{
			var button = new Button();
			var converterParameter = 1;
			var stringFormat = nameof(BindSpecifiedPropertyWithInlineOneWayParameterizedConvertAndPositionalParameters) + " {0}";
			object source = new ViewModel();
			var targetNullValue = nameof(BindSpecifiedPropertyWithInlineOneWayParameterizedConvertAndPositionalParameters) + " null";
			var fallbackValue = nameof(BindSpecifiedPropertyWithInlineOneWayParameterizedConvertAndPositionalParameters) + " fallback";

			button.Bind(
				Button.TextProperty,
				nameof(viewModel.Text),
				BindingMode.OneWay,
				(string? text, int? repeat) =>
				{
					ArgumentNullException.ThrowIfNull(repeat);
					return string.Concat(Enumerable.Repeat($"'{text?.Trim('\'')}'", repeat.Value));
				},
				null,
				converterParameter,
				stringFormat,
				source,
				targetNullValue,
				fallbackValue
			);

			BindingHelpers.AssertBindingExists(
				button,
				targetProperty: Button.TextProperty,
				path: nameof(viewModel.Text),
				mode: BindingMode.OneWay,
				assertConverterInstanceIsAnyNotNull: true,
				converterParameter: converterParameter,
				stringFormat: stringFormat,
				source: source,
				targetNullValue: targetNullValue,
				fallbackValue: fallbackValue,
				assertConvert: c => c.AssertConvert("test", 2, "'test''test'")
			);
		}

		[Test]
		public void BindSpecifiedPropertyWithInlineTwoWayConvertAndPositionalParameters()
		{
			var button = new Button();
			var stringFormat = nameof(BindSpecifiedPropertyWithInlineTwoWayConvertAndPositionalParameters) + " {0}";
			object source = new ViewModel();
			var targetNullValue = nameof(BindSpecifiedPropertyWithInlineTwoWayConvertAndPositionalParameters) + " null";
			var fallbackValue = nameof(BindSpecifiedPropertyWithInlineTwoWayConvertAndPositionalParameters) + " fallback";

			button.Bind(
				Button.TextProperty,
				nameof(viewModel.Text),
				BindingMode.TwoWay,
				text => $"'{text?.Trim('\'')}'",
				text =>
				{
					ArgumentNullException.ThrowIfNull(text);
					return text.Trim('\'');
				},
				stringFormat,
				source,
				targetNullValue,
				fallbackValue
			);

			BindingHelpers.AssertBindingExists(
				button,
				targetProperty: Button.TextProperty,
				path: nameof(viewModel.Text),
				mode: BindingMode.TwoWay,
				assertConverterInstanceIsAnyNotNull: true,
				stringFormat: stringFormat,
				source: source,
				targetNullValue: targetNullValue,
				fallbackValue: fallbackValue,
				assertConvert: c => c.AssertConvert("test", "'test'", twoWay: true)
			);
		}

		[Test]
		public void BindSpecifiedPropertyWithInlineTwoWayParameterizedConvertAndPositionalParameters()
		{
			var button = new Button();
			var converterParameter = 1;
			var stringFormat = nameof(BindSpecifiedPropertyWithInlineTwoWayParameterizedConvertAndPositionalParameters) + " {0}";
			object source = new ViewModel();
			var targetNullValue = nameof(BindSpecifiedPropertyWithInlineTwoWayParameterizedConvertAndPositionalParameters) + " null";
			var fallbackValue = nameof(BindSpecifiedPropertyWithInlineTwoWayParameterizedConvertAndPositionalParameters) + " fallback";

			button.Bind(
				Button.TextProperty,
				nameof(viewModel.Text),
				BindingMode.TwoWay,
				(string? text, int? repeat) =>
				{
					ArgumentNullException.ThrowIfNull(repeat);

					return string.Concat(Enumerable.Repeat($"'{text?.Trim('\'')}'", repeat.Value));
				},
				(text, repeat) =>
				{
					ArgumentNullException.ThrowIfNull(text);
					ArgumentNullException.ThrowIfNull(repeat);

					return text[..(text.Length / repeat.Value)].Trim('\'');
				},
				converterParameter,
				stringFormat,
				source,
				targetNullValue,
				fallbackValue
			);

			BindingHelpers.AssertBindingExists(
				button,
				targetProperty: Button.TextProperty,
				path: nameof(viewModel.Text),
				mode: BindingMode.TwoWay,
				assertConverterInstanceIsAnyNotNull: true,
				converterParameter: converterParameter,
				stringFormat: stringFormat,
				source: source,
				targetNullValue: targetNullValue,
				fallbackValue: fallbackValue,
				assertConvert: c => c.AssertConvert("test", 2, "'test''test'", twoWay: true)
			);
		}

		[Test]
		public void BindDefaultPropertyWithDefaults()
		{
			var label = new Label();
			label.Bind(nameof(viewModel.Text));
			BindingHelpers.AssertBindingExists(label, Label.TextProperty, nameof(viewModel.Text));
		}

		[Test]
		public void BindDefaultPropertyWithPositionalParameters()
		{
			var label = new Label();
			object converterParameter = 1;
			var stringFormat = nameof(BindDefaultPropertyWithPositionalParameters) + " {0}";
			IValueConverter converter = new ToStringConverter();
			object source = new ViewModel();
			object targetNullValue = nameof(BindDefaultPropertyWithPositionalParameters) + " null";
			object fallbackValue = nameof(BindDefaultPropertyWithPositionalParameters) + " fallback";

			label.Bind(
				nameof(viewModel.Text),
				BindingMode.OneWay,
				converter,
				converterParameter,
				stringFormat,
				source,
				targetNullValue,
				fallbackValue
			);

			BindingHelpers.AssertBindingExists(
				label,
				targetProperty: Label.TextProperty,
				path: nameof(viewModel.Text),
				mode: BindingMode.OneWay,
				converter: converter,
				converterParameter: converterParameter,
				stringFormat: stringFormat,
				source: source,
				targetNullValue: targetNullValue,
				fallbackValue: fallbackValue
			);
		}

		[Test]
		public void BindDefaultPropertyWithInlineOneWayConvertAndDefaults()
		{
			var label = new Label();
			label.Bind(
				nameof(viewModel.Text),
				convert: (string? text) => $"'{text}'"
			);

			BindingHelpers.AssertBindingExists(
				label,
				Label.TextProperty,
				nameof(viewModel.Text),
				assertConverterInstanceIsAnyNotNull: true,
				assertConvert: c => c.AssertConvert("test", "'test'")
			);
		}

		[Test]
		public void BindDefaultPropertyWithInlineOneWayParameterizedConvertAndDefaults()
		{
			var label = new Label();
			label.Bind(
				nameof(viewModel.Text),
				convert: (string? text, int? repeat) =>
				{
					ArgumentNullException.ThrowIfNull(text);
					ArgumentNullException.ThrowIfNull(repeat);

					return string.Concat(Enumerable.Repeat($"'{text.Trim('\'')}'", repeat.Value));
				},
				converterParameter: 1
			);

			BindingHelpers.AssertBindingExists<string, int>(
				label,
				Label.TextProperty,
				nameof(viewModel.Text),
				assertConverterInstanceIsAnyNotNull: true,
				converterParameter: 1,
				assertConvert: c => c.AssertConvert("test", 2, "'test''test'")
			);
		}

		[Test]
		public void BindDefaultPropertyWithInlineTwoWayConvertAndDefaults()
		{
			var label = new Label();
			label.Bind(
				nameof(viewModel.Text),
				BindingMode.TwoWay,
				(string? text) => $"'{text?.Trim('\'')}'",
				text =>
				{
					ArgumentNullException.ThrowIfNull(text);
					return text.Trim('\'');
				}
			);

			BindingHelpers.AssertBindingExists(
				label,
				Label.TextProperty,
				nameof(viewModel.Text),
				BindingMode.TwoWay,
				assertConverterInstanceIsAnyNotNull: true,
				assertConvert: c => c.AssertConvert("test", "'test'", twoWay: true)
			);
		}

		[Test]
		public void BindDefaultPropertyWithInlineTwoWayParameterizedConvertAndDefaults()
		{
			var label = new Label();
			label.Bind(
				nameof(viewModel.Text),
				BindingMode.TwoWay,
				(string? text, int? repeat) =>
				{
					ArgumentNullException.ThrowIfNull(text);
					ArgumentNullException.ThrowIfNull(repeat);

					return string.Concat(Enumerable.Repeat($"'{text.Trim('\'')}'", repeat.Value));
				},
				(text, repeat) =>
				{
					ArgumentNullException.ThrowIfNull(text);
					ArgumentNullException.ThrowIfNull(repeat);

					return text[..(text.Length / repeat.Value)].Trim('\'');
				},
				2
			);

			BindingHelpers.AssertBindingExists<string, int>(
				label,
				Label.TextProperty,
				nameof(viewModel.Text),
				BindingMode.TwoWay,
				assertConverterInstanceIsAnyNotNull: true,
				converterParameter: 2,
				assertConvert: c => c.AssertConvert("test", 2, "'test''test'", twoWay: true)
			);
		}

		[Test]
		public void BindDefaultPropertyWithInlineOneWayConvertAndPositionalParameters()
		{
			var label = new Label();
			var stringFormat = nameof(BindDefaultPropertyWithInlineOneWayConvertAndPositionalParameters) + " {0}";
			object source = new ViewModel();
			var targetNullValue = nameof(BindDefaultPropertyWithInlineOneWayConvertAndPositionalParameters) + " null";
			var fallbackValue = nameof(BindDefaultPropertyWithInlineOneWayConvertAndPositionalParameters) + " fallback";

			label.Bind(
				nameof(viewModel.Text),
				BindingMode.OneWay,
				(string? text) => $"'{text?.Trim('\'')}'",
				null,
				stringFormat,
				source,
				targetNullValue,
				fallbackValue
			);

			BindingHelpers.AssertBindingExists(
				label,
				targetProperty: Label.TextProperty,
				path: nameof(viewModel.Text),
				mode: BindingMode.OneWay,
				assertConverterInstanceIsAnyNotNull: true,
				stringFormat: stringFormat,
				source: source,
				targetNullValue: targetNullValue,
				fallbackValue: fallbackValue,
				assertConvert: c => c.AssertConvert("test", "'test'")
			);
		}

		[Test]
		public void BindDefaultPropertyWithInlineOneWayParameterizedConvertAndPositionalParameters()
		{
			var label = new Label();
			var converterParameter = 1;
			var stringFormat = nameof(BindDefaultPropertyWithInlineOneWayParameterizedConvertAndPositionalParameters) + " {0}";
			object source = new ViewModel();
			var targetNullValue = nameof(BindDefaultPropertyWithInlineOneWayParameterizedConvertAndPositionalParameters) + " null";
			var fallbackValue = nameof(BindDefaultPropertyWithInlineOneWayParameterizedConvertAndPositionalParameters) + " fallback";

			label.Bind(
				nameof(viewModel.Text),
				BindingMode.OneWay,
				(string? text, int? repeat) =>
				{
					ArgumentNullException.ThrowIfNull(repeat);

					return string.Concat(Enumerable.Repeat($"'{text?.Trim('\'')}'", repeat.Value));
				},
				null,
				converterParameter,
				stringFormat,
				source,
				targetNullValue,
				fallbackValue
			);

			BindingHelpers.AssertBindingExists(
				label,
				targetProperty: Label.TextProperty,
				path: nameof(viewModel.Text),
				mode: BindingMode.OneWay,
				assertConverterInstanceIsAnyNotNull: true,
				converterParameter: converterParameter,
				stringFormat: stringFormat,
				source: source,
				targetNullValue: targetNullValue,
				fallbackValue: fallbackValue,
				assertConvert: c => c.AssertConvert("test", 2, "'test''test'")
			);
		}

		[Test]
		public void BindDefaultPropertyWithInlineTwoWayConvertAndPositionalParameters()
		{
			var label = new Label();
			var stringFormat = nameof(BindDefaultPropertyWithInlineTwoWayConvertAndPositionalParameters) + " {0}";
			object source = new ViewModel();
			var targetNullValue = nameof(BindDefaultPropertyWithInlineTwoWayConvertAndPositionalParameters) + " null";
			var fallbackValue = nameof(BindDefaultPropertyWithInlineTwoWayConvertAndPositionalParameters) + " fallback";

			label.Bind(
				nameof(viewModel.Text),
				BindingMode.TwoWay,
				text => $"'{text?.Trim('\'')}'",
				text =>
				{
					ArgumentNullException.ThrowIfNull(text);
					return text.Trim('\'');
				},
				stringFormat,
				source,
				targetNullValue,
				fallbackValue
			);

			BindingHelpers.AssertBindingExists(
				label,
				targetProperty: Label.TextProperty,
				path: nameof(viewModel.Text),
				mode: BindingMode.TwoWay,
				assertConverterInstanceIsAnyNotNull: true,
				stringFormat: stringFormat,
				source: source,
				targetNullValue: targetNullValue,
				fallbackValue: fallbackValue,
				assertConvert: c => c.AssertConvert("test", "'test'", twoWay: true)
			);
		}

		[Test]
		public void BindDefaultPropertyWithInlineTwoWayParameterizedConvertAndPositionalParameters()
		{
			var label = new Label();
			var converterParameter = 1;
			var stringFormat = nameof(BindDefaultPropertyWithInlineTwoWayParameterizedConvertAndPositionalParameters) + " {0}";
			object source = new ViewModel();
			var targetNullValue = nameof(BindDefaultPropertyWithInlineTwoWayParameterizedConvertAndPositionalParameters) + " null";
			var fallbackValue = nameof(BindDefaultPropertyWithInlineTwoWayParameterizedConvertAndPositionalParameters) + " fallback";

			label.Bind(
				nameof(viewModel.Text),
				BindingMode.TwoWay,
				(string? text, int? repeat) =>
				{
					ArgumentNullException.ThrowIfNull(repeat);

					return string.Concat(Enumerable.Repeat($"'{text?.Trim('\'')}'", repeat.Value));
				},
				(text, repeat) =>
				{
					ArgumentNullException.ThrowIfNull(text);
					ArgumentNullException.ThrowIfNull(repeat);

					return text[..(text.Length / repeat.Value)].Trim('\'');
				},
				converterParameter,
				stringFormat,
				source,
				targetNullValue,
				fallbackValue
			);

			BindingHelpers.AssertBindingExists(
				label,
				targetProperty: Label.TextProperty,
				path: nameof(viewModel.Text),
				mode: BindingMode.TwoWay,
				assertConverterInstanceIsAnyNotNull: true,
				converterParameter: converterParameter,
				stringFormat: stringFormat,
				source: source,
				targetNullValue: targetNullValue,
				fallbackValue: fallbackValue,
				assertConvert: c => c.AssertConvert("test", 2, "'test''test'", twoWay: true)
			);
		}

		[Test]
		public void BindCommandWithDefaults()
		{
			var button = new Button();
			var path = nameof(viewModel.Command);

			button.BindCommand(path);

			BindingHelpers.AssertBindingExists(button, Button.CommandProperty, path);
			BindingHelpers.AssertBindingExists(button, Button.CommandParameterProperty);
		}

		[Test]
		public void BindCommandWithoutParameter()
		{
			var button = new Button();
			var path = nameof(viewModel.Command);

			button.BindCommand(path, parameterPath: null);

			BindingHelpers.AssertBindingExists(button, Button.CommandProperty, path);
			Assert.That(BindingHelpers.GetBinding(button, Button.CommandParameterProperty), Is.Null);
		}

		[Test]
		public void BindCommandWithPositionalParameters()
		{
			var button = new Button();
			object source = new ViewModel();
			var path = nameof(viewModel.Command);
			var parameterPath = nameof(viewModel.Id);
			object parameterSource = new ViewModel();

			button.BindCommand(path, source, parameterPath, parameterSource);

			BindingHelpers.AssertBindingExists(button, Button.CommandProperty, path, source: source);
			BindingHelpers.AssertBindingExists(button, Button.CommandParameterProperty, parameterPath, source: parameterSource);
		}

		[Test]
		public void SupportDerivedElements()
		{
			Assert.Multiple(() =>
			{
				Assert.That(new DerivedFromLabel()
						.Bind(nameof(viewModel.Text))
						.Bind(
							nameof(viewModel.Text),
							convert: (string? text) => $"'{text}'")
						.Bind(
							nameof(viewModel.Text),
							convert: (string? text, int? repeat) =>
							{
								ArgumentNullException.ThrowIfNull(text);
								ArgumentNullException.ThrowIfNull(repeat);

								return string.Concat(Enumerable.Repeat($"'{text.Trim('\'')}'", repeat.Value));
							})
						.Bind(
							DerivedFromLabel.TextColorProperty,
							nameof(viewModel.TextColor))
						.Bind(
							DerivedFromLabel.BackgroundColorProperty,
							nameof(viewModel.IsRed),
							convert: (bool? isRed) => isRed.HasValue && isRed.Value ? Colors.Black : Colors.Transparent)
						.Bind(
							Label.TextColorProperty,
							nameof(viewModel.IsRed),
							convert: (bool? isRed, float? alpha) =>
							{
								ArgumentNullException.ThrowIfNull(alpha);

								return (isRed.HasValue && isRed.Value ? Colors.Red : Colors.Green).MultiplyAlpha(alpha.Value);
							})
						.Invoke(l => l.Text = nameof(SupportDerivedElements))
						.Assign(out DerivedFromLabel assignDerivedFromLabel),
					Is.InstanceOf<DerivedFromLabel>());

				Assert.That(assignDerivedFromLabel, Is.InstanceOf<DerivedFromLabel>());
			});
		}

		[TestCase(AppTheme.Light)]
		[TestCase(AppTheme.Dark)]
		[TestCase(AppTheme.Unspecified)]
		public void AppThemeColorBindingCorrectlySetsPropertyToChangeBasedOnApplicationsAppTheme(AppTheme appTheme)
		{
			var expectedColor = appTheme == AppTheme.Dark ? Colors.Orange : Colors.Purple;

			ApplicationTestHelpers.PerformAppThemeBasedTest(
				appTheme,
				() => new Label().AppThemeColorBinding(Label.TextColorProperty, Colors.Purple, Colors.Orange),
				label => Assert.That(label.TextColor, Is.EqualTo(expectedColor)));
		}

		[TestCase(AppTheme.Light)]
		[TestCase(AppTheme.Dark)]
		[TestCase(AppTheme.Unspecified)]
		public void AppThemeBindingCorrectlySetsPropertyToChangeBasedOnApplicationsAppTheme(AppTheme appTheme)
		{
			const string dark = nameof(AppTheme.Dark);
			const string light = nameof(AppTheme.Light);

			var expectedText = appTheme == AppTheme.Dark ? dark : light;

			ApplicationTestHelpers.PerformAppThemeBasedTest(
				appTheme,
				() => new Label().AppThemeBinding(Label.TextProperty, light, dark),
				label => Assert.That(label.Text, Is.EqualTo(expectedText)));
		}

		sealed class ViewModel
		{
			public Guid Id { get; set; }

			public ICommand? Command { get; set; }

			public string? Text { get; set; }

			public Color TextColor { get; set; } = Colors.Transparent;

			public bool IsRed { get; set; }

			public double HeightRequest { get; set; }
		}
	}
}

namespace BindableObjectViews // This namespace simulates derived controls defined in a separate app, for use in the tests in this file only
{
	class DerivedFromLabel : Label
	{
	}
}