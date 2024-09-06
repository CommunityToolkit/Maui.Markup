using IImage = Microsoft.Maui.IImage;
namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension methods for IImage
/// </summary>
public static class ImageExtensions
{
	/// <summary>
	/// Sets the <see cref="IImageSourcePart.Source" /> Property/>
	/// </summary>
	/// <typeparam name="TBindable"><see cref="BindableObject"/> : <see cref="IImageSourcePart"/></typeparam>
	/// <param name="bindable">The <see cref="BindableObject"/> on which to set the <see cref="IImageSourcePart.Source"/> Property</param>
	/// <param name="imageSource">The <see cref="Microsoft.Maui.Controls.ImageSource"/> value</param>
	/// <returns></returns>
	public static TBindable Source<TBindable>(this TBindable bindable, ImageSource? imageSource) where TBindable : BindableObject, IImageSourcePart
	{
		bindable.SetValue(ImageElement.SourceProperty, imageSource);
		return bindable;
	}

	/// <summary>
	/// Sets the <see cref="Microsoft.Maui.IImage.Aspect" /> Property/>
	/// </summary>
	/// <typeparam name="TBindable"><see cref="BindableObject"/> : <see cref="Microsoft.Maui.IImage"/></typeparam>
	/// <param name="bindable">The <see cref="BindableObject"/> on which to set the <see cref="Microsoft.Maui.IImage.Aspect"/> Property</param>
	/// <param name="aspect">The <see cref="Microsoft.Maui.Aspect"/> vaue</param>
	/// <returns></returns>
	public static TBindable Aspect<TBindable>(this TBindable bindable, Aspect aspect) where TBindable : BindableObject, IImage
	{
		bindable.SetValue(ImageElement.AspectProperty, aspect);
		return bindable;
	}

	/// <summary>
	/// Sets the <see cref="Microsoft.Maui.IImage.IsOpaque" /> Property/>
	/// </summary>
	/// <typeparam name="TBindable"><see cref="BindableObject"/> : <see cref="Microsoft.Maui.IImage"/></typeparam>
	/// <param name="bindable">The <see cref="BindableObject"/> on which to set the <see cref="Microsoft.Maui.IImage.IsOpaque"/> Property</param>
	/// <param name="isOpaque">The <see cref="bool"/> vaue</param>
	/// <returns></returns>
	public static TBindable IsOpaque<TBindable>(this TBindable bindable, bool isOpaque) where TBindable : BindableObject, IImage
	{
		bindable.SetValue(ImageElement.IsOpaqueProperty, isOpaque);
		return bindable;
	}
}