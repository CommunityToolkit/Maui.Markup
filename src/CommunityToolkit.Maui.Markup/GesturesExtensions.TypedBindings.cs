using System.Linq.Expressions;
using System.Windows.Input;

namespace CommunityToolkit.Maui.Markup;

public static partial class GesturesExtensions
{
	/// <summary>Add a <see cref="ClickGestureRecognizer"/> and bind to its Command</summary>
	public static TGestureElement BindClickGesture<TGestureElement, TCommandBindingContext>(
		this TGestureElement gestureElement,
		Expression<Func<TCommandBindingContext, ICommand>> getter,
		Action<TCommandBindingContext, ICommand>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode mode = BindingMode.Default,
		int? numberOfClicksRequired = null) where TGestureElement : BindableObject, IGestureRecognizers
	{
		return BindClickGesture<TGestureElement, TCommandBindingContext, object?, object?>(
				gestureElement,
				getter,
				setter,
				source,
				mode,
				numberOfClicksRequired: numberOfClicksRequired);
	}

	/// <summary>Add a <see cref="ClickGestureRecognizer"/> and bind to its Command and (optionally) CommandParameter properties</summary>
	public static TGestureElement BindClickGesture<TGestureElement, TCommandBindingContext, TParameterBindingContext, TParameterSource>(
		this TGestureElement gestureElement,
		Expression<Func<TCommandBindingContext, ICommand>> getter,
		Action<TCommandBindingContext, ICommand>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode commandBindingMode = BindingMode.Default,
		Expression<Func<TParameterBindingContext, TParameterSource>>? parameterGetter = null,
		Action<TParameterBindingContext, TParameterSource>? parameterSetter = null,
		BindingMode parameterBindingMode = BindingMode.Default,
		TParameterBindingContext? parameterSource = default,
		int? numberOfClicksRequired = null) where TGestureElement : BindableObject, IGestureRecognizers
	{
		var getterFunc = ConvertExpressionToFunc(getter);
		var parameterGetterFunc = parameterGetter switch
		{
			null => null,
			_ => ConvertExpressionToFunc(parameterGetter)
		};

		var parameterGetterHandlers = parameterGetter switch
		{
			null => null,
			_ => new (Func<TParameterBindingContext, object?>, string)[] { ((TParameterBindingContext b) => b, GetMemberName(parameterGetter)) }
		};

		return BindClickGesture(
				gestureElement,
				getterFunc,
				new (Func<TCommandBindingContext, object?>, string)[] { ((TCommandBindingContext b) => b, GetMemberName(getter)) },
				setter,
				source,
				commandBindingMode,
				parameterGetterFunc,
				parameterGetterHandlers,
				parameterSetter,
				parameterBindingMode,
				parameterSource,
				numberOfClicksRequired);
	}

	/// <summary>Add a <see cref="ClickGestureRecognizer"/> and bind to its Command </summary>
	public static TGestureElement BindClickGesture<TGestureElement, TCommandBindingContext>(
		this TGestureElement gestureElement,
		Func<TCommandBindingContext, ICommand> getter,
		(Func<TCommandBindingContext, object?>, string)[] handlers,
		Action<TCommandBindingContext, ICommand>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode mode = BindingMode.Default,
		int? numberOfClicksRequired = null) where TGestureElement : BindableObject, IGestureRecognizers
	{
		return gestureElement.BindClickGesture<TGestureElement, TCommandBindingContext, object?, object?>(
											getter,
											handlers,
											setter,
											source,
											mode,
											numberOfClicksRequired: numberOfClicksRequired);
	}

