using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace CommunityToolkit.Maui.Markup.SourceGenerators;

[Generator(LanguageNames.CSharp)]
class TextAlignmentExtensionsGenerator : IIncrementalGenerator
{
	const string iTextAlignmentInterface = "Microsoft.Maui.ITextAlignment";
	const string mauiControlsAssembly = "Microsoft.Maui.Controls";

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		// Get All Classes in User Library
		var userGeneratedClassesProvider = context.SyntaxProvider.CreateSyntaxProvider(
			static (syntaxNode, cancellationToken) => syntaxNode is ClassDeclarationSyntax { BaseList: not null },
			static (context, cancellationToken) => (ClassDeclarationSyntax)context.Node);

		// Get Microsoft.Maui.Controls Assembly Symbol
		var mauiControlsAssemblySymbolProvider = context.CompilationProvider.Select(
			static (compilation, token) => compilation.SourceModule.ReferencedAssemblySymbols.Single(q => q.Name == mauiControlsAssembly));

		var inputs = userGeneratedClassesProvider.Collect()
						.Combine(mauiControlsAssemblySymbolProvider)
						.Select(static (combined, cancellationToken) => (UserGeneratedClassesProvider: combined.Left, MauiControlsAssemblySymbolProvider: combined.Right))
						.Combine(context.CompilationProvider)
						.Select(static (combined, cancellationToken) => (combined.Left.UserGeneratedClassesProvider, combined.Left.MauiControlsAssemblySymbolProvider, Compilation: combined.Right));

