using BenchmarkDotNet.Attributes;

namespace CommunityToolkit.Maui.Markup.Benchmarks;

[MemoryDiagnoser]
public class InitializeBindings : BaseTest
{
	readonly Label defaultBindingsLabel = new()
	{
		BindingContext = new LabelViewModel()
	};
	
	readonly Label defaultMarkupBindingsLabel = new()
	{
		BindingContext = new LabelViewModel()
	};
	
	readonly Label typedMarkupBindingsLabel = new()
	{
		BindingContext = new LabelViewModel()
	};

	[Benchmark(Baseline = true)]
	public void DefaultBindings()
	{
		defaultBindingsLabel.SetBinding(Label.TextProperty, nameof(LabelViewModel.Text));
		defaultBindingsLabel.SetBinding(Label.TextColorProperty, nameof(LabelViewModel.TextColor));
	}
	
	[Benchmark]
	public void DefaultBindingsMarkup()
	{
		defaultMarkupBindingsLabel
			.Bind(Label.TextProperty, nameof(LabelViewModel.Text))
			.Bind(Label.TextColorProperty, nameof(LabelViewModel.TextColor));
	}
	
	[Benchmark]
	public void TypedBindingsMarkup()
	{
		typedMarkupBindingsLabel
			.Bind(Label.TextProperty,
				getter: (LabelViewModel vm) => vm.Text)
			.Bind(Label.TextColorProperty,
				getter: (LabelViewModel vm) => vm.TextColor);
	}
}