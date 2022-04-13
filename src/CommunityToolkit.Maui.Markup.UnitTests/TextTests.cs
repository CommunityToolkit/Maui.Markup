using System;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests;

class LabelTextTests : BaseMarkupTestFixture<Label>
{
	[Test]
	public void LabelTextColor_ProvidedColor()
		=> TestPropertiesSet(l => l.TextColor(Colors.Red), (TextElement.TextColorProperty, Colors.Red));

	[Test]
	public void LabelTextColor_CustomColor()
		=> TestPropertiesSet(l => l.TextColor(new Color(0.124f, 0.654f, 0.9234f, 0.100f)), (TextElement.TextColorProperty, new Color(0.124f, 0.654f, 0.9234f, 0.100f)));

	[Test]
	public void LabelText_NoColor()
		=> TestPropertiesSet(l => l.Text("Hello World"), (Label.TextProperty, "Hello World"));

	[Test]
	public void LabelText_ProvidedColor()
		=> TestPropertiesSet(l => l.Text("Hello World", Colors.Green), (Label.TextProperty, "Hello World"), (TextElement.TextColorProperty, Colors.Green));

	[Test]
	public void LabelText_CustomColor()
		=> TestPropertiesSet(l => l.Text("Hello World", new Color(250, 5, 128, 1)), (Label.TextProperty, "Hello World"), (TextElement.TextColorProperty, new Color(250, 5, 128, 1)));

	[Test]
	public void SupportDerivedFromLabel()
	{
		Assert.IsInstanceOf<DerivedFromLabel>(
			new DerivedFromLabel()
			.Effects(new NullEffect())
			.FontSize(8)
			.Bold()
			.Italic()
			.Text("Hello World", new Color(255, 255, 128, 1))
			.Font("AFontName", 8, true, true));
	}

	class DerivedFromLabel : Label
	{
	}
}

class ButtonTextTests : BaseMarkupTestFixture<Button>
{
	[Test]
	public void ButtonTextColor_ProvidedColor()
		=> TestPropertiesSet(b => b.TextColor(Colors.Red), (TextElement.TextColorProperty, Colors.Red));

	[Test]
	public void ButtonTextColor_CustomColor()
		=> TestPropertiesSet(b => b.TextColor(new Color(0.124f, 0.654f, 0.9234f, 0.100f)), (TextElement.TextColorProperty, new Color(0.124f, 0.654f, 0.9234f, 0.100f)));

	[Test]
	public void ButtonText_NoColor()
		=> TestPropertiesSet(b => b.Text("Hello World"), (Button.TextProperty, "Hello World"));

	[Test]
	public void ButtonText_ProvidedColor()
		=> TestPropertiesSet(b => b.Text("Hello World", Colors.Green), (Button.TextProperty, "Hello World"), (TextElement.TextColorProperty, Colors.Green));

	[Test]
	public void ButtonText_CustomColor()
		=> TestPropertiesSet(b => b.Text("Hello World", new Color(250, 5, 128, 1)), (Button.TextProperty, "Hello World"), (TextElement.TextColorProperty, new Color(250, 5, 128, 1)));

	[Test]
	public void SupportDerivedFromButton()
	{
		Assert.IsInstanceOf<DerivedFromButton>(
			new DerivedFromButton()
			.Effects(new NullEffect())
			.FontSize(8)
			.Bold()
			.Italic()
			.Text("Hello World", new Color(255, 255, 128, 1))
			.Font("AFontName", 8, true, true));
	}

	class DerivedFromButton : Button
	{
	}
}

class MenuItemTextTests : BaseMarkupTestFixture<MenuItem>
{
	[Test]
	public void MenuItemTextColor_ProvidedColor()
		=> Assert.Throws<NotSupportedException>(() => Bindable.TextColor(Colors.Teal));

	[Test]
	public void ButtonText_NoColor()
		=> TestPropertiesSet(b => b.Text("Hello World"), (MenuItem.TextProperty, "Hello World"));

	[Test]
	public void ButtonText_ProvidedColor()
		=> Assert.Throws<NotSupportedException>(() => Bindable.Text("Hello World", Colors.Green));

	[Test]
	public void SupportDerivedFromButton()
	{
		Assert.IsInstanceOf<DerivedFromMenuItem>(
			new DerivedFromMenuItem()
			.Effects(new NullEffect())
			.Text("Hello World"));
	}

	class DerivedFromMenuItem : MenuItem
	{
	}
}

class EditorTextTests : BaseMarkupTestFixture<Editor>
{
	[Test]
	public void EditorTextColor_ProvidedColor()
		=> TestPropertiesSet(b => b.TextColor(Colors.Red), (TextElement.TextColorProperty, Colors.Red));

