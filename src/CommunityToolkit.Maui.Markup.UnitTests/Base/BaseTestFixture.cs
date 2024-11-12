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

		InitializeMockApplication(out _);
	}

	[TearDown]
	public virtual void TearDown()
	{
		Thread.CurrentThread.CurrentCulture = defaultCulture ?? throw new NullReferenceException();
		Thread.CurrentThread.CurrentUICulture = defaultUICulture ?? throw new NullReferenceException();
		DispatcherProvider.SetCurrent(null);
	}

	protected static TElementHandler CreateElementHandler<TElementHandler>(IElement view, bool hasMauiContext = true)
		where TElementHandler : IElementHandler, new()
	{
		var mockElementHandler = new TElementHandler();
		mockElementHandler.SetVirtualView(view);
		if (hasMauiContext)
		{
			mockElementHandler.SetMauiContext(Application.Current?.Handler?.MauiContext ?? throw new NullReferenceException());
		}

		return mockElementHandler;
	}

	protected static TViewHandler CreateViewHandler<TViewHandler>(IView view, bool hasMauiContext = true)
		where TViewHandler : IViewHandler, new()
	{
		var mockViewHandler = new TViewHandler();
		mockViewHandler.SetVirtualView(view);
		if (hasMauiContext)
		{
			mockViewHandler.SetMauiContext(Application.Current?.Handler?.MauiContext ?? throw new NullReferenceException());
		}

		return mockViewHandler;
	}

	static void InitializeMockApplication(out IServiceProvider serviceProvider)
	{
		var appBuilder = MauiApp.CreateBuilder()
			.UseMauiCommunityToolkit()
			.UseMauiApp<MockApplication>();

		var mauiApp = appBuilder.Build();

		var application = (MockApplication)mauiApp.Services.GetRequiredService<IApplication>();
		application.AddWindow(new Window());
		serviceProvider = mauiApp.Services;

		IPlatformApplication.Current = application;

		application.Handler = new ApplicationHandlerStub();
		application.Handler.SetMauiContext(new HandlersContextStub(mauiApp.Services));
	}
}