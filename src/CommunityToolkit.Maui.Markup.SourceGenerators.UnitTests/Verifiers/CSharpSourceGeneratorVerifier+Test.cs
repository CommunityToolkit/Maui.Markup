using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Testing;

namespace CommunityToolkit.Maui.Markup.SourceGenerators.UnitTests;

public static partial class CSharpSourceGeneratorVerifier<TSourceGenerator>
	where TSourceGenerator : IIncrementalGenerator, new()
{
	class Test : CSharpSourceGeneratorTest<TSourceGenerator, Microsoft.CodeAnalysis.Testing.DefaultVerifier>
	{
		public Test(params Type[] assembliesUnderTest)
		{
#if NET8_0
			ReferenceAssemblies = Microsoft.CodeAnalysis.Testing.ReferenceAssemblies.Net.Net80;
#else
#error ReferenceAssemblies must be updated to current version of .NET
#endif
			List<Type> typesForAssembliesUnderTest =
			[
				typeof(Microsoft.Maui.Controls.Xaml.Extensions), // Microsoft.Maui.Controls.Xaml
				typeof(MauiApp),// Microsoft.Maui.Hosting
				typeof(Application), // Microsoft.Maui.Controls
			];
			typesForAssembliesUnderTest.AddRange(assembliesUnderTest);

			foreach (var type in typesForAssembliesUnderTest)
			{
				TestState.AdditionalReferences.Add(MetadataReference.CreateFromFile(type.Assembly.Location));
			}

			SolutionTransforms.Add((solution, projectId) =>
			{
				ArgumentNullException.ThrowIfNull(solution);

				if (solution.GetProject(projectId) is not Project project)
				{
					throw new ArgumentException("Invalid ProjectId");
				}

				var compilationOptions = project.CompilationOptions ?? throw new InvalidOperationException($"{nameof(project.CompilationOptions)} cannot be null");
				compilationOptions = compilationOptions.WithSpecificDiagnosticOptions(
					compilationOptions.SpecificDiagnosticOptions.SetItems(CSharpVerifierHelper.NullableWarnings));
				solution = solution.WithProjectCompilationOptions(projectId, compilationOptions);

				return solution;
			});
		}
		
		protected override CompilationOptions CreateCompilationOptions()
		{
			var compilationOptions = base.CreateCompilationOptions();
			return compilationOptions.WithSpecificDiagnosticOptions(
				compilationOptions.SpecificDiagnosticOptions.SetItems(GetNullableWarningsFromCompiler()));
		}

		public LanguageVersion LanguageVersion { get; } = LanguageVersion.Default;

		static ImmutableDictionary<string, ReportDiagnostic> GetNullableWarningsFromCompiler()
		{
			string[] args = { "/warnaserror:nullable" };
			var commandLineArguments = CSharpCommandLineParser.Default.Parse(args, baseDirectory: Environment.CurrentDirectory, sdkDirectory: Environment.CurrentDirectory);
			var nullableWarnings = commandLineArguments.CompilationOptions.SpecificDiagnosticOptions;

			return nullableWarnings;
		}

		protected override ParseOptions CreateParseOptions()
		{
			return ((CSharpParseOptions)base.CreateParseOptions()).WithLanguageVersion(LanguageVersion);
		}
	}
}