	[Test]
	public void EditorTextColor_CustomColor()
		=> TestPropertiesSet(b => b.TextColor(new Color(0.124f, 0.654f, 0.9234f, 0.100f)), (TextElement.TextColorProperty, new Color(0.124f, 0.654f, 0.9234f, 0.100f)));

	[Test]
	public void EditorText_NoColor()
		=> TestPropertiesSet(b => b.Text("Hello World"), (Editor.TextProperty, "Hello World"));

	[Test]
	public void EditorText_ProvidedColor()
		=> TestPropertiesSet(b => b.Text("Hello World", Colors.Green), (Editor.TextProperty, "Hello World"), (TextElement.TextColorProperty, Colors.Green));

	[Test]
	public void EditorText_CustomColor()
		=> TestPropertiesSet(b => b.Text("Hello World", new Color(250, 5, 128, 1)), (Editor.TextProperty, "Hello World"), (TextElement.TextColorProperty, new Color(250, 5, 128, 1)));

	[Test]
	public void SupportDerivedFromEnditor()
	{
		Assert.IsInstanceOf<DerivedFromEditor>(
			new DerivedFromEditor()
			.Effects(new NullEffect())
			.FontSize(8)
			.Bold()
			.Italic()
			.Text("Hello World", new Color(255, 255, 128, 1))
			.Font("AFontName", 8, true, true));
	}

	class DerivedFromEditor : Editor
	{
	}
}

class EntryTextTests : BaseMarkupTestFixture<Entry>
{
	[Test]
	public void EntryTextColor_ProvidedColor()
		=> TestPropertiesSet(b => b.TextColor(Colors.Red), (TextElement.TextColorProperty, Colors.Red));

	[Test]
	public void EntryTextColor_CustomColor()
		=> TestPropertiesSet(b => b.TextColor(new Color(0.124f, 0.654f, 0.9234f, 0.100f)), (TextElement.TextColorProperty, new Color(0.124f, 0.654f, 0.9234f, 0.100f)));

	[Test]
	public void EntryText_NoColor()
		=> TestPropertiesSet(b => b.Text("Hello World"), (Entry.TextProperty, "Hello World"));

	[Test]
	public void EntryText_ProvidedColor()
		=> TestPropertiesSet(b => b.Text("Hello World", Colors.Green), (Entry.TextProperty, "Hello World"), (TextElement.TextColorProperty, Colors.Green));

	[Test]
	public void EntryText_CustomColor()
		=> TestPropertiesSet(b => b.Text("Hello World", new Color(250, 5, 128, 1)), (Entry.TextProperty, "Hello World"), (TextElement.TextColorProperty, new Color(250, 5, 128, 1)));

	[Test]
	public void SupportDerivedFromEntry()
	{
		Assert.IsInstanceOf<DerivedFromEntry>(
			new DerivedFromEntry()
			.Effects(new NullEffect())
			.FontSize(8)
			.Bold()
			.Italic()
			.Text("Hello World", new Color(255, 255, 128, 1))
			.Font("AFontName", 8, true, true));
	}

	class DerivedFromEntry : Entry
	{
	}
}

class SearchBarTextTests : BaseMarkupTestFixture<SearchBar>
{
	[Test]
	public void SearchBarTextColor_ProvidedColor()
		=> TestPropertiesSet(b => b.TextColor(Colors.Red), (TextElement.TextColorProperty, Colors.Red));

	[Test]
	public void SearchBarTextColor_CustomColor()
		=> TestPropertiesSet(b => b.TextColor(new Color(0.124f, 0.654f, 0.9234f, 0.100f)), (TextElement.TextColorProperty, new Color(0.124f, 0.654f, 0.9234f, 0.100f)));

	[Test]
	public void SearchBarText_NoColor()
		=> TestPropertiesSet(b => b.Text("Hello World"), (SearchBar.TextProperty, "Hello World"));

	[Test]
	public void SearchBarText_ProvidedColor()
		=> TestPropertiesSet(b => b.Text("Hello World", Colors.Green), (SearchBar.TextProperty, "Hello World"), (TextElement.TextColorProperty, Colors.Green));

	[Test]
	public void SearchBarText_CustomColor()
		=> TestPropertiesSet(b => b.Text("Hello World", new Color(250, 5, 128, 1)), (SearchBar.TextProperty, "Hello World"), (TextElement.TextColorProperty, new Color(250, 5, 128, 1)));

	[Test]
	public void SupportDerivedFromSearchBar()
	{
		Assert.IsInstanceOf<DerivedFromSearchBar>(
			new DerivedFromSearchBar()
			.Effects(new NullEffect())
			.FontSize(8)
			.Bold()
			.Italic()
			.Text("Hello World", new Color(255, 255, 128, 1))
			.Font("AFontName", 8, true, true));
	}

	class DerivedFromSearchBar : SearchBar
	{
	}
}
