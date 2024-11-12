using System.Globalization;
using System.Reflection;
using Microsoft.Maui.Controls.Internals;
using NUnit.Framework;
namespace CommunityToolkit.Maui.Markup.UnitTests;

static class BindingHelpers
{
	static MethodInfo? getContextMethodInfo;

	internal static void AssertBindingExists(
		BindableObject bindable,
		BindableProperty targetProperty,
		string path = ".",
		BindingMode mode = BindingMode.Default,
		bool assertConverterInstanceIsAnyNotNull = false,
		IValueConverter? converter = null,
		object? converterParameter = null,
		string? stringFormat = null,
		object? source = null,
		object? targetNullValue = default,
		object? fallbackValue = default,
		Action<IValueConverter>? assertConvert = null)
		=> AssertBindingExists<object, object>(
			bindable, targetProperty, path, mode, assertConverterInstanceIsAnyNotNull, converter, converterParameter,
			stringFormat, source, targetNullValue, fallbackValue, assertConvert);

	internal static void AssertBindingExists<TDest>(
		BindableObject bindable,
		BindableProperty targetProperty,
		string path = ".",
		BindingMode mode = BindingMode.Default,
		bool assertConverterInstanceIsAnyNotNull = false,
		IValueConverter? converter = null,
		string? stringFormat = null,
		object? source = null,
		TDest? targetNullValue = default,
		TDest? fallbackValue = default,
		Action<IValueConverter>? assertConvert = null)
		=> AssertBindingExists<TDest, object>(
			bindable, targetProperty, path, mode, assertConverterInstanceIsAnyNotNull, converter, null,
			stringFormat, source, targetNullValue, fallbackValue, assertConvert);

	internal static void AssertTypedBindingExists<TBindable, TBindingContext>(
		TBindable bindable,
		BindableProperty targetProperty,
		BindingMode expectedBindingMode,
		TBindingContext expectedSource,
		string? expectedFormat = null) where TBindable : BindableObject
		=> AssertTypedBindingExists<TBindable, TBindingContext, object?, object?, object?>(
			bindable, targetProperty, expectedBindingMode, expectedSource, expectedStringFormat: expectedFormat);

	internal static void AssertTypedBindingExists<TBindable, TBindingContext, TSource, TDest>(
		TBindable bindable,
		BindableProperty targetProperty,
		BindingMode expectedBindingMode,
		TBindingContext expectedSource,
		Func<TSource?, TDest>? expectedConverter = null,
		string? expectedFormat = null) where TBindable : BindableObject
	{
		var funcConverter = expectedConverter switch
		{
			null => null,
			_ => new FuncConverter<TSource, TDest, object>(expectedConverter)
		};

		AssertTypedBindingExists<TBindable, TBindingContext, TSource, object?, TDest>(
			bindable, targetProperty, expectedBindingMode, expectedSource, funcConverter, expectedStringFormat: expectedFormat);
	}

	internal static void AssertTypedBindingExists<TBindable, TBindingContext, TSource, TParam, TDest>(
		TBindable bindable,
		BindableProperty targetProperty,
		BindingMode expectedBindingMode,
		TBindingContext expectedSource,
		IValueConverter? expectedConverter = null,
		string? expectedStringFormat = null,
		TDest? expectedTargetNullValue = default,
		TDest? expectedFallbackValue = default,
		TParam? expectedConverterParameter = default) where TBindable : BindableObject
	{
		var binding = GetTypedBinding(bindable, targetProperty) ?? throw new NullReferenceException();

		Assert.Multiple(() =>
		{
			Assert.That(binding, Is.Not.Null);
			Assert.That(binding.Mode, Is.EqualTo(expectedBindingMode));

			Assert.That(binding.Converter?.ToString(), Is.EqualTo(expectedConverter?.ToString()));

			Assert.That(binding.ConverterParameter, Is.EqualTo(expectedConverterParameter));

			Assert.That(expectedSource, Is.InstanceOf<TBindingContext>());
			Assert.That(binding.StringFormat, Is.EqualTo(expectedStringFormat));
			Assert.That(binding.TargetNullValue, Is.EqualTo(expectedTargetNullValue));
			Assert.That(binding.FallbackValue, Is.EqualTo(expectedFallbackValue));
		});
	}

