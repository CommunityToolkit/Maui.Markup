namespace CommunityToolkit.Maui.Markup.Benchmarks.Mocks;

using Microsoft.Maui.Animations;

class HandlersContextStub : IMauiContext
{
	public HandlersContextStub(IServiceProvider services)
	{
		Services = services;
		Handlers = Services.GetRequiredService<IMauiHandlersFactory>();
		AnimationManager = services.GetService<IAnimationManager>() ?? throw new NullReferenceException();
	}

	public IServiceProvider Services { get; }

	public IMauiHandlersFactory Handlers { get; }

	public IAnimationManager AnimationManager { get; }
}