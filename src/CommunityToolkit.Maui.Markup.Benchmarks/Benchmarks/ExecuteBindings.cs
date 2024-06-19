using BenchmarkDotNet.Attributes;
using CommunityToolkit.Maui.Markup.Benchmarks.Extensions;

namespace CommunityToolkit.Maui.Markup.Benchmarks;

[MemoryDiagnoser]
public class ExecuteBindings : BaseTest
{
	const string helloWorldText = "Hello World";

	readonly LabelViewModel defaultBindingsLabelViewModel = new();
	readonly LabelViewModel defaultMarkupBindingsLabelViewModel = new();
	readonly LabelViewModel typedMarkupBindingsLabelViewModel = new();

	readonly Label defaultBindingsLabel, defaultMarkupBindingsLabel, typedMarkupBindingsLabel;

	public ExecuteBindings()
	{
		defaultBindingsLabel = new()
		{
			BindingContext = defaultBindingsLabelViewModel
		};
		defaultBindingsLabel.SetBinding(Label.TextProperty, nameof(LabelViewModel.Text));
		defaultBindingsLabel.SetBinding(Label.TextColorProperty, nameof(LabelViewModel.TextColor));
		defaultBindingsLabel.EnableAnimations();

		defaultMarkupBindingsLabel = new Label
			{
				BindingContext = defaultMarkupBindingsLabelViewModel
			}.Bind(Label.TextProperty, nameof(LabelViewModel.Text))
			.Bind(Label.TextColorProperty, nameof(LabelViewModel.TextColor));
		defaultMarkupBindingsLabel.EnableAnimations();

		typedMarkupBindingsLabel = new Label
			{
				BindingContext = typedMarkupBindingsLabelViewModel
			}.Bind(Label.TextProperty,
				getter: (LabelViewModel vm) => vm.Text)
			.Bind(Label.TextColorProperty,
				getter: (LabelViewModel vm) => vm.TextColor);
		typedMarkupBindingsLabel.EnableAnimations();
	}

	[Benchmark(Baseline = true)]
	public void DefaultBindings()
	{
		defaultBindingsLabelViewModel.TextColor = Colors.Green;
		defaultBindingsLabelViewModel.Text = helloWorldText;
	}

	[Benchmark]
	public void DefaultBindingsMarkup()
	{
		defaultMarkupBindingsLabelViewModel.TextColor = Colors.Green;
		defaultMarkupBindingsLabelViewModel.Text = helloWorldText;
	}

	[Benchmark]
	public void TypedBindingsMarkup()
	{
		typedMarkupBindingsLabelViewModel.TextColor = Colors.Green;
		typedMarkupBindingsLabelViewModel.Text = helloWorldText;
	}
}