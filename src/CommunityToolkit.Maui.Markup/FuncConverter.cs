using System.Globalization;

namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// An <see cref="IValueConverter" /> implementation that provides the ability to define type safe <c>Func</c> implementations that will be used in the conversion process.
/// </summary>
/// <typeparam name="TSource">The type of the value coming from the source of the <see cref="Binding" />.</typeparam>
/// <typeparam name="TDest">The type of the value going to the target of the <see cref="Binding" />.</typeparam>
/// <typeparam name="TParam">The type of the <c>ConverterParameter</c>.</typeparam>
public class FuncConverter<TSource, TDest, TParam> : IValueConverter
{
	readonly Func<TSource?, TDest?>? convert;
	readonly Func<TDest?, TSource?>? convertBack;

	readonly Func<TSource?, TParam?, TDest?>? convertWithParam;
	readonly Func<TDest?, TParam?, TSource?>? convertBackWithParam;

	readonly Func<TSource?, TParam?, CultureInfo?, TDest?>? convertWithParamAndCulture;
	readonly Func<TDest?, TParam?, CultureInfo?, TSource?>? convertBackWithParamAndCulture;

	/// <summary>
	/// Initializes a new instance of <see cref="FuncConverter{TSource, TDest, TParam}" /> that allows support for acessing the <c>parameter</c> and <see cref="CultureInfo" /> in the conversion.
	/// </summary>
	/// <param name="convertWithParamAndCulture">
	/// The <see cref="Func{TSource, TParam, CultureInfo, TDest}" /> implementation that will provide the conversion for the underlying <see cref="IValueConverter.Convert" /> method.
	/// </param>
	/// <param name="convertBackWithParamAndCulture">
	/// The <see cref="Func{TDest, TParam, CultureInfo, TSource}" /> implementation that will provide the conversion for the underlying <see cref="IValueConverter.ConvertBack" /> method.
	/// </param>
	public FuncConverter(Func<TSource?, TParam?, CultureInfo?, TDest>? convertWithParamAndCulture = null, Func<TDest?, TParam?, CultureInfo?, TSource>? convertBackWithParamAndCulture = null)
	{
		this.convertWithParamAndCulture = convertWithParamAndCulture;
		this.convertBackWithParamAndCulture = convertBackWithParamAndCulture;
	}

	/// <summary>
	/// Initializes a new instance of <see cref="FuncConverter{TSource, TDest, TParam}" /> that allows support for acessing the <c>parameter</c> in the conversion.
	/// </summary>
	/// <param name="convertWithParam">
	/// The <see cref="Func{TSource, TParam, TDest}" /> implementation that will provide the conversion for the underlying <see cref="IValueConverter.Convert" /> method.
	/// </param>
	/// <param name="convertBackWithParam">
	/// The <see cref="Func{TDest, TParam, TSource}" /> implementation that will provide the conversion for the underlying <see cref="IValueConverter.ConvertBack" /> method.
	/// </param>
	public FuncConverter(Func<TSource?, TParam?, TDest>? convertWithParam = null, Func<TDest?, TParam?, TSource>? convertBackWithParam = null)
	{
		this.convertWithParam = convertWithParam;
		this.convertBackWithParam = convertBackWithParam;
	}

	/// <summary>
	/// Initializes a new instance of <see cref="FuncConverter{TSource, TDest, TParam}" />.
	/// </summary>
	/// <param name="convert">
	/// The <see cref="Func{TSource, TDest}" /> implementation that will provide the conversion for the underlying <see cref="IValueConverter.Convert" /> method.
	/// </param>
	/// <param name="convertBack">
	/// The <see cref="Func{TDest, TSource}" /> implementation that will provide the conversion for the underlying <see cref="IValueConverter.ConvertBack" /> method.
	/// </param>
	public FuncConverter(Func<TSource?, TDest?>? convert = null, Func<TDest?, TSource?>? convertBack = null)
	{
		this.convert = convert;
		this.convertBack = convertBack;
	}

	/// <inheritdoc />
	public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
	{
		if (convert != null)
		{
			return convert.Invoke(
				value != null ? (TSource)value : default(TSource));
		}

		if (convertWithParam != null)
		{
			return convertWithParam.Invoke(
				value != null ? (TSource)value : default(TSource),
				parameter != null ? (TParam)parameter : default(TParam));
		}

		if (convertWithParamAndCulture != null)
		{
			return convertWithParamAndCulture.Invoke(
				value != null ? (TSource)value : default(TSource),
				parameter != null ? (TParam)parameter : default(TParam),
				culture);
		}

		return default(TDest);
	}

