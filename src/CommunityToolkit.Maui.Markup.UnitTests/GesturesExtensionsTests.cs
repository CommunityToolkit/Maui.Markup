using System.Windows.Input;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using NUnit.Framework;
namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture(typeof(Label))] // Derived from View
class GesturesExtensionsTests<TGestureElement> : BaseMarkupTestFixture where TGestureElement : View, IGestureRecognizers, new()
{
	[Test]
	public void TapGesture()
	{
		const int numberOfTaps = 2;
		int taps = 0;

		var gestureElement = new TGestureElement();

		gestureElement.TapGesture(() => taps++, numberOfTaps);
		((TapGestureRecognizer)gestureElement.GestureRecognizers[0]).SendTapped(gestureElement);

		Assert.Multiple(() =>
		{
			Assert.That(taps, Is.GreaterThan(0));
			Assert.That(gestureElement.GestureRecognizers, Has.Count.EqualTo(1));
			Assert.That(gestureElement.GestureRecognizers[0], Is.InstanceOf<TapGestureRecognizer>());
			Assert.That(((TapGestureRecognizer)gestureElement.GestureRecognizers[0]).NumberOfTapsRequired, Is.EqualTo(numberOfTaps));
		});
	}

	[Test]
	public void PanGesture()
	{
		const int touchPoints = 2;
		int panCount = 0;

		var gestureElement = new TGestureElement();

		gestureElement.PanGesture(OnPan, touchPoints);
		((IPanGestureController)gestureElement.GestureRecognizers[0]).SendPan(null, 1, 2, 1);

		Assert.Multiple(() =>
		{
			Assert.That(panCount, Is.EqualTo(1));
			Assert.That(gestureElement.GestureRecognizers, Has.Count.EqualTo(1));
			Assert.That(gestureElement.GestureRecognizers[0], Is.InstanceOf<PanGestureRecognizer>());
			Assert.That(((PanGestureRecognizer)gestureElement.GestureRecognizers[0]).TouchPoints, Is.EqualTo(touchPoints));
		});

		void OnPan(object? sender, PanUpdatedEventArgs e) => panCount++;
	}

	[Test]
	public void PinchGesture()
	{
		int pinchCount = 0;

		var gestureElement = new TGestureElement();

		gestureElement.PinchGesture(OnPinch);
		((IPinchGestureController)gestureElement.GestureRecognizers[0]).SendPinch(null, 2, new Point());

		Assert.Multiple(() =>
		{
			Assert.That(pinchCount, Is.EqualTo(1));
			Assert.That(gestureElement.GestureRecognizers, Has.Count.EqualTo(pinchCount));
			Assert.That(gestureElement.GestureRecognizers[0], Is.InstanceOf<PinchGestureRecognizer>());
		});

		void OnPinch(object? sender, PinchGestureUpdatedEventArgs e) => pinchCount++;
	}

	[Test]
	public void SwipeGesture()
	{
		const SwipeDirection direction = SwipeDirection.Up;
		const uint threshold = 2000;
		int swipeCount = 0;

		var gestureElement = new TGestureElement();

		gestureElement.SwipeGesture(OnSwipe, direction, threshold);
		((SwipeGestureRecognizer)gestureElement.GestureRecognizers[0]).SendSwiped(null, direction);

		Assert.Multiple(() =>
		{
			Assert.That(swipeCount, Is.EqualTo(1));
			Assert.That(gestureElement.GestureRecognizers, Has.Count.EqualTo(1));
			Assert.That(gestureElement.GestureRecognizers[0], Is.InstanceOf<SwipeGestureRecognizer>());
			Assert.That(((SwipeGestureRecognizer)gestureElement.GestureRecognizers[0]).Direction, Is.EqualTo(direction));
			Assert.That(((SwipeGestureRecognizer)gestureElement.GestureRecognizers[0]).Threshold, Is.EqualTo(threshold));
		});

		void OnSwipe(object? sender, SwipedEventArgs e) => swipeCount++;
	}

	[Test]
	public void MultipleGestures()
	{
		var gestureElement = new TGestureElement();

		gestureElement.PanGesture();
		gestureElement.SwipeGesture();

		Assert.Multiple(() =>
		{
			Assert.That(gestureElement.GestureRecognizers, Has.Count.EqualTo(2));
			Assert.That(gestureElement.GestureRecognizers[0], Is.InstanceOf<PanGestureRecognizer>());
			Assert.That(gestureElement.GestureRecognizers[1], Is.InstanceOf<SwipeGestureRecognizer>());
		});
	}

