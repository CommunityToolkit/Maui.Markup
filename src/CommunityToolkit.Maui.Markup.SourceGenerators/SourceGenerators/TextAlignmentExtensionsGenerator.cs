﻿using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
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

	static List<INamedTypeSymbol> mauiTypes = new();

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		//this didn't work on mac
		System.Diagnostics.Debugger.Launch();
		
		// Get All Classes in User Library
		var userGeneratedClassesProvider = context.SyntaxProvider.CreateSyntaxProvider(
			static (syntaxNode, cancellationToken) => syntaxNode is ClassDeclarationSyntax { BaseList: not null },
			static (context, cancellationToken) =>
			{
				Compilation compilation = context.SemanticModel.Compilation;

				INamedTypeSymbol iTextAlignmentInterfaceSymbol = compilation.GetTypeByMetadataName(iTextAlignmentInterface) ?? throw new Exception("There's no .NET MAUI referenced in the project.");
				INamedTypeSymbol? classSymbol = context.SemanticModel.GetDeclaredSymbol((ClassDeclarationSyntax)context.Node, cancellationToken);

				if (classSymbol is null || classSymbol.DeclaringSyntaxReferences[0].GetSyntax(cancellationToken) != context.Node)
				{
					// In case of multiple partial declarations, we want to run only once.
					// So we run only for the first syntax reference.
					return null;
				}

				//Find maui types just once, since it can't change during each build
				// could not debug to make sure this is just called once
				// sadly work with SG on mac is (still) terrible
				if (mauiTypes.Count is 0)
				{
					IAssemblySymbol mauiAssembly = compilation.SourceModule.ReferencedAssemblySymbols.Single(q => q.Name == mauiControlsAssembly);
					mauiTypes.AddRange(ThisGetMauiInterfaceImplementors(mauiAssembly, iTextAlignmentInterfaceSymbol));
					
					static IEnumerable<INamedTypeSymbol> ThisGetMauiInterfaceImplementors(IAssemblySymbol mauiControlsAssemblySymbolProvider, INamedTypeSymbol itextAlignmentSymbol)
					{
						return mauiControlsAssemblySymbolProvider.GlobalNamespace.GetNamedTypeSymbols().Where(x =>
							x.AllInterfaces.Contains(itextAlignmentSymbol, SymbolEqualityComparer.Default));
					}
				}
				
				while (classSymbol is not null)
				{
					if (classSymbol.ContainingAssembly.Name == mauiControlsAssembly || mauiTypes.ContainsSymbolBaseType(classSymbol))
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
			}).Where(static m => m is not null);

		// Get Microsoft.Maui.Controls Symbols that implements the desired interfaces
		var mauiControlsAssemblySymbolProvider = context.CompilationProvider.Select(
			static (compilation, token) =>
			{
				INamedTypeSymbol iTextAlignmentInterfaceSymbol = compilation.GetTypeByMetadataName(iTextAlignmentInterface) ?? throw new Exception("There's no .NET MAUI referenced in the project.");
				IAssemblySymbol mauiAssembly = compilation.SourceModule.ReferencedAssemblySymbols.Single(q => q.Name == mauiControlsAssembly);

				return EquatableArray.AsEquatableArray(GetMauiInterfaceImplementors(mauiAssembly, iTextAlignmentInterfaceSymbol).ToImmutableArray());
			});
		
		
		// Here we Collect all the Classes candidates from the first pipeline
		// Then we merge them with the Maui.Controls that implements the desired interfaces
		// Then we make sure they are unique and the user control doesn't inherit from any Maui control that implements the desired interface already
		// Then we transform the ISymbol to be a type that we can compare and preserve the Incremental behavior of this Source Generator
		context.RegisterSourceOutput(userGeneratedClassesProvider, Execute);
		context.RegisterSourceOutput(mauiControlsAssemblySymbolProvider, ExecuteArray);
	}

	static void ExecuteArray(SourceProductionContext context, EquatableArray<TextAlignmentClassMetadata> metadataArray)
	{
		foreach (var metadata in metadataArray.AsImmutableArray())
		{
			Execute(context, metadata);
		}
	}

	static void Execute(SourceProductionContext context, [NotNull] TextAlignmentClassMetadata? textAlignmentClassMetadata)
	{
		if (textAlignmentClassMetadata is null)
		{
			throw new ArgumentNullException(nameof(textAlignmentClassMetadata));
		}

		var genericTypeParameters = GetGenericTypeParametersDeclarationString(textAlignmentClassMetadata.GenericArguments);
		var genericArguments = GetGenericArgumentsString(textAlignmentClassMetadata.GenericArguments);
		var source = $$"""
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