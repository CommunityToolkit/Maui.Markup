using System.ComponentModel;
using Microsoft.Maui.Controls.Internals;

namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension Methods for Bindable Objects
/// </summary>
public static class TypedBindingExtensions
{
	const string bindingContextPath = Binding.SelfPath;

	/// <summary>Bind to a specified property with inline conversion</summary>
	public static TBindable Bind<TBindable, TBindingContext, TSource, TDest>(
		this TBindable bindable,
		BindableProperty targetProperty,
		Func<TBindingContext, TSource> getter,
		Action<TBindingContext, TSource>? setter = null,
		BindingMode mode = BindingMode.Default,
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
			Converter = converter,
			Mode = mode,
			StringFormat = stringFormat,
			Source = source,
			TargetNullValue = targetNullValue,
			FallbackValue = fallbackValue
		});

		return bindable;
	}

	/// <summary>Bind to a specified property</summary>
	public static TBindable Bind<TBindable, TBindingContext, TSource>(
		this TBindable bindable,
		BindableProperty targetProperty,
		Func<TBindingContext, TSource> getter,
		Action<TBindingContext, TSource>? setter = null,
		BindingMode mode = BindingMode.Default,
		string? stringFormat = null,
		TBindingContext? source = default) where TBindable : BindableObject
	{
		bindable.SetBinding(targetProperty, new TypedBinding<TBindingContext, TSource>(result => (getter(result), true), setter, null)
		{
			Mode = mode,
			StringFormat = stringFormat,
			Source = source,
		});

		return bindable;
	}
}

