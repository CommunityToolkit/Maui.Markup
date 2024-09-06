using System.Globalization;

namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// An <see cref="IMultiValueConverter" /> implementation that provides the ability to define type safe <c>Func</c> implementations that will be used in the conversion process.
/// </summary>
/// <typeparam name="TDest">The type of the value going to the target of the <see cref="MultiBinding" />.</typeparam>
/// <typeparam name="TParam">The type of the <c>ConverterParameter</c>.</typeparam>
public class FuncMultiConverter<TDest, TParam> : IMultiValueConverter
{
	readonly Func<object[], TDest>? convert;
	readonly Func<TDest?, object?[]>? convertBack;

	readonly Func<object[], TParam?, TDest>? convertWithParam;
	readonly Func<TDest?, TParam?, object?[]>? convertBackWithParam;

	readonly Func<object[], TParam?, CultureInfo, TDest>? convertWithParamAndCulture;
	readonly Func<TDest?, TParam?, CultureInfo, object[]>? convertBackWithParamAndCulture;

	/// <summary>
	/// Initializes a new instance of <see cref="FuncMultiConverter{TDest, TParam}" /> that allows support for acessing the <c>parameter</c> and <see cref="CultureInfo" /> in the conversion.
	/// </summary>
	/// <param name="convertWithParamAndCulture">
	/// The <see cref="Func{Array, TParam, CultureInfo, TDest}" /> implementation that will provide the conversion for the underlying <see cref="IMultiValueConverter.Convert" /> method.
	/// </param>
	/// <param name="convertBackWithParamAndCulture">
	/// The <see cref="Func{TDest, TParam, CultureInfo, Array}" /> implementation that will provide the conversion for the underlying <see cref="IMultiValueConverter.ConvertBack" /> method.
	/// </param>
	public FuncMultiConverter(Func<object[], TParam?, CultureInfo, TDest>? convertWithParamAndCulture = null, Func<TDest?, TParam?, CultureInfo, object[]>? convertBackWithParamAndCulture = null)
	{
		this.convertWithParamAndCulture = convertWithParamAndCulture;
		this.convertBackWithParamAndCulture = convertBackWithParamAndCulture;
	}

	/// <summary>
	/// Initializes a new instance of <see cref="FuncMultiConverter{TDest, TParam}" /> that allows support for acessing the <c>parameter</c> in the conversion.
	/// </summary>
	/// <param name="convertWithParam">
	/// The <see cref="Func{Array, TParam, TDest}" /> implementation that will provide the conversion for the underlying <see cref="IMultiValueConverter.Convert" /> method.
	/// </param>
	/// <param name="convertBackWithParam">
	/// The <see cref="Func{TDest, TParam, Array}" /> implementation that will provide the conversion for the underlying <see cref="IMultiValueConverter.ConvertBack" /> method.
	/// </param>
	public FuncMultiConverter(Func<object[], TParam?, TDest>? convertWithParam = null, Func<TDest?, TParam?, object?[]>? convertBackWithParam = null)
	{
		this.convertWithParam = convertWithParam;
		this.convertBackWithParam = convertBackWithParam;
	}

	/// <summary>
	/// Initializes a new instance of <see cref="FuncMultiConverter{TDest, TParam}" />.
	/// </summary>
	/// <param name="convert">
	/// The <see cref="Func{Array, TDest}" /> implementation that will provide the conversion for the underlying <see cref="IMultiValueConverter.Convert" /> method.
	/// </param>
	/// <param name="convertBack">
	/// The <see cref="Func{TDest, Array}" /> implementation that will provide the conversion for the underlying <see cref="IMultiValueConverter.ConvertBack" /> method.
	/// </param>
	public FuncMultiConverter(Func<object[], TDest>? convert = null, Func<TDest?, object?[]>? convertBack = null)
	{
		this.convert = convert;
		this.convertBack = convertBack;
	}

	/// <inheritdoc />
	public object? Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
	{
		if (convert is not null)
		{
			return convert(values);
		}

		if (convertWithParam != null)
		{
			return convertWithParam(
				values,
				parameter != null ? (TParam)parameter : default);
		}

		if (convertWithParamAndCulture != null)
		{
			return convertWithParamAndCulture(
				values,
				parameter != null ? (TParam)parameter : default,
				culture);
		}

		return BindableProperty.UnsetValue;
	}

	/// <inheritdoc />
	public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
	{
		if (convertBack != null)
		{
			return convertBack(
				value != null ? (TDest)value : default);
		}

		if (convertBackWithParam != null)
		{
			return convertBackWithParam(
				value != null ? (TDest)value : default,
				parameter != null ? (TParam)parameter : default);
		}

		if (convertBackWithParamAndCulture != null)
		{
			return convertBackWithParamAndCulture(
				value != null ? (TDest)value : default,
				parameter != null ? (TParam)parameter : default,
				culture);
		}

		return null;
	}
}

