using Microsoft.Maui.Handlers;
namespace CommunityToolkit.Maui.Markup.UnitTests.Mocks;

class MockApplication : Application, IPlatformApplication
{
	public MockApplication(IServiceProvider serviceProvider)
	{
		Resources = new MockResourceDictionary();

		Services = serviceProvider;
	}

	public IApplication Application => this;
	public IServiceProvider Services { get; }
}

// Inspired by https://github.com/dotnet/maui/blob/main/src/Controls/tests/Core.UnitTests/TestClasses/ApplicationHandlerStub.cs
class ApplicationHandlerStub() : ElementHandler<IApplication, object>(Mapper, CommandMapper)
{
	public static IPropertyMapper<IApplication, ApplicationHandlerStub> Mapper = new PropertyMapper<IApplication, ApplicationHandlerStub>(ElementMapper);

	public static CommandMapper<IApplication, ApplicationHandlerStub> CommandMapper = new(ElementCommandMapper)
	{
		[nameof(IApplication.OpenWindow)] = MapOpenWindow
	};

	protected override object CreatePlatformElement()
	{
		return new object();
	}

	// Application.OpenWindow only queues the Window and asks the ApplicationHandler to create it.
	// Like the platform ApplicationHandlers, call IApplication.CreateWindow, which adds the Window to Application.Windows.
	static void MapOpenWindow(ApplicationHandlerStub handler, IApplication application, object? args)
	{
		var mauiContext = handler.MauiContext ?? throw new InvalidOperationException($"{nameof(handler.MauiContext)} must be set before opening a {nameof(Window)}");

		var activationState = args is OpenWindowRequest { State: not null } request
			? new ActivationState(mauiContext, request.State)
			: new ActivationState(mauiContext);

		application.CreateWindow(activationState);
	}
}

class MockResourceDictionary : ResourceDictionary
{
	public MockResourceDictionary()
	{
		Add(new Style<Label>());
	}
}