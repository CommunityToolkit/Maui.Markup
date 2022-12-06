using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
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
		System.Threading.Thread.CurrentThread.CurrentCulture = System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
	}

	[TearDown]
	public override void TearDown()
	{
		viewModel = null;
		base.TearDown();
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
		}.Bind<Label, ViewModel, double>(Label.TextProperty, static (ViewModel viewModel) => viewModel.Percentage, stringFormat: stringFormat);

		viewModel.PropertyChanged += HandlePropertyChanged;

		BindingHelpers.AssertTypedBindingExists<Label, ViewModel, string>(label, Label.TextProperty, BindingMode.Default, viewModel, stringFormat);
		Assert.AreEqual(string.Format(stringFormat, ViewModel.DefaultPercentage), label.Text);

		label.Text = string.Format(stringFormat, 0.1);
		Assert.AreEqual("0.1%", label.Text);
		Assert.AreEqual(ViewModel.DefaultPercentage, viewModel.Percentage);

		viewModel.Percentage = 0.6;
		var propertyName = await propertyChangedEventArgsTCS.Task;

		Assert.True(didPropertyChangeFire);
		Assert.AreEqual(nameof(ViewModel.Percentage), propertyName);

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
		}.Bind<Label, ViewModel, Color>(Label.TextColorProperty, static (ViewModel viewModel) => viewModel.TextColor);

		viewModel.PropertyChanged += HandleViewModelPropertyChanged;
		label.PropertyChanged += HandleLabelPropertyChanged;

		BindingHelpers.AssertTypedBindingExists<Label, ViewModel, Color>(label, Label.TextColorProperty, BindingMode.Default, viewModel);
		Assert.AreEqual(ViewModel.DefaultColor, label.TextColor);

		viewModel.TextColor = Colors.Green;
		var viewModelPropertyName = await viewModelPropertyChangedEventArgsTCS.Task;
		var labelPropertyName = await labelPropertyChangedEventArgsTCS.Task;

		Assert.True(didViewModelPropertyChangeFire);
		Assert.AreEqual(nameof(ViewModel.TextColor), viewModelPropertyName);

		Assert.True(didLabelPropertyChangeFire);
		Assert.AreEqual(nameof(Label.TextColor), labelPropertyName);

		Assert.AreEqual(Colors.Green, viewModel.TextColor);
		Assert.AreEqual(Colors.Green, label.GetValue(Label.TextColorProperty));

		label.TextColor = Colors.Red;
		Assert.AreEqual(Colors.Red, label.TextColor);
		Assert.AreEqual(Colors.Green, viewModel.TextColor);

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
		}.Bind<Label, ViewModel, Color>(Label.TextColorProperty, static (ViewModel viewModel) => viewModel.TextColor, static (ViewModel viewModel, Color color) => viewModel.TextColor = color, BindingMode.OneTime);

		viewModel.PropertyChanged += HandleViewModelPropertyChanged;
		label.PropertyChanged += HandleLabelPropertyChanged;

		BindingHelpers.AssertTypedBindingExists<Label, ViewModel, Color>(label, Label.TextColorProperty, BindingMode.OneTime, viewModel);
		Assert.AreEqual(ViewModel.DefaultColor, label.TextColor);

		viewModel.TextColor = Colors.Green;
		Assert.AreEqual(Colors.Green, viewModel.TextColor);
		Assert.AreEqual(ViewModel.DefaultColor, label.GetValue(Label.TextColorProperty));

		label.TextColor = Colors.Red;
		Assert.AreEqual(Colors.Red, label.TextColor);
		Assert.AreEqual(Colors.Green, viewModel.TextColor);

		Assert.True(didViewModelPropertyChangeFire);
		Assert.True(didLabelPropertyChangeFire);

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
		}.Bind<Slider, ViewModel, double>(Slider.ValueProperty, static (ViewModel viewModel) => viewModel.Percentage, static (ViewModel viewModel, double temperature) => viewModel.Percentage = temperature);

		slider.PropertyChanged += HandleSliderPropertyChanged;
		viewModel.PropertyChanged += HandleViewModelPropertyChanged;

		BindingHelpers.AssertTypedBindingExists<Slider, ViewModel, string>(slider, Slider.ValueProperty, BindingMode.Default, viewModel);
		Assert.AreEqual(ViewModel.DefaultPercentage, slider.Value);

		slider.Value = 1;
		Assert.AreEqual(1, slider.Value);
		Assert.AreEqual(1, viewModel.Percentage);

		viewModel.Percentage = 0.6;
		var viewModelPropertyName = await viewModelPropertyChangedEventArgsTCS.Task;
		var sliderPropertyName = await sliderPropertyChangedEventArgsTCS.Task;

		Assert.True(didViewModelPropertyChangeFire);
		Assert.AreEqual(nameof(ViewModel.Percentage), viewModelPropertyName);
		Assert.AreEqual(2, viewModelPropertyChangedEventCount);

		Assert.True(didSliderPropertyChangeFire);
		Assert.AreEqual(nameof(Slider.Value), sliderPropertyName);
		Assert.AreEqual(2, sliderPropertyChangedEventCount);

		Assert.AreEqual(0.6, slider.Value);
		Assert.AreEqual(0.6, viewModel.Percentage);

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
		var bindable = new Entry();
		var bindingMode = isDefault ? BindingMode.Default : BindingMode.OneWay;
		var textColor = Colors.Pink;

		var viewmodel = new NestedViewModel
		{
			Model = new NestedViewModel
			{
				Model = new NestedViewModel
				{
					TextColor = textColor
				}
			}
		};

		if (setContextFirst)
		{
			bindable.BindingContext = viewmodel;
			bindable.Bind(Entry.TextColorProperty,
							static (NestedViewModel vm) => vm.Model?.Model?.TextColor,
							new (Func<NestedViewModel, object?>, string)[]
							{
								(vm => vm, "Model"),
								(vm => vm.Model, "Model"),
								(vm => vm.Model.Model, "Text")
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
			bindable.Bind(Entry.TextColorProperty,
							static (NestedViewModel vm) => vm.Model?.Model?.TextColor,
							new (Func<NestedViewModel, object?>, string)[]
							{
								(vm => vm, "Model"),
								(vm => vm.Model, "Model"),
								(vm => vm.Model.Model, "Text")
							},
							static (NestedViewModel vm, Color? color) =>
							{
								if (vm.Model?.Model?.TextColor is not null && color is not null)
								{
									vm.Model.Model.TextColor = color;
								}
							},
							bindingMode);
			bindable.BindingContext = viewmodel;
		}

		Assert.AreEqual(textColor, viewmodel.Model.Model.TextColor);
		Assert.AreEqual(textColor, bindable.GetValue(Entry.TextColorProperty));
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