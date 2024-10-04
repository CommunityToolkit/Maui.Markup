using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;

namespace CommunityToolkit.Maui.Markup.SourceGenerators.UnitTests;

public static partial class CSharpSourceGeneratorVerifier<TSourceGenerator>
	where TSourceGenerator : IIncrementalGenerator, new()
{
	/// <inheritdoc cref="AnalyzerVerifier{TAnalyzer, TTest, TVerifier}.VerifyAnalyzerAsync(string, DiagnosticResult[])"/>
	public static async Task VerifySourceGeneratorAsync(string source, Type[] assembliesUnderTest, params DiagnosticResult[] expected)
	{
		var test = new Test(assembliesUnderTest)
		{
			TestCode = source,
		};

		test.ExpectedDiagnostics.AddRange(expected);
		await test.RunAsync(CancellationToken.None);
	}
}