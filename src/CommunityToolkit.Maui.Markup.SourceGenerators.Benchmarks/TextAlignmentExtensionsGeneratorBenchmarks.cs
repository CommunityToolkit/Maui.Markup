using BenchmarkDotNet.Attributes;
using CommunityToolkit.Maui.Markup.SourceGenerators.UnitTests;

namespace CommunityToolkit.Maui.Markup.SourceGenerators.Benchmarks;

[MemoryDiagnoser]
public class TextAlignmentExtensionsGeneratorBenchmarks
{
	readonly TextAlignmentExtensionsGeneratorTests textAlignmentExtensionsGeneratorTests = new();

	[Benchmark]
	public Task VerifyGeneratedSource_WhenClassIsGeneric()
	{
		return textAlignmentExtensionsGeneratorTests.VerifyGeneratedSource_WhenClassIsGeneric();
	}

	[Benchmark]
	public Task VerifyNoErrorsWhenUseMauiCommunityToolkitHasAdditonalWhitespace()
	{
		return textAlignmentExtensionsGeneratorTests.VerifyGeneratedSource_WhenClassImplementsITextAlignmentInterface();
	}
}