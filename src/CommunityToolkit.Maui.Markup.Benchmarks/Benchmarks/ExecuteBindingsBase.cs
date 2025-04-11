using CommunityToolkit.Maui.Markup.Benchmarks.Extensions;

namespace CommunityToolkit.Maui.Markup.Benchmarks;

public abstract class ExecuteBindingsBase : BaseTest
{
	protected const string helloWorldText = "Hello World";

	protected ExecuteBindingsBase()
	{
		DefaultBindingsLabel = new()
		{
			BindingContext = DefaultBindingsLabelViewModel
		};

		DefaultBindingsLabel.SetBinding(Label.TextProperty, nameof(LabelViewModel.Text), mode: BindingMode.TwoWay);
		DefaultBindingsLabel.SetBinding(Label.TextColorProperty, nameof(LabelViewModel.TextColor), mode: BindingMode.TwoWay);
		DefaultBindingsLabel.EnableAnimations();

		DefaultMarkupBindingsLabel = new Label
		{
			BindingContext = DefaultMarkupBindingsLabelViewModel
		}.Bind(Label.TextProperty, nameof(LabelViewModel.Text), mode: BindingMode.TwoWay)
			.Bind(Label.TextColorProperty, nameof(LabelViewModel.TextColor), mode: BindingMode.TwoWay);
		DefaultMarkupBindingsLabel.EnableAnimations();

		TypedMarkupBindingsLabel = new Label
		{
			BindingContext = TypedMarkupBindingsLabelViewModel
		}.Bind(Label.TextProperty,
				getter: static (LabelViewModel vm) => vm.Text,
				setter: static (vm, text) => vm.Text = text ?? string.Empty,
				mode: BindingMode.TwoWay)
			.Bind(Label.TextColorProperty,
				getter: static (LabelViewModel vm) => vm.TextColor,
				setter: static (vm, textColor) => vm.TextColor = textColor ?? Colors.Transparent,
				mode: BindingMode.TwoWay);
		TypedMarkupBindingsLabel.EnableAnimations();
	}

	protected LabelViewModel DefaultBindingsLabelViewModel { get; } = new();
	protected LabelViewModel DefaultMarkupBindingsLabelViewModel { get; } = new();
	protected LabelViewModel TypedMarkupBindingsLabelViewModel { get; } = new();

	protected Label DefaultBindingsLabel { get; }
	protected Label DefaultMarkupBindingsLabel { get; }
	protected Label TypedMarkupBindingsLabel { get; }
}