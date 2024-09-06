using System.Collections;
using ILayout = Microsoft.Maui.ILayout;
namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extensions for Bindable Layouts
/// </summary>
public static class BindableLayoutExtensions
{
	/// <summary>
	/// Adds an EmptyView to a Layout
	/// </summary>
	/// <typeparam name="TLayout"></typeparam>
	/// <param name="layout"></param>
	/// <param name="view"></param>
	/// <returns>Layout with Empty View</returns>
	public static TLayout EmptyView<TLayout>(this TLayout layout, object view) where TLayout : BindableObject, ILayout
	{
		BindableLayout.SetEmptyView(layout, view);
		return layout;
	}

	/// <summary>
	/// SetEmptyViewTemplate 
	/// </summary>
	/// <typeparam name="TLayout"></typeparam>
	/// <param name="layout"></param>
	/// <param name="template"></param>
	/// <returns>Layout with Empty View Tempalte</returns>
	public static TLayout EmptyViewTemplate<TLayout>(this TLayout layout, DataTemplate template) where TLayout : BindableObject, ILayout
	{
		BindableLayout.SetEmptyViewTemplate(layout, template);
		return layout;
	}
	/// <summary>
	/// SetEmptyViewTemplate 
	/// </summary>
	/// <typeparam name="TLayout"></typeparam>
	/// <param name="layout"></param>
	/// <param name="loadTemplate"></param>
	/// <returns>Layout with Empty View Tempalte</returns>
	public static TLayout EmptyViewTemplate<TLayout>(this TLayout layout, Func<object> loadTemplate) where TLayout : BindableObject, ILayout =>
		layout.EmptyViewTemplate(new DataTemplate(loadTemplate));

	/// <summary>
	/// Set Item Source
	/// </summary>
	/// <typeparam name="TLayout"></typeparam>
	/// <param name="layout"></param>
	/// <param name="source"></param>
	/// <returns>Layout with Item Source</returns>
	public static TLayout ItemsSource<TLayout>(this TLayout layout, IEnumerable source) where TLayout : BindableObject, ILayout
	{
		BindableLayout.SetItemsSource(layout, source);
		return layout;
	}

	/// <summary>
	/// Set Item Template
	/// </summary>
	/// <typeparam name="TLayout"></typeparam>
	/// <param name="layout"></param>
	/// <param name="template"></param>
	/// <returns>Layout with Item Template</returns>
	public static TLayout ItemTemplate<TLayout>(this TLayout layout, DataTemplate template) where TLayout : BindableObject, ILayout
	{
		BindableLayout.SetItemTemplate(layout, template);
		return layout;
	}

	/// <summary>
	/// Set Item Template
	/// </summary>
	/// <typeparam name="TLayout"></typeparam>
	/// <param name="layout"></param>
	/// <param name="loadTemplate"></param>
	/// <returns>Layout with Item Template</returns>
	public static TLayout ItemTemplate<TLayout>(this TLayout layout, Func<object> loadTemplate) where TLayout : BindableObject, ILayout =>
		layout.ItemTemplate(new DataTemplate(loadTemplate));

	/// <summary>
	/// Sets Item Template Selector
	/// </summary>
	/// <typeparam name="TLayout"></typeparam>
	/// <param name="layout"></param>
	/// <param name="selector"></param>
	/// <returns>Layout with Item Template Selector</returns>
	public static TLayout ItemTemplateSelector<TLayout>(this TLayout layout, DataTemplateSelector selector) where TLayout : BindableObject, ILayout
	{
		BindableLayout.SetItemTemplateSelector(layout, selector);
		return layout;
	}
}