﻿using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace CommunityToolkit.Maui.Markup.SourceGenerators;

[Generator(LanguageNames.CSharp)]
public class TextAlignmentExtensionsGenerator : IIncrementalGenerator
{
	const string textAlignmentInterface = "Microsoft.Maui.ITextAlignment";
	const string mauiControlsAssembly = "Microsoft.Maui.Controls";

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		IncrementalValuesProvider<(INamedTypeSymbol ClassSymbol, INamedTypeSymbol ITextAlignmentInterfaceSymbol)> userGeneratedClassesProvider = context.SyntaxProvider
			.CreateSyntaxProvider(
				static (syntaxNode, _) => syntaxNode is ClassDeclarationSyntax { BaseList: not null },
				static (syntaxContext, ct) =>
				{
					var compilation = syntaxContext.SemanticModel.Compilation;
					var iTextAlignmentInterfaceSymbol = compilation.GetTypeByMetadataName(textAlignmentInterface);
					if (iTextAlignmentInterfaceSymbol is null)
					{
						return default; // Early return to avoid unnecessary processing
					}

					var classSymbol = syntaxContext.SemanticModel.GetDeclaredSymbol((ClassDeclarationSyntax)syntaxContext.Node, ct);
					if (classSymbol is null || classSymbol.DeclaringSyntaxReferences[0].GetSyntax(ct) != syntaxContext.Node)
					{
						return default;
					}

					return (classSymbol, iTextAlignmentInterfaceSymbol);
				})
			.Where(static tuple => tuple != default && ShouldGenerateTextAlignmentExtension(tuple.classSymbol, tuple.iTextAlignmentInterfaceSymbol));

		var compilationProvider = context.CompilationProvider;

		// Combine providers to reduce the number of operations
		var combined = userGeneratedClassesProvider
			.Collect()
			.Combine(compilationProvider);

