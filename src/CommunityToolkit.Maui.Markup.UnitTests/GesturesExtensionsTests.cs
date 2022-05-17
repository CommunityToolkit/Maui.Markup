using System;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture(typeof(Label))] // Derived from View
class GesturesExtensionsTests<TGestureElement> : BaseMarkupTestFixture where TGestureElement : IGestureRecognizers, new()
{
	[Test]
	public void BindClickGestureDefaults()
	{
		var gestureElement = new TGestureElement();

		gestureElement.BindClickGesture(nameof(ViewModel.Command));

		Assert.AreEqual(1, gestureElement.GestureRecognizers.Count);
		Assert.IsInstanceOf<ClickGestureRecognizer>(gestureElement.GestureRecognizers[0]);
		BindingHelpers.AssertBindingExists((ClickGestureRecognizer)gestureElement.GestureRecognizers[0], ClickGestureRecognizer.CommandProperty, nameof(ViewModel.Command));
	}

	[Test]
	public void BindClickGesturePositionalParameters()
	{
		const int numberOfClicks = 2;

		var gestureElement = new TGestureElement();
		object commandSource = new ViewModel();
		object parameterSource = new ViewModel();

		gestureElement.BindClickGesture(nameof(ViewModel.Command), commandSource, nameof(ViewModel.Id), parameterSource, numberOfClicks);

		Assert.AreEqual(1, gestureElement.GestureRecognizers.Count);
		Assert.IsInstanceOf<ClickGestureRecognizer>(gestureElement.GestureRecognizers[0]);
		Assert.AreEqual(numberOfClicks, ((ClickGestureRecognizer)gestureElement.GestureRecognizers[0]).NumberOfClicksRequired);
		BindingHelpers.AssertBindingExists((ClickGestureRecognizer)gestureElement.GestureRecognizers[0], ClickGestureRecognizer.CommandProperty, nameof(ViewModel.Command), source: commandSource);
		BindingHelpers.AssertBindingExists((ClickGestureRecognizer)gestureElement.GestureRecognizers[0], ClickGestureRecognizer.CommandParameterProperty, nameof(ViewModel.Id), source: parameterSource);
	}

	[Test]
	public void BindTapGestureDefaults()
	{
		var gestureElement = new TGestureElement();

		gestureElement.BindTapGesture(nameof(ViewModel.Command));

		Assert.AreEqual(1, gestureElement.GestureRecognizers.Count);
		Assert.IsInstanceOf<TapGestureRecognizer>(gestureElement.GestureRecognizers[0]);
		BindingHelpers.AssertBindingExists((TapGestureRecognizer)gestureElement.GestureRecognizers[0], TapGestureRecognizer.CommandProperty, nameof(ViewModel.Command));
	}

	[Test]
	public void BindTapGesturePositionalParameters()
	{
		const int numberOfTaps = 2;

		var gestureElement = new TGestureElement();
		object commandSource = new ViewModel();
		object parameterSource = new ViewModel();

		gestureElement.BindTapGesture(nameof(ViewModel.Command), commandSource, nameof(ViewModel.Id), parameterSource, numberOfTaps);

		Assert.AreEqual(1, gestureElement.GestureRecognizers.Count);
		Assert.IsInstanceOf<TapGestureRecognizer>(gestureElement.GestureRecognizers[0]);
		Assert.AreEqual(numberOfTaps, ((TapGestureRecognizer)gestureElement.GestureRecognizers[0]).NumberOfTapsRequired);
		BindingHelpers.AssertBindingExists((TapGestureRecognizer)gestureElement.GestureRecognizers[0], TapGestureRecognizer.CommandProperty, nameof(ViewModel.Command), source: commandSource);
		BindingHelpers.AssertBindingExists((TapGestureRecognizer)gestureElement.GestureRecognizers[0], TapGestureRecognizer.CommandParameterProperty, nameof(ViewModel.Id), source: parameterSource);
	}

	[Test]
	public void ClickGesture()
	{
		const int numberOfClicks = 2;
		int clicks = 0;

		var gestureElement = new TGestureElement();

		gestureElement.ClickGesture(() => clicks++, numberOfClicks);
		((ClickGestureRecognizer)gestureElement.GestureRecognizers[0]).SendClicked(null, ButtonsMask.Primary);

		Assert.Greater(clicks, 0);
		Assert.AreEqual(1, gestureElement.GestureRecognizers.Count);
		Assert.IsInstanceOf<ClickGestureRecognizer>(gestureElement.GestureRecognizers[0]);
		Assert.AreEqual(numberOfClicks, ((ClickGestureRecognizer)gestureElement.GestureRecognizers[0]).NumberOfClicksRequired);
	}

	[Test]
	public void TapGesture()
	{
		const int numberOfTaps = 2;
		int taps = 0;

		var gestureElement = new TGestureElement();

		gestureElement.TapGesture(() => taps++, numberOfTaps);
		((TapGestureRecognizer)gestureElement.GestureRecognizers[0]).SendTapped(null);

		Assert.Greater(taps, 0);
		Assert.AreEqual(1, gestureElement.GestureRecognizers.Count);
		Assert.IsInstanceOf<TapGestureRecognizer>(gestureElement.GestureRecognizers[0]);
		Assert.AreEqual(numberOfTaps, ((TapGestureRecognizer)gestureElement.GestureRecognizers[0]).NumberOfTapsRequired);
	}

