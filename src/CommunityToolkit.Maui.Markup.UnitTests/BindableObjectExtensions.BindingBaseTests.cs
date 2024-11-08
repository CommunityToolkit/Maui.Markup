using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using NUnit.Framework;
namespace CommunityToolkit.Maui.Markup.UnitTests;

class BindingBaseTests : BaseTestFixture
{
	static readonly IReadOnlyDictionary<string, Color> colors = typeof(Colors)
		.GetFields(BindingFlags.Static | BindingFlags.Public)
		.ToDictionary(c => c.Name, c => (Color)(c.GetValue(null) ?? throw new InvalidOperationException()));

	[Test]
	public async Task ConfirmStringFormat()
	{
		var viewModel = new ViewModel();

		const string stringFormat = "{0}%";
		bool didPropertyChangeFire = false;
		TaskCompletionSource<string?> propertyChangedEventArgsTCS = new();

		var label = new Label
		{
			BindingContext = viewModel
		}.Bind(Label.TextProperty, BindingBase.Create(static (ViewModel viewModel) => viewModel.Percentage, stringFormat: stringFormat));

		viewModel.PropertyChanged += HandlePropertyChanged;

		BindingHelpers.AssertTypedBindingExists(label, Label.TextProperty, BindingMode.Default, viewModel, stringFormat);
		Assert.That(label.Text, Is.EqualTo(string.Format(stringFormat, ViewModel.DefaultPercentage)));

		label.Text = string.Format(stringFormat, 0.1);

		Assert.Multiple(() =>
		{
			Assert.That(label.Text, Is.EqualTo("0.1%"));
			Assert.That(viewModel.Percentage, Is.EqualTo(ViewModel.DefaultPercentage));
		});

		viewModel.Percentage = 0.6;
		var propertyName = await propertyChangedEventArgsTCS.Task;

		Assert.Multiple(() =>
		{
			Assert.That(didPropertyChangeFire, Is.True);
			Assert.That(propertyName, Is.EqualTo(nameof(ViewModel.Percentage)));
		});

		void HandlePropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			didPropertyChangeFire = true;
			propertyChangedEventArgsTCS.SetResult(e.PropertyName);
		}
	}

	[Test]
	public async Task ConfirmReadOnlyTypedBindings()
	{
		var viewModel = new ViewModel();

		bool didViewModelPropertyChangeFire = false;
		TaskCompletionSource<string?> viewModelPropertyChangedEventArgsTCS = new();

		bool didLabelPropertyChangeFire = false;
		TaskCompletionSource<string?> labelPropertyChangedEventArgsTCS = new();

		var label = new Label
		{
			BindingContext = viewModel
		}.Bind(Label.TextColorProperty, BindingBase.Create(static (ViewModel viewModel) => viewModel.TextColor));

		viewModel.PropertyChanged += HandleViewModelPropertyChanged;
		label.PropertyChanged += HandleLabelPropertyChanged;

		BindingHelpers.AssertTypedBindingExists(label, Label.TextColorProperty, BindingMode.Default, viewModel);
		Assert.That(label.TextColor, Is.EqualTo(ViewModel.DefaultColor));

		viewModel.TextColor = Colors.Green;
		var viewModelPropertyName = await viewModelPropertyChangedEventArgsTCS.Task;
		var labelPropertyName = await labelPropertyChangedEventArgsTCS.Task;

		Assert.Multiple(() =>
		{
			Assert.That(didViewModelPropertyChangeFire, Is.True);
			Assert.That(viewModelPropertyName, Is.EqualTo(nameof(ViewModel.TextColor)));

			Assert.That(didLabelPropertyChangeFire, Is.True);
			Assert.That(labelPropertyName, Is.EqualTo(nameof(Label.TextColor)));

			Assert.That(viewModel.TextColor, Is.EqualTo(Colors.Green));
			Assert.That(label.GetValue(Label.TextColorProperty), Is.EqualTo(Colors.Green));
		});

		label.TextColor = Colors.Red;

		Assert.Multiple(() =>
		{
			Assert.That(label.TextColor, Is.EqualTo(Colors.Red));
			Assert.That(viewModel.TextColor, Is.EqualTo(Colors.Green));
		});

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
		var viewModel = new ViewModel();

		bool didLabelPropertyChangeFire = false;
		bool didViewModelPropertyChangeFire = false;

		var label = new Label
		{
			BindingContext = viewModel
		}.Bind(Label.TextColorProperty, BindingBase.Create(static (ViewModel viewModel) => viewModel.TextColor, BindingMode.OneTime));

		viewModel.PropertyChanged += HandleViewModelPropertyChanged;
		label.PropertyChanged += HandleLabelPropertyChanged;

		BindingHelpers.AssertTypedBindingExists(label, Label.TextColorProperty, BindingMode.OneTime, viewModel);
		Assert.That(label.TextColor, Is.EqualTo(ViewModel.DefaultColor));

		viewModel.TextColor = Colors.Green;
		Assert.Multiple(() =>
		{
			Assert.That(viewModel.TextColor, Is.EqualTo(Colors.Green));
			Assert.That(label.GetValue(Label.TextColorProperty), Is.EqualTo(ViewModel.DefaultColor));
		});

		label.TextColor = Colors.Red;

		Assert.Multiple(() =>
		{
			Assert.That(label.TextColor, Is.EqualTo(Colors.Red));
			Assert.That(viewModel.TextColor, Is.EqualTo(Colors.Green));

			Assert.That(didViewModelPropertyChangeFire, Is.True);
			Assert.That(didLabelPropertyChangeFire, Is.True);
		});

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
		var viewModel = new ViewModel();

		bool didViewModelPropertyChangeFire = false;
		int viewModelPropertyChangedEventCount = 0;
		TaskCompletionSource<string?> viewModelPropertyChangedEventArgsTCS = new();

		bool didSliderPropertyChangeFire = false;
		int sliderPropertyChangedEventCount = 0;
		TaskCompletionSource<string?> sliderPropertyChangedEventArgsTCS = new();

		var slider = new Slider
		{
			BindingContext = viewModel
		}.Bind(Slider.ValueProperty, BindingBase.Create(static (ViewModel viewModel) => viewModel.Percentage));

		slider.PropertyChanged += HandleSliderPropertyChanged;
		viewModel.PropertyChanged += HandleViewModelPropertyChanged;

		BindingHelpers.AssertTypedBindingExists(slider, Slider.ValueProperty, BindingMode.Default, viewModel);
		Assert.That(slider.Value, Is.EqualTo(ViewModel.DefaultPercentage));

		slider.Value = 1;

		Assert.Multiple(() =>
		{
			Assert.That(slider.Value, Is.EqualTo(1));
			Assert.That(viewModel.Percentage, Is.EqualTo(1));
		});

		viewModel.Percentage = 0.6;
		var viewModelPropertyName = await viewModelPropertyChangedEventArgsTCS.Task;
		var sliderPropertyName = await sliderPropertyChangedEventArgsTCS.Task;

		Assert.Multiple(() =>
		{
			Assert.That(didViewModelPropertyChangeFire, Is.True);
			Assert.That(viewModelPropertyName, Is.EqualTo(nameof(ViewModel.Percentage)));
			Assert.That(viewModelPropertyChangedEventCount, Is.EqualTo(2));

			Assert.That(didSliderPropertyChangeFire, Is.True);
			Assert.That(sliderPropertyName, Is.EqualTo(nameof(Slider.Value)));
			Assert.That(sliderPropertyChangedEventCount, Is.EqualTo(2));

			Assert.That(slider.Value, Is.EqualTo(0.6));
			Assert.That(viewModel.Percentage, Is.EqualTo(0.6));
		});

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
			entry.Bind(Entry.TextColorProperty, BindingBase.Create<NestedViewModel, Color?>(
				static vm => vm.Model?.Model?.TextColor,
				mode: bindingMode));
		}
		else
		{
			entry.Bind(Entry.TextColorProperty, BindingBase.Create<NestedViewModel, Color?>(
				static vm => vm.Model?.Model?.TextColor,
				mode: bindingMode));

			entry.BindingContext = viewmodel;
		}

		Assert.Multiple(() =>
		{
			Assert.That(viewmodel.Model.Model.TextColor, Is.EqualTo(ViewModel.DefaultColor));
			Assert.That(entry.GetValue(Entry.TextColorProperty), Is.EqualTo(ViewModel.DefaultColor));
		});

		var textColor = Colors.Pink;

		viewmodel.Model.Model.TextColor = textColor;

		Assert.Multiple(() =>
		{
			Assert.That(viewmodel.Model.Model.TextColor, Is.EqualTo(textColor));
			Assert.That(entry.GetValue(Entry.TextColorProperty), Is.EqualTo(textColor));
		});
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
			label.Bind(
				Label.TextProperty,
				BindingBase.Create(static (NestedViewModel vm) => vm.Model?.Model?.TextColor,
					bindingMode,
					converter: colorToHexRgbStringConverter));
		}
		else
		{
			label.Bind(
				Label.TextProperty,
				BindingBase.Create(static (NestedViewModel vm) => vm.Model?.Model?.TextColor,
					bindingMode,
					converter: colorToHexRgbStringConverter));

			label.BindingContext = viewmodel;
		}

		Assert.Multiple(() =>
		{
			Assert.That(viewmodel.Model.Model.TextColor, Is.EqualTo(ViewModel.DefaultColor));
			Assert.That(label.GetValue(Label.TextProperty), Is.EqualTo(colorToHexRgbStringConverter.ConvertFrom(ViewModel.DefaultColor)));
		});

		var textColor = Colors.Pink;

		viewmodel.Model.Model.TextColor = textColor;
		Assert.Multiple(() =>
		{
			Assert.That(viewmodel.Model.Model.TextColor, Is.EqualTo(textColor));
			Assert.That(label.GetValue(Label.TextProperty), Is.EqualTo(colorToHexRgbStringConverter.ConvertFrom(textColor)));
		});
	}

	[Test]
	public async Task ConfirmReadOnlyTypedBindingWithIValueConverter()
	{
		var viewModel = new ViewModel();

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
		}.Bind(Label.TextProperty,
			BindingBase.Create(static (ViewModel viewModel) => viewModel.TextColor,
				converter: colorToHexRgbStringConverter));

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

		Assert.Multiple(() =>
		{
			Assert.That(didViewModelPropertyChangeFire, Is.True);
			Assert.That(viewModelPropertyName, Is.EqualTo(nameof(ViewModel.TextColor)));
			Assert.That(viewModelPropertyChangedEventCount, Is.EqualTo(1));

			Assert.That(didLabelPropertyChangeFire, Is.True);
			Assert.That(labelPropertyName, Is.EqualTo(nameof(Label.Text)));
			Assert.That(labelPropertyChangedEventCount, Is.EqualTo(1));
			Assert.That(colorToHexRgbStringConverter.ConvertFrom(updatedTextColor), Is.EqualTo(label.Text));
		});

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

	internal class ViewModel : INotifyPropertyChanged
	{
		public const double DefaultPercentage = 0.5;
		public const double DefaultHeightRequest = 500;

		double percentage = DefaultPercentage, heightRequest = DefaultHeightRequest;
		Color textColor = DefaultColor;

		public ViewModel()
		{
			Command = new Command(() => TextColor = colors.Skip(Random.Shared.Next(colors.Keys.Count())).First().Value);
		}

		public static Color DefaultColor { get; } = Colors.Transparent;

		public bool IsRed => Equals(TextColor, Colors.Red);

		public Guid Id { get; } = Guid.NewGuid();

		public ICommand Command { get; }

		public event PropertyChangedEventHandler? PropertyChanged;

		public double HeightRequest
		{
			get => heightRequest;
			set => SetProperty(ref heightRequest, value);
		}

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

	internal class NestedViewModel : ViewModel
	{
		NestedViewModel? model;

		public NestedViewModel? Model
		{
			get => model;
			set => SetProperty(ref model, value);
		}
	}
}