// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using System.CodeDom.Compiler;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using EtAlii.Ubigia.Api.Functional.Antlr.Context;
    using EtAlii.Ubigia.Api.Functional.Antlr.Traversal;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Text;
    using Serilog;
    using Serilog.Core;

    [Generator]
    public class SchemaPocoGenerator : ISourceGenerator
    {
        private static readonly DiagnosticDescriptor _ubigiaInvalidGclSchemaRule = new
        (
            id: "UB1001",
            title: "GCL schema is invalid",
            messageFormat: "GCL schema is invalid: {0}",
            category: "Code-Gen",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );

        private ISchemaParser _schemaParser;
        private ILogger _logger;
        private Logger _rootLogger;

        private readonly INamespaceWriter _namespaceWriter;
        private readonly IHeaderWriter _headerWriter;

        public SchemaPocoGenerator()
        {
            var resultMapperWriter = new ResultMapperWriter();
            var annotationCommentWriter = new AnnotationCommentWriter();
            var propertyWriter = new ValuePropertyWriter(annotationCommentWriter);
            var classWriter = new ClassWriter(propertyWriter, annotationCommentWriter, resultMapperWriter);
            var variableFinder = new VariableFinder();
            var graphContextExtensionWriter = new GraphContextExtensionWriter(variableFinder);
            _namespaceWriter = new NamespaceWriter(classWriter, graphContextExtensionWriter);
            _headerWriter = new HeaderWriter();
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }

        private void SetupLogging()
        {
            var loggerConfiguration = new LoggerConfiguration();

            var executingAssemblyName = Assembly.GetCallingAssembly().GetName();

            loggerConfiguration.MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                // These ones do not give elegant results during unit tests.
                // .Enrich.WithAssemblyName()
                // .Enrich.WithAssemblyVersion()
                // Let's do it ourselves.
                .Enrich.WithProperty("RootAssemblyName", executingAssemblyName.Name)
                .Enrich.WithProperty("RootAssemblyVersion", executingAssemblyName.Version)
                .Enrich.WithProperty("UniqueProcessId", Guid.NewGuid()) // An int process ID is not enough
                .WriteTo.Seq("http://seq.avalon:5341");

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

                    using var sourceWriter = new StringWriter();
                    using var writer = new IndentedTextWriter(sourceWriter, "\t") {Indent = 0};

                    _headerWriter.Write(writer, fileName);
                    _namespaceWriter.Write(_logger, writer, schema);

                    var targetFileName = $"{fileName}.Gcl.Generated.cs";
                    var sourceTextString = sourceWriter.ToString();
                    _logger
                        .ForContext("SourceText", sourceTextString)
                        .Information("Writing source to file: {FileName}", targetFileName);
                    context.AddSource(targetFileName, sourceTextString);
                }
            }

            _rootLogger.Dispose();
            _rootLogger = null;
            _logger = null;
        }

    }
}