	[Test]
	public void BindSwipeGestureDefaults()
	{
		var gestureElement = new TGestureElement();

		gestureElement.BindSwipeGesture(nameof(ViewModel.Command));

		Assert.AreEqual(1, gestureElement.GestureRecognizers.Count);
		Assert.IsInstanceOf<SwipeGestureRecognizer>(gestureElement.GestureRecognizers[0]);
		BindingHelpers.AssertBindingExists((SwipeGestureRecognizer)gestureElement.GestureRecognizers[0], SwipeGestureRecognizer.CommandProperty, nameof(ViewModel.Command));
	}

	[Test]
	public void BindSwipeGesturePositionalParameters()
	{
		const SwipeDirection direction = SwipeDirection.Up;
		const uint threshold = 2000;

		var gestureElement = new TGestureElement();
		object commandSource = new ViewModel();
		object parameterSource = new ViewModel();

		gestureElement.BindSwipeGesture(nameof(ViewModel.Command), commandSource, nameof(ViewModel.Id), parameterSource, direction, threshold);

		Assert.AreEqual(1, gestureElement.GestureRecognizers.Count);
		Assert.IsInstanceOf<SwipeGestureRecognizer>(gestureElement.GestureRecognizers[0]);
		Assert.AreEqual(direction, ((SwipeGestureRecognizer)gestureElement.GestureRecognizers[0]).Direction);
		Assert.AreEqual(threshold, ((SwipeGestureRecognizer)gestureElement.GestureRecognizers[0]).Threshold);
		BindingHelpers.AssertBindingExists((SwipeGestureRecognizer)gestureElement.GestureRecognizers[0], SwipeGestureRecognizer.CommandProperty, nameof(ViewModel.Command), source: commandSource);
		BindingHelpers.AssertBindingExists((SwipeGestureRecognizer)gestureElement.GestureRecognizers[0], SwipeGestureRecognizer.CommandParameterProperty, nameof(ViewModel.Id), source: parameterSource);
	}

	[Test]
	public void PanGesture()
	{
		const int touchPoints = 2;
		int panCount = 0;

		var gestureElement = new TGestureElement();

		gestureElement.PanGesture(eventArgs => panCount++, touchPoints);
		((IPanGestureController)gestureElement.GestureRecognizers[0]).SendPan(null, 1, 2, 1);

		Assert.AreEqual(panCount, 1);
		Assert.AreEqual(1, gestureElement.GestureRecognizers.Count);
		Assert.IsInstanceOf<PanGestureRecognizer>(gestureElement.GestureRecognizers[0]);
		Assert.AreEqual(touchPoints, ((PanGestureRecognizer)gestureElement.GestureRecognizers[0]).TouchPoints);
	}

	[Test]
	public void PinchGesture()
	{
		int pinchCount = 0;

		var gestureElement = new TGestureElement();

		gestureElement.PinchGesture(eventArgs => pinchCount++);
		((IPinchGestureController)gestureElement.GestureRecognizers[0]).SendPinch(null, 2, new Microsoft.Maui.Graphics.Point());

		Assert.AreEqual(pinchCount, 1);
		Assert.AreEqual(1, gestureElement.GestureRecognizers.Count);
		Assert.IsInstanceOf<PinchGestureRecognizer>(gestureElement.GestureRecognizers[0]);
	}

	[Test]
	public void SwipeGesture()
	{
		const SwipeDirection direction = SwipeDirection.Up;
		const uint threshold = 2000;
		int swipeCount = 0;

		var gestureElement = new TGestureElement();

		gestureElement.SwipeGesture(eventArgs => swipeCount++, direction, threshold);
		((SwipeGestureRecognizer)gestureElement.GestureRecognizers[0]).SendSwiped(null, direction);

		Assert.AreEqual(swipeCount, 1);
		Assert.AreEqual(1, gestureElement.GestureRecognizers.Count);
		Assert.IsInstanceOf<SwipeGestureRecognizer>(gestureElement.GestureRecognizers[0]);
		Assert.AreEqual(direction, ((SwipeGestureRecognizer)gestureElement.GestureRecognizers[0]).Direction);
		Assert.AreEqual(threshold, ((SwipeGestureRecognizer)gestureElement.GestureRecognizers[0]).Threshold);
	}

	[Test]
	public void MultipleGestures()
	{
		var gestureElement = new TGestureElement();

		gestureElement.PanGesture();
		gestureElement.SwipeGesture();

		Assert.AreEqual(2, gestureElement.GestureRecognizers.Count);
		Assert.IsInstanceOf<PanGestureRecognizer>(gestureElement.GestureRecognizers[0]);
		Assert.IsInstanceOf<SwipeGestureRecognizer>(gestureElement.GestureRecognizers[1]);
	}

	[Test]
	public void SupportDerivedFromLabel() // A View
	{
		Assert.IsInstanceOf<DerivedFromLabel>(
			new DerivedFromLabel()
			.PanGesture());
	}
}

class DerivedFromLabel : Label
{
}


class DerivedFromGestureRecognizer : GestureRecognizer
{
}

class ViewModel
{
	public Guid Id { get; set; }

	public ICommand? Command { get; set; }
}