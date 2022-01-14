using System;
using Microsoft.Maui.Controls;

namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension Methods for Bindable Objects
/// </summary>
public static class BindableObjectExtensions
{
	const string bindingContextPath = Binding.SelfPath;

	/// <summary>Bind to a specified property</summary>
	public static TBindable Bind<TBindable>(
		this TBindable bindable,
		BindableProperty targetProperty,
		string path = bindingContextPath,
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
	public static TBindable Bind<TBindable, TSource, TDest>(
		this TBindable bindable,
		BindableProperty targetProperty,
		string path = bindingContextPath,
		BindingMode mode = BindingMode.Default,
		Func<TSource?, TDest>? convert = null,
		Func<TDest?, TSource>? convertBack = null,
		string? stringFormat = null,
		object? source = null,
		TDest? targetNullValue = default,
		TDest? fallbackValue = default) where TBindable : BindableObject
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
	public static TBindable Bind<TBindable, TSource, TParam, TDest>(
		this TBindable bindable,
		BindableProperty targetProperty,
		string path = bindingContextPath,
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
	public static TBindable Bind<TBindable>(
		this TBindable bindable,
		string path = bindingContextPath,
		BindingMode mode = BindingMode.Default,
		IValueConverter? converter = null,
		object? converterParameter = null,
		string? stringFormat = null,
		object? source = null,
		object? targetNullValue = null,
		object? fallbackValue = null) where TBindable : BindableObject
	{
		bindable.Bind(
			DefaultBindableProperties.GetFor(bindable),
			path, mode, converter, converterParameter, stringFormat, source, targetNullValue, fallbackValue);

		return bindable;
	}

	/// <summary>Bind to the default property with inline conversion</summary>
	public static TBindable Bind<TBindable, TSource, TDest>(
		this TBindable bindable,
		string path = bindingContextPath,
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
			DefaultBindableProperties.GetFor(bindable),
			path, mode, converter, null, stringFormat, source, targetNullValue, fallbackValue);

		return bindable;
	}

	/// <summary>Bind to the default property with inline conversion and conversion parameter</summary>
	public static TBindable Bind<TBindable, TSource, TParam, TDest>(
		this TBindable bindable,
		string path = bindingContextPath,
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
			DefaultBindableProperties.GetFor(bindable),
			path, mode, converter, converterParameter, stringFormat, source, targetNullValue, fallbackValue);

		return bindable;
	}

	/// <summary>Bind to the <typeparamref name="TBindable"/>'s default Command and CommandParameter properties </summary>
	/// <param name="bindable">The Bindable Object</param>
	/// <param name="path">Binding Path</param>
	/// <param name="source">Binding Source</param>
	/// <param name="parameterPath">If null, no binding is created for the CommandParameter property</param>
	/// <param name="parameterSource">Parameter Binding Source</param>
	public static TBindable BindCommand<TBindable>(
		this TBindable bindable,
		string path = bindingContextPath,
		object? source = null,
		string? parameterPath = bindingContextPath,
		object? parameterSource = null) where TBindable : BindableObject
	{
		(var commandProperty, var parameterProperty) = DefaultBindableProperties.GetForCommand(bindable);

		bindable.SetBinding(commandProperty, new Binding(path: path, source: source));

		if (parameterPath is not null)
		{
			bindable.SetBinding(parameterProperty, new Binding(path: parameterPath, source: parameterSource));
		}

		return bindable;
	}

	/// <summary>
	/// Assign TBindable to a variable
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <typeparam name="TVariable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="variable"></param>
	/// <returns>TBindable</returns>
	public static TBindable Assign<TBindable, TVariable>(this TBindable bindable, out TVariable variable)
		where TBindable : BindableObject, TVariable
	{
		variable = bindable;
		return bindable;
	}

	/// <summary>
	/// Perform an action on a Bindable Object
	/// </summary>
	/// <typeparam name="TBindable"></typeparam>
	/// <param name="bindable"></param>
	/// <param name="action"></param>
	/// <returns>TBindable</returns>
	public static TBindable Invoke<TBindable>(this TBindable bindable, Action<TBindable>? action) where TBindable : BindableObject
	{
		action?.Invoke(bindable);
		return bindable;
	}
}