	internal static void AssertBindingExists<TDest, TParam>(
		BindableObject bindable,
		BindableProperty targetProperty,
		string path = ".",
		BindingMode mode = BindingMode.Default,
		bool assertConverterInstanceIsAnyNotNull = false,
		IValueConverter? converter = null,
		TParam? converterParameter = default,
		string? stringFormat = null,
		object? source = null,
		TDest? targetNullValue = default,
		TDest? fallbackValue = default,
		Action<IValueConverter>? assertConvert = null)
	{
		var binding = GetBinding(bindable, targetProperty) ?? throw new NullReferenceException();

		Assert.Multiple(() =>
		{
			Assert.That(binding, Is.Not.Null);
			Assert.That(binding.Path, Is.EqualTo(path));
			Assert.That(binding.Mode, Is.EqualTo(mode));
		});

		if (assertConverterInstanceIsAnyNotNull)
		{
			Assert.That(binding.Converter, Is.Not.Null);
		}
		else
		{
			Assert.That(binding.Converter, Is.EqualTo(converter));
		}

		Assert.Multiple(() =>
		{
			Assert.That(binding.ConverterParameter, Is.EqualTo(converterParameter));
			Assert.That(binding.StringFormat, Is.EqualTo(stringFormat));
			Assert.That(binding.Source, Is.EqualTo(source));
			Assert.That(binding.TargetNullValue, Is.EqualTo(targetNullValue));
			Assert.That(binding.FallbackValue, Is.EqualTo(fallbackValue));
		});

		assertConvert?.Invoke(binding.Converter);
	}

	internal static void AssertBindingExists<TDest>(
		BindableObject bindable,
		BindableProperty targetProperty,
		IList<BindingBase> bindings,
		IMultiValueConverter? converter = null,
		BindingMode mode = BindingMode.Default,
		bool assertConverterInstanceIsAnyNotNull = false,
		string? stringFormat = null,
		TDest? targetNullValue = default,
		TDest? fallbackValue = default,
		Action<IMultiValueConverter>? assertConvert = null)
		=> AssertBindingExists<TDest, object>(
			bindable, targetProperty, bindings, converter, null, mode, assertConverterInstanceIsAnyNotNull,
			stringFormat, targetNullValue, fallbackValue, assertConvert);

	internal static void AssertBindingExists<TDest, TParam>(
		BindableObject bindable,
		BindableProperty targetProperty,
		IList<BindingBase> bindings,
		IMultiValueConverter? converter = null,
		TParam? converterParameter = default,
		BindingMode mode = BindingMode.Default,
		bool assertConverterInstanceIsAnyNotNull = false,
		string? stringFormat = null,
		TDest? targetNullValue = default,
		TDest? fallbackValue = default,
		Action<IMultiValueConverter>? assertConvert = null)
	{
		var binding = GetMultiBinding(bindable, targetProperty) ?? throw new NullReferenceException();

		Assert.Multiple(() =>
		{
			Assert.That(binding, Is.Not.Null);
			Assert.That(binding.Bindings.SequenceEqual(bindings), Is.True);
			Assert.That(binding.Mode, Is.EqualTo(mode));
		});

		if (assertConverterInstanceIsAnyNotNull)
		{
			Assert.That(binding.Converter, Is.Not.Null);
		}
		else
		{
			Assert.That(binding.Converter, Is.EqualTo(converter));
		}

		Assert.Multiple(() =>
		{
			Assert.That(binding.ConverterParameter, Is.EqualTo(converterParameter));
			Assert.That(binding.StringFormat, Is.EqualTo(stringFormat));
			Assert.That(binding.TargetNullValue, Is.EqualTo(targetNullValue));
			Assert.That(binding.FallbackValue, Is.EqualTo(fallbackValue));
		});

		assertConvert?.Invoke(binding.Converter);
	}

