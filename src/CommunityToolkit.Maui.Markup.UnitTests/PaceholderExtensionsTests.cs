using CommunityToolkit.Maui.Markup.UnitTests.Base;
using NUnit.Framework;
namespace CommunityToolkit.Maui.Markup.UnitTests;

class PaceholderExtensionsTests : BaseMarkupTestFixture<Entry>
{
	[Test]
	public void SetPlaceholderTest()
		=> TestPropertiesSet(e => e.Placeholder("Hello World"), (Entry.PlaceholderProperty, "Hello World"));

	[Test]
	public void SetPlaceholderAndColorTest()
		=> TestPropertiesSet(e => e.Placeholder("Hello World", Colors.Red), (Entry.PlaceholderProperty, "Hello World"), (Entry.PlaceholderColorProperty, Colors.Red));

	[Test]
	public void SetPlaceholderColorTest()
		=> TestPropertiesSet(e => e.PlaceholderColor(Colors.Red), (Entry.PlaceholderColorProperty, Colors.Red));

	[Test]
	public void SupportsSearchBar()
	{
		var searchBar = new SearchBar()
			.Placeholder("Hello World")
			.PlaceholderColor(Colors.Blue)
			.Placeholder("Hello World 2", Colors.Red);

		Assert.Multiple(() =>
		{
			Assert.That(searchBar.Placeholder, Is.EqualTo("Hello World 2"));
			Assert.That(searchBar.PlaceholderColor, Is.EqualTo(Colors.Red));
		});
	}

	[Test]
	public void SupportDerivedFromEditor()
	{
		Assert.That(new DerivedFromEditor()
					.Placeholder("Hello World")
					.PlaceholderColor(Colors.Blue)
					.Placeholder("Hello World 2", Colors.Red),
					Is.InstanceOf<DerivedFromEditor>());
	}

	class DerivedFromEditor : Editor
	{

	}
}