	/// <summary>Add a <see cref="ClickGestureRecognizer"/> and bind to its Command and (optionally) CommandParameter properties</summary>
	public static TGestureElement BindClickGesture<TGestureElement, TCommandBindingContext, TParameterBindingContext, TParameterSource>(
		this TGestureElement gestureElement,
		Func<TCommandBindingContext, ICommand> getter,
		(Func<TCommandBindingContext, object?>, string)[] handlers,
		Action<TCommandBindingContext, ICommand>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode commandBindingMode = BindingMode.Default,
		Func<TParameterBindingContext, TParameterSource>? parameterGetter = null,
		(Func<TParameterBindingContext, object?>, string)[]? parameterHandlers = null,
		Action<TParameterBindingContext, TParameterSource>? parameterSetter = null,
		BindingMode parameterBindingMode = BindingMode.Default,
		TParameterBindingContext? parameterSource = default,
		int? numberOfClicksRequired = null) where TGestureElement : BindableObject, IGestureRecognizers
	{
		var clickGesture = gestureElement.BindGesture<ClickGestureRecognizer, TGestureElement, TCommandBindingContext, TParameterBindingContext, TParameterSource>(
											getter,
											handlers,
											setter,
											source,
											commandBindingMode,
											parameterGetter,
											parameterHandlers,
											parameterSetter,
											parameterBindingMode,
											parameterSource);

		return gestureElement.ConfigureClickGesture(clickGesture, numberOfClicksRequired);
	}

	/// <summary>Add a <see cref="SwipeGestureRecognizer"/> and bind to its Command </summary>
	public static TGestureElement BindSwipeGesture<TGestureElement, TCommandBindingContext>(
		this TGestureElement gestureElement,
		Expression<Func<TCommandBindingContext, ICommand>> getter,
		Action<TCommandBindingContext, ICommand>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode mode = BindingMode.Default,
		SwipeDirection? direction = null,
		uint? threshold = null) where TGestureElement : BindableObject, IGestureRecognizers
	{
		return BindSwipeGesture<TGestureElement, TCommandBindingContext, object?, object?>(
				gestureElement,
				getter,
				setter,
				source,
				mode,
				direction: direction,
				threshold: threshold);
	}

	/// <summary>Add a <see cref="SwipeGestureRecognizer"/> and bind to its Command and (optionally) CommandParameter properties</summary>
	public static TGestureElement BindSwipeGesture<TGestureElement, TCommandBindingContext, TParameterBindingContext, TParameterSource>(
		this TGestureElement gestureElement,
		Expression<Func<TCommandBindingContext, ICommand>> getter,
		Action<TCommandBindingContext, ICommand>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode commandBindingMode = BindingMode.Default,
		Expression<Func<TParameterBindingContext, TParameterSource>>? parameterGetter = null,
		Action<TParameterBindingContext, TParameterSource>? parameterSetter = null,
		BindingMode parameterBindingMode = BindingMode.Default,
		TParameterBindingContext? parameterSource = default,
		SwipeDirection? direction = null,
		uint? threshold = null) where TGestureElement : BindableObject, IGestureRecognizers
	{
		var getterFunc = ConvertExpressionToFunc(getter);
		var parameterGetterFunc = parameterGetter switch
		{
			null => null,
			_ => ConvertExpressionToFunc(parameterGetter)
		};

		var parameterGetterHandlers = parameterGetter switch
		{
			null => null,
			_ => new (Func<TParameterBindingContext, object?>, string)[] { ((TParameterBindingContext b) => b, GetMemberName(parameterGetter)) }
		};

		return BindSwipeGesture(
				gestureElement,
				getterFunc,
				new (Func<TCommandBindingContext, object?>, string)[] { ((TCommandBindingContext b) => b, GetMemberName(getter)) },
				setter,
				source,
				commandBindingMode,
				parameterGetterFunc,
				parameterGetterHandlers,
				parameterSetter,
				parameterBindingMode,
				parameterSource,
				direction,
				threshold);
	}

	/// <summary>Add a <see cref="SwipeGestureRecognizer"/> and bind to its Command</summary>
	public static TGestureElement BindSwipeGesture<TGestureElement, TCommandBindingContext>(
		this TGestureElement gestureElement,
		Func<TCommandBindingContext, ICommand> getter,
		(Func<TCommandBindingContext, object?>, string)[] handlers,
		Action<TCommandBindingContext, ICommand>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode mode = BindingMode.Default,
		SwipeDirection? direction = null,
		uint? threshold = null) where TGestureElement : BindableObject, IGestureRecognizers
	{
		return gestureElement.BindSwipeGesture<TGestureElement, TCommandBindingContext, object?, object?>(
											getter,
											handlers,
											setter,
											source,
											mode,
											direction: direction,
											threshold: threshold);
	}