/// <summary>
/// An <see cref="IMultiValueConverter" /> implementation that expects 2 known input values.
/// </summary>
/// <typeparam name="TSource1">The type of the <b>first</b> value coming from the source of the <see cref="MultiBinding" />.</typeparam>
/// <typeparam name="TSource2">The type of the <b>second</b> value coming from the source of the <see cref="MultiBinding" />.</typeparam>
/// <typeparam name="TDest">The type of the value going to the target of the <see cref="MultiBinding" />.</typeparam>
public class FuncMultiConverter<TSource1, TSource2, TDest> : FuncMultiConverter<TDest, object>
{
	static T? To<T>(object? value) => value != null ? (T)value : default;

	static object?[] ToObjects(ValueTuple<TSource1, TSource2> values) => [values.Item1, values.Item2];

	/// <summary>
	/// Initializes a new instance of <see cref="FuncMultiConverter{TSource1, TSource2, TDest}" />.
	/// </summary>
	/// <param name="convert">
	/// The <see cref="Func{ValueTuple, TDest}" /> implementation that will provide the conversion for the underlying <see cref="IMultiValueConverter.Convert" /> method.
	/// </param>
	/// <param name="convertBack">
	/// The <see cref="Func{TDest, ValueTuple}" /> implementation that will provide the conversion for the underlying <see cref="IMultiValueConverter.ConvertBack" /> method.
	/// </param>
	public FuncMultiConverter(Func<ValueTuple<TSource1?, TSource2?>, TDest>? convert = null,
								Func<TDest?, ValueTuple<TSource1, TSource2>>? convertBack = null)
	: base(convert is null ? default(Func<object[], TDest>) : (object[] values) => convert((To<TSource1>(values[0]), To<TSource2>(values[1]))),
			convertBack is null ? default(Func<TDest?, object?[]>) : (TDest? value) => ToObjects(convertBack(value)))
	{
	}
}

/// <summary>
/// An <see cref="IMultiValueConverter" /> implementation that expects 3 known input values.
/// </summary>
/// <typeparam name="TSource1">The type of the <b>first</b> value coming from the source of the <see cref="MultiBinding" />.</typeparam>
/// <typeparam name="TSource2">The type of the <b>second</b> value coming from the source of the <see cref="MultiBinding" />.</typeparam>
/// <typeparam name="TSource3">The type of the <b>third</b> value coming from the source of the <see cref="MultiBinding" />.</typeparam>
/// <typeparam name="TDest">The type of the value going to the target of the <see cref="MultiBinding" />.</typeparam>
public class FuncMultiConverter<TSource1, TSource2, TSource3, TDest> : FuncMultiConverter<TDest, object>
{
	static T? To<T>(object? value) => value != null ? (T)value : default;

	static object?[] ToObjects(ValueTuple<TSource1, TSource2, TSource3> values) => [values.Item1, values.Item2, values.Item3];

	/// <summary>
	/// Initializes a new instance of <see cref="FuncMultiConverter{TSource1, TSource2, TSource3, TDest}" />.
	/// </summary>
	/// <param name="convert">
	/// The <see cref="Func{ValueTuple, TDest}" /> implementation that will provide the conversion for the underlying <see cref="IMultiValueConverter.Convert" /> method.
	/// </param>
	/// <param name="convertBack">
	/// The <see cref="Func{TDest, ValueTuple}" /> implementation that will provide the conversion for the underlying <see cref="IMultiValueConverter.ConvertBack" /> method.
	/// </param>
	public FuncMultiConverter(Func<ValueTuple<TSource1?, TSource2?, TSource3?>, TDest>? convert = null,
								Func<TDest?, ValueTuple<TSource1, TSource2, TSource3>>? convertBack = null)
	: base(convert is null ? default(Func<object[], TDest>) : (object[] values) => convert((To<TSource1>(values[0]), To<TSource2>(values[1]), To<TSource3>(values[2]))),
			convertBack is null ? default(Func<TDest?, object?[]>) : (TDest? value) => ToObjects(convertBack(value)))
	{

	}
}

