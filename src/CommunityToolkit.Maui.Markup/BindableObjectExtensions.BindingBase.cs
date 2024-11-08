namespace CommunityToolkit.Maui.Markup;

public partial class BindableObjectExtensions
{
	/// <summary>Bind to a specified property with inline conversion and conversion parameter</summary>
	public static TBindable Bind<TBindable>(
		this TBindable bindable,
		BindableProperty targetProperty,
		BindingBase binding)
		where TBindable : BindableObject
	{
		bindable.SetBinding(targetProperty, binding);
		return bindable;
	}
}