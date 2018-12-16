namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Collections.Generic;
    using GraphQL;
    using GraphQL.Instrumentation;
    using Newtonsoft.Json;
    using GraphQL.Language.AST;

    [JsonConverter(typeof(ExecutionResultJsonConverter))]
    public class QueryProcessingResult
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

        public QueryProcessingResult(object data, string dataAsString)
        {
            Data = data;
            DataAsString = dataAsString;
        }

        
        public QueryProcessingResult(ExecutionResult result, string dataAsString)
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
            
            DataAsString = dataAsString;
        }

        public static ExecutionResult ToGraphQlExecutionResult(QueryProcessingResult queryProcessingResult)
        {
            return new ExecutionResult
            {
                Data = queryProcessingResult.Data,
                Errors = queryProcessingResult.Errors,
                Query = queryProcessingResult.Query,
                Operation = queryProcessingResult.Operation,
                Document = queryProcessingResult.Document,
                Perf = queryProcessingResult.Perf,
                ExposeExceptions = queryProcessingResult.ExposeExceptions,
                Extensions = queryProcessingResult.Extensions,
            };
        }

    }
}
