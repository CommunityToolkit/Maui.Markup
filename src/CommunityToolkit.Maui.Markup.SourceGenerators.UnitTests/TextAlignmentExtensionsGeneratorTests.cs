using NUnit.Framework;
using static CommunityToolkit.Maui.Markup.SourceGenerators.UnitTests.CSharpSourceGeneratorVerifier<CommunityToolkit.Maui.Markup.SourceGenerators.TextAlignmentExtensionsGenerator>;

namespace CommunityToolkit.Maui.Markup.SourceGenerators.UnitTests;

public class TextAlignmentExtensionsGeneratorTests
{
	[Test]
	public async Task VerifyGeneratedSource_WhenClassImplementsITextAlignmentInterface()
	{
		// Arrange
		const string source = """

							  namespace MyNamespace
							  {
							      public class MyClass : Microsoft.Maui.ITextAlignment
							      {
							      }
							  }
							  """;

		// Act // Assert
		await VerifySourceGeneratorAsync(source, [typeof(TextAlignmentExtensionsGenerator)]);
	}

	[Test]
	public async Task VerifyGeneratedSource_WhenClassDoesNotImplementITextAlignmentInterface()
	{
		// Arrange
		const string source = """

							  namespace MyNamespace
							  {
							      public class MyClass
							      {
							      }
							  }
							  """;

		// Act // Assert
		await VerifySourceGeneratorAsync(source, [typeof(TextAlignmentExtensionsGenerator)]);
	}

	[Test]
	public async Task VerifyGeneratedSource_WhenClassIsGeneric()
	{
		// Arrange
		const string source = """

							  namespace MyNamespace
							  {
							      public class MyClass<T, U> : Microsoft.Maui.ITextAlignment
							          where T : IDisposable, new()
							          where U : class
							      {
							      }
							  }
							  """;

		// Act // Assert
		await VerifySourceGeneratorAsync(source, [typeof(TextAlignmentExtensionsGenerator)]);
	}
}