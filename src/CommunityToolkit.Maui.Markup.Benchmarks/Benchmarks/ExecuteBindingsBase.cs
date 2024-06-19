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
				getter: (LabelViewModel vm) => vm.Text,
				mode: BindingMode.TwoWay)
			.Bind(Label.TextColorProperty,
				getter: (LabelViewModel vm) => vm.TextColor,
				mode: BindingMode.TwoWay);
		TypedMarkupBindingsLabel.EnableAnimations();
	}
	
	protected LabelViewModel DefaultBindingsLabelViewModel { get; } = new();
	protected LabelViewModel DefaultMarkupBindingsLabelViewModel { get; } = new();
	protected LabelViewModel TypedMarkupBindingsLabelViewModel { get; } = new();
	
	protected Label DefaultBindingsLabel { get; }
	protected Label DefaultMarkupBindingsLabel{ get; } 
	protected Label TypedMarkupBindingsLabel{ get; }
}