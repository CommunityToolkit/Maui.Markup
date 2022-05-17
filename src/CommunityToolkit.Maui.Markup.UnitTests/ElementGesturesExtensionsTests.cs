using System;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using Microsoft.Maui.Controls;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture(typeof(Label))] // Derived from View
[TestFixture(typeof(Span))] // Derived from GestureElement
class ElementGesturesExtensionsTests<TGestureElement> : ElementGesturesBaseTestFixture where TGestureElement : IGestureRecognizers, new()
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
		var gestureElement = new TGestureElement();
		object commandSource = new ViewModel();
		object parameterSource = new ViewModel();

		gestureElement.BindClickGesture(nameof(ViewModel.Command), commandSource, nameof(ViewModel.Id), parameterSource);

		Assert.AreEqual(1, gestureElement.GestureRecognizers.Count);
		Assert.IsInstanceOf<ClickGestureRecognizer>(gestureElement.GestureRecognizers[0]);
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
		var gestureElement = new TGestureElement();
		object commandSource = new ViewModel();
		object parameterSource = new ViewModel();

		gestureElement.BindTapGesture(nameof(ViewModel.Command), commandSource, nameof(ViewModel.Id), parameterSource);

		Assert.AreEqual(1, gestureElement.GestureRecognizers.Count);
		Assert.IsInstanceOf<TapGestureRecognizer>(gestureElement.GestureRecognizers[0]);
		BindingHelpers.AssertBindingExists((TapGestureRecognizer)gestureElement.GestureRecognizers[0], TapGestureRecognizer.CommandProperty, nameof(ViewModel.Command), source: commandSource);
		BindingHelpers.AssertBindingExists((TapGestureRecognizer)gestureElement.GestureRecognizers[0], TapGestureRecognizer.CommandParameterProperty, nameof(ViewModel.Id), source: parameterSource);
	}

	[Test]
	public void ClickGesture()
	{
		int clicks = 0;

		var gestureElement = new TGestureElement();

		gestureElement.ClickGesture(() => clicks++);
		((ClickGestureRecognizer)gestureElement.GestureRecognizers[0]).SendClicked(null, ButtonsMask.Primary);

		Assert.Greater(0, clicks);
		Assert.AreEqual(1, gestureElement.GestureRecognizers.Count);
		Assert.IsInstanceOf<TapGestureRecognizer>(gestureElement.GestureRecognizers[0]);
	}

	[Test]
	public void TapGesture()
	{
		int taps = 0;

		var gestureElement = new TGestureElement();

		gestureElement.TapGesture(() => taps++);
		((TapGestureRecognizer)gestureElement.GestureRecognizers[0]).SendTapped(null);

		Assert.Greater(0, taps);
		Assert.AreEqual(1, gestureElement.GestureRecognizers.Count);
		Assert.IsInstanceOf<TapGestureRecognizer>(gestureElement.GestureRecognizers[0]);
	}
}

//[TestFixture]
//class ElementGesturesExtensionsTests : ElementGesturesBaseTestFixture
//{
//	[Test]
//	public void BindSwipeGestureDefaults()
//	{
//		var gestureElement = new Label();

//		gestureElement.BindSwipeGesture(nameof(ViewModel.Command));

//		var gestureRecognizer = AssertHasGestureRecognizer<SwipeGestureRecognizer>(gestureElement);
//		BindingHelpers.AssertBindingExists(gestureRecognizer, SwipeGestureRecognizer.CommandProperty, nameof(ViewModel.Command));
//	}

//	[Test]
//	public void BindSwipeGesturePositionalParameters()
//	{
//		var gestureElement = new Label();
//		object commandSource = new ViewModel();
//		object parameterSource = new ViewModel();

//		gestureElement.BindSwipeGesture(nameof(ViewModel.Command), commandSource, nameof(ViewModel.Id), parameterSource);

//		var gestureRecognizer = AssertHasGestureRecognizer<SwipeGestureRecognizer>(gestureElement);
//		BindingHelpers.AssertBindingExists(gestureRecognizer, SwipeGestureRecognizer.CommandProperty, nameof(ViewModel.Command), source: commandSource);
//		BindingHelpers.AssertBindingExists(gestureRecognizer, SwipeGestureRecognizer.CommandParameterProperty, nameof(ViewModel.Id), source: parameterSource);
//	}

//	[Test]
//	public void PanGesture()
//	{
//		var gestureElement = new Label();
//		PanGestureRecognizer? gestureRecognizer = null;

//		gestureElement.PanGesture(g => gestureRecognizer = g);

//		AssertHasGestureRecognizer(gestureElement, gestureRecognizer ?? throw new NullReferenceException());
//	}

//	[Test]
//	public void PinchGesture()
//	{
//		var gestureElement = new Label();
//		PinchGestureRecognizer? gestureRecognizer = null;

//		gestureElement.PinchGesture(g => gestureRecognizer = g);

//		AssertHasGestureRecognizer(gestureElement, gestureRecognizer ?? throw new NullReferenceException());
//	}

//	[Test]
//	public void SwipeGesture()
//	{
//		var gestureElement = new Label();
//		SwipeGestureRecognizer? gestureRecognizer = null;

//		gestureElement.SwipeGesture(g => gestureRecognizer = g);

//		AssertHasGestureRecognizer(gestureElement, gestureRecognizer ?? throw new NullReferenceException());
//	}

//	[Test]
//	public void MultipleGestures()
//	{
//		var gestureElement = new Label();
//		TapGestureRecognizer? gestureRecognizer1 = null, gestureRecognizer2 = null;
//		SwipeGestureRecognizer? gestureRecognizer3 = null;

//		gestureElement.TapGesture(g => gestureRecognizer1 = g);
//		gestureElement.TapGesture(g => gestureRecognizer2 = g);
//		gestureElement.SwipeGesture(g => gestureRecognizer3 = g);

//		AssertHasGestureRecognizers(gestureElement, gestureRecognizer1 ?? throw new NullReferenceException(), gestureRecognizer2 ?? throw new NullReferenceException());
//		AssertHasGestureRecognizer(gestureElement, gestureRecognizer3 ?? throw new NullReferenceException());
//	}

//	[Test]
//	public void Gesture()
//	{
//		var gestureElement = new Label();
//		DerivedFromGestureRecognizer? gestureRecognizer = null;

//		gestureElement.Gesture((DerivedFromGestureRecognizer g) => gestureRecognizer = g);

//		AssertHasGestureRecognizer(gestureElement, gestureRecognizer ?? throw new NullReferenceException());
//	}

//	[Test]
//	public void SupportDerivedFromLabel() // A View
//	{
//		Assert.IsInstanceOf<DerivedFromLabel>(
//			new DerivedFromLabel()
//			.Gesture((TapGestureRecognizer g) => g.Bind(nameof(ViewModel.Command))));
//	}

//	[Test]
//	public void SupportDerivedFromSpan() // A GestureElement
//	{
//		Assert.IsInstanceOf<DerivedFromSpan>(
//			new DerivedFromSpan()
//			.Gesture((TapGestureRecognizer g) => g.Bind(nameof(ViewModel.Command))));
//	}
//}

class ElementGesturesBaseTestFixture : BaseMarkupTestFixture
{
	protected class DerivedFromLabel : Label { }

	protected class DerivedFromSpan : Span { }

	protected class DerivedFromGestureRecognizer : GestureRecognizer { }
}

class ViewModel
{
	public Guid Id { get; set; }

	public ICommand? Command { get; set; }
}