using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
namespace CommunityToolkit.Maui.Markup.Benchmarks;

public class Program
{
	public static void Main(string[] args)
	{
		var config = DefaultConfig.Instance;

		BenchmarkRunner.Run<InitializeBindings>(config, args);
		BenchmarkRunner.Run<ExecuteBindings_ViewModelToView>(config, args);
		BenchmarkRunner.Run<ExecuteBindings_ViewToViewModel>(config, args);
	}
}