	/// <summary>Add a <see cref="SwipeGestureRecognizer"/> and bind to its Command and (optionally) CommandParameter properties</summary>
	public static TGestureElement BindSwipeGesture<TGestureElement, TCommandBindingContext, TParameterBindingContext, TParameterSource>(
		this TGestureElement gestureElement,
		Func<TCommandBindingContext, ICommand> getter,
		(Func<TCommandBindingContext, object?>, string)[] handlers,
		Action<TCommandBindingContext, ICommand>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode commandBindingMode = BindingMode.Default,
		Func<TParameterBindingContext, TParameterSource>? parameterGetter = null,
		(Func<TParameterBindingContext, object?>, string)[]? parameterHandlers = null,
		Action<TParameterBindingContext, TParameterSource>? parameterSetter = null,
		BindingMode parameterBindingMode = BindingMode.Default,
		TParameterBindingContext? parameterSource = default,
		SwipeDirection? direction = null,
		uint? threshold = null) where TGestureElement : BindableObject, IGestureRecognizers
	{
		var clickGesture = gestureElement.BindGesture<SwipeGestureRecognizer, TGestureElement, TCommandBindingContext, TParameterBindingContext, TParameterSource>(
											getter,
											handlers,
											setter,
											source,
											commandBindingMode,
											parameterGetter,
											parameterHandlers,
											parameterSetter,
											parameterBindingMode,
											parameterSource);

		return gestureElement.ConfigureSwipeGesture(clickGesture, direction, threshold);
	}

	/// <summary>Adds a <see cref="TapGestureRecognizer"/> and bind its Command</summary>
	public static TGestureElement BindTapGesture<TGestureElement, TCommandBindingContext>(
		this TGestureElement gestureElement,
		Expression<Func<TCommandBindingContext, ICommand>> getter,
		Action<TCommandBindingContext, ICommand>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode mode = BindingMode.Default,
		int? numberOfTapsRequired = null) where TGestureElement : BindableObject, IGestureRecognizers
	{

		return BindTapGesture<TGestureElement, TCommandBindingContext, object?, object?>(
				gestureElement,
				getter,
				setter,
				source,
				mode,
				numberOfTapsRequired: numberOfTapsRequired);
	}

	/// <summary>Adds a <see cref="TapGestureRecognizer"/> and bind its Command and (optionally) CommandParameter properties</summary>
	public static TGestureElement BindTapGesture<TGestureElement, TCommandBindingContext, TParameterBindingContext, TParameterSource>(
		this TGestureElement gestureElement,
		Expression<Func<TCommandBindingContext, ICommand>> getter,
		Action<TCommandBindingContext, ICommand>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode commandBindingMode = BindingMode.Default,
		Expression<Func<TParameterBindingContext, TParameterSource>>? parameterGetter = null,
		Action<TParameterBindingContext, TParameterSource>? parameterSetter = null,
		BindingMode parameterBindingMode = BindingMode.Default,
		TParameterBindingContext? parameterSource = default,
		int? numberOfTapsRequired = null) where TGestureElement : BindableObject, IGestureRecognizers
	{
		var getterFunc = ConvertExpressionToFunc(getter);
		var parameterGetterFunc = parameterGetter switch
		{
			null => null,
			_ => ConvertExpressionToFunc(parameterGetter)
		};

		var parameterGetterHandlers = parameterGetter switch
		{
			null => null,
			_ => new (Func<TParameterBindingContext, object?>, string)[] { ((TParameterBindingContext b) => b, GetMemberName(parameterGetter)) }
		};

		return BindTapGesture(
				gestureElement,
				getterFunc,
				new (Func<TCommandBindingContext, object?>, string)[] { ((TCommandBindingContext b) => b, GetMemberName(getter)) },
				setter,
				source,
				commandBindingMode,
				parameterGetterFunc,
				parameterGetterHandlers,
				parameterSetter,
				parameterBindingMode,
				parameterSource,
				numberOfTapsRequired);
	}

