using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class TypedBindingExtensionsTests : BaseMarkupTestFixture
{
	static readonly IReadOnlyDictionary<string, Color> colors = typeof(Colors)
		.GetFields(BindingFlags.Static | BindingFlags.Public)
		.ToDictionary(c => c.Name, c => (Color)(c.GetValue(null) ?? throw new InvalidOperationException()));

	ViewModel? viewModel;

	[SetUp]
	public override void Setup()
	{
		base.Setup();
		viewModel = new ViewModel();
		Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
	}

	[TearDown]
	public override void TearDown()
	{
		viewModel = null;
		base.TearDown();
	}

	[Test]
	public void BindCommandThrowsArgumentNullExceptionWhenParameterHandlersNull()
	{
		Assert.Throws<ArgumentNullException>(() =>
		{
			new Button()
				.BindCommand<Button, ViewModel, object?, Color>(
					(ViewModel vm) => vm.Command,
					new (Func<ViewModel, object?>, string)[]
					{
						(vm => vm, nameof(ViewModel.Command)),
					},
					parameterGetter: _ => Colors.Black);
		});
	}

	[Test]
	public void BindCommandWithDefaults()
	{
		var textCell = new TextCell
		{
			BindingContext = viewModel
		};

		textCell.BindCommand(static (ViewModel vm) => vm.Command);

		BindingHelpers.AssertTypedBindingExists(textCell, TextCell.CommandProperty, BindingMode.Default, viewModel);
		Assert.That(BindingHelpers.GetBinding(textCell, TextCell.CommandParameterProperty), Is.Null);
	}

	[Test]
	public void BindCommandWithParameters()
	{
		ArgumentNullException.ThrowIfNull(viewModel);

		var textCell = new TextCell
		{
			BindingContext = viewModel
		};

		textCell.BindCommand(
			static (ViewModel vm) => vm.Command,
			commandBindingMode: BindingMode.OneTime,
			parameterGetter: static (ViewModel vm) => vm.Id,
			parameterBindingMode: BindingMode.OneWay);

		BindingHelpers.AssertTypedBindingExists(textCell, TextCell.CommandProperty, BindingMode.OneTime, viewModel);
		BindingHelpers.AssertTypedBindingExists(textCell, TextCell.CommandParameterProperty, BindingMode.OneWay, viewModel);

		Assert.That(viewModel.Command, Is.EqualTo(textCell.Command));
		Assert.That(viewModel.Id, Is.EqualTo(textCell.CommandParameter));
	}

	[Test]
	public async Task ConfirmStringFormat()
	{
		ArgumentNullException.ThrowIfNull(viewModel);

		const string stringFormat = "{0}%";
		bool didPropertyChangeFire = false;
		TaskCompletionSource<string?> propertyChangedEventArgsTCS = new();

		var label = new Label
		{
			BindingContext = viewModel
		}.Bind(Label.TextProperty, static (ViewModel viewModel) => viewModel.Percentage, stringFormat: stringFormat);

		viewModel.PropertyChanged += HandlePropertyChanged;

		BindingHelpers.AssertTypedBindingExists(label, Label.TextProperty, BindingMode.Default, viewModel, stringFormat);
		Assert.That(label.Text, Is.EqualTo(string.Format(stringFormat, ViewModel.DefaultPercentage)));

		label.Text = string.Format(stringFormat, 0.1);
		Assert.That(label.Text, Is.EqualTo("0.1%"));
		Assert.That(viewModel.Percentage, Is.EqualTo(ViewModel.DefaultPercentage));

		viewModel.Percentage = 0.6;
		var propertyName = await propertyChangedEventArgsTCS.Task;

		Assert.That(didPropertyChangeFire, Is.True);
		Assert.That(propertyName, Is.EqualTo(nameof(ViewModel.Percentage)));

		void HandlePropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			didPropertyChangeFire = true;
			propertyChangedEventArgsTCS.SetResult(e.PropertyName);
		}
	}

	[Test]
	public async Task ConfirmReadOnlyTypedBindings()
	{
		ArgumentNullException.ThrowIfNull(viewModel);

		bool didViewModelPropertyChangeFire = false;
		TaskCompletionSource<string?> viewModelPropertyChangedEventArgsTCS = new();

		bool didLabelPropertyChangeFire = false;
		TaskCompletionSource<string?> labelPropertyChangedEventArgsTCS = new();

		var label = new Label
		{
			BindingContext = viewModel
		}.Bind(Label.TextColorProperty, static (ViewModel viewModel) => viewModel.TextColor);

		viewModel.PropertyChanged += HandleViewModelPropertyChanged;
		label.PropertyChanged += HandleLabelPropertyChanged;

		BindingHelpers.AssertTypedBindingExists(label, Label.TextColorProperty, BindingMode.Default, viewModel);
		Assert.That(label.TextColor, Is.EqualTo(ViewModel.DefaultColor));

		viewModel.TextColor = Colors.Green;
		var viewModelPropertyName = await viewModelPropertyChangedEventArgsTCS.Task;
		var labelPropertyName = await labelPropertyChangedEventArgsTCS.Task;

		Assert.That(didViewModelPropertyChangeFire, Is.True);
		Assert.That(viewModelPropertyName, Is.EqualTo(nameof(ViewModel.TextColor)));

		Assert.That(didLabelPropertyChangeFire, Is.True);
		Assert.That(labelPropertyName, Is.EqualTo(nameof(Label.TextColor)));

		Assert.That(viewModel.TextColor, Is.EqualTo(Colors.Green));
		Assert.That(label.GetValue(Label.TextColorProperty), Is.EqualTo(Colors.Green));

		label.TextColor = Colors.Red;
		Assert.That(label.TextColor, Is.EqualTo(Colors.Red));
		Assert.That(viewModel.TextColor, Is.EqualTo(Colors.Green));

		void HandleViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			didViewModelPropertyChangeFire = true;
			viewModelPropertyChangedEventArgsTCS.SetResult(e.PropertyName);
		}

		void HandleLabelPropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			didLabelPropertyChangeFire = true;
			labelPropertyChangedEventArgsTCS.TrySetResult(e.PropertyName);
		}
	}

	[Test]
	public void ConfirmOneTimeBinding()
	{
		ArgumentNullException.ThrowIfNull(viewModel);

		bool didLabelPropertyChangeFire = false;
		bool didViewModelPropertyChangeFire = false;

		var label = new Label
		{
			BindingContext = viewModel
		}.Bind(Label.TextColorProperty, static (ViewModel viewModel) => viewModel.TextColor, static (ViewModel viewModel, Color color) => viewModel.TextColor = color, BindingMode.OneTime);

		viewModel.PropertyChanged += HandleViewModelPropertyChanged;
		label.PropertyChanged += HandleLabelPropertyChanged;

		BindingHelpers.AssertTypedBindingExists(label, Label.TextColorProperty, BindingMode.OneTime, viewModel);
		Assert.That(label.TextColor, Is.EqualTo(ViewModel.DefaultColor));

		viewModel.TextColor = Colors.Green;
		Assert.That(viewModel.TextColor, Is.EqualTo(Colors.Green));
		Assert.That(label.GetValue(Label.TextColorProperty), Is.EqualTo(ViewModel.DefaultColor));

		label.TextColor = Colors.Red;
		Assert.That(label.TextColor, Is.EqualTo(Colors.Red));
		Assert.That(viewModel.TextColor, Is.EqualTo(Colors.Green));

		Assert.That(didViewModelPropertyChangeFire, Is.True);
		Assert.That(didLabelPropertyChangeFire, Is.True);

		void HandleViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			didViewModelPropertyChangeFire = true;
		}

		void HandleLabelPropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			didLabelPropertyChangeFire = true;
		}
	}

	[Test]
	public async Task ConfirmReadWriteTypedBinding()
	{
		ArgumentNullException.ThrowIfNull(viewModel);

		bool didViewModelPropertyChangeFire = false;
		int viewModelPropertyChangedEventCount = 0;
		TaskCompletionSource<string?> viewModelPropertyChangedEventArgsTCS = new();

		bool didSliderPropertyChangeFire = false;
		int sliderPropertyChangedEventCount = 0;
		TaskCompletionSource<string?> sliderPropertyChangedEventArgsTCS = new();

		var slider = new Slider
		{
			BindingContext = viewModel
		}.Bind(Slider.ValueProperty, static (ViewModel viewModel) => viewModel.Percentage, static (ViewModel viewModel, double temperature) => viewModel.Percentage = temperature);

		slider.PropertyChanged += HandleSliderPropertyChanged;
		viewModel.PropertyChanged += HandleViewModelPropertyChanged;

		BindingHelpers.AssertTypedBindingExists(slider, Slider.ValueProperty, BindingMode.Default, viewModel);
		Assert.That(slider.Value, Is.EqualTo(ViewModel.DefaultPercentage));

		slider.Value = 1;
		Assert.That(slider.Value, Is.EqualTo(1));
		Assert.That(viewModel.Percentage, Is.EqualTo(1));

		viewModel.Percentage = 0.6;
		var viewModelPropertyName = await viewModelPropertyChangedEventArgsTCS.Task;
		var sliderPropertyName = await sliderPropertyChangedEventArgsTCS.Task;

		Assert.That(didViewModelPropertyChangeFire, Is.True);
		Assert.That(viewModelPropertyName, Is.EqualTo(nameof(ViewModel.Percentage)));
		Assert.That(viewModelPropertyChangedEventCount, Is.EqualTo(2));

		Assert.That(didSliderPropertyChangeFire, Is.True);
		Assert.That(sliderPropertyName, Is.EqualTo(nameof(Slider.Value)));
		Assert.That(sliderPropertyChangedEventCount, Is.EqualTo(2));

		Assert.That(slider.Value, Is.EqualTo(0.6));
		Assert.That(viewModel.Percentage, Is.EqualTo(0.6));

		void HandleViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			didViewModelPropertyChangeFire = true;
			viewModelPropertyChangedEventCount++;
			viewModelPropertyChangedEventArgsTCS.TrySetResult(e.PropertyName);
		}

		void HandleSliderPropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			didSliderPropertyChangeFire = true;
			sliderPropertyChangedEventCount++;
			sliderPropertyChangedEventArgsTCS.TrySetResult(e.PropertyName);
		}
	}

	[TestCase(false, false)]
	[TestCase(false, true)]
	[TestCase(true, false)]
	[TestCase(true, true)]
	public void ValueSetOnOneWayWithNestedPathBinding(bool setContextFirst, bool isDefault)
	{
		var entry = new Entry();
		var bindingMode = isDefault ? BindingMode.Default : BindingMode.OneWay;

		var viewmodel = new NestedViewModel
		{
			Model = new NestedViewModel
			{
				Model = new NestedViewModel()
			}
		};

		if (setContextFirst)
		{
			entry.BindingContext = viewmodel;
			entry.Bind(Entry.TextColorProperty,
				static (NestedViewModel vm) => vm.Model?.Model?.TextColor,
				new (Func<NestedViewModel, object?>, string)[]
				{
					(vm => vm, nameof(NestedViewModel.Model)),
					(vm => vm.Model, nameof(NestedViewModel.Model)),
					(vm => vm.Model?.Model, nameof(NestedViewModel.Model.TextColor))
				},
				static (NestedViewModel vm, Color? color) =>
				{
					if (vm.Model?.Model?.TextColor is not null && color is not null)
					{
						vm.Model.Model.TextColor = color;
					}
				},
				bindingMode);
		}
		else
		{
			entry.Bind(Entry.TextColorProperty,
				static (NestedViewModel vm) => vm.Model?.Model?.TextColor,
				new (Func<NestedViewModel, object?>, string)[]
				{
					(vm => vm, nameof(NestedViewModel.Model)),
					(vm => vm.Model, nameof(NestedViewModel.Model)),
					(vm => vm.Model?.Model, nameof(NestedViewModel.Model.TextColor))
				},
				static (NestedViewModel vm, Color? color) =>
				{
					if (vm.Model?.Model?.TextColor is not null && color is not null)
					{
						vm.Model.Model.TextColor = color;
					}
				},
				bindingMode);

			entry.BindingContext = viewmodel;
		}

		Assert.That(viewmodel.Model.Model.TextColor, Is.EqualTo(ViewModel.DefaultColor));
		Assert.That(entry.GetValue(Entry.TextColorProperty), Is.EqualTo(ViewModel.DefaultColor));

		var textColor = Colors.Pink;

		viewmodel.Model.Model.TextColor = textColor;
		Assert.That(viewmodel.Model.Model.TextColor, Is.EqualTo(textColor));
		Assert.That(entry.GetValue(Entry.TextColorProperty), Is.EqualTo(textColor));
	}

	[TestCase(false, false)]
	[TestCase(false, true)]
	[TestCase(true, false)]
	[TestCase(true, true)]
	public void ValueSetOnOneWayWithNestedPathBindingWithIValueConverter(bool setContextFirst, bool isDefault)
	{
		var label = new Label();
		var bindingMode = isDefault ? BindingMode.Default : BindingMode.OneWay;

		var viewmodel = new NestedViewModel
		{
			Model = new NestedViewModel
			{
				Model = new NestedViewModel()
			}
		};

		var colorToHexRgbStringConverter = new ColorToHexRgbStringConverter();

		if (setContextFirst)
		{
			label.BindingContext = viewmodel;
			label.Bind<Label, NestedViewModel, Color?, string>(Label.TextProperty,
				static (NestedViewModel vm) => vm.Model?.Model?.TextColor,
				new (Func<NestedViewModel, object?>, string)[]
				{
					(vm => vm, nameof(NestedViewModel.Model)),
					(vm => vm.Model, nameof(NestedViewModel.Model)),
					(vm => vm.Model?.Model, nameof(NestedViewModel.Model.TextColor))
				},
				static (NestedViewModel vm, Color? color) =>
				{
					if (vm.Model?.Model?.TextColor is not null && color is not null)
					{
						vm.Model.Model.TextColor = color;
					}
				},
				bindingMode,
				converter: colorToHexRgbStringConverter);
		}
		else
		{
			label.Bind<Label, NestedViewModel, Color?, string>(Label.TextProperty,
				static (NestedViewModel vm) => vm.Model?.Model?.TextColor,
				new (Func<NestedViewModel, object?>, string)[]
				{
					(vm => vm, nameof(NestedViewModel.Model)),
					(vm => vm.Model, nameof(NestedViewModel.Model)),
					(vm => vm.Model?.Model, nameof(NestedViewModel.Model.TextColor))
				},
				static (NestedViewModel vm, Color? color) =>
				{
					if (vm.Model?.Model?.TextColor is not null && color is not null)
					{
						vm.Model.Model.TextColor = color;
					}
				},
				bindingMode,
				converter: colorToHexRgbStringConverter);

			label.BindingContext = viewmodel;
		}

		Assert.That(viewmodel.Model.Model.TextColor, Is.EqualTo(ViewModel.DefaultColor));
		Assert.That(label.GetValue(Label.TextProperty), Is.EqualTo(colorToHexRgbStringConverter.ConvertFrom(ViewModel.DefaultColor)));

		var textColor = Colors.Pink;

		viewmodel.Model.Model.TextColor = textColor;
		Assert.That(viewmodel.Model.Model.TextColor, Is.EqualTo(textColor));
		Assert.That(label.GetValue(Label.TextProperty), Is.EqualTo(colorToHexRgbStringConverter.ConvertFrom(textColor)));
	}

	[Test]
	public async Task ConfirmReadOnlyTypedBindingWithIValueConverter()
	{
		ArgumentNullException.ThrowIfNull(viewModel);

		var colorToHexRgbStringConverter = new ColorToHexRgbStringConverter();
		var updatedTextColor = Colors.Pink;

		bool didViewModelPropertyChangeFire = false;
		int viewModelPropertyChangedEventCount = 0;
		TaskCompletionSource<string?> viewModelPropertyChangedEventArgsTCS = new();

		bool didLabelPropertyChangeFire = false;
		int labelPropertyChangedEventCount = 0;
		TaskCompletionSource<string?> labelPropertyChangedEventArgsTCS = new();

		var label = new Label
		{
			BindingContext = viewModel
		}.Bind<Label, ViewModel, Color, string>(Label.TextProperty,
			static (ViewModel viewModel) => viewModel.TextColor,
			converter: colorToHexRgbStringConverter);

		label.PropertyChanged += HandleLabelPropertyChanged;
		viewModel.PropertyChanged += HandleViewModelPropertyChanged;

		BindingHelpers.AssertTypedBindingExists<Label, ViewModel, Color, object?, string>(
			label,
			Label.TextProperty,
			BindingMode.Default,
			viewModel,
			expectedConverter: colorToHexRgbStringConverter);

		viewModel.TextColor = updatedTextColor;
		var viewModelPropertyName = await viewModelPropertyChangedEventArgsTCS.Task;
		var labelPropertyName = await labelPropertyChangedEventArgsTCS.Task;

		Assert.That(didViewModelPropertyChangeFire, Is.True);
		Assert.That(viewModelPropertyName, Is.EqualTo(nameof(ViewModel.TextColor)));
		Assert.That(viewModelPropertyChangedEventCount, Is.EqualTo(1));

		Assert.That(didLabelPropertyChangeFire, Is.True);
		Assert.That(labelPropertyName, Is.EqualTo(nameof(Label.Text)));
		Assert.That(labelPropertyChangedEventCount, Is.EqualTo(1));
		Assert.That(colorToHexRgbStringConverter.ConvertFrom(updatedTextColor), Is.EqualTo(label.Text));

		void HandleViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			didViewModelPropertyChangeFire = true;
			viewModelPropertyChangedEventCount++;
			viewModelPropertyChangedEventArgsTCS.TrySetResult(e.PropertyName);
		}

		void HandleLabelPropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			didLabelPropertyChangeFire = true;
			labelPropertyChangedEventCount++;
			labelPropertyChangedEventArgsTCS.TrySetResult(e.PropertyName);
		}
	}

	[Test]
	public async Task ConfirmReadOnlyTypedBindingWithFuncConversion()
	{
		ArgumentNullException.ThrowIfNull(viewModel);

		bool didViewModelPropertyChangeFire = false;
		int viewModelPropertyChangedEventCount = 0;
		TaskCompletionSource<string?> viewModelPropertyChangedEventArgsTCS = new();

		bool didSliderPropertyChangeFire = false;
		int sliderPropertyChangedEventCount = 0;
		TaskCompletionSource<string?> sliderPropertyChangedEventArgsTCS = new();

		var slider = new Slider
		{
			BindingContext = viewModel
		}.Bind(Slider.ThumbColorProperty,
			static (ViewModel viewModel) => viewModel.Percentage,
			convert: (double percentage) => percentage > 0.5 ? Colors.Green : Colors.Red);

		slider.PropertyChanged += HandleSliderPropertyChanged;
		viewModel.PropertyChanged += HandleViewModelPropertyChanged;

		BindingHelpers.AssertTypedBindingExists<Slider, ViewModel, double, Color>(
			slider,
			Slider.ThumbColorProperty,
			BindingMode.Default,
			viewModel,
			percentage => percentage > 0.5 ? Colors.Green : Colors.Red);

		viewModel.Percentage = 0.6;
		var viewModelPropertyName = await viewModelPropertyChangedEventArgsTCS.Task;
		var sliderPropertyName = await sliderPropertyChangedEventArgsTCS.Task;

		Assert.That(didViewModelPropertyChangeFire, Is.True);
		Assert.That(viewModelPropertyName, Is.EqualTo(nameof(ViewModel.Percentage)));
		Assert.That(viewModelPropertyChangedEventCount, Is.EqualTo(1));

		Assert.That(didSliderPropertyChangeFire, Is.True);
		Assert.That(sliderPropertyName, Is.EqualTo(nameof(Slider.ThumbColor)));
		Assert.That(sliderPropertyChangedEventCount, Is.EqualTo(1));
		Assert.That(slider.ThumbColor, Is.EqualTo(Colors.Green));

		void HandleViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			didViewModelPropertyChangeFire = true;
			viewModelPropertyChangedEventCount++;
			viewModelPropertyChangedEventArgsTCS.TrySetResult(e.PropertyName);
		}

		void HandleSliderPropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			didSliderPropertyChangeFire = true;
			sliderPropertyChangedEventCount++;
			sliderPropertyChangedEventArgsTCS.TrySetResult(e.PropertyName);
		}
	}

	class ViewModel : INotifyPropertyChanged
	{
		public const double DefaultPercentage = 0.5;

		double percentage = DefaultPercentage;
		Color textColor = DefaultColor;

		public ViewModel()
		{
			Command = new Command(() => TextColor = colors.Skip(Random.Shared.Next(colors.Keys.Count())).First().Value);
		}

		public static Color DefaultColor { get; } = Colors.Transparent;

		public bool IsRed => TextColor == Colors.Red;

		public Guid Id { get; } = Guid.NewGuid();

		public ICommand Command { get; }

		public event PropertyChangedEventHandler? PropertyChanged;

		public double Percentage
		{
			get => percentage;
			set => SetProperty(ref percentage, value);
		}

		public Color TextColor
		{
			get => textColor;
			set => SetProperty(ref textColor, value);
		}

		protected void SetProperty<T>(ref T backingStore, in T value, [CallerMemberName] in string propertyname = "")
		{
			if (EqualityComparer<T>.Default.Equals(backingStore, value))
			{
				return;
			}

			backingStore = value;

			OnPropertyChanged(propertyname);
		}

		void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	class NestedViewModel : ViewModel
	{
		NestedViewModel? model;

		public NestedViewModel? Model
		{
			get => model;
			set => SetProperty(ref model, value);
		}
	}
}