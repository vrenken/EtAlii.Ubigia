namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.CodeAnalysis;
    using System.Linq;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Text;

    [Generator]
    public class SchemaPocoGenerator : ISourceGenerator
    {
        public const string UbigiaPocoMustBePartialDiagnosticId = "UB0001";

        private static readonly DiagnosticDescriptor _ubigiaPocoMustBePartialRule = new(UbigiaPocoMustBePartialDiagnosticId, "non-partial Ubigia poco class", "all Ubigia poco classes should be partial", "code-generation", DiagnosticSeverity.Error, true);

        public void Initialize(GeneratorInitializationContext context)
        {

            // Register a factory that can create our custom syntax receiver
            context.RegisterForSyntaxNotifications(() => new PocoSyntaxReceiver());
        }

        private (INamedTypeSymbol Symbol, ClassDeclarationSyntax Declaration)[] FindPocoClasses(GeneratorExecutionContext context, PocoSyntaxReceiver syntaxReceiver)
        {
            var compilation = context.Compilation;

            var attributeSymbol = compilation.GetTypeByMetadataName(typeof(UbigiaPocoAttribute).FullName!);

            var pocoClassSymbols = new List<(INamedTypeSymbol Symbol, ClassDeclarationSyntax Declaration)>();
            foreach (var candidateClass in syntaxReceiver.CandidateClasses)
            {
                var classSymbol = compilation
                        .GetSemanticModel(candidateClass.SyntaxTree)
                        .GetDeclaredSymbol(candidateClass);

                // todo, weird that ad.AttributeClass.Equals(attributeSymbol, SymbolEqualityComparer.Default) always returns null
                // see https://github.com/dotnet/roslyn/issues/30248 maybe?
                //if(classSymbol.Name == "UserPoco")
                if (classSymbol!.GetAttributes().Any(ad => ad.AttributeClass!.Name == attributeSymbol!.Name || ad.AttributeClass.Name + "Attribute" == attributeSymbol.Name))
                {
                    pocoClassSymbols.Add(((INamedTypeSymbol)classSymbol, candidateClass));
                }
            }

            return pocoClassSymbols.ToArray();
        }

        public void Execute(GeneratorExecutionContext context)
        {
            // the generator infrastructure will create a receiver and populate it
            // we can retrieve the populated instance via the context
            var syntaxReceiver = (PocoSyntaxReceiver)context.SyntaxReceiver;

            var relevantEntities = FindPocoClasses(context, syntaxReceiver);

            foreach (var relevantEntity in relevantEntities)
            {
                var isPartial = relevantEntity.Declaration.Modifiers.Any(token => token.Kind() == SyntaxKind.PartialKeyword);
                if (!isPartial)
                {
                    var location = Location.Create(relevantEntity.Declaration.SyntaxTree, relevantEntity.Declaration.Span);
                    var diagnostic = Diagnostic.Create(_ubigiaPocoMustBePartialRule, location);
                    context.ReportDiagnostic(diagnostic);
                    continue;
                }
                var symbol = relevantEntity.Symbol;

                // add the generated implementation to the compilation
                var sourceText = SourceText.From($@"
                namespace {symbol.ContainingNamespace.ToDisplayString()}
                {{
                    public partial class {symbol.Name}
                    {{
                        public void GeneratedMethod()
                        {{
                            // generated code
                        }}
                    }}
                }}", Encoding.UTF8);
                context.AddSource($"{symbol.Name}.Gcl.Poco.cs", sourceText);
            }
        }

    }
}
