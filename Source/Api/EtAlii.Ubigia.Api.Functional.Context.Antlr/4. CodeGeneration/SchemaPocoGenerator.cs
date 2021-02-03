namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Text;

    [Generator]
    public partial class SchemaPocoGenerator : ISourceGenerator
    {
        public const string UbigiaPocoMustBePartialDiagnosticId = "UB1001";

        private static readonly DiagnosticDescriptor _ubigiaPocoMustBePartialRule = new(UbigiaPocoMustBePartialDiagnosticId, "non-partial Ubigia poco class", "all Ubigia poco classes should be partial", "Code-Gen", DiagnosticSeverity.Error, true);

        private readonly ISchemaParser _schemaParser;

        public SchemaPocoGenerator()
        {
            var configuration = new SchemaParserConfiguration().
                Use(new TraversalParserConfiguration().UseAntlr());
            _schemaParser = new AntlrSchemaParserFactory().Create(configuration);
        }
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var additionalFiles = context.AdditionalFiles
                .Where(f => Path.GetExtension(f.Path) == ".gcl")
                .ToArray();
            //
            // var gclFiles = additionalFiles
            //     .Select(f => f.Path)
            //     .ToArray();

            //foreach (var gclFile in gclFiles)
            //foreach(var additionalFile in additionalFiles)

            Debug(context, "AdditionalFiles", additionalFiles.Select(f => f.Path));

            try
            {

                for(var i = 0; i< additionalFiles.Length; i++)
                {
                    var additionalFile = additionalFiles[i];

                    Debug(context, "AdditionalFile", additionalFile.Path, i);
                    var fileName = Path.GetFileNameWithoutExtension(additionalFile.Path);
                    var text = additionalFile.GetText()?.ToString();
                    if (text != null)
                    {
                        Debug(context, "AdditionalFileText", text, i);

                        var schema = _schemaParser.Parse(text);
                        var rootName = schema.Schema.Structure.Name;

                        var sourceText = SourceText.From($@"
namespace EtAlii.Ubigia.Api.Functional.Context.Tests \\{{symbol.ContainingNamespace.ToDisplayString()}}
{{
    public partial class {rootName} // {{symbol.Name}}
    {{
        public void GeneratedMethod()
        {{
            // generated code
        }}
    }}
}}", Encoding.UTF8);
                        context.AddSource($"{fileName}.Gcl.cs", sourceText);
                    }
                }
            }
            catch (Exception e)
            {
                Debug(context, "Exception3", e.Message);
            }
        }
    }
}
