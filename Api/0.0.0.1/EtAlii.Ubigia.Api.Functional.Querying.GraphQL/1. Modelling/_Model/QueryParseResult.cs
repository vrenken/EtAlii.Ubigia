namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
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
