using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace CommunityToolkit.Maui.Markup.SourceGenerators.Benchmarks;
public class Program
{
	public static void Main(string[] args)
	{
		var config = DefaultConfig.Instance;
		var summary = BenchmarkRunner.Run<TextAlignmentExtensionsGeneratorBenchmarks>(config, args);
	}
}