		context.RegisterSourceOutput(inputs, (context, collectedValues) =>
		Execute(context, collectedValues.Compilation, collectedValues.UserGeneratedClassesProvider, collectedValues.MauiControlsAssemblySymbolProvider));
	}

	static void Execute(SourceProductionContext context, Compilation compilation, ImmutableArray<ClassDeclarationSyntax> userGeneratedClassesProvider, IAssemblySymbol mauiControlsAssemblySymbolProvider)
	{
		var textAlignmentSymbol = compilation.GetTypeByMetadataName(iTextAlignmentInterface);

		if (textAlignmentSymbol is null)
		{
			var diag = Diagnostic.Create(TextAlignmentDiagnostics.MauiReferenceIsMissing, Location.None);
			context.ReportDiagnostic(diag);
			return;
		}

		var textAlignmentClassList = new List<(string ClassName, string ClassAcessModifier, string Namespace, string GenericArguments, string GenericConstraints)>();

		// Collect Microsoft.Maui.Controls that Implement ITextAlignment
		var mauiTextAlignmentImplementors = mauiControlsAssemblySymbolProvider.GlobalNamespace.GetNamedTypeSymbols().Where(x => x.AllInterfaces.Contains(textAlignmentSymbol, SymbolEqualityComparer.Default));

		foreach (var namedTypeSymbol in mauiTextAlignmentImplementors)
		{
			textAlignmentClassList.Add((namedTypeSymbol.Name, "public", namedTypeSymbol.ContainingNamespace.ToDisplayString(), namedTypeSymbol.TypeArguments.GetGenericTypeArgumentsString(), namedTypeSymbol.GetGenericTypeConstraintsAsString()));
		}

		// Collect All Classes in User Library that Implement ITextAlignment
		foreach (var classDeclarationSyntax in userGeneratedClassesProvider)
		{
			var declarationSymbol = compilation.GetSymbol<INamedTypeSymbol>(classDeclarationSyntax);
			if (declarationSymbol is null)
			{
				var diag = Diagnostic.Create(TextAlignmentDiagnostics.InvalidClassDeclarationSyntax, Location.None, classDeclarationSyntax.Identifier.Text);
				context.ReportDiagnostic(diag);
				continue;
			}

			// If the control inherits from a Maui control that implements ITextAlignment, we don't need to generate a extension method for it.
			// We just generate a method if the Control is a new implementation of ITextAlignment and IAnimatable
			var doesContainSymbolBaseType = mauiTextAlignmentImplementors.ContainsSymbolBaseType(declarationSymbol);

			if (!doesContainSymbolBaseType
				&& declarationSymbol.AllInterfaces.Contains(textAlignmentSymbol, SymbolEqualityComparer.Default))
			{
				if (declarationSymbol.ContainingNamespace.IsGlobalNamespace)
				{
					var diag = Diagnostic.Create(TextAlignmentDiagnostics.GlobalNamespace, Location.None, declarationSymbol.Name);
					context.ReportDiagnostic(diag);
					continue;
				}

				var nameSpace = declarationSymbol.ContainingNamespace.ToDisplayString();

				var accessModifier = GetClassAccessModifier(declarationSymbol);

				if (accessModifier == string.Empty)
				{
					var diag = Diagnostic.Create(TextAlignmentDiagnostics.InvalidModifierAccess, Location.None, declarationSymbol.Name);
					context.ReportDiagnostic(diag);
					continue;
				}

				textAlignmentClassList.Add((declarationSymbol.Name, accessModifier, nameSpace, declarationSymbol.TypeArguments.GetGenericTypeArgumentsString(), declarationSymbol.GetGenericTypeConstraintsAsString()));
			}
		}

		var options = ((CSharpCompilation)compilation).SyntaxTrees[0].Options as CSharpParseOptions;
		foreach (var textAlignmentClass in textAlignmentClassList)
		{
			var textColorToBuilder = @"
// <auto-generated>
// See: CommunityToolkit.Maui.Markup.SourceGenerators.TextAlignmentGenerator
#nullable enable

using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace CommunityToolkit.Maui.Markup
{
	/// <summary>
	/// Extension Methods for <see cref=""ITextAlignment""/>
	/// </summary>
	" + textAlignmentClass.ClassAcessModifier + " static partial class TextAlignmentExtensions_" + textAlignmentClass.ClassName + @"
	{
		/// <summary>
		/// <see cref=""ITextAlignment.HorizontalTextAlignment""/> = <see cref=""TextAlignment.Start""/>
		/// </summary>
		/// <param name=""textAlignmentControl""></param>
		/// <returns>" + textAlignmentClass.Namespace + "." + textAlignmentClass.ClassName + @" with added <see cref=""TextAlignment.Start""/></returns>
		public static TAssignable TextStart<TAssignable" + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? ">" : ("," + textAlignmentClass.GenericArguments + ">")) + "(this TAssignable textAlignmentControl) where TAssignable : " + textAlignmentClass.Namespace + "." + textAlignmentClass.ClassName + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? "" : "<") + textAlignmentClass.GenericArguments + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? "" : ">") + textAlignmentClass.GenericConstraints + @"
		{
			ArgumentNullException.ThrowIfNull(textAlignmentControl);

			if (textAlignmentControl is not ITextAlignment)
			{
				throw new ArgumentException($""Element must implement {nameof(ITextAlignment)}"", nameof(textAlignmentControl));
			}

			textAlignmentControl.HorizontalTextAlignment = TextAlignment.Start;
			return textAlignmentControl;
		}

		/// <summary>
		/// <see cref=""ITextAlignment.HorizontalTextAlignment""/> = <see cref=""TextAlignment.Center""/>
		/// </summary>
		/// <param name=""textAlignmentControl""></param>
		/// <returns>" + textAlignmentClass.Namespace + "." + textAlignmentClass.ClassName + @" with added <see cref=""TextAlignment.Center""/></returns>
		public static TAssignable TextCenterHorizontal<TAssignable" + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? ">" : ("," + textAlignmentClass.GenericArguments + ">")) + "(this TAssignable textAlignmentControl) where TAssignable : " + textAlignmentClass.Namespace + "." + textAlignmentClass.ClassName + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? "" : "<") + textAlignmentClass.GenericArguments + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? "" : ">") + textAlignmentClass.GenericConstraints + @"
		{
			ArgumentNullException.ThrowIfNull(textAlignmentControl);

			if (textAlignmentControl is not ITextAlignment)
			{
				throw new ArgumentException($""Element must implement {nameof(ITextAlignment)}"", nameof(textAlignmentControl));
			}

			textAlignmentControl.HorizontalTextAlignment = TextAlignment.Center;
			return textAlignmentControl;
		}

		/// <summary>
		/// <see cref=""ITextAlignment.HorizontalTextAlignment""/> = <see cref=""TextAlignment.End""/>
		/// </summary>
		/// <param name=""textAlignmentControl""></param>
		/// <returns>" + textAlignmentClass.Namespace + "." + textAlignmentClass.ClassName + @" with added <see cref=""TextAlignment.End""/></returns>
		public static TAssignable TextEnd<TAssignable" + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? ">" : ("," + textAlignmentClass.GenericArguments + ">")) + "(this TAssignable textAlignmentControl) where TAssignable : " + textAlignmentClass.Namespace + "." + textAlignmentClass.ClassName + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? "" : "<") + textAlignmentClass.GenericArguments + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? "" : ">") + textAlignmentClass.GenericConstraints + @"
		{
			ArgumentNullException.ThrowIfNull(textAlignmentControl);

			if (textAlignmentControl is not ITextAlignment)
			{
				throw new ArgumentException($""Element must implement {nameof(ITextAlignment)}"", nameof(textAlignmentControl));
			}

			textAlignmentControl.HorizontalTextAlignment = TextAlignment.End;
			return textAlignmentControl;
		}

		/// <summary>
		/// <see cref=""ITextAlignment.VerticalTextAlignment""/> = <see cref=""TextAlignment.Start""/>
		/// </summary>
		/// <param name=""textAlignmentControl""></param>
		/// <returns>" + textAlignmentClass.Namespace + "." + textAlignmentClass.ClassName + @" with added <see cref=""TextAlignment.Start""/></returns>
		public static TAssignable TextTop<TAssignable" + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? ">" : ("," + textAlignmentClass.GenericArguments + ">")) + "(this TAssignable textAlignmentControl) where TAssignable : " + textAlignmentClass.Namespace + "." + textAlignmentClass.ClassName + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? "" : "<") + textAlignmentClass.GenericArguments + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? "" : ">") + textAlignmentClass.GenericConstraints + @"
		{
			ArgumentNullException.ThrowIfNull(textAlignmentControl);

			if (textAlignmentControl is not ITextAlignment)
			{
				throw new ArgumentException($""Element must implement {nameof(ITextAlignment)}"", nameof(textAlignmentControl));
			}

			textAlignmentControl.VerticalTextAlignment = TextAlignment.Start;
			return textAlignmentControl;
		}

		/// <summary>
		/// <see cref=""ITextAlignment.VerticalTextAlignment""/> = <see cref=""TextAlignment.Center""/>
		/// </summary>
		/// <param name=""textAlignmentControl""></param>
		/// <returns>" + textAlignmentClass.Namespace + "." + textAlignmentClass.ClassName + @" with added <see cref=""TextAlignment.Center""/></returns>
		public static TAssignable TextCenterVertical<TAssignable" + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? ">" : ("," + textAlignmentClass.GenericArguments + ">")) + "(this TAssignable textAlignmentControl) where TAssignable : " + textAlignmentClass.Namespace + "." + textAlignmentClass.ClassName + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? "" : "<") + textAlignmentClass.GenericArguments + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? "" : ">") + textAlignmentClass.GenericConstraints + @"
		{
			ArgumentNullException.ThrowIfNull(textAlignmentControl);

			if (textAlignmentControl is not ITextAlignment)
			{
				throw new ArgumentException($""Element must implement {nameof(ITextAlignment)}"", nameof(textAlignmentControl));
			}

			textAlignmentControl.VerticalTextAlignment = TextAlignment.Center;
			return textAlignmentControl;
		}

		/// <summary>
		/// <see cref=""ITextAlignment.VerticalTextAlignment""/> = <see cref=""TextAlignment.End""/>
		/// </summary>
		/// <param name=""textAlignmentControl""></param>
		/// <returns>" + textAlignmentClass.Namespace + "." + textAlignmentClass.ClassName + @" with added <see cref=""TextAlignment.End""/></returns>
		public static TAssignable TextBottom<TAssignable" + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? ">" : ("," + textAlignmentClass.GenericArguments + ">")) + "(this TAssignable textAlignmentControl) where TAssignable : " + textAlignmentClass.Namespace + "." + textAlignmentClass.ClassName + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? "" : "<") + textAlignmentClass.GenericArguments + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? "" : ">") + textAlignmentClass.GenericConstraints + @"
		{
			ArgumentNullException.ThrowIfNull(textAlignmentControl);

			if (textAlignmentControl is not ITextAlignment)
			{
				throw new ArgumentException($""Element must implement {nameof(ITextAlignment)}"", nameof(textAlignmentControl));
			}

			textAlignmentControl.VerticalTextAlignment = TextAlignment.End;
			return textAlignmentControl;
		}

		/// <summary>
		/// <see cref=""ITextAlignment.VerticalTextAlignment""/> = <see cref=""ITextAlignment.HorizontalTextAlignment""/> = <see cref=""TextAlignment.Center""/>
		/// </summary>
		/// <param name=""textAlignmentControl""></param>
		/// <returns>" + textAlignmentClass.Namespace + "." + textAlignmentClass.ClassName + @" with added <see cref=""TextAlignment.Center""/></returns>
		public static TAssignable TextCenter<TAssignable" + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? ">" : ("," + textAlignmentClass.GenericArguments + ">")) + "(this TAssignable textAlignmentControl) where TAssignable : " + textAlignmentClass.Namespace + "." + textAlignmentClass.ClassName + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? "" : "<") + textAlignmentClass.GenericArguments + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? "" : ">") + textAlignmentClass.GenericConstraints + @"
			=> textAlignmentControl.TextCenterHorizontal<TAssignable" + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? ">" : ("," + textAlignmentClass.GenericArguments + ">")) + "().TextCenterVertical<TAssignable" + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? " > " : ("," + textAlignmentClass.GenericArguments + " > ")) + @"();
	}


	// The extensions in these sub-namespaces are designed to be used together with the extensions in the parent namespace.
	// Keep them in a single file for better maintainability

	namespace LeftToRight
    {
	    /// <summary>
	    /// Extension Methods for <see cref=""ITextAlignment""/>
	    /// </summary>
	    " + textAlignmentClass.ClassAcessModifier + " static partial class TextAlignmentExtensions_" + textAlignmentClass.ClassName + @"
	    {
		    /// <summary>
		    /// <see cref=""ITextAlignment.HorizontalTextAlignment""/> = <see cref=""TextAlignment.Start""/>
		    /// </summary>
		    /// <param name=""textAlignmentControl""></param>
		    /// <returns>" + textAlignmentClass.Namespace + "." + textAlignmentClass.ClassName + @" with <see cref=""TextAlignment.Start""/></returns>
		    public static TAssignable TextLeft<TAssignable" + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? ">" : ("," + textAlignmentClass.GenericArguments + ">")) + "(this TAssignable textAlignmentControl) where TAssignable : " + textAlignmentClass.Namespace + "." + textAlignmentClass.ClassName + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? "" : "<") + textAlignmentClass.GenericArguments + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? "" : ">") + textAlignmentClass.GenericConstraints + @"
		    {
				ArgumentNullException.ThrowIfNull(textAlignmentControl);

				if (textAlignmentControl is not ITextAlignment)
				{
					throw new ArgumentException($""Element must implement {nameof(ITextAlignment)}"", nameof(textAlignmentControl));
				}

			    textAlignmentControl.HorizontalTextAlignment = TextAlignment.Start;
			    return textAlignmentControl;
		    }

		    /// <summary>
		    /// <see cref=""ITextAlignment.HorizontalTextAlignment""/> = <see cref=""TextAlignment.End""/>
		    /// </summary>
		    /// <param name=""textAlignmentControl""></param>
		    /// <returns>" + textAlignmentClass.Namespace + "." + textAlignmentClass.ClassName + @" with <see cref=""TextAlignment.End""/></returns>
		    public static TAssignable TextRight<TAssignable" + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? ">" : ("," + textAlignmentClass.GenericArguments + ">")) + "(this TAssignable textAlignmentControl) where TAssignable : " + textAlignmentClass.Namespace + "." + textAlignmentClass.ClassName + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? "" : "<") + textAlignmentClass.GenericArguments + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? "" : ">") + textAlignmentClass.GenericConstraints + @"
		    {
				ArgumentNullException.ThrowIfNull(textAlignmentControl);

				if (textAlignmentControl is not ITextAlignment)
				{
					throw new ArgumentException($""Element must implement {nameof(ITextAlignment)}"", nameof(textAlignmentControl));
				}

			    textAlignmentControl.HorizontalTextAlignment = TextAlignment.End;
			    return textAlignmentControl;
		    }
	    }
    }

    // The extensions in these sub-namespaces are designed to be used together with the extensions in the parent namespace.
    // Keep them in a single file for better maintainability
    namespace RightToLeft
    {
	    /// <summary>
	    /// Extension methods for <see cref=""ITextAlignment""/>
	    /// </summary>
	    " + textAlignmentClass.ClassAcessModifier + " static partial class TextAlignmentExtensions_" + textAlignmentClass.ClassName + @"
	    {
		    /// <summary>
		    /// <see cref=""ITextAlignment.HorizontalTextAlignment""/> = <see cref=""TextAlignment.End""/>
		    /// </summary>
		    /// <param name=""textAlignmentControl""></param>
		    /// <returns>" + textAlignmentClass.Namespace + "." + textAlignmentClass.ClassName + @" with <see cref=""TextAlignment.End""/></returns>
		    public static TAssignable TextLeft<TAssignable" + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? ">" : ("," + textAlignmentClass.GenericArguments + ">")) + "(this TAssignable textAlignmentControl) where TAssignable : " + textAlignmentClass.Namespace + "." + textAlignmentClass.ClassName + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? "" : "<") + textAlignmentClass.GenericArguments + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? "" : ">") + textAlignmentClass.GenericConstraints + @"
		    {
				ArgumentNullException.ThrowIfNull(textAlignmentControl);

				if (textAlignmentControl is not ITextAlignment)
				{
					throw new ArgumentException($""Element must implement {nameof(ITextAlignment)}"", nameof(textAlignmentControl));
				}

			    textAlignmentControl.HorizontalTextAlignment = TextAlignment.End;
			    return textAlignmentControl;
		    }

		    /// <summary>
		    /// <see cref=""ITextAlignment.HorizontalTextAlignment""/> = <see cref=""TextAlignment.Start""/>
		    /// </summary>
		    /// <param name=""textAlignmentControl""></param>
		    /// <returns>" + textAlignmentClass.Namespace + "." + textAlignmentClass.ClassName + @" with <see cref=""TextAlignment.Start""/></returns>
		    public static TAssignable TextRight<TAssignable" + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? ">" : ("," + textAlignmentClass.GenericArguments + ">")) + "(this TAssignable textAlignmentControl) where TAssignable : " + textAlignmentClass.Namespace + "." + textAlignmentClass.ClassName + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? "" : "<") + textAlignmentClass.GenericArguments + (string.IsNullOrWhiteSpace(textAlignmentClass.GenericArguments) ? "" : ">") + textAlignmentClass.GenericConstraints + @"
		    {
				ArgumentNullException.ThrowIfNull(textAlignmentControl);

				if (textAlignmentControl is not ITextAlignment)
				{
					throw new ArgumentException($""Element must implement {nameof(ITextAlignment)}"", nameof(textAlignmentControl));
				}

			    textAlignmentControl.HorizontalTextAlignment = TextAlignment.Start;
			    return textAlignmentControl;
		    }
	    }
    }
}";
			var source = textColorToBuilder.ToString();
			SourceStringExtensions.FormatText(ref source, options);
			context.AddSource($"{textAlignmentClass.ClassName}TextAlignmentExtensions.g.cs", SourceText.From(source, Encoding.UTF8));
		}

		static string GetClassAccessModifier(INamedTypeSymbol namedTypeSymbol) => namedTypeSymbol.DeclaredAccessibility switch
		{
			Accessibility.Public => "public",
			Accessibility.Internal => "internal",
			_ => string.Empty
		};
	}
}