using System.Windows.Input;
using Microsoft.Maui.Controls.Internals;
namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// TypedBinding Extension Methods for Bindable Objects
/// </summary>
public static partial class TypedBindingExtensions
{
	/// <summary>Bind to the <typeparamref name="TBindable"/>'s default Command properties </summary>
	public static TBindable BindCommand<TBindable, TCommandBindingContext>(
		this TBindable bindable,
		Func<TCommandBindingContext, ICommand> getter,
		(Func<TCommandBindingContext, object?>, string)[] handlers,
		Action<TCommandBindingContext, ICommand>? setter = null,
		BindingMode mode = BindingMode.Default,
		TCommandBindingContext? source = default) 
		where TBindable : BindableObject
		where TCommandBindingContext : class?
	{
		return BindCommand<TBindable, TCommandBindingContext, object?, object?>(
			bindable,
			getter,
			handlers,
			setter,
			source,
			mode);
	}

	/// <summary>Bind to the <typeparamref name="TBindable"/>'s default Command and CommandParameter properties </summary>
	public static TBindable BindCommand<TBindable, TCommandBindingContext, TParameterBindingContext, TParameterSource>(
		this TBindable bindable,
		Func<TCommandBindingContext, ICommand> getter,
		(Func<TCommandBindingContext, object?>, string)[] handlers,
		Action<TCommandBindingContext, ICommand>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode commandBindingMode = BindingMode.Default,
		Func<TParameterBindingContext, TParameterSource>? parameterGetter = null,
		(Func<TParameterBindingContext, object?>, string)[]? parameterHandlers = null,
		Action<TParameterBindingContext, TParameterSource>? parameterSetter = null,
		TParameterBindingContext? parameterSource = default,
		BindingMode parameterBindingMode = BindingMode.Default) 
		where TBindable : BindableObject
		where TCommandBindingContext : class?
		where TParameterBindingContext : class?
	{
		var (commandProperty, parameterProperty) = DefaultBindableProperties.GetCommandAndCommandParameterProperty<TBindable>();

		Bind(bindable,
			commandProperty,
			getter,
			handlers,
			setter,
			commandBindingMode,
			source: source);

		if (parameterGetter is not null)
		{
			ArgumentNullException.ThrowIfNull(parameterHandlers);

			Bind(
				bindable,
				parameterProperty,
				parameterGetter,
				parameterHandlers,
				parameterSetter,
				parameterBindingMode,
				source: parameterSource);
		}

		return bindable;
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
		TBindingContext? source = default)
		where TBindable : BindableObject
		where TBindingContext : class?
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
			source);
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
		TDest? fallbackValue = default)
		where TBindable : BindableObject
		where TBindingContext : class?
	{
		return Bind<TBindable, TBindingContext, TSource, object?, TDest>(
			bindable,
			targetProperty,
			getter,
			handlers,
			setter,
			mode,
			convert is null ? null : (sourceType, _) => convert(sourceType),
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
		TDest? fallbackValue = default)
		where TBindable : BindableObject
		where TBindingContext : class?
		
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

	/// <summary>Bind to a specified property with inline conversion and conversion parameter</summary>
	public static TBindable Bind<TBindable, TBindingContext, TSource, TParam, TDest>(
		this TBindable bindable,
		BindableProperty targetProperty,
		Func<TBindingContext, TSource> getter,
		(Func<TBindingContext, object?>, string)[] handlers,
		Action<TBindingContext, TSource>? setter = null,
		BindingMode mode = BindingMode.Default,
		IValueConverter? converter = null,
		TParam? converterParameter = default,
		string? stringFormat = null,
		TBindingContext? source = default,
		TDest? targetNullValue = default,
		TDest? fallbackValue = default)
		where TBindable : BindableObject
		where TBindingContext : class?
		

	{
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