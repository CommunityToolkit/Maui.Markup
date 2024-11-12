using System.Diagnostics.CodeAnalysis;

namespace CommunityToolkit.Maui.Markup;

public static partial class BindableObjectExtensions
{
	/// <summary>Bind to a specified property</summary>
	[RequiresUnreferencedCode("Using bindings with string paths is not trim safe. Use expression-based binding instead.")]
	public static TBindable Bind<TBindable>(
		this TBindable bindable,
		BindableProperty targetProperty,
		string path = Binding.SelfPath,
		BindingMode mode = BindingMode.Default,
		IValueConverter? converter = null,
		object? converterParameter = null,
		string? stringFormat = null,
		object? source = null,
		object? targetNullValue = null,
		object? fallbackValue = null) where TBindable : BindableObject
	{
		bindable.SetBinding(
			targetProperty,
			new Binding(path, mode, converter, converterParameter, stringFormat, source)
			{
				TargetNullValue = targetNullValue,
				FallbackValue = fallbackValue
			});

		return bindable;
	}

	/// <summary>Bind to a specified property with inline conversion</summary>
	[RequiresUnreferencedCode("Using bindings with string paths is not trim safe. Use expression-based binding instead.")]
	public static TBindable Bind<TBindable, TSource, TDest>(
		this TBindable bindable,
		BindableProperty targetProperty,
		string path = Binding.SelfPath,
		BindingMode mode = BindingMode.Default,
		Func<TSource?, TDest>? convert = null,
		Func<TDest?, TSource>? convertBack = null,
		string? stringFormat = null,
		object? source = null,
		TDest? targetNullValue = default,
		TDest? fallbackValue = default)
		where TBindable : BindableObject
	{
		var converter = new FuncConverter<TSource, TDest, object>(convert, convertBack);
		bindable.SetBinding(
			targetProperty,
			new Binding(path, mode, converter, null, stringFormat, source)
			{
				TargetNullValue = targetNullValue,
				FallbackValue = fallbackValue
			});

		return bindable;
	}

	/// <summary>Bind to a specified property with inline conversion and conversion parameter</summary>
	[RequiresUnreferencedCode("Using bindings with string paths is not trim safe. Use expression-based binding instead.")]
	public static TBindable Bind<TBindable, TSource, TParam, TDest>(
		this TBindable bindable,
		BindableProperty targetProperty,
		string path = Binding.SelfPath,
		BindingMode mode = BindingMode.Default,
		Func<TSource?, TParam?, TDest>? convert = null,
		Func<TDest?, TParam?, TSource>? convertBack = null,
		TParam? converterParameter = default,
		string? stringFormat = null,
		object? source = null,
		TDest? targetNullValue = default,
		TDest? fallbackValue = default) where TBindable : BindableObject
	{
		var converter = new FuncConverter<TSource, TDest, TParam>(convert, convertBack);
		bindable.SetBinding(
			targetProperty,
			new Binding(path, mode, converter, converterParameter, stringFormat, source)
			{
				TargetNullValue = targetNullValue,
				FallbackValue = fallbackValue
			});

		return bindable;
	}

	/// <summary>Bind to the default property</summary>
	[RequiresUnreferencedCode("Using bindings with string paths is not trim safe. Use expression-based binding instead.")]
	public static TBindable Bind<TBindable>(
		this TBindable bindable,
		string path = Binding.SelfPath,
		BindingMode mode = BindingMode.Default,
		IValueConverter? converter = null,
		object? converterParameter = null,
		string? stringFormat = null,
		object? source = null,
		object? targetNullValue = null,
		object? fallbackValue = null) where TBindable : BindableObject
	{
		bindable.Bind(
			DefaultBindableProperties.GetDefaultProperty<TBindable>(),
			path, mode, converter, converterParameter, stringFormat, source, targetNullValue, fallbackValue);

		return bindable;
	}

	/// <summary>Bind to the default property with inline conversion</summary>
	[RequiresUnreferencedCode("Using bindings with string paths is not trim safe. Use expression-based binding instead.")]
	public static TBindable Bind<TBindable, TSource, TDest>(
		this TBindable bindable,
		string path = Binding.SelfPath,
		BindingMode mode = BindingMode.Default,
		Func<TSource?, TDest>? convert = null,
		Func<TDest?, TSource>? convertBack = null,
		string? stringFormat = null,
		object? source = null,
		TDest? targetNullValue = default,
		TDest? fallbackValue = default) where TBindable : BindableObject
	{
		var converter = new FuncConverter<TSource, TDest, object>(convert, convertBack);

		bindable.Bind(
			DefaultBindableProperties.GetDefaultProperty<TBindable>(),
			path, mode, converter, null, stringFormat, source, targetNullValue, fallbackValue);

		return bindable;
	}

	/// <summary>Bind to the default property with inline conversion and conversion parameter</summary>
	[RequiresUnreferencedCode("Using bindings with string paths is not trim safe. Use expression-based binding instead.")]
	public static TBindable Bind<TBindable, TSource, TParam, TDest>(
		this TBindable bindable,
		string path = Binding.SelfPath,
		BindingMode mode = BindingMode.Default,
		Func<TSource?, TParam?, TDest>? convert = null,
		Func<TDest?, TParam?, TSource>? convertBack = null,
		TParam? converterParameter = default,
		string? stringFormat = null,
		object? source = null,
		TDest? targetNullValue = default,
		TDest? fallbackValue = default) where TBindable : BindableObject
	{
		var converter = new FuncConverter<TSource, TDest, TParam>(convert, convertBack);
		bindable.Bind(
			DefaultBindableProperties.GetDefaultProperty<TBindable>(),
			path, mode, converter, converterParameter, stringFormat, source, targetNullValue, fallbackValue);

		return bindable;
	}

	/// <summary>Bind to the <typeparamref name="TBindable"/>'s default Command and CommandParameter properties </summary>
	/// <param name="bindable">The Bindable Object</param>
	/// <param name="path">Binding Path</param>
	/// <param name="source">Binding Source</param>
	/// <param name="parameterPath">If null, no binding is created for the CommandParameter property</param>
	/// <param name="parameterSource">Parameter Binding Source</param>
	[RequiresUnreferencedCode("Using bindings with string paths is not trim safe. Use expression-based binding instead.")]
	public static TBindable BindCommand<TBindable>(
		this TBindable bindable,
		string path = Binding.SelfPath,
		object? source = null,
		string? parameterPath = Binding.SelfPath,
		object? parameterSource = null) where TBindable : BindableObject
	{
		var (commandProperty, parameterProperty) = DefaultBindableProperties.GetCommandAndCommandParameterProperty<TBindable>();

		bindable.SetBinding(commandProperty, new Binding(path: path, source: source));

		if (parameterPath is not null)
		{
			bindable.SetBinding(parameterProperty, new Binding(path: parameterPath, source: parameterSource));
		}

		return bindable;
	}
}