	[Test]
	public void SupportDerivedFromLabel() // A View
	{
		Assert.That(new DerivedFromLabel().PanGesture(), Is.InstanceOf<DerivedFromLabel>());
	}
}

[TestFixture(typeof(Label))] // Derived from View
class GesturesExtensionsTypedBindingsTests<TGestureElement> : BaseMarkupTestFixture where TGestureElement : View, IGestureRecognizers, new()
{
	[Test]
	public void BindTapGestureDefaults()
	{
		var viewModel = new ViewModel();

		var gestureElement = new TGestureElement
		{
			BindingContext = viewModel
		};

		gestureElement.BindTapGesture(static (ViewModel vm) => vm.SetGuidCommand);

		Assert.Multiple(() =>
		{
			Assert.That(gestureElement.GestureRecognizers, Has.Count.EqualTo(1));
			Assert.That(gestureElement.GestureRecognizers[0], Is.InstanceOf<TapGestureRecognizer>());
		});

		BindingHelpers.AssertTypedBindingExists((TapGestureRecognizer)gestureElement.GestureRecognizers[0], TapGestureRecognizer.CommandProperty, BindingMode.Default, gestureElement.BindingContext);
	}

	[Test]
	public void BindTapGestureDefaultsWithNestedBindings()
	{
		var gestureElement = new TGestureElement
		{
			BindingContext = new ViewModel()
		};

		gestureElement.BindTapGesture(
			getter: static (ViewModel vm) => vm.NestedCommand.SetGuidCommand,
			handlers:
			[
				(vm => vm, nameof(ViewModel.NestedCommand)),
				(vm => vm.NestedCommand, nameof(ViewModel.NestedCommand.SetGuidCommand))
			],
			mode: BindingMode.OneTime);

		Assert.Multiple(() =>
		{
			Assert.That(gestureElement.GestureRecognizers, Has.Count.EqualTo(1));
			Assert.That(gestureElement.GestureRecognizers[0], Is.InstanceOf<TapGestureRecognizer>());
		});

		BindingHelpers.AssertTypedBindingExists((TapGestureRecognizer)gestureElement.GestureRecognizers[0], TapGestureRecognizer.CommandProperty, BindingMode.OneTime, gestureElement.BindingContext);
	}

	[Test]
	public void BindTapGesturePositionalParameters()
	{
		const int numberOfTaps = 2;
		var guid = Guid.NewGuid();
		var viewModel = new ViewModel();

		var gestureElement = new TGestureElement
		{
			BindingContext = viewModel
		};

		gestureElement.BindTapGesture(
			static (ViewModel vm) => vm.SetGuidCommand,
			commandBindingMode: BindingMode.OneTime,
			parameterGetter: (ViewModel vm) => guid,
			parameterBindingMode: BindingMode.OneTime,
			numberOfTapsRequired: numberOfTaps);

		var tapGestureRecognizer = (TapGestureRecognizer)gestureElement.GestureRecognizers[0];

		Assert.That(viewModel.Id, Is.EqualTo(Guid.Empty));

		tapGestureRecognizer.SendTapped(gestureElement);

		Assert.Multiple(() =>
		{
			Assert.That(viewModel.Id, Is.EqualTo(guid));
			Assert.That(gestureElement.GestureRecognizers, Has.Count.EqualTo(1));
			Assert.That(gestureElement.GestureRecognizers[0], Is.InstanceOf<TapGestureRecognizer>());
			Assert.That(tapGestureRecognizer.NumberOfTapsRequired, Is.EqualTo(numberOfTaps));
		});

		BindingHelpers.AssertTypedBindingExists(tapGestureRecognizer, TapGestureRecognizer.CommandProperty, BindingMode.OneTime, gestureElement.BindingContext);
		BindingHelpers.AssertTypedBindingExists(tapGestureRecognizer, TapGestureRecognizer.CommandParameterProperty, BindingMode.OneTime, gestureElement.BindingContext);
	}

