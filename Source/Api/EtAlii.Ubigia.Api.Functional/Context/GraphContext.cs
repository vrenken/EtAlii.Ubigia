// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <inheritdoc />
    internal partial class GraphContext : IGraphContext
    {
        private readonly ISchemaProcessor _schemaProcessor;
        private readonly ISchemaParser _schemaParser;

        public GraphContext(
            ISchemaProcessor schemaProcessor,
            ISchemaParser schemaParser)
        {
            _schemaProcessor = schemaProcessor;
            _schemaParser = schemaParser;
        }

        /// <inheritdoc />
        SchemaParseResult IGraphContext.Parse(string text, ExecutionScope scope) => _schemaParser.Parse(text, scope);

        /// <inheritdoc />
        IAsyncEnumerable<Structure> IGraphContext.Process(Schema schema, ExecutionScope scope) => _schemaProcessor.Process(schema, scope);

        /// <inheritdoc />
        IAsyncEnumerable<Structure> IGraphContext.Process(string[] text, ExecutionScope scope)
        {
            var queryParseResult = ((IGraphContext)this).Parse(string.Join(Environment.NewLine, text), scope);

            if (queryParseResult.Errors.Any())
            {
                var firstError = queryParseResult.Errors.First();
                throw new SchemaParserException(firstError.Message, firstError.Exception);
            }

            return ((IGraphContext)this).Process(queryParseResult.Schema, scope);
        }

        /// <inheritdoc />
        IAsyncEnumerable<Structure> IGraphContext.Process(string text, ExecutionScope scope, params object[] args)
        {
            text = string.Format(text, args);
            return ((IGraphContext)this).Process(text, scope);
        }

        /// <inheritdoc />
        IAsyncEnumerable<Structure> IGraphContext.Process(string text, ExecutionScope scope)
        {
            var queryParseResult = ((IGraphContext)this).Parse(text, scope);

            if (queryParseResult.Errors.Any())
            {
                var firstError = queryParseResult.Errors.First();
                throw new SchemaParserException(firstError.Message, firstError.Exception);
            }

            return ((IGraphContext)this).Process(queryParseResult.Schema, scope);
        }
    }
}
