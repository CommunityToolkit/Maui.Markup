using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;

namespace CommunityToolkit.Maui.Markup.SourceGenerators.UnitTests;

public static partial class CSharpSourceGeneratorVerifier<TSourceGenerator>
	where TSourceGenerator : IIncrementalGenerator, new()
{
	/// <inheritdoc cref="AnalyzerVerifier{TAnalyzer, TTest, TVerifier}.VerifyAnalyzerAsync(string, DiagnosticResult[])"/>
	public static async Task VerifySourceGeneratorAsync(string source, string expectedGeneratedCode, Type[] assembliesUnderTest, params DiagnosticResult[] expectedDiagnosticResults)
	{
		var test = new Test(assembliesUnderTest)
		{
			TestBehaviors = TestBehaviors.SkipGeneratedSourcesCheck,
			TestState =
			{
				Sources = { source },
				GeneratedSources =
				{
					(typeof(TSourceGenerator), string.Empty, SourceText.From(expectedGeneratedCode, Encoding.UTF8, SourceHashAlgorithm.Sha256)),
				}
			}
		};

		test.ExpectedDiagnostics.AddRange(expectedDiagnosticResults);

		await test.RunAsync(CancellationToken.None).ConfigureAwait(false);
	}
}