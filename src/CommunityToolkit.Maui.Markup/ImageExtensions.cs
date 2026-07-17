namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension methods for <see cref="Image"/> and <see cref="ImageButton"/>
/// </summary>
public static class ImageExtensions
{
	/// <summary>
	/// Sets the <see cref="IImageSourcePart.Source" /> property.
	/// </summary>
	/// <typeparam name="TImage">The <see cref="Image"/> type.</typeparam>
	/// <param name="bindable">The <see cref="BindableObject"/> on which to set the <see cref="IImageSourcePart.Source"/> property.</param>
	/// <param name="imageSource">The <see cref="Microsoft.Maui.Controls.ImageSource"/> value</param>
	/// <param name="overload">Unused overload discriminator.</param>
	/// <returns></returns>
	public static TImage Source<TImage>(this TImage bindable, ImageSource? imageSource, Image? overload = null) where TImage : Image
	{
		return SetSource(bindable, imageSource);
	}

	/// <inheritdoc />
	public static TImageButton Source<TImageButton>(this TImageButton bindable, ImageSource? imageSource, ImageButton? overload = null) where TImageButton : ImageButton
	{
		return SetSource(bindable, imageSource);
	}

	/// <summary>
	/// Sets the <see cref="Microsoft.Maui.IImage.Aspect" /> property.
	/// </summary>
	/// <typeparam name="TImage">The <see cref="Image"/> type.</typeparam>
	/// <param name="bindable">The <see cref="BindableObject"/> on which to set the <see cref="Microsoft.Maui.IImage.Aspect"/> property.</param>
	/// <param name="aspect">The <see cref="Microsoft.Maui.Aspect"/> value.</param>
	/// <param name="overload">Unused overload discriminator.</param>
	/// <returns></returns>
	public static TImage Aspect<TImage>(this TImage bindable, Aspect aspect, Image? overload = null) where TImage : Image
	{
		return SetAspect(bindable, aspect);
	}

	/// <inheritdoc />
	public static TImageButton Aspect<TImageButton>(this TImageButton bindable, Aspect aspect, ImageButton? overload = null) where TImageButton : ImageButton
	{
		return SetAspect(bindable, aspect);
	}

	/// <summary>
	/// Sets the <see cref="Microsoft.Maui.IImage.IsOpaque" /> property.
	/// </summary>
	/// <typeparam name="TImage">The <see cref="Image"/> type.</typeparam>
	/// <param name="bindable">The <see cref="BindableObject"/> on which to set the <see cref="Microsoft.Maui.IImage.IsOpaque"/> property.</param>
	/// <param name="isOpaque">The <see cref="bool"/> value.</param>
	/// <param name="overload">Unused overload discriminator.</param>
	/// <returns></returns>
	public static TImage IsOpaque<TImage>(this TImage bindable, bool isOpaque, Image? overload = null) where TImage : Image
	{
		return SetIsOpaque(bindable, isOpaque);
	}

	/// <inheritdoc />
	public static TImageButton IsOpaque<TImageButton>(this TImageButton bindable, bool isOpaque, ImageButton? overload = null) where TImageButton : ImageButton
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