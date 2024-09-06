using System.Linq.Expressions;
using System.Windows.Input;
namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// TypedBinding Extension Methods for Bindable Objects
/// </summary>
public static partial class TypedBindingExtensions
{
	/// <summary>Bind to the <typeparamref name="TBindable"/>'s default Command properties </summary>
	public static TBindable BindCommand<TBindable, TCommandBindingContext>(
		this TBindable bindable,
		Expression<Func<TCommandBindingContext, ICommand>> getter,
		Action<TCommandBindingContext, ICommand>? setter = null,
		BindingMode mode = BindingMode.Default,
		TCommandBindingContext? source = default)
		where TBindable : BindableObject
		where TCommandBindingContext : class?
	{
		return BindCommand<TBindable, TCommandBindingContext, object?, object?>(
			bindable,
			getter,
			setter,
			source,
			mode);
	}

	/// <summary>Bind to the <typeparamref name="TBindable"/>'s default Command and CommandParameter properties </summary>
	public static TBindable BindCommand<TBindable, TCommandBindingContext, TParameterBindingContext, TParameterSource>(
		this TBindable bindable,
		Expression<Func<TCommandBindingContext, ICommand>> getter,
		Action<TCommandBindingContext, ICommand>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode commandBindingMode = BindingMode.Default,
		Expression<Func<TParameterBindingContext, TParameterSource>>? parameterGetter = null,
		Action<TParameterBindingContext, TParameterSource>? parameterSetter = null,
		BindingMode parameterBindingMode = BindingMode.Default,
		TParameterBindingContext? parameterSource = default)
		where TBindable : BindableObject
		where TCommandBindingContext : class?
		where TParameterBindingContext : class?
	{
		var (commandProperty, parameterProperty) = DefaultBindableProperties.GetCommandAndCommandParameterProperty<TBindable>();

		Bind(bindable,
			commandProperty,
			getter,
			setter,
			commandBindingMode,
			source: source);

		if (parameterGetter is not null)
		{
			Bind(
				bindable,
				parameterProperty,
				parameterGetter,
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
		Expression<Func<TBindingContext, TSource>> getter,
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
		Expression<Func<TBindingContext, TSource>> getter,
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

	/// <summary>Bind to a specified property with inline conversion</summary>
	public static TBindable Bind<TBindable, TBindingContext, TSource, TDest>(
		this TBindable bindable,
		BindableProperty targetProperty,
		Expression<Func<TBindingContext, TSource>> getter,
		Action<TBindingContext, TSource>? setter = null,
		BindingMode mode = BindingMode.Default,
		IValueConverter? converter = null,
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
			setter,
			mode,
			converter,
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
		IValueConverter? converter = null,
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
			converter,
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
		TDest? fallbackValue = default)
		where TBindable : BindableObject
		where TBindingContext : class?

	{
		var getterFunc = ConvertExpressionToFunc(getter);

		return Bind(
			bindable,
			targetProperty,
			getterFunc,
			[(b => b, GetMemberName(getter))],
			setter,
			mode,
			convert,
			convertBack,
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
		Expression<Func<TBindingContext, TSource>> getter,
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
		var getterFunc = ConvertExpressionToFunc(getter);

		return Bind(
			bindable,
			targetProperty,
			getterFunc,
			[(b => b, GetMemberName(getter))],
			setter,
			mode,
			converter,
			converterParameter,
			stringFormat,
			source,
			targetNullValue,
			fallbackValue);
	}

	static Func<TBindingContext, TSource> ConvertExpressionToFunc<TBindingContext, TSource>(in Expression<Func<TBindingContext, TSource>> expression) => expression.Compile();

	static string GetMemberName<T>(in Expression<T> expression) => expression.Body switch
	{
		MemberExpression m => m.Member.Name,
		UnaryExpression { Operand: MemberExpression m } => m.Member.Name,
		_ => throw new InvalidOperationException("Could not retrieve member name")
	};
}