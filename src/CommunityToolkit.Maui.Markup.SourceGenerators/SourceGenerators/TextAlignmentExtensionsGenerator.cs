﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
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
			static (context, cancellationToken) =>
			{
				var compilation = context.SemanticModel.Compilation;

				var iTextAlignmentInterfaceSymbol = compilation.GetTypeByMetadataName(iTextAlignmentInterface);

				if (iTextAlignmentInterfaceSymbol is null)
				{
					throw new Exception("There's no .NET MAUI referenced in the project.");
				}

				var classSymbol = context.SemanticModel.GetDeclaredSymbol((ClassDeclarationSyntax)context.Node, cancellationToken);
				if (classSymbol is null || classSymbol.DeclaringSyntaxReferences[0].GetSyntax(cancellationToken) != context.Node)
				{
					// In case of multiple partial declarations, we want to run only once.
					// So we run only for the first syntax reference.
					return null;
				}

				while (classSymbol is not null)
				{
					if (classSymbol.ContainingAssembly.Name == mauiControlsAssembly)
					{
						break;
					}

					if (classSymbol.Interfaces.Any(i => i.Equals(iTextAlignmentInterfaceSymbol, SymbolEqualityComparer.Default) || i.AllInterfaces.Contains(iTextAlignmentInterfaceSymbol, SymbolEqualityComparer.Default)))
					{
						return GenerateMetadata(classSymbol);
					}

					classSymbol = classSymbol.BaseType;
				}

				return null;
			}).Where(m => m is not null);

		// Get Microsoft.Maui.Controls Symbols that implements the desired interfaces
		var mauiControlsAssemblySymbolProvider = context.CompilationProvider.Select(
			static (compilation, token) =>
			{
				var iTextAlignmentInterfaceSymbol = compilation.GetTypeByMetadataName(iTextAlignmentInterface);

				if (iTextAlignmentInterfaceSymbol is null)
				{
					throw new Exception("There's no .NET MAUI referenced in the project.");
				}

				var mauiAssembly = compilation.SourceModule.ReferencedAssemblySymbols.Single(q => q.Name == mauiControlsAssembly);
				return EquatableArray.AsEquatableArray(GetMauiInterfaceImplementors(mauiAssembly, iTextAlignmentInterfaceSymbol).ToImmutableArray());
			});


		// Here we Collect all the Classes candidates from the first pipeline
		// Then we merge them with the Maui.Controls that implements the desired interfaces
		// Then we make sure they are unique and the user control doesn't inherit from any Maui control that implements the desired interface already
		// Then we transform the ISymbol to be a type that we can compare and preserve the Incremental behavior of this Source Generator
		context.RegisterSourceOutput(userGeneratedClassesProvider, Execute!);
		context.RegisterSourceOutput(mauiControlsAssemblySymbolProvider, ExecuteArray);
	}

	static void ExecuteArray(SourceProductionContext context, EquatableArray<TextAlignmentClassMetadata> metadataArray)
	{
		var immutable = metadataArray.AsImmutableArray();
		foreach (var metadata in metadataArray)
		{
			Execute(context, metadata);
		}
	}

	static void Execute(SourceProductionContext context, TextAlignmentClassMetadata textAlignmentClassMetadata)
	{
		var genericTypeParameters = GetGenericTypeParametersDeclarationString(textAlignmentClassMetadata.GenericArguments);
		var genericArguments = GetGenericArgumentsString(textAlignmentClassMetadata.GenericArguments);
		var textColorToBuilder = $$"""
// <auto-generated>
// See: CommunityToolkit.Maui.Markup.SourceGenerators.TextAlignmentGenerator
#nullable enable
using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace CommunityToolkit.Maui.Markup
{
    /// <summary>
    /// Extension Methods for <see cref="ITextAlignment"/>
    /// </summary>
    {{textAlignmentClassMetadata.ClassAcessModifier}} static partial class TextAlignmentExtensions_{{textAlignmentClassMetadata.ClassName}}
    {
        /// <summary>
        /// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.Start"/>
        /// </summary>
        /// <param name="textAlignmentControl"></param>
        /// <returns><typeparamref name="TAssignable"/> with added <see cref="TextAlignment.Start"/></returns>
        public static TAssignable TextStart{{genericTypeParameters}}(this TAssignable textAlignmentControl)
			where TAssignable : {{textAlignmentClassMetadata.Namespace}}.{{textAlignmentClassMetadata.ClassName}}{{genericArguments}}{{textAlignmentClassMetadata.GenericConstraints}}
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
        public static TAssignable TextCenterHorizontal{{genericTypeParameters}}(this TAssignable textAlignmentControl)
			where TAssignable : {{textAlignmentClassMetadata.Namespace}}.{{textAlignmentClassMetadata.ClassName}}{{genericArguments}}{{textAlignmentClassMetadata.GenericConstraints}}
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
        public static TAssignable TextEnd{{genericTypeParameters}}(this TAssignable textAlignmentControl)
			where TAssignable : {{textAlignmentClassMetadata.Namespace}}.{{textAlignmentClassMetadata.ClassName}}{{genericArguments}}{{textAlignmentClassMetadata.GenericConstraints}}
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
        public static TAssignable TextTop{{genericTypeParameters}}(this TAssignable textAlignmentControl)
			where TAssignable : {{textAlignmentClassMetadata.Namespace}}.{{textAlignmentClassMetadata.ClassName}}{{genericArguments}}{{textAlignmentClassMetadata.GenericConstraints}}
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
        public static TAssignable TextCenterVertical{{genericTypeParameters}}(this TAssignable textAlignmentControl)
			where TAssignable : {{textAlignmentClassMetadata.Namespace}}.{{textAlignmentClassMetadata.ClassName}}{{genericArguments}}{{textAlignmentClassMetadata.GenericConstraints}}
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
        public static TAssignable TextBottom{{genericTypeParameters}}(this TAssignable textAlignmentControl)
			where TAssignable : {{textAlignmentClassMetadata.Namespace}}.{{textAlignmentClassMetadata.ClassName}}{{genericArguments}}{{textAlignmentClassMetadata.GenericConstraints}}
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
        public static TAssignable TextCenter{{genericTypeParameters}}(this TAssignable textAlignmentControl)
			where TAssignable : {{textAlignmentClassMetadata.Namespace}}.{{textAlignmentClassMetadata.ClassName}}{{genericArguments}}{{textAlignmentClassMetadata.GenericConstraints}}
            => textAlignmentControl.TextCenterHorizontal{{genericTypeParameters}}().TextCenterVertical{{genericTypeParameters}}();
    }


    // The extensions in these sub-namespaces are designed to be used together with the extensions in the parent namespace.
    // Keep them in a single file for better maintainability

    namespace LeftToRight
    {
        /// <summary>
        /// Extension Methods for <see cref="ITextAlignment"/>
        /// </summary>
        {{textAlignmentClassMetadata.ClassAcessModifier}} static partial class TextAlignmentExtensions_{{textAlignmentClassMetadata.ClassName}}
        {
            /// <summary>
            /// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.Start"/>
            /// </summary>
            /// <param name="textAlignmentControl"></param>
            /// <returns><typeparamref name="TAssignable"/> with <see cref="TextAlignment.Start"/></returns>
            public static TAssignable TextLeft{{genericTypeParameters}}(this TAssignable textAlignmentControl)
				where TAssignable : {{textAlignmentClassMetadata.Namespace}}.{{textAlignmentClassMetadata.ClassName}}{{genericArguments}}{{textAlignmentClassMetadata.GenericConstraints}}
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
            public static TAssignable TextRight{{genericTypeParameters}}(this TAssignable textAlignmentControl) where TAssignable : {{textAlignmentClassMetadata.Namespace}}.{{textAlignmentClassMetadata.ClassName}}{{genericArguments}}{{textAlignmentClassMetadata.GenericConstraints}}
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
        {{textAlignmentClassMetadata.ClassAcessModifier}} static partial class TextAlignmentExtensions_{{textAlignmentClassMetadata.ClassName}}
        {
            /// <summary>
            /// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.End"/>
            /// </summary>
            /// <param name="textAlignmentControl"></param>
            /// <returns><typeparamref name="TAssignable"/> with <see cref="TextAlignment.End"/></returns>
            public static TAssignable TextLeft{{genericTypeParameters}}(this TAssignable textAlignmentControl)
				where TAssignable : {{textAlignmentClassMetadata.Namespace}}.{{textAlignmentClassMetadata.ClassName}}{{genericArguments}}{{textAlignmentClassMetadata.GenericConstraints}}
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
            public static TAssignable TextRight{{genericTypeParameters}}(this TAssignable textAlignmentControl)
				where TAssignable : {{textAlignmentClassMetadata.Namespace}}.{{textAlignmentClassMetadata.ClassName}}{{genericArguments}}{{textAlignmentClassMetadata.GenericConstraints}}
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
""";
		var source = textColorToBuilder.ToString();
		context.AddSource($"{textAlignmentClassMetadata.ClassName}TextAlignmentExtensions.g.cs", SourceText.From(source, Encoding.UTF8));
	}

	static IEnumerable<TextAlignmentClassMetadata> GetMauiInterfaceImplementors(IAssemblySymbol mauiControlsAssemblySymbolProvider, INamedTypeSymbol itextAlignmentSymbol)
	{
		return mauiControlsAssemblySymbolProvider.GlobalNamespace.GetNamedTypeSymbols().Where(x => x.AllInterfaces.Contains(itextAlignmentSymbol, SymbolEqualityComparer.Default)).Select(GenerateMetadata);
	}

	static string GetClassAccessModifier(INamedTypeSymbol namedTypeSymbol) => namedTypeSymbol.DeclaredAccessibility switch
	{
		Accessibility.Public => "public",
		Accessibility.Internal => "internal",
		_ => string.Empty
	};

	static string GetGenericTypeParametersDeclarationString(in string genericArguments)
	{
		if (string.IsNullOrWhiteSpace(genericArguments))
		{
			return "<TAssignable>";
		}

		return $"<TAssignable,{genericArguments}>";
	}

	static string GetGenericArgumentsString(in string genericArguments)
	{
		if (string.IsNullOrWhiteSpace(genericArguments))
		{
			return string.Empty;
		}

		return $"<{genericArguments}>";
	}

	static TextAlignmentClassMetadata GenerateMetadata(INamedTypeSymbol namedTypeSymbol)
	{
		var accessModifier = mauiControlsAssembly == namedTypeSymbol.ContainingNamespace.ToDisplayString()
			? "internal"
			: GetClassAccessModifier(namedTypeSymbol);

		return new(namedTypeSymbol.Name, accessModifier, namedTypeSymbol.ContainingNamespace.ToDisplayString(), namedTypeSymbol.TypeArguments.GetGenericTypeArgumentsString(), namedTypeSymbol.GetGenericTypeConstraintsAsString());
	}

	record TextAlignmentClassMetadata(string ClassName, string ClassAcessModifier, string Namespace, string GenericArguments, string GenericConstraints);
}
