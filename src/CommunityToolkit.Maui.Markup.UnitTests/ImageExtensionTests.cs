using CommunityToolkit.Maui.Markup.UnitTests.Base;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests;

class ImageExtensionTests : BaseMarkupTestFixture<Image>
{
	const string resourceToLoad = "CommunityToolkit.Maui.UnitTests.Resources.dotnet-bot.png";

	[Test]
	public void SetSourceTest()
	{
		var image = new Image();

		Assert.Null(image.Source);
		Assert.IsNotInstanceOf<FileImageSource>(image.Source);

		image.Source(resourceToLoad);

		Assert.IsInstanceOf<FileImageSource>(image.Source);
		Assert.False(image.Source.IsEmpty);
	}

	[Test]
	public void SetAspectTest()
	{
		TestPropertiesSet(i => i.Aspect(Aspect.AspectFill), (ImageElement.AspectProperty, Aspect.AspectFill));
		TestPropertiesSet(i => i.Aspect(Aspect.Center), (ImageElement.AspectProperty, Aspect.Center));
		TestPropertiesSet(i => i.Aspect(Aspect.Fill), (ImageElement.AspectProperty, Aspect.Fill));
	}

	[Test]
	public void SetIsOpaqueTest()
		=> TestPropertiesSet(i => i.IsOpaque(true), (ImageElement.IsOpaqueProperty, true));

	[Test]
	public void SupportDerivedFromLabel()
	{
		Assert.IsInstanceOf<DerivedFromImage>(
			new DerivedFromImage()
			.Source(resourceToLoad)
			.Aspect(Aspect.Center)
			.IsOpaque(true));
	}

	class DerivedFromImage : Image
	{

	}
}
