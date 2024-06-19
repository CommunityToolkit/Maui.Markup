using CommunityToolkit.Maui.Markup.Benchmarks.Mocks;

namespace CommunityToolkit.Maui.Markup.Benchmarks;

public abstract class BaseTest
{
	protected BaseTest()
	{
		CreateAndSetMockApplication(out var serviceProvider);
		ServiceProvider = serviceProvider;
	}

	protected IServiceProvider ServiceProvider { get; }

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

	static void CreateAndSetMockApplication(out IServiceProvider serviceProvider)
	{
		var appBuilder = MauiApp.CreateBuilder()
			.UseMauiApp<MockApplication>();

		appBuilder.Services.AddSingleton<IDispatcher>(_ => new MockDispatcherProvider().GetForCurrentThread());
		
		var mauiApp = appBuilder.Build();

		var application = mauiApp.Services.GetRequiredService<IApplication>();
		serviceProvider = mauiApp.Services;

		IPlatformApplication.Current = (IPlatformApplication)application;

		application.Handler = new ApplicationHandlerStub();
		application.Handler.SetMauiContext(new HandlersContextStub(mauiApp.Services));
	}
}