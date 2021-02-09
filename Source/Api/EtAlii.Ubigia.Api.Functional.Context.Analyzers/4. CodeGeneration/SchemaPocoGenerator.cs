namespace EtAlii.Ubigia.Api.Functional.Context.Analyzers
{
    using System;
    using System.CodeDom.Compiler;
    using System.IO;
    using System.Linq;
    using System.Text;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Text;
    using Serilog;
    using Serilog.Core;
    using ValueType = EtAlii.Ubigia.Api.Functional.Context.ValueType;

    [Generator]
    public class SchemaPocoGenerator : ISourceGenerator
    {
        private static readonly DiagnosticDescriptor _ubigiaPocoMustBePartialRule = new
        (
            id: "UB1001",
            title: "non-partial Ubigia poco class",
            messageFormat: "all Ubigia poco classes should be partial",
            category: "Code-Gen",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );

        private static readonly DiagnosticDescriptor _ubigiaInvalidGclSchemaRule = new
        (
            id: "UB1002",
            title: "GCL schema is invalid",
            messageFormat: "GCL schema is invalid: {0}",
            category: "Code-Gen",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );


        private ISchemaParser _schemaParser;
        private ILogger _logger;
        private Logger _rootLogger;

        public void Initialize(GeneratorInitializationContext context)
        {
        }

        private void SetupLogging()
        {
            var loggerConfiguration = new LoggerConfiguration()
                //.WriteTo.Seq("http://vrenken.duckdns.org:5341", period: TimeSpan.Zero);
                .WriteTo.Seq("http://192.168.1.130:5341", period: TimeSpan.Zero);

            _rootLogger = loggerConfiguration
                .CreateLogger();
            _logger = _rootLogger
                .ForContext<SchemaPocoGenerator>()
                .ForContext("CodeGeneration", ShortGuid.New());

            _logger.Information("Setting up SchemaPocoGenerator");

            AppDomain.CurrentDomain.UnhandledException += (_, e) => _logger.Fatal(e.ExceptionObject as Exception, "Fatal exception while creating SchemaPocoGenerator");
        }

        private void SetupParser()
        {
            _logger.Information("Setting up schema parser");
            try
            {
                var traversalConfiguration = new TraversalParserConfiguration()
                    .UseAntlr();
                var configuration = new SchemaParserConfiguration()
                    .Use(traversalConfiguration);
                _schemaParser = new AntlrSchemaParserFactory()
                    .Create(configuration);
            }
            catch (Exception e)
            {
                _logger.Fatal(e, "Unable to setup schema parser");
            }
        }

        private bool TryParseSchema(GeneratorExecutionContext context, AdditionalText file, out Schema schema)
        {
            try
            {
                var schemaText = file.GetText()?.ToString();
                _logger.ForContext("SchemaText", schemaText, true).Information("Parsing schema");
                var result = _schemaParser.Parse(schemaText);
                if (result.Errors.Any())
                {
                    _logger
                        .ForContext("SchemaParseErrors", result.Errors, true)
                        .Information("Parsing schema resulted in errors");

                    foreach (var error in result.Errors)
                    {
                        var linePositionStart = new LinePosition(error.Line, error.Column);
                        var linePositionEnd = new LinePosition(error.Line, error.Column);
                        var linePositionSpan = new LinePositionSpan(linePositionStart, linePositionEnd);
                        var textSpan = new TextSpan(error.Column, 0);
                        var location = Location.Create(file.Path, textSpan, linePositionSpan);
                        var diagnostic = Diagnostic.Create(_ubigiaInvalidGclSchemaRule, location, error.Message, error.Exception.StackTrace);
                        context.ReportDiagnostic(diagnostic);
                    }
                }
                else
                {
                    schema = result.Schema;
                    _logger
                        .ForContext("Schema", schema, true)
                        .Information("Parsed schema");
                    return true;

                }
            }
            catch (Exception e)
            {
                _logger.Fatal(e, "Unable to parse schema");
            }

            schema = null;
            return false;
        }
        public void Execute(GeneratorExecutionContext context)
        {
            SetupLogging();
            _logger.Information("Executing");

            SetupParser();

            var additionalFiles = context.AdditionalFiles
                .Where(f => Path.GetExtension(f.Path) == ".gcl")
                .ToArray();

            foreach (var file in additionalFiles)
            {
                _logger.Information("Processing file: {FileName}", file.Path);
                if (TryParseSchema(context, file, out var schema))
                {
                    var fileName = Path.GetFileNameWithoutExtension(file.Path);
                    WriteNamespace(context, fileName, schema);
                }
            }

            _rootLogger.Dispose();
            _rootLogger = null;
            _logger = null;
        }

        private void WriteNamespace(GeneratorExecutionContext context, string fileName, Schema schema)
        {
            using var sourceWriter = new StringWriter();
            using var writer = new IndentedTextWriter(sourceWriter, "\t") {Indent = 0};

            var @namespace = schema.Namespace ?? "EtAlii.Ubigia";

            _logger.Information("Writing namespace: {Namespace}", @namespace);
            writer.WriteLine($"namespace {@namespace}");
            writer.WriteLine("{");
            writer.Indent += 1;
            WriteClass(writer, schema.Structure);
            writer.Indent -= 1;
            writer.WriteLine("}");

            var sourceText = SourceText.From(sourceWriter.ToString(), Encoding.UTF8);
            context.AddSource($"{fileName}.Gcl.cs", sourceText);
        }

        private void WriteClass(IndentedTextWriter writer, StructureFragment structureFragment)
        {
            var className = structureFragment.Name;
            _logger
                .ForContext("StructureFragment", structureFragment, true)
                .Information("Writing class: {Class}", className);
            writer.WriteLine($"public partial class {className}");
            writer.WriteLine("{");
            writer.Indent += 1;

            foreach (var valueFragment in structureFragment.Values)
            {
                WriteProperty(writer, valueFragment);
            }

            writer.Indent -= 1;
            writer.WriteLine("}");
        }

        private void WriteProperty(IndentedTextWriter writer, ValueFragment valueFragment)
        {
            var propertyName = valueFragment.Name;

            var prefix = valueFragment.Prefix;
            var annotation = valueFragment.Annotation;

            _logger
                .ForContext("ValueFragment", valueFragment, true)
                .ForContext("Annotation", annotation?.ToString())
                .ForContext("Prefix", prefix.ToString())
                .Information("Writing property: {Property}", propertyName);

            var requirementAsString = prefix.Requirement switch
            {
                Requirement.None => "",
                Requirement.Mandatory => "!",
                Requirement.Optional => "?",
                _ => throw new NotSupportedException()
            };
            var typeAsString = prefix.ValueType switch
            {
                ValueType.Object => "object",
                ValueType.String => "string",
                ValueType.Bool => "bool",
                ValueType.Float => "float",
                ValueType.Int => "int",
                ValueType.DateTime => "System.DateTime",
                _ => throw new NotSupportedException()
            };
            var valueAsString = prefix.ValueType switch
            {
                ValueType.Object => "new object()",
                ValueType.String => $"\"{annotation?.ToString()?.Replace("\\", "\\\\") ?? ""}\"",
                ValueType.Bool => "true",
                ValueType.Float => "42.42f",
                ValueType.Int => "42",
                ValueType.DateTime => "System.DateTime.Now",
                _ => throw new NotSupportedException()
            };
            writer.WriteLine($"public {typeAsString} {propertyName} {{get;}} = {valueAsString};");
        }
    }
}