	internal static Binding? GetBinding(BindableObject bindable, BindableProperty property) => GetBindingBase<Binding>(bindable, property);

	internal static TypedBindingBase? GetTypedBinding(BindableObject bindable, BindableProperty property) => GetBindingBase<TypedBindingBase>(bindable, property);

	internal static MultiBinding? GetMultiBinding(BindableObject bindable, BindableProperty property) => GetBindingBase<MultiBinding>(bindable, property);

	/// <remarks>
	/// Note that we are only testing whether the Markup helpers create the correct bindings,
	/// we are not testing the binding mechanism itself; this is why it is justified to access
	/// private binding API's here for testing.
	/// </remarks>
	internal static TBinding? GetBindingBase<TBinding>(BindableObject bindable, BindableProperty property) where TBinding : BindingBase
	{
		getContextMethodInfo ??= typeof(BindableObject).GetMethod("GetContext", BindingFlags.NonPublic | BindingFlags.Instance);

		var context = (BindableObject.BindablePropertyContext?)getContextMethodInfo?.Invoke(bindable, [property]);
		return (TBinding?)context?.Bindings.GetValue();
	}

	internal static IValueConverter AssertConvert<TValue, TConvertedValue>(this IValueConverter converter, TValue value, object? parameter, TConvertedValue expectedConvertedValue, bool twoWay = false, bool backOnly = false, CultureInfo? culture = null)
	{
		Assert.Multiple(() =>
		{
			Assert.That(converter.Convert(value, typeof(object), parameter, culture ?? CultureInfo.InvariantCulture), Is.EqualTo(backOnly ? default : expectedConvertedValue));
			Assert.That(converter.ConvertBack(expectedConvertedValue, typeof(object), parameter, culture ?? CultureInfo.InvariantCulture), Is.EqualTo(twoWay || backOnly ? value : default(TValue)));
		});
		return converter;
	}

	internal static IValueConverter AssertConvert<TValue, TConvertedValue>(this IValueConverter converter, TValue value, TConvertedValue expectedConvertedValue, bool twoWay = false, bool backOnly = false, CultureInfo? culture = null)
		=> AssertConvert(converter, value, null, expectedConvertedValue, twoWay: twoWay, backOnly: backOnly, culture: culture);

	internal static IMultiValueConverter AssertConvert<TConvertedValue>(this IMultiValueConverter converter, object[] values, object? parameter, TConvertedValue expectedConvertedValue, bool twoWay = false, bool backOnly = false, CultureInfo? culture = null)
	{
		Assert.That(converter.Convert(values, typeof(TConvertedValue), parameter, culture), Is.EqualTo(backOnly ? BindableProperty.UnsetValue : expectedConvertedValue));

		var convertedBackValues = converter.ConvertBack(expectedConvertedValue, null, parameter, culture);

		if (twoWay || backOnly)
		{
			Assert.That(convertedBackValues, Has.Length.EqualTo(values.Length));
			for (var i = 0; i < values.Length; i++)
			{
				Assert.That(convertedBackValues[i], Is.EqualTo(values[i]));
			}
		}
		else
		{
			Assert.That(convertedBackValues, Is.Null);
		}

		return converter;
	}

	internal static IMultiValueConverter AssertConvert<TConvertedValue>(this IMultiValueConverter converter, object[] values, TConvertedValue expectedConvertedValue, bool twoWay = false, bool backOnly = false, CultureInfo? culture = null)
		=> AssertConvert(converter, values, null, expectedConvertedValue, twoWay, backOnly, culture);
}