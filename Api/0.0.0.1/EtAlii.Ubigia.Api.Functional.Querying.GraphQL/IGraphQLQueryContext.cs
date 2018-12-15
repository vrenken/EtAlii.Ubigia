namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;
    using GraphQL;

    public interface IGraphQLQueryContext
    {
        Task<QueryParseResult> Parse(string text);
        
        Task<QueryProcessingResult> Process(Query query);
    }
}