	[Test]
	public void BindTapGesturePositionalParametersWithNestedBindings()
	{
		const int numberOfTaps = 2;

		var gestureElement = new TGestureElement
		{
			BindingContext = new ViewModel()
		};
		object commandSource = gestureElement.BindingContext;
		object parameterSource = gestureElement.BindingContext;

		gestureElement.BindTapGesture(
			getter: static (ViewModel vm) => vm.NestedCommand.SetGuidCommand,
			handlers:
			[
				(vm => vm, nameof(ViewModel.NestedCommand)),
				(vm => vm.NestedCommand, nameof(ViewModel.NestedCommand.SetGuidCommand))
			],
			commandBindingMode: BindingMode.OneTime,
			parameterGetter: static (ViewModel vm) => vm.NestedCommand.Id,
			parameterHandlers:
			[
				(vm => vm, nameof(ViewModel.NestedCommand)),
				(vm => vm.NestedCommand, nameof(ViewModel.NestedCommand.Id))
			],
			numberOfTapsRequired: numberOfTaps);

		var tapGestureRecognizer = (TapGestureRecognizer)gestureElement.GestureRecognizers[0];

		Assert.Multiple(() =>
		{
			Assert.That(gestureElement.GestureRecognizers, Has.Count.EqualTo(1));
			Assert.That(gestureElement.GestureRecognizers[0], Is.InstanceOf<TapGestureRecognizer>());
			Assert.That(tapGestureRecognizer.NumberOfTapsRequired, Is.EqualTo(numberOfTaps));
		});

		BindingHelpers.AssertTypedBindingExists(tapGestureRecognizer, TapGestureRecognizer.CommandProperty, BindingMode.OneTime, gestureElement.BindingContext);
		BindingHelpers.AssertTypedBindingExists(tapGestureRecognizer, TapGestureRecognizer.CommandParameterProperty, BindingMode.Default, gestureElement.BindingContext);
	}

	[Test]
	public void BindSwipeGestureDefaults()
	{
		var gestureElement = new TGestureElement
		{
			BindingContext = new ViewModel()
		};

		gestureElement.BindSwipeGesture(static (ViewModel vm) => vm.SetGuidCommand);

		Assert.That(gestureElement.GestureRecognizers, Has.Count.EqualTo(1));
		Assert.That(gestureElement.GestureRecognizers[0], Is.InstanceOf<SwipeGestureRecognizer>());
		BindingHelpers.AssertTypedBindingExists((SwipeGestureRecognizer)gestureElement.GestureRecognizers[0], SwipeGestureRecognizer.CommandProperty, BindingMode.Default, gestureElement.BindingContext);
	}

	[Test]
	public void BindSwipeGestureWithNestedBindings()
	{
		var gestureElement = new TGestureElement
		{
			BindingContext = new ViewModel()
		};

		gestureElement.BindSwipeGesture(
			getter: static (ViewModel vm) => vm.NestedCommand.SetGuidCommand,
			handlers:
			[
				(vm => vm, nameof(ViewModel.NestedCommand)),
				(vm => vm.NestedCommand, nameof(ViewModel.NestedCommand.SetGuidCommand))
			],
			mode: BindingMode.OneTime);

		Assert.Multiple(() =>
		{
			Assert.That(gestureElement.GestureRecognizers, Has.Count.EqualTo(1));
			Assert.That(gestureElement.GestureRecognizers[0], Is.InstanceOf<SwipeGestureRecognizer>());
		});

		BindingHelpers.AssertTypedBindingExists((SwipeGestureRecognizer)gestureElement.GestureRecognizers[0], SwipeGestureRecognizer.CommandProperty, BindingMode.OneTime, gestureElement.BindingContext);
	}

