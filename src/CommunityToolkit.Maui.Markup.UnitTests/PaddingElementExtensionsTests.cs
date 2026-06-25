using CommunityToolkit.Maui.Markup.UnitTests.Base;
using NUnit.Framework;
namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class PaddingElementExtensionsTests : BaseMarkupTestFixture
{
	static readonly PaddingCase[] paddingCases =
	[
		new("Border", () => new Border(), Border.PaddingProperty, (bindable, padding) => ((Border)bindable).Padding(padding), (bindable, uniformSize) => ((Border)bindable).Padding(uniformSize), (bindable, horizontal, vertical) => ((Border)bindable).Padding(horizontal, vertical), (bindable, left, top, right, bottom) => ((Border)bindable).Paddings(left, top, right, bottom)),
		new("Button", () => new Button(), Button.PaddingProperty, (bindable, padding) => ((Button)bindable).Padding(padding), (bindable, uniformSize) => ((Button)bindable).Padding(uniformSize), (bindable, horizontal, vertical) => ((Button)bindable).Padding(horizontal, vertical), (bindable, left, top, right, bottom) => ((Button)bindable).Paddings(left, top, right, bottom)),
		new("ImageButton", () => new ImageButton(), ImageButton.PaddingProperty, (bindable, padding) => ((ImageButton)bindable).Padding(padding), (bindable, uniformSize) => ((ImageButton)bindable).Padding(uniformSize), (bindable, horizontal, vertical) => ((ImageButton)bindable).Padding(horizontal, vertical), (bindable, left, top, right, bottom) => ((ImageButton)bindable).Paddings(left, top, right, bottom)),
		new("Label", () => new Label(), Label.PaddingProperty, (bindable, padding) => ((Label)bindable).Padding(padding), (bindable, uniformSize) => ((Label)bindable).Padding(uniformSize), (bindable, horizontal, vertical) => ((Label)bindable).Padding(horizontal, vertical), (bindable, left, top, right, bottom) => ((Label)bindable).Paddings(left, top, right, bottom)),
		new("Page", () => new Page(), Page.PaddingProperty, (bindable, padding) => ((Page)bindable).Padding(padding), (bindable, uniformSize) => ((Page)bindable).Padding(uniformSize), (bindable, horizontal, vertical) => ((Page)bindable).Padding(horizontal, vertical), (bindable, left, top, right, bottom) => ((Page)bindable).Paddings(left, top, right, bottom)),
		new("ScrollView", () => new ScrollView(), ScrollView.PaddingProperty, (bindable, padding) => ((ScrollView)bindable).Padding(padding), (bindable, uniformSize) => ((ScrollView)bindable).Padding(uniformSize), (bindable, horizontal, vertical) => ((ScrollView)bindable).Padding(horizontal, vertical), (bindable, left, top, right, bottom) => ((ScrollView)bindable).Paddings(left, top, right, bottom))
	];

	[TestCaseSource(nameof(paddingCases))]
	public void PaddingThickness(PaddingCase paddingCase)
	{
		var bindable = paddingCase.Create();
		TestPropertiesSet(bindable, b => paddingCase.SetThickness(b, new Thickness(1)), (paddingCase.Property, new Thickness(0), new Thickness(1)));
	}

	[TestCaseSource(nameof(paddingCases))]
	public void PaddingUniform(PaddingCase paddingCase)
	{
		var bindable = paddingCase.Create();
		TestPropertiesSet(bindable, b => paddingCase.SetUniform(b, 1), (paddingCase.Property, new Thickness(0), new Thickness(1)));
	}

	[TestCaseSource(nameof(paddingCases))]
	public void PaddingHorizontalVertical(PaddingCase paddingCase)
	{
		var bindable = paddingCase.Create();
		TestPropertiesSet(bindable, b => paddingCase.SetHorizontalVertical(b, 1, 2), (paddingCase.Property, new Thickness(0), new Thickness(1, 2)));
	}

	[TestCaseSource(nameof(paddingCases))]
	public void Paddings(PaddingCase paddingCase)
	{
		var bindable = paddingCase.Create();
		TestPropertiesSet(bindable, b => paddingCase.SetPaddings(b, 1, 2, 3, 4), (paddingCase.Property, new Thickness(0), new Thickness(1, 2, 3, 4)));
	}

	[Test]
	public void SupportDerivedFrom()
	{
		DerivedFrom derivedFrom = new DerivedFrom()
			.Padding(1)
			.Padding(1, 2)
			.Paddings(left: 1, top: 2, right: 3, bottom: 4);

		DerivedFromScrollView derivedFromScrollView = new DerivedFromScrollView()
			.Padding(1)
			.Padding(1, 2)
			.Paddings(left: 1, top: 2, right: 3, bottom: 4);

		Assert.Multiple(() =>
		{
			Assert.That(derivedFrom, Is.InstanceOf<DerivedFrom>());
			Assert.That(derivedFromScrollView, Is.InstanceOf<DerivedFromScrollView>());
		});
	}

	public sealed record PaddingCase(
		string Name,
		Func<BindableObject> Create,
		BindableProperty Property,
		Action<BindableObject, Thickness> SetThickness,
		Action<BindableObject, double> SetUniform,
		Action<BindableObject, double, double> SetHorizontalVertical,
		Action<BindableObject, double, double, double, double> SetPaddings)
	{
		public override string ToString() => Name;
	}

	class DerivedFrom : ContentView { }

	class DerivedFromScrollView : ScrollView { }
}
