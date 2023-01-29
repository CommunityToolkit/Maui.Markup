using System.Linq.Expressions;
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

	/// <summary>Bind to a specified property</summary>
	public static TBindable Bind<TBindable, TBindingContext, TSource>(
		this TBindable bindable,
		BindableProperty targetProperty,
		Func<TBindingContext, TSource> getter,
		(Func<TBindingContext, object?>, string)[] handlers,
		Action<TBindingContext, TSource>? setter = null,
		BindingMode mode = BindingMode.Default,
		string? stringFormat = null,
		TBindingContext? source = default) where TBindable : BindableObject
	{
		return Bind<TBindable, TBindingContext, TSource, object?, object?>(
					bindable,
					targetProperty,
					getter,
					handlers,
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
	public static TBindable dBind<TBindable, TBindingContext, TSource, TDest>(
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

	/// <summary>Bind to a specified property with inline conversion</summary>
	public static TBindable Bind<TBindable, TBindingContext, TSource, TDest>(
		this TBindable bindable,
		BindableProperty targetProperty,
		Func<TBindingContext, TSource> getter,
		(Func<TBindingContext, object?>, string)[] handlers,
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
					handlers,
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
		var getterFunc = convertExpressionToFunc(getter);

		return Bind<TBindable, TBindingContext, TSource, TParam, TDest>(
				bindable,
				targetProperty,
				getterFunc,
				new (Func<TBindingContext, object?>, string)[] { ((TBindingContext b) => b, GetMemberName(getter)) },
				setter,
				mode,
				convert,
				convertBack,
				converterParameter,
				stringFormat,
				source,
				targetNullValue,
				fallbackValue);

		static Func<TBindingContext, TSource> convertExpressionToFunc(in Expression<Func<TBindingContext, TSource>> expression) => expression.Compile();

		static string GetMemberName<T>(in Expression<T> expression) => expression.Body switch
		{
			MemberExpression m => m.Member.Name,
			UnaryExpression u when u.Operand is MemberExpression m => m.Member.Name,
			_ => throw new InvalidOperationException("Could not retreive member name")
		};
	}

	/// <summary>Bind to a specified property with inline conversion and conversion parameter</summary>
	public static TBindable Bind<TBindable, TBindingContext, TSource, TParam, TDest>(
		this TBindable bindable,
		BindableProperty targetProperty,
		Func<TBindingContext, TSource> getter,
		(Func<TBindingContext, object?>, string)[] handlers,
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

		bindable.SetBinding(targetProperty, new TypedBinding<TBindingContext, TSource>(bindingContext => (getter(bindingContext), true), setter, handlers.Select(x => x.ToTuple()).ToArray())
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
}