	[Test]
	public void BindSwipeGesturePositionalParameters()
	{
		const SwipeDirection direction = SwipeDirection.Up;
		const uint threshold = 2000;

		var guid = Guid.NewGuid();
		var viewModel = new ViewModel();

		var gestureElement = new TGestureElement
		{
			BindingContext = viewModel
		};

		gestureElement.BindSwipeGesture(
			static (ViewModel vm) => vm.SetGuidCommand,
			commandBindingMode: BindingMode.OneTime,
			parameterGetter: (ViewModel vm) => guid,
			parameterBindingMode: BindingMode.OneTime,
			direction: direction,
			threshold: threshold);

		Assert.That(viewModel.Id, Is.EqualTo(Guid.Empty));

		var swipeGestureRecognizer = (SwipeGestureRecognizer)gestureElement.GestureRecognizers[0];
		Assert.That(viewModel.Id, Is.EqualTo(Guid.Empty));

		swipeGestureRecognizer.SendSwiped(gestureElement, direction);

		Assert.Multiple(() =>
		{
			Assert.That(viewModel.Id, Is.EqualTo(guid));
			Assert.That(gestureElement.GestureRecognizers, Has.Count.EqualTo(1));
			Assert.That(gestureElement.GestureRecognizers[0], Is.InstanceOf<SwipeGestureRecognizer>());
			Assert.That(((SwipeGestureRecognizer)gestureElement.GestureRecognizers[0]).Direction, Is.EqualTo(direction));
			Assert.That(((SwipeGestureRecognizer)gestureElement.GestureRecognizers[0]).Threshold, Is.EqualTo(threshold));
		});

		BindingHelpers.AssertTypedBindingExists((SwipeGestureRecognizer)gestureElement.GestureRecognizers[0], SwipeGestureRecognizer.CommandProperty, BindingMode.OneTime, gestureElement.BindingContext);
		BindingHelpers.AssertTypedBindingExists((SwipeGestureRecognizer)gestureElement.GestureRecognizers[0], SwipeGestureRecognizer.CommandParameterProperty, BindingMode.OneTime, gestureElement.BindingContext);
	}

	[Test]
	public void BindSwipeGesturePositionalParametersWithNestedBindings()
	{
		const SwipeDirection direction = SwipeDirection.Up;
		const uint threshold = 2000;

		var gestureElement = new TGestureElement
		{
			BindingContext = new ViewModel()
		};

		gestureElement.BindSwipeGesture(
			getter: static (ViewModel vm) => vm.NestedCommand.SetGuidCommand,
			handlers:
			[
				(vm => vm, nameof(ViewModel.NestedCommand)),
				(vm => vm.NestedCommand, nameof(ViewModel.NestedCommand.SetGuidCommand))
			],
			commandBindingMode: BindingMode.OneTime,
			parameterGetter: static (ViewModel vm) => vm.NestedCommand.Id,
			parameterHandlers:
			[
				(vm => vm, nameof(ViewModel.NestedCommand)),
				(vm => vm.NestedCommand, nameof(ViewModel.NestedCommand.Id))
			],
			direction: direction,
			threshold: threshold);

		Assert.Multiple(() =>
		{
			Assert.That(gestureElement.GestureRecognizers, Has.Count.EqualTo(1));
			Assert.That(gestureElement.GestureRecognizers[0], Is.InstanceOf<SwipeGestureRecognizer>());
			Assert.That(((SwipeGestureRecognizer)gestureElement.GestureRecognizers[0]).Direction, Is.EqualTo(direction));
			Assert.That(((SwipeGestureRecognizer)gestureElement.GestureRecognizers[0]).Threshold, Is.EqualTo(threshold));
		});

		BindingHelpers.AssertTypedBindingExists((SwipeGestureRecognizer)gestureElement.GestureRecognizers[0], SwipeGestureRecognizer.CommandProperty, BindingMode.OneTime, gestureElement.BindingContext);
		BindingHelpers.AssertTypedBindingExists((SwipeGestureRecognizer)gestureElement.GestureRecognizers[0], SwipeGestureRecognizer.CommandParameterProperty, BindingMode.Default, gestureElement.BindingContext);
	}

	[Test]
	public void MultipleGestureBindings()
	{
		var gestureElement = new TGestureElement
		{
			BindingContext = new ViewModel()
		}
			.BindSwipeGesture(static (ViewModel vm) => vm.SetGuidCommand)
			.BindTapGesture(static (ViewModel vm) => vm.SetGuidCommand);

		Assert.Multiple(() =>
		{
			Assert.That(gestureElement.GestureRecognizers, Has.Count.EqualTo(2));
			Assert.That(gestureElement.GestureRecognizers[0], Is.InstanceOf<SwipeGestureRecognizer>());
			Assert.That(gestureElement.GestureRecognizers[1], Is.InstanceOf<TapGestureRecognizer>());
		});
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
	public ViewModel()
	{
		SetGuidCommand = new Command<Guid>(guid => Id = guid);
	}

	public ICommand SetGuidCommand { get; }

	public NestedCommand NestedCommand { get; } = new();

	public Guid Id { get; private set; }
}

class NestedCommand
{
	public NestedCommand()
	{
		SetGuidCommand = new Command<Guid>(guid => Id = guid);
	}

	public ICommand SetGuidCommand { get; }

	public Guid Id { get; private set; }
}