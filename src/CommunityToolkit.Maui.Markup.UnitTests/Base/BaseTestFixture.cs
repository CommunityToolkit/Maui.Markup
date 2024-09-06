using System.Globalization;
using CommunityToolkit.Maui.Markup.UnitTests.Mocks;
using NUnit.Framework;
namespace CommunityToolkit.Maui.Markup.UnitTests.Base;

abstract class BaseTestFixture
{
	CultureInfo? defaultCulture;
	CultureInfo? defaultUICulture;

	[SetUp]
	public virtual void Setup()
	{
		defaultCulture = Thread.CurrentThread.CurrentCulture;
		defaultUICulture = Thread.CurrentThread.CurrentUICulture;

		DispatcherProvider.SetCurrent(new MockDispatcherProvider());
	}

	[TearDown]
	public virtual void TearDown()
	{
		Thread.CurrentThread.CurrentCulture = defaultCulture ?? throw new NullReferenceException();
		Thread.CurrentThread.CurrentUICulture = defaultUICulture ?? throw new NullReferenceException();

		DispatcherProvider.SetCurrent(null);
	}
}