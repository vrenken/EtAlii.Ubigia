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
        SchemaParseResult Parse(string text);

        // TODO: Add non-async methods.
        //void Process(Query query, IQueryScope scope)
        //void Process(Query query, IQueryScope scope, IProgress<QueryProcessingProgress> progress)
        //IEnumerable<object> Process(string text, IProgress<QueryProcessingProgress> progress)
        //IEnumerable<object> Process(string text, params object[] args)
        //IEnumerable<object> Process(string text, IProgress<QueryProcessingProgress> progress, params object[] args)

        // TODO: Rename to ProcessAsync.
        IAsyncEnumerable<Structure> Process(Schema schema, ISchemaScope scope);
        //Task Process(Query query, IQueryScope scope, IProgress<QueryProcessingProgress> progress)
        //Task<IEnumerable<object>> Process(string text, IProgress<QueryProcessingProgress> progress)
        IAsyncEnumerable<Structure> Process(string text, params object[] args);
        IAsyncEnumerable<Structure> Process(string[] text);
        IAsyncEnumerable<Structure> Process(string[] text, ISchemaScope scope);

        IAsyncEnumerable<Structure> Process(string text);
        //Task<IEnumerable<object>> Process(string text, IProgress<QueryProcessingProgress> progress, params object[] args)

        Task<TResult> ProcessSingle<TResult>(string text, IResultMapper<TResult> resultMapper);
        IAsyncEnumerable<TResult> ProcessMultiple<TResult>(string text, IResultMapper<TResult> resultMapper);
    }
}
