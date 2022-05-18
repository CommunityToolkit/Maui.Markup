using System;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;

namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension Methods for Element Gestures
/// </summary>
public static class GesturesExtensions
{
	/// <summary>Add a <see cref="ClickGestureRecognizer"/> and bind to its Command and (optionally) CommandParameter properties</summary>
	/// <param name="gestureElement">An <see cref="Element"/> implementing <see cref="IGestureRecognizers"/></param>
	/// <param name="commandPath">Path to Command Binding</param>
	/// <param name="commandSource">Binding source for Command Binding</param>
	/// <param name="parameterPath">If not specified or null, no binding is created for the CommandParameter property</param>
	/// <param name="parameterSource">Binding source for Command Binding</param>
	/// <param name="numberOfClicksRequired">Total number of clicks required to trigger <see cref="ICommand"/></param>
	/// <returns><paramref name="gestureElement"/></returns>
	public static TGestureElement BindClickGesture<TGestureElement>(this TGestureElement gestureElement,
																		string commandPath,
																		object? commandSource = null,
																		string? parameterPath = null,
																		object? parameterSource = null,
																		int? numberOfClicksRequired = null) where TGestureElement : IGestureRecognizers
	{
		var clickGesture = gestureElement.BindGesture<TGestureElement, ClickGestureRecognizer>(commandPath, commandSource, parameterPath, parameterSource);

		if (numberOfClicksRequired is not null)
		{
			clickGesture.NumberOfClicksRequired = numberOfClicksRequired.Value;
		}

		return gestureElement;
	}

	/// <summary>Add a <see cref="SwipeGestureRecognizer"/> and bind to its Command and (optionally) CommandParameter properties</summary>
	/// <param name="gestureElement">An <see cref="Element"/> implementing <see cref="IGestureRecognizers"/></param>
	/// <param name="commandPath">Path to Command Binding</param>
	/// <param name="commandSource">Binding source for Command Binding</param>
	/// <param name="parameterPath">If not specified or null, no binding is created for the CommandParameter property</param>
	/// <param name="parameterSource">Binding source for Command Binding</param>
	/// <param name="direction">Swipe gesture direction</param>
	/// <param name="threshold">Minimum swipe distance that will cause the gesture to be recognized</param>
	/// <returns><paramref name="gestureElement"/></returns>
	public static TGestureElement BindSwipeGesture<TGestureElement>(this TGestureElement gestureElement,
																		string commandPath,
																		object? commandSource = null,
																		string? parameterPath = null,
																		object? parameterSource = null,
																		Microsoft.Maui.SwipeDirection? direction = null,
																		uint? threshold = null) where TGestureElement : IGestureRecognizers
	{
		var swipeGesture = gestureElement.BindGesture<TGestureElement, SwipeGestureRecognizer>(commandPath, commandSource, parameterPath, parameterSource);

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

	/// <summary>Adds a <see cref="TapGestureRecognizer"/> and bind its Command and (optionally) CommandParameter properties</summary>
	/// <param name="gestureElement">An <see cref="Element"/> implementing <see cref="IGestureRecognizers"/></param>
	/// <param name="commandPath">Path to Command Binding</param>
	/// <param name="commandSource">Binding source for Command Binding</param>
	/// <param name="parameterPath">If not specified or null, no binding is created for the CommandParameter property</param>
	/// <param name="parameterSource">Binding source for Command Binding</param>
	/// <param name="numberOfTapsRequired">Number of taps required to trigger the <see cref="ICommand"/></param>
	/// <returns><paramref name="gestureElement"/></returns>
	public static TGestureElement BindTapGesture<TGestureElement>(this TGestureElement gestureElement,
																	string commandPath,
																	object? commandSource = null,
																	string? parameterPath = null,
																	object? parameterSource = null,
																	int? numberOfTapsRequired = null) where TGestureElement : IGestureRecognizers
	{
		var tapGesture = gestureElement.BindGesture<TGestureElement, TapGestureRecognizer>(commandPath, commandSource, parameterPath, parameterSource);

		if (numberOfTapsRequired is not null)
		{
			tapGesture.NumberOfTapsRequired = numberOfTapsRequired.Value;
		}

		return gestureElement;
	}

	/// <summary>
	/// Adds a <see cref="ClickGestureRecognizer"/> and sets defines its action when clicked
	/// </summary>
	/// <param name="gestureElement">An <see cref="Element"/> implementing <see cref="IGestureRecognizers"/></param>
	/// <param name="onClicked"><see cref="Action"/>invoked once <paramref name="numberOfClicksRequired"/> threshold is reached</param>
	/// <param name="numberOfClicksRequired">Total number of clicks required to trigger <paramref name="onClicked"/></param>
	/// <returns><paramref name="gestureElement"/></returns>
	public static TGestureElement ClickGesture<TGestureElement>(this TGestureElement gestureElement,
																	Action onClicked,
																	int? numberOfClicksRequired = null) where TGestureElement : IGestureRecognizers
	{
		var gestureRecognizer = new ClickGestureRecognizer
		{
			Command = new Command(onClicked)
		};

		if (numberOfClicksRequired is not null)
		{
			gestureRecognizer.NumberOfClicksRequired = numberOfClicksRequired.Value;
		}

		gestureElement.GestureRecognizers.Add(gestureRecognizer);

		return gestureElement;
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
																Microsoft.Maui.SwipeDirection? direction = null,
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

	static TGestureRecognizer BindGesture<TGestureElement, TGestureRecognizer>(
		this TGestureElement gestureElement,
		string commandPath,
		object? commandSource = null,
		string? parameterPath = null,
		object? parameterSource = null) where TGestureElement : IGestureRecognizers
										where TGestureRecognizer : BindableObject, IGestureRecognizer, new()
	{
		var gestureRecognizer = new TGestureRecognizer().BindCommand(commandPath, commandSource, parameterPath, parameterSource);
		gestureElement.GestureRecognizers.Add(gestureRecognizer);

		return gestureRecognizer;
	}
}