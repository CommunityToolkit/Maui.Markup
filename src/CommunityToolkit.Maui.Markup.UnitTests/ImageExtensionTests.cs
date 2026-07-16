using CommunityToolkit.Maui.Markup.UnitTests.Base;
using NUnit.Framework;
namespace CommunityToolkit.Maui.Markup.UnitTests;

class ImageExtensionTests : BaseMarkupTestFixture<Image>
{
	const string resourceToLoad = "CommunityToolkit.Maui.UnitTests.Resources.dotnet-bot.png";

	[Test]
	public void SetSourceTest()
	{
		var image = new Image();

		Assert.That(image.Source, Is.Null);
		Assert.That(image.Source, Is.Not.InstanceOf<FileImageSource>());

		image.Source(resourceToLoad);

		Assert.That(image.Source, Is.InstanceOf<FileImageSource>());
		Assert.That(image.Source.IsEmpty, Is.False);
	}

	[Test]
	public void SetAspectTest()
	{
		TestPropertiesSet(i => i.Aspect(Aspect.AspectFill), (Image.AspectProperty, Aspect.AspectFill));
		TestPropertiesSet(i => i.Aspect(Aspect.Center), (Image.AspectProperty, Aspect.Center));
		TestPropertiesSet(i => i.Aspect(Aspect.Fill), (Image.AspectProperty, Aspect.Fill));
	}

	[Test]
	public void SetIsOpaqueTest()
		=> TestPropertiesSet(i => i.IsOpaque(true), (Image.IsOpaqueProperty, true));

	[Test]
	public void SupportsImageButton()
	{
		var imageButton = new ImageButton().Source(resourceToLoad);

		Assert.Multiple(() =>
		{
			Assert.That(imageButton.Source, Is.InstanceOf<FileImageSource>());
			Assert.That(((FileImageSource)imageButton.Source).File, Is.EqualTo(resourceToLoad));
		});

		TestPropertiesSet(new ImageButton(), imageButton => imageButton.Aspect(Aspect.Center), (ImageButton.AspectProperty, Aspect.Center));
		TestPropertiesSet(new ImageButton(), imageButton => imageButton.IsOpaque(true), (ImageButton.IsOpaqueProperty, true));
	}

	[Test]
	public void SupportDerivedFromImage()
	{
		Assert.That(new DerivedFromImage()
					.Source(resourceToLoad)
					.Aspect(Aspect.Center)
					.IsOpaque(true),
			Is.InstanceOf<DerivedFromImage>());
	}

	class DerivedFromImage : Image
	{

	}
}