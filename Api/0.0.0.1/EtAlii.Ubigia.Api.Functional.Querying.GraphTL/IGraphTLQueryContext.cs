﻿namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Threading.Tasks;

    /// <summary>
    /// A <see cref="GraphTLQueryContext"/> can be used to execute GTL queries on a Ubigia space. 
    /// </summary>
    public interface IGraphTLQueryContext
    {
        /// <summary>
        /// Parse the specified text into a GTL query. 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        QueryParseResult Parse(string text);

        // TODO: Add non-async methods.
        //void Process(Query query, IQueryScope scope)
        //void Process(Query query, IQueryScope scope, IProgress<QueryProcessingProgress> progress)
        //IEnumerable<object> Process(string text, IProgress<QueryProcessingProgress> progress)
        //IEnumerable<object> Process(string text, params object[] args)
        //IEnumerable<object> Process(string text, IProgress<QueryProcessingProgress> progress, params object[] args)

        // TODO: Rename to ProcessAsync.
        Task<QueryProcessingResult> Process(Query query, IQueryScope scope);
        //Task Process(Query query, IQueryScope scope, IProgress<QueryProcessingProgress> progress)
        //Task<IEnumerable<object>> Process(string text, IProgress<QueryProcessingProgress> progress)
        Task<QueryProcessingResult> Process(string text, params object[] args);
        Task<QueryProcessingResult> Process(string[] text);
        Task<QueryProcessingResult> Process(string[] text, IQueryScope scope);

        Task<QueryProcessingResult> Process(string text);
        //Task<IEnumerable<object>> Process(string text, IProgress<QueryProcessingProgress> progress, params object[] args)

    }
}