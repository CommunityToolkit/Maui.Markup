using Microsoft.Maui.Controls.Internals;
namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension Methods for IDynamicResourceHandler
/// </summary>
public static class DynamicResourceHandlerExtensions
{
	/// <summary>
	/// Set Dynamic Resource
	/// </summary>
	/// <typeparam name="TDynamicResourceHandler"></typeparam>
	/// <param name="dynamicResourceHandler"></param>
	/// <param name="property"></param>
	/// <param name="key"></param>
	/// <returns>Layout with added Dynamic Resource</returns>
	public static TDynamicResourceHandler DynamicResource<TDynamicResourceHandler>(this TDynamicResourceHandler dynamicResourceHandler, BindableProperty property, string key)
		where TDynamicResourceHandler : IDynamicResourceHandler
	{
		dynamicResourceHandler.SetDynamicResource(property, key);

		return dynamicResourceHandler;
	}

	/// <summary>
	/// Set Dynamic Resource
	/// </summary>
	/// <typeparam name="TDynamicResourceHandler"></typeparam>
	/// <param name="dynamicResourceHandler"></param>
	/// <param name="resources"></param>
	/// <returns>Layout with added Dynamic Resource</returns>
	public static TDynamicResourceHandler DynamicResources<TDynamicResourceHandler>(this TDynamicResourceHandler dynamicResourceHandler, params ReadOnlySpan<(BindableProperty property, string key)> resources)
		where TDynamicResourceHandler : IDynamicResourceHandler
	{
		foreach (var (property, key) in resources)
		{
			dynamicResourceHandler.DynamicResource(property, key);
		}

		return dynamicResourceHandler;
	}
}