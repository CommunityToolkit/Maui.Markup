namespace CommunityToolkit.Maui.Markup.UnitTests.Mocks;

// Inspired by https://github.com/dotnet/maui/blob/main/src/Core/tests/UnitTests/TestClasses/DispatcherStub.cs
sealed class MockDispatcherProvider : IDispatcherProvider, IDisposable
{
	static readonly DispatcherMock dispatcherMock = new();

	readonly ThreadLocal<IDispatcher> dispatcherInstance = new(() => dispatcherMock);

	public IDispatcher GetForCurrentThread() => dispatcherInstance.Value ?? throw new InvalidOperationException();

	void IDisposable.Dispose() => dispatcherInstance.Dispose();

	sealed class DispatcherMock : IDispatcher
	{
		public bool IsDispatchRequired => false;

		public int ManagedThreadId { get; } = Environment.CurrentManagedThreadId;

		public IDispatcherTimer CreateTimer() => throw new NotImplementedException();

		public bool DispatchDelayed(TimeSpan delay, Action action) => throw new NotImplementedException();

		public bool Dispatch(Action action)
		{
			action();

			return true;
		}

	}
}