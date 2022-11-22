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
		Func<TBindingContext, TSource> getter,
		Action<TBindingContext, TSource>? setter = null,
		BindingMode? mode = null,
		string? stringFormat = null,
		TBindingContext? source = default) where TBindable : BindableObject
	{
		bindable.SetBinding(targetProperty, new TypedBinding<TBindingContext, TSource>(result => (getter(result), true), setter, null)
		{
			Mode = (setter, mode) switch
			{
				(_, not null) => mode.Value, // Always use the provided mode when given
				(null, null) => BindingMode.OneWay, // When setter is null, binding is read-only; use BindingMode.OneWay to improve performance
				_ => BindingMode.Default // Default to BindingMode.Default
			},
			StringFormat = stringFormat,
			Source = source,
		});

		return bindable;
	}

	/// <summary>Bind to a specified property with inline conversion</summary>
	public static TBindable Bind<TBindable, TBindingContext, TSource, TDest>(
		this TBindable bindable,
		BindableProperty targetProperty,
		Func<TBindingContext, TSource> getter,
		Action<TBindingContext, TSource>? setter = null,
		BindingMode? mode = null,
		Func<TSource?, TDest>? convert = null,
		Func<TDest?, TSource>? convertBack = null,
		string? stringFormat = null,
		TBindingContext? source = default,
		TDest? targetNullValue = default,
		TDest? fallbackValue = default) where TBindable : BindableObject
	{
		var converter = new FuncConverter<TSource, TDest, object>(convert, convertBack);
		bindable.SetBinding(targetProperty, new TypedBinding<TBindingContext, TSource>(result => (getter(result), true), setter, null)
		{
			Mode = (setter, mode) switch
			{
				(_, not null) => mode.Value, // Always use the provided mode when given
				(null, null) => BindingMode.OneWay, // When setter is null, binding is read-only; use BindingMode.OneWay to improve performance
				_ => BindingMode.Default // Default to BindingMode.Default
			},
			Converter = converter,
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
		Action<TBindingContext, TSource>? setter = null,
		BindingMode? mode = null,
		Func<TSource?, TParam?, TDest>? convert = null,
		Func<TDest?, TParam?, TSource>? convertBack = null,
		TParam? converterParameter = default,
		string? stringFormat = null,
		TBindingContext? source = default,
		TDest? targetNullValue = default,
		TDest? fallbackValue = default) where TBindable : BindableObject
	{
		var converter = new FuncConverter<TSource, TDest, TParam>(convert, convertBack);
		bindable.SetBinding(targetProperty, new TypedBinding<TBindingContext, TSource>(result => (getter(result), true), setter, null)
		{
			Mode = (setter, mode) switch
			{
				(_, not null) => mode.Value, // Always use the provided mode when given
				(null, null) => BindingMode.OneWay, // When setter is null, binding is read-only; use BindingMode.OneWay to improve performance
				_ => BindingMode.Default // Default to BindingMode.Default
			},
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