/// <summary>
/// An <see cref="IMultiValueConverter" /> implementation that expects 4 known input values.
/// </summary>
/// <typeparam name="TSource1">The type of the <b>first</b> value coming from the source of the <see cref="MultiBinding" />.</typeparam>
/// <typeparam name="TSource2">The type of the <b>second</b> value coming from the source of the <see cref="MultiBinding" />.</typeparam>
/// <typeparam name="TSource3">The type of the <b>third</b> value coming from the source of the <see cref="MultiBinding" />.</typeparam>
/// <typeparam name="TSource4">The type of the <b>fourth</b> value coming from the source of the <see cref="MultiBinding" />.</typeparam>
/// <typeparam name="TDest">The type of the value going to the target of the <see cref="MultiBinding" />.</typeparam>
public class FuncMultiConverter<TSource1, TSource2, TSource3, TSource4, TDest> : FuncMultiConverter<TDest, object>
{
	static T? To<T>(object? value) => value != null ? (T)value : default;

	static object?[] ToObjects(ValueTuple<TSource1, TSource2, TSource3, TSource4> values) => [values.Item1, values.Item2, values.Item3, values.Item4];

	/// <summary>
	/// Initializes a new instance of <see cref="FuncMultiConverter{TSource1, TSource2, TSource3, TSource4, TDest}" />.
	/// </summary>
	/// <param name="convert">
	/// The <see cref="Func{ValueTuple, TDest}" /> implementation that will provide the conversion for the underlying <see cref="IMultiValueConverter.Convert" /> method.
	/// </param>
	/// <param name="convertBack">
	/// The <see cref="Func{TDest, ValueTuple}" /> implementation that will provide the conversion for the underlying <see cref="IMultiValueConverter.ConvertBack" /> method.
	/// </param>
	public FuncMultiConverter(Func<ValueTuple<TSource1?, TSource2?, TSource3?, TSource4?>, TDest>? convert = null,
								Func<TDest?, ValueTuple<TSource1, TSource2, TSource3, TSource4>>? convertBack = null)
	: base(convert is null ? default(Func<object[], TDest>) : (object[] values) => convert((To<TSource1>(values[0]), To<TSource2>(values[1]), To<TSource3>(values[2]), To<TSource4>(values[3]))),
			convertBack is null ? default(Func<TDest?, object?[]>) : (TDest? value) => ToObjects(convertBack(value)))
	{

	}
}

/// <summary>
/// An <see cref="IMultiValueConverter" /> implementation that expects 2 known input values and a <c>ConverterParameter</c>.
/// </summary>
/// <typeparam name="TSource1">The type of the <b>first</b> value coming from the source of the <see cref="MultiBinding" />.</typeparam>
/// <typeparam name="TSource2">The type of the <b>second</b> value coming from the source of the <see cref="MultiBinding" />.</typeparam>
/// <typeparam name="TDest">The type of the value going to the target of the <see cref="MultiBinding" />.</typeparam>
/// <typeparam name="TParam">The type of the <c>ConverterParameter</c>.</typeparam>
public class FuncMultiConverterWithParam<TSource1, TSource2, TDest, TParam> : FuncMultiConverter<TDest, TParam>
{
	static T? To<T>(object? value) => value != null ? (T)value : default;

	static object?[] ToObjects(ValueTuple<TSource1, TSource2> values) => [values.Item1, values.Item2];

	/// <summary>
	/// Initializes a new instance of <see cref="FuncMultiConverter{TSource1, TSource2, TDest, TParam}" />.
	/// </summary>
	/// <param name="convert">
	/// The <see cref="Func{ValueTuple, TParam, TDest}" /> implementation that will provide the conversion for the underlying <see cref="IMultiValueConverter.Convert" /> method.
	/// </param>
	/// <param name="convertBack">
	/// The <see cref="Func{TDest, TParam, ValueTuple}" /> implementation that will provide the conversion for the underlying <see cref="IMultiValueConverter.ConvertBack" /> method.
	/// </param>
	public FuncMultiConverterWithParam(Func<ValueTuple<TSource1?, TSource2?>, TParam?, TDest>? convert = null,
										Func<TDest?, TParam?, ValueTuple<TSource1, TSource2>>? convertBack = null)
	: base(convert is null ? default(Func<object[], TParam?, TDest>) : (object[] values, TParam? param) => convert((To<TSource1>(values[0]), To<TSource2>(values[1])), param),
			convertBack is null ? default(Func<TDest?, TParam?, object?[]>) : (TDest? value, TParam? param) => ToObjects(convertBack(value, param)))
	{
	}
}