	/// <summary>Adds a <see cref="TapGestureRecognizer"/> and bind its Command </summary>
	public static TGestureElement BindTapGesture<TGestureElement, TCommandBindingContext>(
		this TGestureElement gestureElement,
		Func<TCommandBindingContext, ICommand> getter,
		(Func<TCommandBindingContext, object?>, string)[] handlers,
		Action<TCommandBindingContext, ICommand>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode mode = BindingMode.Default,
		int? numberOfTapsRequired = null) where TGestureElement : BindableObject, IGestureRecognizers
	{
		return gestureElement.BindTapGesture<TGestureElement, TCommandBindingContext, object?, object?>(
											getter,
											handlers,
											setter,
											source,
											mode,
											numberOfTapsRequired: numberOfTapsRequired);
	}

	/// <summary>Adds a <see cref="TapGestureRecognizer"/> and bind its Command and (optionally) CommandParameter properties</summary>
	public static TGestureElement BindTapGesture<TGestureElement, TCommandBindingContext, TParameterBindingContext, TParameterSource>(
		this TGestureElement gestureElement,
		Func<TCommandBindingContext, ICommand> getter,
		(Func<TCommandBindingContext, object?>, string)[] handlers,
		Action<TCommandBindingContext, ICommand>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode commandBindingMode = BindingMode.Default,
		Func<TParameterBindingContext, TParameterSource>? parameterGetter = null,
		(Func<TParameterBindingContext, object?>, string)[]? parameterHandlers = null,
		Action<TParameterBindingContext, TParameterSource>? parameterSetter = null,
		BindingMode parameterBindingMode = BindingMode.Default,
		TParameterBindingContext? parameterSource = default,
		int? numberOfTapsRequired = null) where TGestureElement : BindableObject, IGestureRecognizers
	{
		var tapGesture = gestureElement.BindGesture<TapGestureRecognizer, TGestureElement, TCommandBindingContext, TParameterBindingContext, TParameterSource>(
											getter,
											handlers,
											setter,
											source,
											commandBindingMode,
											parameterGetter,
											parameterHandlers,
											parameterSetter,
											parameterBindingMode,
											parameterSource);

		return gestureElement.ConfigureTapGesture(tapGesture, numberOfTapsRequired);
	}

	static Func<TBindingContext, TSource> ConvertExpressionToFunc<TBindingContext, TSource>(in Expression<Func<TBindingContext, TSource>> expression) => expression.Compile();

	static string GetMemberName<T>(in Expression<T> expression) => expression.Body switch
	{
		MemberExpression m => m.Member.Name,
		UnaryExpression u when u.Operand is MemberExpression m => m.Member.Name,
		_ => throw new InvalidOperationException("Could not retreive member name")
	};

	static TGestureRecognizer BindGesture<TGestureRecognizer, TGestureElement, TCommandBindingContext, TParameterBindingContext, TParameterSource>(
		this TGestureElement gestureElement,
		Func<TCommandBindingContext, ICommand> getter,
		(Func<TCommandBindingContext, object?>, string)[] handlers,
		Action<TCommandBindingContext, ICommand>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode commandBindingMode = BindingMode.Default,
		Func<TParameterBindingContext, TParameterSource>? parameterGetter = null,
		(Func<TParameterBindingContext, object?>, string)[]? parameterHandlers = null,
		Action<TParameterBindingContext, TParameterSource>? parameterSetter = null,
		BindingMode parameterBindingMode = BindingMode.Default,
		TParameterBindingContext? parameterSource = default) where TGestureElement : IGestureRecognizers
																where TGestureRecognizer : BindableObject, IGestureRecognizer, new()
	{
		var gestureRecognizer = new TGestureRecognizer().BindCommand(getter,
																		handlers,
																		setter,
																		source,
																		commandBindingMode,
																		parameterGetter,
																		parameterHandlers,
																		parameterSetter,
																		parameterSource,
																		parameterBindingMode);
		gestureElement.GestureRecognizers.Add(gestureRecognizer);

		return gestureRecognizer;
	}
}

