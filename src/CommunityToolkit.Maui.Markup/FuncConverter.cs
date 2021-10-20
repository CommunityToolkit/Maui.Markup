using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// IValueConverter Function
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <typeparam name="TDest"></typeparam>
/// <typeparam name="TParam"></typeparam>
public class FuncConverter<TSource, TDest, TParam> : IValueConverter
{
    readonly Func<TSource?, TDest?>? convert;
    readonly Func<TDest?, TSource?>? convertBack;

    readonly Func<TSource?, TParam?, TDest?>? convertWithParam;
    readonly Func<TDest?, TParam?, TSource?>? convertBackWithParam;

    readonly Func<TSource?, TParam?, CultureInfo?, TDest?>? convertWithParamAndCulture;
    readonly Func<TDest?, TParam?, CultureInfo?, TSource?>? convertBackWithParamAndCulture;

    /// <summary>
    /// Initialize FuncConverter
    /// </summary>
    /// <param name="convertWithParamAndCulture"></param>
    /// <param name="convertBackWithParamAndCulture"></param>
    public FuncConverter(Func<TSource?, TParam?, CultureInfo?, TDest>? convertWithParamAndCulture = null, Func<TDest?, TParam?, CultureInfo?, TSource>? convertBackWithParamAndCulture = null)
    {
        this.convertWithParamAndCulture = convertWithParamAndCulture; 
        this.convertBackWithParamAndCulture = convertBackWithParamAndCulture;
    }

    /// <summary>
    /// Initialize FuncConverter
    /// </summary>
    /// <param name="convertWithParam"></param>
    /// <param name="convertBackWithParam"></param>
    public FuncConverter(Func<TSource?, TParam?, TDest>? convertWithParam = null, Func<TDest?, TParam?, TSource>? convertBackWithParam = null)
    {
        this.convertWithParam = convertWithParam;
        this.convertBackWithParam = convertBackWithParam;
    }

    /// <summary>
    /// Initialize FuncConverter
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="convertBack"></param>
    public FuncConverter(Func<TSource?, TDest?>? convert = null, Func<TDest?, TSource?>? convertBack = null)
    {
        this.convert = convert;
        this.convertBack = convertBack;
    }

    /// <summary>
    /// Execute FuncConverter
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Execute FuncConverter
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
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
/// IValueConverter Function
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <typeparam name="TDest"></typeparam>
public class FuncConverter<TSource, TDest> : FuncConverter<TSource, TDest, object>
{
    /// <summary>
    /// Initialize FuncConverter
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="convertBack"></param>
    public FuncConverter(Func<TSource?, TDest>? convert = null, Func<TDest?, TSource>? convertBack = null)
        : base(convert, convertBack)
    {
    }
}

/// <summary>
/// IValueConverter Function
/// </summary>
/// <typeparam name="TSource"></typeparam>
public class FuncConverter<TSource> : FuncConverter<TSource, object, object>
{
    /// <summary>
    /// Initialize FuncConverter
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="convertBack"></param>
    public FuncConverter(Func<TSource?, object>? convert = null, Func<object?, TSource>? convertBack = null)
        : base(convert, convertBack)
    {
    }
}

/// <summary>
/// IValueConverter Function
/// </summary>
public class FuncConverter : FuncConverter<object, object, object>
{
    /// <summary>
    /// Initialize FuncConverter
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="convertBack"></param>
    public FuncConverter(Func<object?, object>? convert = null, Func<object?, object>? convertBack = null)
        : base(convert, convertBack)
    {
    }
}

/// <summary>
/// String Converter
/// </summary>
public class ToStringConverter : FuncConverter<object, string>
{
    /// <summary>
    /// Initialize ToString Converter
    /// </summary>
    /// <param name="format"></param>
    public ToStringConverter(string format = "{0}")
        : base(o => string.Format(CultureInfo.InvariantCulture, format, o))
    {
    }
}

/// <summary>
/// Not Converter
/// </summary>
public class NotConverter : FuncConverter<bool, bool>
{
    static readonly Lazy<NotConverter> instance = new(() => new NotConverter());

    /// <summary>
    /// Singleton Instance
    /// </summary>
    public static NotConverter Instance => instance.Value;

    /// <summary>
    /// Initialize NotConverter
    /// </summary>
    public NotConverter() : base(t => !t, t => !t)
    {
    }
}
