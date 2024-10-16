using Microsoft.Maui.Handlers;
namespace CommunityToolkit.Maui.Markup.UnitTests.Mocks;

class MockShell : Shell
{
	public MockShell(List<ContentPage> pageList)
	{
		Handler = new ShellHandlerStub();
		
		foreach (var page in pageList)
		{
			Items.Add(page);
		}
	}
}

class ShellHandlerStub() : ViewHandler<Shell, object>(Mapper)
{
	public static IPropertyMapper<Shell, ShellHandlerStub> Mapper = new PropertyMapper<Shell, ShellHandlerStub>(ElementMapper);

	protected override object CreatePlatformView()
	{
		return new object();
	}
}