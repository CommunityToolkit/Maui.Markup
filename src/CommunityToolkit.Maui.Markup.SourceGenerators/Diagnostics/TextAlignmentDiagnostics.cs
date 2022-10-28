using Microsoft.CodeAnalysis;

namespace CommunityToolkit.Maui.Markup.SourceGenerators;

class TextAlignmentDiagnostics
{
	const string category = "TextAlignmentExtensions";

	public static readonly DiagnosticDescriptor GlobalNamespace = new(
		   "MMCT001",
		   "Global namespace is not support for this Source Generator",
		   "Please put '{0}' inside a valid namespace",
		   category,
		   DiagnosticSeverity.Warning,
		   true);

	public static readonly DiagnosticDescriptor MauiReferenceIsMissing = new(
		   "MMCT002",
		   "Unable to find Microsoft.Maui.ITextAlignment",
		   "Please make sure that your project is referencing Microsoft.Maui",
		   category,
		   DiagnosticSeverity.Error,
		   true);

	public static readonly DiagnosticDescriptor InvalidClassDeclarationSyntax = new(
		   "MMCT003",
		   "Unable to get information from the Class",
		   "Please make sure that the code inside '{0}' has not error, the TextColorTo methods will not be generated for this file",
		   category,
		   DiagnosticSeverity.Info,
		   true);

	public static readonly DiagnosticDescriptor InvalidModifierAccess = new(
		   "MMCT004",
		   "Class marked with invalid modifier access",
		   "TextColorTo only supports public and internal classes inheriting from ITextStyle, please fix '{0}'",
		   category,
		   DiagnosticSeverity.Info,
		   true);

}