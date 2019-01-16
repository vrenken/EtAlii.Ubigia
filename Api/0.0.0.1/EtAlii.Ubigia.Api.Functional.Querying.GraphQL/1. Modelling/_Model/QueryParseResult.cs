namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Collections.Generic;
    using GraphQL;
    using GraphQL.Instrumentation;
    using Newtonsoft.Json;
    using GraphQL.Language.AST;

    public class QueryParseResult
    {
        public string Source { get; }

        public Query Query { get; }

        public QueryParserError[] Errors { get; }

        public QueryParseResult(string source, Query query, QueryParserError[] errors)
        {
            Source = source;
            Query = query;
            Errors = errors;
        }
    }
}
