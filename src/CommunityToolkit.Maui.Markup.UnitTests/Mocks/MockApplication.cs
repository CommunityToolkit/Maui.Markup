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
class ApplicationHandlerStub() : ElementHandler<IApplication, object>(Mapper)
{
	public static IPropertyMapper<IApplication, ApplicationHandlerStub> Mapper = new PropertyMapper<IApplication, ApplicationHandlerStub>(ElementMapper);

	protected override object CreatePlatformElement()
	{
		return new object();
	}
}

class MockResourceDictionary : ResourceDictionary
{
	public MockResourceDictionary()
	{
		Add(new Style<Label>());
	}
}