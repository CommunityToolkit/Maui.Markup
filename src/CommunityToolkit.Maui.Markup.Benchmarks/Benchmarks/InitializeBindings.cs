using BenchmarkDotNet.Attributes;

namespace CommunityToolkit.Maui.Markup.Benchmarks;

[MemoryDiagnoser]
public class InitializeBindings : BaseTest
{
	readonly Label defaultBindingsLabel = new()
	{
		BindingContext = new LabelViewModel()
	};

	readonly Label defaultBindingBaseCreateLabel = new()
	{
		BindingContext = new LabelViewModel()
	};

	readonly Label typedMarkupBindingsLabel = new()
	{
		BindingContext = new LabelViewModel()
	};

	[Benchmark(Baseline = true)]
	public void InitializeDefaultStringBindings()
	{
		defaultBindingsLabel.SetBinding(Label.TextProperty, nameof(LabelViewModel.Text), mode: BindingMode.TwoWay);
		defaultBindingsLabel.SetBinding(Label.TextColorProperty, nameof(LabelViewModel.TextColor), mode: BindingMode.TwoWay);
	}

	[Benchmark]
	public void InitializeDefaultLambdaBindings()
	{
		defaultBindingBaseCreateLabel.SetBinding(Label.TextProperty,
			BindingBase.Create(static (LabelViewModel vm) => vm.Text, mode: BindingMode.TwoWay));
		defaultBindingBaseCreateLabel.Bind(Label.TextColorProperty,
			BindingBase.Create(static (LabelViewModel vm) => vm.TextColor, mode: BindingMode.TwoWay));
	}

	[Benchmark]
	public void InitializeMarkupBindingsWithBindingBaseCreate()
	{
		defaultBindingBaseCreateLabel
			.Bind(Label.TextProperty,
				BindingBase.Create(static (LabelViewModel vm) => vm.Text, mode: BindingMode.TwoWay))
			.Bind(Label.TextColorProperty,
				BindingBase.Create(static (LabelViewModel vm) => vm.TextColor, mode: BindingMode.TwoWay));
	}

	[Benchmark]
	public void InitializeTypedBindingsMarkup()
	{
		typedMarkupBindingsLabel
			.Bind(Label.TextProperty,
				getter: (LabelViewModel vm) => vm.Text,
				setter: (vm, text) => vm.Text = text,
				mode: BindingMode.TwoWay)
			.Bind(Label.TextColorProperty,
				getter: (LabelViewModel vm) => vm.TextColor,
				setter: (vm, textColor) => vm.TextColor = textColor,
				mode: BindingMode.TwoWay);
	}
}