namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Collections.Generic;
    using GraphQL;
    using GraphQL.Instrumentation;
    using Newtonsoft.Json;
    using GraphQL.Language.AST;

    [JsonConverter(typeof(ExecutionResultJsonConverter))]
    public class QueryExecutionResult
    {
        // TODO: Remove everything we don't need. This will be most of the GraphQL specific typed properties.
        public object Data { get; set; }

        [JsonIgnore]
        public string DataAsString { get; }
        
        public ExecutionErrors Errors { get; }

        public string Query { get; }

        public GraphQL.Language.AST.Document Document { get; }

        public Operation Operation { get; }

        public PerfRecord[] Perf { get; }

        public bool ExposeExceptions { get; }

        public Dictionary<string, object> Extensions { get; }

        public QueryExecutionResult(object data, string dataAsString)
        {
            Data = data;
        }

        
        public QueryExecutionResult(ExecutionResult result, string dataAsString)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));
            this.Data = result.Data;
            this.Errors = result.Errors;
            this.Query = result.Query;
            this.Operation = result.Operation;
            this.Document = result.Document;
            this.Perf = result.Perf;
            this.ExposeExceptions = result.ExposeExceptions;
            this.Extensions = result.Extensions;
        }

        public static ExecutionResult ToGraphQlExecutionResult(QueryExecutionResult queryExecutionResult)
        {
            return new ExecutionResult
            {
                Data = queryExecutionResult.Data,
                Errors = queryExecutionResult.Errors,
                Query = queryExecutionResult.Query,
                Operation = queryExecutionResult.Operation,
                Document = queryExecutionResult.Document,
                Perf = queryExecutionResult.Perf,
                ExposeExceptions = queryExecutionResult.ExposeExceptions,
                Extensions = queryExecutionResult.Extensions,
            };
        }

    }
}
