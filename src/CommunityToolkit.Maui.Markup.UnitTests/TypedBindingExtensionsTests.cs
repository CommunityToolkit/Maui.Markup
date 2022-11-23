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
using Microsoft.Maui.Dispatching;
using Microsoft.Maui.Graphics;
using NUnit.Framework;

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
	public async Task ConfirmReadOnlyTypedBinding()
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

		BindingHelpers.AssertTypedBindingExists<Label, ViewModel, string>(label, Label.TextProperty, BindingMode.OneWay, viewModel, stringFormat);
		Assert.AreEqual(string.Format(stringFormat, ViewModel.DefaultPercentage), label.Text);

		label.Text = string.Format(stringFormat, 0.1);
		Assert.AreEqual("0.1%", label.Text);
		Assert.AreEqual(ViewModel.DefaultPercentage, viewModel.Percentage);

		await label.Dispatcher.DispatchAsync(()=> viewModel.Percentage = 0.6);
		var propertyName= await propertyChangedEventArgsTCS.Task;

		Assert.True(didPropertyChangeFire);
		Assert.AreEqual(nameof(ViewModel.Percentage), propertyName);

		void HandlePropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			didPropertyChangeFire = true;
			propertyChangedEventArgsTCS.SetResult(e.PropertyName);
		}
	}

	[Test]
	public void ConfirmReadWriteTypedBinding()
	{
		ArgumentNullException.ThrowIfNull(viewModel);

		var slider = new Slider
		{
			BindingContext = viewModel
		}.Bind<Slider, ViewModel, double>(Slider.ValueProperty, getter, setter);

		BindingHelpers.AssertTypedBindingExists<Slider, ViewModel, string>(slider, Slider.ValueProperty, BindingMode.Default, viewModel);
		Assert.AreEqual(ViewModel.DefaultPercentage, slider.Value);

		slider.Value = 1;
		Assert.AreEqual(1, slider.Value);
		Assert.AreEqual(1, viewModel.Percentage);

		static double getter(ViewModel viewModel) => viewModel.Percentage;
		static void setter(ViewModel viewModel, double temperature) => viewModel.Percentage = temperature;
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

		protected void SetProperty<T>(ref T backingStore, in T value, in Action? onChanged = null, [CallerMemberName] in string propertyname = "")
		{
			if (EqualityComparer<T>.Default.Equals(backingStore, value))
			{
				return;
			}

			backingStore = value;

			onChanged?.Invoke();

			OnPropertyChanged(propertyname);
		}

		void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}