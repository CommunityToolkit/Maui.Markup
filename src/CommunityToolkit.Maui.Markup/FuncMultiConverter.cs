using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// IMultiValueConverter for Multiple Func
/// </summary>
/// <typeparam name="TDest"></typeparam>
/// <typeparam name="TParam"></typeparam>
public class FuncMultiConverter<TDest, TParam> : IMultiValueConverter
{
    readonly Func<object[], TDest>? convert;
    readonly Func<TDest?, object?[]>? convertBack;

    readonly Func<object[], TParam?, TDest>? convertWithParam;
    readonly Func<TDest?, TParam?, object?[]>? convertBackWithParam;

    readonly Func<object[], TParam?, CultureInfo, TDest>? convertWithParamAndCulture;
    readonly Func<TDest?, TParam?, CultureInfo, object[]>? convertBackWithParamAndCulture;

    /// <summary>
    /// Initialize FuncMultiComverter
    /// </summary>
    /// <param name="convertWithParamAndCulture"></param>
    /// <param name="convertBackWithParamAndCulture"></param>
    public FuncMultiConverter(Func<object[], TParam?, CultureInfo, TDest>? convertWithParamAndCulture = null, Func<TDest?, TParam?, CultureInfo, object[]>? convertBackWithParamAndCulture = null)
    {
        this.convertWithParamAndCulture = convertWithParamAndCulture;
        this.convertBackWithParamAndCulture = convertBackWithParamAndCulture;
    }

    /// <summary>
    /// Initialize FuncMultiComverter
    /// </summary>
    /// <param name="convertWithParam"></param>
    /// <param name="convertBackWithParam"></param>
    public FuncMultiConverter(Func<object[], TParam?, TDest>? convertWithParam = null, Func<TDest?, TParam?, object?[]>? convertBackWithParam = null)
    {
        this.convertWithParam = convertWithParam;
        this.convertBackWithParam = convertBackWithParam;
    }

    /// <summary>
    /// Initialize FuncMultiComverter
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="convertBack"></param>
    public FuncMultiConverter(Func<object[], TDest>? convert = null, Func<TDest?, object?[]>? convertBack = null)
    {
        this.convert = convert;
        this.convertBack = convertBack;
    }

    /// <summary>
    /// Execute FuncMultiConverter
    /// </summary>
    /// <param name="values"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public object? Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (convert != null)
            return convert(values);

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

    /// <summary>
    /// Reverse Execute FuncMultiConverter
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetTypes"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
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
/// IMultiValueConverter for Multiple Func
/// </summary>
/// <typeparam name="TSource1"></typeparam>
/// <typeparam name="TSource2"></typeparam>
/// <typeparam name="TDest"></typeparam>
public class FuncMultiConverter<TSource1, TSource2, TDest> : FuncMultiConverter<TDest, object>
{
    static T? To<T>(object? value) => value != null ? (T)value : default;

    static object?[] ToObjects(ValueTuple<TSource1, TSource2> values) => new object?[] { values.Item1, values.Item2 };

    /// <summary>
    /// Initialize FuncMultiConverter
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="convertBack"></param>
    public FuncMultiConverter(Func<ValueTuple<TSource1?, TSource2?>, TDest>? convert = null,
                                Func<TDest?, ValueTuple<TSource1, TSource2>>? convertBack = null)
    : base(convert is null ? default(Func<object[], TDest>) : (object[] values) => convert((To<TSource1>(values[0]), To<TSource2>(values[1]))),
            convertBack is null ? default(Func<TDest?, object?[]>) : (TDest? value) => ToObjects(convertBack(value)))
    {

    }
}

/// <summary>
/// IMultiValueConverter for Multiple Func
/// </summary>
/// <typeparam name="TSource1"></typeparam>
/// <typeparam name="TSource2"></typeparam>
/// <typeparam name="TSource3"></typeparam>
/// <typeparam name="TDest"></typeparam>
public class FuncMultiConverter<TSource1, TSource2, TSource3, TDest> : FuncMultiConverter<TDest, object>
{
    static T? To<T>(object? value) => value != null ? (T)value : default;

    static object?[] ToObjects(ValueTuple<TSource1, TSource2, TSource3> values) => new object?[] { values.Item1, values.Item2, values.Item3 };

    /// <summary>
    /// Initialize FuncMultiConverter
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="convertBack"></param>
    public FuncMultiConverter(Func<ValueTuple<TSource1?, TSource2?, TSource3?>, TDest>? convert = null,
                                Func<TDest?, ValueTuple<TSource1, TSource2, TSource3>>? convertBack = null)
    : base(convert is null ? default(Func<object[], TDest>) : (object[] values) => convert((To<TSource1>(values[0]), To<TSource2>(values[1]), To<TSource3>(values[2]))),
            convertBack is null ? default(Func<TDest?, object?[]>) : (TDest? value) => ToObjects(convertBack(value)))
    {

    }
}

/// <summary>
/// IMultiValueConverter for Multiple Func
/// </summary>
/// <typeparam name="TSource1"></typeparam>
/// <typeparam name="TSource2"></typeparam>
/// <typeparam name="TSource3"></typeparam>
/// <typeparam name="TSource4"></typeparam>
/// <typeparam name="TDest"></typeparam>
public class FuncMultiConverter<TSource1, TSource2, TSource3, TSource4, TDest> : FuncMultiConverter<TDest, object>
{
    static T? To<T>(object? value) => value != null ? (T)value : default;

