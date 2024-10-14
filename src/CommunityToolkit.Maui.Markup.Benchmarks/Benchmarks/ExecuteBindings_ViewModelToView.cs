using BenchmarkDotNet.Attributes;
namespace CommunityToolkit.Maui.Markup.Benchmarks;

[MemoryDiagnoser]
public class ExecuteBindings_ViewModelToView : ExecuteBindingsBase
{
	[Benchmark(Baseline = true)]
	public void ExecuteDefaultBindings_ViewModelToView()
	{
		DefaultBindingsLabelViewModel.TextColor = Colors.Green;
		DefaultBindingsLabelViewModel.Text = helloWorldText;
	}

	[Benchmark]
	public void ExecuteTypedBindingsMarkup_ViewModelToView()
	{
		TypedMarkupBindingsLabelViewModel.TextColor = Colors.Green;
		TypedMarkupBindingsLabelViewModel.Text = helloWorldText;
	}
}