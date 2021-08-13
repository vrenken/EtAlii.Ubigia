// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <inheritdoc />
    internal partial class GraphContext : IGraphContext
    {
        private readonly FunctionalOptions _options;
        private readonly ISchemaProcessorFactory _schemaProcessorFactory;
        private readonly ISchemaParserFactory _schemaParserFactory;

        public GraphContext(
            FunctionalOptions options,
            ISchemaProcessorFactory schemaProcessorFactory,
            ISchemaParserFactory schemaParserFactory)
        {
            _options = options;
            _schemaProcessorFactory = schemaProcessorFactory;
            _schemaParserFactory = schemaParserFactory;
        }

        /// <inheritdoc />
        SchemaParseResult IGraphContext.Parse(string text)
        {
            var parser = _schemaParserFactory.Create(_options);
            return parser.Parse(text);
        }

        /// <inheritdoc />
        IAsyncEnumerable<Structure> IGraphContext.Process(Schema schema, ISchemaScope scope)
        {
            var options = _options.CreateScope(scope);
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
