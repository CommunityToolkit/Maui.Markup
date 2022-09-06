using Microsoft.CodeAnalysis;

namespace CommunityToolkit.Maui.Markup.SourceGenerators;

class TextAlignmentDiagnostics
{
	const string category = "TextColorTo";

	public static readonly DiagnosticDescriptor GlobalNamespace = new(
		   "MMCT001",
		   "Global namespace is not support for this Source Generator",
		   "Please put '{0}' inside a valid namespace",
		   category,
		   DiagnosticSeverity.Warning,
		   true);

	public static readonly DiagnosticDescriptor MauiReferenceIsMissing = new(
		   "MMCT002",
		   "Was not possible to find Microsoft.Maui.ITextStyle and or Microsoft.Maui.IAnimatable",
		   "Please make sure that your project is referencing Microsoft.Maui",
		   category,
		   DiagnosticSeverity.Error,
		   true);

}