	/// <inheritdoc />
	public object? ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture)
	{
		if (convertBack != null)
		{
			return convertBack.Invoke(
				value != null ? (TDest)value : default(TDest));
		}

		if (convertBackWithParam != null)
		{
			return convertBackWithParam.Invoke(
				value != null ? (TDest)value : default(TDest),
				parameter != null ? (TParam)parameter : default(TParam));
		}

		if (convertBackWithParamAndCulture != null)
		{
			return convertBackWithParamAndCulture.Invoke(
				value != null ? (TDest)value : default(TDest),
				parameter != null ? (TParam)parameter : default(TParam),
				culture);
		}

		return default(TSource);
	}
}

/// <summary>
/// An <see cref="IValueConverter" /> implementation that provides the ability to define type safe <c>Func</c> implementations that will be used in the conversion process.
/// </summary>
/// <typeparam name="TSource">The type of the value coming from the source of the <see cref="Binding" />.</typeparam>
/// <typeparam name="TDest">The type of the value going to the target of the <see cref="Binding" />.</typeparam>
public class FuncConverter<TSource, TDest> : FuncConverter<TSource, TDest, object>
{
	/// <summary>
	/// Initializes a new instance of <see cref="FuncConverter{TSource, TDest}" />.
	/// </summary>
	/// <param name="convert">
	/// The <see cref="Func{TSource, TDest}" /> implementation that will provide the conversion for the underlying <see cref="IValueConverter.Convert" /> method.
	/// </param>
	/// <param name="convertBack">
	/// The <see cref="Func{TDest, TSource}" /> implementation that will provide the conversion for the underlying <see cref="IValueConverter.ConvertBack" /> method.
	/// </param>
	public FuncConverter(Func<TSource?, TDest>? convert = null, Func<TDest?, TSource>? convertBack = null)
		: base(convert, convertBack)
	{
	}
}

/// <summary>
/// An <see cref="IValueConverter" /> implementation that provides the ability to define type safe <c>Func</c> implementations that will be used in the conversion process.
/// </summary>
/// <typeparam name="TSource">The type of the value coming from the source of the <see cref="Binding" />.</typeparam>
public class FuncConverter<TSource> : FuncConverter<TSource, object, object>
{
	/// <summary>
	/// Initializes a new instance of <see cref="FuncConverter{TSource}" />.
	/// </summary>
	/// <param name="convert">
	/// The <see cref="Func{TSource, Object}" /> implementation that will provide the conversion for the underlying <see cref="IValueConverter.Convert" /> method.
	/// </param>
	/// <param name="convertBack">
	/// The <see cref="Func{Object, TSource}" /> implementation that will provide the conversion for the underlying <see cref="IValueConverter.ConvertBack" /> method.
	/// </param>
	public FuncConverter(Func<TSource?, object>? convert = null, Func<object?, TSource>? convertBack = null)
		: base(convert, convertBack)
	{
	}
}

/// <summary>
/// An <see cref="IValueConverter" /> implementation that provides the ability to define type safe <c>Func</c> implementations that will be used in the conversion process.
/// </summary>
public class FuncConverter : FuncConverter<object, object, object>
{
	/// <summary>
	/// Initializes a new instance of <see cref="FuncConverter" />.
	/// </summary>
	/// <param name="convert">
	/// The <see cref="Func{Object, Object}" /> implementation that will provide the conversion for the underlying <see cref="IValueConverter.Convert" /> method.
	/// </param>
	/// <param name="convertBack">
	/// The <see cref="Func{Object, Object}" /> implementation that will provide the conversion for the underlying <see cref="IValueConverter.ConvertBack" /> method.
	/// </param>
	public FuncConverter(Func<object?, object>? convert = null, Func<object?, object>? convertBack = null)
		: base(convert, convertBack)
	{
	}
}

/// <summary>
/// An <see cref="IValueConverter" /> implementation that performs a <see cref="Object.ToString()" /> on the incoming value.
/// </summary>
public class ToStringConverter : FuncConverter<object, string>
{
	/// <summary>
	/// Initializes a new instance of <see cref="ToStringConverter" />.
	/// </summary>
	/// <param name="format">The format to apply when performing the <see cref="Object.ToString()" /> method call.</param>
	public ToStringConverter(string format = "{0}")
		: base(o => string.Format(CultureInfo.InvariantCulture, format, o))
	{
	}
}

/// <summary>
/// An <see cref="IValueConverter" /> implementation that performs a logical NOT operation on the incoming value.
/// </summary>
public class NotConverter : FuncConverter<bool, bool>
{
	static readonly Lazy<NotConverter> instance = new(() => new NotConverter());

	/// <summary>
	/// Gets a singleton instance of the <see cref="NotConverter" />.
	/// </summary>
	public static NotConverter Instance => instance.Value;

	/// <summary>
	/// Initializes a new instance of <see cref="NotConverter" />.
	/// </summary>
	public NotConverter() : base(t => !t, t => !t)
	{
	}
}