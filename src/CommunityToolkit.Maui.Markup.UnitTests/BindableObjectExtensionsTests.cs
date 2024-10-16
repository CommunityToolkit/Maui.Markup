using System.Windows.Input;
using BindableObjectViews;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using NUnit.Framework;
namespace CommunityToolkit.Maui.Markup.UnitTests
{
	[TestFixture]
	class BindableObjectExtensionsTests : BaseMarkupTestFixture
	{
		ViewModel? viewModel;

		[SetUp]
		public override void Setup()
		{
			base.Setup();
			viewModel = new ViewModel();
		}

		[TearDown]
		public override void TearDown()
		{
			viewModel = null;
			base.TearDown();
		}

		[TestCase(AppTheme.Light)]
		[TestCase(AppTheme.Dark)]
		[TestCase(AppTheme.Unspecified)]
		public void AppThemeColorBindingCorrectlySetsPropertyToChangeBasedOnApplicationsAppTheme(AppTheme appTheme)
		{
			var expectedColor = appTheme == AppTheme.Dark ? Colors.Orange : Colors.Purple;

			ApplicationTestHelpers.PerformAppThemeBasedTest(
				appTheme,
				() => new Label().AppThemeColorBinding(Label.TextColorProperty, Colors.Purple, Colors.Orange),
				label => Assert.That(label.TextColor, Is.EqualTo(expectedColor)));
		}

		[TestCase(AppTheme.Light)]
		[TestCase(AppTheme.Dark)]
		[TestCase(AppTheme.Unspecified)]
		public void AppThemeBindingCorrectlySetsPropertyToChangeBasedOnApplicationsAppTheme(AppTheme appTheme)
		{
			const string dark = nameof(AppTheme.Dark);
			const string light = nameof(AppTheme.Light);

			var expectedText = appTheme == AppTheme.Dark ? dark : light;

			ApplicationTestHelpers.PerformAppThemeBasedTest(
				appTheme,
				() => new Label().AppThemeBinding(Label.TextProperty, light, dark),
				label => Assert.That(label.Text, Is.EqualTo(expectedText)));
		}

		sealed class ViewModel
		{
			public Guid Id { get; set; }

			public ICommand? Command { get; set; }

			public string? Text { get; set; }

			public Color TextColor { get; set; } = Colors.Transparent;

			public bool IsRed { get; set; }

			public double HeightRequest { get; set; }
		}
	}
}

namespace BindableObjectViews // This namespace simulates derived controls defined in a separate app, for use in the tests in this file only
{
	class DerivedFromLabel : Label
	{
	}

	class DerivedFromTextCell : TextCell
	{
	}
}