/// <summary>
/// An <see cref="IMultiValueConverter" /> implementation that expects 3 known input values and a <c>ConverterParameter</c>.
/// </summary>
/// <typeparam name="TSource1">The type of the <b>first</b> value coming from the source of the <see cref="MultiBinding" />.</typeparam>
/// <typeparam name="TSource2">The type of the <b>second</b> value coming from the source of the <see cref="MultiBinding" />.</typeparam>
/// <typeparam name="TSource3">The type of the <b>third</b> value coming from the source of the <see cref="MultiBinding" />.</typeparam>
/// <typeparam name="TDest">The type of the value going to the target of the <see cref="MultiBinding" />.</typeparam>
/// <typeparam name="TParam">The type of the <c>ConverterParameter</c>.</typeparam>
public class FuncMultiConverterWithParam<TSource1, TSource2, TSource3, TDest, TParam> : FuncMultiConverter<TDest, TParam>
{
	static T? To<T>(object value) => value != null ? (T)value : default;

	static object?[] ToObjects(ValueTuple<TSource1, TSource2, TSource3> values) => [values.Item1, values.Item2, values.Item3];

	/// <summary>
	/// Initializes a new instance of <see cref="FuncMultiConverter{TSource1, TSource2, TSource3, TDest, TParam}" />.
	/// </summary>
	/// <param name="convert">
	/// The <see cref="Func{ValueTuple, TParam, TDest}" /> implementation that will provide the conversion for the underlying <see cref="IMultiValueConverter.Convert" /> method.
	/// </param>
	/// <param name="convertBack">
	/// The <see cref="Func{TDest, TParam, ValueTuple}" /> implementation that will provide the conversion for the underlying <see cref="IMultiValueConverter.ConvertBack" /> method.
	/// </param>
	public FuncMultiConverterWithParam(Func<ValueTuple<TSource1?, TSource2?, TSource3?>, TParam?, TDest>? convert = null,
										Func<TDest?, TParam?, ValueTuple<TSource1, TSource2, TSource3>>? convertBack = null)
	: base(convert is null ? default(Func<object[], TParam?, TDest>) : (object[] values, TParam? param) => convert((To<TSource1>(values[0]), To<TSource2>(values[1]), To<TSource3>(values[2])), param),
			convertBack is null ? default(Func<TDest?, TParam?, object?[]>) : (TDest? value, TParam? param) => ToObjects(convertBack(value, param)))
	{

	}
}

/// <summary>
/// An <see cref="IMultiValueConverter" /> implementation that expects 4 known input values and a <c>ConverterParameter</c>.
/// </summary>
/// <typeparam name="TSource1">The type of the <b>first</b> value coming from the source of the <see cref="MultiBinding" />.</typeparam>
/// <typeparam name="TSource2">The type of the <b>second</b> value coming from the source of the <see cref="MultiBinding" />.</typeparam>
/// <typeparam name="TSource3">The type of the <b>third</b> value coming from the source of the <see cref="MultiBinding" />.</typeparam>
/// <typeparam name="TSource4">The type of the <b>fourth</b> value coming from the source of the <see cref="MultiBinding" />.</typeparam>
/// <typeparam name="TDest">The type of the value going to the target of the <see cref="MultiBinding" />.</typeparam>
/// <typeparam name="TParam">The type of the <c>ConverterParameter</c>.</typeparam>
public class FuncMultiConverterWithParam<TSource1, TSource2, TSource3, TSource4, TDest, TParam> : FuncMultiConverter<TDest, TParam>
{
	static T? To<T>(object? value) => value != null ? (T)value : default;

	static object?[] ToObjects(ValueTuple<TSource1, TSource2, TSource3, TSource4> values) => [values.Item1, values.Item2, values.Item3, values.Item4];

	/// <summary>
	/// Initializes a new instance of <see cref="FuncMultiConverterWithParam{TSource1, TSource2, TSource3, TSource4, TDest, TParam}" />.
	/// </summary>
	/// <param name="convert">
	/// The <see cref="Func{ValueTuple, TParam, TDest}" /> implementation that will provide the conversion for the underlying <see cref="IMultiValueConverter.Convert" /> method.
	/// </param>
	/// <param name="convertBack">
	/// The <see cref="Func{TDest, TParam, ValueTuple}" /> implementation that will provide the conversion for the underlying <see cref="IMultiValueConverter.ConvertBack" /> method.
	/// </param>
	public FuncMultiConverterWithParam(Func<ValueTuple<TSource1?, TSource2?, TSource3?, TSource4?>, TParam?, TDest>? convert = null,
										Func<TDest?, TParam?, ValueTuple<TSource1, TSource2, TSource3, TSource4>>? convertBack = null)
	: base(convert is null ? default(Func<object[], TParam?, TDest>) : (object[] values, TParam? param) => convert((To<TSource1>(values[0]), To<TSource2>(values[1]), To<TSource3>(values[2]), To<TSource4>(values[3])), param),
			convertBack is null ? default(Func<TDest?, TParam?, object?[]>) : (TDest? value, TParam? param) => ToObjects(convertBack(value, param)))
	{

	}
}