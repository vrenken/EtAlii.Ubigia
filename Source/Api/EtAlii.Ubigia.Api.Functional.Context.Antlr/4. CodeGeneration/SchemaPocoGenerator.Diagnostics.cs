namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Text;

    public partial class SchemaPocoGenerator
    {
        private void Debug(GeneratorExecutionContext context, string name, IEnumerable<string> items, int index = 0)
        {
            var sourceText = SourceText.From($@"namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{{
    public partial class DiagnosticsOutput
    {{
        public const string {name}_{index} = @""{string.Join(Environment.NewLine, items)}"";
    }}
}}", Encoding.UTF8);
            context.AddSource($"DiagnosticsOutput_{name}_{index}.Log.cs", sourceText);
        }

        private void Debug(GeneratorExecutionContext context, string name, string item, int index = 0)
        {
            var sourceText = SourceText.From($@"namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{{
    public partial class DiagnosticsOutput
    {{
        public const string {name}_{index} = @""{item ?? "NULL"}"";
    }}
}}", Encoding.UTF8);
            context.AddSource($"DiagnosticsOutput_{name}_{index}.Log.cs", sourceText);
        }

        public void ExecuteDiagnostics(GeneratorExecutionContext context)
        {
            //var gclFiles = context.GetMSBuildItems("GclFile");
            var additionalFiles = context.AdditionalFiles
                .Where(f => Path.GetExtension(f.Path) == ".gcl")
                .ToArray();

            var gclFiles = additionalFiles
                .Select(f => f.Path)
                .ToArray();

            var gclSettings = additionalFiles
                .Select(f => context.AnalyzerConfigOptions.GetOptions(f))
                .Select(f =>
                {

                    if (f.TryGetValue("SourceItemGroup", out var sourceItemGroup))
                    {
                        return (string)sourceItemGroup;
                    }

                    var members = f.GetType().GetMember("_backing", BindingFlags.NonPublic | BindingFlags.Instance);
                    //return members.Any() ? "true" : "false";
                    var memberInfo = members.Single();
                    // return memberInfo != null ? "true" : "false";
                    var o = ((FieldInfo)memberInfo).GetValue(f);
                    //return o.ToString();
                    var dictionary = (ImmutableDictionary<string, string>)o;
                    return dictionary.Count.ToString();
                    // var items = dictionary.Select(kvp => $"{kvp.Key}={kvp.Value}");
                    // return string.Join(Environment.NewLine, items);
                })
                .ToArray();

            var sourceText = SourceText.From($@"namespace DebugTest
{{
    public class Output
    {{
        public const string GclFiles = @""{string.Join(Environment.NewLine, gclFiles)}"";
        public const string GclSettings = @""{string.Join(Environment.NewLine, gclSettings)}"";
    }}
}}", Encoding.UTF8);
            context.AddSource($"LogFile.cs", sourceText);


            // foreach (var gclFile in gclFiles)
            // {
            //     var fileName = Path.GetFileName(gclFile);
            //     var name = Path.GetFileNameWithoutExtension(gclFile);
            //     var sourceText = SourceText.From($@"
            //     namespace EtAlii.Ubigia.Api.Functional.Context.Tests \\{{symbol.ContainingNamespace.ToDisplayString()}}
            //     {{
            //         public partial class {name} // {{symbol.Name}}
            //         {{
            //             public void GeneratedMethod()
            //             {{
            //                 // generated code
            //             }}
            //         }}
            //     }}", Encoding.UTF8);
            //     context.AddSource($"{name}.Gcl.cs", sourceText);
            // }

        //     // the generator infrastructure will create a receiver and populate it
        //     // we can retrieve the populated instance via the context
        //     var syntaxReceiver = (PocoSyntaxReceiver)context.SyntaxReceiver;
        //
        //     var relevantEntities = FindPocoClasses(context, syntaxReceiver);
        //
        //     foreach (var relevantEntity in relevantEntities)
        //     {
        //         var isPartial = relevantEntity.Declaration.Modifiers.Any(token => token.Kind() == SyntaxKind.PartialKeyword);
        //         if (!isPartial)
        //         {
        //             var location = Location.Create(relevantEntity.Declaration.SyntaxTree, relevantEntity.Declaration.Span);
        //             var diagnostic = Diagnostic.Create(_ubigiaPocoMustBePartialRule, location);
        //             context.ReportDiagnostic(diagnostic);
        //             continue;
        //         }
        //         var symbol = relevantEntity.Symbol;
        //
        //         // add the generated implementation to the compilation
        //         var sourceText = SourceText.From($@"
        //         namespace {symbol.ContainingNamespace.ToDisplayString()}
        //         {{
        //             public partial class {symbol.Name}
        //             {{
        //                 public void GeneratedMethod()
        //                 {{
        //                     // generated code
        //                 }}
        //             }}
        //         }}", Encoding.UTF8);
        //         context.AddSource($"{symbol.Name}.Gcl.Poco.cs", sourceText);
        //     }
        }
    }
}
