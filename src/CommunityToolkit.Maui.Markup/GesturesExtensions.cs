using System.Linq.Expressions;
using System.Windows.Input;
namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension Methods for Element Gestures
/// </summary>
public static class GesturesExtensions
{
	/// <summary>Add a <see cref="SwipeGestureRecognizer"/> and bind to its Command </summary>
	public static TGestureElement BindSwipeGesture<TGestureElement, TCommandBindingContext>(
		this TGestureElement gestureElement,
		Expression<Func<TCommandBindingContext, ICommand>> getter,
		Action<TCommandBindingContext, ICommand>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode mode = BindingMode.Default,
		SwipeDirection? direction = null,
		uint? threshold = null)
		where TGestureElement : BindableObject, IGestureRecognizers
		where TCommandBindingContext : class
	{
		return BindSwipeGesture<TGestureElement, TCommandBindingContext, object, object?>(
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
		uint? threshold = null)
		where TGestureElement : BindableObject, IGestureRecognizers
		where TCommandBindingContext : class
		where TParameterBindingContext : class
	{
		var getterFunc = ConvertExpressionToFunc(getter);
		var parameterGetterFunc = parameterGetter switch
		{
			null => null,
			_ => ConvertExpressionToFunc(parameterGetter)
		};

		(Func<TParameterBindingContext, object?>, string)[]? parameterGetterHandlers = parameterGetter switch
		{
			null => null,
			_ => [(b => b, GetMemberName(parameterGetter))]
		};

		return BindSwipeGesture(
			gestureElement,
			getterFunc,
			[
				(b => b, GetMemberName(getter))
			],
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
		uint? threshold = null)
		where TGestureElement : BindableObject, IGestureRecognizers
		where TCommandBindingContext : class
	{
		return gestureElement.BindSwipeGesture<TGestureElement, TCommandBindingContext, object, object?>(
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
		uint? threshold = null)
		where TGestureElement : BindableObject, IGestureRecognizers
		where TCommandBindingContext : class
		where TParameterBindingContext : class
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
		int? numberOfTapsRequired = null)
		where TGestureElement : BindableObject, IGestureRecognizers
		where TCommandBindingContext : class
	{

		return BindTapGesture<TGestureElement, TCommandBindingContext, object, object?>(
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
		int? numberOfTapsRequired = null)
		where TGestureElement : BindableObject, IGestureRecognizers
		where TCommandBindingContext : class
		where TParameterBindingContext : class
	{
		var getterFunc = ConvertExpressionToFunc(getter);
		var parameterGetterFunc = parameterGetter switch
		{
			null => null,
			_ => ConvertExpressionToFunc(parameterGetter)
		};

		(Func<TParameterBindingContext, object?>, string)[]? parameterGetterHandlers = parameterGetter switch
		{
			null => null,
			_ => [(b => b, GetMemberName(parameterGetter))]
		};

		return BindTapGesture(
			gestureElement,
			getterFunc,
			[
				(b => b, GetMemberName(getter))
			],
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
		int? numberOfTapsRequired = null)
		where TGestureElement : BindableObject, IGestureRecognizers
		where TCommandBindingContext : class
	{
		return gestureElement.BindTapGesture<TGestureElement, TCommandBindingContext, object, object?>(
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
		int? numberOfTapsRequired = null)
		where TGestureElement : BindableObject, IGestureRecognizers
		where TCommandBindingContext : class
		where TParameterBindingContext : class
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
	
	/// <summary>
	/// Adds a <see cref="PanGestureRecognizer"/>
	/// </summary>
	/// <param name="gestureElement">An <see cref="Element"/> implementing <see cref="IGestureRecognizers"/></param>
	/// <param name="onPanUpdated"><see cref="Action"/> that invokes when the <see cref="PanGestureRecognizer.PanUpdated"/> event is invoked</param>
	/// <param name="touchPoints">Number of touch points in the gesture</param>
	/// <returns><paramref name="gestureElement"/></returns>
	public static TGestureElement PanGesture<TGestureElement>(this TGestureElement gestureElement,
																EventHandler<PanUpdatedEventArgs>? onPanUpdated = null,
																int? touchPoints = null) where TGestureElement : IGestureRecognizers
	{
		var gestureRecognizer = new PanGestureRecognizer();

		if (onPanUpdated is not null)
		{
			gestureRecognizer.PanUpdated += onPanUpdated;
		}

		if (touchPoints is not null)
		{
			gestureRecognizer.TouchPoints = touchPoints.Value;
		}

		gestureElement.GestureRecognizers.Add(gestureRecognizer);

		return gestureElement;
	}

	/// <summary>
	/// Adds a <see cref="PinchGestureRecognizer"/>
	/// </summary>
	/// <param name="gestureElement">An <see cref="Element"/> implementing <see cref="IGestureRecognizers"/></param>
	/// <param name="onPinchGestureUpdated"><see cref="Action"/> that invokes when the <see cref="PinchGestureRecognizer.PinchUpdated"/> event is invoked</param>
	/// <returns><paramref name="gestureElement"/></returns>
	public static TGestureElement PinchGesture<TGestureElement>(this TGestureElement gestureElement,
																EventHandler<PinchGestureUpdatedEventArgs>? onPinchGestureUpdated = null) where TGestureElement : IGestureRecognizers
	{
		var gestureRecognizer = new PinchGestureRecognizer();

		if (onPinchGestureUpdated is not null)
		{
			gestureRecognizer.PinchUpdated += onPinchGestureUpdated;
		}

		gestureElement.GestureRecognizers.Add(gestureRecognizer);

		return gestureElement;
	}

	/// <summary>
	/// Adds a <see cref="SwipeGestureRecognizer"/>
	/// </summary>
	/// <param name="gestureElement">An <see cref="Element"/> implementing <see cref="IGestureRecognizers"/></param>
	/// <param name="onSwiped"><see cref="Action"/> that invokes when the <see cref="SwipeGestureRecognizer.Swiped"/> event is invoked</param>
	/// <param name="direction">Swipe gesture direction</param>
	/// <param name="threshold">Minimum swipe distance that will cause the gesture to be recognized</param>
	/// <returns><paramref name="gestureElement"/></returns>
	public static TGestureElement SwipeGesture<TGestureElement>(this TGestureElement gestureElement,
																EventHandler<SwipedEventArgs>? onSwiped = null,
																SwipeDirection? direction = null,
																uint? threshold = null) where TGestureElement : IGestureRecognizers
	{
		var gestureRecognizer = new SwipeGestureRecognizer();

		if (onSwiped is not null)
		{
			gestureRecognizer.Swiped += onSwiped;
		}

		if (direction is not null)
		{
			gestureRecognizer.Direction = direction.Value;
		}

		if (threshold is not null)
		{
			gestureRecognizer.Threshold = threshold.Value;
		}

		gestureElement.GestureRecognizers.Add(gestureRecognizer);

		return gestureElement;
	}

	/// <summary>
	/// Adds a <see cref="TapGestureRecognizer"/> and sets defines its action when tapped
	/// </summary>
	/// <param name="gestureElement">An <see cref="Element"/> implementing <see cref="IGestureRecognizers"/></param>
	/// <param name="onTapped"><see cref="Action"/>invoked once <paramref name="numberOfTapsRequired"/> threshold is reached</param>
	/// <param name="numberOfTapsRequired">Number of taps required to trigger <paramref name="onTapped"/></param>
	/// <returns><paramref name="gestureElement"/></returns>
	public static TGestureElement TapGesture<TGestureElement>(this TGestureElement gestureElement,
																Action onTapped,
																int? numberOfTapsRequired = null) where TGestureElement : IGestureRecognizers
	{
		var gestureRecognizer = new TapGestureRecognizer
		{
			Command = new Command(onTapped)
		};

		if (numberOfTapsRequired is not null)
		{
			gestureRecognizer.NumberOfTapsRequired = numberOfTapsRequired.Value;
		}

		gestureElement.GestureRecognizers.Add(gestureRecognizer);

		return gestureElement;
	}
	
	static Func<TBindingContext, TSource> ConvertExpressionToFunc<TBindingContext, TSource>(in Expression<Func<TBindingContext, TSource>> expression) => expression.Compile();

	static string GetMemberName<T>(in Expression<T> expression) => expression.Body switch
	{
		MemberExpression m => m.Member.Name,
		UnaryExpression { Operand: MemberExpression m } => m.Member.Name,
		_ => throw new InvalidOperationException("Invalid getter. The `getter` parameter must point directly to a property in the ViewModel and cannot add additional logic")
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
		TParameterBindingContext? parameterSource = default)
		where TGestureElement : IGestureRecognizers
		where TGestureRecognizer : BindableObject, IGestureRecognizer, new()
		where TCommandBindingContext : class
		where TParameterBindingContext : class
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
	
	static TGestureElement ConfigureSwipeGesture<TGestureElement>(this TGestureElement gestureElement,
		SwipeGestureRecognizer swipeGesture,
		SwipeDirection? direction = null,
		uint? threshold = null) where TGestureElement : IGestureRecognizers
	{
		if (direction is not null)
		{
			swipeGesture.Direction = direction.Value;
		}

		if (threshold is not null)
		{
			swipeGesture.Threshold = threshold.Value;
		}

		return gestureElement;
	}

	static TGestureElement ConfigureTapGesture<TGestureElement>(this TGestureElement gestureElement,
		TapGestureRecognizer tapGesture,
		int? numberOfTapsRequired = null) where TGestureElement : IGestureRecognizers
	{
		if (numberOfTapsRequired is not null)
		{
			tapGesture.NumberOfTapsRequired = numberOfTapsRequired.Value;
		}

		return gestureElement;
	}
}