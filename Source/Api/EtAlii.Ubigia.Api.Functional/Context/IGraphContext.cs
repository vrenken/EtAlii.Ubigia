// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// A <see cref="GraphContext"/> can be used to execute GCL queries on a Ubigia space.
    /// </summary>
    public interface IGraphContext
    {
        /// <summary>
        /// Parse the specified text into a GCL query.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        internal SchemaParseResult Parse(string text);

        internal IAsyncEnumerable<Structure> Process(Schema schema, ISchemaScope scope);
        internal IAsyncEnumerable<Structure> Process(string text, params object[] args);
        internal IAsyncEnumerable<Structure> Process(string text, ISchemaScope scope);
        internal IAsyncEnumerable<Structure> Process(string[] text);
        internal IAsyncEnumerable<Structure> Process(string[] text, ISchemaScope scope);
        internal IAsyncEnumerable<Structure> Process(string text);

        Task<TResult> ProcessSingle<TResult>(string text, IResultMapper<TResult> resultMapper, ISchemaScope scope);
        IAsyncEnumerable<TResult> ProcessMultiple<TResult>(string text, IResultMapper<TResult> resultMapper, ISchemaScope scope);
    }
}
