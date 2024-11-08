using BenchmarkDotNet.Attributes;
namespace CommunityToolkit.Maui.Markup.Benchmarks;

[MemoryDiagnoser]
public class ExecuteBindings_ViewToViewModel : ExecuteBindingsBase
{
	[Benchmark(Baseline = true)]
	public void ExecuteDefaultBindings_ViewToViewModel()
	{
		DefaultBindingsLabel.TextColor = Colors.Green;
		DefaultBindingsLabel.Text = helloWorldText;
	}

	[Benchmark]
	public void ExecuteMarkupBindingBaseCreateBindingsViewModel_ViewToViewModel()
	{
		MarkupBindingBaseCreateBindingsLabel.TextColor = Colors.Green;
		MarkupBindingBaseCreateBindingsViewModel.Text = helloWorldText;
	}

	[Benchmark]
	public void ExecuteTypedBindingsMarkup_ViewToViewModel()
	{
		TypedMarkupBindingsLabel.TextColor = Colors.Green;
		TypedMarkupBindingsLabel.Text = helloWorldText;
	}
}