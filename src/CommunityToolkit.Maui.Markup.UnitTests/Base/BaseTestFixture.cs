using System;
using System.Globalization;
using CommunityToolkit.Maui.Markup.UnitTests.Mocks;
using Microsoft.Maui.Dispatching;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests.Base;

abstract class BaseTestFixture
{
	CultureInfo? defaultCulture;
	CultureInfo? defaultUICulture;

	[SetUp]
	public virtual void Setup()
	{
		defaultCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
		defaultUICulture = System.Threading.Thread.CurrentThread.CurrentUICulture;

		DispatcherProvider.SetCurrent(new MockDispatcherProvider());
	}

	[TearDown]
	public virtual void TearDown()
	{
		System.Threading.Thread.CurrentThread.CurrentCulture = defaultCulture ?? throw new NullReferenceException();
		System.Threading.Thread.CurrentThread.CurrentUICulture = defaultUICulture ?? throw new NullReferenceException();

		DispatcherProvider.SetCurrent(null);
	}
}