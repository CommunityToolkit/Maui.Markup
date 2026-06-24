using System.ComponentModel;
using System.Windows.Input;

namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// TypedBinding Extension Methods for Bindable Objects
/// </summary>
public static partial class TypedBindingExtensions
{
	static readonly BindableProperty sourceUpdateHandlersProperty = BindableProperty.CreateAttached(
		"SourceUpdateHandlers",
		typeof(Dictionary<BindableProperty, SourceUpdateHandlers>),
		typeof(TypedBindingExtensions),
		default(Dictionary<BindableProperty, SourceUpdateHandlers>));

	/// <summary>Bind to the <typeparamref name="TBindable"/>'s default Command properties </summary>
	public static TBindable BindCommand<TBindable, TCommandBindingContext>(
		this TBindable bindable,
		Func<TCommandBindingContext, ICommand> getter,
		(Func<TCommandBindingContext, object?>, string)[] handlers,
		Action<TCommandBindingContext, ICommand?>? setter = null,
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
		Action<TCommandBindingContext, ICommand?>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode commandBindingMode = BindingMode.Default,
		Func<TParameterBindingContext, TParameterSource>? parameterGetter = null,
		(Func<TParameterBindingContext, object?>, string)[]? parameterHandlers = null,
		Action<TParameterBindingContext, TParameterSource?>? parameterSetter = null,
		TParameterBindingContext? parameterSource = default,
		BindingMode parameterBindingMode = BindingMode.Default)
		where TBindable : BindableObject
		where TCommandBindingContext : class?
		where TParameterBindingContext : class?
	{
		var (commandProperty, parameterProperty) = DefaultBindableProperties.GetCommandAndCommandParameterProperty<TBindable>();

		SetTypedBinding<TBindable, TCommandBindingContext, ICommand, object?, object?>(bindable,
			commandProperty,
			getter,
			GetMemberPath(handlers),
			setter,
			commandBindingMode,
			source: source);

		if (parameterGetter is not null)
		{
			SetTypedBinding<TBindable, TParameterBindingContext, TParameterSource, object?, object?>(
				bindable,
				parameterProperty,
				parameterGetter,
				parameterHandlers is null ? null : GetMemberPath(parameterHandlers),
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
		Action<TBindingContext, TSource?>? setter = null,
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
		Action<TBindingContext, TSource?>? setter = null,
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
		Action<TBindingContext, TSource?>? setter = null,
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

		return SetTypedBinding(bindable,
			targetProperty,
			getter,
			GetMemberPath(handlers),
			setter,
			mode,
			converter,
			converterParameter,
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
		Action<TBindingContext, TSource?>? setter = null,
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
		return SetTypedBinding(bindable,
			targetProperty,
			getter,
			GetMemberPath(handlers),
			setter,
			mode,
			converter,
			converterParameter,
			stringFormat,
			source,
			targetNullValue,
			fallbackValue);
	}

	/// <summary>Remove a typed binding from a specified property.</summary>
	public static TBindable RemoveTypedBinding<TBindable>(this TBindable bindable, BindableProperty targetProperty)
		where TBindable : BindableObject
	{
		RemoveSourceUpdateHandler(bindable, targetProperty);
		bindable.RemoveBinding(targetProperty);

		return bindable;
	}

	static TBindable SetTypedBinding<TBindable, TBindingContext, TSource, TParam, TDest>(
		TBindable bindable,
		BindableProperty targetProperty,
		Func<TBindingContext, TSource> getter,
		string? path,
		Action<TBindingContext, TSource?>? setter = null,
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
		ArgumentNullException.ThrowIfNull(getter);

		if (path is null && setter is not null)
		{
			throw CreateInvalidGetterException();
		}

		RemoveSourceUpdateHandler(bindable, targetProperty);

		var resolvedMode = ResolveBindingMode(targetProperty, mode);
		var updatesTarget = UpdatesTarget(resolvedMode);
		var isApplyingBinding = false;

		void ApplyBinding()
		{
			if (!updatesTarget)
			{
				bindable.RemoveBinding(targetProperty);
				return;
			}

			try
			{
				isApplyingBinding = true;
				var binding = new Binding(
					path ?? Binding.SelfPath,
					GetTargetUpdateBindingMode(resolvedMode),
					path is null ? new GetterValueConverter<TBindingContext, TSource>(getter, converter) : converter,
					converterParameter,
					stringFormat,
					source)
				{
					TargetNullValue = targetNullValue,
					FallbackValue = fallbackValue
				};

				bindable.SetBinding(targetProperty, binding);
			}
			finally
			{
				isApplyingBinding = false;
			}
		}

		ApplyBinding();

		if (setter is not null && UpdatesSource(resolvedMode))
		{
			var sourceUpdateHandler = CreateSourceUpdateHandler(bindable, targetProperty, getter, setter, converter, converterParameter, source, () => isApplyingBinding, ApplyBinding);
			var bindingContextChangedHandler = source is null && !updatesTarget
				? CreateBindingContextChangedHandler(bindable, targetProperty, getter, setter, converter, converterParameter, source)
				: null;

			SetSourceUpdateHandler(bindable, targetProperty, sourceUpdateHandler, bindingContextChangedHandler);

			if (!updatesTarget)
			{
				UpdateSourceFromTarget(bindable, targetProperty, getter, setter, converter, converterParameter, source);
			}
		}

		return bindable;
	}

	static BindingMode ResolveBindingMode(BindableProperty targetProperty, BindingMode mode)
		=> mode is BindingMode.Default ? targetProperty.DefaultBindingMode : mode;

	static bool UpdatesTarget(BindingMode mode)
		=> mode is BindingMode.OneWay or BindingMode.TwoWay or BindingMode.OneTime;

	static bool UpdatesSource(BindingMode mode)
		=> mode is BindingMode.TwoWay or BindingMode.OneWayToSource;

	static BindingMode GetTargetUpdateBindingMode(BindingMode mode)
		=> mode is BindingMode.OneTime ? BindingMode.OneTime : BindingMode.OneWay;

	static PropertyChangedEventHandler CreateSourceUpdateHandler<TBindingContext, TSource, TParam>(
		BindableObject bindable,
		BindableProperty targetProperty,
		Func<TBindingContext, TSource> getter,
		Action<TBindingContext, TSource?> setter,
		IValueConverter? converter,
		TParam? converterParameter,
		TBindingContext? source,
		Func<bool> isApplyingBinding,
		Action applyBinding)
		where TBindingContext : class?
	{
		return (_, args) =>
		{
			if (isApplyingBinding() || !string.Equals(args.PropertyName, targetProperty.PropertyName, StringComparison.Ordinal))
			{
				return;
			}

			if (!UpdateSourceFromTarget(bindable, targetProperty, getter, setter, converter, converterParameter, source))
			{
				return;
			}

			applyBinding();
		};
	}

	static EventHandler CreateBindingContextChangedHandler<TBindingContext, TSource, TParam>(
		BindableObject bindable,
		BindableProperty targetProperty,
		Func<TBindingContext, TSource> getter,
		Action<TBindingContext, TSource?> setter,
		IValueConverter? converter,
		TParam? converterParameter,
		TBindingContext? source)
		where TBindingContext : class?
	{
		return (_, _) => UpdateSourceFromTarget(bindable, targetProperty, getter, setter, converter, converterParameter, source);
	}

	static bool UpdateSourceFromTarget<TBindingContext, TSource, TParam>(
		BindableObject bindable,
		BindableProperty targetProperty,
		Func<TBindingContext, TSource> getter,
		Action<TBindingContext, TSource?> setter,
		IValueConverter? converter,
		TParam? converterParameter,
		TBindingContext? source)
		where TBindingContext : class?
	{
		var sourceObject = source ?? bindable.BindingContext as TBindingContext;
		if (sourceObject is null || !TryConvertTargetValue<TSource, TParam>(bindable.GetValue(targetProperty), converter, converterParameter, out var sourceValue))
		{
			return false;
		}

		if (!TryGetCurrentSourceValue(sourceObject, getter, out var currentSourceValue) || Equals(currentSourceValue, sourceValue))
		{
			return false;
		}

		setter(sourceObject, sourceValue);
		return true;
	}

	static bool TryGetCurrentSourceValue<TBindingContext, TSource>(
		TBindingContext sourceObject,
		Func<TBindingContext, TSource> getter,
		out TSource sourceValue)
	{
		try
		{
			sourceValue = getter(sourceObject);
			return true;
		}
		catch (Exception ex) when (ex is NullReferenceException or KeyNotFoundException or IndexOutOfRangeException or ArgumentOutOfRangeException)
		{
			sourceValue = default!;
			return false;
		}
	}

	static bool TryConvertTargetValue<TSource, TParam>(object? targetValue, IValueConverter? converter, TParam? converterParameter, out TSource? sourceValue)
	{
		var convertedValue = converter?.ConvertBack(targetValue, typeof(TSource), converterParameter, System.Globalization.CultureInfo.CurrentUICulture) ?? targetValue;

		if (ReferenceEquals(convertedValue, BindableProperty.UnsetValue) || ReferenceEquals(convertedValue, Binding.DoNothing))
		{
			sourceValue = default;
			return false;
		}

		try
		{
			sourceValue = convertedValue is null ? default : ConvertTargetValue<TSource>(convertedValue);
			return true;
		}
		catch (Exception ex) when (ex is InvalidCastException or FormatException or OverflowException or NotSupportedException)
		{
			sourceValue = default;
			return false;
		}
	}

	static TSource ConvertTargetValue<TSource>(object targetValue)
	{
		var sourceType = Nullable.GetUnderlyingType(typeof(TSource)) ?? typeof(TSource);
		if (targetValue is string targetText && string.IsNullOrEmpty(targetText) && Nullable.GetUnderlyingType(typeof(TSource)) is not null)
		{
			return default!;
		}

		return targetValue is TSource sourceValue
			? sourceValue
			: (TSource)System.Convert.ChangeType(targetValue, sourceType, System.Globalization.CultureInfo.CurrentUICulture);
	}

	static void SetSourceUpdateHandler(BindableObject bindable, BindableProperty targetProperty, PropertyChangedEventHandler propertyChangedHandler, EventHandler? bindingContextChangedHandler)
	{
		var handlers = (Dictionary<BindableProperty, SourceUpdateHandlers>?)bindable.GetValue(sourceUpdateHandlersProperty);
		if (handlers is null)
		{
			handlers = [];
			bindable.SetValue(sourceUpdateHandlersProperty, handlers);
		}

		handlers[targetProperty] = new SourceUpdateHandlers(propertyChangedHandler, bindingContextChangedHandler);
		bindable.PropertyChanged += propertyChangedHandler;
		if (bindingContextChangedHandler is not null)
		{
			bindable.BindingContextChanged += bindingContextChangedHandler;
		}
	}

	static void RemoveSourceUpdateHandler(BindableObject bindable, BindableProperty targetProperty)
	{
		var handlers = (Dictionary<BindableProperty, SourceUpdateHandlers>?)bindable.GetValue(sourceUpdateHandlersProperty);
		if (handlers?.Remove(targetProperty, out var handler) is true)
		{
			bindable.PropertyChanged -= handler.PropertyChangedHandler;
			if (handler.BindingContextChangedHandler is not null)
			{
				bindable.BindingContextChanged -= handler.BindingContextChangedHandler;
			}
		}
	}

	sealed record SourceUpdateHandlers(PropertyChangedEventHandler PropertyChangedHandler, EventHandler? BindingContextChangedHandler);

	sealed class GetterValueConverter<TBindingContext, TSource>(Func<TBindingContext, TSource> getter, IValueConverter? converter) : IValueConverter
		where TBindingContext : class?
	{
		public object? Convert(object? value, Type? targetType, object? parameter, System.Globalization.CultureInfo? culture)
		{
			if (value is not TBindingContext sourceObject)
			{
				return BindableProperty.UnsetValue;
			}

			var sourceValue = getter(sourceObject);
			return converter?.Convert(sourceValue, targetType ?? typeof(object), parameter, culture ?? System.Globalization.CultureInfo.CurrentUICulture) ?? sourceValue;
		}

		public object? ConvertBack(object? value, Type? targetType, object? parameter, System.Globalization.CultureInfo? culture)
			=> BindableProperty.UnsetValue;

		public override string? ToString() => converter?.ToString() ?? base.ToString();
	}
}