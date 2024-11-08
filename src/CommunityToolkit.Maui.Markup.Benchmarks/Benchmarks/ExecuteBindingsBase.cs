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

		DefaultBindingsLabel.SetBinding(Label.TextProperty, BindingBase.Create(static (LabelViewModel vm) => vm.Text, mode: BindingMode.TwoWay));
		DefaultBindingsLabel.SetBinding(Label.TextColorProperty, BindingBase.Create(static (LabelViewModel vm) => vm.TextColor, mode: BindingMode.TwoWay));
		DefaultBindingsLabel.EnableAnimations();

		TypedMarkupBindingsLabel = new Label
			{
				BindingContext = TypedMarkupBindingsLabelViewModel
			}.Bind(Label.TextProperty,
				getter: static (LabelViewModel vm) => vm.Text,
				setter: static (vm, text) => vm.Text = text,
				mode: BindingMode.TwoWay)
			.Bind(Label.TextColorProperty,
				getter: static (LabelViewModel vm) => vm.TextColor,
				setter: static (vm, textColor) => vm.TextColor = textColor,
				mode: BindingMode.TwoWay);
		TypedMarkupBindingsLabel.EnableAnimations();

		MarkupBindingBaseCreateBindingsLabel = new Label
		{
			BindingContext = MarkupBindingBaseCreateBindingsViewModel
		}.Bind(Label.TextProperty, BindingBase.Create(
			getter: static (LabelViewModel vm) => vm.Text,
			mode: BindingMode.TwoWay));
		TypedMarkupBindingsLabel.EnableAnimations();
	}

	protected internal LabelViewModel DefaultBindingsLabelViewModel { get; } = new();
	protected internal LabelViewModel TypedMarkupBindingsLabelViewModel { get; } = new();
	protected internal LabelViewModel MarkupBindingBaseCreateBindingsViewModel { get; } = new();

	protected Label DefaultBindingsLabel { get; }
	protected Label TypedMarkupBindingsLabel { get; }
	protected Label MarkupBindingBaseCreateBindingsLabel { get; }
}