    static object?[] ToObjects(ValueTuple<TSource1, TSource2, TSource3, TSource4> values) => new object?[] { values.Item1, values.Item2, values.Item3, values.Item4 };

    /// <summary>
    /// Initialize FuncMultiConverter
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="convertBack"></param>
    public FuncMultiConverter(Func<ValueTuple<TSource1?, TSource2?, TSource3?, TSource4?>, TDest>? convert = null,
                                Func<TDest?, ValueTuple<TSource1, TSource2, TSource3, TSource4>>? convertBack = null)
    : base(convert is null ? default(Func<object[], TDest>) : (object[] values) => convert((To<TSource1>(values[0]), To<TSource2>(values[1]), To<TSource3>(values[2]), To<TSource4>(values[3]))),
            convertBack is null ? default(Func<TDest?, object?[]>) : (TDest? value) => ToObjects(convertBack(value)))
    {

    }
}

/// <summary>
/// IMultiValueConverter for Multiple Func
/// </summary>
/// <typeparam name="TSource1"></typeparam>
/// <typeparam name="TSource2"></typeparam>
/// <typeparam name="TDest"></typeparam>
/// <typeparam name="TParam"></typeparam>
public class FuncMultiConverterWithParam<TSource1, TSource2, TDest, TParam> : FuncMultiConverter<TDest, TParam>
{
    static T? To<T>(object? value) => value != null ? (T)value : default;

    static object?[] ToObjects(ValueTuple<TSource1, TSource2> values) => new object?[] { values.Item1, values.Item2 };

    /// <summary>
    /// Initialize FuncMultiConverterWithParam
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="convertBack"></param>
    public FuncMultiConverterWithParam(Func<ValueTuple<TSource1?, TSource2?>, TParam?, TDest>? convert = null,
                                        Func<TDest?, TParam?, ValueTuple<TSource1, TSource2>>? convertBack = null)
    : base(convert is null ? default(Func<object[], TParam?, TDest>) : (object[] values, TParam? param) => convert((To<TSource1>(values[0]), To<TSource2>(values[1])), param),
            convertBack is null ? default(Func<TDest?, TParam?, object?[]>) : (TDest? value, TParam? param) => ToObjects(convertBack(value, param)))
    {
    }
}

/// <summary>
/// IMultiValueConverter for Multiple Func
/// </summary>
/// <typeparam name="TSource1"></typeparam>
/// <typeparam name="TSource2"></typeparam>
/// <typeparam name="TSource3"></typeparam>
/// <typeparam name="TDest"></typeparam>
/// <typeparam name="TParam"></typeparam>
public class FuncMultiConverterWithParam<TSource1, TSource2, TSource3, TDest, TParam> : FuncMultiConverter<TDest, TParam>
{
    static T? To<T>(object value) => value != null ? (T)value : default;

    static object?[] ToObjects(ValueTuple<TSource1, TSource2, TSource3> values) => new object?[] { values.Item1, values.Item2, values.Item3 };

    /// <summary>
    /// Initialize FuncMultiConverterWithParam
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="convertBack"></param>
    public FuncMultiConverterWithParam(Func<ValueTuple<TSource1?, TSource2?, TSource3?>, TParam?, TDest>? convert = null,
                                        Func<TDest?, TParam?, ValueTuple<TSource1, TSource2, TSource3>>? convertBack = null)
    : base(convert is null ? default(Func<object[], TParam?, TDest>) : (object[] values, TParam? param) => convert((To<TSource1>(values[0]), To<TSource2>(values[1]), To<TSource3>(values[2])), param),
            convertBack is null ? default(Func<TDest?, TParam?, object?[]>) : (TDest? value, TParam? param) => ToObjects(convertBack(value, param)))
    { 
    
    }
}

/// <summary>
/// IMultiValueConverter for Multiple Func
/// </summary>
/// <typeparam name="TSource1"></typeparam>
/// <typeparam name="TSource2"></typeparam>
/// <typeparam name="TSource3"></typeparam>
/// <typeparam name="TSource4"></typeparam>
/// <typeparam name="TDest"></typeparam>
/// <typeparam name="TParam"></typeparam>
public class FuncMultiConverterWithParam<TSource1, TSource2, TSource3, TSource4, TDest, TParam> : FuncMultiConverter<TDest, TParam>
{
    static T? To<T>(object? value) => value != null ? (T)value : default;

    static object?[] ToObjects(ValueTuple<TSource1, TSource2, TSource3, TSource4> values) => new object?[] { values.Item1, values.Item2, values.Item3, values.Item4 };

    /// <summary>
    /// Initialize FuncMultiConverterWithParam
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="convertBack"></param>
    public FuncMultiConverterWithParam(Func<ValueTuple<TSource1?, TSource2?, TSource3?, TSource4?>, TParam?, TDest>? convert = null,
                                        Func<TDest?, TParam?, ValueTuple<TSource1, TSource2, TSource3, TSource4>>? convertBack = null)
    : base(convert is null ? default(Func<object[], TParam?, TDest>) : (object[] values, TParam? param) => convert((To<TSource1>(values[0]), To<TSource2>(values[1]), To<TSource3>(values[2]), To<TSource4>(values[3])), param),
            convertBack is null ? default(Func<TDest?, TParam?, object?[]>) : (TDest? value, TParam? param) => ToObjects(convertBack(value, param)))
    { 

    }
}
