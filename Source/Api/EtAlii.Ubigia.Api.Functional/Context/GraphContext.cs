// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

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
        SchemaParseResult IGraphContext.Parse(string text)
        {
            var parserOptions = _traversalContext.ParserOptionsProvider();
                //.Use(_logicalContext.Configuration)
                //.Use(_diagnostics)
            var parser = _schemaParserFactory.Create(parserOptions);
            return parser.Parse(text);
        }

        /// <inheritdoc />
        IAsyncEnumerable<Structure> IGraphContext.Process(Schema schema, ISchemaScope scope)
        {
            var options = new SchemaProcessorOptions()
                .Use(scope)
                .Use(_traversalContext);
            var processor = _schemaProcessorFactory.Create(options);
            return processor.Process(schema);
        }

        /// <inheritdoc />
        IAsyncEnumerable<Structure> IGraphContext.Process(string[] text, ISchemaScope scope)
        {
            var queryParseResult = ((IGraphContext)this).Parse(string.Join(Environment.NewLine, text));

            if (queryParseResult.Errors.Any())
            {
                var firstError = queryParseResult.Errors.First();
                throw new SchemaParserException(firstError.Message, firstError.Exception);
            }

            return ((IGraphContext)this).Process(queryParseResult.Schema, scope);
        }

        /// <inheritdoc />
        IAsyncEnumerable<Structure> IGraphContext.Process(string text, params object[] args)
        {
            text = string.Format(text, args);
            return ((IGraphContext)this).Process(text);
        }

        /// <inheritdoc />
        IAsyncEnumerable<Structure> IGraphContext.Process(string[] text)
        {
            return ((IGraphContext)this).Process(string.Join(Environment.NewLine, text));
        }

        /// <inheritdoc />
        IAsyncEnumerable<Structure> IGraphContext.Process(string text)
        {
            var scope = new SchemaScope();
            return ((IGraphContext)this).Process(text, scope);
        }

        /// <inheritdoc />
        IAsyncEnumerable<Structure> IGraphContext.Process(string text, ISchemaScope scope)
        {
            var queryParseResult = ((IGraphContext)this).Parse(text);

            if (queryParseResult.Errors.Any())
            {
                var firstError = queryParseResult.Errors.First();
                throw new SchemaParserException(firstError.Message, firstError.Exception);
            }

            return ((IGraphContext)this).Process(queryParseResult.Schema, scope);
        }
    }
}
