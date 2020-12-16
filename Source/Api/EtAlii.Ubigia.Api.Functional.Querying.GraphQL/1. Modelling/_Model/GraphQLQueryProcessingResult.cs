namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;
    using System.Collections.Generic;
    using GraphQL;
    using GraphQL.Instrumentation;
    using GraphQL.Language.AST;
    using GraphQL.NewtonsoftJson;
    using Newtonsoft.Json;

    [JsonConverter(typeof(ExecutionResultJsonConverter))]
    public class GraphQLQueryProcessingResult
    {
        // TODO: Remove everything we don't need. This will be most of the GraphQL specific typed properties.
        public object Data { get; set; }

        [JsonIgnore]
        public string DataAsString { get; }

        public ExecutionErrors Errors { get; }

        public string Query { get; }

        public global::GraphQL.Language.AST.Document Document { get; }

        public Operation Operation { get; }

        public PerfRecord[] Perf { get; }

        public Dictionary<string, object> Extensions { get; }

        public GraphQLQueryProcessingResult(object data, string dataAsString)
        {
            Data = data;
            DataAsString = dataAsString;
        }


        public GraphQLQueryProcessingResult(ExecutionResult result, string dataAsString)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));

            Data = result.Data;
            Errors = result.Errors;
            Query = result.Query;
            Operation = result.Operation;
            Document = result.Document;
            Perf = result.Perf;
            Extensions = result.Extensions;

            DataAsString = dataAsString;
        }

        public static ExecutionResult ToGraphQlExecutionResult(GraphQLQueryProcessingResult graphQLQueryProcessingResult)
        {
            return new()
            {
                Data = graphQLQueryProcessingResult.Data,
                Errors = graphQLQueryProcessingResult.Errors,
                Query = graphQLQueryProcessingResult.Query,
                Operation = graphQLQueryProcessingResult.Operation,
                Document = graphQLQueryProcessingResult.Document,
                Perf = graphQLQueryProcessingResult.Perf,
                Extensions = graphQLQueryProcessingResult.Extensions,
            };
        }

    }
}
