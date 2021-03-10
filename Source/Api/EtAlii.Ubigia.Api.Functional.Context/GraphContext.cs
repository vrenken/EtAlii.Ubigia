namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    /// <inheritdoc />
    internal partial class GraphContext : IGraphContext
    {
        private readonly ISchemaProcessorFactory _schemaProcessorFactory;
        private readonly ISchemaParserFactory _schemaParserFactory;
        private readonly ITraversalContext _traversalContext;

        public GraphContext(
            ISchemaProcessorFactory schemaProcessorFactory,
            ISchemaParserFactory schemaParserFactory,
            ITraversalContext traversalContext)
        {
            _schemaProcessorFactory = schemaProcessorFactory;
            _schemaParserFactory = schemaParserFactory;
            _traversalContext = traversalContext;
        }

        /// <inheritdoc />
        public SchemaParseResult Parse(string text)
        {
            var parserConfiguration = _traversalContext.ParserConfigurationProvider();
            var queryParserConfiguration = new SchemaParserConfiguration()
                .Use(parserConfiguration);
                //.Use(_logicalContext.Configuration)
                //.Use(_diagnostics)
            var parser = _schemaParserFactory.Create(queryParserConfiguration);
            return parser.Parse(text);
        }

        /// <inheritdoc />
        public IAsyncEnumerable<Structure> Process(Schema schema, ISchemaScope scope)
        {
            var configuration = new SchemaProcessorConfiguration()
                .Use(scope)
                .Use(_traversalContext);
            var processor = _schemaProcessorFactory.Create(configuration);
            return processor.Process(schema);
        }

        /// <inheritdoc />
        public IAsyncEnumerable<Structure> Process(string[] text, ISchemaScope scope)
        {
            var queryParseResult = Parse(string.Join(Environment.NewLine, text));

            if (queryParseResult.Errors.Any())
            {
                var firstError = queryParseResult.Errors.First();
                throw new SchemaParserException(firstError.Message, firstError.Exception);
            }

            return Process(queryParseResult.Schema, scope);
        }

        /// <inheritdoc />
        public IAsyncEnumerable<Structure> Process(string text, params object[] args)
        {
            text = string.Format(text, args);
            return Process(text);
        }

        /// <inheritdoc />
        public IAsyncEnumerable<Structure> Process(string[] text)
        {
            return Process(string.Join(Environment.NewLine, text));
        }

        /// <inheritdoc />
        public IAsyncEnumerable<Structure> Process(string text)
        {
            var scope = new SchemaScope();
            return Process(text, scope);
        }

        /// <inheritdoc />
        public IAsyncEnumerable<Structure> Process(string text, ISchemaScope scope)
        {
            var queryParseResult = Parse(text);

            if (queryParseResult.Errors.Any())
            {
                var firstError = queryParseResult.Errors.First();
                throw new SchemaParserException(firstError.Message, firstError.Exception);
            }

            return Process(queryParseResult.Schema, scope);
        }
    }
}
