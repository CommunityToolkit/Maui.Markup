using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls.Internals;

namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// TypedBinding Extension Methods for Bindable Objects
/// </summary>
public static class TypedBindingExtensions
{
	/// <summary>Bind to a specified property</summary>
	public static TBindable Bind<TBindable, TBindingContext, TSource>(
		this TBindable bindable,
		BindableProperty targetProperty,
		Expression<Func<TBindingContext, TSource>> getter,
		Action<TBindingContext, TSource>? setter = null,
		BindingMode mode = BindingMode.Default,
		string? stringFormat = null,
		TBindingContext? source = default) where TBindable : BindableObject
	{
		return Bind<TBindable, TBindingContext, TSource, object?, object?>(
					bindable,
					targetProperty,
					getter,
					setter,
					mode,
					null,
					null,
					null,
					stringFormat,
					source,
					null,
					null);
	}

	/// <summary>Bind to a specified property with inline conversion</summary>
	public static TBindable Bind<TBindable, TBindingContext, TSource, TDest>(
		this TBindable bindable,
		BindableProperty targetProperty,
		Expression<Func<TBindingContext, TSource>> getter,
		Action<TBindingContext, TSource>? setter = null,
		BindingMode mode = BindingMode.Default,
		Func<TSource?, TDest>? convert = null,
		Func<TDest?, TSource>? convertBack = null,
		string? stringFormat = null,
		TBindingContext? source = default,
		TDest? targetNullValue = default,
		TDest? fallbackValue = default) where TBindable : BindableObject
	{
		return Bind<TBindable, TBindingContext, TSource, object?, TDest>(
					bindable,
					targetProperty,
					getter,
					setter,
					mode,
					convert is null ? null : (source, _) => convert(source),
					convertBack is null ? null : (dest, _) => convertBack(dest),
					null,
					stringFormat,
					source,
					targetNullValue,
					fallbackValue);
	}

	/// <summary>Bind to a specified property with inline conversion and conversion parameter</summary>
	public static TBindable Bind<TBindable, TBindingContext, TSource, TParam, TDest>(
		this TBindable bindable,
		BindableProperty targetProperty,
		Expression<Func<TBindingContext, TSource>> getter,
		Action<TBindingContext, TSource>? setter = null,
		BindingMode mode = BindingMode.Default,
		Func<TSource?, TParam?, TDest>? convert = null,
		Func<TDest?, TParam?, TSource>? convertBack = null,
		TParam? converterParameter = default,
		string? stringFormat = null,
		TBindingContext? source = default,
		TDest? targetNullValue = default,
		TDest? fallbackValue = default) where TBindable : BindableObject
	{
		var converter = (convert, convertBack) switch
		{
			(null, null) => null,
			_ => new FuncConverter<TSource, TDest, TParam>(convert, convertBack)
		};

		var handlers = new List<Tuple<Func<TBindingContext, object?>, string>>
		{
			new Tuple<Func<TBindingContext, object?>, string>((TBindingContext b) => b, GetMemberName(getter))
		};

		bindable.SetBinding(targetProperty, new TypedBinding<TBindingContext, TSource>(result => (getter.Compile()(result), true), setter, handlers.ToArray())
		{
			Mode = mode,
			Converter = converter,
			ConverterParameter = converterParameter,
			StringFormat = stringFormat,
			Source = source,
			TargetNullValue = targetNullValue,
			FallbackValue = fallbackValue
		});

		return bindable;
	}

	static string GetMemberName<T>(Expression<T> expression) => expression.Body switch
	{
		MemberExpression m => m.Member.Name,
		UnaryExpression u when u.Operand is MemberExpression m => m.Member.Name,
		_ => throw new NotImplementedException(expression.GetType().ToString())
	};
}