		// Register the source output
		context.RegisterSourceOutput(combined, static (spc, source) => Execute(spc, source.Right, [..source.Left.Select(static x => x.ClassSymbol)]));
	}

	static bool ShouldGenerateTextAlignmentExtension(INamedTypeSymbol classSymbol, INamedTypeSymbol iTextAlignmentInterfaceSymbol)
	{
		return DoesImplementInterfaceIgnoringBaseType(classSymbol, iTextAlignmentInterfaceSymbol)
			&& !DoesImplementInterface(classSymbol.BaseType, iTextAlignmentInterfaceSymbol);

		static bool DoesImplementInterfaceIgnoringBaseType(INamedTypeSymbol classSymbol, INamedTypeSymbol interfaceSymbol)
			=> classSymbol.AllInterfaces.Contains(interfaceSymbol);

		static bool DoesImplementInterface(INamedTypeSymbol? classSymbol, INamedTypeSymbol interfaceSymbol)
			=> classSymbol?.AllInterfaces.Contains(interfaceSymbol) ?? false;
	}

	static void Execute(SourceProductionContext context, Compilation compilation, ImmutableArray<INamedTypeSymbol> userClasses)
	{
		var mauiAssembly = compilation.SourceModule.ReferencedAssemblySymbols.FirstOrDefault(static a => a.Name == mauiControlsAssembly);
		if (mauiAssembly is null)
		{
			return;
		}

		var iTextAlignmentInterfaceSymbol = compilation.GetTypeByMetadataName(textAlignmentInterface);
		if (iTextAlignmentInterfaceSymbol is null)
		{
			return;
		}

		var mauiClasses = GetMauiInterfaceImplementors(mauiAssembly, iTextAlignmentInterfaceSymbol);

		// Use HashSet for faster lookup
		var processedClasses = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);

		foreach (var classSymbol in userClasses.Concat(mauiClasses))
		{
			if (processedClasses.Add(classSymbol))
			{
				var metadata = GenerateMetadata(classSymbol);
				GenerateExtensionClass(context, metadata);
			}
		}
	}

	static IEnumerable<INamedTypeSymbol> GetMauiInterfaceImplementors(IAssemblySymbol mauiAssembly, INamedTypeSymbol iTextAlignmentSymbol)
	{
		return mauiAssembly.GlobalNamespace.GetNamedTypeSymbols()
			.Where(x => ShouldGenerateTextAlignmentExtension(x, iTextAlignmentSymbol));
	}

	static string GetClassAccessModifier(INamedTypeSymbol namedTypeSymbol) => namedTypeSymbol.DeclaredAccessibility switch
	{
		Accessibility.Public => "public",
		Accessibility.Internal => "internal",
		_ => string.Empty
	};

	static void GenerateExtensionClass(SourceProductionContext context, TextAlignmentClassMetadata metadata)
	{
		var source = GenerateExtensionClassSource(metadata);
		context.AddSource($"{metadata.ClassName}TextAlignmentExtensions.g.cs", SourceText.From(source, Encoding.UTF8));
	}

	static string GenerateExtensionClassSource(TextAlignmentClassMetadata metadata)
	{
		var assemblyVersion = typeof(TextAlignmentExtensionsGenerator).Assembly.GetName().Version.ToString();
		var className = typeof(TextAlignmentExtensionsGenerator).FullName;

		var sb = new StringBuilder();
		sb.AppendLine( /* language=C#-test */
			$$"""
			  // <auto-generated>
			  // See: CommunityToolkit.Maui.Markup.SourceGenerators.TextAlignmentGenerator

			  #nullable enable
			  #pragma warning disable

			  using System;
			  using Microsoft.Maui;
			  using Microsoft.Maui.Controls;

			  namespace CommunityToolkit.Maui.Markup
			  {
			      /// <summary>
			      /// Extension Methods for <see cref="ITextAlignment"/>
			      /// </summary>
			      [global::System.CodeDom.Compiler.GeneratedCode("{{className}}", "{{assemblyVersion}}")]
			      [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
			      {{metadata.ClassAccessModifier}} static partial class TextAlignmentExtensions_{{metadata.ClassName}}
			      {
			          /// <summary>
			          /// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.Start"/>
			          /// </summary>
			          /// <param name="textAlignmentControl"></param>
			          /// <returns><typeparamref name="TAssignable"/> with added <see cref="TextAlignment.Start"/></returns>
			          public static TAssignable TextStart{{metadata.GenericTypeParameters}}(this TAssignable textAlignmentControl)
			  			where TAssignable : {{metadata.Namespace}}.{{metadata.ClassName}}{{metadata.GenericArguments}}{{metadata.GenericConstraints}}
			          {
			              ArgumentNullException.ThrowIfNull(textAlignmentControl);
			  
			              if (textAlignmentControl is not ITextAlignment)
			              {
			                  throw new ArgumentException($"Element must implement {nameof(ITextAlignment)}", nameof(textAlignmentControl));
			              }
			  
			              textAlignmentControl.HorizontalTextAlignment = TextAlignment.Start;
			              return textAlignmentControl;
			          }
			  
			          /// <summary>
			          /// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.Center"/>
			          /// </summary>
			          /// <param name="textAlignmentControl"></param>
			          /// <returns><typeparamref name="TAssignable"/> with added <see cref="TextAlignment.Center"/></returns>
			          public static TAssignable TextCenterHorizontal{{metadata.GenericTypeParameters}}(this TAssignable textAlignmentControl)
			  			where TAssignable : {{metadata.Namespace}}.{{metadata.ClassName}}{{metadata.GenericArguments}}{{metadata.GenericConstraints}}
			          {
			              ArgumentNullException.ThrowIfNull(textAlignmentControl);
			  
			              if (textAlignmentControl is not ITextAlignment)
			              {
			                  throw new ArgumentException($"Element must implement {nameof(ITextAlignment)}", nameof(textAlignmentControl));
			              }
			  
			              textAlignmentControl.HorizontalTextAlignment = TextAlignment.Center;
			              return textAlignmentControl;
			          }
			  
			          /// <summary>
			          /// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.End"/>
			          /// </summary>
			          /// <param name="textAlignmentControl"></param>
			          /// <returns><typeparamref name="TAssignable"/> with added <see cref="TextAlignment.End"/></returns>
			          public static TAssignable TextEnd{{metadata.GenericTypeParameters}}(this TAssignable textAlignmentControl)
			  			where TAssignable : {{metadata.Namespace}}.{{metadata.ClassName}}{{metadata.GenericArguments}}{{metadata.GenericConstraints}}
			          {
			              ArgumentNullException.ThrowIfNull(textAlignmentControl);
			  
			              if (textAlignmentControl is not ITextAlignment)
			              {
			                  throw new ArgumentException($"Element must implement {nameof(ITextAlignment)}", nameof(textAlignmentControl));
			              }
			  
			              textAlignmentControl.HorizontalTextAlignment = TextAlignment.End;
			              return textAlignmentControl;
			          }
			  
			          /// <summary>
			          /// <see cref="ITextAlignment.VerticalTextAlignment"/> = <see cref="TextAlignment.Start"/>
			          /// </summary>
			          /// <param name="textAlignmentControl"></param>
			          /// <returns><typeparamref name="TAssignable"/> with added <see cref="TextAlignment.Start"/></returns>
			          public static TAssignable TextTop{{metadata.GenericTypeParameters}}(this TAssignable textAlignmentControl)
			  			where TAssignable : {{metadata.Namespace}}.{{metadata.ClassName}}{{metadata.GenericArguments}}{{metadata.GenericConstraints}}
			          {
			              ArgumentNullException.ThrowIfNull(textAlignmentControl);
			  
			              if (textAlignmentControl is not ITextAlignment)
			              {
			                  throw new ArgumentException($"Element must implement {nameof(ITextAlignment)}", nameof(textAlignmentControl));
			              }
			  
			              textAlignmentControl.VerticalTextAlignment = TextAlignment.Start;
			              return textAlignmentControl;
			          }
			  
			          /// <summary>
			          /// <see cref="ITextAlignment.VerticalTextAlignment"/> = <see cref="TextAlignment.Center"/>
			          /// </summary>
			          /// <param name="textAlignmentControl"></param>
			          /// <returns><typeparamref name="TAssignable"/> with added <see cref="TextAlignment.Center"/></returns>
			          public static TAssignable TextCenterVertical{{metadata.GenericTypeParameters}}(this TAssignable textAlignmentControl)
			  			where TAssignable : {{metadata.Namespace}}.{{metadata.ClassName}}{{metadata.GenericArguments}}{{metadata.GenericConstraints}}
			          {
			              ArgumentNullException.ThrowIfNull(textAlignmentControl);
			  
			              if (textAlignmentControl is not ITextAlignment)
			              {
			                  throw new ArgumentException($"Element must implement {nameof(ITextAlignment)}", nameof(textAlignmentControl));
			              }
			  
			              textAlignmentControl.VerticalTextAlignment = TextAlignment.Center;
			              return textAlignmentControl;
			          }
			  
			          /// <summary>
			          /// <see cref="ITextAlignment.VerticalTextAlignment"/> = <see cref="TextAlignment.End"/>
			          /// </summary>
			          /// <param name="textAlignmentControl"></param>
			          /// <returns><typeparamref name="TAssignable"/> with added <see cref="TextAlignment.End"/></returns>
			          public static TAssignable TextBottom{{metadata.GenericTypeParameters}}(this TAssignable textAlignmentControl)
			  			where TAssignable : {{metadata.Namespace}}.{{metadata.ClassName}}{{metadata.GenericArguments}}{{metadata.GenericConstraints}}
			          {
			              ArgumentNullException.ThrowIfNull(textAlignmentControl);
			  
			              if (textAlignmentControl is not ITextAlignment)
			              {
			                  throw new ArgumentException($"Element must implement {nameof(ITextAlignment)}", nameof(textAlignmentControl));
			              }
			  
			              textAlignmentControl.VerticalTextAlignment = TextAlignment.End;
			              return textAlignmentControl;
			          }
			  
			          /// <summary>
			          /// <see cref="ITextAlignment.VerticalTextAlignment"/> = <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.Center"/>
			          /// </summary>
			          /// <param name="textAlignmentControl"></param>
			          /// <returns><typeparamref name="TAssignable"/> with added <see cref="TextAlignment.Center"/></returns>
			          public static TAssignable TextCenter{{metadata.GenericTypeParameters}}(this TAssignable textAlignmentControl)
			  			where TAssignable : {{metadata.Namespace}}.{{metadata.ClassName}}{{metadata.GenericArguments}}{{metadata.GenericConstraints}}
			              => textAlignmentControl.TextCenterHorizontal{{metadata.GenericTypeParameters}}().TextCenterVertical{{metadata.GenericTypeParameters}}();
			      }
			  
			  
			      // The extensions in these sub-namespaces are designed to be used together with the extensions in the parent namespace.
			      // Keep them in a single file for better maintainability
			  
			      namespace LeftToRight
			      {
			          /// <summary>
			          /// Extension Methods for <see cref="ITextAlignment"/>
			          /// </summary>
			          [global::System.CodeDom.Compiler.GeneratedCode("{{className}}", "{{assemblyVersion}}")]
			          [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
			          {{metadata.ClassAccessModifier}} static partial class TextAlignmentExtensions_{{metadata.ClassName}}
			          {
			              /// <summary>
			              /// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.Start"/>
			              /// </summary>
			              /// <param name="textAlignmentControl"></param>
			              /// <returns><typeparamref name="TAssignable"/> with <see cref="TextAlignment.Start"/></returns>
			              public static TAssignable TextLeft{{metadata.GenericTypeParameters}}(this TAssignable textAlignmentControl)
			  				where TAssignable : {{metadata.Namespace}}.{{metadata.ClassName}}{{metadata.GenericArguments}}{{metadata.GenericConstraints}}
			              {
			                  ArgumentNullException.ThrowIfNull(textAlignmentControl);
			  
			                  if (textAlignmentControl is not ITextAlignment)
			                  {
			                      throw new ArgumentException($"Element must implement {nameof(ITextAlignment)}", nameof(textAlignmentControl));
			                  }
			  
			                  textAlignmentControl.HorizontalTextAlignment = TextAlignment.Start;
			                  return textAlignmentControl;
			              }
			  
			              /// <summary>
			              /// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.End"/>
			              /// </summary>
			              /// <param name="textAlignmentControl"></param>
			              /// <returns><typeparamref name="TAssignable"/> with <see cref="TextAlignment.End"/></returns>
			              public static TAssignable TextRight{{metadata.GenericTypeParameters}}(this TAssignable textAlignmentControl) where TAssignable : {{metadata.Namespace}}.{{metadata.ClassName}}{{metadata.GenericArguments}}{{metadata.GenericConstraints}}
			              {
			                  ArgumentNullException.ThrowIfNull(textAlignmentControl);
			  
			                  if (textAlignmentControl is not ITextAlignment)
			                  {
			                       throw new ArgumentException($"Element must implement {nameof(ITextAlignment)}", nameof(textAlignmentControl));
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
			          /// Extension methods for <see cref="ITextAlignment"/>
			          /// </summary>
			          {{metadata.ClassAccessModifier}} static partial class TextAlignmentExtensions_{{metadata.ClassName}}
			          {
			              /// <summary>
			              /// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.End"/>
			              /// </summary>
			              /// <param name="textAlignmentControl"></param>
			              /// <returns><typeparamref name="TAssignable"/> with <see cref="TextAlignment.End"/></returns>
			              public static TAssignable TextLeft{{metadata.GenericTypeParameters}}(this TAssignable textAlignmentControl)
			  				where TAssignable : {{metadata.Namespace}}.{{metadata.ClassName}}{{metadata.GenericArguments}}{{metadata.GenericConstraints}}
			              {
			                  ArgumentNullException.ThrowIfNull(textAlignmentControl);
			  
			                  if (textAlignmentControl is not ITextAlignment)
			                  {
			                      throw new ArgumentException($"Element must implement {nameof(ITextAlignment)}", nameof(textAlignmentControl));
			                  }
			  
			                  textAlignmentControl.HorizontalTextAlignment = TextAlignment.End;
			                  return textAlignmentControl;
			              }
			  
			              /// <summary>
			              /// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.Start"/>
			              /// </summary>
			              /// <param name="textAlignmentControl"></param>
			              /// <returns><typeparamref name="TAssignable"/> with <see cref="TextAlignment.Start"/></returns>
			              public static TAssignable TextRight{{metadata.GenericTypeParameters}}(this TAssignable textAlignmentControl)
			  				where TAssignable : {{metadata.Namespace}}.{{metadata.ClassName}}{{metadata.GenericArguments}}{{metadata.GenericConstraints}}
			              {
			                  ArgumentNullException.ThrowIfNull(textAlignmentControl);
			  
			                  if (textAlignmentControl is not ITextAlignment)
			                  {
			                      throw new ArgumentException($"Element must implement {nameof(ITextAlignment)}", nameof(textAlignmentControl));
			                  }
			  
			                  textAlignmentControl.HorizontalTextAlignment = TextAlignment.Start;
			                  return textAlignmentControl;
			              }
			          }
			      }
			  }
			  """);

		return sb.ToString();
	}

	static TextAlignmentClassMetadata GenerateMetadata(INamedTypeSymbol namedTypeSymbol)
	{
		var accessModifier = namedTypeSymbol.ContainingNamespace.ToDisplayString() == mauiControlsAssembly
			? "internal"
			: GetClassAccessModifier(namedTypeSymbol);

		var genericTypeParameters = GetGenericTypeParametersDeclarationString(namedTypeSymbol);
		var genericArguments = GetGenericArgumentsString(namedTypeSymbol);
		var genericConstraints = GetGenericConstraintsString(namedTypeSymbol);

		return new TextAlignmentClassMetadata(
			namedTypeSymbol.Name,
			accessModifier,
			namedTypeSymbol.ContainingNamespace.ToDisplayString(),
			genericTypeParameters,
			genericArguments,
			genericConstraints
		);
	}

	static string GetGenericTypeParametersDeclarationString(INamedTypeSymbol namedTypeSymbol)
	{
		if (namedTypeSymbol.TypeParameters.Length is 0)
		{
			return "<TAssignable>";
		}

		var typeParams = string.Join(", ", namedTypeSymbol.TypeParameters.Select(t => t.Name));
		return $"<TAssignable, {typeParams}>";
	}

	static string GetGenericArgumentsString(INamedTypeSymbol namedTypeSymbol)
	{
		return namedTypeSymbol.TypeParameters.Length > 0
			? $"<{string.Join(", ", namedTypeSymbol.TypeParameters.Select(t => t.Name))}>"
			: string.Empty;
	}

	static string GetGenericConstraintsString(INamedTypeSymbol namedTypeSymbol)
	{
		var constraints = namedTypeSymbol.TypeParameters
			.Select(GetGenericParameterConstraints)
			.Where(static c => !string.IsNullOrEmpty(c));

		return string.Join(" ", constraints);
	}

	static string GetGenericParameterConstraints(ITypeParameterSymbol typeParameter)
	{
		var constraints = new List<string>();

		// Primary constraint (class, struct, unmanaged)
		if (typeParameter.HasReferenceTypeConstraint)
		{
			constraints.Add(typeParameter.ReferenceTypeConstraintNullableAnnotation is NullableAnnotation.Annotated 
				? "class?" 
				: "class");
		}
		else if (typeParameter.HasValueTypeConstraint)
		{
			constraints.Add(typeParameter.HasUnmanagedTypeConstraint ? "unmanaged" : "struct");
		}
		else if (typeParameter.HasUnmanagedTypeConstraint)
		{
			constraints.Add("unmanaged");
		}
		else if (typeParameter.HasNotNullConstraint)
		{
			constraints.Add("notnull");
		}

		// Secondary constraints (specific types)
		foreach (var constraintType in typeParameter.ConstraintTypes)
		{
			var symbolDisplayFormat = new SymbolDisplayFormat(
				typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
				miscellaneousOptions: SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier);
			
			var constraintTypeString = constraintType.ToDisplayString(symbolDisplayFormat);

			constraints.Add(constraintTypeString);
		}

		// Check for record constraint
		if (typeParameter.IsRecord)
		{
			constraints.Add("record");
		}

		// Constructor constraint (must be last)
		if (typeParameter.HasConstructorConstraint)
		{
			constraints.Add("new()");
		}

		return constraints.Count > 0
			? $"where {typeParameter.Name} : {string.Join(", ", constraints)}"
			: string.Empty;
	}
}