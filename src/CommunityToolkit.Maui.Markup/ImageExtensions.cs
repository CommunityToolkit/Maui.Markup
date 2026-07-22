namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension methods for <see cref="Image"/> and <see cref="ImageButton"/>
/// </summary>
public static class ImageExtensions
{
	/// <summary>
	/// Sets the <see cref="IImageSourcePart.Source" /> property.
	/// </summary>
	/// <typeparam name="TBindable">The <see cref="BindableObject"/> type implementing <see cref="IImageSourcePart"/>.</typeparam>
	/// <param name="bindable">The <see cref="BindableObject"/> on which to set the <see cref="IImageSourcePart.Source"/> property.</param>
	/// <param name="imageSource">The <see cref="Microsoft.Maui.Controls.ImageSource"/> value</param>
	/// <returns></returns>
	public static TBindable Source<TBindable>(this TBindable bindable, ImageSource? imageSource) where TBindable : BindableObject, IImageSourcePart
	{
		return SetSource(bindable, imageSource);
	}

	/// <summary>
	/// Sets the <see cref="Microsoft.Maui.IImage.Aspect" /> property.
	/// </summary>
	/// <typeparam name="TBindable">The <see cref="BindableObject"/> type implementing <see cref="Microsoft.Maui.IImage"/>.</typeparam>
	/// <param name="bindable">The <see cref="BindableObject"/> on which to set the <see cref="Microsoft.Maui.IImage.Aspect"/> property.</param>
	/// <param name="aspect">The <see cref="Microsoft.Maui.Aspect"/> value.</param>
	/// <returns></returns>
	public static TBindable Aspect<TBindable>(this TBindable bindable, Aspect aspect) where TBindable : BindableObject, Microsoft.Maui.IImage
	{
		return SetAspect(bindable, aspect);
	}

	/// <summary>
	/// Sets the <see cref="Microsoft.Maui.IImage.IsOpaque" /> property.
	/// </summary>
	/// <typeparam name="TBindable">The <see cref="BindableObject"/> type implementing <see cref="Microsoft.Maui.IImage"/>.</typeparam>
	/// <param name="bindable">The <see cref="BindableObject"/> on which to set the <see cref="Microsoft.Maui.IImage.IsOpaque"/> property.</param>
	/// <param name="isOpaque">The <see cref="bool"/> value.</param>
	/// <returns></returns>
	public static TBindable IsOpaque<TBindable>(this TBindable bindable, bool isOpaque) where TBindable : BindableObject, Microsoft.Maui.IImage
	{
		return SetIsOpaque(bindable, isOpaque);
	}

	static TBindable SetSource<TBindable>(TBindable bindable, ImageSource? imageSource) where TBindable : BindableObject
	{
		bindable.SetValue(BindablePropertyHelpers.GetImageSourceProperty(bindable), imageSource);
		return bindable;
	}

	static TBindable SetAspect<TBindable>(TBindable bindable, Aspect aspect) where TBindable : BindableObject
	{
		bindable.SetValue(BindablePropertyHelpers.GetImageAspectProperty(bindable), aspect);
		return bindable;
	}

	static TBindable SetIsOpaque<TBindable>(TBindable bindable, bool isOpaque) where TBindable : BindableObject
	{
		bindable.SetValue(BindablePropertyHelpers.GetImageIsOpaqueProperty(bindable), isOpaque);
		return bindable;
	}
}