using CommunityToolkit.Maui.Markup.UnitTests.Base;
using CommunityToolkit.Maui.Markup.UnitTests.Mocks;
using NUnit.Framework;
namespace CommunityToolkit.Maui.Markup.UnitTests;

class DynamicResourceHandlerTests : BaseTestFixture
{
	[Test]
	public void DynamicResource()
	{
		var label = new Label { Resources = new ResourceDictionary { { "TextKey", "TextValue" } } };
		Assert.That(label.Text, Is.EqualTo(Label.TextProperty.DefaultValue));

		label.DynamicResource(Label.TextProperty, "TextKey");
		Assert.That(label.Text, Is.EqualTo("TextValue"));
	}

	[Test]
	public void DynamicResources() => AssertDynamicResources();

	static Label AssertDynamicResources()
	{
		var label = new Label { Resources = new ResourceDictionary { { "TextKey", "TextValue" }, { "ColorKey", Colors.Green } } };
		
		ArgumentNullException.ThrowIfNull(Application.Current);
		
		Application.Current.ActivateWindow(new Window(new MockShell([
			new ContentPage
			{
				Content = label
			}
		])));

		Assert.Multiple(() =>
		{
			Assert.That(label.Text, Is.EqualTo(Label.TextProperty.DefaultValue));
			Assert.That(label.TextColor, Is.EqualTo(Label.TextColorProperty.DefaultValue));
		});

		label.DynamicResources((Label.TextProperty, "TextKey"),
							   (Label.TextColorProperty, "ColorKey"));

		Assert.Multiple(() =>
		{
			Assert.That(label.Text, Is.EqualTo("TextValue"));
			Assert.That(label.TextColor, Is.EqualTo(Colors.Green));
		});

		return label;
	}
}